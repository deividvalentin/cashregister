namespace Domain.Entities
{
    public class ProductWeight : Product
    {
        public decimal WeightPrice { get; private set; }

        public ProductWeight(string name, decimal weightprice) : base(name)
        {
            WeightPrice = weightprice;
        }
    }
}