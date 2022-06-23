using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AatmanProject_.net_
{
    class DC
    {//connection
        public static string server = "127.0.0.1";
        public static string un = "root";
        public static string pwd = "";
        public static string db = "restroapp";
        public static SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\Aatman\AatmanProject(.net)\AatmanProject(.net)\bin\Debug\AatmanProject(.net).mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");

        //for order , billing
        public static int t_no;
        public static int ot_no;
        public static int oidno;
        public static DataGridView dg;
        public static int totalbill;
        public static int totalgst;


        //for takeaway
        public static string t_nm;
        public static long t_num;

        public static void order(HomeWindow f, int oid, string name, int price)
        {
            totalbill += price;
            totalgst = totalbill * 5 / 100;
          //  f.Label_Amount.Text = DC.totalbill.ToString();
           // f.Lable_Gst.Text = DC.totalgst.ToString();
            dg.Rows.Add(new object[] { oid, name, 1, price });
        }
        public static int return_totalbill()
        {
            return totalbill;
        }


        /*
         * Live Connection //Don't mind it
         * 
         * public static string server = "sql101.epizy.com";
           public static string un = "epiz_30442405";
           public static string pwd = "AatmanKacha@79";
           public static string db = "epiz_30442405_restroapp";
           public static MySqlConnection con = "SERVER= address;PORT=3306;DATABASE=nameofdatabase;UID=uid;PASSWORD=password
         */

        public static Form activeForm = null;
        public static void openChildForm(Form childForm, Panel p)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            p.Controls.Add(childForm);
            p.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
