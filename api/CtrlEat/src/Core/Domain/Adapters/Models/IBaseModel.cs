namespace Domain.Adapters.Models
{
    public interface IBaseModel
    {
        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}
