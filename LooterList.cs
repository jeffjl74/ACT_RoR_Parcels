using System.Collections.Generic;
using System.Xml.Serialization;

namespace ACT_RoR_Parcels
{
    // just need this class to allow specification of a binding list at design time
    [XmlRoot]
    public class LooterList
    {
        public List<Looter> Looters { get; set; } = new List<Looter>();
    }
}
