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

        private void btn_order_Click(object sender, EventArgs e)
        {
            string del = "delete from order_data where tno = '" + DC.ot_no + "'";
            SqlDataAdapter dad = new SqlDataAdapter(del, DC.con);
            DataTable dtd = new DataTable();
            dad.Fill(dtd);

            foreach (DataGridViewRow row in DC.dg.Rows)
            {
                string it = row.Cells["Item"].Value.ToString();
                int qty = Convert.ToInt32(row.Cells["Qty"].Value.ToString());
                int prc = Convert.ToInt32(row.Cells["Price"].Value.ToString());
                string ins = "insert into order_data(tno,item,qty,price,t_amount) values('" + DC.ot_no + "','" + it + "','" + qty + "','" + prc + "','" + DC.totalbill + "')";
                SqlDataAdapter dai = new SqlDataAdapter(ins, DC.con);
                DataTable dti = new DataTable();
                dai.Fill(dti);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DC.oidno = Convert.ToInt32(comboBox1.Text);
            DC.openChildForm(new MenuItem(p_fm, this, DC.oidno), this.p_fm);
            if (DC.dg.Rows.Count > 0)
            {
                DC.dg.Rows.Clear();
            }
            DC.t_no = Convert.ToInt32(comboBox1.Text);
            DC.ot_no = Convert.ToInt32(comboBox1.Text);
            int am = 0, gst;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
