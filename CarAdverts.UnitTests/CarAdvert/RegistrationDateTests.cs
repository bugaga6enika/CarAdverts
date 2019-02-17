using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Validation;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Globalization;

namespace CarAdverts.UnitTests.CarAdvert
{
    public class RegistrationDateTests
    {
        [TestCase("2019-01-25", "yyyy-MM-dd")]
        [TestCase("20190125", "yyyyMMdd")]
        public void RegistrationDate_Must_Be_Valid_For_Selected_ISO_Date_Formats(string date, string format)
        {
            var registrationDate = RegistrationDate.Create(date);
            var parsedDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);

            var areEquals = registrationDate.Date == parsedDate.Date;

            areEquals.Should().Be(true);
        }

        [TestCase("23-01-2019")]
        [TestCase("2019-02-30")]       
        public void RegistrationDate_Must_Throw_Validation_Exception(string date)
        {
            Action createRegistrationDateAction = () => RegistrationDate.Create(date);

            createRegistrationDateAction.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void RegistrationDate_Must_Throw_Validation_Exception()
        {
            Action createRegistrationDateAction = () => RegistrationDate.Create(DateTime.Now.AddDays(1));

            createRegistrationDateAction.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void RegistrationDate_Must_Be_Valid()
        {
            var date = DateTime.Now.AddDays(-1);

            var firstRegistrationDate =  RegistrationDate.Create(date);

            firstRegistrationDate.Date.Should().Be(date.Date);
        }

        [TestCase("20190216", "2019-02-16")]
        [TestCase("2019-02-16", "2019-02-16")]
        [TestCase("20190216", "20190216")]
        public void RegistrationDates_Should_Be_Equal(string firstDate, string secondDate)
        {
            var firstRegistrationDate = RegistrationDate.Create(firstDate);
            var secondRegistrationDate = RegistrationDate.Create(secondDate);

            var areEqual = firstRegistrationDate == secondRegistrationDate;

            areEqual.Should().Be(true);
        }

        [Test]
        public void NotSpecified_RegistrationDates_Should_Be_Equal()
        {
            var firstDate = RegistrationDate.NotSpecified;
            var secondDate = RegistrationDate.NotSpecified;

            var areEqual = firstDate == secondDate;

            areEqual.Should().Be(true);
        }       
    }
}
