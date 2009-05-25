using System;
using System.Collections;
using System.Windows.Forms;
using Habanero.Base.Exceptions;
using Habanero.BO;
using Habanero.UI.Base;
using Habanero.UI.Grid;
using Habanero.Util;

namespace Habanero.UI.Forms
{
    /// <summary>
    /// Provides an editor that contains a treeview on the left and a corresponding
    /// editable grid on the right for each treeview entry.  This editor is useful
    /// for editing simple information such as lookup lists or contacts.
    /// <br/>
    /// Simply supply a data source that implements ITreeViewDataSource, which
    /// includes GetTreeViewData().  This supplies an IList with DictionaryEntry objects
    /// containing the text to appear in the treeview and the BusinessObject to model
    /// in the grid.  The grid design is drawn from the "ui" and "grid" elements in
    /// the object's class definition (XML).
    /// </summary>
    public class TreeViewTableEditor : UserControl
    {

        private ITableDataSource _tableDataSource;
        private SplitContainer _splitContainer1;
        protected TreeView _treeView;
        private GroupBox _groupBox1;
        private EditableGridWithButtons _gridAndButtons;
        private ITreeViewDataSource _treeViewDataSource;

        /// <summary>
        /// Constructor to initialise a new editor
        /// </summary>
        public TreeViewTableEditor()
        {
            Permission.Check(this);
            //BorderLayoutManager mainManager = new BorderLayoutManager(this);
            //_treeView = ControlFactory.CreateTreeView("TreeView");
            //mainManager.AddControl(_treeView, BorderLayoutManager.Position.West, true);
            InitializeComponent();

            _tableDataSource = new DatabaseTableDataSource();
            _treeViewDataSource = new NullTreeViewDataSource();
            _treeView.BeforeSelect += delegate(object sender, TreeViewCancelEventArgs e)
                  {
                      DirtyCheckHandler(sender, e);
                  };
        }

        /// <summary>
        /// Gets and sets the data source for the table
        /// </summary>
        public ITableDataSource TableDataSource
        {
            get { return _tableDataSource; }
            set { _tableDataSource = value; }
        }

        /// <summary>
        /// Gets and sets the data source for the tree view
        /// </summary>
        public ITreeViewDataSource TreeViewDataSource
        {
            get { return _treeViewDataSource; }
            set { _treeViewDataSource = value; }
        }

        /// <summary>
        /// Gets the editable grid in which values are edited
        /// </summary>
        public EditableGridWithButtons DataGrid
        {
            get { return _gridAndButtons; }
        }

        /// <summary>
        /// Fills the tree view with data
        /// </summary>
        public void PopulateTreeView()
        {
            IList data = _treeViewDataSource.GetTreeViewData();
            TreeNode currentParentNode = null;
            foreach (DictionaryEntry entry in data)
            {
                if (entry.Value == null)
                {
                    if (currentParentNode != null)
                    {
                        _treeView.Nodes.Add(currentParentNode);
                    }
                    currentParentNode = new TreeNode(entry.Key.ToString());
                }
                else
                {
                    TreeNode childNode = new TreeNode(entry.Key.ToString());
                    childNode.Tag = entry.Value;
                    currentParentNode.Nodes.Add(childNode);
                }
            }
            _treeView.Nodes.Add(currentParentNode);
            _treeView.AfterSelect += new TreeViewEventHandler(AfterSelectHandler);
        }

        /// <summary>
        /// Handles instructions to carry out a select event
        /// </summary>
        /// <param name="sender">The object that notified of the event</param>
        /// <param name="e">Attached arguments regarding the event</param>
        private void AfterSelectHandler(object sender, TreeViewEventArgs e)
        {
            NodeSelected(e);
        }

        /// <summary>
        /// Handles the event of a node being selected (this is called by
        /// AfterSelectHandler())
        /// </summary>
        /// <param name="e">Attached arguments regarding the event</param>
        protected virtual void NodeSelected(TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                BusinessObject sampleBo = (BusinessObject) e.Node.Tag;
				IBusinessObjectCollection collectionForNode;
                try
                {
                    collectionForNode = _tableDataSource.GetCollection(sampleBo);
                    _gridAndButtons.Grid.SetCollection(collectionForNode);
                    //_grid.te = e.Node.Text;
                }
                catch (Exception ex)
                {
                    ExceptionUtilities.Display(ex);
                }
            }
        }

