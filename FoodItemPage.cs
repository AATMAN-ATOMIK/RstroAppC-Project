using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AatmanProject_.net_
{
    public partial class FoodItemPage : Form
    {
        int totcat, oid;
        int pxloc = 50, pyloc = 80;
        string nnm, nm, prc;
        HomeWindow fn;
        Panel main;
        public FoodItemPage(HomeWindow f, Panel p, int oi)
        {
            InitializeComponent();
            fn = f;
            main = p;
            oid = oi;
        }


        private void FoodItemPage_Load(object sender, EventArgs e)
        {
            string sel = "select * from "+DC.cat+"";
            SqlDataAdapter das = new SqlDataAdapter(sel, DC.con);
            DataTable dts = new DataTable();
            das.Fill(dts);

            totcat = dts.Rows.Count;
            for (int i = 0; i < totcat; i++)
            {
                PictureBox p = new PictureBox();
                this.Controls.Add(p);
                p.Name = "P_Item" + i.ToString();
                p.Size = new Size(200, 150);
                p.Location = new System.Drawing.Point(pxloc, pyloc);
                string i_path = Path.Combine(dts.Rows[i]["img"].ToString());
                p.Image = Image.FromFile(i_path);
                p.SizeMode = PictureBoxSizeMode.Zoom;


                Label l = new Label();
                this.Controls.Add(l);
                l.Name = dts.Rows[i]["name"].ToString();
                l.Size = new Size(200, 50);
                l.Location = new System.Drawing.Point(pxloc + 40, pyloc + 170);
                l.Text = dts.Rows[i]["name"].ToString() + " \nRs. " + dts.Rows[i]["price"].ToString(); ;
                l.Click += new System.EventHandler(this.l_Click);



                pxloc += 250;


                if (p.Location.X > 500)
                {
                    pyloc += 250;
                    pxloc = 50;
                }
            }
        }
        public void l_Click(object sender, System.EventArgs e)
        {
            nm = sender.ToString();
            nnm = nm.Substring(34);
            prc = nm.Substring(nm.Length - 3);
            DC.order(fn, oid, nnm, Convert.ToInt32(prc));
        }
    }
}