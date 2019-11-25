using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Auction.Buddy.Core.Common
{
    public abstract class ValueObject
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValueObject) obj);
        }

        protected bool Equals(ValueObject other)
        {
            return GetProperties()
                .All(p => p.GetValue(this).Equals(p.GetValue(other)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetProperties()
                    .Select(property => property.GetValue(this))
                    .Where(value => value != null)
                    .Aggregate(0, (current, value) => current ^ value.GetHashCode() * 397);
            }
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return GetType().GetProperties(BindingFlags.GetProperty);
        }
    }
}