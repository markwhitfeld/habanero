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

// ------------------------------------------------------------------------------
// This partial class was auto-generated for use with the Habanero Architecture.
// ------------------------------------------------------------------------------

using Habanero.BO.ClassDefinition;
using Habanero.BO.Loaders;

namespace Habanero.Test.Structure
{
    public partial class Car
    {
        public new static ClassDef LoadDefaultClassDef()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef = itsLoader.LoadClass(@"
			  <class name=""Car"" assembly=""Habanero.Test.Structure"" table=""table_Car"">
			    <property name=""CarID"" type=""Guid"" databaseField=""field_Car_ID"" />
			    <property name=""RegistrationNo"" databaseField=""field_Registration_No"" />
			    <property name=""Length"" type=""Double"" databaseField=""field_Length"" />
			    <property name=""IsConvertible"" type=""Boolean"" databaseField=""field_Is_Convertible"" />
			    <property name=""DriverID"" type=""Guid"" databaseField=""field_Driver_ID"" />
			    <primaryKey>
			      <prop name=""CarID"" />
			    </primaryKey>
			    <relationship name=""Driver"" type=""single"" relatedClass=""Person"" relatedAssembly=""Habanero.Test.Structure"">
			      <relatedProperty property=""DriverID"" relatedProperty=""PersonID"" />
			    </relationship>
			  </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public new static ClassDef LoadClassDef_WithClassTableInheritance()
        {
            XmlClassLoader itsLoader = new XmlClassLoader();
            ClassDef itsClassDef = itsLoader.LoadClass(@"
			  <class name=""Car"" assembly=""Habanero.Test.Structure"" table=""table_class_Car"">
			    <superClass class=""Vehicle"" assembly=""Habanero.Test.Structure"" />
			    <property name=""CarID"" type=""Guid"" databaseField=""field_Car_ID"" />
			    <property name=""RegistrationNo"" databaseField=""field_Registration_No"" />
			    <property name=""Length"" type=""Double"" databaseField=""field_Length"" />
			    <property name=""IsConvertible"" type=""Boolean"" databaseField=""field_Is_Convertible"" />
			    <property name=""DriverID"" type=""Guid"" databaseField=""field_Driver_ID"" />
			    <primaryKey>
			      <prop name=""CarID"" />
			    </primaryKey>
			    <relationship name=""Driver"" type=""single"" relatedClass=""Person"" relatedAssembly=""Habanero.Test.Structure"">
			      <relatedProperty property=""DriverID"" relatedProperty=""PersonID"" />
			    </relationship>
			  </class>
			");
            ClassDef.ClassDefs.Add(itsClassDef);
            return itsClassDef;
        }

        public static Car CreateSavedCar()
        {
            Car car = CreateUnsavedCar();
            car.Save();
            return car;
        }

        public static Car CreateSavedCar(string registrationNo)
        {
            Car car = CreateUnsavedCar(registrationNo);
            car.Save();
            return car;
        }

        private static Car CreateUnsavedCar()
        {
            return CreateUnsavedCar(TestUtil.CreateRandomString());
        }

        private static Car CreateUnsavedCar(string registrationNo)
        {
            Car car = new Car();
            car.RegistrationNo = registrationNo;
            return car;
        }
    }
}