using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Mission
    {
        [Key] public int Id { get; set; }
        public string Title { get; set; } = new string("Default");
        public string Descroption { get; set; } = new string("Default");
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
