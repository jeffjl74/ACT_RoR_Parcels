using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACT_RoR_Parcels
{
    public partial class ManageAlts : Form
    {
        public bool dataChanged = false;

        Looter _selected;
        List<Looter> _raiders;

        public ManageAlts()
        {
            InitializeComponent();
        }

        public ManageAlts(Looter looter, List<Looter> looters)
        {
            InitializeComponent();
            _selected = looter;
            _raiders = looters;
        }

        private void listBoxRaiders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxRaiders.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                if (!listBoxAlts.Items.Contains(listBoxRaiders.Items[index]))
                {
                    listBoxAlts.Items.Add(listBoxRaiders.Items[index]);
                    groupBoxLink.Enabled = true;
                }
            }
        }

        private void ManageAlts_Load(object sender, EventArgs e)
        {
            labelMain.Text = _selected.Player;
            this.Text = "Manage Alts for " + labelMain.Text;
            foreach (Looter looter in _raiders)
                listBoxRaiders.Items.Add(looter);

            foreach (string alt in _selected.Alts)
            {
				Looter looter = _raiders.Find(x => x.Player == alt);
                if(looter != null)
                    listBoxAlts.Items.Add(looter);
            }
        }

        private void buttonLink_Click(object sender, EventArgs e)
        {
            if (radioButtonAdd.Checked)
                LinkAdd();
            else if (radioButtonMax.Checked)
                LinkMax();
            else if (radioButtonMin.Checked)
                LinkMin();

        }

        private void LinkAdd()
        {
            if(listBoxAlts.Items.Count > 0)
            {
                // first pass, collect all the names and dates
                List<string> aliases = new List<string>();
                aliases.Add(_selected.Player);
                List<DateTime> t1 = new List<DateTime>();
                List<DateTime> t2 = new List<DateTime>();
                foreach (DateTime t in _selected.lootDates)
                    t1.Add(t);
                foreach (DateTime t in _selected.T2LootDates)
                    t2.Add(t);
                foreach (Looter looter in listBoxAlts.Items)
                {
                    aliases.Add(looter.Player);
                    foreach (DateTime t in looter.lootDates)
                    {
                        if (!t1.Contains(t))
                            t1.Add(t);
                    }
                    foreach (DateTime t in looter.T2LootDates)
                    {
                        if (!t2.Contains(t))
                            t2.Add(t);
                    }
                }

                // 2nd pass, set everybody to the combined lists
                dataChanged = true;
                _selected.lootDates = t1;
                _selected.T2LootDates = t2;
                foreach (Looter looter in listBoxAlts.Items)
                {
                    looter.lootDates = t1;
                    looter.T2LootDates = t2;
                }

                // 3rd pass, cross reference all the names
                _selected.Alts = aliases.Where(s => s != _selected.Player).ToList();
                foreach (Looter looter in listBoxAlts.Items)
                {
                    looter.Alts = aliases.Where(s => s != looter.Player).ToList();
                }
            }
        }

        private void LinkMax()
        {
            if(listBoxAlts.Items.Count > 0)
            { 
                // first pass, find the looter with the most items
                // and collect all the names
                List<string> aliases = new List<string>();
                aliases.Add(_selected.Player);
                List<DateTime> max1 = _selected.lootDates;
                List<DateTime> max2 = _selected.T2LootDates;
                foreach (Looter looter in listBoxAlts.Items)
                {
                    aliases.Add(looter.Player);
                    if (looter.T1Count > max1.Count)
                    {
                        max1 = looter.lootDates;
                    }
                    if (looter.T2Count > max2.Count)
                    {
                        max2 = looter.T2LootDates;
                    }
                }

                // 2nd pass, set everybody to the max lists
                _selected.lootDates = max1;
                _selected.T2LootDates = max2;
                foreach (Looter looter in listBoxAlts.Items)
                {
                    looter.lootDates = max1;
                    looter.T2LootDates = max2;
                }

                // 3rd pass, cross reference all the names
                _selected.Alts = aliases.Where(s => s != _selected.Player).ToList();
                foreach (Looter looter in listBoxAlts.Items)
                {
                    looter.Alts = aliases.Where(s => s != looter.Player).ToList();
                }
                dataChanged = true;
            }
        }

        private void LinkMin()
        {
            if (listBoxAlts.Items.Count > 0)
            {
                // first pass, find the looter with the fewest items
                // and collect all the names
                List<string> aliases = new List<string>();
                aliases.Add(_selected.Player);
                List<DateTime> min1 = _selected.lootDates;
                List<DateTime> min2 = _selected.T2LootDates;
                foreach (Looter looter in listBoxAlts.Items)
                {
                    aliases.Add(looter.Player);
                    if (looter.T1Count < min1.Count)
                    {
                        min1 = looter.lootDates;
                    }
                    if (looter.T2Count < min2.Count)
                    {
                        min2 = looter.T2LootDates;
                    }
                }

                // 2nd pass, set everybody to the min lists
                _selected.lootDates = min1;
                _selected.T2LootDates = min2;
                foreach (Looter looter in listBoxAlts.Items)
                {
                    looter.lootDates = min1;
                    looter.T2LootDates = min2;
                }

                // 3rd pass, cross reference all the names
                _selected.Alts = aliases.Where(s => s != _selected.Player).ToList();
                dataChanged = true;
                foreach (Looter looter in listBoxAlts.Items)
                {
                    looter.Alts = aliases.Where(s => s != looter.Player).ToList();
                }
            }
        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            int index = listBoxAlts.SelectedIndex;
            if (index != ListBox.NoMatches)
            {
                Looter single = (Looter)listBoxAlts.Items[index];
                single.lootDates = new List<DateTime>(_selected.lootDates);
                single.T2LootDates = new List<DateTime>(_selected.T2LootDates);

                _selected.Alts.Remove(single.ToString());
                foreach (Looter looter in listBoxAlts.Items)
                {
                    if(looter.Player != single.ToString())
                        looter.Alts.Remove(single.ToString());
                    else
                        looter.Alts.Remove(_selected.Player);
                }

                listBoxAlts.Items.Remove(listBoxAlts.Items[index]);
            }
        }

        private void listBoxAlts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxAlts.SelectedIndex;
            if (index != ListBox.NoMatches)
            {
                groupBoxSplit.Enabled = true;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
