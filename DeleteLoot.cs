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
    public partial class DeleteLoot : Form
    {
        public bool dataChanged = false;

        Looter _looter;
        int _col;

        public DeleteLoot()
        {
            InitializeComponent();
        }

        public DeleteLoot(Looter looter, int col)
        {
            InitializeComponent();

            _looter = looter;
            _col = col;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteLoot_Load(object sender, EventArgs e)
        {
            labelHeader.Text = $"T{_col/2} loot dates for {_looter.Player}:";

            int tier;
            DateTime time;
            _looter.RestartIterator();
            do
            {
                (tier, time) = _looter.GetNextTime();
                if (tier == _col / 2)
                    listBox1.Items.Add(time.ToLocalTime());
            } while (tier != 0);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                DateTime t = (DateTime)listBox1.SelectedItem;
                if (_looter.DeleteTime(_col / 2, t.ToUniversalTime()))
                {
                    listBox1.Items.Remove(t);
                    dataChanged = true;
                }
            }
        }
    }
}
