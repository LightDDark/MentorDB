using Domain.Priv;
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
        private Rrule? rule;

        [Key] public int Id { get; set; }
        public string Title { get; set; } = new string("Default");
        public string Description { get; set; } = new string("Default");
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool AllDay { get; set; }

        public string getRule()
        {
            if (rule == null) return "";
            return rule.ToString();
        }
        public Rrule? Rule
        {
            set {
                rule = value;
            }
        }
    }
}
