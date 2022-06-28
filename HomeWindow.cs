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
        DataGridViewRow row = new DataGridViewRow();
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

            string sel = "select tno,item,qty,price from order_data where tno = '" + DC.ot_no + "'";
            SqlDataAdapter das = new SqlDataAdapter(sel, DC.con);
            DataTable dts = new DataTable();
            das.Fill(dts);
            foreach (DataRow r in dts.Rows)
            {
                am += Convert.ToInt32(r["price"].ToString());
                DC.dg.Rows.Add(new object[] { r["tno"].ToString(), r["item"].ToString(), r["qty"].ToString(), r["price"].ToString() });
            }
            DC.totalbill = am;
            gst = am * 5 / 100;
            DC.totalgst = gst;
            Lable_Gst.Text = gst.ToString();
            Label_Amount.Text = am.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_bill_Click(object sender, EventArgs e)
        {
            int height = DC.dg.Height;
            DC.dg.Height = DC.dg.RowCount * DC.dg.RowTemplate.Height + 200;
            bitmap = new Bitmap(DC.dg.Width, DC.dg.Height);
            DC.dg.DrawToBitmap(bitmap, new Rectangle(0, 0, DC.dg.Width, DC.dg.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.Document.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("prnm", DC.dg.Width, DC.dg.Height);
            printPreviewDialog1.ShowDialog();
            DC.dg.Height = height;

            string del = "delete from order_data where tno = '" + DC.ot_no + "'";
            SqlDataAdapter dad = new SqlDataAdapter(del, DC.con);
            DataTable dtd = new DataTable();
            dad.Fill(dtd);

            string ot = "table";
            string ins = "insert into total_earnings (tno,date,amount,o_type) values ('" + DC.ot_no + "','" + dt + "','" + DC.totalbill + "','" + ot + "')";
            SqlDataAdapter dai = new SqlDataAdapter(ins, DC.con);
            DataTable dti = new DataTable();
            dai.Fill(dti);

            DC.dg.Rows.Clear();
            Label_Amount.Text = "0";
            Lable_Gst.Text = "0";
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Restro name", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Brushes.Black, 100, 10);
            e.Graphics.DrawString("Takeaway Id : " + takeaway_id.ToString(), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Brushes.Black, 150, 40);
            e.Graphics.DrawImage(bitmap, 0, 60);
            e.Graphics.DrawString(" Total GST : " + DC.totalgst.ToString() + " \n " + "Total Bill : " + DC.totalbill.ToString(), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Brushes.Black, 150, printPreviewDialog1.Document.DefaultPageSettings.PaperSize.Height - 40);
        }

        private void btn_takeaway_Click(object sender, EventArgs e)
        {
            if (i == 0)
            {
                DC.openChildForm(new MenuItem(p_fm, this, DC.oidno), this.p_fm);
                i = 1;
            }
            else if (i == 1)
            {
                takeawaydataget t = new takeawaydataget();
                t.ShowDialog();

                string sel = "select * from takeaway_data";
                SqlDataAdapter das = new SqlDataAdapter(sel, DC.con);
                DataTable dts = new DataTable();
                das.Fill(dts);

                if (dts.Rows.Count > 0)
                {
                    takeaway_id = Convert.ToInt32(dts.Rows[dts.Rows.Count - 1]["id"].ToString());
                }
                else
                {
                    takeaway_id = 1;
                }
                int height = DC.dg.Height;
                DC.dg.Height = DC.dg.RowCount * DC.dg.RowTemplate.Height + 200;
                bitmap = new Bitmap(DC.dg.Width, DC.dg.Height);
                DC.dg.DrawToBitmap(bitmap, new Rectangle(0, 0, DC.dg.Width, DC.dg.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.Document.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("prnm", DC.dg.Width, DC.dg.Height);
                printDocument1.PrinterSettings.Copies = 2;
                printPreviewDialog1.ShowDialog();
                DC.dg.Height = height;

                string ins1 = "insert into takeaway_data (guest_nm,guest_num,price) values ('" + DC.t_nm + "','" + DC.t_num + "','" + DC.totalbill + "')";
                SqlDataAdapter dai1 = new SqlDataAdapter(ins1, DC.con);
                DataTable dti1 = new DataTable();
                dai1.Fill(dti1);

                string ot = "takeaway";
                string ins2 = "insert into total_earnings (tno,date,amount,o_type) values ('" + DC.ot_no + "','" + dt + "','" + DC.totalbill + "','" + ot + "')";
                SqlDataAdapter dai = new SqlDataAdapter(ins2, DC.con);
                DataTable dti = new DataTable();
                dai.Fill(dti);

                DC.dg.Rows.Clear();
                Label_Amount.Text = "0";
                Lable_Gst.Text = "0";
                DC.totalbill = 0;
                DC.totalgst = 0;
            }
        }
    }
}
