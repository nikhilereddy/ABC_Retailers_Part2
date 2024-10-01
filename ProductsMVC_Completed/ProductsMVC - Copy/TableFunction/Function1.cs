using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TableFunction
{
    public static class Function1
    {
        private static readonly UserService _userService = new UserService("<Your_Azure_Storage_Connection_String>");

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request to create a new user.");

            // Read request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateUserRequest>(requestBody);

            // Validate input
            if (data == null || string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Password))
            {
                return new BadRequestObjectResult("Please provide valid user details including Email and Password.");
            }

            // Hash the password before storing it
            string passwordHash = HashPassword(data.Password);

            // Create the new user object
            var newUser = new User
            {
                CustEmail = data.Email,
                CustPasswordHash = passwordHash,
                PartitionKey = data.Email, // Using email as PartitionKey
                RowKey = Guid.NewGuid().ToString() // Unique identifier for RowKey
            };

            // Call UserService to add the user to Table Storage
            bool result = await _userService.AddCustomerAsync(newUser);

            if (result)
            {
                return new OkObjectResult($"User '{data.Email}' created successfully.");
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); // Server error if failed
            }
        }

        // A simple password hashing function for security (replace with your actual implementation)
        private static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }

    // Model class to deserialize request body
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // The User class represents your user entity in Azure Table Storage
    public class User
    {
        public string CustEmail { get; set; }
        public string CustPasswordHash { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}