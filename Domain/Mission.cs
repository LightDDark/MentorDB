using Domain.Out;
using Domain.Priv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Mission
    {
        public enum Prior {
            [Description("high")] High,
            [Description("medium")]  Meduim,
            [Description("low")]  Low
        }
        /*private Rrule? rule;*/
        private bool allDay;

        [Key] public int Id { get; set; }
        public string Title { get; set; } = new string("Default");
        public string? Description { get; set; } = new string("Default");
        public string? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Length { get; set; } = 2;
        public List<string> OptionalDays { get; set; } = new List<string>();
        public string[] OptionalHours { get; set; } = new string[2];
        public DateTime DeadLine { get; set; }

        public bool AllDay {
            get {
                return allDay;
            }
            set {
                if (value == true) {
                    OptionalHours[0] = "9:00:00";
                    OptionalHours[1] = "18:00:00";
                }
                allDay = value;
            }
        }
        public bool IsRepeat { get; set; }

        /*public string getRule()
        {
            if (rule == null) return "";
            return rule.ToString();
        }
        public Rrule? Rule
        {
            set {
                rule = value;
                IsRepeat = true;
            }
        }*/
        public List<MissionRank> RankListHistory { get; set; } = new List<MissionRank>();
        public Prior Priority { get; set; }

        public AlgoMission ToAlgo() {
            return new AlgoMission {
                OptionalDays = OptionalDays,
                OptionalHours = OptionalHours,
                Id = Id,
                DeadLine = DeadLine,
                IsRepeat = IsRepeat,
                Length = Length,
                Priority = Priority.ToString(),
                RankListHistory = new List<MissionRank>(RankListHistory),
                Type = Type
            };
        }

        public UiMission ToUi() {
            return new UiMission {
                Id = Id,
                Title = Title,
                Description = Description,
                Type = Type,
                StartDate = StartDate,
                EndDate = EndDate,
                AllDay = allDay
            };
        }
    }
}
