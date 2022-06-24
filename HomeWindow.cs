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
            DC.openChildForm(new MenuItem(btn_bill, this, DC.oidno), this.btn_bill);
            i = 1;
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

            int height = DC.dg.Height;
            DC.dg.Height = DC.dg.RowCount * DC.dg.RowTemplate.Height + 200;
            bitmap = new Bitmap(DC.dg.Width, DC.dg.Height);
            DC.dg.DrawToBitmap(bitmap, new Rectangle(0, 0, DC.dg.Width, DC.dg.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.Document.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("prnm", DC.dg.Width, DC.dg.Height);
            printPreviewDialog1.ShowDialog();
            DC.dg.Height = height;

            DC.dg.Rows.Clear();
            Label_Amount.Text = "0";
            Lable_Gst.Text = "0";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}
