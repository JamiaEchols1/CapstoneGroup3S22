using System;

namespace TravelPlannerLibrary.Util
{
    public static class TimeChecker
    {
        public static bool timesOverlapping(DateTime span1Start, DateTime span1End, DateTime span2Start, DateTime span2End)
        {
            return DateTime.Compare(span1Start, span2End) < 0 && DateTime.Compare(span2Start, span1End) < 0;
        }
    }
}
