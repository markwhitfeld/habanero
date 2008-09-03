using System.Collections.Generic;
using Habanero.Base;
using Habanero.BO;
using Habanero.BO.ClassDefinition;
using Habanero.BO.Loaders;
using Habanero.BO.ObjectManager;
using NUnit.Framework;

namespace Habanero.Test.BO.Security
{
    [TestFixture]
    public class TestBusinessObjectAuthorisation //:TestBase
    {
        [SetUp]
        public void SetupTest()
        {
            //Runs every time that any testmethod is executed
            //base.SetupTest();
            ClassDef.ClassDefs.Clear();
            BORegistry.DataAccessor = new DataAccessorInMemory();
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
            //base.TearDownTest();
        }
        #region TestDelete
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowDelete()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanDelete_True();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanDelete));

            //---------------Execute Test ----------------------
            string message;
            bool isDeletable = myBoStub.IsDeletable(out message);

            //---------------Test Result -----------------------
            Assert.IsTrue(isDeletable);
            Assert.AreEqual("", message);
        }
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowDelete_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanDelete_False();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanDelete));
            //---------------Execute Test ----------------------
            string message;
            bool isDeletable = myBoStub.IsDeletable(out message);

            //---------------Test Result -----------------------
            Assert.IsFalse(isDeletable);
            StringAssert.Contains("The logged on user", message);
            StringAssert.Contains("is not authorised to delete ", message);
        }

        [Test]
        public void Test_DeleteBO_AllowDelete_True()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanDelete_True();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanDelete));
            Assert.IsFalse(myBoStub.Status.IsNew);

            //---------------Execute Test ----------------------
            myBoStub.Delete();
            myBoStub.Save();

            //---------------Test Result -----------------------
            Assert.IsTrue(myBoStub.Status.IsDeleted);
            Assert.IsTrue(myBoStub.Status.IsNew);

        }

        [Test]
        public void Test_DeleteBO_Fail_AllowDelete_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanDelete_False();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanDelete));

            //---------------Execute Test ----------------------
            try
            {
                myBoStub.Delete();
                Assert.Fail("expected Err");
            }
                //---------------Test Result -----------------------
            catch (BusObjDeleteException ex)
            {
                StringAssert.Contains("The logged on user", ex.Message);
                StringAssert.Contains("is not authorised to delete ", ex.Message);
            }
        }

        [Test]
        public void Test_SaveDeletedBO_Fail_AllowDelete_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanDelete_False();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();
            myBoStub.SetStatus(BOStatus.Statuses.isDeleted, true);

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanDelete));
            Assert.IsTrue(myBoStub.Status.IsDeleted);
            Assert.IsFalse(myBoStub.Status.IsNew);
            //---------------Execute Test ----------------------
            try
            {
                myBoStub.Save();
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (BusObjPersistException ex)
            {
                StringAssert.Contains("The logged on user", ex.Message);
                StringAssert.Contains("is not authorised to delete ", ex.Message);
            }
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanDelete_False()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            return authorisationStub;
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanDelete_True()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanDelete);
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            return authorisationStub;
        }
        #endregion //TestDelete

        #region TestCreate
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowCreate()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanCreate));

            //---------------Execute Test ----------------------
            string message;
            bool isCreatable = myBoStub.IsCreatable(out message);

            //---------------Test Result -----------------------
            Assert.IsTrue(isCreatable);
            Assert.AreEqual("", message);
        }
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowCreate_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanCreate));
            //---------------Execute Test ----------------------
            string message;
            bool isCreatable = myBoStub.IsCreatable(out message);

            //---------------Test Result -----------------------
            Assert.IsFalse(isCreatable);
            StringAssert.Contains("The logged on user", message);
            StringAssert.Contains("is not authorised to create ", message);
        }

        [Test]
        public void Test_SaveNewBO_AllowCreate_True()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanCreate_True();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanCreate));

            //---------------Execute Test ----------------------
            myBoStub.Save();
            //---------------Test Result -----------------------
        }

        [Test]
        public void Test_SaveNewBO_Fail_AllowCreate_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanCreate_False();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanCreate));

            //---------------Execute Test ----------------------
            try
            {
                myBoStub.Save();
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (BusObjPersistException ex)
            {
                StringAssert.Contains("The logged on user", ex.Message);
                StringAssert.Contains("is not authorised to create ", ex.Message);
            }
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanCreate_True()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            return authorisationStub;
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanCreate_False()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            return authorisationStub;
        }
        #endregion /TestDelete

        #region TestUpdate
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowUpdate()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanUpdate_True();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanUpdate));

            //---------------Execute Test ----------------------
            string message;
            bool isEditable = myBoStub.IsEditable(out message);

            //---------------Test Result -----------------------
            Assert.IsTrue(isEditable);
            Assert.AreEqual("", message);
        }

        [Test]
        public void Test_BusinessObjectAuthorisation_AllowUpdate_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanUpdate_False();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanUpdate));
            //---------------Execute Test ----------------------
            string message;
            bool isEditable = myBoStub.IsEditable(out message);

            //---------------Test Result -----------------------
            Assert.IsFalse(isEditable);
            StringAssert.Contains("The logged on user", message);
            StringAssert.Contains("is not authorised to update ", message);
        }

        [Test]
        public void Test_SetValue_AllowUpdate_True()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanUpdate_True();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanUpdate));
            Assert.IsFalse(myBoStub.Status.IsNew);
            Assert.IsFalse(myBoStub.Status.IsDirty);

            //---------------Execute Test ----------------------
            const string newPropValue = "1112";
            myBoStub.SetPropertyValue("Prop1", newPropValue);

            //---------------Test Result -----------------------
            Assert.IsTrue(myBoStub.Status.IsDirty);
            Assert.AreEqual(newPropValue, myBoStub.GetPropertyValue("Prop1"));
        }

        [Test]
        public void Test_SetValue_Fail_AllowUpdate_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanUpdate_False();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanUpdate));
            Assert.IsFalse(myBoStub.Status.IsNew);
            Assert.IsFalse(myBoStub.Status.IsDirty);

            //---------------Execute Test ----------------------
            const string newPropValue = "1112";
            try
            {
                myBoStub.SetPropertyValue("Prop1", newPropValue);
                Assert.Fail("expected Err");
            }
                //---------------Test Result -----------------------
            catch (BusObjEditableException ex)
            {
                StringAssert.Contains("You cannot Edit ", ex.Message);
                StringAssert.Contains("as the IsEditable is set to false for the object", ex.Message);
            }
        }

        [Test]
        public void Test_SaveExistingBO_AllowUpdate_True()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanUpdate_True();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();
            myBoStub.SetPropertyValue("Prop1", "1112");

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanUpdate));
            Assert.IsFalse(myBoStub.Status.IsNew);
            Assert.IsTrue(myBoStub.Status.IsDirty);

            //---------------Execute Test ----------------------
            myBoStub.Save();

            //---------------Test Result -----------------------
            Assert.IsFalse(myBoStub.Status.IsDirty);
        }

        [Test]
        public void Test_SaveExistingBO_Fail_AllowUpdate_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanUpdate_False();

            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();
            IBOProp prop1Prop = myBoStub.Props["Prop1"];
            prop1Prop.Value = "1112";
            myBoStub.SetStatus(BOStatus.Statuses.isDirty, true);

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanUpdate));
            Assert.IsTrue(prop1Prop.IsDirty);
            Assert.IsTrue(myBoStub.Status.IsDirty);

            //---------------Execute Test ----------------------
            try
            {
                myBoStub.Save();
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (BusObjPersistException ex)
            {
                StringAssert.Contains("The logged on user", ex.Message);
                StringAssert.Contains("is not authorised to update ", ex.Message);
            }
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanUpdate_True()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanUpdate);
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanRead);
            return authorisationStub;
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanUpdate_False()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanRead);
            return authorisationStub;
        }
        #endregion /TestDelete

        #region TestRead
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowRead()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanRead);
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanRead));

            //---------------Execute Test ----------------------
            string message;
            bool isReadable = myBoStub.IsReadable(out message);

            //---------------Test Result -----------------------
            Assert.IsTrue(isReadable);
            Assert.AreEqual("", message);
        }
        [Test]
        public void Test_BusinessObjectAuthorisation_AllowRead_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanRead));

            //---------------Execute Test ----------------------
            string message;
            bool isReadable = myBoStub.IsReadable(out message);

            //---------------Test Result -----------------------
            Assert.IsFalse(isReadable);
            StringAssert.Contains("The logged on user", message);
            StringAssert.Contains("is not authorised to read ", message);
        }

        [Test]
        public void Test_LoadExistingBO_AllowRead_True()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanCreate_True();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();

            authorisationStub = GetAuthorisationStub_CanRead_True();
            myBoStub.SetAuthorisation(authorisationStub);
            IPrimaryKey id = myBoStub.ID;
            BusinessObjectManager.Instance.ClearLoadedObjects();
            
            //---------------Assert Precondition----------------
            Assert.IsTrue(authorisationStub.IsAuthorised(BusinessObjectActions.CanRead));
            Assert.IsFalse(myBoStub.Status.IsNew);

            //---------------Execute Test ----------------------
            myBoStub = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<MyBoAuthenticationStub>(id);
            object value = myBoStub.GetPropertyValue("Prop1");
            //---------------Test Result -----------------------
            Assert.IsNull(value);
            Assert.IsFalse(myBoStub.Status.IsDirty);
        }
        [Test]
        public void Test_LoadExistingBO_Fail_AllowRead_False()
        {
            //---------------Set up test pack-------------------
            MyBoAuthenticationStub.LoadDefaultClassDef();
            IBusinessObjectAuthorisation authorisationStub = GetAuthorisationStub_CanCreate_True();
            MyBoAuthenticationStub myBoStub = new MyBoAuthenticationStub();
            myBoStub.SetAuthorisation(authorisationStub);
            myBoStub.Save();

            authorisationStub = GetAuthorisationStub_CanRead_False();
            myBoStub.SetAuthorisation(authorisationStub);
            IPrimaryKey id = myBoStub.ID;
            BusinessObjectManager.Instance.ClearLoadedObjects();

            //---------------Assert Precondition----------------
            Assert.IsFalse(authorisationStub.IsAuthorised(BusinessObjectActions.CanRead));
            Assert.IsFalse(myBoStub.Status.IsNew);

            //---------------Execute Test ----------------------
            myBoStub = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<MyBoAuthenticationStub>(id);
            try
            {
                myBoStub.GetPropertyValue("Prop1");
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (BusObjReadException ex)
            {
                StringAssert.Contains("The logged on user", ex.Message);
                StringAssert.Contains("is not authorised to read ", ex.Message);
            }
        }

        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanRead_True()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanRead);
            authorisationStub.AddAuthorisedRole("A Role", BusinessObjectActions.CanCreate);
            return authorisationStub;
        }
        private static IBusinessObjectAuthorisation GetAuthorisationStub_CanRead_False()
        {
            IBusinessObjectAuthorisation authorisationStub = new AuthorisationStub();
            return authorisationStub;
        }
        #endregion /TestRead
    }

    internal class MyBoAuthenticationStub: BusinessObject
    {
        public static ClassDef LoadDefaultClassDef()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""MyBoAuthenticationStub"" assembly=""Habanero.Test.BO"">
					<property  name=""MyBoAuthenticationStubID"" type=""Guid""/>
                    <property  name=""Prop1""/>
					<primaryKey>
						<prop name=""MyBoAuthenticationStubID"" />
					</primaryKey>
					<ui>
					</ui>                   
				</class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public void SetAuthorisation(IBusinessObjectAuthorisation authorisationStub)
        {
            SetAuthorisationRules(authorisationStub);
        }
    }

    internal class AuthorisationStub : IBusinessObjectAuthorisation
    {
        private readonly List<BusinessObjectActions> actionsAllowed = new List<BusinessObjectActions>();

        public void AddAuthorisedRole(string role, BusinessObjectActions actionToPerform)
        {
            if (actionsAllowed.Contains(actionToPerform)) return;
            
            actionsAllowed.Add(actionToPerform);
        }

        public bool IsAuthorised(BusinessObjectActions actionToPerform)
        {
            return (actionsAllowed.Contains(actionToPerform));
        }
    }
}