using MaretManagement.Domain.Aggregates.Product;
using MaretManagement.Domain.Aggregates.ShoppingCart;
using MaretManagement.Domain.Specifications.DiscountRules;

namespace MarketManagement.Domain.UnitTests.ShoppingCartTests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void ShoppingCarteAddProductToCart_WhenEmptyCart_ShouldInitCartItem()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            var expectedValue = 5;
            // Act 
            cart.AddProductToCart(productToadd, 5);

            // Assert
            Assert.Equal(expectedValue, cart.GetProductQuantity(1));
        }

        [Fact]
        public void ShoppingCarteAddProductToCart_WhenCartAlreaydContainsProduct_ShouldUpdateQuantity()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            var expectedValue = 10;
            // Act 
            cart.AddProductToCart(productToadd, 5);
            cart.AddProductToCart(productToadd, 5);

            // Assert
            Assert.Equal(expectedValue, cart.GetProductQuantity(1));
        }

        [Fact]
        public void ShoppingCarteAddProductToCart_WhenCartDoesntContainsProduct_ShouldAddNEwCartITem()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            Product productToadd2 = new(2, "Test2", 10);
            var expectedValue = 5;
            var expectedtotalCount = 2;

            // Act 
            cart.AddProductToCart(productToadd, 5);
            cart.AddProductToCart(productToadd2, 5);

            // Assert
            Assert.Equal(expectedValue, cart.GetProductQuantity(1));
            Assert.Equal(expectedtotalCount, cart.CartItems.Count);
        }

        [Fact]
        public void ShoppingCarteApplyDiscount_WhenByeXGetYFreeDiscountIsAvailable_ShouldApplyDiscount()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            Product productToadd2 = new(2, "Test2", 5);
            cart.AddProductToCart(productToadd, 10);
            cart.AddProductToCart(productToadd2, 1);
            cart.AddDiscountRules([new BuyXGetYFree(productToadd, 10, 1)]);

            var expectedDiscountValue = 10;
            var expectedCartAmoutBeforediscount = 105;
            var expectedCartAmoutAfterediscount = 95;
            // Act
            var cartTotal = cart.GetTotalBeforeDiscount();
            var discountAmount = cart.GetDiscountsAmount();
            var totalAfterDiscount = cart.GetTotalBeforeDiscount() - cart.GetDiscountsAmount();
            // Assert
            Assert.Equal(expectedCartAmoutBeforediscount, cartTotal);
            Assert.Equal(expectedDiscountValue, discountAmount);
            Assert.Equal(expectedCartAmoutAfterediscount, totalAfterDiscount);
        }

        [Fact]
        public void ShoppingCarteApplyDiscount_WhenByeXGetYFreeDiscountIsAvailableAndConditionNotMet_ShouldNotApplyDiscount()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            Product productToadd2 = new(2, "Test2", 5);
            cart.AddProductToCart(productToadd, 1);
            cart.AddProductToCart(productToadd2, 1);
            cart.AddDiscountRules([new BuyXGetYFree(productToadd, 10, 1)]);

            var expectedDiscountValue = 0;
            var expectedCartAmoutBeforediscount = 15;
            var expectedCartAmoutAfterediscount = 15;
            // Act
            var cartTotal = cart.GetTotalBeforeDiscount();
            var discountAmount = cart.GetDiscountsAmount();
            var totalAfterDiscount = cart.GetTotalBeforeDiscount() - cart.GetDiscountsAmount();
            // Assert
            Assert.Equal(expectedCartAmoutBeforediscount, cartTotal);
            Assert.Equal(expectedDiscountValue, discountAmount);
            Assert.Equal(expectedCartAmoutAfterediscount, totalAfterDiscount);
        }

        [Fact]
        public void ShoppingCarteApplyDiscount_WhenMultipleDiscountAppliedOnSingleProduct_ShouldApplyallDiscounts()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            Product productToadd2 = new(2, "Test2", 5);
            cart.AddProductToCart(productToadd, 10);
            cart.AddProductToCart(productToadd2, 1);
            cart.AddDiscountRules([new BuyXGetYFree(productToadd, 10, 1), new BundleOneEuroDiscount()]);

            var expectedDiscountValue = 11;
            var expectedCartAmoutBeforediscount = 105;
            var expectedCartAmoutAfterediscount = 94;
            // Act
            var cartTotal = cart.GetTotalBeforeDiscount();
            var discountAmount = cart.GetDiscountsAmount();
            var totalAfterDiscount = cart.GetTotalBeforeDiscount() - cart.GetDiscountsAmount();
            // Assert
            Assert.Equal(expectedCartAmoutBeforediscount, cartTotal);
            Assert.Equal(expectedDiscountValue, discountAmount);
            Assert.Equal(expectedCartAmoutAfterediscount, totalAfterDiscount);
        }

        [Fact]
        public void ShoppingCarteApplyDiscount_WhenMultipleDiscountAppliedOnMultipleProducts_ShouldApplyallDiscounts()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            Product productToadd2 = new(2, "Test2", 5);
            cart.AddProductToCart(productToadd, 10);
            cart.AddProductToCart(productToadd2, 10);
            cart.AddDiscountRules([new BuyXGetYFree(productToadd, 10, 1), new BundleOneEuroDiscount()]);

            var expectedDiscountValue = 12;
            var expectedCartAmoutBeforediscount = 150;
            var expectedCartAmoutAfterediscount = 138;
            // Act
            var cartTotal = cart.GetTotalBeforeDiscount();
            var discountAmount = cart.GetDiscountsAmount();
            var totalAfterDiscount = cart.GetTotalBeforeDiscount() - cart.GetDiscountsAmount();
            // Assert
            Assert.Equal(expectedCartAmoutBeforediscount, cartTotal);
            Assert.Equal(expectedDiscountValue, discountAmount);
            Assert.Equal(expectedCartAmoutAfterediscount, totalAfterDiscount);
        }


        [Fact]
        public void ShoppingCarteApplyDiscount_WhenMultipleBundleDiscount_ShouldApplDiscounts()
        {
            // Arrange
            ShoppingCart cart = new();
            Product productToadd = new(1, "Test", 10);
            cart.AddProductToCart(productToadd, 20);
            cart.AddDiscountRules([new BundleOneEuroDiscount()]);

            var expectedDiscountValue = 2;
            var expectedCartAmoutBeforediscount = 200;
            var expectedCartAmoutAfterediscount = 198;
            // Act
            var cartTotal = cart.GetTotalBeforeDiscount();
            var discountAmount = cart.GetDiscountsAmount();
            var totalAfterDiscount = cart.GetTotalBeforeDiscount() - cart.GetDiscountsAmount();
            // Assert
            Assert.Equal(expectedCartAmoutBeforediscount, cartTotal);
            Assert.Equal(expectedDiscountValue, discountAmount);
            Assert.Equal(expectedCartAmoutAfterediscount, totalAfterDiscount);
        }
    }
}
