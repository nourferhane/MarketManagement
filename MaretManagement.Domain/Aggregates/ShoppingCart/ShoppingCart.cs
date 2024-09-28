using MaretManagement.Domain.Abstraction;
using MaretManagement.Domain.Aggregates.ShoppingCart.Entities;
using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;
using MaretManagement.Domain.Specifications.DiscountRules;

namespace MaretManagement.Domain.Aggregates.ShoppingCart;

public class ShoppingCart : IAggregate
{
    private IEnumerable<IPricingDiscountRule> _discountRules = [];


    public int Id { get; private set; } // gére par persistance
    public ICollection<CartItem> CartItems { get; private set; } = [];
    private Amount DiscountAmount = Amount.AmountFor(0);
    public void AddDiscountRules(IEnumerable<IPricingDiscountRule> pricingDiscountRules)
    {
        _discountRules = pricingDiscountRules;
    }

    /// <remarks>
    /// TODO : ajouter validation si le total des discount > total faut que l'aggregat valide cette logique => voir avec le métier
    /// </remarks>
    public decimal GetDiscountsAmount()
    {
        Amount discountAmount = Amount.AmountFor(0);
        foreach (var discount in _discountRules)
        {
            discountAmount += discount.GetDiscountAmount(this);
        }
        return discountAmount.GetValue();
    }

    public decimal GetTotalBeforeDiscount()
    {
        return CartItems.Sum(x => x.GetTotalPrice().GetValue());
    }

    public void AddProductToCart(Product.Product product, int quantity = 1)
    {
        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);
        if (existingItem == null)
        {
            CartItems.Add(new CartItem(product.Price, Quantity.QuantityFor(quantity), product.Id));
            return;
        }
        existingItem.UpdateQuantity(existingItem.Quantity.AddQuantity(quantity));
    }


    public int GetProductQuantity(int productId)
    {
        var cartItem = CartItems.FirstOrDefault(x => x.ProductId == productId);
        return cartItem == null ? 0 : cartItem.Quantity.GetValue();
    }
}
