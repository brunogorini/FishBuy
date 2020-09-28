namespace FishBuy.Application.ViewModels.ProductModels
{
    public class ProductViewModel : Validation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileName { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddMessage("Product name was not informed");
            }
            if (string.IsNullOrEmpty(Description))
            {
                AddMessage("Description was not informed");
            }
            if (string.IsNullOrEmpty(Price.ToString()))
            {
                AddMessage("Price was not informed");
            }
        }
    }
}
