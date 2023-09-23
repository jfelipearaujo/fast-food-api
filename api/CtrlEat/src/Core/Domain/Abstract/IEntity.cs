namespace Domain.Abstract
{
    public interface IEntity
    {
        DateTime CreatedAtUtc { get; set; }
        DateTime UpdatedAtUtc { get; set; }
    }
}