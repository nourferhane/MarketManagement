using MaretManagement.Domain.Aggregates.Product;
using MaretManagement.Domain.Aggregates.ShoppingCart;
using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;

namespace MaretManagement.Domain.Specifications.DiscountRules;

public class BuyXGetYFree : IPricingDiscountRule
{
    private readonly Product _product;
    private readonly int _paidQuantity;
    private readonly int _freeQuantity;

    public BuyXGetYFree(Product product, int paidQuantity = 10, int freeQuantity = 1)
    {
        _product = product;
        _paidQuantity = paidQuantity;
        _freeQuantity = freeQuantity;
    }

    public Amount GetDiscountAmount(ShoppingCart shoppingCart)
    {
        var totalProductuantity = shoppingCart.GetProductQuantity(_product.Id);
        if (totalProductuantity >= _paidQuantity)
        {
            return _product.Price;
        }
        return Amount.AmountFor(0);
    }
}