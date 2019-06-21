namespace Harvest.Home.Core.Common.Storage
{
    public interface IEntity
    {
        long Id { get; set; }
    }

    public abstract class Entity : IEntity
    {
        public long Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Entity entity)
                return Equals(entity);
            
            return false;
        }

        private bool Equals(Entity other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}