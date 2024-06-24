namespace FirstWebApp.Models
{
    public class ProductSearchViewModel
    {
        public string SearchText { get; set; }
		public List<Product> SearchResults { get; set; } = new List<Product>();
	}

}
