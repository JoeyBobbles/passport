using System;
using NUnit.Framework;

namespace UK.Passport.Validation.Tests
{
    [TestFixture]
    public class CharacterConverterTests
    {
        [Test]
        [TestCase('1', 1)]
        [TestCase('5', 5)]
        [TestCase('8', 8)]
        [TestCase('<', 0)]
        [TestCase('A', 10)]
        [TestCase('D', 13)]
        [TestCase('F', 15)]
        [TestCase('U', 30)]
        [TestCase('Z', 35)]
        public void CharacterConverter_TestConversion(char input, int expectedOutput)
        {
            var output = CharacterConverter.ConvertCharacterToInt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void CharacterConverter_TryInvalidCharacter([Values('(', '*', '!', '@')] char input)
        {
            Assert.Throws<FormatException>(() => CharacterConverter.ConvertCharacterToInt(input));
        }
    }
}