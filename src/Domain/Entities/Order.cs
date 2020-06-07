using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public decimal Total { get; private set; }
        private readonly IList<OrderItem> _orderItems;
        public IEnumerable<OrderItem> OrderItems => _orderItems;
        private readonly IList<Coupon> _coupons;
        public IEnumerable<Coupon> Coupons => _coupons;
        private readonly IList<Discount> _discounts;
        public IEnumerable<Discount> Discounts => _discounts;

        public Order() : base()
        {
            _orderItems = new List<OrderItem>();
            _coupons = new List<Coupon>();
            _discounts = new List<Discount>();
        }

        public void AddItem(Product product, decimal quantity)
        {
            var orderItem = _orderItems.SingleOrDefault(p => p.Product.Id == product.Id);
            if (orderItem == null)
                _orderItems.Add(new OrderItem(product, quantity));
            else
                orderItem.IncrementQuantity(quantity);

        }

        public void AddCoupon(Coupon coupon)
        {
            if (_coupons.Any(c => c.Code == coupon.Code))
                throw new ArgumentException("Coupon has been redeemed");

            CalculateTotal();

            if (Total < coupon.ThresholdAmount)
                throw new ArgumentException("Coupon cannot be added. Due to Total items is less than Coupon threshold amount");

            _coupons.Add(coupon);
        }


        public void CalculateTotal()
        {
            var totalItems = _orderItems.Sum(i => i.Price);
            var totalDiscount = _coupons.Sum(c => c.DiscountAmount);
            Total = totalItems - totalDiscount;
        }

        public void ApplyBulkDiscount(BulkDiscount bulkDiscount)
        {
            foreach (var item in _orderItems)
            {
                if (item.Product is ProductUnit)
                {
                    var productUnit = (item.Product as ProductUnit);

                    if (bulkDiscount.Product.Id == productUnit.Id && item.Quantity > bulkDiscount.ThresholdQuantity)
                    {
                        var discountAmount = Math.Round((item.Quantity / (bulkDiscount.ThresholdQuantity + bulkDiscount.QuantityOff)) * bulkDiscount.QuantityOff) * productUnit.UnitPrice;
                        item.SetDiscountPrice(discountAmount);
                        _discounts.Add(bulkDiscount);
                    }
                }
            }
        }
    }
}