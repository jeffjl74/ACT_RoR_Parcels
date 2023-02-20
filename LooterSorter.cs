using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACT_RoR_Parcels
{
    public class LooterSorter : IComparer<Looter>
    {
        string _field;
        SortOrder _order;

        public LooterSorter(string field, SortOrder order)
        {
            _order = order;
            _field = field;
        }

        public int Compare(Looter x, Looter y)
        {
            if(_order == SortOrder.Descending)
            {
                Looter tmp = x;
                x = y;
                y = tmp;
            }

            switch(_field)
            {
                case "Player":
                    return x.Player.CompareTo(y.Player);
                case "Count":
                    int first = x.Count.CompareTo(y.Count);
                    if (first == 0)
                    {
                        // secondary sort is by InRaid, always descending
                        int second;
                        if (_order == SortOrder.Descending)
                            second = x.InRaid.CompareTo(y.InRaid);
                        else
                            second = y.InRaid.CompareTo(x.InRaid);
                        if (second == 0)
                        {
                            // tertiary name sort is always ascending
                            if (_order == SortOrder.Ascending)
                                return x.Player.CompareTo(y.Player);
                            else
                                return y.Player.CompareTo(x.Player);
                        }
                        else
                            return second;
                    }
                    else
                        return first;
                case "Dates":
                    int xIdx = 0;
                    int yIdx = 0;
                    if(_order == SortOrder.Descending)
                    {
                        xIdx = x.lootDates.Count - 1;
                        yIdx = y.lootDates.Count - 1;
                    }
                    return x.lootDates[xIdx].CompareTo(y.lootDates[yIdx]);
                case "InRaid":
                    first = x.InRaid.CompareTo(y.InRaid);
                    if(first == 0)
                    {
                        // secondary sort is Count, always ascending
                        int second;
                        if (_order == SortOrder.Descending)
                            second = y.Count.CompareTo(x.Count);
                        else
                            second = x.Count.CompareTo(y.Count);
                        if (second == 0)
                        {
                            // tertiary name sort is always ascending
                            if (_order == SortOrder.Ascending)
                                return x.Player.CompareTo(y.Player);
                            else
                                return y.Player.CompareTo(x.Player);
                        }
                        else
                            return second;
                    }
                    return first;
                default:
                    return x.Player.CompareTo(y.Player);
            }
        }
    }
}
