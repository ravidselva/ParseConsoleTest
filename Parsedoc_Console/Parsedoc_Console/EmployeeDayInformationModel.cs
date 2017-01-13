using System;
using System.Collections.Generic;
using System.Linq;
using Novacode;

namespace Parsedoc_Console
{
    public class EmployeeDayInformationModel
    {
        public DateTime Date { get; set; } // 01.10.2016
        public string LastName { get; set; } //Абрамов
        public string FirstName { get; set; } //Алексей

        public DateTime FirstEnter { get; set; } //01.10.2016 08:33:35
        public DateTime LastLeave { get; set; } //01.10.2016 18:27:35
        public bool WasInTheOfficeAt11Am { get; set; } //Was particular employee at 11:00 AM at the office or not
        public long TimeInTheOficeInSeconds { get; set; }

        public bool Print()
        {
            Console.WriteLine("Date = {0},First Name ={1} ", Date, FirstName);
            Console.WriteLine("Last Name = {0}, FirstEnter = {1}", LastName, FirstEnter);
            Console.WriteLine("LastLeave = {0},  WasInTheOfficeAt11Am = {1}, TimeInTheOficeInSeconds = {2}", LastLeave, WasInTheOfficeAt11Am, TimeInTheOficeInSeconds);
            Console.WriteLine("TimeInTheOficeInSeconds = {0}", TimeInTheOficeInSeconds);
            Console.WriteLine("-----------------------------------------------");
            return true;
        }

        public List<EmployeeDayInformationModel> GetEmployeeDayInfo(string fileLocation)
        {
            var empDayInfoCollection = new List<EmployeeDayInformationModel>();

            var empData = new EventItemModel().LoadCollection(fileLocation).FindAll(x => x.Event != EventType.Pass);

            foreach (var emp in empData.GroupBy(x => new { x.FirstName, x.LastName })
                         .Select(g => g.First())
                         .ToList())
            {
                foreach (var date in empData.Select(y => y.DateTime.Date).Distinct())
                {
                    var availableAt11Am = new DateTime(date.Year, date.Month, date.Day, 11, 0, 0);
                    if (
                        empData.FindAll(
                            x =>
                                x.FirstName == emp.FirstName && x.LastName == emp.LastName &&
                                x.DateTime.Date == date.Date).Count <= 0) continue;

                    var empDayInfo = new EmployeeDayInformationModel
                    {
                        FirstEnter =
                            empData.FindAll(x => x.FirstName == emp.FirstName && x.LastName == emp.LastName && x.Event == EventType.Enter).First().DateTime
                                ,

                        Date = Convert.ToDateTime(date.Date),
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        LastLeave = empData.FindAll(x => x.FirstName == emp.FirstName && x.LastName == emp.LastName && x.Event == EventType.Leave).Last().DateTime
                    };

                    var duration = empDayInfo.LastLeave.Subtract(empDayInfo.FirstEnter);
                    empDayInfo.TimeInTheOficeInSeconds = Convert.ToInt32(duration.TotalSeconds);
                    empDayInfo.WasInTheOfficeAt11Am = empData.FindAll(
                        x =>
                            x.FirstName == emp.FirstName && x.LastName == emp.LastName &&
                            x.DateTime <= availableAt11Am && x.Event == EventType.Enter &&
                            x.Event != EventType.Leave).Count > 0;
                   
                    empDayInfoCollection.Add(empDayInfo);
                }
            }
            return empDayInfoCollection;
        }
    }
}
