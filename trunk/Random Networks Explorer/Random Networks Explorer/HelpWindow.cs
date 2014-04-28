using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace RandomNetworksExplorer
{
    public partial class HelpWindow : Form
    {
        public HelpWindow(string url)
        {
            InitializeComponent();

            // TODO read help path from config file
            string curDir = "D:\\Disertation\\System (code)\\Last Version\\Random Networks Explorer";
            this.webBrowser.Url = new Uri(String.Format(@"file:///{0}/" + url, curDir));
        }
    }
}
