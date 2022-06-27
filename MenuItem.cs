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
        
    }
}