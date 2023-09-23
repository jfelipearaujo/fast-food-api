namespace Domain.Abstract
{
    public abstract class Entity<TEntityId> : IEntity
    {
        public TEntityId Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}