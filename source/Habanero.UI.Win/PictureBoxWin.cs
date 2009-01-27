using System.Windows.Forms;
using Habanero.UI.Base;
using ScrollBars=Habanero.UI.Base.ScrollBars;

namespace Habanero.UI.Win
{
    /// <summary>
    /// Represents a TextBox control
    /// </summary>
    public class PictureBoxWin : PictureBox, IPictureBox
    {
        /// <summary>
        /// Gets or sets the anchoring style.
        /// </summary>
        /// <value></value>
        Base.AnchorStyles IControlHabanero.Anchor
        {
            get { return (Base.AnchorStyles)base.Anchor; }
            set { base.Anchor = (System.Windows.Forms.AnchorStyles)value; }
        }

        /// <summary>
        /// Gets the collection of controls contained within the control
        /// </summary>
        IControlCollection IControlHabanero.Controls
        {
            get { return new ControlCollectionWin(base.Controls); }
        }

        /// <summary>
        /// Gets or sets which control borders are docked to its parent
        /// control and determines how a control is resized with its parent
        /// </summary>
        Base.DockStyle IControlHabanero.Dock
        {
            get { return DockStyleWin.GetDockStyle(base.Dock); }
            set { base.Dock = DockStyleWin.GetDockStyle(value); }
        }

        #region Implementation of IPictureBox

        /// <summary>
        /// Indicates how the image is displayed.
        /// </summary>
        ///	<returns>One of the <see cref="Habanero.UI.Base.PictureBoxSizeMode"></see> values. The default is <see cref="Habanero.UI.Base.PictureBoxSizeMode.Normal"></see>.</returns>
        ///	<exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="Habanero.UI.Base.PictureBoxSizeMode"></see> values. </exception>
        //[DefaultValue(0), Localizable(true), SRDescription("PictureBoxSizeModeDescr"), SRCategory("CatBehavior"), RefreshProperties(RefreshProperties.Repaint)]
        Base.PictureBoxSizeMode IPictureBox.SizeMode
        {
            get { return (Base.PictureBoxSizeMode)base.SizeMode; }
            set { base.SizeMode = (System.Windows.Forms.PictureBoxSizeMode)value; }
        }

        #endregion
    }


}