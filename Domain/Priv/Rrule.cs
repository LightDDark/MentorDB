using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Priv
{
    public class Rrule
    {
        public enum Freq
        {
            Daily,
            Weekly,
            Monthly,
            Yearly
        }
        private static Dictionary<Freq, string> freqDescription = new Dictionary<Freq, string>()
            {
                {Freq.Daily,  "Daily"},
                {Freq.Weekly,  "Weekly"},
                {Freq.Monthly, "Monthly"},
                {Freq.Yearly, "Yearly"}
            };
        private static Dictionary<string, Freq> descFreq = new Dictionary<string, Freq>()
            {
                {"Daily", Freq.Daily},
                {"Weekly", Freq.Weekly},
                {"Monthly", Freq.Monthly},
                {"Yearly", Freq.Yearly}
            };
        private Freq freq;
        private int[] cid = new int[2];
        private DateTime? until = null;


        public Rrule(Freq freq, DateTime until)
        {
            this.freq = freq;
            this.until = until;
        }
        public Rrule(Freq freq, int count = 0, int interval = 0)
        {

            this.freq = freq;
            this.cid[0] = count;
            this.cid[1] = interval;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FREQ=");
            sb.Append(freqDescription.GetValueOrDefault(this.freq));
            if (until != null)
            {
                sb.Append(";UNTIL=");
                sb.Append(until.ToString());
                return sb.ToString();
            }

            if (cid[0] != 0)
            {
                sb.Append(";COUNT=");
                sb.Append(cid[0]);
            }
            if (cid[1] != 0)
            {
                sb.Append(";INTERVAL=");
                sb.Append(cid[1]);
            }
            return sb.ToString();
        }

        public static Rrule fromString(string rule)
        {
            Freq? f = null;
            int count = 0, interval = 0;
            DateTime? d = null;
            string[] subs = rule.Split(';');
            foreach (string item in subs)
            {
                string[] subsub = item.Split('=');
                if (subsub[0] == "FREQ")
                {
                    f = descFreq.GetValueOrDefault(subsub[1]);
                }
                else if (subsub[0] == "COUNT")
                {
                    count = int.Parse(subsub[1]);
                }
                else if (subsub[0] == "INTERVAL")
                {
                    interval = int.Parse(subsub[1]);
                }
                else
                {
                    d = DateTime.Parse(subsub[1]);
                }
            }
            if (d != null)
            {
                return new Rrule((Freq)f, (DateTime)d);
            }
            return new Rrule((Freq)f, count, interval);
        }
    }
}
