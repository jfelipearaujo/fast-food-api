namespace Domain.Abstract
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}