        private void DirtyCheckHandler(object sender, TreeViewCancelEventArgs e)
        {
            if (this.DataGrid != null
                && this.DataGrid.Grid != null
                && this.DataGrid.Grid.GetCollection() != null)
            {
                if (((BusinessObjectCollection<BusinessObject>)this.DataGrid.Grid.GetCollection()).IsDirty)
                {
                    DialogResult result = MessageBox.Show("Do you want to save changes?",
                                                          "Save?",
                                                          MessageBoxButtons.YesNoCancel,
                                                          MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            ((EditableGrid) DataGrid.Grid).AcceptChanges();
                        }
                        catch (Exception)
                        {
                            e.Cancel = true;
                        }
                    }
                    if (result == DialogResult.No)
                    {
                        try
                        {
                            ((EditableGrid) DataGrid.Grid).RejectChanges();
                        }
                        catch (Exception)
                        {
                            e.Cancel = true;
                        }
                    }
                    if (result == DialogResult.Cancel) e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// An interface to model a data source for a table
        /// </summary>
        public interface ITableDataSource
        {
            /// <summary>
            /// Returns the business object collection
            /// </summary>
            /// <param name="sampleBo">A sample business object</param>
            /// <returns>Returns the business object collection</returns>
			IBusinessObjectCollection GetCollection(BusinessObject sampleBo);
        }

        /// <summary>
        /// An interface to model a data source for a tree view
        /// </summary>
        public interface ITreeViewDataSource
        {
            /// <summary>
            /// Returns a list of the tree view data
            /// </summary>
            /// <returns>Returns an IList object</returns>
            IList GetTreeViewData();
        }

        /// <summary>
        /// Manages a data source for a table
        /// </summary>
        private class DatabaseTableDataSource : ITableDataSource
        {
            /// <summary>
            /// Returns the business object collection
            /// </summary>
            /// <param name="sampleBo">A sample business object</param>
            /// <returns>Returns the business object collection</returns>
			public IBusinessObjectCollection GetCollection(BusinessObject sampleBo)
            {
                return BOLoader.Instance.GetBusinessObjectCol(sampleBo.GetType(), "", "");
            }
        }

        /// <summary>
        /// Provides an empty data source for a tree view
        /// </summary>
        private class NullTreeViewDataSource : ITreeViewDataSource
        {
            /// <summary>
            /// Returns an empty list
            /// </summary>
            /// <returns>Returns a new, empty IList object</returns>
            public IList GetTreeViewData()
            {
                return new ArrayList();
            }
        }

        /// <summary>
        /// Initialises the user interface components
        /// </summary>
        private void InitializeComponent()
        {
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._treeView = new System.Windows.Forms.TreeView();
            _treeView.Name = "TreeView";
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._gridAndButtons = new EditableGridWithButtons();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            this._groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer1
            // 
            this._splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer1.Location = new System.Drawing.Point(0, 0);
            this._splitContainer1.Name = "SplitContainer";
            // 
            // _splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.Name = "Panel1";
            this._splitContainer1.Panel1.Controls.Add(this._treeView);
            // 
            // _splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._groupBox1);
            this._splitContainer1.Size = new System.Drawing.Size(408, 231);
            this._splitContainer1.SplitterDistance = 136;
            this._splitContainer1.TabIndex = 0;
            // 
            // _treeView
            // 
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.Location = new System.Drawing.Point(0, 0);
            this._treeView.Name = "MyTreeView";
            this._treeView.Size = new System.Drawing.Size(136, 231);
            this._treeView.TabIndex = 1;
            // 
            // _groupBox1
            // 
            this._groupBox1.Controls.Add(this._gridAndButtons);
            this._groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox1.Location = new System.Drawing.Point(0, 0);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(268, 231);
            this._groupBox1.TabIndex = 0;
            this._groupBox1.TabStop = false;
            // 
            // _gridAndButtons
            // 
            this._gridAndButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridAndButtons.Location = new System.Drawing.Point(3, 16);
            this._gridAndButtons.Name = "_gridAndButtons";
            this._gridAndButtons.Size = new System.Drawing.Size(262, 212);
            this._gridAndButtons.TabIndex = 0;
            // 
            // TreeViewTableEditor
            // 
            this.Controls.Add(this._splitContainer1);
            this.Name = "TreeViewTableEditor";
            this.Size = new System.Drawing.Size(408, 231);
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel2.ResumeLayout(false);
            this._splitContainer1.ResumeLayout(false);
            this._groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        internal void Initialise()
        {
            this.InitializeComponent();
        }
    }
}