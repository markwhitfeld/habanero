namespace Habanero.UI.Base
{
    public enum ErrorBlinkStyleChilli
    {
        /// <summary>
        /// Always blink when the error icon is first displayed, or when a error description string is set for the control and the error icon is already displayed.
        /// </summary>
        AlwaysBlink = 1,
        /// <summary>
        /// Blinks when the icon is already displayed and a new error string is set for the control.
        /// </summary>
        BlinkIfDifferentError = 0,
        /// <summary>
        /// Never blink the error icon.
        /// </summary>
        NeverBlink = 2
    }


    /// <summary>
    /// Specifies constants indicating the locations that an error icon can appear in relation to the control with an error.
    /// </summary>
    //[Serializable()]
    public enum ErrorIconAlignmentChilli
    {
        /// <summary>
        /// The icon appears aligned with the bottom of the control and the left of the control.
        /// </summary>
        BottomLeft = 4,

        /// <summary>
        /// The icon appears aligned with the bottom of the control and the right of the control.
        /// </summary>
        BottomRight = 5,

        /// <summary>
        /// The icon appears aligned with the middle of the control and the left of the control.
        /// </summary>
        MiddleLeft = 2,


        /// <summary>
        /// The icon appears aligned with the middle of the control and the right of the control.
        /// </summary>
        MiddleRight = 3,

        /// <summary>
        /// The icon appears aligned with the top of the control and to the left of the control.
        /// </summary>
        TopLeft = 0,

        /// <summary>
        /// The icon appears aligned with the top of the control and to the right of the control.
        /// </summary>
        TopRight = 1
    }

    public interface IErrorProvider
    {
        /// <summary>
        /// Returns the current error description string for the specified control.
        /// </summary>
        ///	<returns>The error description string for the specified control.</returns>
        ///	<param name="objControl">The item to get the error description string for. </param>
        ///	<exception cref="T:System.ArgumentNullException">control is null.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        string GetError(IControlChilli objControl);

        /// <summary>
        /// Gets a value indicating where the error icon should be placed in relation to the control.
        /// </summary>
        ///	<returns>One of the <see cref="T:Habanero.UI.Base.ErrorIconAlignmentChilli"></see> values. The default icon alignment is <see cref="F:Habanero.UI.Base.ErrorIconAlignmentChilli.MiddleRight"></see>.</returns>
        ///	<param name="objControl">The control to get the icon location for. </param>
        ///	<exception cref="T:System.ArgumentNullException">control is null.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        ErrorIconAlignmentChilli GetIconAlignment(IControlChilli objControl);

        /// <summary>
        /// Returns the amount of extra space to leave next to the error icon.
        /// </summary>
        /// <returns>The number of pixels to leave between the icon and the control. </returns>
        /// <param name="objControl">The control to get the padding for. </param>
        /// <exception cref="T:System.ArgumentNullException">control is null.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        int GetIconPadding(IControlChilli objControl);

        /// <summary>
        /// Sets the error description string for the specified control.
        /// </summary>
        /// <param name="objControl">The control to set the error description string for. </param>
        /// <param name="strValue">The error description string. </param>
        /// <exception cref="T:System.ArgumentNullException">control is null.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        void SetError(IControlChilli objControl, string strValue);

        /// <summary>
        /// Sets the location where the error icon should be placed in relation to the control.
        /// </summary>
        /// <param name="objControl">The control to set the icon location for. </param>
        /// <param name="enmValue">One of the <see cref="T:Habanero.UI.Base.ErrorIconAlignmentChilli"/> values. </param>
        /// <exception cref="T:System.ArgumentNullException">control is null.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        void SetIconAlignment(IControlChilli objControl, ErrorIconAlignmentChilli enmValue);

        /// <summary>
        /// Sets the amount of extra space to leave between the specified control and the error icon.
        /// </summary>
        /// <param name="objControl">The control to set the padding for. </param>
        /// <param name="intPadding">The number of pixels to add between the icon and the control. </param>
        /// <exception cref="T:System.ArgumentNullException">control is null.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        void SetIconPadding(IControlChilli objControl, int intPadding);

        /// <summary>
        /// Provides a method to update the bindings of the <see cref="P:Habanero.UI.Base.IErrorProvider.DataSource"/>, <see cref="P:Habanero.UI.Base.IErrorProvider.DataMember"/>, and the error text.
        /// </summary>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        void UpdateBinding();

        /// <summary>
        /// Gets a value indicating whether a control can be extended.
        /// </summary>
        /// <returns>true if the control can be extended; otherwise, false.This property will be true if the object is a <see cref="T:Habanero.UI.Base.IControlChilli"/>.</returns>
        /// <param name="objExtendee">The control to be extended. </param>
        bool CanExtend(object objExtendee);

        /// <summary>
        /// Gets or sets the rate at which the error icon flashes.
        /// </summary>
        /// <returns>The rate, in milliseconds, at which the error icon should flash. The default is 250 milliseconds.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>

        int BlinkRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when the error icon flashes.
        /// </summary>
        /// <returns>One of the <see cref="T:Habanero.UI.Base.ErrorBlinkStyleChilli"/> values. The default is <see cref="F:Habanero.UI.Base.ErrorBlinkStyleChilli.BlinkIfDifferentError"/>.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:Habanero.UI.Base.ErrorBlinkStyleChilli"/> values. </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>

        ErrorBlinkStyleChilli BlinkStyleChilli { get; set; }
        /// <summary>
        /// Gets or sets the list within a data source to monitor.
        /// </summary>
        /// <returns>The string that represents a list within the data source specified by the <see cref="P:Habanero.UI.Base.IErrorProvider.DataSource"/> to be monitored. Typically, this will be a <see cref="T:System.Data.DataTable"/>.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        string DataMember { get; set; }

        /// <summary>
        /// Gets or sets the data source that the <see cref="T:Habanero.UI.Base.IErrorProvider"/> monitors.
        /// </summary>
        /// <returns>A data source based on the <see cref="T:System.Collections.IList"/> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet"/> to be monitored for errors.</returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        object DataSource { get; set; }
    }
}