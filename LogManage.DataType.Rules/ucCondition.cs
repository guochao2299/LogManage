using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType;
using LogManage.DataType.Relations;

namespace LogManage.DataType.Rules
{
    public partial class ucCondition : UserControl
    {
        private string m_initValueOfCombo1st = string.Empty;
        private string m_initValueOfComboRelation = string.Empty;
        private bool m_initValueOfRbConstant = true;
        private bool m_initValueOfRbAnotherCol = false;
        private string m_initValueOfTxtContent = string.Empty;
        private string m_initValueOfTxtLeftBound = string.Empty;
        private string m_initValueOfTxtRightBound = string.Empty;
        private string m_initValueOfComboContent = string.Empty;
        private string m_initValueOfComboLeftBound = string.Empty;
        private string m_initValueOfComboRightBound = string.Empty;
        private int m_panel2Height = 0;

        public ucCondition()
        {
            InitializeComponent();
            InitCondition();
            InitLogColPool();
            m_panel2Height = this.splitContainer1.Panel2.Height;
        }

        private void InitCondition()
        {
            this.cbSelected.Checked = false;
            this.combo1st.Text = "";
            this.comboRelation.Text = "";
            this.rbConstant.Checked = true;
            this.rbAnotherCol.Checked = false;
            this.tpCol.Parent = null;
        }

        private List<LogColumn> m_lstColumns = new List<LogColumn>();
        private List<LogColumn> m_matchedColumns = null;
        //private List<LogColumn> m_leftBoundColumns = null;
        //private List<LogColumn> m_rightBoundColumns = null;
       
        private void InitLogColPool()
        {
            m_lstColumns = SecurityEventService.Instance.DBManager.GetDefinedColumns();

            FillCombobox(combo1st, m_lstColumns);

            if (combo1st.Items.Count > 0)
            {
                this.combo1st.SelectedIndex = 0;
            }            
        }

        private List<LogColumn> GetMatchedColumns(string colType)
        {
            List<LogColumn> subArray = new List<LogColumn>();

            foreach (LogColumn lc in m_lstColumns)
            {
                if (string.Equals(lc.Type, colType, StringComparison.OrdinalIgnoreCase))
                {
                    subArray.Add(lc);
                }
            }

            return subArray;
        }

        private List<string> m_relations = null;

        private void InitRelation(string colType)
        {
            this.comboRelation.Items.Clear();

            m_relations = RelationService.Instance.GetAvaliableRelations(colType);

            foreach (string item in m_relations)
            {
                this.comboRelation.Items.Add(item);
            }
        }

        private void FillCombobox(ComboBox cb, List<LogColumn> lstColumns)
        {
            cb.Items.Clear();

            foreach (LogColumn lc in lstColumns)
            {
                cb.Items.Add(lc.Name);
            }
        }

        private List<LogColumn> GetSubArray(List<LogColumn> lstColumns, string bannedItem)
        {
            List<LogColumn> subArray = new List<LogColumn>();

            foreach (LogColumn lc in lstColumns)
            {
                if (string.Equals(lc.Name, bannedItem, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                subArray.Add(lc);
            }

            return subArray;
        }

        private List<LogColumn> GetSubArray(List<LogColumn> lstColumns, List<string> bannedItems)
        {
            List<LogColumn> subArray = new List<LogColumn>();

            foreach (LogColumn lc in lstColumns)
            {
                if (bannedItems.Contains(lc.Name))
                {
                    continue;
                }

                subArray.Add(lc);
            }

            return subArray;
        }

        private void combo2st_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 控件是否被选中
        /// </summary>
        public bool IsRuleSelected
        {
            get
            {
                return cbSelected.Checked;
            }
            set
            {
                cbSelected.Checked = value;
            }
        }

        /// <summary>
        /// 是否显示条件编辑面板
        /// </summary>
        [DefaultValue(true)]
        public bool ShowEditPanel
        {
            get
            {
                return !this.splitContainer1.Panel2Collapsed;
            }
            set
            {
                if (!value)
                {
                    this.Height -= m_panel2Height;
                }
                else
                {
                    this.Height += m_panel2Height;
                }

                this.splitContainer1.Panel2Collapsed = !value;

                this.btnExpand.ImageIndex = this.splitContainer1.Panel2Collapsed ? 1 : 0;                
            }
        }

        private void cbSelected_CheckedChanged(object sender, EventArgs e)
        {
            cbSelected.Text = cbSelected.Checked ? "条件已选中" : "条件未选中";
            this.BackColor = cbSelected.Checked ? Color.LightSkyBlue : Color.Cornsilk;            
        }

        private void combo1st_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(combo1st.Text))
            {
                LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];

                InitRelation(col.Type);

                if (comboRelation.Items.Count > 0)
                {
                    comboRelation.SelectedIndex = 0;
                }

                m_matchedColumns = GetMatchedColumns(col.Type);

                FillCombobox(comboContent, m_matchedColumns);
                FillCombobox(comboLeftBound, m_matchedColumns);
                FillCombobox(comboRightBound, m_matchedColumns);
            }

