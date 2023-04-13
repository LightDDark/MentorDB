using Domain.Out;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<UiComplete> CalculateSchedule(string userId, AlgoComplete missions) {
            // call algo with missions
            
            // return algo results
            return new UiComplete();
        }
    }
}
