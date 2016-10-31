using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogManage
{
    public partial class frmSplash : Form
    {
        private static frmSplash splashScreen;
        public frmSplash()
        {
            InitializeComponent();

            this.TransparencyKey = SystemColors.Control;
        }

        public static frmSplash SplashScreen
        {
            get
            {
                return splashScreen;
            }
            set
            {
                splashScreen = value;
            }
        }

        public static void ShowSplashScreen()
        {
            splashScreen = new frmSplash();
            splashScreen.Show();
        }
    }
}
