using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LogManage.DataType
{
    public static class CGeneralFuncion
    {
        public static DialogResult ShowWindow(Form parent,Form f,bool isModalForm)
        {
            f.ShowInTaskbar = false;
            f.StartPosition = FormStartPosition.CenterScreen;

            f.Shown += new EventHandler(
                delegate(object sender, EventArgs e) 
                {
                    if (parent != null)
                    {
                        parent.Cursor = Cursors.Default;
                    }
                }
                );

            DialogResult result = DialogResult.None;

            if (isModalForm)
            {
                result = f.ShowDialog(parent);
            }
            else
            {
                f.Show(parent);
            }

            return result;
        }
    }
}
