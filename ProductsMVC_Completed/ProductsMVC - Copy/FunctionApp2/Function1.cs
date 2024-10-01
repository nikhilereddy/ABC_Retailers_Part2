using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp2
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([QueueTrigger("myqueue-items", Connection = "DefaultEndpointsProtocol=https;AccountName=storageaccountst10338305;AccountKey=2xRjWLRR/YvpbWespDTNln+G+ivqnYkWDd/WVAj4l0DSr+btcDNlHfMlcZFl0eHdLS73jUsJyBS5+AStizVRqQ==;EndpointSuffix=core.windows.net")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
