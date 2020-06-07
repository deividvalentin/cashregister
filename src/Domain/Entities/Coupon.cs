namespace Domain.Entities
{
    public class Coupon : Discount
    {
        public string Code { get; private set; }
        public decimal ThresholdAmount { get; private set; }
        public decimal DiscountAmount { get; private set; }

        protected Coupon(string name) : base(name)
        {
        }

        public Coupon(string code, string name, decimal thresholdamount, decimal discountamount) : this(name)
        {
            Code = code;
            ThresholdAmount = thresholdamount;
            DiscountAmount = discountamount;
        }
    }
}