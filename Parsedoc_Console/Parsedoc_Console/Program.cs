using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Parsedoc_Console
{
    class Program
    {
        static void Main(string[] args)
        {            
           var obj = new EventItemModel();
            var objDayInfo = new EmployeeDayInformationModel();
            const string fileName = "full.docx";
            var fileLocation = new FileInfo(fileName).FullName;

            Console.WriteLine("Enter 0 for full info and 1 for day wise info");
            var intType = Convert.ToInt32(Console.ReadLine());

            switch (intType)
            {
                case 0:
                    var empData = obj.LoadCollection(fileLocation);

                    foreach (var emp in empData)
                    {
                        Console.WriteLine("First Name = {0}, Last Name = {1}, Date/Time = {2}, Door = {3}, EventType = {4}, Description = {5}", emp.FirstName, emp.LastName, emp.DateTime, emp.Door, emp.Event, emp.Details);
                        Console.WriteLine("-----------------------------------------------");
                    }
                    break;
                case 1:
                    var empDayInfo = objDayInfo.GetEmployeeDayInfo(fileLocation).OrderBy(o => o.Date).ToList();
                    empDayInfo.ForEach(x=>x.Print());
                    break;
                default:
                    Console.WriteLine("Wrong enter. please try again !!");
                    Console.WriteLine("Enter 0 for full info and 1 for day wise info");
                    intType = Convert.ToInt32(Console.ReadLine());
                    break;
            }


            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
          
        }
    }
}
