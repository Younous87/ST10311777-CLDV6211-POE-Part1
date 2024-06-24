using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Azure.Identity;
using System;
using System.Collections.Generic;
using System.IO;



namespace FirstWebApp.Controllers
{
	// Define the ProductController class which inherits from the Controller class
	public class ProductController : Controller
	{
		// Instantiate a productsTable object
		public productsTable prodtbl = new productsTable();

		// Declare private readonly fields for logging and HTTP context accessor
		private readonly ILogger<ProductController> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Search searchService;

        // Constructor for ProductController, initializes the logger and HTTP context accessor
        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor; // Assign the HTTP context accessor
            searchService = new Search();
        }

        


        // Action method to handle POST requests for adding a product with an image
        [HttpPost]
		public ActionResult MyWork(productsTable product, IFormFile Image)
		{
			// Check if an image is provided
			if (Image != null && Image.Length > 0)
			{
				try
				{
					// Create a BlobServiceClient to interact with Azure Blob Storage
					var blobServiceClient = new BlobServiceClient(new Uri("https://cldv1.blob.core.windows.net/"), new DefaultAzureCredential());
					var blobContainerClient = blobServiceClient.GetBlobContainerClient("images");

					// Generate a unique name for the image and get a BlobClient
					var blobName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
					var blobClient = blobContainerClient.GetBlobClient(blobName);

					// Set HTTP headers for the blob
					var blobHttpHeaders = new BlobHttpHeaders
					{
						ContentType = Image.ContentType
					};

					// Upload the image to the blob storage
					using (var stream = Image.OpenReadStream())
					{
						blobClient.Upload(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
					}

					// Set the product's image URL and mark it as active
					product.ImageURL = blobClient.Uri.AbsoluteUri;
					product.IsActive = 1;
					// Insert the product into the database
					prodtbl.insert_product(product);

					// Return a JSON response indicating success and the image URL
					return Json(new { success = true, imageUrl = product.ImageURL });
				}
				catch (Exception ex)
				{
					// Return a JSON response indicating an error occurred
					return Json(new { success = false, error = ex.Message });
				}
			}
			else
			{
				// Return a JSON response indicating no image was provided
				return Json(new { success = false, error = "No image provided." });
			}
		}

		// Action method to handle GET requests for viewing the user's work/products
		[HttpGet]
		public ActionResult MyWork()
		{
			// Retrieve the userID from the session
			int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("userID");

			// Check if the user is logged in
			if (userId == null)
			{
				// If not logged in, set a message and redirect to the Login view
				ViewData["Message"] = "Please login to view your cart.";
				return RedirectToAction("Login", "Home");
			}
			else
			{
				// Retrieve all products from the database
				List<productsTable> products = productsTable.GetAllProducts();

				// Pass the products and userID to the view
				ViewData["Products"] = products;
				ViewData["userID"] = userId;

				// Return the view with the products
				return View(products);
			}
		}

		// Action method to handle POST requests for removing a product
		[HttpPost]
		public ActionResult RemoveProduct(int productID)
		{
			// Retrieve the product from the database by productID
			productsTable product = productsTable.GetAllProducts().FirstOrDefault(p => p.ProductID == productID);

			// Check if the product exists
			if (product != null)
			{
				// Mark the product as inactive
				product.IsActive = 0;
				// Update the product in the database
				productsTable.UpdateProduct(product);
			}

			// Redirect to the MyWork action
			return RedirectToAction("MyWork");
		}

		// Action method to handle POST requests for adding a product
		[HttpPost]
		public ActionResult AddProduct(int productID)
		{
			// Retrieve the product from the database by productID
			productsTable product = productsTable.GetAllProducts().FirstOrDefault(p => p.ProductID == productID);

			// Check if the product exists
			if (product != null)
			{
				// Mark the product as active
				product.IsActive = 1;
				// Update the product in the database
				productsTable.UpdateProduct(product);
			}

			// Redirect to the MyWork action
			return RedirectToAction("MyWork");
		}

		// Action method to handle POST requests for updating product availability
		[HttpPost]
		public ActionResult UpdateAvailability(int productID, string availability)
		{
			// Update the product's availability in the database
			int rowsAffected = prodtbl.update_availability(productID, availability);
			// Redirect to the MyWork action
			return RedirectToAction("MyWork");
		}

		// Action method to handle POST requests for updating product price
		[HttpPost]
		public ActionResult UpdatePrice(int productID, string price)
		{
			// Update the product's price in the database
			int rowsAffected = prodtbl.update_price(productID, price);
			// Redirect to the MyWork action
			return RedirectToAction("MyWork");
		}

        

        [HttpGet]
        public ActionResult Search()
        {
            var model = new ProductSearchViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Search(ProductSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Log to ensure this part of code is reached
                _logger.LogInformation("Search initiated for: " + model.SearchText);

                model.SearchResults = await searchService.SearchProductsAsync(model.SearchText);
                return View(model);
			}
            else
            {
                // Log invalid model state
                _logger.LogWarning("Invalid model state");
            }

            foreach (var modelStateEntry in ModelState.Values)
            {
	            foreach (var error in modelStateEntry.Errors)
	            {
		            _logger.LogWarning($"Error: {error.ErrorMessage}");
	            }
            }
            model.SearchResults = null;
			return View(model);
        }

    }

}
