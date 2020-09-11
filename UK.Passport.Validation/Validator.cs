using System;

namespace UK.Passport.Validation
{
    public class Validator
    {
        private readonly string _value;

        public Validator(string value)
        {
            _value = value;
        }

        public bool CrossCheck(string valueToCrossCheck)
        {
            return _value.Equals(valueToCrossCheck);
        }

        public bool ValidateCheckDigit(int checkDigit)
        {
            return checkDigit == CheckDigitCalculator.Calculate(_value);
        }
    }
}