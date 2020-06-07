using System;
using System.Linq;
using Domain.Entities;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Order")]
    public class OrderTests
    {
        private readonly Order _order;

        public OrderTests()
        {
            _order = new Order();
        }

        [Fact]
        public void Given_OrderCreated_WhenAddItem_ThenCheck_OrderItemsAreNotEmpty()
        {
            var product = new ProductUnit("Book", 10.0m);

            _order.AddItem(product, 10.0m);

            Assert.NotEmpty(_order.OrderItems);
        }

        [Fact]
        public void Given_Order_WhenAddItemByUnit_ThenReturns_OrderItemsQuantity()
        {
            var product = new ProductUnit("Book", 10.0m);

            _order.AddItem(product, 5);

            Assert.Single(_order.OrderItems);
        }

        [Fact]
        public void Given_Order_WhenAddItemByWeight_ThenReturns_OrderItemsQuantity()
        {
            var product = new ProductWeight("Apple", 5.0m);

            _order.AddItem(product, 0.800m);

            Assert.Single(_order.OrderItems);
        }


        [Theory]
        [InlineData(new object[] {"Apple", 5.99, 0.800}, 4.792)]
        [InlineData(new object[] {"Grape", 3.99, 0.500}, 1.995)]
        [InlineData(new object[] {"Banana", 0.89, 1.100}, 0.979)]
        public void Given_Order_WhenAddProductByWeight_ThenReturns_TotalOrder(object[] input, decimal totalOrderExpected)
        {
            var product = new ProductUnit(input[0].ToString(), Convert.ToDecimal(input[1]));

            _order.AddItem(product, Convert.ToDecimal(input[2]));
            
            _order.CalculateTotal();

            Assert.Equal(totalOrderExpected, _order.Total);
        }

        [Theory]
        [InlineData(new object[] {"Apple", 5.99, 0.800}, 4.792)]
        [InlineData(new object[] {"Book", 10.99, 10}, 109.9)]
        [InlineData(new object[] {"Banana", 0.89, 1.100}, 0.979)]
        [InlineData(new object[] {"Pen", 1.50, 5}, 7.5)]
        public void Given_Order_WhenAddMixProduct_ThenReturns_TotalOrder(object[] input, decimal totalOrderExpected)
        {
            var product = new ProductUnit(input[0].ToString(), Convert.ToDecimal(input[1]));

            _order.AddItem(product, Convert.ToDecimal(input[2]));
            
            _order.CalculateTotal();

            Assert.Equal(totalOrderExpected, _order.Total);
        }

        [Fact]
        public void Given_Order_WhenAddCoupon_ThrowsException_IfTotalIsNotGreater_Than_CouponThresholdAmount()
        {
            var coupon = new Coupon("BBFFFG2020", "$10 off when you spend $200 or more", 200.00m, 10.00m);

            var ex = Assert.Throws<ArgumentException>( () => _order.AddCoupon(coupon));

            Assert.Equal("Coupon cannot be added. Due to Total items is less than Coupon threshold amount", ex.Message);
        }

        [Fact]
        public void Given_Order_WhenAddCoupon_ThrowsException_IfCouponHasBeenRedeemed()
        {
            var product = new ProductUnit("Book", 10.0m);
            var coupon = new Coupon("BBFFFG2020", "$10 off when you spend $100 or more", 100.00m, 10.00m);
            
            _order.AddItem(product, 15);
            
            var ex = Assert.Throws<ArgumentException>( () => {
                _order.AddCoupon(coupon);
                _order.AddCoupon(coupon);
            });

            Assert.Equal("Coupon has been redeemed", ex.Message);
        }

        [Fact]
        public void Given_Order_WhenAddCoupon_ThenRetuns_TotalOrderWithDiscount()
        {
            var product = new ProductUnit("Book", 10.0m);
            var coupon = new Coupon("AASDF", "$5 off when you spend $100 or more", 100.00m, 5.00m);
            _order.AddItem(product, 15);
            _order.AddCoupon(coupon);
            _order.CalculateTotal();

            Assert.Equal(145.00m, _order.Total);
        }


        [Fact]
        public void Given_Order_When_Buy2_Get1_Free_ThenReturns_CalculateTotalOrder()
        {
            var product = new ProductUnit("Book", 10.0m);
            _order.AddItem(product, 3);
            _order.ApplyBulkDiscount(new BulkDiscount(product, "Buy 2 gets 1 free", 2, 1));
            _order.CalculateTotal();

            Assert.Equal(20.00m, _order.Total);
        }

        [Fact]
        public void Given_Order_When_Buy4_Get1_Free_ThenReturns_CalculateTotalOrder()
        {
            var product = new ProductUnit("Book", 10.0m);
            _order.AddItem(product, 7);
            _order.ApplyBulkDiscount(new BulkDiscount(product, "Buy 4 gets 1 free", 4, 1));
            _order.CalculateTotal();

            Assert.Equal(60.00m, _order.Total);
        }

        [Fact]
        public void Given_Order_When_AddMultipleItems_ThenApply_Buy2_Get1_Free_Discount_ThenReturns_CalculateTotalOrder()
        {
            var product = new ProductUnit("Book", 10.0m);
            _order.AddItem(product, 1);
            _order.AddItem(product, 1);
            _order.AddItem(product, 1);
            _order.AddItem(product, 1);
            _order.AddItem(product, 1);
            _order.AddItem(product, 1);
            _order.ApplyBulkDiscount(new BulkDiscount(product, "Buy 2 gets 1 free", 2, 1));
            _order.CalculateTotal();

            Assert.Equal(40.00m, _order.Total);
        }

        [Fact]
        public void Given_Order_When_AddDiscount_To_ProductWeight_ThenDoesNotApplyDiscount()
        {
            var productUnit = new ProductUnit("Book", 10.0m);
            var productWeight = new ProductWeight("Apple", 2.49m);
            _order.AddItem(productUnit, 1);
            _order.AddItem(productUnit, 1);
            _order.AddItem(productWeight, 1);
            _order.AddItem(productWeight, 1);
            _order.AddItem(productWeight, 1);
            _order.AddItem(productUnit, 1);
            _order.ApplyBulkDiscount(new BulkDiscount(productWeight, "Buy 2 gets 1 free", 2, 1));
            
            _order.CalculateTotal();

            Assert.Equal(37.47m, _order.Total);
        }

        [Fact]
        public void Given_Order_When_AddDiscount_ThenCheck_DiscountIsAdded()
        {
            var product = new ProductUnit("Book", 10.0m);
            _order.AddItem(product, 6);
            _order.AddItem(product, 6);
            _order.ApplyBulkDiscount(new BulkDiscount(product, "Buy 2 gets 1 free", 2, 1));
            var bulkDiscount = _order.Discounts.FirstOrDefault(d => d.Name == "Buy 2 gets 1 free");
            
            Assert.NotNull(bulkDiscount);
        }
    }
}
