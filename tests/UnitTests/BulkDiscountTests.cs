using Domain.Entities;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Discount")]
    public class BulkDiscountTests
    {
        [Fact]
        public void Given_BulkDiscountIsCreated_ThenReturnsBaseClassType()
        {
            var product = new ProductUnit("Book", 10.0m);
            var bulkDiscount = new BulkDiscount(product, "Buy 2 gets 1 free", 2, 1);
            Assert.IsAssignableFrom<Discount>(bulkDiscount);
        }

        [Fact]
        public void Given_BulkDiscount_WhenCreateBulkDiscount_ThenReturns_BulkDiscountObject()
        {
            var product = new ProductUnit("Book", 10.0m);
            var bulkDiscount = new BulkDiscount(product, "Buy 2 gets 1 free", 2, 1);

            Assert.NotNull(bulkDiscount.Product);
            Assert.Equal("Buy 2 gets 1 free", bulkDiscount.Name);
            Assert.Equal(2, bulkDiscount.ThresholdQuantity);
            Assert.Equal(1, bulkDiscount.QuantityOff);
        }
    }
}