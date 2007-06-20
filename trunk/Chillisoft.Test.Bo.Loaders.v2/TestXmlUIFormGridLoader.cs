using Habanero.Bo.Loaders;
using Habanero.Generic;
using Habanero.Ui.Application;
using NUnit.Framework;

namespace Chillisoft.Test.Bo.Loaders.v2
{
    /// <summary>
    /// Summary description for TestXmlUIPropertyLoader.
    /// </summary>
    [TestFixture]
    public class TestXmlUIFormGridLoader
    {
        private XmlUIFormGridLoader loader;

        [SetUp]
        public void SetupTest()
        {
            loader = new XmlUIFormGridLoader();
        }

        [Test]
        public void TestSimpleGrid()
        {
            UIFormGrid formGrid =
                loader.LoadUIFormGrid(
                    @"<uiFormGrid relationshipName=""testrelationshipname"" correspondingRelationshipName=""testcorrespondingrelationshipname"" />");
            Assert.AreEqual("testrelationshipname", formGrid.RelationshipName);
            Assert.AreEqual("testcorrespondingrelationshipname", formGrid.CorrespondingRelationshipName);
            Assert.AreEqual(typeof (SimpleGrid), formGrid.GridType);
        }
    }
}