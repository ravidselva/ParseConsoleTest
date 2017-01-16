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
            var firstName = string.Empty;
            var lastName = string.Empty;
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
                                eventItemModel.Event = EventType.Leave;
                                break;
                            case "Вход":
                                eventItemModel.Event = EventType.Enter;
                                break;
                            case "Проход":
                                eventItemModel.Event = EventType.Pass;
                                break;
                        }

                        #region for testing code in english

                        //switch (cellValues[1].Trim())
                        //{
                        //    case "Exit":
                        //        eventItemModel.Event = EventType.Leave;
                        //        break;
                        //    case "Entrance":
                        //        eventItemModel.Event = EventType.Enter;
                        //        break;
                        //    case "Passage":
                        //        eventItemModel.Event = EventType.Pass;
                        //        break;
                        //}

                        #endregion

                        if (cellValues[4] == "Дата рождения")
                        {
                            lastName = cellValues[1];
                        }
                        if (cellValues[4] == "Должность")
                        {
                            firstName = cellValues[1];
                        }

                        #region #Testing For English

                        //if (cellValues[4] == "Date of Birth")
                        //{
                        //    lastName = cellValues[1];
                        //}
                        //if (cellValues[4] == "Position")
                        //{
                        //    firstName = cellValues[1];
                        //}

                        #endregion

                        // DateTime

                        eventItemModel.DateTime = cellValues[0] == "дата/время"
                            ? DateTime.MinValue
                            : string.IsNullOrEmpty(cellValues[0])
                                ? DateTime.MinValue
                                : Convert.ToDateTime(cellValues[0]);

                        eventItemModel.LastName = lastName; // Last Name
                        eventItemModel.FirstName = firstName; // First Name
                        eventItemModel.Door = cellValues[2]; //Door
                        eventItemModel.AccessArea = cellValues[3]; // Access Area                      
                        eventItemModel.Details = cellValues[4]; // Details
                        if (eventItemModel.Details != "Табельный номер")
                        {
                            empData.Add(eventItemModel);
                        }
                    }
                }
            }
            return
                empData.FindAll(x => !string.IsNullOrEmpty(x.FirstName))
                    .FindAll(y => y.Door != string.Empty)
                    .FindAll(z => z.Door != "дверь");
        }
    }

    public enum EventType
    {
        Enter,
        Leave,
        Pass
    }
}