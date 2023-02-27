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
                case "T1 Count":
                case "T2 Count":
                    int first;
                    if(_field == "T1 Count")
                        first = x.T1Count.CompareTo(y.T1Count);
                    else
                        first = x.T2Count.CompareTo(y.T2Count);
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
                case "T1 Dates":
                case "T2 Dates":
                    List<DateTime> xList;
                    if (_field == "T1 Dates")
                        xList = x.lootDates;
                    else
                        xList = x.T2LootDates;
                    List<DateTime> yList;
                    if(_field == "T1 Dates")
                        yList = y.lootDates;
                    else
                        yList = y.T2LootDates;
                    if (xList.Count == 0 && yList.Count > 0)
                        return 1;
                    else if (yList.Count == 0 && xList.Count > 0)
                        return -1;
                    else if (xList.Count == 0 && yList.Count == 0)
                    {
                        // no loot dates
                        // secondary sort by name, always ascending
                        if (_order == SortOrder.Ascending)
                            return x.Player.CompareTo(y.Player);
                        else
                            return y.Player.CompareTo(x.Player);
                    }
                    else
                    {
                        // both have loot dates
                        int xIdx = xList.Count - 1;
                        int yIdx = yList.Count - 1;
                        first = xList[xIdx].CompareTo(yList[yIdx]);
                        if (first == 0)
                        {
                            // secondary sort by name, always ascending
                            // really shouldn't ever get here
                            if (_order == SortOrder.Ascending)
                                return x.Player.CompareTo(y.Player);
                            else
                                return y.Player.CompareTo(x.Player);
                        }
                        else
                            return first;
                    }
                case "In Raid":
                    first = x.InRaid.CompareTo(y.InRaid);
                    if(first == 0)
                    {
                        // secondary sort is T1 Count, always ascending
                        int second;
                        if (_order == SortOrder.Descending)
                            second = y.T1Count.CompareTo(x.T1Count);
                        else
                            second = x.T1Count.CompareTo(y.T1Count);
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
                case "In Raid2":
                    first = x.InRaid.CompareTo(y.InRaid);
                    if (first == 0)
                    {
                        // secondary sort is T2 Count, always ascending
                        int second;
                        if (_order == SortOrder.Descending)
                            second = y.T2Count.CompareTo(x.T2Count);
                        else
                            second = x.T2Count.CompareTo(y.T2Count);
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
