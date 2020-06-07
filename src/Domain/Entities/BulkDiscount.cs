namespace Domain.Entities
{
    public class BulkDiscount : Discount
    {
        public Product Product { get; private set; }
        public decimal ThresholdQuantity { get; private set; }
        public decimal QuantityOff { get; private set; }

        private BulkDiscount(string name) : base(name)
        {
        }

        public BulkDiscount(Product product, string name, decimal thresholdQuantity, decimal quantityOff) : this(name)
        {
            Product = product;
            ThresholdQuantity = thresholdQuantity;
            QuantityOff = quantityOff;
        }
    }
}