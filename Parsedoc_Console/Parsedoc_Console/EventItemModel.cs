using System;
using System.Collections.Generic;
using System.Linq;
using Novacode;

namespace Parsedoc_Console
{
    public class EventItemModel
    {
        public string LastName { get; set; } //e.g. Абрамов
        public string FirstName { get; set; } //e.g. Алексей
        public string DateTime { get; set; } //e.g. 01.10.2016 13:56:03
        public EventType Event { get; set; } //e.g. EventType.Enter
        public string AccessArea { get; set; }
        public string Details { get; set; }
        public string Door { get; set; }

        public List<EventItemModel> LoadCollection(string fileLocation)
        {
            var empData = new List<EventItemModel>();

            using (
                var docX =
                    DocX.Load(fileLocation))
            {
                foreach (var table in docX.Tables)
                {
                    foreach (var row in table.Rows)
                    {
                        var eventItemModel = new EventItemModel();
                        var cellValues = row.Cells.Select(cell => cell.Paragraphs.First().Text).ToList();
                        switch (cellValues.Count)
                        {
                            case 1:
                                cellValues.Add(string.Empty);
                                cellValues.Add(string.Empty);
                                cellValues.Add(string.Empty);
                                cellValues.Add(string.Empty);
                                break;
                            case 2:
                                cellValues.Add(string.Empty);
                                cellValues.Add(string.Empty);
                                cellValues.Add(string.Empty);
                                break;
                            case 3:
                                cellValues.Add(string.Empty);
                                cellValues.Add(string.Empty);
                                break;
                            case 4:
                                cellValues.Add(string.Empty);
                                break;
                        }
                        // Event Type
                        switch (cellValues[1].Trim())
                        {
                            case "Выход":
                                eventItemModel.Event = EventType.Exit;
                                break;
                            case "Вход":
                                eventItemModel.Event = EventType.Entrance;
                                break;
                            case "Проход":
                                eventItemModel.Event = EventType.Passage;
                                break;
                        }

                        if (cellValues[4] == "Date of birth")
                        {
                            Global.LastName = cellValues[1];
                        }
                        if (cellValues[4] == "Position")
                        {
                            Global.FirstName = cellValues[1];
                        }
                       
                        // DateTime
                        eventItemModel.DateTime = cellValues[0];
                        eventItemModel.LastName = Global.LastName;
                        eventItemModel.FirstName = Global.FirstName;
                        eventItemModel.Door = cellValues[2].ToString(); //Door
                        eventItemModel.AccessArea = cellValues[3].ToString(); // Access Area                      
                        eventItemModel.Details = cellValues[4].ToString(); // Details
                        empData.Add(eventItemModel);
                    }
                }
            }

            return empData.FindAll(x => x.FirstName !=null).FindAll(y=>y.Door != string.Empty).FindAll(z=>z.Door != "Door");
        }

    }

    public enum EventType
    {
        Entrance, 
        Exit,
        Passage
    }

    public static class Global
    {
        public static string LastName { get; set; }
        public static string FirstName { get; set; }
    }
} 