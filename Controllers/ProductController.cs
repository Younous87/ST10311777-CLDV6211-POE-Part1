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
    public class ProductController : Controller
    {
        public productsTable prodtbl = new productsTable();

        private readonly ILogger<ProductController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor)
        {
	        _logger = logger;
	        _httpContextAccessor = httpContextAccessor; // Initialize IHttpContextAccessor
        }


		[HttpPost]
		public ActionResult MyWork(productsTable product, IFormFile Image)
		{
			if (Image != null && Image.Length > 0)
			{
				try
				{
					var blobServiceClient = new BlobServiceClient(new Uri("https://cldv1.blob.core.windows.net/"), new DefaultAzureCredential());
					var blobContainerClient = blobServiceClient.GetBlobContainerClient("images");

					var blobName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
					var blobClient = blobContainerClient.GetBlobClient(blobName);

					var blobHttpHeaders = new BlobHttpHeaders
					{
						ContentType = Image.ContentType
					};

					using (var stream = Image.OpenReadStream())
					{
						blobClient.Upload(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
					}

					product.ImageURL = blobClient.Uri.AbsoluteUri;
					product.IsActive = 1;
					prodtbl.insert_product(product);


					return Json(new { success = true, imageUrl = product.ImageURL });
				}
				catch (Exception ex)
				{
					return Json(new { success = false, error = ex.Message });
				}
			}
			else
			{
				return Json(new { success = false, error = "No image provided." });
			}
		}

		[HttpGet]
        public ActionResult MyWork()
        {

			int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("userID");

			if (userId == null)
			{
				// User is not logged in, display a message
				ViewData["Message"] = "Please login to view your cart.";
				return RedirectToAction("Login", "Home");
			}
			else
			{
				// Retrieve all products from the database
				List<productsTable> products = productsTable.GetAllProducts();



				// Pass products and userID to the view
				ViewData["Products"] = products;
				ViewData["userID"] = userId;

				return View(products);
			}
		}

		[HttpPost]
		public ActionResult RemoveProduct(int productID)
		{
			// Retrieve the product from the database
			productsTable product = productsTable.GetAllProducts().FirstOrDefault(p => p.ProductID == productID);

			if (product != null)
			{
				// Set IsActive to 0 to "remove" the product
				product.IsActive = 0;
				// Update the product in the database
				productsTable.UpdateProduct(product);
			}

			return RedirectToAction("MyWork");
		}

		[HttpPost]
		public ActionResult AddProduct(int productID)
		{
			// Retrieve the product from the database
			productsTable product = productsTable.GetAllProducts().FirstOrDefault(p => p.ProductID == productID);

			if (product != null)
			{
				// Set IsActive to 0 to "remove" the product
				product.IsActive = 1;
				// Update the product in the database
				productsTable.UpdateProduct(product);
			}

			return RedirectToAction("MyWork");
		}

		[HttpPost]
		public ActionResult UpdateAvailability(int productID, string availability)
		{
			int rowsAffected = prodtbl.update_availability(productID, availability);
			return RedirectToAction("MyWork");
		}

		[HttpPost]
		public ActionResult UpdatePrice(int productID, string price)
		{
			int rowsAffected = prodtbl.update_price(productID, price);
			return RedirectToAction("MyWork");
		}


	}
}
