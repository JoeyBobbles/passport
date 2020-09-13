namespace UK.Passport.Validation
{
    public static class CharacterConverter
    {
        /// <summary>
        ///     Convert the character into it's encoded equivilent
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int ConvertCharacterToInt(char character)
        {
            // JSG: Padding character is zero
            if (character == '<')
                return 0;

            // JSG: Create a string of permissible characters
            const string digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // JSG: Loop through those chars and match them up if we can
            for (var i = 0; i < digits.Length; i++)
                if (digits[i] == character)
                    return i + 10; // Lets add 10 to whatever we match to get the resulting conversion

            // JSG: If we throw here nothing else matched and so the input type was wrong
            // which would fail unit tests without having to write a custom exception
            return int.Parse(character.ToString());
        }
    }
}