using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using FirstWebApp.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace FirstWebApp.DurableFunctions
{
    public class StartOrderProcessing
    {
        
            [FunctionName("StartOrderProcessing")]
            public static async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
                [DurableClient] IDurableOrchestrationClient starter)
            {
                var order = await req.ReadFromJsonAsync<productsTable>();

                string instanceId = await starter.StartNewAsync("OrderProcessingOrchestrator", order);
                return starter.CreateCheckStatusResponse(req, instanceId);
            }
        
    }
}
