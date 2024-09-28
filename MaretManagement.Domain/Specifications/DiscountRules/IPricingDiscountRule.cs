using MaretManagement.Domain.Aggregates.Product;
using MaretManagement.Domain.Aggregates.ShoppingCart;
using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;

namespace MaretManagement.Domain.Specifications.DiscountRules;

public interface IPricingDiscountRule
{
    Amount GetDiscountAmount(ShoppingCart shoppingCart);
}
