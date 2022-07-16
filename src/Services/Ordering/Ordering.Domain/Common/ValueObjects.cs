namespace Ordering.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponent();
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }
            return left?.Equals(right) != false;
        }
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) { return false; }
            var other = (ValueObject)obj;
            return GetEqualityComponent().SequenceEqual(other.GetEqualityComponent());
        }
        public override int GetHashCode()
        {
            return GetEqualityComponent().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
        }
    }
}
