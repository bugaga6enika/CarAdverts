using CarAdverts.Domain.Core.Models;
using CarAdverts.Domain.Core.Validation;
using System;
using System.Globalization;

namespace CarAdverts.Domain.CarAdvert
{
    public class RegistrationDate : ValueObject<RegistrationDate>
    {
        public const string BaseDateFormat = "yyyy-MM-dd";

        public DateTime? Date { get; protected set; }

        private RegistrationDate()
        {
        }

        protected RegistrationDate(string date)
        {
            if (!DateTime.TryParseExact(date, BaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime firstResult))
            {
                if (!DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime secondResult))
                {
                    throw new ValidationException("Invalid date format", "Registration Date");
                }

                Date = CheckDate(secondResult);
                return;
            }

            Date = CheckDate(firstResult);
        }

        private DateTime CheckDate(DateTime dateTime)
        {
            if (DateTime.Compare(dateTime.Date, DateTime.Now.Date) < 0)
            {
                return dateTime;
            }

            throw new ValidationException("Value must be less then today", "Registration Date");
        }

        protected override bool EqualsCore(RegistrationDate other)
            => Date.HasValue && other.Date.HasValue ? Date.Value.Date.Equals(other.Date.Value.Date) : !Date.HasValue && !other.Date.HasValue;

        protected override int GetHashCodeCore()
            => Date.HasValue ? Date.Value.Date.GetHashCode() : 0;

        public override string ToString()
            => Date.HasValue ? Date.Value.ToString(BaseDateFormat) : "";

        public static RegistrationDate Create(string date) => new RegistrationDate(date);
        public static RegistrationDate Create(DateTime date) => new RegistrationDate(date.ToString(BaseDateFormat));
        public static RegistrationDate NotSpecified => new RegistrationDate();
    }
}
