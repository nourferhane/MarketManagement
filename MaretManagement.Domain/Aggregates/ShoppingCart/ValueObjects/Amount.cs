using System.Runtime.CompilerServices;

namespace MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;

public class Amount : IEquatable<Amount>
{
    private readonly decimal _value;

    private Amount(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Le montant doit être supérieur à 0");
        }

        _value = value;
    }

    public static Amount AmountFor(decimal value) => new(value);

    public bool Equals(Amount? other) =>
        other is not null && other._value == _value;

    public static Amount operator +(Amount? lhs, Amount? rhs)
    {
        if (lhs is null) return rhs ?? new Amount(0);
        if (rhs is null) return lhs ?? new Amount(0);

        return new Amount(lhs._value + rhs._value);
    }

    public static Amount operator -(Amount? lhs, Amount? rhs)
    {
        if (lhs is null) return rhs ?? new Amount(0);
        if (rhs is null) return lhs ?? new Amount(0);

        return new Amount(lhs._value - rhs._value);
    }

    public static Amount operator *(Amount? lhs, Amount? rhs)
    {
        if (lhs is null || rhs is null) return new Amount(0);

        return new Amount(lhs._value * rhs._value);
    }


    public static Amount operator /(Amount? lhs, Amount? rhs)
    {
        if (lhs is null || rhs is null) return new Amount(0);

        if (rhs._value == 0)
            throw new DivideByZeroException("Cannot divide by zero.");

        return new Amount(lhs._value / rhs._value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj);
    }

    public decimal GetValue() => _value;
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}
