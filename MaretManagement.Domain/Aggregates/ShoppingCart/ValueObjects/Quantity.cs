namespace MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects
{
    public class Quantity : IEquatable<Quantity>
    {
        private readonly int _value;

        public Quantity(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("La quantité doit être supérieur strictement à 0");
            }
            _value = value;
        }

        public static Quantity QuantityFor(int value) => new Quantity(value);

        public int GetValue() => _value;
        public bool Equals(Quantity? other)
        {
            return other is not null && other._value == _value;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj);
        }

        internal Quantity AddQuantity(int value) =>
            new(value + _value);

        public override int GetHashCode()
        {
            return _value;
        }
    }
}
