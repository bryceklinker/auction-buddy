using System;
using System.Collections.Generic;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Auctions.Exceptions;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions
{
    public class AuctionId : IdentityBase
    {
        public AuctionId(string id = null) 
            : base("auction", id ?? $"{Guid.NewGuid()}")
        {
        }
    }
    
    public class Auction : AggregateRootBase<AuctionId>
    {
        private readonly List<AuctionItem> _items = new List<AuctionItem>();
        public string Name { get; private set; }
        public DateTimeOffset AuctionDate { get; private set; }
        public IReadOnlyCollection<AuctionItem> Items => _items;

        public Auction(string name, in DateTimeOffset auctionDate) 
            : base(new AuctionId())
        {
            Emit(new AuctionCreatedEvent(Id, name, auctionDate));
        }

        public Auction(AuctionId id)
            : base(id)
        {
        }

        public void UpdateAuction(string name = null, in DateTimeOffset? auctionDate = null)
        {
            Emit(new AuctionUpdatedEvent(Id, name, auctionDate));
        }

        public void AddAuctionItem(AuctionItem auctionItem)
        {
            if (HasItemWithName(auctionItem.Name))
                throw new DuplicateAuctionItemException(Id, auctionItem.Name);
            
            Emit(new AuctionItemAddedEvent(Id, auctionItem));
        }

        public void UpdateAuctionItem(string oldName, string newName = null, string newDonor = null, string newDescription = null, int? newQuantity = null)
        {
            if (IsMissingItemWithName(oldName))
                throw new MissingAuctionItemException(Id, oldName);
            
            Emit(new AuctionItemUpdatedEvent(Id, oldName, newName, newDonor, newDescription, newQuantity));
        }

        public void RemoveAuctionItem(string name)
        {
            if (IsMissingItemWithName(name))
                throw new MissingAuctionItemException(Id, name);
            
            Emit(new AuctionItemRemovedEvent(Id, name));
        }

        protected override void Apply(DomainEvent<AuctionId> domainEvent)
        {
            switch (domainEvent)
            {
                case AuctionCreatedEvent createdEvent:
                    Apply(createdEvent);
                    break;
                
                case AuctionUpdatedEvent updatedEvent:
                    Apply(updatedEvent);
                    break;
                
                case AuctionItemAddedEvent itemAddedEvent:
                    Apply(itemAddedEvent);
                    break;
                
                case AuctionItemUpdatedEvent quantityEvent:
                    Apply(quantityEvent);
                    break;
                
                case AuctionItemRemovedEvent removeEvent:
                    Apply(removeEvent);
                    break;
            }
        }

        private void Apply(AuctionCreatedEvent createdEvent)
        {
            Name = createdEvent.Name;
            AuctionDate = createdEvent.AuctionDate;
        }

        private void Apply(AuctionUpdatedEvent updatedEvent)
        {
            Name = updatedEvent.Name ?? Name;
            AuctionDate = updatedEvent.AuctionDate ?? AuctionDate;
        }

        private void Apply(AuctionItemAddedEvent itemAddedEvent)
        {
            _items.Add(itemAddedEvent.Item);
        }

        private void Apply(AuctionItemUpdatedEvent auctionItemUpdatedEvent)
        {
            var item = GetItemByName(auctionItemUpdatedEvent.OldName);
            _items.Remove(item);

            var name = auctionItemUpdatedEvent.NewName ?? item.Name;
            var donor = auctionItemUpdatedEvent.NewDonor ?? item.Donor;
            var description = auctionItemUpdatedEvent.NewDescription ?? item.Description;
            var quantity = auctionItemUpdatedEvent.NewQuantity ?? item.Quantity;
            _items.Add(new AuctionItem(name, donor, description, quantity));
        }

        private void Apply(AuctionItemRemovedEvent removeItem)
        {
            var item = GetItemByName(removeItem.Name);
            _items.Remove(item);
        }

        private bool IsMissingItemWithName(string name)
        {
            return !HasItemWithName(name);
        }

        private bool HasItemWithName(string name)
        {
            return GetItemByName(name) != null;
        }

        private AuctionItem GetItemByName(string name)
        {
            return _items.Find(i => i.Name == name);
        }
    }
}