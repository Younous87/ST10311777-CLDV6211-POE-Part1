using Microsoft.AspNetCore.Components;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;
using FirstWebApp.Models;

namespace FirstWebApp.DurableFunctions
{
    public static class OrderProcessing
    {
        [Inject]
        private static IProductRepository _productRepository { get; set; }

        [FunctionName("OrderProcessingOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var order = context.GetInput<productsTable>();

            try
            {
                if (!await context.CallActivityAsync<bool>("CheckInventory", order.ProductID))
                {
                    return;
                }
                
                await context.CallActivityAsync("UpdateInventory", new { ProductId = order.ProductID, NewAvailability = "Not Available" });

                await context.CallActivityAsync("SendConfirmation", order);
            }
            catch (Exception ex)
            {
            }
        }

        [FunctionName("CheckInventory")]
        public static bool CheckInventory([ActivityTrigger] int productId)
        {
            return _productRepository.IsProductAvailable(productId); 
        }

        [FunctionName("UpdateInventory")]
        public static void UpdateInventory([ActivityTrigger] dynamic input)
        {
            _productRepository.UpdateProductAvailability(input.ProductId, input.NewAvailability); 
        }
    }

    public interface IProductRepository
    {
        bool IsProductAvailable(int productId); 
        void UpdateProductAvailability(int productId, string newAvailability);
    }
}
