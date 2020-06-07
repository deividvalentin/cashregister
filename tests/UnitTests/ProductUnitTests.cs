using Domain.Entities;
using Xunit;
using System;

namespace UnitTests
{
    [Trait("Category", "Product")]
    public class ProductUnitTests
    {
        [Fact]
        public void Given_ProductUnitIsCreated_ThenReturnsBaseClassType()
        {
            var product = new ProductUnit("Book", 10);
            Assert.IsAssignableFrom<Product>(product);
        }

        [Fact]
        public void CreatedProductUnit_CheckProperties()
        {
            var product = new ProductUnit("Book", 10);
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal("Book", product.Name);
            Assert.Equal(10, product.UnitPrice);
        }
    }
}