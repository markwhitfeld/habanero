//---------------------------------------------------------------------------------
// Copyright (C) 2007 Chillisoft Solutions
// 
// This file is part of Habanero Standard.
// 
//     Habanero Standard is free software: you can redistribute it and/or modify
//     it under the terms of the GNU Lesser General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     Habanero Standard is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU Lesser General Public License for more details.
// 
//     You should have received a copy of the GNU Lesser General Public License
//     along with Habanero Standard.  If not, see <http://www.gnu.org/licenses/>.
//---------------------------------------------------------------------------------

using Habanero.BO.ClassDefinition;
using Habanero.BO;
using NUnit.Framework;

namespace Habanero.Test.General
{
    [TestFixture]
    public class TestInheritanceBasics
    {
        private ClassDef shapeClassDef;
        private ClassDef circleClassDef;
        private ClassDef circleNoPrimaryKeyClassDef;
        private BusinessObject objShape;
        private BusinessObject objCircle;
        private BusinessObject objCircleNoPrimaryKey;

        [TestFixtureSetUp]
        public void SetupTest()
        {
            shapeClassDef = Shape.GetClassDef();
            circleClassDef = Circle.GetClassDef();
            circleNoPrimaryKeyClassDef = CircleNoPrimaryKey.GetClassDef();
            objShape = new Shape();
            objCircle = new Circle();
            objCircleNoPrimaryKey = new CircleNoPrimaryKey();
        }

        [Test]
        public void TestSuperClassDefProperty()
        {
        	Assert.AreSame(shapeClassDef, circleClassDef.SuperClassDef.SuperClassClassDef,
                           "SuperClassDef.ClassDef property on ClassDef should return the SuperClass's ClassDef");
        }

        [Test]
        public void TestCreateShapeObject()
        {
            Assert.AreSame(typeof (Shape), objShape.GetType(),
                           "objShape should be of type Shape, but is of type " + objShape.GetType().Name);
        }

        [Test]
        public void TestCreateCircleObject()
        {
            Assert.AreSame(typeof (Circle), objCircle.GetType(),
                           "objCircle should be of type Circle, but is of type " + objCircle.GetType().Name);
        }

        [Test]
        public void TestObjCircleIsAShape()
        {
            Assert.IsTrue(objCircle is Shape, "A Circle object should be a Shape");
        }

        [Test]
        public void TestObjCircleHasCorrectProperties()
        {
            objCircle.GetPropertyValue("ShapeName");
        }

        [Test]
        public void TestObjCircleHasShapeKeys()
        {
            Assert.AreEqual(1, objCircle.GetBOKeyCol().Count, "The Circle should have one key inherited from Shape");
        }

        //[Test]
        //public void TestObjCircleHasShapeRelationship()
        //{
        //    Assert.AreEqual(1, objCircle.Relationships.Count,
        //                    "The Circle object should have one relationship inherited from Shape");
        //}

        [Test]
        public void TestCircleNoPrimaryKeyInheritsID()
        {
            Assert.IsNull(circleNoPrimaryKeyClassDef.PrimaryKeyDef);
            Assert.IsNotNull(shapeClassDef.PrimaryKeyDef);

            Shape parent = (Shape) objCircleNoPrimaryKey;
            Assert.AreEqual(objCircleNoPrimaryKey.ID, parent.ID);
            Assert.AreEqual(objCircleNoPrimaryKey.GetPropertyValue("ShapeID"), parent.GetPropertyValue("ShapeID"));
        }
    }
}