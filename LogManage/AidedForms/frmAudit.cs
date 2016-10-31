using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;
using LogManage.Services;
using LogManage.SelfDefineControl;

namespace LogManage.AidedForms
{
    public partial class frmAudit : Form
    {
        public frmAudit(string appGuid)
        {
            InitializeComponent();
           
            this.ucAudit1.ResetApp(appGuid);
            this.Text = this.ucAudit1.SuggestText;
        }
    }
}
