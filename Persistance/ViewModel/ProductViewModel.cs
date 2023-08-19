namespace CustomAPI.ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string imagePath { get; set; }
        public List<ProductDetailsViewModel> productDetails { get; set; }

        

    }
}
