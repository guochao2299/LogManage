using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

using LogManage.DataType;
using LogManage.SelfDefineControl;

namespace LogManage.SelfDefineControl
{
    internal class AuditTabPage:TabPage
    {
        public AuditTabPage(string appGuid):base()
        {
            ucAudit audit = new ucAudit();
            audit.ResetApp(appGuid);
            audit.Dock = DockStyle.Fill;

            this.Text = audit.SuggestText;

            this.Controls.Add(audit);
        }
    }
}
