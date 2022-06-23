using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace AatmanProject_.net_
{
    public partial class HomeWindow : Form
    {
        DGVPrinter printer = new DGVPrinter();
        Bitmap bitmap;
        int takeaway_id, i = 0;
        string dt = DateTime.Now.Date.ToString();
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DC.openChildForm(new MenuItem(p_fm, this, DC.oidno), this.p_fm);
            i = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void p_fm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HomeWindow_Load(object sender, EventArgs e)
        {
            DC.dg = this.grid;  
        }
    }
}
