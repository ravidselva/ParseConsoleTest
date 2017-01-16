using System;
using System.Collections.Generic;
using System.Linq;

namespace Parsedoc_Console
{
    public static class AppExtenstions
    {
        public static double GetTotalDurationFor(this List<EventItemModel> lst)
        {
            var selectedEmployeDayinout = lst.FindAll(x => !x.Event.Equals(EventType.Pass)).OrderBy(d => d.DateTime).ToList();
            const int enterExitOperations = 1;
            double duration = 0;
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

        public static DateTime GetDateTime(this string dateString, DocumentDateFormat docDateFormat)
        {
            const int dateIndex = 0;
            const int timeIndex = 1;
            const char splitChar = ' ';
            const int dateAndTimeFounder = 2;
            var dateAndTimeAre = dateString.Split(splitChar);
            const char dateSeparator = '.';
            const char timeSeparator = ':';
            if (dateAndTimeAre.Count() == dateAndTimeFounder)
            {
                var dateParts = dateAndTimeAre[dateIndex].Split(dateSeparator);
                var timeParts = dateAndTimeAre[timeIndex].Split(timeSeparator);
                if (docDateFormat == DocumentDateFormat.ddMMyyyy)
                {
                    return new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]),
                        Convert.ToInt32(dateParts[0]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]),
                        Convert.ToInt32(timeParts[2]));
                }
                if (docDateFormat == DocumentDateFormat.MMddyyyy)
                {
                    return new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[0]),
                        Convert.ToInt32(dateParts[1]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]),
                        Convert.ToInt32(timeParts[2]));
                }
            }
            throw new Exception("Invalid date time string");
        }
    }
}