using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AatmanProject_.net_
{
    public partial class MenuItem : Form
    {
        public static Panel main;
        public static HomeWindow fn;
        int totcat,oid;
        int pxloc = 30  , pyloc = 80 , lxloc = 100  , lyloc = 240 ;
        
        public MenuItem()
        {
            InitializeComponent();
        }

        public MenuItem(Panel p,HomeWindow f, int oi)
        {
            InitializeComponent();
            main = p;
            fn = f;
            oid = oi;
        }
        private void MenuItem_Load(object sender, EventArgs e)
        {
            string sel = "select * from categories";
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
                string i_path = Path.Combine(dts.Rows[i]["c_img"].ToString());
                p.Image = Image.FromFile(i_path);
                p.SizeMode = PictureBoxSizeMode.Zoom;


                Label l = new Label();
                this.Controls.Add(l);
                l.Name = dts.Rows[i]["c_name"].ToString();
                l.Size = new Size(200, 50);
                l.Location = new System.Drawing.Point(pxloc + 30, pyloc + 180);
                l.Text = dts.Rows[i]["c_name"].ToString();
                l.Cursor = Cursors.Hand;
                l.Click += new System.EventHandler(this.l_Click);

                pxloc += 230;


                if (p.Location.X > 600)
                {
                    pyloc += 250;
                    pxloc = 30;
                }
            }
        }

    }
}