using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace CarAdverts.Infrastructure.ValueConverters
{
    public class DateOnlyComparer : ValueComparer<DateOnly>
    {
        public DateOnlyComparer() : base(
            (d1, d2) => d1.DayNumber == d2.DayNumber,
            d => d.GetHashCode())
        {
        }
    }
}
