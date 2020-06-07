namespace Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal DiscountPrice { get; private set; }

        protected OrderItem() : base()
        {
        }

        public OrderItem(Product product, decimal quantity) : this()
        {
            Product = product;
            Quantity = quantity;
            Calculate();
        }

        private void Calculate()
        {
            var productPrice = (Product as ProductUnit)?.UnitPrice ?? (Product as ProductWeight).WeightPrice;
            Price = Quantity * productPrice - DiscountPrice;
        }

        public void SetDiscountPrice(decimal price)
        {
            DiscountPrice = price;
            Calculate();
        }

        public void IncrementQuantity(decimal quantity) {
            Quantity += quantity;
            Calculate();
        }
    }
}