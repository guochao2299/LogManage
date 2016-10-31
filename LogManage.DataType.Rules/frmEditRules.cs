using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LogManage.DataType.Rules.UndoRedo;

namespace LogManage.DataType.Rules
{
    public partial class frmEditRules : Form
    {
        private CommandBox m_undoRedoBuffer = null;
        private const int EventLevel = 0;
        private const int UserActionLevel = 1;
        private List<LogColumn> m_columns = null;

        public frmEditRules(IManageRuleData mgr,List<LogColumn> lstColumns)
        {
            InitializeComponent();

            SecurityEventService.Instance.DBManager=mgr;
            
            m_undoRedoBuffer = new CommandBox();
            m_columns = lstColumns;
            
            InitSecurityEventTree();
        }

        private void InitSecurityEventTree()
        {
            this.Cursor = Cursors.WaitCursor;
            this.tvEvents.SuspendLayout();

            try
            {
                this.tvEvents.Nodes.Clear();

                foreach (SecurityEvent se in SecurityEventService.Instance.AvaliableEvents.Values)
                {
                    TreeNode tnSE = new TreeNode(se.Name);
                    tnSE.Tag = se.EventGuid;

                    foreach (SecurityAction sa in se.SecurityActions)
                    {
                        TreeNode tnActions = new TreeNode(sa.Name);
                        tnActions.Tag = sa.ActionGuid;

                        tnSE.Nodes.Add(tnActions);
                    }

                    this.tvEvents.Nodes.Add(tnSE);
                }

                if (this.tvEvents.Nodes.Count > 0)
                {
                    this.tvEvents.SelectedNode = this.tvEvents.Nodes[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化安全事件树失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.tvEvents.ResumeLayout();
                this.Cursor = Cursors.Default;
            }
        }

        private void AddCommand(ICommand cmd)
        {
            m_undoRedoBuffer.AddCommand(cmd);
            UpdateUndoRedoStatus();
        }

        private void UpdateUndoRedoStatus()
        {
            menuUndo.Enabled = this.m_undoRedoBuffer.CanUndo;
            menuRedo.Enabled = this.m_undoRedoBuffer.CanRedo;
        }

        private void UpdateUserActionMenuStatus()
        {
            this.menuNewUserAction.Enabled = this.tvEvents.SelectedNode != null;
            this.menuDeleteUserAction.Enabled = (this.tvEvents.SelectedNode != null) && (this.tvEvents.SelectedNode.Level == UserActionLevel);

            this.menuNewCondition.Enabled = this.tvEvents.SelectedNode != null && (this.tvEvents.SelectedNode.Level == UserActionLevel);
            this.menuDeleteCondition.Enabled = (this.tvEvents.SelectedNode != null) && (this.tvEvents.SelectedNode.Level == UserActionLevel);
        }

        private void UpdateEventMenuStatus()
        {
            this.menuNewEvent.Enabled = true;
            this.menuDeleteEvent.Enabled = (this.tvEvents.SelectedNode != null) && (this.tvEvents.SelectedNode.Level == EventLevel);
        }

        private void RemoveEventFromTree(UndoRedoEventArg e)
        {
            RemoveEventFromTree(e.FirstLevelGuid);
        }
        private void RemoveEventFromTree(string eventGuid)
        {
            for (int i = this.tvEvents.Nodes.Count - 1; i >= 0; i--)
            {
                if (string.Equals(Convert.ToString(this.tvEvents.Nodes[i].Tag), eventGuid, StringComparison.OrdinalIgnoreCase))
                {
                    this.tvEvents.Nodes.RemoveAt(i);
                    break;
                }
            }

            UpdateEventMenuStatus();
        }

        private void AddEvent2Tree(UndoRedoEventArg e)
        {
            AddEvent2Tree(Convert.ToString(e.Tag), e.FirstLevelGuid);
        }
        private void AddEvent2Tree(string eventName, string eventGuid)
        {
            TreeNode tn = new TreeNode(eventName);
            tn.Tag = eventGuid;
            this.tvEvents.Nodes.Add(tn);
            this.tvEvents.SelectedNode = tn;

            UpdateEventMenuStatus();
        }

        private void RefreshEventTree(UndoRedoEventArg e)
        {
            RefreshEventTree(Convert.ToString(e.Tag), e.FirstLevelGuid);
        }
        private void RefreshEventTree(string eventName, string eventGuid)
        {
            for (int i = this.tvEvents.Nodes.Count - 1; i >= 0; i--)
            {
                if (string.Equals(Convert.ToString(this.tvEvents.Nodes[i].Tag), eventGuid, StringComparison.OrdinalIgnoreCase))
                {
                    this.tvEvents.Nodes[i].Text = eventName;
                    break;
                }
            }
        }

        private void menuNewEvent_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmEditEventProperty fe = new frmEditEventProperty();
                fe.Text = "编辑安全事件属性";

                if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                {
                    SecurityEvent se = SecurityEvent.CreateNewSecurityEvent(fe.EventName, fe.Description);

                    CreateNewSecurityEventCommand cmd = new CreateNewSecurityEventCommand(se);
                    cmd.UndoDone += new UndoRedoEventHandler(RemoveEventFromTree);
                    cmd.RedoDone += new UndoRedoEventHandler(AddEvent2Tree);
                    cmd.Execute();
                    AddCommand(cmd);
                    AddEvent2Tree(se.Name, se.EventGuid);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建新安全事件失败,错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void menuDeleteEvent_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (MessageBox.Show(string.Format("确定删除安全事件\"{0}\"及下面的结构", this.tvEvents.SelectedNode.Text),
                    "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string eventGuid = Convert.ToString(this.tvEvents.SelectedNode.Tag);
                DeleteSecurityEventCommand cmd = new DeleteSecurityEventCommand(eventGuid);
                cmd.RedoDone += new UndoRedoEventHandler(RemoveEventFromTree);
                cmd.UndoDone += new UndoRedoEventHandler(AddEvent2Tree);

                cmd.Execute();
                
                AddCommand(cmd);
                RemoveEventFromTree(eventGuid);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除安全事件失败,错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region 安全行为

        /// <summary>
        /// 查找安全行为所在的安全事件节点
        /// </summary>
        private bool FindParentNode(string eventGuid, out TreeNode pNode)
        {
            pNode = null;

            foreach (TreeNode parentNode in this.tvEvents.Nodes)
            {
                if (string.Equals(Convert.ToString(parentNode.Tag), eventGuid, StringComparison.OrdinalIgnoreCase))
                {
                    pNode = parentNode;
                    return true;
                }
            }

            return false;
        }

        private void AddAction2Tree(UndoRedoEventArg e)
        {
            AddAction2Tree(e.FirstLevelGuid, e.SecondLevelGuid, Convert.ToString(e.Tag));
        }
        private void AddAction2Tree(string eventGuid, string actionGuid, string actionName)
        {
            TreeNode tn = new TreeNode(actionName);
            tn.Tag = actionGuid;

            TreeNode pNode = null;
            if (!FindParentNode(eventGuid, out pNode))
            {
                return;
            }

            pNode.Nodes.Add(tn);
            this.tvEvents.SelectedNode = tn;

            UpdateUserActionMenuStatus();
        }

        private void RefreshActionNodeName(UndoRedoEventArg e)
        {
            RefreshActionNodeName(e.FirstLevelGuid, e.SecondLevelGuid, Convert.ToString(e.Tag));
        }

        private void RefreshActionNodeName(string eventGuid, string actionGuid, string newName)
        {
            TreeNode pNode = null;
            if (!FindParentNode(eventGuid, out pNode))
            {
                return;
            }

            foreach (TreeNode tn in pNode.Nodes)
            {
                if (string.Equals(Convert.ToString(tn.Tag), actionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    tn.Text = newName;
                    break;
                }
            }
        }

        private void RemoveActionFromTree(UndoRedoEventArg e)
        {
            RemoveActionFromTree(e.FirstLevelGuid, e.SecondLevelGuid);
        }
        private void RemoveActionFromTree(string eventGuid, string actionGuid)
        {
            TreeNode pNode = null;
            if (!FindParentNode(eventGuid, out pNode))
            {
                return;
            }

            for (int i = pNode.Nodes.Count - 1; i >= 0; i--)
            {
                if (string.Equals(Convert.ToString(pNode.Nodes[i].Tag), actionGuid, StringComparison.OrdinalIgnoreCase))
                {
                    pNode.Nodes.RemoveAt(i);
                    break;
                }
            }

            this.tvEvents.SelectedNode = null;

            UpdateUserActionMenuStatus();
        }
        #endregion

        #region 安全条件

        /// <summary>
        /// 查找安全条件所属的安全行为节点
        /// </summary>
        private bool FindParentNode(string eventGuid, string actionGuid,out TreeNode pNode)
        {
            pNode = null;

            foreach (TreeNode parentNode in this.tvEvents.Nodes)
            {
                if (string.Equals(Convert.ToString(parentNode.Tag), eventGuid, StringComparison.OrdinalIgnoreCase))
                {

                    foreach (TreeNode tnSon in parentNode.Nodes)
                    {
                        if (string.Equals(Convert.ToString(tnSon.Tag), actionGuid, StringComparison.OrdinalIgnoreCase))
                        {
                            pNode = tnSon;
                            return true;
                        }
                    }                   
                }
            }

            return false;
        }

        private void AddConditon2Action(UndoRedoEventArg e)
        {
            AddConditon2Action(e.FirstLevelGuid, e.SecondLevelGuid, (SecurityCondition)e.Tag);
        }

        private ucCondition FindConditon(string conditionGuid)
        {
            foreach (Control c in this.flowLayoutPanel1.Controls)
            {
                if (string.Compare(conditionGuid, Convert.ToString(c.Tag), true) == 0)
                {                    
                    return (ucCondition)c;
                }
            }

            return null;
        }

        private void AddConditon2Action(string eventGuid, string actionGuid, SecurityCondition sc)
        { 
            TreeNode pNode = null;
            if (!FindParentNode(eventGuid,actionGuid,out pNode))
            {
                MessageBox.Show("添加条件前，请先选中条件所属安全行为");
                return;
            }

            ucCondition ctl = FindConditon(sc.ConditionGuid);
            if (ctl != null)
            {
                MessageBox.Show("已经存在相同标识符的条件");
                ctl.IsRuleSelected = true;

                return;
            }

            ctl = new ucCondition();
            ctl.SetCondition(sc);
            ctl.UpdateConditionEvent += new ucCondition.UpdateConditionEventHandler(UpdateCondition);

            this.flowLayoutPanel1.Controls.Add(ctl);
        }

        private void UpdateCondition(object sener, SaveConditionEventArgs arg)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string eventGuid = Convert.ToString(tvEvents.SelectedNode.Parent.Tag);
                string actionGuid = Convert.ToString(tvEvents.SelectedNode.Tag);

                UpdateActionConditionCommand cmd = new UpdateActionConditionCommand(eventGuid, actionGuid, arg.Condition);
                cmd.Execute();

                ((ucCondition)sener).SaveInitValue(arg.Condition);
                
                cmd.UndoDone+=new UndoRedoEventHandler(RefreshCondition);
                cmd.RedoDone+=new UndoRedoEventHandler(RefreshCondition);

                AddCommand(cmd);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新工艺条件信息失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void AddConditons2Action(UndoRedoEventArg e)
        {
            AddConditons2Action(e.FirstLevelGuid, e.SecondLevelGuid, (List<SecurityCondition>)e.Tag);
        }

        private void AddConditons2Action(string eventGuid, string actionGuid, List<SecurityCondition> scs)
        {
            foreach (SecurityCondition sc in scs)
            {
                AddConditon2Action(eventGuid, actionGuid, sc);
            }
        }

        private void RefreshCondition(UndoRedoEventArg e)
        {
            RefreshCondition(e.FirstLevelGuid, e.SecondLevelGuid, (SecurityCondition)e.Tag);
        }

        private void RefreshCondition(string eventGuid, string actionGuid, SecurityCondition sc)
        {
            TreeNode pNode = null;
            if (!FindParentNode(eventGuid, actionGuid, out pNode))
            {
                MessageBox.Show("更新条件前，请先选中条件所属安全行为");
                return;
            }

            ucCondition ctl = FindConditon(sc.ConditionGuid);

            if (ctl == null)
            {
                MessageBox.Show("要更新的条件不存在");
                return;
            }

            ctl.SetCondition(sc);
        }

        private void RemoveConditon(UndoRedoEventArg e)
        {
            RemoveConditon(e.FirstLevelGuid, e.SecondLevelGuid,Convert.ToString(e.Tag));
        }
        private void RemoveConditon(string eventGuid, string actionGuid,string conditionGuid)
        {
            TreeNode pNode = null;
            if (!FindParentNode(eventGuid, actionGuid, out pNode))
            {
                MessageBox.Show("删除条件前，请先选中条件所属安全行为");
                return;
            }

            ucCondition ctl = FindConditon(conditionGuid);

            if (ctl == null)
            {
                MessageBox.Show("要删除的条件不存在");
                return;
            }

            this.flowLayoutPanel1.Controls.Remove(ctl);
        }

        private void RemoveConditons(UndoRedoEventArg e)
        {
            RemoveConditons(e.FirstLevelGuid, e.SecondLevelGuid, (List<string>)e.Tag);
        }
        private void RemoveConditons(string eventGuid, string actionGuid, List<string> conditionGuids)
        {
            foreach (string s in conditionGuids)
            {
                RemoveConditon(eventGuid, actionGuid, s);
            }
        }
        #endregion

        private void menuNewUserAction_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvEvents.SelectedNode == null)
                {
                    MessageBox.Show("请先选择安全行为所属安全事件");
                    return;
                }

                string eventGuid = string.Empty;

                if (tvEvents.SelectedNode.Level == EventLevel)
                {
                    eventGuid = Convert.ToString(tvEvents.SelectedNode.Tag);
                }
                else if (tvEvents.SelectedNode.Level == UserActionLevel)
                {
                    eventGuid = Convert.ToString(tvEvents.SelectedNode.Parent.Tag);
                }

                this.Cursor = Cursors.WaitCursor;
                frmEditUserActionProperty fe = new frmEditUserActionProperty();
                fe.Text = "编辑安全行为属性";
                
                if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                {
                    SecurityAction sa = SecurityAction.CreateNewSecurityAction(
                        fe.ActionName, fe.ActionDesc, fe.ActionResultGuid);

                    CreateNewUserActionCommand cmd = new CreateNewUserActionCommand(eventGuid, sa);
                    cmd.UndoDone += new UndoRedoEventHandler(RemoveActionFromTree);
                    cmd.RedoDone += new UndoRedoEventHandler(AddAction2Tree);
                    cmd.Execute();
                    
                    AddCommand(cmd);
                    AddAction2Tree(eventGuid, sa.ActionGuid, sa.Name);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建新安全行为失败,错误消息为：" + ex.Message);
            }
        }

        private void menuDeleteUserAction_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (tvEvents.SelectedNode == null || 
                    tvEvents.SelectedNode.Level!=UserActionLevel)
                {
                    MessageBox.Show("请先选择要删除的安全行为");
                    return;
                }

                string eventGuid = Convert.ToString(tvEvents.SelectedNode.Parent.Tag);
                string actionGuid = Convert.ToString(tvEvents.SelectedNode.Tag);

                DeleteSecurityActionCommand cmd = new DeleteSecurityActionCommand(eventGuid, actionGuid);
                cmd.RedoDone += new UndoRedoEventHandler(RemoveActionFromTree);
                cmd.UndoDone += new UndoRedoEventHandler(AddAction2Tree);

                cmd.Execute();
                
                AddCommand(cmd);
                RemoveActionFromTree(eventGuid, actionGuid);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除安全行为失败,错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void menuNewCondition_Click(object sender, EventArgs e)
        {                
            this.Cursor = Cursors.WaitCursor;

            try
            { 
                if (tvEvents.SelectedNode == null || 
                    tvEvents.SelectedNode.Level!=UserActionLevel)
                {
                    MessageBox.Show("请先选择要添加安全条件的安全行为");
                    return;
                }

                string eventGuid = Convert.ToString(tvEvents.SelectedNode.Parent.Tag);
                string actionGuid = Convert.ToString(tvEvents.SelectedNode.Tag);

                SecurityCondition condition = SecurityCondition.CreateNewSecurityCondition();

                CreateActionConditionCommand cmd = new CreateActionConditionCommand(eventGuid, actionGuid, condition);

                cmd.RedoDone += new UndoRedoEventHandler(AddConditon2Action);
                cmd.UndoDone += new UndoRedoEventHandler(RemoveConditon);

                cmd.Execute();
                
                AddCommand(cmd);
                AddConditon2Action(eventGuid, actionGuid,condition);
            }
            catch (Exception ex)
            {
                MessageBox.Show("新建安全行为条件失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void menuDeleteCondition_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (tvEvents.SelectedNode == null || 
                    tvEvents.SelectedNode.Level!=UserActionLevel)
                {
                    MessageBox.Show("请先选择要删除安全条件的安全行为");
                    return;
                }

                string eventGuid = Convert.ToString(tvEvents.SelectedNode.Parent.Tag);
                string actionGuid = Convert.ToString(tvEvents.SelectedNode.Tag);
                List<SecurityCondition> lstConditions = new List<SecurityCondition>();
                List<string> lstConditonGuids = new List<string>();

                foreach (ucCondition c in this.flowLayoutPanel1.Controls)
                {
                    if (!c.IsRuleSelected)
                    {
                        continue;
                    }

                    lstConditions.Add(c.GetEditedCondition());
                    lstConditonGuids.Add(lstConditions.Last().ConditionGuid);
                }

                DeleteActionConditionCommand cmd = new DeleteActionConditionCommand(eventGuid, actionGuid,lstConditions);
                cmd.RedoDone += new UndoRedoEventHandler(RemoveConditons);
                cmd.UndoDone += new UndoRedoEventHandler(AddConditons2Action);

                cmd.Execute();
                
                AddCommand(cmd);
                RemoveConditons(eventGuid, actionGuid, lstConditonGuids);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除安全行为条件失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void menuUndo_Click(object sender, EventArgs e)
        {
            m_undoRedoBuffer.Undo();
            UpdateUndoRedoStatus();
        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
            m_undoRedoBuffer.Redo();
            UpdateUndoRedoStatus();
        }

        private void cmProperties_DropDownOpening(object sender, EventArgs e)
        {
            cmProperties.Enabled = tvEvents.SelectedNode != null;
        }


        private void cmProperties_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (tvEvents.SelectedNode.Level == EventLevel)
                {
                    SecurityEvent se=SecurityEventService.Instance.GetSecurityEvent(Convert.ToString(tvEvents.SelectedNode.Tag));

                    frmEditEventProperty fe = new frmEditEventProperty(se);
                    fe.Text = "编辑安全事件属性";

                    if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                    {
                        SecurityEvent newSe = SecurityEvent.CreateSecurityEvent(fe.EventName, se.EventGuid,fe.Description);

                        UpdateSecurityEventCommand cmd = new UpdateSecurityEventCommand(newSe);
                        cmd.UndoDone += new UndoRedoEventHandler(RefreshEventTree);
                        cmd.RedoDone += new UndoRedoEventHandler(RefreshEventTree);
                        cmd.Execute();
                        
                        AddCommand(cmd);
                        RefreshEventTree(se.Name, se.EventGuid);                        
                    }
                }
                else if (tvEvents.SelectedNode.Level == UserActionLevel)
                {               
                    string eventGuid=Convert.ToString(tvEvents.SelectedNode.Parent.Tag);

                    SecurityAction sa = SecurityEventService.Instance.GetSecurityAction(
                        eventGuid,
                        Convert.ToString(tvEvents.SelectedNode.Tag));

                    frmEditUserActionProperty fe = new frmEditUserActionProperty(sa);
                    fe.Text = "编辑安全行为属性";

                    if (CGeneralFuncion.ShowWindow(this, fe, true) == System.Windows.Forms.DialogResult.OK)
                    {
                        SecurityAction newData = SecurityAction.CreateSecurityAction(fe.ActionName,
                            sa.ActionGuid, fe.ActionDesc, fe.ActionResultGuid);

                        UpdateSecurityActionCommand cmd = new UpdateSecurityActionCommand(eventGuid,
                            newData);
                        cmd.UndoDone += new UndoRedoEventHandler(RefreshActionNodeName);
                        cmd.RedoDone += new UndoRedoEventHandler(RefreshActionNodeName);
                        cmd.Execute();
                        
                        AddCommand(cmd);
                        RefreshActionNodeName(eventGuid, newData.ActionGuid, newData.Name);                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改属性信息失败，错误消息为：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tvEvents_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void ClearFlowPanel()
        {
            this.flowLayoutPanel1.Controls.Clear();
            GC.Collect();
        }

        private void ListConditions(SecurityAction sa)
        {
            this.Cursor = Cursors.WaitCursor;
            this.flowLayoutPanel1.SuspendLayout();

            try
            { 
                ClearFlowPanel();

                foreach (SecurityCondition sc in sa.Conditions)
                {
                    ucCondition ctl = new ucCondition();
                    ctl.UpdateConditionEvent += new ucCondition.UpdateConditionEventHandler(UpdateCondition);
                    ctl.SetCondition(sc);
                    ctl.ShowEditPanel = false;

                    this.flowLayoutPanel1.Controls.Add(ctl);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("初始化用户行为{0}的条件列表失败，错误消息为：{1}", sa.Name, ex.Message));
            }
            finally
            {
                this.flowLayoutPanel1.ResumeLayout();
                this.Cursor = Cursors.Default;
            }
        }

        private void tvEvents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateEventMenuStatus();
            UpdateUserActionMenuStatus();

            if (e.Node.Level == EventLevel)
            {
                ClearFlowPanel();
            }
            else if(e.Node.Level==UserActionLevel)
            {
                ListConditions(SecurityEventService.Instance.GetSecurityAction(
                    Convert.ToString(e.Node.Parent.Tag),
                    Convert.ToString(e.Node.Tag))); 
            }
        }
    }
}
