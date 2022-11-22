using CarAdverts.Domain.Core.Models;
using CarAdverts.Domain.Core.Validation;
using System;
using System.Globalization;

namespace CarAdverts.Domain.CarAdvert
{
    public class RegistrationDate : ValueObject<RegistrationDate>, IComparable<RegistrationDate>
    {
        public const string BaseDateFormat = "yyyy-MM-dd";

        public DateOnly? Date { get; protected set; }

        private RegistrationDate()
        {
        }

        protected RegistrationDate(string date)
        {
            if (!DateOnly.TryParseExact(date, BaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly firstResult))
            {
                if (!DateOnly.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly secondResult))
                {
                    throw new ValidationException("Invalid date format", "Registration Date");
                }

                Date = CheckDate(secondResult);
                return;
            }

            Date = CheckDate(firstResult);
        }

        private static DateOnly CheckDate(DateOnly date)
        {
            if (date.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0)
            {
                return date;
            }

            throw new ValidationException("Value must be less then today", "Registration Date");
        }

        protected override bool EqualsCore(RegistrationDate other)
            => Date.HasValue && other.Date.HasValue ? Date.Value.Equals(other.Date.Value) : !Date.HasValue && !other.Date.HasValue;

        protected override int GetHashCodeCore()
            => Date.HasValue ? Date.Value.GetHashCode() : 0;

        public override string ToString()
            => Date.HasValue ? Date.Value.ToString(BaseDateFormat) : "";

        public static RegistrationDate Create(string date) => new RegistrationDate(date);
        public static RegistrationDate Create(DateTime date) => new RegistrationDate(date.ToString(BaseDateFormat));

        public int CompareTo(RegistrationDate other)
        {
            if (!Date.HasValue && !other.Date.HasValue)
            {
                return 0;
            }

            if (!Date.HasValue)
            {
                return -1;
            }

            return Date.Value.CompareTo(other.Date);
        }

        public static RegistrationDate NotSpecified => new RegistrationDate();
    }
}
