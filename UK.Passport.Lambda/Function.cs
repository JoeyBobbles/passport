﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using UK.Passport.Validation;
using UK.Passport.Validation.DTOs;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace UK.Passport.Lambda
{
    public class Function
    {
        private static async Task<List<ValidationResult>> GetPassportValidation()
        {
            return await Validator.ValidateAsync(new PassportLine(), String.Empty);
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            var validationResults = await GetPassportValidation();

            return new APIGatewayProxyResponse
            {
                Body = JsonConvert.SerializeObject(validationResults),
                StatusCode = 200,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}