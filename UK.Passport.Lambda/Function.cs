using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using UK.Passport.Validation;
using UK.Passport.DTO;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace UK.Passport.Lambda
{
    public class Function
    {
        private static List<ValidationResult> GetPassportValidation(DTO.Passport passport)
        {
            return Validator.Validate(passport);
        }

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            if(string.IsNullOrEmpty(apigProxyEvent.Body))
                throw new ArgumentNullException("body", "body of message is null or empty, nothing to parse");

            var passportObject = JsonConvert.DeserializeObject<DTO.Passport>(apigProxyEvent.Body);

            var validationResults = GetPassportValidation(passportObject);

            return new APIGatewayProxyResponse
            {
                Body = JsonConvert.SerializeObject(validationResults),
                StatusCode = 200,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}