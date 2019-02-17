namespace CarAdverts.Domain.Core.Models
{
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : struct
    {
        public TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<TKey>;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (ReferenceEquals(null, compareTo))
            {
                return false;
            }

            if (!AreKeysEquals(Id, compareTo.Id))
            {
                return false;
            }

            return AreEquals(this, compareTo);
        }

        protected abstract bool AreKeysEquals(TKey self, TKey other);

        protected abstract bool AreEquals(IEntity<TKey> self, IEntity<TKey> other);

        public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TKey> a, Entity<TKey> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 2049) + Id.GetHashCode();
        }
    }
}
