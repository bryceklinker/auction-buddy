namespace Auction.Buddy.Core.Common.Entities
{
    public interface IEntity
    {
        int Id { get; }
    }
    
    public abstract class Entity : IEntity 
    {
        public int Id { get; internal set; }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Entity);
        }

        protected bool Equals(Entity other)
        {
            return Id == other?.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}