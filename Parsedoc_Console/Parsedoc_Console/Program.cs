using System;
using System.IO;
namespace Parsedoc_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new EventItemModel();
            const string fileName = "UnitTest.docx";
            var fileLocation = new FileInfo(fileName).FullName;
            var empData = obj.LoadCollection(fileLocation);

            foreach (var emp in empData)
            {
               // Console.WriteLine("{0} | {1} |{2} |{3}|{4}|{5}", emp.FirstName, emp.LastName, emp.DateTime, emp.Door, string.Empty, emp.Details);
               
                Console.WriteLine("First Name = {0}, Last Name = {1}, Date/Time = {2}, Door = {3}, EventType = {4}, Description = {5}", emp.FirstName, emp.LastName,emp.DateTime, emp.Door, emp.Event, emp.Details);
                Console.WriteLine("-----------------------------------------------");
            }
            
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();

        }
    }
}
