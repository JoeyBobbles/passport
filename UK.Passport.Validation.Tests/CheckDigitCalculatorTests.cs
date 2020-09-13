using System;
using NUnit.Framework;

namespace UK.Passport.Validation.Tests
{
    [TestFixture]
    public class CheckDigitCalculatorTests
    {
        [Test]
        [TestCase("564105349", 1)] // Passport Number
        [TestCase("542138647", 4)] // Passport Number
        [TestCase("880512", 0)] // Date of Birth
        [TestCase("200102", 3)] // Exp. Date
        [TestCase("ZE184226B<<<<<", 1)] // Personal number check
        [TestCase("<<<<<<<<<<<<<<", 0)] // Personal number check
        [TestCase("L898902C<369080619406236ZE184226B<<<<<1", 4)] // Final check digit
        [TestCase("AB2134", 5)] // Example Case
        public void CheckDidgitcalculator_ValidateInputString(string input, int expectedValue)
        {
            var output = CheckDigitCalculator.Calculate(input);

            Assert.AreEqual(expectedValue, output);
        }

        [Test]
        public void CheckDigitCalculator_TryNullString()
        {
            Assert.Throws<ArgumentNullException>(() => CheckDigitCalculator.Calculate(null));
        }
    }
}