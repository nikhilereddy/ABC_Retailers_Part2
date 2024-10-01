using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure;

namespace AddUser
{
    public static class AddUserFunction
    {
        private readonly TableClient _userTableClient;

        public AddCustomerFunction(string connectionString)
        {
            _userTableClient = new TableClient(connectionString, "Users");
            _userTableClient.CreateIfNotExists();
        }

        [FunctionName("AddCustomer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "addcustomer")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing Add Customer request.");

            // Read request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var customer = JsonConvert.DeserializeObject<User>(requestBody);

            if (customer == null || string.IsNullOrEmpty(customer.CustEmail))
            {
                return new BadRequestObjectResult("Invalid customer data.");
            }

            if (await FindCustomerByEmailAsync(customer.CustEmail))
            {
                return new ConflictObjectResult("Customer already exists.");
            }

            customer.PartitionKey = customer.CustEmail;
            customer.RowKey = Guid.NewGuid().ToString();

            try
            {
                await _userTableClient.AddEntityAsync(customer);
                return new OkObjectResult("Customer added successfully.");
            }
            catch (RequestFailedException ex)
            {
                log.LogError($"Error adding customer: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        private async Task<bool> FindCustomerByEmailAsync(string email)
        {
            var query = _userTableClient.QueryAsync<User>(filter: $"PartitionKey eq '{email}'");
            await foreach (var customer in query)
            {
                return true;
            }
            return false;
        }
    }
}