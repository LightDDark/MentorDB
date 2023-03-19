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
    }
}
