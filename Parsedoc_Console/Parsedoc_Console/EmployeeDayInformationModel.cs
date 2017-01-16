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
        public DateTime? FirstEnter { get; set; } //01.10.2016 08:33:35
        public DateTime? LastLeave { get; set; } //01.10.2016 18:27:35
        public bool WasInTheOfficeAt11Am { get; set; } //Was particular employee at 11:00 AM at the office or not
        public long TimeInTheOficeInSeconds { get; set; }

        public bool Print()
        {
            Console.WriteLine("Date = {0},First Name ={1} ", Date, FirstName);
            Console.WriteLine("Last Name = {0}, FirstEnter = {1}", LastName, FirstEnter);
            Console.WriteLine("LastLeave = {0},  WasInTheOfficeAt11Am = {1}", LastLeave, WasInTheOfficeAt11Am);
            Console.WriteLine("TimeInTheOficeInSeconds = {0}", TimeInTheOficeInSeconds);
            Console.WriteLine("-----------------------------------------------");
            return true;
        }

        public List<EmployeeDayInformationModel> GetEmployeeDayInfo(string fileLocation)
        {
            var empDayInfoCollection = new List<EmployeeDayInformationModel>();

            var empData = new EventItemModel().LoadCollection(fileLocation);

            foreach (var emp in empData.GroupBy(x => new { x.FirstName, x.LastName })
                         .Select(g => g.First())
                         .ToList())
            {
                foreach (var date in empData.Select(y => y.DateTime.Date).Distinct())
                {
                    var filteredData = empData.FindAll(
                        x =>
                            x.FirstName == emp.FirstName && x.LastName == emp.LastName &&
                            x.DateTime.Date == date.Date);
                    var checkEnter = filteredData.FindAll(x => x.Event == EventType.Enter);
                    var checkLeave = filteredData.FindAll(x => x.Event == EventType.Leave);
                    if (filteredData.Count <= 0) continue;

                    var empDayInfo = new EmployeeDayInformationModel
                    {
                        FirstEnter = checkEnter.Count > 0 ? checkEnter.First().DateTime : (DateTime?) null,
                        Date = Convert.ToDateTime(date.Date),
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        LastLeave = checkLeave.Count > 0 ? checkLeave.Last().DateTime : (DateTime?) null,
                        TimeInTheOficeInSeconds = Convert.ToInt32(filteredData.GetTotalDurationFor()),
                        WasInTheOfficeAt11Am = filteredData.WasInTheOfficeAt11Am(date)
                    };
                    empDayInfo.Print();
                    empDayInfoCollection.Add(empDayInfo);
                }
            }
            return empDayInfoCollection;
        }
    }
}
