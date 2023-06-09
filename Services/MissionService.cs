using Domain;
using Domain.Out;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services {
    public class MissionService {

        private readonly MentorDataContext _context;

        public MissionService(MentorDataContext context) {
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

        public async Task<List<UiMission>> GetMissions(string userId) {

            User ? user = await UserWithAll(userId);

            if (user == null) {
                return null;
            }

            List<UiMission> missions = new List<UiMission>();
            user.Missions.ForEach(x => {
                missions.Add(
                    new UiMission() { Id = x.Id, AllDay = x.AllDay, Description = x.Description, EndDate = x.EndDate, StartDate = x.StartDate, Title = x.Title, Type = x.Type});
            });

            return missions;
        }

        public async Task<bool> UpdateMission(UiMission mission, string userId) {

            User? user = await UserWithAll(userId);

            if (user == null) {
                return false;
            }

            Mission? user_mission = user.Missions.Find(m => m.Id == mission.Id);

            if (user_mission == null) {
                return false;
            }

            user_mission.AllDay = mission.AllDay;
            user_mission.Description = mission.Description;
            user_mission.EndDate = mission.EndDate;
            user_mission.StartDate = mission.StartDate;
            user_mission.Title = mission.Title;
            user_mission.Type = mission.Type;

            _context.Entry(user_mission).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return true;
        }

        public async Task<int?> AddMission(Mission mission, string userId) {
            if (_context.User == null) {
                return null;
            }
            User? user = await UserWithAll(userId);
            if (user == null) {
                return null;
            }
            if (user.Missions == null) {
                user.Missions = new List<Mission>();
            }
            user.Missions.Add(mission);
            _context.Entry(user).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return mission.Id;
        }

        public async Task<bool?> DeleteMission(int missionId, string userId) {
            if (_context.User == null) {
                return null;
            }
            User? user = await UserWithAll(userId);
            if (user == null) {
                return null;
            }

            int numRemoved = user.Missions.RemoveAll(x => {
                return x.Id == missionId;
            });

            _context.Entry(user).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            if (numRemoved == 0) {
                return false;
            }

            return true;
        }
    }
}
