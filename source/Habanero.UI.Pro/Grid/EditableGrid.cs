using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Habanero.BO;
using Habanero.UI.Base;
using Habanero.UI.Grid;

namespace Habanero.UI.Grid
{
    /// <summary>
    /// Manages an editable grid, that displays a business object
    /// collection that has been pre-loaded.<br/>
    /// NOTE: Changes are not persisted until AcceptChanges() is called.
    /// Either use EditableGridWithButtons, which has a Save and Cancel button
    /// attached, or add some feature that causes changes to be saved
    /// once the user has finished editing.<br/>
    /// Note that this grid does not warn the user when it is being closed
    /// with unsaved changes.  The Closing event on the parent form needs to
    /// be used to do a dirty check and prevent closing.
    /// </summary>
    public class EditableGrid : GridBase, IEditableGrid
    {
        private bool _confirmDeletion;
        private bool _comboBoxClickOnce;
        private DeleteKeyBehaviours _deleteKeyBehaviour;
        
        /// <summary>
        /// Indicates what action should be taken when a selection of
        /// cells is selected and the Delete key is pressed.  Note that
        /// this has no correlation to how DataGridView handles the
        /// Delete key when the full row has been selected.
        /// </summary>
        public enum DeleteKeyBehaviours
        {
            /// <summary>Nothing is done</summary>
            None,
            /// <summary>Each row containing part of the selection is deleted</summary>
            DeleteRow,
            /// <summary>Clears the contents of the selected cells</summary>
            ClearContents
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditableGrid()
        {
            Permission.Check(this);
            _confirmDeletion = false;
            _comboBoxClickOnce = true;
            _deleteKeyBehaviour = DeleteKeyBehaviours.DeleteRow;
            CompulsoryColumnsBold = true;

            UserDeletingRow += new DataGridViewRowCancelEventHandler(ConfirmRowDeletion);
            CellClick += new DataGridViewCellEventHandler(CellClickHandler);
        }

        /// <summary>
        /// Gets or sets the boolean value that determines whether to confirm
        /// deletion with the user when they have chosen to delete a row
        /// </summary>
        public bool ConfirmDeletion
        {
            get { return _confirmDeletion; }
            set { _confirmDeletion = value; }
        }

        /// <summary>
        /// Gets or sets the boolean value that determines whether clicking on
        /// a ComboBox cell causes the drop-down to appear immediately.  Set this
        /// to false if the user should click twice (first to select, then to
        /// edit).
        /// </summary>
        public bool ComboBoxClickOnce
        {
            get { return _comboBoxClickOnce; }
            set { _comboBoxClickOnce = value; }
        }

        /// <summary>
        /// Indicates what action should be taken when a selection of
        /// cells is selected and the Delete key is pressed.  Note that
        /// this has no correlation to how DataGridView handles the
        /// Delete key when the full row has been selected.
        /// </summary>
        public DeleteKeyBehaviours DeleteKeyBehaviour
        {
            get { return _deleteKeyBehaviour; }
            set { _deleteKeyBehaviour = value; }
        }

        /// <summary>
        /// Returns the selected business object
        /// </summary>
        public BusinessObject SelectedBusinessObject
        {
            get { return this.GetSelectedBusinessObject(); }
        }

        /// <summary>
        /// Accepts and saves all changes to the data table
        /// </summary>
        public void AcceptChanges()
        {
            this._dataTable.AcceptChanges();
        }

        /// <summary>
        /// Saves changes made on the grid (calls AcceptChanges())
        /// </summary>
        public void SaveChanges()
        {
            AcceptChanges();
        }

        /// <summary>
        /// Rolls back all changes to the data table
        /// </summary>
        public void RejectChanges()
        {
            this._dataTable.RejectChanges();
        }

        /// <summary>
        /// If deletion is to be confirmed, checks deletion with the user before continuing
        /// </summary>
        private void ConfirmRowDeletion(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (ConfirmDeletion && MessageBox.Show("Are you sure you want to delete the selected row(s)?",
                    "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Creates a new data set provider for the given business object
        /// collection
        /// </summary>
        /// <param name="col">The business object collection</param>
        /// <returns>Returns a new data set provider</returns>
        protected override BOCollectionDataSetProvider CreateBusinessObjectCollectionDataSetProvider(
			IBusinessObjectCollection col)
        {
            return new BOCollectionEditableDataSetProvider(col);
        }

        /// <summary>
        /// When the delete key has been pressed, deletes the current row
        /// unless the current cell is in editing mode.  This does not
        /// apply to full row selection, which is handled anyway.
        /// </summary>
        private void DeleteKeyPressHandler()
        {
            if (!CurrentCell.IsInEditMode && SelectedRows.Count == 0 && CurrentRow != null)
            {
                if (DeleteKeyBehaviour == DeleteKeyBehaviours.DeleteRow && AllowUserToDeleteRows)
                {
                    if (!ConfirmDeletion || MessageBox.Show("Are you sure you want to delete the selected row(s)?",
                                                            "Delete?", MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        ArrayList rowIndexes = new ArrayList();
                        foreach (DataGridViewCell cell in SelectedCells)
                        {
                            if (!rowIndexes.Contains(cell.RowIndex) && cell.RowIndex != NewRowIndex)
                            {
                                rowIndexes.Add(cell.RowIndex);
                            }
                        }
                        rowIndexes.Sort();

                        for (int row = rowIndexes.Count - 1; row >= 0; row--)
                        {
                            Rows.RemoveAt((int) rowIndexes[row]);
                        }
                    }
                }
                if (DeleteKeyBehaviour == DeleteKeyBehaviours.ClearContents)
                {
                    foreach (DataGridViewCell cell in SelectedCells)
                    {
                        cell.Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// A Microsoft-suggested override to catch key presses, since KeyPress does
        /// not work correctly on DataGridView
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);
            if (key == Keys.Delete)
            {
                DeleteKeyPressHandler();
                return this.ProcessDeleteKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// A Microsoft-suggested override to catch key presses, since KeyPress does
        /// not work correctly on DataGridView
        /// </summary>
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteKeyPressHandler();
                return this.ProcessDeleteKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// Carries out additional actions when a cell is clicked.  Specifically, if
        /// a combobox cell is clicked, the cell goes into edit mode immediately.
        /// </summary>
        private void CellClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (!CurrentCell.IsInEditMode && Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    BeginEdit(true);
                    if (EditingControl is DataGridViewComboBoxEditingControl)
                    {
                        ((DataGridViewComboBoxEditingControl) EditingControl).DroppedDown = true;
                    }
                }
            }
        }
    }
}