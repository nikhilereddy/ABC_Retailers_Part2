using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp2
{
    public class Function
    {
        [FunctionName("Function")]
        public void Run([BlobTrigger("samples-workitems/{name}", Connection = "DefaultEndpointsProtocol=https;AccountName=storageaccountst10338305;AccountKey=2xRjWLRR/YvpbWespDTNln+G+ivqnYkWDd/WVAj4l0DSr+btcDNlHfMlcZFl0eHdLS73jUsJyBS5+AStizVRqQ==;EndpointSuffix=core.windows.net")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
