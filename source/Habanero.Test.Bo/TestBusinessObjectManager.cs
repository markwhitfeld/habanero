//---------------------------------------------------------------------------------
// Copyright (C) 2008 Chillisoft Solutions
// 
// This file is part of the Habanero framework.
// 
//     Habanero is a free framework: you can redistribute it and/or modify
//     it under the terms of the GNU Lesser General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     The Habanero framework is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU Lesser General Public License for more details.
// 
//     You should have received a copy of the GNU Lesser General Public License
//     along with the Habanero framework.  If not, see <http://www.gnu.org/licenses/>.
//---------------------------------------------------------------------------------
using System;
using Habanero.Base;
using Habanero.Base.Exceptions;
using Habanero.BO;
using Habanero.BO.ClassDefinition;
using NUnit.Framework;

namespace Habanero.Test.BO
{
    [TestFixture]
    public class TestBusinessObjectManager //:TestBase
    {
        #region Setup/Teardown

        [SetUp]
        public void SetupTest()
        {
            //Runs every time that any testmethod is executed
            //base.SetupTest();
            ClassDef.ClassDefs.Clear();
            new Address();
            BusinessObjectManager.Instance.ClearLoadedObjects();
            
        }

        [TearDown]
        public void TearDownTest()
        {
            //runs every time any testmethod is complete
            //base.TearDownTest();
        }

        #endregion

        protected static void SetupDataAccessor()
        {
            BORegistry.DataAccessor = new DataAccessorDB();
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            //Code that is executed before any test is run in this class. If multiple tests
            // are executed then it will still only be called once.
            SetupDataAccessor();
            new TestUsingDatabase().SetupDBConnection();
        }

        [Test]
        public void Test_CreateObjectManager()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            //---------------Test Result -----------------------
            Assert.AreEqual(0, boMan.Count);
//            Assert.IsInstanceOfType(typeof(BusinessObjectManager), boMan);
        }

        [Test]
        public void Test_AddedToObjectManager()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();

