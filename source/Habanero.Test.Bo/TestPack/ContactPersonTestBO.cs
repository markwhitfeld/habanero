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
using Habanero.BO;
using Habanero.BO.ClassDefinition;
using Habanero.BO.Loaders;
using Habanero.DB;
using NUnit.Framework;

namespace Habanero.Test.BO
{
    public class ContactPersonTestBO : BusinessObject
    {
        #region ContactType enum

        public enum ContactType
        {
            Family,
            Friend,
            Business
        }

        #endregion

        private bool _afterLoadCalled;

        public static ClassDef LoadDefaultClassDef()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property name=""FirstName"" databaseField=""FirstName_field"" />
					<property name=""DateOfBirth"" type=""DateTime"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }
        public static ClassDef LoadDefaultClassDef_W_IntegerProperty()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property name=""FirstName"" databaseField=""FirstName_field"" />
					<property name=""DateOfBirth"" type=""DateTime"" />
					<property name=""IntegerProperty"" type=""Int32"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }
        public static ClassDef LoadDefaultClassDefWithUIDef()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property name=""FirstName"" databaseField=""FirstName_field"" />
					<property name=""DateOfBirth"" type=""DateTime"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<ui>
						<grid>
							<column heading=""Surname"" property=""Surname"" type=""DataGridViewTextBoxColumn"" />
						</grid>
						<form>
							<tab name=""Tab1"">
								<columnLayout>
									<field label=""Surname"" property=""Surname"" type=""TextBox"" mapperType=""TextBoxMapper"" />
									<field label=""First Name"" property=""FirstName"" type=""TextBox"" mapperType=""TextBoxMapper"" />
								</columnLayout>
							</tab>
						</form>
					</ui>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
//            XmlClassLoader _loader = new XmlClassLoader();
//            ClassDef _classDef =
//                itsLoader.LoadClass(
//                    @"
//				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
//					<property name=""ContactPersonID"" type=""Guid"" />
//					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
//                    <property name=""FirstName"" databaseField=""FirstName_field"" />
//					<property name=""DateOfBirth"" type=""DateTime"" />
//					<primaryKey>
//						<prop name=""ContactPersonID"" />
//					</primaryKey>
//					<ui>
//						<grid>
//							<column heading=""Surname"" property=""Surname"" type=""DataGridViewTextBoxColumn"" />
//							<column heading=""FirstName"" property=""FirstName"" type=""DataGridViewComboBoxColumn"" />
//						</grid>
//						<form>
//							<tab name=""Tab1"">
//								<columnLayout>
//									<field label=""Surname"" property=""Surname"" type=""TextBox"" mapperType=""TextBoxMapper"" />
//									<field label=""First Name"" property=""FirstName"" type=""TextBox"" mapperType=""TextBoxMapper"" />
//								</columnLayout>
//							</tab>
//						</form>
//					</ui>
//			    </class>
//			");
//            ClassDef.ClassDefs.Add(_classDef);
//            return _classDef;
        }


