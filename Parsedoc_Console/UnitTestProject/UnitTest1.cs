﻿using System.IO;
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
            const int recordsinFile = 4;
            const string fileName = "UnitTest.docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var eventItemCollection = obj.LoadCollection(fileLocation);
            if (eventItemCollection == null) return;
            var result = eventItemCollection.Count;
            Assert.AreEqual(result, recordsinFile);
        }


        [TestMethod]
        public void TestMethod2()
        {
            var obj = new EventItemModel();
            const string fileName = "full.docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var eventItemCollection = obj.LoadCollection(fileLocation);

            eventItemCollection.Count.Should().BeGreaterThan(10);
        }
    }
}
