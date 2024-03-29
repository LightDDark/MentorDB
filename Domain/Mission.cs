﻿using Domain.In;
using Domain.Out;
using Domain.Priv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
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
        public int Length { get; set; } = 60;
        public List<DayString> OptionalDays { get; set; } = new List<DayString>();
        public List<HourString> OptionalHours { get; set; } = new List<HourString>() ;
        public DateTime DeadLine { get; set; }

        public bool AllDay {
            get {
                return allDay;
            }
            set {
                if (value == true) {
                    OptionalHours[0].Hour = "9:00:00";
                    OptionalHours[1].Hour = "18:00:00";
                }
                allDay = value;
            }
        }
        public bool IsRepeat { get; set; } = false;

        public List<MissionRank> RankListHistory { get; set; } = new List<MissionRank>();
        public Prior Priority { get; set; }

        public bool Settled { get; set; } = false;

        public int Rank { get; set; } = 4;

        public AlgoMission ToAlgo() {
            return new AlgoMission {
                OptionalDays = OptionalDays.Select(x => x.Day).ToList(),
                OptionalHours = OptionalHours.Select(x => x.Hour).ToList(),
                Id = Id,
                DeadLine = DeadLine,
                IsRepeat = IsRepeat,
                Length = Length,
                Description = Description,
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
                Settled = Settled,
            };
        }

        public void update(InAlgo algoMission) {
            EndDate = algoMission.EndDate;
            StartDate = algoMission.StartDate;
            Settled = true;
        }
    }
}
