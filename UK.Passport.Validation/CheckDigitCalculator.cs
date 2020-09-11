using System;

namespace UK.Passport.Validation
{
    public static class CheckDigitCalculator
    {
        public static int Calculate(string input)
        {
            // JSG: Validation
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input),
                    "Input value was null or empty. Please enter valid value");

            decimal checkSum = 0; // JSG: Initialize our return checksum

            int[] weight = {7, 3, 1};

            for (var i = 0; i < input.Length; i++)
            {
                // JSG: Convert our character so that we can work with it
                var convertedChar = CharacterConverter.ConvertCharacterToInt(input[i]);

                // JSG: Add to our existing checksum our converted char multiplied by weight
                //  we find weight by taking current position mod 3 to get the sequence.
                checkSum += convertedChar * weight[i % 3];
            }

            // JSG: According to the paperwork we take the checksum and divide by 10
            //  to get the first decimal. However in this case our number mod 10 will work.
            return Convert.ToInt32(checkSum % 10);
        }
    }
}