using System;

namespace UK.Passport.Validation.DTOs
{
    public class PassportLine
    {
        // User can set these props through the UI
        public string PassportNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PersonalNumber { get; set; }
    }
}