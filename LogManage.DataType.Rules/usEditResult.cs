using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogManage.DataType.Rules
{
    public partial class usEditResult : UserControl
    { 
        public usEditResult()
        {
            InitializeComponent();

            this.pictureBox1.BackColor = Color.White;
        }

        public void SetActionResult(SecurityActionResult result)
        {
            this.textBox1.Text = result.Description;
            this.pictureBox1.BackColor = Color.FromArgb(result.BackgroundColor);
            this.Tag = result.ResultGuid;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = this.pictureBox1.BackColor;

            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                this.pictureBox1.BackColor = cd.Color;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.cbIsChecked.Checked;
            }
            set
            {
                this.cbIsChecked.Checked = value;
            }
        }

        public bool CanEdit
        {
            set
            {
                if (value)
                {
                    this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
                    this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
                }
                else
                {
                    this.pictureBox1.Click -= new System.EventHandler(this.pictureBox1_Click);
                    this.pictureBox1.MouseHover -= new System.EventHandler(this.pictureBox1_MouseHover);
                }

                this.textBox1.ReadOnly = !value;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("行为结果名称不能为空，请重新填写");
                textBox1.Select();
                return;
            }
        }

        public SecurityActionResult EditedResult
        {
            get
            {
                return SecurityActionResult.CreateSecurityActionResult(this.textBox1.Text,
                    Convert.ToString(this.Tag), this.pictureBox1.BackColor.ToArgb());
            }
        }

        private void usEditResult_MouseEnter(object sender, EventArgs e)
        {
            if (this.BackColor != Color.LightSkyBlue)
            {
                this.BackColor = Color.LightSkyBlue;
            }
        }

        private void usEditResult_MouseLeave(object sender, EventArgs e)
        {
            cbIsChecked_CheckedChanged(null, null);
        }

        private void cbIsChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsChecked.Checked)
            {
                this.BackColor = Color.LightCoral;
                this.textBox1.BackColor = Color.LightCoral;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.textBox1.BackColor = Color.White;
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(pictureBox1, "点击颜色区域切换颜色");
        }
    }

    public class ActionResultEventArgs : EventArgs
    {
 
    }
}
