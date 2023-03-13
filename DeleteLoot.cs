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
            switch(_col)
            {
                case 2:
                    LoadT1();
                    break;
                case 4:
                    LoadT2();
                    break;
            }
        }

        private void LoadT1()
        {
            foreach(DateTime t in _looter.lootDates)
            {
                listBox1.Items.Add(t);
            }
        }

        private void LoadT2()
        {
            foreach (DateTime t in _looter.T2LootDates)
            {
                listBox1.Items.Add(t);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DateTime t = (DateTime)listBox1.SelectedItem;
            if(t != null)
            {
                switch(_col)
                {
                    case 2:
                        _looter.lootDates.Remove(t);
                        listBox1.Items.Remove(t);
                        dataChanged = true;
                        break;
                    case 4:
                        _looter.T2LootDates.Remove(t);
                        listBox1.Items.Remove(t);
                        dataChanged = true;
                        break;
                }
            }
        }
    }
}
