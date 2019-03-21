using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime DateSetToUtcNowIfNull(this DateTime? dt)
        {
            if (dt == null) return DateTime.UtcNow;
            else return dt.Value;
        }
    }
}
