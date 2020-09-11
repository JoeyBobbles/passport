﻿namespace UK.Passport.Validation.DTOs
{
    public class ValidationResult
    {
        public ValidationResult(string id, bool result)
        {
            Id = id;
            Result = result;
        }
        
        public string Id { get; set; }
        public bool Result { get; set; }
    }
}