using System;
using System.Collections.Generic;
using System.Linq;

namespace Parsedoc_Console
{
    public static class AppExtenstions
    {
        public static double GetTotalDurationFor(this List<EventItemModel> lst)
        {
            var selectedEmployeDayinout = lst.OrderBy(d => d.DateTime).ToList();
            const int enterExitOperations = 1;
            double duration = 0;

           // if ((selectedEmployeDayinout.Count()%enterExitOperations) != 0) return duration;
            for (var i = 0; i < selectedEmployeDayinout.Count()-1; i += enterExitOperations)
            {
                var enterDate = selectedEmployeDayinout[i].DateTime;
                var leaveDate = selectedEmployeDayinout[i + 1].DateTime;
                duration += leaveDate.Subtract(enterDate).TotalSeconds;
            }
            return duration;
        }

        public static bool WasInTheOfficeAt11Am(this List<EventItemModel> lst, DateTime date)
        {
            var availableAt11Am = new DateTime(date.Year, date.Month, date.Day, 11, 0, 0);
            var selectedEmployeDayin = lst.FindAll(x => x.Event.Equals(EventType.Enter)).OrderBy(d => d.DateTime).ToList();
            var selectedEmployeDayout = lst.FindAll(x => x.Event.Equals(EventType.Leave)).OrderBy(d => d.DateTime).ToList();
            var avail = false;            
            var loopCount = selectedEmployeDayin.Count > selectedEmployeDayout.Count
                ? selectedEmployeDayin.Count
                : selectedEmployeDayout.Count;            
            for (var i = 0; i < loopCount; i++)
            {
                try
                {
                    var enterDate = selectedEmployeDayin[i].DateTime;
                    var leaveDate = selectedEmployeDayout[i].DateTime;
                    if (enterDate.TimeOfDay > availableAt11Am.TimeOfDay ||
                        leaveDate.TimeOfDay < availableAt11Am.TimeOfDay) continue;
                    avail = true;
                    break;
                }
                catch (Exception)
                {
                    avail = false;
                    break;
                }
            }
            return avail;
        }
    }
}