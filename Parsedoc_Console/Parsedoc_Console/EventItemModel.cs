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
        public DateTime DateTime { get; set; } //e.g. 01.10.2016 13:56:03
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
                        var cellValues = new List<string>();
                        foreach (var cell in row.Cells)
                        {
                            cellValues.Add(cell.Paragraphs.First().Text);
                        }
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
                        eventItemModel.LastName = "";
                        eventItemModel.FirstName = "";

                        eventItemModel.DateTime = DateTime.Now; // DateTime
                        //eventItemModel.EventType = cellValues[1] EventType.Enter; // Event Type
                        eventItemModel.Door = cellValues[2]; //Door
                        eventItemModel.AccessArea = cellValues[3]; // Access Area                      
                        eventItemModel.Details = cellValues[4]; // Details
                        empData.Add(eventItemModel);
                    }
                }
            }
            return empData;
        }
    }

    public enum EventType
    {
        Enter,
        Leave,
        Pass
    }
}