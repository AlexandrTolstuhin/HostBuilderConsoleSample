using System;

namespace HostBuilderConsoleSample
{
    internal class DateService : IDateService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}