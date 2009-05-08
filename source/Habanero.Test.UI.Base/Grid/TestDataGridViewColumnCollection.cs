using Gizmox.WebGUI.Forms;
using Habanero.UI.Base;
using Habanero.UI.VWG;
using Habanero.UI.Win;
using NUnit.Framework;
using Rhino.Mocks;

namespace Habanero.Test.UI.Base.Grid
{
    [TestFixture]
    public class TestDataGridViewColumnCollectionWin
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
        public void Test_Contains_Calls_UnderlyingDataGridViewContainsMethod()
        {
            //---------------Set up test pack-------------------
            System.Windows.Forms.DataGridViewColumnCollection dataGridViewColumnCollection =
                MockRepository.GenerateMock<System.Windows.Forms.DataGridViewColumnCollection>();
            IDataGridViewColumnCollection columnCollection = new DataGridViewWin.DataGridViewColumnCollectionWin
                (dataGridViewColumnCollection);
            string columnName = TestUtil.GetRandomString();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            columnCollection.Contains(columnName);
            //---------------Test Result -----------------------
            dataGridViewColumnCollection.AssertWasCalled(collection => collection.Contains(columnName));
        }

        [Test]
        public void Test_Contains_ReturnsValueOf_UnderlyingDataGridViewContainsMethod_WhenFalse()
        {
            //---------------Set up test pack-------------------
            string columnName = TestUtil.GetRandomString();
            const bool expectedReturn = false;
            System.Windows.Forms.DataGridViewColumnCollection mock = MockRepository.GenerateMock<System.Windows.Forms.DataGridViewColumnCollection>();
            IDataGridViewColumnCollection columnCollection = new DataGridViewWin.DataGridViewColumnCollectionWin(mock);
            mock.Stub(t => mock.Contains(columnName)).Return(expectedReturn);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            bool returnValue = columnCollection.Contains(columnName);
            //---------------Test Result -----------------------
            Assert.AreEqual(expectedReturn, returnValue);
        }

        [Test]
        public void Test_Contains_ReturnsValueOf_UnderlyingDataGridViewContainsMethod_WhenTrue()
        {
            //---------------Set up test pack-------------------
            string columnName = TestUtil.GetRandomString();
            const bool expectedReturn = true;
            System.Windows.Forms.DataGridViewColumnCollection mock = MockRepository.GenerateMock<System.Windows.Forms.DataGridViewColumnCollection>();
            IDataGridViewColumnCollection columnCollection = new DataGridViewWin.DataGridViewColumnCollectionWin(mock);
            mock.Stub(t => mock.Contains(columnName)).Return(expectedReturn);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            bool returnValue = columnCollection.Contains(columnName);
            //---------------Test Result -----------------------
            Assert.AreEqual(expectedReturn, returnValue);
        }
    }

    [TestFixture]
    public class TestDataGridViewColumnCollectionVWG
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
        private static IDataGridViewColumnCollection GetColumnCollectionWithMock(out DataGridViewColumnCollection mock)
        {
            DataGridView dataGridView = new DataGridView();
            mock = MockRepository.GenerateMock<Gizmox.WebGUI.Forms.DataGridViewColumnCollection>(dataGridView);
            return new DataGridViewVWG.DataGridViewColumnCollectionVWG(mock);
        }

        [Test]
        public void Test_Contains_Calls_UnderlyingDataGridViewContainsMethod()
        {
            //---------------Set up test pack-------------------
            DataGridViewColumnCollection mock;
            IDataGridViewColumnCollection columnCollection = GetColumnCollectionWithMock(out mock);
            string columnName = TestUtil.GetRandomString();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            columnCollection.Contains(columnName);
            //---------------Test Result -----------------------
            mock.AssertWasCalled(collection => collection.Contains(columnName));
        }

        [Test]
        public void Test_Contains_ReturnsValueOf_UnderlyingDataGridViewContainsMethod_WhenFalse()
        {
            //---------------Set up test pack-------------------
            string columnName = TestUtil.GetRandomString();
            const bool expectedReturn = false;
            DataGridViewColumnCollection mock;
            IDataGridViewColumnCollection columnCollection = GetColumnCollectionWithMock(out mock);
            mock.Stub(t => mock.Contains(columnName)).Return(expectedReturn);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            bool returnValue = columnCollection.Contains(columnName);
            //---------------Test Result -----------------------
            Assert.AreEqual(expectedReturn, returnValue);
        }

        [Test]
        public void Test_Contains_ReturnsValueOf_UnderlyingDataGridViewContainsMethod_WhenTrue()
        {
            //---------------Set up test pack-------------------
            string columnName = TestUtil.GetRandomString();
            const bool expectedReturn = true;
            DataGridViewColumnCollection mock;
            IDataGridViewColumnCollection columnCollection = GetColumnCollectionWithMock(out mock);
            mock.Stub(t => mock.Contains(columnName)).Return(expectedReturn);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            bool returnValue = columnCollection.Contains(columnName);
            //---------------Test Result -----------------------
            Assert.AreEqual(expectedReturn, returnValue);
        }
    }
}