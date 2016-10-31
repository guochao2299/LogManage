using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogManage.LogAnalysis
{
    public partial class frmWaiting : Form
    {
        private static frmWaiting splashScreen;

        public frmWaiting()
        {
            InitializeComponent();
        }

        public static frmWaiting SplashScreen
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

        public static void ShowWaitingScreen(Form parent)
        {
            splashScreen = new frmWaiting();
            splashScreen.StartPosition = FormStartPosition.CenterScreen;
            splashScreen.Show(parent);
        }
    }
}
