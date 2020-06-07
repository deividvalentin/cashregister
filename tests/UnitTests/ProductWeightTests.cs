using Domain.Entities;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Product")]
    public class ProductWeightTests
    {
        [Fact]
        public void ProductWeightIsCreated_ThenCheckBaseClassType()
        {
            var product = new ProductWeight("Apple", 2.49m);
            Assert.IsAssignableFrom<Product>(product);
        }

        [Fact]
        public void ProductWeightIsCreated_ThenCheckProperties()
        {
            var product = new ProductWeight("Grape", 3.99m);
            Assert.Equal("Grape", product.Name);
            Assert.Equal(3.99m, product.WeightPrice);
        }
    }
}