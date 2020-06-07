using System;
using Domain.Entities;
using Xunit;

namespace UnitTests
{
    public class OrderItemTests
    {
        [Theory]
        [InlineData(new object[] { "Book", 10.0, 5 }, new object[] { "Book", 50.00 })]
        [InlineData(new object[] { "Apple", 2.49, 0.750 }, new object[] { "Apple", 1.8675 })]
        [InlineData(new object[] { "Pen", 3.00, 7 }, new object[] { "Pen", 21.00 })]
        public void Given_OrderCreated_WhenOrderItemGetsQuantity_ThenReturns_CalculatePrice(object[] input, object[] output)
        {
            var orderItem = new OrderItem(new ProductUnit(input[0].ToString(), Convert.ToDecimal(input[1])), Convert.ToDecimal(input[2]));
            
            var expectedName = output[0].ToString();
            var expectedPrice = Convert.ToDecimal(output[1]);

            Assert.Equal(expectedName, orderItem.Product.Name);
            Assert.Equal(expectedPrice, orderItem.Price);
        }
    }
}