using System;
using System.Globalization;

namespace UK.Passport.Validation
{
    public class DecodedPassport : DTO.Passport
    {
        public DecodedPassport(string mrzLine2)
        {
            MzrLine2 = mrzLine2;

            DecodeString(mrzLine2);
        }

        public string MzrLine2 { get; }

        // JSG: This is an additional class that we'd use when using the decoder for passport
        //  We can split the UI work into components so we dont have unsued fields
        public int PassportNumberCheckDigit { get; set; }
        public int DateOfBirthCheckDigit { get; set; }
        public int ExpirationDateCheckDigit { get; set; }
        public int PersonalNumberCheckDigit { get; set; }
        public string FinalCheckDigitSequence { get; set; }
        public int FinalCheckDigit { get; set; }

        private void DecodeString(string mrzLine2)
        {
            // JSG: we need to decode our string here and populate the object properties
            // Our test string value is:    L898902C<3UTO6908061F9406236ZE184226B<<<<<14

            // JSG: Check if string is null or empty
            if (string.IsNullOrEmpty(mrzLine2))
                throw new ArgumentNullException(nameof(mrzLine2), "MrzLine2 is null or empty it should have a value");

            if (mrzLine2.Length != 44)
                throw new ArgumentOutOfRangeException(nameof(mrzLine2),
                    $"MrzLine2 is not of the correct length, expected 44 characters but I got {mrzLine2.Length}");

            // I cant see any string split options here, but I can tell that it's of fixed length and
            // the character positions dont change. Without going too complicated some basic string functions
            // should suffice for extracting the data we need.

            PassportNumber = mrzLine2.Substring(0, 9).Replace("<", string.Empty);
            PassportNumberCheckDigit = int.Parse(mrzLine2.Substring(9, 1));
            Nationality = mrzLine2.Substring(10, 3);
            DateOfBirth = DateTime.ParseExact(mrzLine2.Substring(13, 6), "yyMMdd", CultureInfo.InvariantCulture);
            DateOfBirthCheckDigit = int.Parse(mrzLine2.Substring(19, 1));
            Gender = mrzLine2.Substring(20, 1);
            ExpirationDate = DateTime.ParseExact(mrzLine2.Substring(21, 6), "yyMMdd", CultureInfo.InvariantCulture);
            ExpirationDateCheckDigit = int.Parse(mrzLine2.Substring(27, 1));
            PersonalNumber = mrzLine2.Substring(28, 14);
            PersonalNumberCheckDigit = int.Parse(mrzLine2.Substring(42, 1));
            FinalCheckDigitSequence =
                $"{mrzLine2.Substring(0, 10)}{mrzLine2.Substring(13, 7)}{mrzLine2.Substring(21, 22)}";
            FinalCheckDigit = int.Parse(mrzLine2.Substring(43, 1));
        }
    }
}