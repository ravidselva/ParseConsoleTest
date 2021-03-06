﻿using System;
using System.Collections.Generic;
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
            const string fileName = "full.docx";
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
            new EmployeeDayInformationModel().GetEmployeeDayInfo(fileLocation);
            eventItemCollection.Count.Should().BeGreaterThan(10);
        }

        [TestMethod]
        public void WasInTheOffice_EnteredBefore11_LeftBefore11_ResultShouldBeFalse()
        {
            new List<EventItemModel>
            {
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 0, 0), Event = EventType.Enter},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 30, 0), Event = EventType.Leave}
            }.WasInTheOfficeAt11Am(new DateTime(2017, 1, 1)).Should().BeFalse();
        }

        [TestMethod]
        public void WasInTheOffice_EnteredBefore11_LeftAfter11_ResultShouldBeTrue()
        {
            new List<EventItemModel>
            {
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 0, 0), Event = EventType.Enter},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 11, 30, 0), Event = EventType.Leave}
            }.WasInTheOfficeAt11Am(new DateTime(2017, 1, 1)).Should().BeTrue();
        }


        [TestMethod]
        public void GetTotalDurationFor_PassEventInBetween()
        {
            new List<EventItemModel>
            {
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 0, 0), Event = EventType.Enter},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 30, 0), Event = EventType.Pass},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 11, 30, 0), Event = EventType.Leave}
            }.GetTotalDurationFor().Should().Be(5400);
        }

        [TestMethod]
        public void GetTotalDurationFor_2PassEventsAtTheEnd()
        {
            new List<EventItemModel>
            {
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 0, 0), Event = EventType.Enter},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 11, 30, 0), Event = EventType.Leave},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 11, 40, 0), Event = EventType.Pass},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 12, 40, 0), Event = EventType.Pass}
            }.GetTotalDurationFor().Should().Be(5400);
        }

        [TestMethod]
        public void GetTotalDurationFor_PassEventsBeforeEnter()
        {
            new List<EventItemModel>
            {
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 11, 40, 0), Event = EventType.Pass},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 12, 40, 0), Event = EventType.Pass},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 10, 0, 0), Event = EventType.Enter},
                new EventItemModel {DateTime = new DateTime(2017, 1, 1, 11, 30, 0), Event = EventType.Leave}
            }.GetTotalDurationFor().Should().Be(5400);
        }
    }
}