using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AatmanProject_.net_
{
    public partial class takeawaydataget : Form
    {
        public takeawaydataget()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tnum.Text != "" || tnum.Text != "")
            {
                DC.t_nm = tnum.Text;
                DC.t_num = Convert.ToInt64(tnum.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please provide info!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
