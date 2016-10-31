// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

// project created on 2/6/2003 at 11:10 AM
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LogManage.CommonControls
{
	/// <summary>
	/// Form used to display display unhandled errors in SharpDevelop.
	/// </summary>
	public class ExceptionBox : Form
	{
		private System.Windows.Forms.TextBox exceptionTextBox;
		private System.Windows.Forms.CheckBox copyErrorCheckBox;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button continueButton;
		private System.Windows.Forms.PictureBox pictureBox;
		Exception exceptionThrown;
        private Button btnReport;
        private TextBox txtUserOpt;
		string message;
		
		internal static void RegisterExceptionBoxForUnhandledExceptions()
		{
			Application.ThreadException += ShowErrorBox;
			AppDomain.CurrentDomain.UnhandledException += ShowErrorBox;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		}
		
		private static void ShowErrorBox(object sender, ThreadExceptionEventArgs e)
		{			
			ShowErrorBox(e.Exception, null);
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")]
        private static void ShowErrorBox(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;			
			ShowErrorBox(ex, "Unhandled exception", e.IsTerminating);
		}
		
		/// <summary>
		/// Displays the exception box.
		/// </summary>
		public static void ShowErrorBox(Exception exception, string message)
		{
			ShowErrorBox(exception, message, false);
		}
		
		[ThreadStatic]
        private static bool showingBox;
		
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void ShowErrorBox(Exception exception, string message, bool mustTerminate)
		{
			if (exception == null)
				throw new ArgumentNullException("错误信息不能为空");
			
			// ignore reentrant calls (e.g. when there's an exception in OnRender)
			if (showingBox)
				return;
			showingBox = true;
			try {				
				using (ExceptionBox box = new ExceptionBox(exception, message, mustTerminate)) {
                    box.ShowDialog();
				}
			} catch (Exception ex) {				
				MessageBox.Show(exception.ToString(), message, MessageBoxButtons.OK,
				                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			} finally {
				showingBox = false;
			}
		}
		
		/// <summary>
		/// Creates a new ExceptionBox instance.
		/// </summary>
		/// <param name="exception">The exception to display</param>
		/// <param name="message">An additional message to display</param>
		/// <param name="mustTerminate">If <paramref name="mustTerminate"/> is true, the
		/// continue button is not available.</param>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public ExceptionBox(Exception exception, string message, bool mustTerminate)
		{
			this.exceptionThrown = exception;
			this.message = message;
			InitializeComponent();
			if (mustTerminate) {
				closeButton.Visible = false;
				continueButton.Text = closeButton.Text;
				continueButton.Left -= closeButton.Width - continueButton.Width;
				continueButton.Width = closeButton.Width;
			}			
			
			exceptionTextBox.Text = getClipboardString();			
		}


        private string getClipboardString()
		{
			StringBuilder sb = new StringBuilder();			
			
			if (message != null) {
				sb.AppendLine(message);
			}
			sb.AppendLine("错误详细内容:");
			sb.AppendLine(exceptionThrown.ToString());			
			return sb.ToString();
		}

        private string CopyInfoToClipboard()
		{
			string exceptionText = exceptionTextBox.Text;
            if (this.copyErrorCheckBox.Checked)
            {
                if (Application.OleRequired() == ApartmentState.STA)
                {
                    Clipboard.SetText(exceptionText);
                }
                else
                {
                    Thread th = new Thread((ThreadStart)delegate
                    {
                        Clipboard.SetText(exceptionText);
                    });
                    th.Name = "CopyInfoToClipboard";
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            }

            return exceptionText;
		}

        private void ReportBug(string error)
        {
 
        }

        private void buttonClick(object sender, System.EventArgs e)
		{
            ReportBug(CopyInfoToClipboard());
		}

        private void continueButtonClick(object sender, System.EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Ignore;
			Close();
		}

        private void CloseButtonClick(object sender, EventArgs e)
		{
			if (MessageBox.Show("你确定因程序错误而退出系统?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly)
			    == DialogResult.Yes)
			{
				Process.GetCurrentProcess().Kill();
			}
		}
		
		[SuppressMessage("Microsoft.Globalization", "CA1303")]
        private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionBox));
            this.closeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.copyErrorCheckBox = new System.Windows.Forms.CheckBox();
            this.exceptionTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.txtUserOpt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(470, 417);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(88, 30);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "关闭系统";
            this.closeButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(230, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(448, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "错误详细内容：";
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(232, 8);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(448, 21);
            this.label.TabIndex = 6;
            this.label.Text = "为了快速修正错误，请简单填写一下您是在做什么操作时报的错误：";
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(592, 417);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(88, 30);
            this.continueButton.TabIndex = 6;
            this.continueButton.Text = "继    续";
            this.continueButton.Click += new System.EventHandler(this.continueButtonClick);
            // 
            // copyErrorCheckBox
            // 
            this.copyErrorCheckBox.Checked = true;
            this.copyErrorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.copyErrorCheckBox.Location = new System.Drawing.Point(230, 379);
            this.copyErrorCheckBox.Name = "copyErrorCheckBox";
            this.copyErrorCheckBox.Size = new System.Drawing.Size(440, 24);
            this.copyErrorCheckBox.TabIndex = 2;
            this.copyErrorCheckBox.Text = "将错误信息复制到剪切板";
            // 
            // exceptionTextBox
            // 
            this.exceptionTextBox.Location = new System.Drawing.Point(230, 183);
            this.exceptionTextBox.Multiline = true;
            this.exceptionTextBox.Name = "exceptionTextBox";
            this.exceptionTextBox.ReadOnly = true;
            this.exceptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.exceptionTextBox.Size = new System.Drawing.Size(448, 184);
            this.exceptionTextBox.TabIndex = 1;
            this.exceptionTextBox.Text = "错误详细内容";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(224, 464);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(334, 417);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(88, 30);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "上报错误";
            this.btnReport.Click += new System.EventHandler(this.buttonClick);
            // 
            // txtUserOpt
            // 
            this.txtUserOpt.Location = new System.Drawing.Point(234, 32);
            this.txtUserOpt.Multiline = true;
            this.txtUserOpt.Name = "txtUserOpt";
            this.txtUserOpt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUserOpt.Size = new System.Drawing.Size(436, 112);
            this.txtUserOpt.TabIndex = 10;
            // 
            // ExceptionBox
            // 
            this.ClientSize = new System.Drawing.Size(688, 453);
            this.Controls.Add(this.txtUserOpt);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.copyErrorCheckBox);
            this.Controls.Add(this.exceptionTextBox);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionBox";
            this.Text = "程序错误";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button closeButton;
	}
}
