// ---------------------------------------------------------------------------------
//  Copyright (C) 2009 Chillisoft Solutions
//  
//  This file is part of the Habanero framework.
//  
//      Habanero is a free framework: you can redistribute it and/or modify
//      it under the terms of the GNU Lesser General Public License as published by
//      the Free Software Foundation, either version 3 of the License, or
//      (at your option) any later version.
//  
//      The Habanero framework is distributed in the hope that it will be useful,
//      but WITHOUT ANY WARRANTY; without even the implied warranty of
//      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//      GNU Lesser General Public License for more details.
//  
//      You should have received a copy of the GNU Lesser General Public License
//      along with the Habanero framework.  If not, see <http://www.gnu.org/licenses/>.
// ---------------------------------------------------------------------------------
using Habanero.UI.Base;
using Habanero.UI.VWG;
using Habanero.UI.Win;
using NUnit.Framework;

namespace Habanero.Test.UI.Base
{
    /// <summary>
    /// This test class tests the base inherited methods of the TabControl class.
    /// </summary>
    [TestFixture]
    public class TestBaseMethodsWin_TabControl : TestBaseMethods.TestBaseMethodsWin
    {
        protected override IControlHabanero CreateControl()
        {
            return GetControlFactory().CreateTabControl();
        }
    }

    /// <summary>
    /// This test class tests the base inherited methods of the TabControl class.
    /// </summary>
    [TestFixture]
    public class TestBaseMethodsVWG_TabControl : TestBaseMethods.TestBaseMethodsVWG
    {
        protected override IControlHabanero CreateControl()
        {
            return GetControlFactory().CreateTabControl();
        }
    }


    /// <summary>
    /// This test class tests the TabControl class.
    /// </summary>
    public abstract class TestTabControl
    {
        protected abstract IControlFactory GetControlFactory();

        protected ITabControl CreateTabControl()
        {
            return GetControlFactory().CreateTabControl();
        }

        [TestFixture]
        public class TestTabControlWin : TestTabControl
        {
            protected override IControlFactory GetControlFactory()
            {
                return new ControlFactoryWin();
            }
        }

        [TestFixture]
        public class TestTabControlVWG : TestTabControl
        {
            protected override IControlFactory GetControlFactory()
            {
                return new ControlFactoryVWG();
            }
        }

        [Test]
        public void Test_Create()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            ITabControl tabControl = CreateTabControl();
            //---------------Test Result -----------------------
            Assert.IsNotNull(tabControl);
        }
    }
}
