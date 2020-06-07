using Domain.Entities;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Discount")]
    public class CouponTests
    {
        [Fact]
        public void Given_CouponIsCreated_ThenReturnsBaseClassType()
        {
            var coupon = new Coupon("AAAAA", "$5 OFF When you spend $100 or more", 100, 5);
            Assert.IsAssignableFrom<Discount>(coupon);
        }

        [Fact]
        public void Given_Cupon_WhenCreateCoupon_ThenReturns_CouponObject()
        {
            var coupon = new Coupon("AAAAA", "$5 OFF When you spend $100 or more", 100, 5);

            Assert.Equal("AAAAA", coupon.Code);
            Assert.Equal("$5 OFF When you spend $100 or more", coupon.Name);
            Assert.Equal(100, coupon.ThresholdAmount);
            Assert.Equal(5, coupon.DiscountAmount);
        }
    }
}