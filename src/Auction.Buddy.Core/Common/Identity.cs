namespace Auction.Buddy.Core.Common
{
    public interface Identity
    {
        string Prefix { get; }
        string Id { get; }
    }
    
    public abstract class IdentityBase : Identity
    {
        public string Prefix { get; }
        public string Id { get; }

        protected IdentityBase(string prefix, string id)
        {
            Prefix = prefix;
            Id = id;
        }

        public override string ToString()
        {
            return $"{Prefix}-{Id}";
        }

        protected bool Equals(Identity other)
        {
            return Prefix == other.Prefix && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Identity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Prefix != null ? Prefix.GetHashCode() : 0) * 397) ^ (Id != null ? Id.GetHashCode() : 0);
            }
        }

        public static implicit operator string(IdentityBase identity)
        {
            return identity.ToString();
        }
    }
}