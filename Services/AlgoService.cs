using Domain;
using Domain.In;
using Domain.Out;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<UiComplete?> CalculateSchedule(string userId, ScheduleSetting setting, List<int> missionsId) {
            // pre-process
            User? user = await UserWithAll(userId);

            if (user == null) {
                return null;
            }

            List<AlgoMission> missions = new List<AlgoMission>();
            user.Missions.ForEach(x => {
                missions.Add(x.ToAlgo());
            });

            AlgoComplete complete = new AlgoComplete() { AlgoMission = missions, ScheduleSetting = setting };
            // call algo with complete
            
            HttpClientHandler handler = new HttpClientHandler() {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient client = new HttpClient(handler);
            
            client.BaseAddress = new Uri("https://api.stackexchange.com/2.2/"); // algo adress
            HttpResponseMessage response = client.GetAsync("answers?order=desc&sort=activity&site=stackoverflow").Result; // spesific get
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Result: " + result);


            List<InAlgo> results = new List<InAlgo>();
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
            return new UiComplete() {settings = setting, missions = user.Missions.Select(x => x.ToUi()).ToList() };
        }
    }
}
