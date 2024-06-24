using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FirstWebApp.Models
{
	public class Search
	{
		private readonly ISearchIndexClient indexClient;

		public Search()
		{
			string searchServiceName = "cloudpoe";
			string queryApiKey = "9kHPkX8xEofKjtynyZfZjYaFwmsUtJmwaeUFN1hfe4AzSeD1rpr9";
			string indexName = "azuresql-index";

			SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(queryApiKey));
			indexClient = serviceClient.Indexes.GetClient(indexName);
		}

		public async Task<List<Product>> SearchProductsAsync(string searchText)
		{
			var parameters = new SearchParameters
			{
				//Filter = "Category eq ''",
				Select = new[] {"prodName", "prodCategory", "prodImage", "prodPrice"}
			};

			DocumentSearchResult<Product> results = await indexClient.Documents.SearchAsync<Product>(searchText, parameters);

			return results.Results.Select(r => r.Document).ToList();
		}
	}



	public class Product
    {
        [JsonProperty("prodID")]
        public int ProductID { get; set; }

        [JsonProperty("prodName")]
        public string prodName { get; set; }

        [JsonProperty("prodPrice")]
        public string Price { get; set; }

        [JsonProperty("prodCategory")]
        public string Category { get; set; }

        [JsonProperty("Availability")]
        public string Availability { get; set; }

        [JsonProperty("prodImage")]
        public string ImageURL { get; set; }

        [JsonProperty("IsActive")]
        public int IsActive { get; set; }
    }
}