            ContactPersonTestBO cp = new ContactPersonTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);

            //---------------Execute Test ----------------------
            cp.Surname = TestUtil.CreateRandomString();
            boMan.Add(cp);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);

            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.IsTrue(boMan.Contains(cp.ID.AsString_CurrentValue()));
            Assert.AreSame(cp, boMan[cp.ID.AsString_CurrentValue()]);
            Assert.AreSame(cp, boMan[cp.ID]);
        }

        [Test]
        public void Test_ClearLoadedObjects()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            ContactPersonTestBO cp = new ContactPersonTestBO();
            cp.Surname = TestUtil.CreateRandomString();
            boMan.Add(cp);

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));

            //---------------Execute Test ----------------------
            boMan.ClearLoadedObjects();

            //---------------Test Result -----------------------
            Assert.AreEqual(0, boMan.Count);
            Assert.IsFalse(boMan.Contains(cp));
        }


        //[Test]
        //public void Test_NewObjectNotAddedToObjectManager()
        //{
        //    //---------------Set up test pack-------------------
        //    ContactPersonTestBO.LoadDefaultClassDef();
        //    BusinessObjectManager boMan = BusinessObjectManager.Instance;

        //    //---------------Assert Precondition----------------
        //    Assert.AreEqual(0, boMan.Count);

        //    //---------------Execute Test ----------------------
        //    new ContactPersonTestBO();

        //    //---------------Test Result -----------------------
        //    Assert.AreEqual(0, boMan.Count);
        //}

        [Test]
        public void Test_RemoveFromObjectManager()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
                        BusinessObjectManager boMan = BusinessObjectManager.Instance;
            ContactPersonTestBO cp = new ContactPersonTestBO();
            cp.Surname = TestUtil.CreateRandomString();
            boMan.Add(cp);

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));

            //---------------Execute Test ----------------------
            boMan.Remove(cp);

            //---------------Test Result -----------------------
            Assert.AreEqual(0, boMan.Count);
            Assert.IsFalse(boMan.Contains(cp));
        }

        [Test]
        public void Test_ObjectManagerIndexers()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = new ContactPersonTestBO();
            boMan.Add(cp);

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);

            //---------------Execute Test ----------------------
            IBusinessObject boFromObjMan_StringID = boMan[cp.ID.AsString_CurrentValue()];
            
            IBusinessObject boFromMan_ObjectID = boMan[cp.ID];
           
            //---------------Test Result -----------------------
            Assert.AreSame(cp, boFromObjMan_StringID);
            Assert.AreSame(cp, boFromMan_ObjectID);

        }

        #pragma warning disable 168
        [Test]
        public void Test_ObjManStringIndexer_ObjectDoesNotExistInObjectMan()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            //ContactPersonTestBO cp = new ContactPersonTestBO();
            Guid guid = Guid.NewGuid();
            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            try
            {

                //IBusinessObject bo = boMan[cp.ID.AsString_CurrentValue()];
                IBusinessObject bo = boMan[guid.ToString()];
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (HabaneroDeveloperException ex)
            {
                StringAssert.Contains("There is an application error please contact your system administrator", ex.Message);
                StringAssert.Contains("There was an attempt to retrieve the object identified by", ex.DeveloperMessage);
            }
        }
        [Test]
        public void Test_ObjManObjectIndexer_ObjectDoesNotExistInObjectMan()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            //ContactPersonTestBO cp = new ContactPersonTestBO();
            BOPrimaryKey boPrimaryKey = new BOPrimaryKey(new PrimaryKeyDef());
            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            try
            {

                //IBusinessObject bo = boMan[cp.ID];
                IBusinessObject bo = boMan[boPrimaryKey];
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (HabaneroDeveloperException ex)
            {
                StringAssert.Contains("There is an application error please contact your system administrator", ex.Message);
                StringAssert.Contains("There was an attempt to retrieve the object identified by", ex.DeveloperMessage);
            }
        }
        #pragma warning restore 168

        [Test]
        public void Test_RemoveObjectFromObjectManagerTwice()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.AreSame(cp, boMan[cp.ID]);

            //---------------Execute Test ----------------------
            boMan.Remove(cp);
            boMan.Remove(cp);

            //---------------Test Result -----------------------
            Assert.AreEqual(0, boMan.Count);
        }
        [Test]
        public void Test_SavedObjectAddedToObjectManager()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = new ContactPersonTestBO();
            cp.Surname = TestUtil.CreateRandomString();

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));

            //---------------Execute Test ----------------------
            cp.Save();

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.IsTrue(boMan.Contains(cp.ID.AsString_CurrentValue()));
            Assert.AreSame(cp, boMan[cp.ID.AsString_CurrentValue()]);
            Assert.AreSame(cp, boMan[cp.ID]);
        }

        //Test Add TwiceNoProblem if object is same
        [Test]
        public void Test_AddSameObjectTwiceShouldNotCauseError()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();

            ContactPersonTestBO cp = new ContactPersonTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            cp.Surname = TestUtil.CreateRandomString();
            boMan.Add(cp);

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);

            //---------------Execute Test ----------------------
            boMan.Add(cp);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);

            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.IsTrue(boMan.Contains(cp.ID.AsString_CurrentValue()));
            Assert.AreSame(cp, boMan[cp.ID.AsString_CurrentValue()]);
            Assert.AreSame(cp, boMan[cp.ID]);
        }

        //Test add second copy of same object throw error.
        //Test Add TwiceNoProblem if object is same.
        [Test]
        public void Test_Add_CopyOfSameObjectTwiceShould_ThrowError()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();

            ContactPersonTestBO cp = new ContactPersonTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);

            //---------------Execute Test ----------------------
            ContactPersonTestBO cp2 = new ContactPersonTestBO();
            
            try
            {
                cp2.ContactPersonID = cp.ContactPersonID;
                Assert.Fail("expected Err");
            }
            //---------------Test Result -----------------------
            catch (HabaneroDeveloperException ex)
            {
                StringAssert.Contains("There was a serious developer exception. Two copies of the business object", ex.Message);
                StringAssert.Contains(" were added to the object manager", ex.Message);
            }
        }
        //Test save twice
        [Test]
        public void Test_SavedObject_Twice_AddedToObjectManager_Once()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = new ContactPersonTestBO();
            cp.Surname = TestUtil.CreateRandomString();
            cp.Save();
            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));

            //---------------Execute Test ----------------------
            
            cp.Surname = TestUtil.CreateRandomString();
            cp.Save();

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.IsTrue(boMan.Contains(cp.ID.AsString_CurrentValue()));
            Assert.AreSame(cp, boMan[cp.ID.AsString_CurrentValue()]);
            Assert.AreSame(cp, boMan[cp.ID]);
        }

        [Test]
        public void Test_ContainsBusinessObjectReturnsFalseIfReferenceNotEquals()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO originalContactPerson = new ContactPersonTestBO();
            ContactPersonTestBO copyContactPerson = new ContactPersonTestBO();
            copyContactPerson.ContactPersonID = originalContactPerson.ContactPersonID;
            //BusinessObjectManager.Instance.Add(copyContactPerson);

            //---------------Assert Precondition----------------
            Assert.AreEqual(2, boMan.Count);
            Assert.IsTrue(boMan.Contains(copyContactPerson));

            //---------------Execute Test ----------------------
            bool containsOrigContactPerson = boMan.Contains(originalContactPerson);

            //---------------Test Result -----------------------
            Assert.IsFalse(containsOrigContactPerson);
        }

        /// <summary>
        /// <see cref="Test_SaveDuplicateObject_DoesNotAddItselfToObjectManager"/>
        /// </summary>
        [Test]
        public void Test_RemoveSecondInstanceOfSameLoadedObjectDoesNotRemoveIt()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO originalContactPerson = new ContactPersonTestBO();
            ContactPersonTestBO copyContactPerson = new ContactPersonTestBO();
            copyContactPerson.ContactPersonID = originalContactPerson.ContactPersonID;
            BusinessObjectManager.Instance.Add(copyContactPerson);

            //---------------Assert Precondition----------------
            Assert.AreNotSame(originalContactPerson, copyContactPerson);
            Assert.AreEqual(originalContactPerson.ID.AsString_CurrentValue(), copyContactPerson.ID.AsString_CurrentValue());
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(copyContactPerson));
            Assert.IsFalse(boMan.Contains(originalContactPerson));

            //---------------Execute Test ----------------------
            BusinessObjectManager.Instance.Remove(originalContactPerson);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(copyContactPerson));
            Assert.IsFalse(boMan.Contains(originalContactPerson));
        }

        [Test]
        public void Test_SaveDuplicateObject_DoesNotAddItselfToObjectManager()
        {
            //This scenario is unlikely to ever happen in normal use but is frequently hit during testing.
            //An object that has a reference to it is removed from the object manager (usually via ClearLoadedObjects).
            // A second instance of the same object is now loaded. This new instance is therefore added to the object manager.
            // The first object is saved. This must not remove the second instance of the object from the object manager and insert a itself.
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            ContactPersonTestBO originalContactPerson = new ContactPersonTestBO();
            originalContactPerson.Surname = "FirstSurname";
            originalContactPerson.Save();
            IPrimaryKey origCPID = originalContactPerson.ID;
            BusinessObjectManager.Instance.ClearLoadedObjects();

            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);
            Assert.IsFalse(boMan.Contains(originalContactPerson));

            //---------------Execute Test Step 1----------------------
            ContactPersonTestBO myContact2 =
                BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(origCPID);

            //---------------Test Result Step 1-----------------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(myContact2));


            //---------------Execute Test Step 2----------------------
            originalContactPerson.Surname = TestUtil.CreateRandomString();
            originalContactPerson.Save();

            //---------------Test Result Step 1-----------------------
            Assert.AreNotSame(originalContactPerson, myContact2);
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(myContact2));
            Assert.IsFalse(boMan.Contains(originalContactPerson));
        }

        //Delete object must remove it from object man.
        [Test]
        public void Test_DeleteObject_RemovesFromObjectMan()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.AreSame(cp, boMan[cp.ID]);

            //---------------Execute Test ----------------------
            cp.Delete();
            cp.Save();

            //---------------Test Result -----------------------
            Assert.AreEqual(0, boMan.Count);
        }

        //Test edit primary key and save.
        [Test]
        public void Test_SaveForCompositeKey_UpdatedObjectMan()
        {
            //---------------Set up test pack-------------------
            ContactPersonCompositeKey.LoadClassDefs();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            boMan.ClearLoadedObjects();

            ContactPersonCompositeKey cp = new ContactPersonCompositeKey();
            cp.PK1Prop1 = TestUtil.CreateRandomString();
            cp.PK1Prop2 = TestUtil.CreateRandomString();
            
            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);

            //---------------Execute Test ----------------------
            cp.Save();

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.IsTrue(boMan.Contains(cp.ID.AsString_CurrentValue()));
            Assert.AreSame(cp, boMan[cp.ID.AsString_CurrentValue()]);
            Assert.AreSame(cp, boMan[cp.ID]);

        }
        [Test]
        public void Test_ChangePrimaryKeyForCompositeKey_UpdatedObjectMan()
        {
            //---------------Set up test pack-------------------
            ContactPersonCompositeKey.LoadClassDefs();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonCompositeKey cp = new ContactPersonCompositeKey();
            cp.PK1Prop1 = TestUtil.CreateRandomString();
            cp.PK1Prop2 = TestUtil.CreateRandomString();
            cp.Save();

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.AreSame(cp, boMan[cp.ID]);

            //---------------Execute Test ----------------------
            cp.PK1Prop1 = TestUtil.CreateRandomString();
            cp.PK1Prop2 = TestUtil.CreateRandomString();
            cp.Save();

            //---------------Test Result -----------------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));
            Assert.IsTrue(boMan.Contains(cp.ID.AsString_CurrentValue()));
            Assert.AreSame(cp, boMan[cp.ID.AsString_CurrentValue()]);
            Assert.AreSame(cp, boMan[cp.ID]);
        }
        // ReSharper disable RedundantAssignment
        [Test]
        public void Test_ObjectDestructor_RemovesFromObjectManager()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = new ContactPersonTestBO();
            boMan.Add(cp);

            //---------------Assert Precondition----------------
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(cp));
            Assert.IsTrue(boMan.Contains(cp.ID));

            //---------------Execute Test ----------------------
            cp = null;
            TestUtil.WaitForGC();

            //---------------Test Result -----------------------
            Assert.AreEqual(0, boMan.Count);
        }
        // ReSharper restore RedundantAssignment

        // ReSharper disable RedundantAssignment
        //Test load objects load them into the boMan
        [Test]
        public void Test_LoadObject_UpdateObjectMan()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();
            IPrimaryKey id = cp.ID;

            cp = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();

            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            ContactPersonTestBO contactPersonTestBO = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(id);
           
            //---------------Test Result -----------------------
            Assert.IsNotNull(contactPersonTestBO);
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(contactPersonTestBO));
            Assert.IsTrue(boMan.Contains(contactPersonTestBO.ID));
            Assert.IsTrue(boMan.Contains(contactPersonTestBO.ID.AsString_CurrentValue()));
            Assert.AreSame(contactPersonTestBO, boMan[contactPersonTestBO.ID.AsString_CurrentValue()]);
            Assert.AreSame(contactPersonTestBO, boMan[contactPersonTestBO.ID]);
        }
        // ReSharper restore RedundantAssignment

        // ReSharper disable RedundantAssignment
        //Test load objects load them into the boMan
        [Test]
        public void Test_LoadObject_UpdateObjectMan_NonGenericLoad()
        {
            //---------------Set up test pack-------------------
            ClassDef classDef = ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();
            IPrimaryKey id = cp.ID;

            cp = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();

            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            ContactPersonTestBO contactPersonTestBO = (ContactPersonTestBO) BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject(classDef ,id);

            //---------------Test Result -----------------------
            Assert.IsNotNull(contactPersonTestBO);
            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(contactPersonTestBO));

            Assert.IsTrue(boMan.Contains(id));
            Assert.IsTrue(boMan.Contains(id.AsString_CurrentValue()));
            Assert.AreSame(contactPersonTestBO, boMan[id.AsString_CurrentValue()]);
            Assert.AreSame(contactPersonTestBO, boMan[id]);
        }
        // ReSharper restore RedundantAssignment

        // ReSharper disable RedundantAssignment
        //Test load objects via colleciton loads into boMan.
        [Test]
        public void Test_LoadObject_ViaCollection_UpdatedObjectMan_NonGeneric()
        {
            //---------------Set up test pack-------------------
            ClassDef classDef = ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();
            Guid contactPersonId = cp.ContactPersonID;
            IPrimaryKey id = cp.ID;
            cp = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();
            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            Criteria criteria = new Criteria("ContactPersonID", Criteria.ComparisonOp.Equals, contactPersonId);
            IBusinessObjectCollection colContactPeople = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObjectCollection(classDef, criteria);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, colContactPeople.Count);
            IBusinessObject loadedCP = colContactPeople[0];
            Assert.IsNotNull(loadedCP);

            Assert.AreNotSame(cp, loadedCP);

            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(loadedCP));
            Assert.IsTrue(boMan.Contains(id));
            Assert.IsTrue(boMan.Contains(id.AsString_CurrentValue()));
            Assert.AreSame(loadedCP, boMan[id]);
            Assert.AreSame(loadedCP, boMan[id.AsString_CurrentValue()]);
        }
        // ReSharper restore RedundantAssignment

        //Test load objects via stronglytypedcollection loads into boMan.
        // ReSharper disable RedundantAssignment
        //Test load objects via colleciton loads into boMan.
        [Test]
        public void Test_LoadObject_ViaCollection_UpdatedObjectMan_Generic()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadDefaultClassDef();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();
            Guid contactPersonId = cp.ContactPersonID;
            IPrimaryKey id = cp.ID;
            cp = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();
            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            Criteria criteria = new Criteria("ContactPersonID", Criteria.ComparisonOp.Equals, contactPersonId);
            IBusinessObjectCollection colContactPeople =
                    BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObjectCollection<ContactPersonTestBO>(criteria);

            //---------------Test Result -----------------------
            Assert.AreEqual(1, colContactPeople.Count);
            IBusinessObject loadedCP = colContactPeople[0];
            Assert.IsNotNull(loadedCP);

            Assert.AreNotSame(cp, loadedCP);

            Assert.AreEqual(1, boMan.Count);
            Assert.IsTrue(boMan.Contains(loadedCP));
            Assert.IsTrue(boMan.Contains(id));
            Assert.IsTrue(boMan.Contains(id.AsString_CurrentValue()));
            Assert.AreSame(loadedCP, boMan[id]);
            Assert.AreSame(loadedCP, boMan[id.AsString_CurrentValue()]);
        }
        // ReSharper restore RedundantAssignment

        // ReSharper disable RedundantAssignment
        //Test load via multiple relationship loads into boMan.
        [Test]
        public void Test_LoadObject_MulitpleRelationship_UpdatedObjectMan_Generic()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadClassDefWithAddressTestBOsRelationship();
            new AddressTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();
            AddressTestBO address = new AddressTestBO();
            address.ContactPersonID = cp.ContactPersonID;
            address.Save();

            IPrimaryKey contactPersonID = cp.ID;
            IPrimaryKey addresssID = address.ID;
            cp = null;
            address = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();

            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            ContactPersonTestBO loadedCP = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(contactPersonID);
            RelatedBusinessObjectCollection<AddressTestBO> addresses = loadedCP.AddressTestBOs;
            
            //---------------Test Result -----------------------
            Assert.AreEqual(1, addresses.Count);
            Assert.AreEqual(2, boMan.Count);

            Assert.IsTrue(boMan.Contains(loadedCP));
            Assert.AreSame(loadedCP, boMan[contactPersonID]);

            AddressTestBO loadedAddress = addresses[0];

            Assert.IsTrue(boMan.Contains(loadedAddress));
            Assert.IsTrue(boMan.Contains(addresssID));
            Assert.IsTrue(boMan.Contains(addresssID.AsString_CurrentValue()));
            Assert.AreSame(loadedAddress, boMan[addresssID]);
            Assert.AreSame(loadedAddress, boMan[addresssID.AsString_CurrentValue()]);
        }
        // ReSharper restore RedundantAssignment


        //Test load single relationship loads into boMan.
        // ReSharper disable RedundantAssignment
        //Test load via multiple relationship loads into boMan.
        [Test]
        public void Test_LoadObject_SingleRelationship_UpdatedObjectMan_Generic()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadClassDefWithAddressTestBOsRelationship();
            new AddressTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            ContactPersonTestBO cp = CreateSavedCP();
            AddressTestBO address = new AddressTestBO();
            address.ContactPersonID = cp.ContactPersonID;
            address.Save();

            IPrimaryKey contactPersonID = cp.ID;
            IPrimaryKey addresssID = address.ID;
            cp = null;
            address = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();

            //---------------Assert Precondition----------------
            Assert.AreEqual(0, boMan.Count);

            //---------------Execute Test ----------------------
            AddressTestBO loadedAddress = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<AddressTestBO>(addresssID);
            ContactPersonTestBO loadedCP = loadedAddress.ContactPersonTestBO;

            //---------------Test Result -----------------------
            Assert.AreEqual(2, boMan.Count);

            Assert.IsTrue(boMan.Contains(loadedCP));
            Assert.IsTrue(boMan.Contains(contactPersonID));
            Assert.AreSame(loadedCP, boMan[contactPersonID]);

            Assert.IsTrue(boMan.Contains(loadedAddress));
            Assert.IsTrue(boMan.Contains(addresssID));
            Assert.IsTrue(boMan.Contains(addresssID.AsString_CurrentValue()));
            Assert.AreSame(loadedAddress, boMan[addresssID]);
            Assert.AreSame(loadedAddress, boMan[addresssID.AsString_CurrentValue()]);
        }
        // ReSharper restore RedundantAssignment

        //Testloading objects when already other objects in object manager
        // ReSharper disable RedundantAssignment
        [Test]
        public void Test_LoadObjectWhenAlreadyObjectInObjectManager()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadClassDefWithAddressTestBOsRelationship();
            new AddressTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;

            AddressTestBO address;
            ContactPersonTestBO cp = CreateSavedCP_WithOneAddresss(out address);

            IPrimaryKey contactPersonID = cp.ID;
            IPrimaryKey addresssID = address.ID;
            cp = null;
            address = null;

            TestUtil.WaitForGC();
            boMan.ClearLoadedObjects();

            AddressTestBO addressOut1;
            AddressTestBO addressOut2;
            AddressTestBO addressOut3;
            ContactPersonTestBO cp2 = CreateSavedCP_WithOneAddresss(out addressOut1);
            ContactPersonTestBO cp3 = CreateSavedCP_WithOneAddresss(out addressOut2);
            ContactPersonTestBO cp4 = CreateSavedCP_WithOneAddresss(out addressOut3);

            //---------------Assert Precondition----------------
            Assert.AreEqual(6, boMan.Count);

            //---------------Execute Test ----------------------
            ContactPersonTestBO loadedCP = BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(contactPersonID);
            RelatedBusinessObjectCollection<AddressTestBO> addresses = loadedCP.AddressTestBOs;

            //---------------Test Result -----------------------
            Assert.AreEqual(1, addresses.Count);
            Assert.AreEqual(8, boMan.Count);

            Assert.IsTrue(boMan.Contains(loadedCP));
            Assert.AreSame(loadedCP, boMan[contactPersonID]);

            AddressTestBO loadedAddress = addresses[0];

            Assert.IsTrue(boMan.Contains(loadedAddress));
            Assert.IsTrue(boMan.Contains(addresssID));
            Assert.IsTrue(boMan.Contains(addresssID.AsString_CurrentValue()));
            Assert.AreSame(loadedAddress, boMan[addresssID]);
            Assert.AreSame(loadedAddress, boMan[addresssID.AsString_CurrentValue()]);
        }
        [Test]
        public void Test_ReturnSameObjectFromBusinessObjectLoader()
        {
            //---------------Set up test pack-------------------
            ContactPersonTestBO.LoadClassDefWithAddressTestBOsRelationship();
            new AddressTestBO();
            BusinessObjectManager boMan = BusinessObjectManager.Instance;
            ContactPersonTestBO originalContactPerson = CreateSavedCP();
            IPrimaryKey id = originalContactPerson.ID;
            originalContactPerson = null;
            boMan.ClearLoadedObjects();
            TestUtil.WaitForGC();

            //load second object from DB to ensure that it is now in the object manager
            ContactPersonTestBO myContact2 =
                BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(id);

            //---------------Assert Precondition----------------
            Assert.AreNotSame(originalContactPerson, myContact2);

            //---------------Execute Test ----------------------
            ContactPersonTestBO myContact3 =
                BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(id);

            //---------------Test Result -----------------------
            Assert.AreNotSame(originalContactPerson, myContact3);
            Assert.AreSame(myContact2, myContact3);
        }


        private static ContactPersonTestBO CreateSavedCP_WithOneAddresss(out AddressTestBO address)
        {
            ContactPersonTestBO cp = CreateSavedCP();
            address = new AddressTestBO();
            address.ContactPersonID = cp.ContactPersonID;
            address.Save();
            return cp;
        }

        // ReSharper restore RedundantAssignment


        [Test]
        public void Test3LayerLoadRelated()
        {
            //---------------Set up test pack-------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            ContactPersonTestBO.DeleteAllContactPeople();
            BORegistry.DataAccessor = new DataAccessorDB();
            OrganisationTestBO.LoadDefaultClassDef();
            TestUtil.WaitForGC();
            AddressTestBO address;
            ContactPersonTestBO contactPersonTestBO =
                ContactPersonTestBO.CreateContactPersonWithOneAddress_CascadeDelete(out address);

            OrganisationTestBO org = new OrganisationTestBO();
            contactPersonTestBO.SetPropertyValue("OrganisationID", org.OrganisationID);
            org.Save();
            contactPersonTestBO.Save();

            //---------------Assert Preconditions --------------
            Assert.AreEqual(3, BusinessObjectManager.Instance.Count);

            //---------------Execute Test ----------------------
            var colContactPeople = org.Relationships.GetMultiple<ContactPersonTestBO>("ContactPeople").BusinessObjectCollection;
            ContactPersonTestBO loadedCP = colContactPeople[0];
            var colAddresses = loadedCP.Relationships.GetMultiple<AddressTestBO>("Addresses").BusinessObjectCollection;
            AddressTestBO loadedAdddress = colAddresses[0];

            //---------------Test Result -----------------------
            Assert.AreEqual(3, BusinessObjectManager.Instance.Count);
            Assert.AreEqual(1, colAddresses.Count);
            Assert.AreSame(contactPersonTestBO, loadedCP);
            Assert.AreSame(address, loadedAdddress);
        }

        [Test]
        public void Test_NewObjectInObjectManager()
        {
            //---------------Set up test pack-------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            ContactPersonTestBO.LoadDefaultClassDef();
            //---------------Assert Precondition----------------
            Assert.AreEqual(0, BusinessObjectManager.Instance.Count);
            //---------------Execute Test ----------------------
            ContactPersonTestBO contactPersonTestBO = new ContactPersonTestBO();
            //---------------Test Result -----------------------
            Assert.IsTrue(contactPersonTestBO.Status.IsNew);
            Assert.IsNotNull(contactPersonTestBO.ContactPersonID);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID));
            Assert.IsFalse(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ContactPersonID.ToString("B")));
        }

        [Test]
        public void Test_SavedObjectInObjectManager()
        {
            //---------------Set up test pack-------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            ContactPersonTestBO.LoadDefaultClassDef();
            ContactPersonTestBO contactPersonTestBO = new ContactPersonTestBO();
            
            //---------------Assert Precondition----------------
            Assert.IsTrue(contactPersonTestBO.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO));
            //---------------Execute Test ----------------------
            contactPersonTestBO.Save();
            //---------------Test Result -----------------------
            Assert.IsFalse(contactPersonTestBO.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID));
        }

        [Test]
        public void Test_ObjectRemovedFromObjectManager()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            ContactPersonTestBO.LoadDefaultClassDef();
            ContactPersonTestBO contactPersonTestBO = new ContactPersonTestBO();
            //--------------- Test Preconditions ----------------
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO));
            //--------------- Execute Test ----------------------
            contactPersonTestBO = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //--------------- Test Result -----------------------
            Assert.AreEqual(0, BusinessObjectManager.Instance.Count);
        }

        [Test]
        public void Test_NewObject_ObjectManagerUpdated_WhenIdChangedOnce_Guid()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            ContactPersonTestBO.LoadDefaultClassDef();
            ContactPersonTestBO contactPersonTestBO = new ContactPersonTestBO();
            Guid firstCpID = Guid.NewGuid();
            Guid secondCpId = Guid.NewGuid();
            contactPersonTestBO.ContactPersonID = firstCpID;

            //---------------Assert Precondition----------------
            Assert.IsTrue(contactPersonTestBO.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.AreNotEqual(firstCpID, secondCpId);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID));
            //---------------Execute Test ----------------------

            contactPersonTestBO.ContactPersonID = secondCpId;
            //---------------Test Result -----------------------
            Assert.IsTrue(contactPersonTestBO.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(contactPersonTestBO.ID));
        }

        [Test]
        public void Test_NewObjectInObjectManager_Int()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            BOWithIntID.LoadClassDefWithIntID();
            //---------------Assert Precondition----------------
            Assert.AreEqual(0, BusinessObjectManager.Instance.Count);
            //---------------Execute Test ----------------------
            BOWithIntID boWithIntID = new BOWithIntID();
            //---------------Test Result -----------------------
            Assert.IsTrue(boWithIntID.Status.IsNew);
            Assert.IsNull(boWithIntID.IntID);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID));
        }

        [Test]
        public void Test_ChangeKey()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            BOWithIntID.LoadClassDefWithIntID();
            BOWithIntID boWithIntID = new BOWithIntID();
            //--------------- Test Preconditions ----------------
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            //--------------- Execute Test ----------------------
            boWithIntID.IntID = TestUtil.GetRandomInt();
            //--------------- Test Result -----------------------
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));

        }
        [Test]
        public void Test_SavedObjectInObjectManager_Int()
        {
            //---------------Set up test pack-------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            BOWithIntID.LoadClassDefWithIntID();
            BOWithIntID boWithIntID = new BOWithIntID();
            boWithIntID.IntID = TestUtil.GetRandomInt();
            //---------------Assert Precondition----------------
            Assert.IsTrue(boWithIntID.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            //---------------Execute Test ----------------------
            boWithIntID.Save();
            //---------------Test Result -----------------------
            Assert.IsFalse(boWithIntID.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID));
        }

        [Test]
        public void Test_ObjectRemovedFromObjectManager_Int()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            BOWithIntID.LoadClassDefWithIntID();
            BOWithIntID boWithIntID = new BOWithIntID();
            //--------------- Test Preconditions ----------------
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            //--------------- Execute Test ----------------------
            boWithIntID = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //--------------- Test Result -----------------------
            Assert.AreEqual(0, BusinessObjectManager.Instance.Count);
        }

        [Test]
        public void Test_NewObject_ObjectManagerUpdated_WhenIdChangedOnce()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            BOWithIntID.LoadClassDefWithIntID();
            BOWithIntID boWithIntID = new BOWithIntID();
            int firstIntID = TestUtil.GetRandomInt();
            int secondIntID = TestUtil.GetRandomInt();
            boWithIntID.IntID = firstIntID;
            
            //---------------Assert Precondition----------------
            Assert.IsTrue(boWithIntID.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.AreNotEqual(firstIntID, secondIntID);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID));
            //---------------Execute Test ----------------------
            
            boWithIntID.IntID = secondIntID;
            //---------------Test Result -----------------------
            Assert.IsTrue(boWithIntID.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID));
        }


        [Test]
        public void Test_NewObject_ObjectManagerUpdated_WhenIdChangedTwice()
        {
            //--------------- Set up test pack ------------------
            BusinessObjectManager.Instance.ClearLoadedObjects();
            BOWithIntID.LoadClassDefWithIntID();
            BOWithIntID boWithIntID = new BOWithIntID();
            int firstIntID = TestUtil.GetRandomInt();
            int secondIntID = TestUtil.GetRandomInt();
            int thirdIntID = TestUtil.GetRandomInt();
            boWithIntID.IntID = firstIntID;
            boWithIntID.IntID = secondIntID;

            //---------------Assert Precondition----------------
            Assert.IsTrue(boWithIntID.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.AreNotEqual(firstIntID, secondIntID);
            Assert.AreNotEqual(secondIntID, thirdIntID);
            Assert.AreNotEqual(firstIntID, thirdIntID);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID));
            //---------------Execute Test ----------------------
            boWithIntID.IntID = thirdIntID;
            //---------------Test Result -----------------------
            Assert.IsTrue(boWithIntID.Status.IsNew);
            Assert.AreEqual(1, BusinessObjectManager.Instance.Count);
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID.AsString_CurrentValue()));
            Assert.IsTrue(BusinessObjectManager.Instance.Contains(boWithIntID.ID));
        }

        //Test if ccreate a new object with object id then must be in object manager.
        //Test that persist this object hten in object manager
        //test that remove revf to this object then call gccolllectt then removed from object mamnager
        //Test for IntID object as above.
        //Test int id change ID ensure in object manger with new id removed with old id
        //test int id change id change id still in object manger with new id removed with intermediate id.
        // test serialization constructor
        private static ContactPersonTestBO CreateSavedCP()
        {
            ContactPersonTestBO cp = new ContactPersonTestBO();
            cp.Surname = TestUtil.CreateRandomString();
            cp.FirstName = TestUtil.CreateRandomString();
            cp.Save();
            return cp;
        }
    }

 
}