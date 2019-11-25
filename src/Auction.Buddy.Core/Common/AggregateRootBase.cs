using System.Collections.Generic;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Common
{
    public interface AggregateRoot<out TId>
        where TId : Identity
    {
        TId Id { get; }
        IEnumerable<DomainEvent<TId>> Changes { get; }

        void Commit();
    }
    
    public abstract class AggregateRootBase<TId> : AggregateRoot<TId>
        where TId : Identity
    {
        private readonly List<DomainEvent<TId>> _changes;
        public TId Id { get; }

        public IEnumerable<DomainEvent<TId>> Changes => _changes;

        protected AggregateRootBase(TId id)
        {
            Id = id;
            _changes = new List<DomainEvent<TId>>();
        }

        protected void Emit(DomainEvent<TId> domainEvent)
        {
            _changes.Add(domainEvent);
            Apply(domainEvent);
        }

        public void Commit()
        {
            _changes.Clear();
        }
        
        protected bool Equals(AggregateRootBase<TId> other)
        {
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AggregateRootBase<TId>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        protected abstract void Apply(DomainEvent<TId> domainEvent);
    }
}