            UpdateExpression();
        }

        private void rbConstant_CheckedChanged(object sender, EventArgs e)
        {
            this.tpCol.Parent = null;

            if (this.tpConstant.Parent == null)
            {
                this.tpConstant.Parent = this.tabControl1;
            }

            UpdateExpression();
        }

        private void rbAnotherCol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.tpCol.Parent == null)
            {
                this.tpCol.Parent = this.tabControl1;


            }

            this.tpConstant.Parent = null;

            UpdateExpression();
        }

        private void comboRelation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboRelation.SelectedIndex >= 0)
            {
                LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];

                IRelation relation = RelationService.Instance.GetRelation(col.Type,comboRelation.Text);

                if (relation.ParamsCount == 1)
                {
                    this.txtContent.Enabled = true;
                    this.txtLeftBound.Enabled = false;
                    this.txtRightBound.Enabled = false;

                    this.comboContent.Enabled = true;
                    this.comboLeftBound.Enabled = false;
                    this.comboRightBound.Enabled = false;
                }
                else if (relation.ParamsCount == 2)
                {
                    this.txtContent.Enabled = false;
                    this.txtLeftBound.Enabled = true;
                    this.txtRightBound.Enabled = true;

                    this.comboContent.Enabled = false;
                    this.comboLeftBound.Enabled = true;
                    this.comboRightBound.Enabled = true;
                }
                else
                {
                    MessageBox.Show("暂时不支持2个参数上的关系");
                    this.tpCol.Parent = null;
                    this.tpConstant.Parent = null;
                }

                UpdateExpression();
            }
        }

        /// <summary>
        /// 在获取EditedCondition，必须调用checkRelation函数检测条件是否成立
        /// </summary>
        public bool CheckRelation()
        {
            return true;
        }

        private int FindComboBoxIndex(ComboBox combo,string item)
        {
            return combo.Items.IndexOf(item);
        }

        private int FindLogColumnArrayIndex(List<LogColumn> columns,int colIndex)
        {
            for(int i=0;i<columns.Count;i++)
            {
                if(columns[i].Index==colIndex)
                {
                    return i;
                }
            }

            return -1;
        }

        private void UpdateStatus()
        {
            bool isNeedSave=false;

            if(!string.Equals(m_initValueOfCombo1st,this.combo1st.Text, StringComparison.OrdinalIgnoreCase))
            {
                isNeedSave=true;
                goto FinalOperate;
            }

            if(!string.Equals(m_initValueOfComboRelation,this.comboRelation.Text, StringComparison.OrdinalIgnoreCase))
            {
                isNeedSave=true;
                goto FinalOperate;
            }

            if(m_initValueOfRbAnotherCol !=this.rbAnotherCol.Checked)
            {
                isNeedSave=true;
                goto FinalOperate;
            }

            if(m_initValueOfRbConstant != this.rbConstant.Checked)
            {
                isNeedSave=true;
                goto FinalOperate;
            }

            if (this.combo1st.SelectedIndex <= 0)
            {
                goto FinalOperate;
            }

            LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];

            IRelation relation = RelationService.Instance.GetRelation(col.Type, comboRelation.Text);

            if (relation.ParamsCount == 1)
            {
                if (this.rbAnotherCol.Checked)
                {
                    if(!string.Equals(m_initValueOfComboContent ,this.comboContent.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        isNeedSave=true;
                        goto FinalOperate;
                    }
                }
                else
                {
                    if(!string.Equals(m_initValueOfTxtContent,txtContent.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        isNeedSave=true;
                        goto FinalOperate;
                    }
                }
            }
            else if (relation.ParamsCount == 2)
            {
                if (this.rbAnotherCol.Checked)
                {
                    if(!string.Equals(m_initValueOfComboLeftBound,this.comboLeftBound.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        isNeedSave=true;
                        goto FinalOperate;
                    }

                    if(!string.Equals(m_initValueOfComboRightBound ,this.comboRightBound.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        isNeedSave=true;
                        goto FinalOperate;      
                    }
                }
                else
                {
                    if(!string.Equals(this.m_initValueOfTxtLeftBound,this.txtLeftBound.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        isNeedSave=true;
                        goto FinalOperate;
                    }

                    if(!string.Equals(this.m_initValueOfTxtRightBound,this.comboRightBound.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        isNeedSave=true;
                        goto FinalOperate;
                    }
                }
            }    
        
      FinalOperate:
            this.lblStatus.Text=isNeedSave?"*未保存":string.Empty;
            this.btnSave.Visible = isNeedSave;
        }

        private string GetConditionExpression()
        {
            if (string.IsNullOrWhiteSpace(this.combo1st.Text))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(this.comboRelation.Text))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("\""+this.combo1st.Text + "\" ");
            sb.Append(this.comboRelation.Text + " \"");
            
            LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];
            IRelation relation = RelationService.Instance.GetRelation(col.Type, comboRelation.Text);

            if (relation.ParamsCount == 1)
            {
                sb.Append(this.rbConstant.Checked ? this.txtContent.Text : this.comboContent.Text);
                sb.Append("\"");
            }
            else if (relation.ParamsCount == 2)
            {
                sb.Append(this.rbConstant.Checked ? (this.txtLeftBound.Text + "\" 与 \"" + this.txtRightBound.Text + "\" 之间") :
                    (this.comboLeftBound.Text + "\" 与 \"" + this.comboRightBound.Text + "\" 之间"));
            }
            else
            {
                sb.Clear();
                sb.Append("错误:暂时不支持2个参数上的关系");
            }

            return sb.ToString();
        }

        public void SaveInitValue(SecurityCondition condition)
        {
            this.lblStatus.Text = string.Empty;
            this.btnSave.Visible = false;

            m_initValueOfCombo1st = this.combo1st.Text;
            m_initValueOfComboRelation = this.comboRelation.Text;
            m_initValueOfRbAnotherCol = this.rbAnotherCol.Checked;
            m_initValueOfRbConstant = this.rbConstant.Checked;

            if (this.combo1st.SelectedIndex <= 0)
            {
                return;
            }

            LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];

            IRelation relation = RelationService.Instance.GetRelation(col.Type, comboRelation.Text);

            if (relation.ParamsCount == 1)
            {
                if (condition.IsUsingDestCol)
                {
                    m_initValueOfComboContent = this.comboContent.Text;
                }
                else
                {
                    m_initValueOfTxtContent = txtContent.Text;
                }
            }
            else if (relation.ParamsCount == 2)
            {
                if (condition.IsUsingDestCol)
                {
                    m_initValueOfComboLeftBound = this.comboLeftBound.Text;
                    m_initValueOfComboRightBound = this.comboRightBound.Text;
                }
                else
                {
                    this.m_initValueOfTxtLeftBound = this.txtLeftBound.Text;
                    this.m_initValueOfTxtRightBound = this.comboRightBound.Text;
                }
            }            
        }

        public void SetCondition(SecurityCondition condition)
        {
            this.Tag = condition.ConditionGuid;

            this.combo1st.SelectedIndex = FindLogColumnArrayIndex(m_lstColumns, condition.SourceCol);
            this.comboRelation.Text = condition.RelationName;
            this.rbAnotherCol.Checked = condition.IsUsingDestCol;
            this.rbConstant.Checked = !condition.IsUsingDestCol;

            if (this.combo1st.SelectedIndex >= 0)
            {
                LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];

                IRelation relation = RelationService.Instance.GetRelation(col.Type, comboRelation.Text);

                if (relation.ParamsCount == 1)
                {
                    if (condition.IsUsingDestCol)
                    {
                        this.comboContent.SelectedIndex = FindLogColumnArrayIndex(m_matchedColumns, condition.DestinationCol);
                    }
                    else
                    {
                        txtContent.Text = condition.GetContent();
                    }
                }
                else if (relation.ParamsCount == 2)
                {                    
                    string firstValue = condition.MultiValues.Count > 0 ? condition.MultiValues[0] : string.Empty;
                    string secondValue = condition.MultiValues.Count > 1 ? condition.MultiValues[1] : string.Empty;

                    if (condition.IsUsingDestCol)
                    {
                        this.comboLeftBound.SelectedIndex = FindLogColumnArrayIndex(m_matchedColumns, Convert.ToInt32(firstValue));
                        this.comboRightBound.SelectedIndex = FindLogColumnArrayIndex(m_matchedColumns, Convert.ToInt32(secondValue));
                    }
                    else
                    {
                        this.txtLeftBound.Text = firstValue;
                        this.txtRightBound.Text = secondValue;
                    }
                }            
            }

            SaveInitValue(condition);
            UpdateExpression();
        }

        public SecurityCondition GetEditedCondition()
        {
            SecurityCondition sc = SecurityCondition.CreateSecurityCondition(Convert.ToString(this.Tag));

            if(!CheckRelation())
            {
                return sc;
            }
            
            sc.RelationName = comboRelation.Text;

            if (this.combo1st.SelectedIndex >= 0)
            {
                LogColumn col = m_lstColumns[this.combo1st.SelectedIndex];
                sc.SourceCol = col.Index;

                IRelation relation = RelationService.Instance.GetRelation(col.Type, comboRelation.Text);

                if (relation.ParamsCount == 1)
                {
                    if (rbConstant.Checked)
                    {
                        sc.SetContent(txtContent.Text);
                    }
                    else
                    {
                        sc.IsUsingDestCol = true;
                        sc.DestinationCol = m_matchedColumns[comboContent.SelectedIndex].Index;
                        sc.SetContent(sc.DestinationCol.ToString());
                    }
                }
                else if (relation.ParamsCount == 2)
                {
                    List<string> lstContent = new List<string>();

                    if (rbConstant.Checked)
                    {
                        lstContent.Add(txtLeftBound.Text);
                        lstContent.Add(txtRightBound.Text);
                    }
                    else
                    {
                        sc.IsUsingDestCol = true;
                        lstContent.Add(m_matchedColumns[comboLeftBound.SelectedIndex].Index.ToString());
                        lstContent.Add(m_matchedColumns[comboRightBound.SelectedIndex].Index.ToString());
                    }

                    sc.SetMultiValues(lstContent);
                }
            }            
            
            return sc;
        }

        private void tpConstant_Click(object sender, EventArgs e)
        {

        }


        public delegate void UpdateConditionEventHandler(object sender,SaveConditionEventArgs arg);
        public event UpdateConditionEventHandler UpdateConditionEvent;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (UpdateConditionEvent != null)
            {
                SaveConditionEventArgs arg = new SaveConditionEventArgs();
                arg.Condition = this.GetEditedCondition();

                try
                {
                    UpdateConditionEvent(this, arg);
                    SaveInitValue(arg.Condition);
                    this.lblStatus.Text = string.Empty;
                    UpdateStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存变动失败，错误消息为：" + ex.Message);
                }                
            }            
        }

        private void combo1st_Leave(object sender, EventArgs e)
        {
            UpdateStatus();

            UpdateExpression();            
        }

        private void UpdateExpression()
        {
            this.lblExpression.Text = GetConditionExpression();
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            UpdateExpression();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            this.ShowEditPanel = !this.ShowEditPanel;
        }
    }    
}
