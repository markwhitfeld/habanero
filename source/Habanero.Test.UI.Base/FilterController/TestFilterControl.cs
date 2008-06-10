using System;
using System.Collections;
using System.Collections.Generic;
using Habanero.Base;
using Habanero.UI.Base;
using Habanero.UI.Base.FilterControl;
using Habanero.UI.WebGUI;
using Habanero.UI.Win;
using NUnit.Framework;

namespace Habanero.Test.UI.Base.FilterController
{
    [TestFixture]
    public class TestFilterControl
    {
        [SetUp]
        public void SetupTest()
        {
            //Runs every time that any testmethod is executed
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            //Code that is executed before any test is run in this class. If multiple tests
            // are executed then it will still only be called once.
        }

        [TearDown]
        public void TearDownTest()
        {
            //runs every time any testmethod is complete
        }

        [Test]
        public void Test_GizOnly_SetFilterModeSearchSetsText()
        {
            //---------------Set up test pack-------------------
            IControlFactory factory = new ControlFactoryGizmox();
            IFilterControl ctl = factory.CreateFilterControl();
            //---------------Assert Preconditions --------------
            Assert.AreEqual("Filter", ctl.FilterButton.Text);
            //---------------Execute Test ----------------------
            ctl.FilterMode = FilterModes.Search;
            //---------------Test Result -----------------------
            Assert.AreEqual("Search", ctl.FilterButton.Text);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void Test_GizOnly_SetFilterModeFilterSetsText()
        {
            //---------------Set up test pack-------------------
            IControlFactory factory = new ControlFactoryGizmox();
            IFilterControl ctl = factory.CreateFilterControl();
            ctl.FilterMode = FilterModes.Search;
            //---------------Assert Preconditions --------------
            Assert.AreEqual("Search", ctl.FilterButton.Text);
            //---------------Execute Test ----------------------
            ctl.FilterMode = FilterModes.Filter;
            //---------------Test Result -----------------------
            Assert.AreEqual("Filter", ctl.FilterButton.Text);
        }

        [Test]
        public void TestSetLayoutManagerWinForms()
        {
            TestSetLayoutManager(new ControlFactoryWin());
        }

        [Test]
        public void TestSetLayoutManagerGiz()
        {
            TestSetLayoutManager(new ControlFactoryGizmox());
        }

        public void TestSetLayoutManager(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            IPanel panel = factory.CreatePanel();
            GridLayoutManager layoutManager = new GridLayoutManager(panel, factory);
            //---------------Execute Test ----------------------
            filterControl.LayoutManager = layoutManager;
            //---------------Test Result -----------------------
            Assert.AreEqual(layoutManager, filterControl.LayoutManager);
            Assert.IsNotNull(filterControl.FilterPanel);
            //---------------Tear Down -------------------------          
        }

        //[Test]
        //public void Test_ControlsLayedOutUsingColumnLayout()
        //{
        //    //---------------Set up test pack-------------------
        //    IControlFactory factory = new ControlFactoryGizmox();
        //    IFilterControl ctl = factory.CreateFilterControl();
        //    //--------------Assert PreConditions----------------            

        //    //---------------Execute Test ----------------------

        //    //---------------Test Result -----------------------

        //    //---------------Tear Down -------------------------          
        //}

        [Test]
        public void TestFilterButtonAccessorGizmox()
        {
            TestFilterButtonAccessor(new ControlFactoryGizmox());
        }

        [Test]
        public void TestFilterButtonAccessorWinForms()
        {
            try
            {
                TestFilterButtonAccessor(new ControlFactoryWin());
            }
            catch (NotImplementedException ex)
            {
                StringAssert.Contains("not implemented on win", ex.Message);
            }
        }

        public void TestFilterButtonAccessor(IControlFactory factory)
        {
            //---------------Set up test pack-------------------

            //---------------Execute Test ----------------------
            IFilterControl filterControl = factory.CreateFilterControl();

            //---------------Test Result -----------------------
            Assert.IsNotNull(filterControl.FilterButton);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestClearButtonAccessorGizmox()
        {
            TestClearButtonAccessor(new ControlFactoryGizmox());
        }

        [Test]
        public void TestClearButtonAccessorWinForms()
        {
            try
            {
                TestClearButtonAccessor(new ControlFactoryWin());
            }
            catch (NotImplementedException ex)
            {
                StringAssert.Contains("not implemented on win", ex.Message);
            }
        }

        public void TestClearButtonAccessor(IControlFactory factory)
        {
            //---------------Set up test pack-------------------

            //---------------Execute Test ----------------------
            IFilterControl filterControl = factory.CreateFilterControl();

            //---------------Test Result -----------------------
            Assert.IsNotNull(filterControl.ClearButton);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestApplyFilterWin()
        {
            //---------------Set up test pack-------------------
            
            IFilterControl filterControl = new ControlFactoryWin().CreateFilterControl();

            //---------------Execute Test ----------------------
            try
            {
                filterControl.ApplyFilter();    
            }
            
            //---------------Test Result -----------------------
            catch (NotImplementedException ex)
            {
                StringAssert.Contains("not implemented on win", ex.Message);
            }
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestClearFiltersWin()
        {
            //---------------Set up test pack-------------------

            IFilterControl filterControl = new ControlFactoryWin().CreateFilterControl();

            //---------------Execute Test ----------------------
            try
            {
                filterControl.ClearFilters();
            }

            //---------------Test Result -----------------------
            catch (NotImplementedException ex)
            {
                StringAssert.Contains("not implemented on win", ex.Message);
            }
            //---------------Tear Down -------------------------
        }

        #region TextBoxFilter

        #region TestAddTextBox

        [Test]
        public void Test_GizOnly_DefaultLayoutManager()
        {
            //---------------Set up test pack-------------------
            IControlFactory factory = new ControlFactoryGizmox();

            //---------------Execute Test ----------------------
//            IControlChilli control = factory.CreatePanel();
            IFilterControl ctl = factory.CreateFilterControl();
            //---------------Test Result -----------------------
            Assert.IsInstanceOfType(typeof (FlowLayoutManager), ctl.LayoutManager);
        }

        [Test]
        public void TestAddTextBoxGizmox()
        {
            TestAddTextBox(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAddTestBoxWinForms()
        {
            TestAddTextBox(new ControlFactoryWin());
        }

        public void TestAddTextBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl ctl = factory.CreateFilterControl();

            //---------------Execute Test ----------------------
            ITextBox myTextBox = ctl.AddStringFilterTextBox("", "");

            //---------------Test Result -----------------------
            Assert.IsNotNull(myTextBox);

            //---------------Tear Down -------------------------          
        }

        [Test]
        public void Test_SetFilterHeaderGizmox()
        {
            Test_SetFilterHeader(new ControlFactoryGizmox());
        }

        [Test]
        public void Test_SetFilterHeaderWinForms()
        {
            try
            {
                Test_SetFilterHeader(new ControlFactoryWin());
                Assert.Fail("Should throw a not implemented exception");
            }
            catch (NotImplementedException ex)
            {
                StringAssert.Contains("not implemented on win", ex.Message);
            }
        }

        public void Test_SetFilterHeader(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl ctl = factory.CreateFilterControl();
            //---------------Assert Preconditions---------------
            Assert.AreEqual("Filter the Grid", ctl.HeaderText);
            //---------------Execute Test ----------------------
            ctl.HeaderText = "Filter Assets";

            //---------------Test Result -----------------------
            Assert.AreEqual("Filter Assets", ctl.HeaderText);

            //---------------Tear Down -------------------------          
        }

        #endregion

        #region TestAddStringFilterTextBox

        [Test]
        public void TestAddStringFilterTextBoxWinForms()
        {
            TestAddStringFilterTextBox(new ControlFactoryWin());
        }

        [Test]
        public void TestAddStringFilterTextBoxGiz()
        {
            TestAddStringFilterTextBox(new ControlFactoryGizmox());
        }

        public void TestAddStringFilterTextBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClause nullClause = new DataViewNullFilterClause();
            IFilterControl filterControl = factory.CreateFilterControl();
            //---------------Execute Test ----------------------
            ITextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn");
            tb.Text = "";
            //---------------Test Result -----------------------
            Assert.AreEqual(nullClause.GetFilterClauseString(), filterControl.GetFilterClause().GetFilterClauseString());
            Assert.AreEqual(1, filterControl.FilterControls.Count);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBoxWin()
        {
            TestAddStringFilterTextBox(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBoxGiz()
        {
            TestAdd_TwoStringFilterTextBox(new ControlFactoryGizmox());
        }

        public void TestAdd_TwoStringFilterTextBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            //---------------Execute Test ----------------------
            filterControl.AddStringFilterTextBox("Test:", "TestColumn");
            filterControl.AddStringFilterTextBox("Test2:", "TestColumn2");
            //---------------Test Result -----------------------
            Assert.AreEqual(2, filterControl.FilterControls.Count);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_GetControlWin()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_GetControlGiz()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryGizmox());
        }

        public void TestAdd_TwoStringFilterTextBox_GetControl(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            ITextBox tbExpected = filterControl.AddStringFilterTextBox("Test:", "TestColumn");
            filterControl.AddStringFilterTextBox("Test2:", "TestColumn2");
            //---------------Execute Test ----------------------
            ITextBox tbReturned = (ITextBox) filterControl.GetChildControl("TestColumn");
            //---------------Test Result -----------------------
            Assert.AreSame(tbExpected, tbReturned);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_Combo_GetControlWin()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_Combo_GetControlGiz()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryGizmox());
        }

        public void TestAdd_TwoStringFilterTextBox_Combo_GetControl(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            IComboBox tbExpected = filterControl.AddStringFilterComboBox("Test:", "TestColumn", null, false);
            filterControl.AddStringFilterTextBox("Test2:", "TestColumn2");
            //---------------Execute Test ----------------------
            IComboBox tbReturned = (IComboBox) filterControl.GetChildControl("TestColumn");
            //---------------Test Result -----------------------
            Assert.AreSame(tbExpected, tbReturned);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_DateTime_GetControlWin()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_DateTime__GetControlGiz()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryGizmox());
        }

        public void TestAdd_TwoStringFilterTextBox_DateTime__GetControl(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            IDateTimePicker tbExpected =
                filterControl.AddDateFilterDateTimePicker("Test:", "TestColumn", DateTime.Now,
                                                          FilterClauseOperator.OpEquals, false);
            filterControl.AddStringFilterTextBox("Test2:", "TestColumn2");
            //---------------Execute Test ----------------------
            IDateTimePicker tbReturned = (IDateTimePicker) filterControl.GetChildControl("TestColumn");
            //---------------Test Result -----------------------
            Assert.AreSame(tbExpected, tbReturned);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_CheckBox_GetControlWin()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_CheckBox__GetControlGiz()
        {
            TestAdd_TwoStringFilterTextBox_GetControl(new ControlFactoryGizmox());
        }

        public void TestAdd_TwoStringFilterTextBox_CheckBox__GetControl(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            ICheckBox tbExpected = filterControl.AddBooleanFilterCheckBox("Test:", "TestColumn", false);
            filterControl.AddStringFilterTextBox("Test2:", "TestColumn2");
            //---------------Execute Test ----------------------
            ICheckBox tbReturned = (ICheckBox) filterControl.GetChildControl("TestColumn");
            //---------------Test Result -----------------------
            Assert.AreSame(tbExpected, tbReturned);
            //---------------Tear Down -------------------------          
        }


        [Test]
        public void TestAdd_TwoStringFilterTextBox_CheckBoxWin()
        {
            TestAdd_TwoStringFilterTextBox_CheckBox(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_TwoStringFilterTextBox_CheckBoxGiz()
        {
            TestAdd_TwoStringFilterTextBox_CheckBox(new ControlFactoryGizmox());
        }

        public void TestAdd_TwoStringFilterTextBox_CheckBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();

            //---------------Execute Test ----------------------
            ICheckBox cb = filterControl.AddBooleanFilterCheckBox("Test:", "TestColumn", false);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, filterControl.FilterPanel.Controls.Count);
            Assert.IsTrue(filterControl.FilterPanel.Controls.Contains(cb));
            //---------------Tear Down -------------------------          
        }

        #endregion

        [Test]
        public void TestGetTextBoxFilterClauseWinForms()
        {
            TestGetTextBoxFilterClause(new ControlFactoryWin());
        }

        [Test]
        public void TestGetTextBoxFilterClauseGiz()
        {
            TestGetTextBoxFilterClause(new ControlFactoryGizmox());
        }

        public void TestGetTextBoxFilterClause(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory itsFilterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            ITextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn");

            //---------------Execute Test ----------------------
            tb.Text = "testvalue";
            string filterClauseString = filterControl.GetFilterClause().GetFilterClauseString();

            //---------------Test Result -----------------------
            IFilterClause clause =
                itsFilterClauseFactory.CreateStringFilterClause("TestColumn", FilterClauseOperator.OpLike, "testvalue");
            Assert.AreEqual(clause.GetFilterClauseString(), filterClauseString);

            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestGetTextBoxFilterClause_EqualsWinForms()
        {
            TestGetTextBoxFilterClause_Equals(new ControlFactoryWin());
        }

        [Test]
        public void TestGetTextBoxFilterClause_EqualsGiz()
        {
            TestGetTextBoxFilterClause_Equals(new ControlFactoryGizmox());
        }

        public void TestGetTextBoxFilterClause_Equals(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory itsFilterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            ITextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn", FilterClauseOperator.OpEquals);

            //---------------Execute Test ----------------------
            tb.Text = "testvalue";
            string filterClauseString = filterControl.GetFilterClause().GetFilterClauseString();

            //---------------Test Result -----------------------
            IFilterClause clause =
                itsFilterClauseFactory.CreateStringFilterClause("TestColumn", FilterClauseOperator.OpEquals, "testvalue");
            Assert.AreEqual(clause.GetFilterClauseString(), filterClauseString);

            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestTwoStringTextBoxFilterWinForms()
        {
            TestTwoStringTextBoxFilter(new ControlFactoryWin());
        }

        [Test]
        public void TestTwoStringTextBoxFilterGiz()
        {
            TestTwoStringTextBoxFilter(new ControlFactoryGizmox());
        }

        public void TestTwoStringTextBoxFilter(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory itsFilterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            ITextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn");
            tb.Text = "testvalue";
            ITextBox tb2 = filterControl.AddStringFilterTextBox("Test:", "TestColumn2");
            tb2.Text = "testvalue2";

            //---------------Execute Test ----------------------
            string filterClauseString = filterControl.GetFilterClause().GetFilterClauseString();

            //---------------Test Result -----------------------
            IFilterClause clause1 =
                itsFilterClauseFactory.CreateStringFilterClause("TestColumn", FilterClauseOperator.OpLike, "testvalue");
            IFilterClause clause2 =
                itsFilterClauseFactory.CreateStringFilterClause("TestColumn2", FilterClauseOperator.OpLike, "testvalue2");
            IFilterClause fullClause =
                itsFilterClauseFactory.CreateCompositeFilterClause(clause1, FilterClauseCompositeOperator.OpAnd, clause2);
            Assert.AreEqual(fullClause.GetFilterClauseString(), filterClauseString);

            //---------------Tear Down -------------------------          
        }


        [Test]
        public void TestLabelAndTextBoxAreOnPanelWinForms()
        {
            TestLabelAndTextBoxAreOnPanel(new ControlFactoryWin());
        }

        [Test]
        public void TestLabelAndTextBoxAreOnPanelGiz()
        {
            TestLabelAndTextBoxAreOnPanel(new ControlFactoryGizmox());
        }

        [Test]
        public void TestLabelAndTextBoxAreOnPanel(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();

            //---------------Assert Preconditions --------------
            Assert.AreEqual(0, filterControl.FilterPanel.Controls.Count);

            //---------------Execute Test ----------------------
            ITextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn");

            //---------------Test Result -----------------------

            Assert.AreEqual(2, filterControl.FilterPanel.Controls.Count);
            Assert.IsTrue(filterControl.FilterPanel.Controls.Contains(tb));
            //---------------Tear Down -------------------------          
        }

        #endregion

        #region ComboBoxFilter

        //------------------------COMBO BOX----------------------------------------------------------

        [Test]
        public void TestAddComboBoxGizmox()
        {
            TestAddComboBox(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAddComboBoxWinForms()
        {
            TestAddComboBox(new ControlFactoryWin());
        }

        public void TestAddComboBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            //IFilterClause nullClause = new DataViewNullFilterClause();
            IFilterControl filterControl = factory.CreateFilterControl();
            //---------------Execute Test ----------------------
            IComboBox cb = filterControl.AddStringFilterComboBox("t", "TestColumn", new ArrayList(), true);

            //---------------Test Result -----------------------
            Assert.IsNotNull(cb);

            //---------------Tear Down -------------------------          
        }


        [Test]
        public void TestAddStringFilterComboBoxGiz()
        {
            TestAddStringFilterComboBox(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAddStringFilterComboBoxWinForms()
        {
            TestAddStringFilterComboBox(new ControlFactoryWin());
        }

        public void TestAddStringFilterComboBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClause nullClause = new DataViewNullFilterClause();
            IFilterControl filterControl = factory.CreateFilterControl();
            //---------------Execute Test ----------------------
            filterControl.AddStringFilterComboBox("Test:", "TestColumn", new ArrayList(), true);
            //---------------Test Result -----------------------
            Assert.AreEqual(nullClause.GetFilterClauseString(), filterControl.GetFilterClause().GetFilterClauseString());

            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestGetComboBoxAddSelectedItemsGiz()
        {
            TestGetComboBoxAddSelectedItems(new ControlFactoryGizmox());
        }

        public void TestGetComboBoxAddSelectedItemsWin()
        {
            TestGetComboBoxAddSelectedItems(new ControlFactoryWin());
        }

        public void TestGetComboBoxAddSelectedItems(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            IList options = new ArrayList();
            options.Add("1");
            options.Add("2");
            //---------------Execute Test ----------------------
            IComboBox comboBox = filterControl.AddStringFilterComboBox("Test:", "TestColumn", options, true);
            //---------------Test Result -----------------------
            int numOfItemsInCollection = 2;
            int numItemsExpectedInComboBox = numOfItemsInCollection + 1; //one extra for the null selected item
            Assert.AreEqual(numItemsExpectedInComboBox, comboBox.Items.Count);
        }

        [Test]
        public void TestSelectItemWinForms()
        {
            TestSelectItem(new ControlFactoryWin());
        }

        [Test]
        public void TestSelectItemGiz()
        {
            TestSelectItem(new ControlFactoryGizmox());
        }

        public void TestSelectItem(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterControl filterControl = factory.CreateFilterControl();
            IList options = new ArrayList();
            options.Add("1");
            options.Add("2");
            IComboBox comboBox = filterControl.AddStringFilterComboBox("Test:", "TestColumn", options, true);
            //---------------Execute Test ----------------------
            comboBox.SelectedIndex = 1;
            //---------------Test Result -----------------------
            Assert.AreEqual("1", comboBox.SelectedItem.ToString());
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestGetComboBoxFilterClauseWinForms()
        {
            TestGetComboBoxFilterClause(new ControlFactoryWin());
        }

        [Test]
        public void TestGetComboBoxFilterClauseGiz()
        {
            TestGetComboBoxFilterClause(new ControlFactoryGizmox());
        }

        public void TestGetComboBoxFilterClause(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory filterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            IComboBox comboBox = GetFilterComboBox_2Items(filterControl);

            //---------------Execute Test ----------------------
            comboBox.SelectedIndex = 1;
            string filterClauseString = filterControl.GetFilterClause().GetFilterClauseString();

            //---------------Test Result -----------------------
            IFilterClause clause =
                filterClauseFactory.CreateStringFilterClause("TestColumn", FilterClauseOperator.OpEquals, "1");
            Assert.AreEqual(clause.GetFilterClauseString(), filterClauseString);

            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestGetComboBoxFilterClauseNoSelectionWinForms()
        {
            TestGetComboBoxFilterClauseNoSelection(new ControlFactoryWin());
        }

        [Test]
        public void TestGetComboBoxFilterClauseNoSelectionGiz()
        {
            TestGetComboBoxFilterClauseNoSelection(new ControlFactoryGizmox());
        }

        public void TestGetComboBoxFilterClauseNoSelection(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory filterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            IComboBox comboBox = GetFilterComboBox_2Items(filterControl);
            //---------------Execute Test ----------------------
            comboBox.SelectedIndex = -1;
            string filterClauseString = filterControl.GetFilterClause().GetFilterClauseString();
            //---------------Test Result -----------------------
            IFilterClause clause = filterClauseFactory.CreateNullFilterClause();
            Assert.AreEqual(clause.GetFilterClauseString(), filterClauseString);
            //---------------Tear Down -------------------------          
        }

        [Test]
        public void TestGetComboBoxFilterClause_SelectDeselectWinForms()
        {
            TestGetComboBoxFilterClause_SelectDeselect(new ControlFactoryWin());
        }

        [Test]
        public void TestGetComboBoxFilterClause_SelectDeselectGiz()
        {
            TestGetComboBoxFilterClause_SelectDeselect(new ControlFactoryGizmox());
        }

        public void TestGetComboBoxFilterClause_SelectDeselect(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory filterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            IComboBox comboBox = GetFilterComboBox_2Items(filterControl);
            //---------------Execute Test ----------------------
            comboBox.SelectedIndex = 1;
            comboBox.SelectedIndex = -1;
            string filterClauseString = filterControl.GetFilterClause().GetFilterClauseString();
            //---------------Test Result -----------------------
            IFilterClause nullClause = filterClauseFactory.CreateNullFilterClause();
            Assert.AreEqual(nullClause.GetFilterClauseString(), filterClauseString);
            //---------------Tear Down -------------------------          
        }

        #endregion

        [Test]
        public void TestMultipleFiltersWinForms()
        {
            MultipleFilters(new ControlFactoryWin());
        }

        [Test]
        public void TestMultipleFiltersGiz()
        {
            MultipleFilters(new ControlFactoryGizmox());
        }

        public void MultipleFilters(IControlFactory factory)
        {
            //---------------Set up test pack-------------------
            IFilterClauseFactory filterClauseFactory = new DataViewFilterClauseFactory();
            IFilterControl filterControl = factory.CreateFilterControl();
            ITextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn");
            tb.Text = "testvalue";
            IFilterClause clause =
                filterClauseFactory.CreateStringFilterClause("TestColumn", FilterClauseOperator.OpLike, "testvalue");

            ITextBox tb2 = filterControl.AddStringFilterTextBox("Test2:", "TestColumn2");
            tb2.Text = "testvalue2";
            //---------------Execute Test ----------------------

            string filterClause = filterControl.GetFilterClause().GetFilterClauseString();
            //---------------Test Result -----------------------
            IFilterClause clause2 =
                filterClauseFactory.CreateStringFilterClause("TestColumn2", FilterClauseOperator.OpLike, "testvalue2");

            IFilterClause compositeClause =
                filterClauseFactory.CreateCompositeFilterClause(clause, FilterClauseCompositeOperator.OpAnd, clause2);

            Assert.AreEqual(compositeClause.GetFilterClauseString(),
                            filterClause);
            //---------------Tear Down ------------------------- 
        }


        [Test]
        public void TestAdd_DateRangeFilterComboBoxInclusiveWinForms()
        {
            TestAdd_DateRangeFilterComboBoxInclusive(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxInclusiveGiz()
        {
            TestAdd_DateRangeFilterComboBoxInclusive(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxWinForms()
        {
            TestAdd_DateRangeFilterComboBox(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxGiz()
        {
            TestAdd_DateRangeFilterComboBox(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBox(IControlFactory factory)
        {
            //---------------Set up test pack-------------------

            IFilterControl filterControl = factory.CreateFilterControl();
            //---------------Execute Test ----------------------
            IDateRangeComboBox dr1 = filterControl.AddDateRangeFilterComboBox("test", "test", true, true);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, filterControl.CountOfFilters);
            Assert.IsTrue(filterControl.FilterPanel.Controls.Contains(dr1));
        }


        [Test]
        public void TestAdd_DateRangeFilterComboBoxInclusive(IControlFactory factory)
        {
            //---------------Set up test pack-------------------

            IFilterControl filterControl = factory.CreateFilterControl();
            IFilterClauseFactory filterClauseFactory = new DataViewFilterClauseFactory();
            DateTime testDate = new DateTime(2007, 1, 2, 3, 4, 5, 6);

            //---------------Execute Test ----------------------
            IDateRangeComboBox dr1 = filterControl.AddDateRangeFilterComboBox("test", "test", true, true);
            dr1.UseFixedNowDate = true;
            dr1.FixedNowDate = testDate;
            dr1.SelectedItem = "Today";
            IFilterClause clause1 =
                filterClauseFactory.CreateDateFilterClause("test", FilterClauseOperator.OpGreaterThanOrEqualTo,
                                                           new DateTime(2007, 1, 2, 0, 0, 0));
            IFilterClause clause2 =
                filterClauseFactory.CreateDateFilterClause("test", FilterClauseOperator.OpLessThanOrEqualTo,
                                                           new DateTime(2007, 1, 2, 3, 4, 5));
            IFilterClause compClause =
                filterClauseFactory.CreateCompositeFilterClause(clause1, FilterClauseCompositeOperator.OpAnd, clause2);
            //---------------Test Result -----------------------

            Assert.AreEqual(compClause.GetFilterClauseString(), filterControl.GetFilterClause().GetFilterClauseString());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxExclusiveWinForms()
        {
            TestAdd_DateRangeFilterComboBoxExclusive(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxExclusiveGiz()
        {
            TestAdd_DateRangeFilterComboBoxExclusive(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxExclusive(IControlFactory factory)
        {
            //---------------Set up test pack-------------------

            IFilterControl filterControl = factory.CreateFilterControl();
            IFilterClauseFactory filterClauseFactory = new DataViewFilterClauseFactory();
            DateTime testDate = new DateTime(2007, 1, 2, 3, 4, 5, 6);

            //---------------Execute Test ----------------------
            IDateRangeComboBox dr1 = filterControl.AddDateRangeFilterComboBox("test", "test", false, false);
            dr1.UseFixedNowDate = true;
            dr1.FixedNowDate = testDate;
            dr1.SelectedItem = "Today";
            IFilterClause clause1 =
                filterClauseFactory.CreateDateFilterClause("test", FilterClauseOperator.OpGreaterThan,
                                                           new DateTime(2007, 1, 2, 0, 0, 0));
            IFilterClause clause2 =
                filterClauseFactory.CreateDateFilterClause("test", FilterClauseOperator.OpLessThan,
                                                           new DateTime(2007, 1, 2, 3, 4, 5));
            IFilterClause compClause =
                filterClauseFactory.CreateCompositeFilterClause(clause1, FilterClauseCompositeOperator.OpAnd, clause2);
            //---------------Test Result -----------------------

            Assert.AreEqual(compClause.GetFilterClauseString(), filterControl.GetFilterClause().GetFilterClauseString());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxOverloadWinForms()
        {
            TestAdd_DateRangeFilterComboBoxOverload(new ControlFactoryWin());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxOverloadGiz()
        {
            TestAdd_DateRangeFilterComboBoxOverload(new ControlFactoryGizmox());
        }

        [Test]
        public void TestAdd_DateRangeFilterComboBoxOverload(IControlFactory factory)
        {
            //---------------Set up test pack-------------------

            IFilterControl filterControl = factory.CreateFilterControl();
            List<DateRangeOptions> options = new List<DateRangeOptions>();
            options.Add(DateRangeOptions.Today);
            options.Add(DateRangeOptions.Yesterday);

            //---------------Execute Test ----------------------
            IDateRangeComboBox dateRangeCombo =
                filterControl.AddDateRangeFilterComboBox("test", "test", options, true, false);
            //---------------Test Result -----------------------
            Assert.AreEqual(3, dateRangeCombo.Items.Count);
        }


        private static IComboBox GetFilterComboBox_2Items(IFilterControl filterControl)
        {
            IList options = new ArrayList();
            options.Add("1");
            options.Add("2");
            return filterControl.AddStringFilterComboBox("Test:", "TestColumn", options, true);
        }

//
//        [Test]
//        public void TestAddStringFilterTextBoxTextChanged()
//        {
//            itsIsFilterClauseChanged = false;
//            filterControl.SetAutomaticUpdate(true);
//            filterControl.FilterClauseChanged += FilterClauseChangedHandler;
//            TextBox tb = filterControl.AddStringFilterTextBox("Test:", "TestColumn");
//            Assert.IsTrue(itsIsFilterClauseChanged, "Adding a new control should make the filter clause change");
//            itsIsFilterClauseChanged = false;
//            tb.Text = "change";
//            Assert.IsTrue(itsIsFilterClauseChanged, "Changing the text should make the filter clause change");
//        }
//
//        private void FilterClauseChangedHandler(object sender, FilterControlEventArgs e)
//        {
//            itsIsFilterClauseChanged = true;
//        }
//
//
//        [Test]
//        public void TestAddStringFilterComboBoxTextChanged()
//        {
//            IList options = new ArrayList();
//            options.Add("1");
//            options.Add("2");
//            itsIsFilterClauseChanged = false;
//            filterControl.FilterClauseChanged += FilterClauseChangedHandler;
//            ComboBox cb = filterControl.AddStringFilterComboBox("Test:", "TestColumn", options, true);
//            Assert.IsTrue(itsIsFilterClauseChanged, "Adding a new control should make the filter clause change");
//            itsIsFilterClauseChanged = false;
//            cb.SelectedIndex = 0;
//            Assert.IsTrue(itsIsFilterClauseChanged, "Changing the selected item should make the filter clause change");
//        }
//

//
//        [Test]
//        public void TestAddStringFilterDateTimeEditor()
//        {
//            DateTime testDate = DateTime.Now;
//            filterControl.AddStringFilterDateTimeEditor("test:", "testcolumn", testDate, true);
//            filterControl.AddStringFilterDateTimeEditor("test:", "testcolumn", testDate, false);
//            IFilterClause clause1 =
//                itsFilterClauseFactory.CreateStringFilterClause("testcolumn", FilterClauseOperator.OpGreaterThanOrEqualTo, testDate.ToString("yyyy/MM/dd"));
//            IFilterClause clause2 =
//                itsFilterClauseFactory.CreateStringFilterClause("testcolumn", FilterClauseOperator.OpLessThanOrEqualTo, testDate.ToString("yyyy/MM/dd"));
//            IFilterClause compClause =
//                itsFilterClauseFactory.CreateCompositeFilterClause(clause1, FilterClauseCompositeOperator.OpAnd, clause2);
//            Assert.AreEqual(compClause.GetFilterClauseString(), filterControl.GetFilterClause().GetFilterClauseString());
//        }
//
//        [Test]
//        public void TestAddDateFilterDateTimePicker()
//        {
//            DateTime testDate = DateTime.Now;
//            filterControl.AddDateFilterDateTimePicker("test:", "testcolumn", testDate, FilterClauseOperator.OpGreaterThan, true);
//            filterControl.AddDateFilterDateTimePicker("test:", "testcolumn", testDate, FilterClauseOperator.OpEquals, false);
//            IFilterClause clause1 = itsFilterClauseFactory.CreateDateFilterClause("testcolumn", FilterClauseOperator.OpGreaterThan, new DateTime(testDate.Year, testDate.Month, testDate.Day));
//            IFilterClause clause2 = itsFilterClauseFactory.CreateDateFilterClause("testcolumn", FilterClauseOperator.OpEquals, testDate);
//            IFilterClause compClause =
//                itsFilterClauseFactory.CreateCompositeFilterClause(clause1, FilterClauseCompositeOperator.OpAnd, clause2);
//            Assert.AreEqual(compClause.GetFilterClauseString(), filterControl.GetFilterClause().GetFilterClauseString());
//        }
//
    }
}