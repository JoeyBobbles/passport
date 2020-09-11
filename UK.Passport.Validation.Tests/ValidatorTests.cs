using UK.Passport.Validation.DTOs;
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

            var result = validator.ValidateCheckDigit(nameof(Validator_CheckDigit_Validate), checkDigit);

            var expectedValidationResult = new DTOs.ValidationResult(nameof(Validator_CheckDigit_Validate), true);
            
            Assert.AreEqual(expectedValidationResult.Id, result.Id);
            Assert.AreEqual(expectedValidationResult.Result, result.Result);
        }

        [Test]
        [TestCase("L898902", "ABC1234")]
        [TestCase("BNSK120", "JBL123S")]
        public void Validator_CrossCheck_AssertDifferent(string inputValue, string crossCheckValue)
        {
            var validator = new Validator(inputValue);

            var result = validator.CrossCheck(nameof(Validator_CrossCheck_AssertDifferent), crossCheckValue);

            var expectedValidationResult = new DTOs.ValidationResult(nameof(Validator_CrossCheck_AssertDifferent), true);
            
            Assert.AreEqual(expectedValidationResult.Id, result.Id);
            Assert.AreNotEqual(expectedValidationResult.Result, result.Result);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("L898902", "L898902")]
        public void Validator_CrossCheck_AssertEqual(string inputValue, string crossCheckValue)
        {
            var validator = new Validator(inputValue);

            var result = validator.CrossCheck(nameof(Validator_CrossCheck_AssertEqual), crossCheckValue);
            
            var expectedValidationResult = new DTOs.ValidationResult(nameof(Validator_CrossCheck_AssertEqual), true);

            Assert.AreEqual(expectedValidationResult.Id, result.Id);
            Assert.AreEqual(expectedValidationResult.Result, result.Result);
        }
    }
}