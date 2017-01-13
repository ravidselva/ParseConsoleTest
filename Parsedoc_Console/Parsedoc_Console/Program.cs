using System;
using System.IO;
namespace Parsedoc_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new EventItemModel();
            var objDayInfo = new EmployeeDayInformationModel();
            const string fileName = "DocRussian.docx";
            var fileLocation = new FileInfo(fileName).FullName;

            Console.WriteLine("Enter 0 for full info and 1 for day wise info");
            int intType = Convert.ToInt32(Console.ReadLine());

            if (intType == 0)
            {
                var empData = obj.LoadCollection(fileLocation);

                foreach (var emp in empData)
                {
                    Console.WriteLine("First Name = {0}, Last Name = {1}, Date/Time = {2}, Door = {3}, EventType = {4}, Description = {5}", emp.FirstName, emp.LastName, emp.DateTime, emp.Door, emp.Event, emp.Details);
                    Console.WriteLine("-----------------------------------------------");
                }

            }
            else if (intType == 1)
            {
                var empDayInfo = objDayInfo.GetEmployeeDayInfo(fileLocation);
                empDayInfo.ForEach(x=>x.Print());
            }
            else
            {
                Console.WriteLine("Wrong enter. please try again !!");
                Console.WriteLine("Enter 0 for full info and 1 for day wise info");
                intType = Convert.ToInt32(Console.ReadLine());
            }


            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();

        }
    }
}
