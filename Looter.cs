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
        public int Count { get { return DateCount(); }  }

        public List<DateTime> lootDates { get; set; } = new List<DateTime>();

        [XmlIgnore]
        public int InRaid { get; set; } = -1;

        public int CompareTo(Looter other)
        {
            return this.Player.CompareTo(other.Player);
        }

        public int DateCount()
        {
            return lootDates.Count;
        }

        public override string ToString()
        {
            return Player;
        }
    }
}
