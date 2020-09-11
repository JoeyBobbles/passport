using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace UK.Passport.Validation.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        [TestCase("690806", 1)]
        public void Validator_CheckDigit_Validate(string inputValue, int checkDigit)
        {
            var validator = new Validator(inputValue);

            var result = validator.ValidateCheckDigit(checkDigit);

            Assert.AreEqual(true, result);
        }

        [Test]
        [TestCase("L898902", "ABC1234")]
        [TestCase("BNSK120", "JBL123S")]
        public void Validator_CrossCheck_AssertDifferent(string inputValue, string crossCheckValue)
        {
            var validator = new Validator(inputValue);

            var result = validator.CrossCheck(crossCheckValue);

            Assert.AreNotEqual(true, result);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("L898902", "L898902")]
        public void Validator_CrossCheck_AssertEqual(string inputValue, string crossCheckValue)
        {
            var validator = new Validator(inputValue);

            var result = validator.CrossCheck(crossCheckValue);

            Assert.AreEqual(true, result);
        }
    }
}