        public static ClassDef LoadClassDefWithSurnameAsPrimaryKey()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
					<primaryKey isObjectID=""false"" >
						<prop name=""Surname"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithCompositePrimaryKey()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
					<primaryKey isObjectID=""false"" >
						<prop name=""ContactPersonID"" />
                        <prop name=""Surname"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithCompositeAlternateKey()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property name=""FirstName"" databaseField=""FirstName_field"" />
                    <key name=""AlternateKey"">
                        <prop name=""Surname"" />
                        <prop name=""FirstName"" />
                    </key>
                    <primaryKey >
						<prop name=""ContactPersonID"" />
					</primaryKey>

			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithCompositePrimaryKeyNameSurname()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property name=""ContactPersonID"" type=""Guid"" />
					<property name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property name=""FirstName"" databaseField=""FirstName_field"" />
                    <primaryKey isObjectID=""false""  >
                        <prop name=""Surname"" />
                        <prop name=""FirstName"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadFullClassDef()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithAddressesRelationship_DeleteRelated()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<relationship name=""Addresses"" type=""multiple"" relatedClass=""Address"" relatedAssembly=""Habanero.Test"" deleteAction=""DeleteRelated"">
						<relatedProperty property=""ContactPersonID"" relatedProperty=""ContactPersonID"" />
					</relationship>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithAddressesRelationship_PreventDelete()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<relationship name=""Addresses"" type=""multiple"" relatedClass=""Address"" relatedAssembly=""Habanero.Test"" deleteAction=""Prevent"">
						<relatedProperty property=""ContactPersonID"" relatedProperty=""ContactPersonID"" />
					</relationship>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithAddressTestBOsRelationship()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<relationship name=""AddressTestBOs"" type=""multiple"" relatedClass=""AddressTestBO"" relatedAssembly=""Habanero.Test.BO"" deleteAction=""Prevent"">
						<relatedProperty property=""ContactPersonID"" relatedProperty=""ContactPersonID"" />
					</relationship>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithAddressesRelationship_PreventDelete_WithUIDef()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<relationship name=""Addresses"" type=""multiple"" relatedClass=""Address"" relatedAssembly=""Habanero.Test"" deleteAction=""Prevent"">
						<relatedProperty property=""ContactPersonID"" relatedProperty=""ContactPersonID"" />
					</relationship>
					<ui>
						<grid>
							<column heading=""Surname"" property=""Surname"" type=""DataGridViewTextBoxColumn"" />
							<column heading=""FirstName"" property=""FirstName"" type=""DataGridViewComboBoxColumn"" />
						</grid>
						<form>
							<tab name=""Tab1"">
								<columnLayout>
									<field label=""Surname"" property=""Surname"" type=""TextBox"" mapperType=""TextBoxMapper"" />
									<field label=""First Name"" property=""FirstName"" type=""TextBox"" mapperType=""TextBoxMapper"" />
								</columnLayout>
							</tab>
						</form>
					</ui>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithAddressesRelationship_DeleteDoNothing()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<relationship name=""Addresses"" type=""multiple"" relatedClass=""Address"" relatedAssembly=""Habanero.Test"" deleteAction=""DoNothing"">
						<relatedProperty property=""ContactPersonID"" relatedProperty=""ContactPersonID"" />
					</relationship>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }


        public static ClassDef LoadClassDefWithAddressesRelationship_SortOrder_AddressLine1()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<relationship name=""Addresses"" type=""multiple"" relatedClass=""Address"" relatedAssembly=""Habanero.Test"" deleteAction=""DoNothing"" orderBy=""AddressLine1"">
						<relatedProperty property=""ContactPersonID"" relatedProperty=""ContactPersonID"" />
					</relationship>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefWithImageProperty()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""Image"" type=""System.Drawing.Bitmap"" assembly=""System.Drawing"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadDefaultClassDefWithUIDef_ReadWriteRule()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<ui>
						<grid>
							<column heading=""Surname"" property=""Surname"" type=""DataGridViewTextBoxColumn"" />
						</grid>
						<form>
							<tab name=""Tab1"">
								<columnLayout>
									<field label=""Surname"" property=""Surname"" type=""TextBox"" mapperType=""TextBoxMapper"" >
                                        <parameter name=""readWriteRule"" value=""WriteNew"" />
                                     </field>
									<field label=""First Name"" property=""FirstName"" type=""TextBox"" mapperType=""TextBoxMapper"" >
                                        <parameter name=""readWriteRule"" value=""WriteNotNew"" />
                                     </field>
								</columnLayout>
							</tab>
						</form>
					</ui>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefOrganisationRelationship()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" databaseField=""Surname_field"" compulsory=""true"" />
                    <property  name=""FirstName"" databaseField=""FirstName_field"" compulsory=""true"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" >
                      <businessObjectLookupList class=""Organisation"" assembly=""Habanero.Test"" />
                    </property>
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<ui>
						<grid>
							<column heading=""OrganisationID"" property=""OrganisationID"" type=""DataGridViewComboBoxColumn"" />
						</grid>
                    </ui>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static ClassDef LoadClassDefOrganisationTestBORelationship()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef =
                itsLoader.LoadClass(
                    @"
				<class name=""ContactPersonTestBO"" assembly=""Habanero.Test.BO"" table=""contact_person"">
					<property  name=""ContactPersonID"" type=""Guid"" />
					<property  name=""Surname"" compulsory=""true"" databaseField=""Surname_field"" />
                    <property  name=""FirstName"" compulsory=""true"" databaseField=""FirstName_field"" />
					<property  name=""DateOfBirth"" type=""DateTime"" />
                    <property  name=""OrganisationID"" type=""Guid"" >
                      <businessObjectLookupList class=""OrganisationTestBO"" assembly=""Habanero.Test.BO"" />
                    </property>
					<primaryKey>
						<prop name=""ContactPersonID"" />
					</primaryKey>
					<ui>
						<grid>
							<column heading=""OrganisationID"" property=""OrganisationID"" type=""DataGridViewComboBoxColumn"" />
						</grid>
                    </ui>
			    </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public override string ToString()
        {
            return Surname;
        }

        internal static void DeleteAllContactPeople()
        {
            string sql = "DELETE FROM contact_person_address";
            DatabaseConnection.CurrentConnection.ExecuteRawSql(sql);
            sql = "DELETE FROM Contact_Person";
            DatabaseConnection.CurrentConnection.ExecuteRawSql(sql);
        }

        public static void CreateSampleData()
        {
            ClassDef.ClassDefs.Clear();
            LoadFullClassDef();

            string[] surnames = {"zzz", "abc", "abcd"};
            string[] firstNames = {"a", "aa", "aa"};

            for (int i = 0; i < surnames.Length; i++)
            {
                Criteria criteria = new Criteria("Surname", Criteria.ComparisonOp.Equals, surnames[i]);
                if (BORegistry.DataAccessor.BusinessObjectLoader.GetBusinessObject<ContactPersonTestBO>(criteria) !=
                    null)
                    continue;
                ContactPersonTestBO contact = new ContactPersonTestBO();
                contact.Surname = surnames[i];
                contact.FirstName = firstNames[i];
                contact.Save();
            }
            ClassDef.ClassDefs.Clear();
        }

        public static ContactPersonTestBO CreateSavedContactPersonNoAddresses()
        {
            ContactPersonTestBO contactPersonTestBO = new ContactPersonTestBO();
            contactPersonTestBO.Surname = TestUtil.CreateRandomString();
            contactPersonTestBO.FirstName = TestUtil.CreateRandomString();
            contactPersonTestBO.Save();
            return contactPersonTestBO;
        }

        public static ContactPersonTestBO CreateContactPersonWithOneAddress_CascadeDelete(out Address address)
        {
            LoadClassDefWithAddressesRelationship_DeleteRelated();
            return CreateContactPerson(out address);
        }

        public static ContactPersonTestBO CreateContactPersonWithOneAddress_PreventDelete(out Address address)
        {
            LoadClassDefWithAddressesRelationship_PreventDelete();
            return CreateContactPerson(out address);
        }

        private static ContactPersonTestBO CreateContactPerson(out Address address)
        {
            ContactPersonTestBO contactPersonTestBO = CreateSavedContactPersonNoAddresses();
            address = contactPersonTestBO.Addresses.CreateBusinessObject();
            address.Save();
            Assert.AreEqual(1, contactPersonTestBO.Addresses.Count);
            return contactPersonTestBO;
        }

        public static ContactPersonTestBO CreateContactPersonWithOneAddress_DeleteDoNothing(out Address address)
        {
            LoadClassDefWithAddressesRelationship_DeleteDoNothing();
            return CreateContactPerson(out address);
        }

        public static ContactPersonTestBO CreateSavedContactPerson(DateTime? dteBirth, string surname, string firstName)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson(surname, firstName);
            if (dteBirth != null)
            {
                contact.DateOfBirth = dteBirth.Value;
            }
            contact.Save();
            return contact;
        }
        public static ContactPersonTestBO CreateSavedContactPerson(string surname, string firstName)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson(surname, firstName);
            contact.Save();
            return contact;
        }

        public static ContactPersonTestBO CreateSavedContactPerson()
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson();
            contact.Save();
            return contact;
        }

        public static ContactPersonTestBO CreateSavedContactPerson(string surname)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson(surname);
            contact.Save();
            return contact;
        }

        public static ContactPersonTestBO CreateSavedContactPerson(DateTime dateOfBirth)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson(dateOfBirth);
            contact.Save();
            return contact;
        }

        public static ContactPersonTestBO CreateSavedContactPerson(DateTime dateOfBirth, string surname)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson(dateOfBirth, surname);
            contact.Save();
            return contact;
        }

        private static ContactPersonTestBO CreateUnsavedContactPerson()
        {
            return CreateUnsavedContactPerson(TestUtil.CreateRandomString());
        }

        private static ContactPersonTestBO CreateUnsavedContactPerson(DateTime dateOfBirth)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson();
            contact.DateOfBirth = dateOfBirth;
            return contact;
        }

        private static ContactPersonTestBO CreateUnsavedContactPerson(string surname)
        {
            return CreateUnsavedContactPerson(surname, TestUtil.CreateRandomString());
        }

        private static ContactPersonTestBO CreateUnsavedContactPerson(DateTime dateOfBirth, string surname)
        {
            ContactPersonTestBO contact = CreateUnsavedContactPerson(surname);
            contact.DateOfBirth = dateOfBirth;
            return contact;
        }

        public static ContactPersonTestBO CreateUnsavedContactPerson(string surname, string firstName)
        {
            ContactPersonTestBO contact = new ContactPersonTestBO();
            contact.Surname = surname;
            contact.FirstName = firstName;
            return contact;
        }

        #region Properties

        public Guid ContactPersonID
        {
            get { return (Guid) GetPropertyValue("ContactPersonID"); }
            set { SetPropertyValue("ContactPersonID", value); }
        }

        public string Surname
        {
            get { return (string) GetPropertyValue("Surname"); }
            set { SetPropertyValue("Surname", value); }
        }

        public string FirstName
        {
            get { return (string) GetPropertyValue("FirstName"); }
            set { SetPropertyValue("FirstName", value); }
        }

        public DateTime DateOfBirth
        {
            get { return (DateTime) GetPropertyValue("DateOfBirth"); }
            set { SetPropertyValue("DateOfBirth", value); }
        }

        public RelatedBusinessObjectCollection<Address> Addresses
        {
            get
            {
                return
                    (RelatedBusinessObjectCollection<Address>) Relationships.GetRelatedCollection<Address>("Addresses");
            }
        }

        public bool AfterLoadCalled
        {
            get { return _afterLoadCalled; }
            set { _afterLoadCalled = value; }
        }

        public RelatedBusinessObjectCollection<AddressTestBO> AddressTestBOs
        {
            get
            {
                return
                    (RelatedBusinessObjectCollection<AddressTestBO>)
                    Relationships.GetRelatedCollection<AddressTestBO>("AddressTestBOs");
            }
        }


        protected internal override void AfterLoad()
        {
            base.AfterLoad();
            _afterLoadCalled = true;
        }

        #endregion //Properties
    }
}