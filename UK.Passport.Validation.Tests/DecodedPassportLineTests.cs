using System;
using NUnit.Framework;
using UK.Passport.DTO;

namespace UK.Passport.Validation.Tests
{
    [TestFixture]
    public class DecodedPassportLineTests
    {
        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", 1969, 8, 6, 1)]
        public void DecodedPassportLine_TestDecoder_DateOfBirth(string input, int year, int month, int day,
            int checkDigit)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(new DateTime(year, month, day), passportLine.DateOfBirth);
            Assert.AreEqual(checkDigit, passportLine.DateOfBirthCheckDigit);
        }

        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", 1994, 6, 23, 6)]
        public void DecodedPassportLine_TestDecoder_DateOfExpiry(string input, int year, int month, int day,
            int checkDigit)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(new DateTime(year, month, day), passportLine.ExpirationDate);
            Assert.AreEqual(checkDigit, passportLine.ExpirationDateCheckDigit);
        }

        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", 4)]
        public void DecodedPassportLine_TestDecoder_FinalCheckDigit(string input, int checkDigit)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(checkDigit, passportLine.FinalCheckDigit);
        }

        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", "F")]
        public void DecodedPassportLine_TestDecoder_Gender(string input, string expected)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(expected, passportLine.Gender);
        }

        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", "UTO")]
        public void DecodedPassportLine_TestDecoder_Nationality(string input, string expected)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(expected, passportLine.Nationality);
        }

        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", "L898902C", 3)]
        public void DecodedPassportLine_TestDecoder_PassportNumber(string input, string expected, int checkDigit)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(expected, passportLine.PassportNumber);
            Assert.AreEqual(checkDigit, passportLine.PassportNumberCheckDigit);
        }

        [Test]
        [TestCase("L898902C<3UTO6908061F9406236ZE184226B<<<<<14", "ZE184226B<<<<<", 1)]
        public void DecodedPassportLine_TestDecoder_PersonalNumber(string input, string expected, int checkDigit)
        {
            var passportLine = new DecodedPassport(input);

            Assert.AreEqual(expected, passportLine.PersonalNumber);
            Assert.AreEqual(checkDigit, passportLine.PersonalNumberCheckDigit);
        }

        [Test]
        public void DecodedPassportLine_TestDecoder_ThrowsExceptionOnInvalidLength()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new DecodedPassport("ASD12345"));

            Assert.True(ex.Message.Contains("MrzLine2 is not of the correct length"));
        }

        [Test]
        public void DecodedPassportLine_TestDecoder_ThrowsExceptionOnNullOrEmtpy()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DecodedPassport(string.Empty));

            Assert.True(ex.Message.Contains("MrzLine2 is null or empty it should have a value"));
        }
    }
}