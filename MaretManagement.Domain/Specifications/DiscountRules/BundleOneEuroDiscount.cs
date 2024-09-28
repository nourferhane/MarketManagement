using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;
using MaretManagement.Domain.Aggregates.ShoppingCart;

namespace MaretManagement.Domain.Specifications.DiscountRules;

public class BundleOneEuroDiscount : IPricingDiscountRule
{
    private readonly int _minimumQuantity;

    public BundleOneEuroDiscount(int minimumQuantity = 10)
    {
        _minimumQuantity=minimumQuantity;
    }

    public Amount GetDiscountAmount(ShoppingCart shoppingCart)
    {
        var totalSumDiscount = 0;
        foreach(var item in  shoppingCart.CartItems)
        {
            var productQuantity = shoppingCart.GetProductQuantity(item.ProductId);
            if(productQuantity >= _minimumQuantity)
            {
                totalSumDiscount +=1;
            }
        }
        return Amount.AmountFor(totalSumDiscount);
    }
}
