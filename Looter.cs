using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACT_RoR_Parcels
{
    public class Looter : IComparable<Looter>
    {
        [XmlAttribute]
        public string Player { get; set; }
        [XmlAttribute]
        public int T1Count { get { return lootDates.Count; }  }
        public int T2Count { get { return T2LootDates.Count; }  }

        public List<string> Alts { get; set; } = new List<string>();

        public List<DateTime> lootDates { get; set; } = new List<DateTime>();

        public List<DateTime> T2LootDates { get; set; } = new List<DateTime>();

        [XmlIgnore]
        public int InRaid { get; set; } = -1;

        [XmlIgnore]
        int t1Index = -1;
        [XmlIgnore]
        int t2Index = -1;

        public int CompareTo(Looter other)
        {
            return this.Player.CompareTo(other.Player);
        }

        public override string ToString()
        {
            return Player;
        }

        public void RestartIterator()
        {
            t1Index = -1;
            t2Index = -1;
        }

        public bool IterationsDone()
        {
            return t1Index+1 == T1Count && t2Index+1 == T2Count;
        }

        public (int tier, DateTime time) GetNextTime()
        {
            if (t1Index+1 < T1Count)
            {
                t1Index++;
                return (1, lootDates[t1Index]);
            }
            else if (t2Index+1 < T2Count)
            {
                t2Index++;
                return (2, T2LootDates[t2Index]);
            }
            else
                return (0, DateTime.MinValue);
        }

        public bool AddTime(int tier, DateTime time)
        {
            bool added = false;
            if(tier == 1)
            {
                if (!lootDates.Contains(time))
                {
                    lootDates.Add(time);
                    added = true;
                }
            }
            else if(tier == 2)
            {
                if (!T2LootDates.Contains(time))
                {
                    T2LootDates.Add(time);
                    added = true;
                }
            }
            return added;
        }

        public bool DeleteTime(int tier, DateTime time)
        {
            bool removed = false;
            if (tier == 1)
            {
                if (lootDates.Contains(time))
                {
                    lootDates.Remove(time);
                    removed = true;
                }
            }
            else if (tier == 2)
            {
                if (T2LootDates.Contains(time))
                {
                    T2LootDates.Remove(time);
                    removed = true;
                }
            }
            return removed;
        }
    }
}
