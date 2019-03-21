using System;
using System.Collections.Generic;
using System.Text;
using DietPlanner.Core.Extensions;
using NUnit.Framework;

namespace DietPlanner.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void date_time_extension_DateSetToUtcNowIfNull_sets_UtcNow_for_null()
        {
            DateTime? testDate = null;
            Assert.AreEqual(testDate.DateSetToUtcNowIfNull().Date, DateTime.UtcNow.Date);
        }

        [Test]
        public void date_time_extension_DateSetToUtcNowIfNull_leaves_correct_date_for_not_null()
        {
            DateTime? testDate = DateTime.Parse("03/03/2009 05:42:00");
            Assert.AreEqual(testDate.DateSetToUtcNowIfNull().Date, DateTime.Parse("03/03/2009 05:42:00").Date);
        }

    }
}
