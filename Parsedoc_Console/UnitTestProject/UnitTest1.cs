using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsedoc_Console;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = new EventItemModel();
            const string fileName = "DocRussian.docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var eventItemCollection = obj.LoadCollection(fileLocation);
            if (eventItemCollection == null) return;
            eventItemCollection.Count.Should().Be(9);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var obj = new EventItemModel();
            const string fileName = "DocRussian.docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var eventItemCollection = obj.LoadCollection(fileLocation);
            eventItemCollection.Count.Should().Be(9);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var obj = new EmployeeDayInformationModel();
            const string fileName = "UnitTestEnglish .docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var eventItemCollection = obj.GetEmployeeDayInfo(fileLocation);

            eventItemCollection.Count.Should().BeGreaterThan(1);
        }



        [TestMethod]
        public void Artur_TestMethod()
        {
            var obj = new EventItemModel();
            const string fileName = "full.docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var eventItemCollection = obj.LoadCollection(fileLocation);
            var dayInfo = new EmployeeDayInformationModel().GetEmployeeDayInfo(fileLocation);


            eventItemCollection.Count.Should().BeGreaterThan(10);
        }
    }
}
