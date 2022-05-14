
namespace EDGV
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    /*
    public class TripleTreeNode : TreeNode
    {
        private System.Windows.Forms.CheckState checkState;
        private TripleTreeNode parent;

        private TripleTreeNode(string Text, object Value, System.Windows.Forms.CheckState State, TripleTreeNodeType NodeType) : base(Text)
        {
            this.CheckState = State;
            this.NodeType = NodeType;
            this.Value = Value;
        }

        private void AddChild(TripleTreeNode child)
        {
            child.Parent = this;
            base.Nodes.Add(child);
        }

        public TripleTreeNode Clone()
        {
            TripleTreeNode node = null;
            switch (this.NodeType)
            {
                case TripleTreeNodeType.AllsNode:
                    node = CreateAllsNode(base.Text, this.checkState);
                    break;

                case TripleTreeNodeType.EmptysNode:
                    node = CreateEmptysNode(base.Text, this.checkState);
                    break;

                case TripleTreeNodeType.MSecDateTimeNode:
                    node = CreateMSecNode(base.Text, this.Value, this.checkState);
                    break;

                case TripleTreeNodeType.SecDateTimeNode:
                    node = CreateSecNode(base.Text, this.Value, this.checkState);
                    break;

                case TripleTreeNodeType.MinDateTimeNode:
                    node = CreateMinNode(base.Text, this.Value, this.checkState);
                    break;

                case TripleTreeNodeType.HourDateTimeNode:
                    node = CreateHourNode(base.Text, this.Value, this.checkState);
                    break;

                case TripleTreeNodeType.DayDateTimeNode:
                    node = CreateDayNode(base.Text, this.Value, this.checkState);
                    break;

                case TripleTreeNodeType.MonthDateTimeNode:
                    node = CreateMonthNode(base.Text, this.Value, this.checkState);
                    break;

                case TripleTreeNodeType.YearDateTimeNode:
                    node = CreateYearNode(base.Text, this.Value, this.checkState);
                    break;

                default:
                    node = CreateNode(base.Text, this.Value, this.checkState);
                    break;
            }
            node.NodeFont = base.NodeFont;
            if (base.GetNodeCount(false) > 0)
            {
                foreach (TripleTreeNode node2 in base.Nodes)
                {
                    node.AddChild(node2.Clone());
                }
            }
            return node;
        }

        public static TripleTreeNode CreateAllsNode(string Text, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, null, State, TripleTreeNodeType.AllsNode);
        }

        public TripleTreeNode CreateChildNode(string Text, object Value)
        {
            return this.CreateChildNode(Text, Value, this.checkState);
        }

        public TripleTreeNode CreateChildNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            TripleTreeNode child = null;
            switch (this.NodeType)
            {
                case TripleTreeNodeType.SecDateTimeNode:
                    child = CreateMSecNode(Text, Value, State);
                    break;

                case TripleTreeNodeType.MinDateTimeNode:
                    child = CreateSecNode(Text, Value, State);
                    break;

                case TripleTreeNodeType.HourDateTimeNode:
                    child = CreateMinNode(Text, Value, State);
                    break;

                case TripleTreeNodeType.DayDateTimeNode:
                    child = CreateHourNode(Text, Value, State);
                    break;

                case TripleTreeNodeType.MonthDateTimeNode:
                    child = CreateDayNode(Text, Value, State);
                    break;

                case TripleTreeNodeType.YearDateTimeNode:
                    child = CreateMonthNode(Text, Value, State);
                    break;

                default:
                    child = null;
                    break;
            }
            if (child != null)
            {
                this.AddChild(child);
            }
            return child;
        }

        public static TripleTreeNode CreateDayNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.DayDateTimeNode);
        }

        public static TripleTreeNode CreateEmptysNode(string Text, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, null, State, TripleTreeNodeType.EmptysNode);
        }

        public static TripleTreeNode CreateHourNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.HourDateTimeNode);
        }

        public static TripleTreeNode CreateMinNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.MinDateTimeNode);
        }

        public static TripleTreeNode CreateMonthNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.MonthDateTimeNode);
        }

        public static TripleTreeNode CreateMSecNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.MSecDateTimeNode);
        }

        public static TripleTreeNode CreateNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.Default);
        }

        public static TripleTreeNode CreateSecNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.SecDateTimeNode);
        }

        public static TripleTreeNode CreateYearNode(string Text, object Value, System.Windows.Forms.CheckState State = CheckState.Checked)
        {
            return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.YearDateTimeNode);
        }

        private void SetCheckImage()
        {
            switch (this.checkState)
            {
                case System.Windows.Forms.CheckState.Checked:
                    base.StateImageIndex = 1;
                    return;

                case System.Windows.Forms.CheckState.Indeterminate:
                    base.StateImageIndex = 2;
                    return;
            }
            base.StateImageIndex = 0;
        }

        public TripleTreeNodeType NodeType { get; private set; }

        public object Value { get; private set; }

        public TripleTreeNode Parent
        {
            get
            {
                return ((this.parent == null) ? null : this.parent);
            }
            set
            {
                this.parent = value;
            }
        }

        public bool Checked
        {
            get
            {
                return (this.checkState == System.Windows.Forms.CheckState.Checked);
            }
            set
            {
                this.CheckState = value ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
            }
        }

        public System.Windows.Forms.CheckState CheckState
        {
            get
            {
                return this.checkState;
            }
            set
            {
                this.checkState = value;
                this.SetCheckImage();
            }
        }
    }*/
}
