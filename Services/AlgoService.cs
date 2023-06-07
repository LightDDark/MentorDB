﻿using Domain;
using Domain.In;
using Domain.Out;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class AlgoService
    {
        private readonly MentorDataContext _context;

        public AlgoService(MentorDataContext context)
        {
            _context = context;
        }

        private async ValueTask<User?> UserWithAll(string userId) {
            if (_context.User == null) {
                return null;
            }
            User? user = await _context.User.FindAsync(userId);

            if (user == null) {
                return null;
            }

            user = await _context.User.Include(x => x.Missions).Include(x => x.Schedule).FirstOrDefaultAsync(u => u.Id == user.Id);
            return user;
        }

        public async Task<UiComplete?> CalculateSchedule(string userId, JointMissions missionListSetting) {
            // pre-process
            User? user = await UserWithAll(userId);

            if (user == null) {
                return null;
            }

            List<AlgoMission> missions = new List<AlgoMission>();
            List<int> missionIdList = missionListSetting.MissionsId;
            List<Mission> missionsComplete = await _context.Mission.Include(y => y.OptionalDays).Include(y => y.OptionalHours).Include(y => y.RankListHistory).Where(y => missionIdList.Contains(y.Id)).ToListAsync();
            missionsComplete.ForEach(x => {
                missions.Add(x.ToAlgo());
            });

            AlgoComplete complete = new AlgoComplete() { AlgoMission = missions, ScheduleSetting = missionListSetting.Setting };
            // call algo with complete
            var myContent = JsonConvert.SerializeObject(complete);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClientHandler handler = new HttpClientHandler() {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient client = new HttpClient(handler);
            
            client.BaseAddress = new Uri("http://localhost:5000/algoComplete"); // algo address
            HttpResponseMessage response = client.PostAsync("algoComplete", byteContent).Result;
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Result: " + responseString);
            List<InAlgo> results = JsonConvert.DeserializeObject<List<InAlgo>>(responseString);


            // save algo results
            results.ForEach(x => {
                user.Missions.Find(a => a.Id == x.Id).update(x);
            });

            _context.Entry(user).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            // return algo results
            return new UiComplete() {settings = missionListSetting.Setting, missions = user.Missions.Select(x => x.ToUi()).ToList() };
        }
    }
}
