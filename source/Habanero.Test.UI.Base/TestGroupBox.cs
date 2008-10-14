using System;
using System.Collections.Generic;
using System.Text;
using Habanero.UI.Base;
using NUnit.Framework;

namespace Habanero.Test.UI.Base
{
    /// <summary>
    /// This test class tests the base inherited methods of the GroupBox class.
    /// </summary>
    public class TestBaseMethodsWin_GroupBox : TestBaseMethods.TestBaseMethodsWin
    {
        protected override IControlHabanero CreateControl()
        {
            return GetControlFactory().CreateGroupBox();
        }
    }

    /// <summary>
    /// This test class tests the base inherited methods of the GroupBox class.
    /// </summary>
    public class TestBaseMethodsVWG_GroupBox : TestBaseMethods.TestBaseMethodsVWG
    {
        protected override IControlHabanero CreateControl()
        {
            return GetControlFactory().CreateGroupBox();
        }
    }

    /// <summary>
    /// This test class tests the GroupBox class.
    /// </summary>
    [TestFixture]
    public class TestGroupBox
    {
    }
}
