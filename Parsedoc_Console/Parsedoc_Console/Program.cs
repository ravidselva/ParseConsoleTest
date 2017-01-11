using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                Console.WriteLine(emp.DateTime + "|" + emp.Door + "|" + string.Empty + "|" + emp.Details.ToString());
                
                //Console.WriteLine(String.Format("Date/Time = {0}, Door = {1}, EventType = {2}, Description = {3}", emp.DateTime, emp.Door, string.Empty, emp.Details));
            }

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
            
        }
    }
}
