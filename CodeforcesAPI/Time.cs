using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeforcesAPI
{
    public class Time
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private readonly DateTime startTime;

        public Time(DateTime start)
        {
            startTime = start;
        }

        public bool SubmittedAfterStart(DateTime submitTime)
        {
            return submitTime.ToLocalTime() >= startTime;
        }
    }
}
