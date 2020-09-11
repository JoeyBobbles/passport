using System.Collections.Generic;
using System.Threading.Tasks;
using UK.Passport.Validation.DTOs;

namespace UK.Passport.Validation
{
    public class Validator
    {
        private readonly string _value;

        public Validator(string value)
        {
            _value = value;
        }

        public ValidationResult CrossCheck(string name, string valueToCrossCheck)
        {
            return new ValidationResult(name, _value.Equals(valueToCrossCheck));
        }

        public ValidationResult ValidateCheckDigit(string name, int checkDigit)
        {
            return new ValidationResult(name, checkDigit == CheckDigitCalculator.Calculate(_value));
        }

        public static List<ValidationResult> Validate(PassportLine passportLine, string mrzLine2)
        {
            // JG: Use dictionary type to return results. Always initialise
            // This allows us to give a result string and result value (pass / fail)
            var results = new List<ValidationResult>();

            // JG: We need our decoded line to start with. So we've got something to compare
            var decodedPassportLine = new DecodedPassportLine(mrzLine2);

            // instantiate each validator and run the checks.

            var passportNumberValidator = new Validator(decodedPassportLine.PassportNumber);
            var dateOfBirthValidator = new Validator(decodedPassportLine.DateOfBirth.ToString("yyMMdd"));
            var expirationDateValidator = new Validator(decodedPassportLine.ExpirationDate.ToString("yyMMdd"));
            var personalNumberValidator = new Validator(decodedPassportLine.PersonalNumber);
            var genderValidator = new Validator(decodedPassportLine.Gender);
            var nationalityValidator = new Validator(decodedPassportLine.Nationality);
            var finalCheckDigitValidator = new Validator(decodedPassportLine.FinalCheckDigitSequence);
            
            results.Add(passportNumberValidator.ValidateCheckDigit("Passport Number Check Digit", decodedPassportLine.PassportNumberCheckDigit));
            results.Add(passportNumberValidator.CrossCheck("Passport Number Cross Check", passportLine.PassportNumber));
            results.Add(dateOfBirthValidator.ValidateCheckDigit("Date of Birth Check Digit", decodedPassportLine.DateOfBirthCheckDigit));
            results.Add(dateOfBirthValidator.CrossCheck("Date of Birth Cross Check", passportLine.DateOfBirth.ToString("yyMMdd")));
            results.Add(expirationDateValidator.ValidateCheckDigit("Expiration Date Check Digit", decodedPassportLine.ExpirationDateCheckDigit));
            results.Add(expirationDateValidator.CrossCheck("Expiration Date Cross Check",passportLine.ExpirationDate.ToString("yyMMdd")));
            results.Add(personalNumberValidator.ValidateCheckDigit("Personal Number Check Digit", decodedPassportLine.PersonalNumberCheckDigit));
            results.Add(genderValidator.CrossCheck("Gender Cross Check", passportLine.Gender));
            results.Add(nationalityValidator.CrossCheck("Nationality Cross Check",passportLine.Nationality));
            results.Add(finalCheckDigitValidator.ValidateCheckDigit("Final Check Digit", decodedPassportLine.FinalCheckDigit));

            return results;
        }

        public static async Task<List<ValidationResult>> ValidateAsync(PassportLine passportLine, string mrzLine2)
        {
            var validationResult = await new Task<List<ValidationResult>>(() => Validate(passportLine, mrzLine2));

            return validationResult;
        }
    }
}