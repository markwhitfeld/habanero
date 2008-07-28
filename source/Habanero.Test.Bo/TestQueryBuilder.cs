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
    public class TestQueryBuilder
    {
        [SetUp]
        public void SetupTest()
        {
            ClassDef.ClassDefs.Clear();
        }

        //TODO: make the fields and order fields case insensitive
        [Test]
        public void TestBuiltFields()
        {
            //---------------Set up test pack-------------------
            MyBO.LoadDefaultClassDef();
            ClassDef classdef = ClassDef.ClassDefs[typeof(MyBO)];
            //---------------Execute Test ----------------------
            ISelectQuery query = QueryBuilder.CreateSelectQuery(classdef);
            //---------------Test Result -----------------------
            Assert.AreEqual(3, query.Fields.Count);
            Assert.AreEqual("MyBoID", query.Fields["MyBoID"].PropertyName);
            Assert.AreEqual("MyBoID", query.Fields["MyBoID"].FieldName);
            Assert.AreEqual(new Source("MyBO"), query.Fields["MyBoID"].Source);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestBuiltWithCriteria()
        {
            //---------------Set up test pack-------------------
            MyBO.LoadDefaultClassDef();
            ClassDef classdef = ClassDef.ClassDefs[typeof(MyBO)];
            Criteria criteria = new Criteria("DateOfBirth", Criteria.Op.Equals, DateTime.Now);
            //---------------Execute Test ----------------------
            ISelectQuery query = QueryBuilder.CreateSelectQuery(classdef, criteria);
            //---------------Test Result -----------------------
            Assert.AreEqual(criteria, query.Criteria);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestBuiltSource()
        {
            //---------------Set up test pack-------------------
            MyBO.LoadDefaultClassDef();
            ClassDef classdef = ClassDef.ClassDefs[typeof(MyBO)];
            //---------------Execute Test ----------------------
            ISelectQuery query = QueryBuilder.CreateSelectQuery(classdef);
            //---------------Test Result -----------------------
            Assert.AreEqual("MyBO", query.Source.EntityName);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestHasClassDef()
        {
            //---------------Set up test pack-------------------
            MyBO.LoadDefaultClassDef();
            ClassDef classdef = ClassDef.ClassDefs[typeof(MyBO)];
            //---------------Execute Test ----------------------
            ISelectQuery query = QueryBuilder.CreateSelectQuery(classdef);

            //---------------Test Result -----------------------
            Assert.AreSame(classdef, query.ClassDef);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestCreateOrderCriteria()
        {
            //---------------Set up test pack-------------------
            MyBO.LoadDefaultClassDefWithDifferentTableAndFieldNames();
            ClassDef classdef = ClassDef.ClassDefs[typeof(MyBO)];
            //---------------Execute Test ----------------------
            OrderCriteria orderCriteria = QueryBuilder.CreateOrderCriteria(classdef, "TestProp");
            //---------------Test Result -----------------------
            OrderCriteria.Field field = orderCriteria.Fields[0];
            Assert.AreEqual(classdef.ClassName, field.Source.Name);
            Assert.AreEqual(classdef.GetTableName(), field.Source.EntityName);
            Assert.AreEqual(classdef.GetPropDef("TestProp").DatabaseFieldName, field.FieldName);
            Assert.AreEqual(0, field.Source.Joins.Count);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestCreateOrderCriteria_InvalidProperty()
        {
            //---------------Set up test pack-------------------
            MyBO.LoadDefaultClassDefWithDifferentTableAndFieldNames();
            string propName = "NonExistantTestProp";
            ClassDef classdef = ClassDef.ClassDefs[typeof(MyBO)];
            //---------------Execute Test ----------------------
            Exception exception = null;
            try
            {
               
                QueryBuilder.CreateOrderCriteria(classdef, propName);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            //---------------Test Result -----------------------
            Assert.IsInstanceOfType(typeof(InvalidPropertyNameException), exception);
            StringAssert.Contains(String.Format(
                                                           "The property definition for the property '{0}' could not be " +
                                                           "found on a ClassDef of type '{1}'", propName, classdef.ClassNameFull), exception.Message);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestCreateOrderCriteria_ThroughRelationship()
        {
            //---------------Set up test pack-------------------
            ClassDef myRelatedBoClassDef = MyRelatedBo.LoadClassDefWithDifferentTableAndFieldNames();
            ClassDef myBoClassdef = MyBO.LoadClassDefWithRelationship(); 

            //---------------Execute Test ----------------------
            OrderCriteria orderCriteria = QueryBuilder.CreateOrderCriteria(myBoClassdef, "MyRelationship.MyRelatedTestProp");
            //---------------Test Result -----------------------
            OrderCriteria.Field field = orderCriteria.Fields[0];
            Assert.AreEqual(myRelatedBoClassDef.GetPropDef("MyRelatedTestProp").DatabaseFieldName, field.FieldName);
            Assert.AreEqual(myBoClassdef.ClassName, field.Source.Name);
            Assert.AreEqual(myBoClassdef.GetTableName(), field.Source.EntityName);
            Assert.AreEqual(1, field.Source.Joins.Count);
            Source.Join relJoin = field.Source.Joins[0];
            Assert.AreEqual("MyRelationship", relJoin.ToSource.Name);
            Assert.AreEqual(myRelatedBoClassDef.GetTableName(), relJoin.ToSource.EntityName);
            Assert.AreEqual(1, relJoin.JoinFields.Count);
            Assert.AreEqual("RelatedID", relJoin.JoinFields[0].FromField.PropertyName);
            Assert.AreEqual("MyRelatedBoID", relJoin.JoinFields[0].ToField.PropertyName);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestCreateOrderCriteria_ThroughRelationship_TwoLevels()
        {
            //---------------Set up test pack-------------------
            ClassDef engineClassDef = Engine.LoadClassDef_IncludingCarAndOwner();
            string orderByString = "Car.Owner.Surname";

            //---------------Execute Test ----------------------
            OrderCriteria orderCriteria = QueryBuilder.CreateOrderCriteria(engineClassDef, orderByString);
            //---------------Test Result -----------------------
            OrderCriteria.Field field = orderCriteria.Fields[0];
            Assert.AreEqual("Surname", field.PropertyName);
            Assert.AreEqual("Surname_field", field.FieldName);
            Assert.AreEqual(1, field.Source.Joins.Count);
            Assert.AreEqual("Engine.Car.Owner", field.Source.ToString());
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestSetOrderCriteria_AddsJoinToSource()
        {
            //---------------Set up test pack-------------------
            MyRelatedBo.LoadClassDefWithDifferentTableAndFieldNames();
            ClassDef myBoClassdef = MyBO.LoadClassDefWithRelationship_DifferentTableAndFieldNames();

            ISelectQuery selectQuery = QueryBuilder.CreateSelectQuery(myBoClassdef);
            OrderCriteria orderCriteria = QueryBuilder.CreateOrderCriteria(myBoClassdef, "MyRelationship.MyRelatedTestProp");

            //---------------Execute Test ----------------------
            selectQuery.OrderCriteria = orderCriteria;

            //---------------Test Result -----------------------
            Assert.AreEqual(1, selectQuery.Source.Joins.Count);
            Source.Join join = selectQuery.Source.Joins[0];
            Assert.AreEqual(selectQuery.Source, join.FromSource);
            Assert.AreEqual("MyRelationship", join.ToSource.Name);
            Assert.AreEqual(1, join.JoinFields.Count);
            Assert.AreEqual("RelatedID" ,join.JoinFields[0].FromField.PropertyName);
            Assert.AreEqual("related_id", join.JoinFields[0].FromField.FieldName);
            Assert.AreEqual("MyRelatedBoID", join.JoinFields[0].ToField.PropertyName);
            Assert.AreEqual("My_Related_Bo_ID", join.JoinFields[0].ToField.FieldName);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestSetOrderCriteria_AddsJoinToSource_OnlyOnce()
        {
            //---------------Set up test pack-------------------
            MyRelatedBo.LoadClassDefWithDifferentTableAndFieldNames();
            ClassDef myBoClassdef = MyBO.LoadClassDefWithRelationship();

            ISelectQuery selectQuery = QueryBuilder.CreateSelectQuery(myBoClassdef);
            OrderCriteria orderCriteria = QueryBuilder.CreateOrderCriteria(myBoClassdef, "MyRelationship.MyRelatedTestProp, MyRelationship.MyRelatedTestProp2");

            //---------------Execute Test ----------------------
            selectQuery.OrderCriteria = orderCriteria;

            //---------------Test Result -----------------------
            Assert.AreEqual(1, selectQuery.Source.Joins.Count);
            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestSetOrderCriteria_AddsJoinToSource_TwoLevels()
        {
            //---------------Set up test pack-------------------
            ClassDef engineClassDef = Engine.LoadClassDef_IncludingCarAndOwner();

            ISelectQuery selectQuery = QueryBuilder.CreateSelectQuery(engineClassDef);
            OrderCriteria orderCriteria = QueryBuilder.CreateOrderCriteria(engineClassDef, "Car.Owner.Surname");

            //---------------Execute Test ----------------------
            selectQuery.OrderCriteria = orderCriteria;

            //---------------Test Result -----------------------
            Assert.AreEqual(1, selectQuery.Source.Joins.Count);
            Source carSource = selectQuery.Source.Joins[0].ToSource;
            Assert.AreEqual("Car", carSource.Name);
            Assert.AreEqual(1, carSource.Joins.Count);
            Assert.AreSame(carSource, carSource.Joins[0].FromSource);
            Assert.AreEqual("Owner", carSource.Joins[0].ToSource.Name);

            //---------------Tear Down -------------------------
        }

        [Test]
        public void TestSingleTableInheritance_Fields()
        {
            //---------------Set up test pack-------------------
            ClassDef circleClassDef = CircleNoPrimaryKey.GetClassDefWithSingleInheritance();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            ISelectQuery selectQuery = QueryBuilder.CreateSelectQuery(circleClassDef);
            //---------------Test Result -----------------------
            Assert.AreEqual(3, selectQuery.Fields.Count);
            Assert.IsTrue(selectQuery.Fields.ContainsKey("ShapeID"));
            Assert.AreEqual("Shape_table", selectQuery.Source.EntityName);
        }

        [Test]
        public void TestSingleTableInheritance_BaseHasDiscriminator()
        {
            //---------------Set up test pack-------------------
            ClassDef circleClassDef = CircleNoPrimaryKey.GetClassDefWithSingleInheritance();
            ClassDef shapeClassDef = circleClassDef.SuperClassClassDef;
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            ISelectQuery selectQuery = QueryBuilder.CreateSelectQuery(shapeClassDef);
            //---------------Test Result -----------------------
            Assert.AreEqual(3, selectQuery.Fields.Count);
            Assert.IsTrue(selectQuery.Fields.ContainsKey("ShapeID"));
            Assert.IsTrue(selectQuery.Fields.ContainsKey("ShapeName"));
            Assert.IsTrue(selectQuery.Fields.ContainsKey("ShapeType_field"));
            Assert.AreEqual("Shape_table", selectQuery.Source.EntityName);
        }


    }

}
