namespace Domain.ValueObjects.Extensions
{
    public static class ValueObjectExtensions
    {
        public static bool HasData(this DocumentId valueObject)
        {
            return !string.IsNullOrEmpty(valueObject?.Value);
        }

        public static bool HasData(this Email valueObject)
        {
            return !string.IsNullOrEmpty(valueObject?.Value);
        }

        public static bool HasData(this Name valueObject)
        {
            return !string.IsNullOrEmpty(valueObject?.Value);
        }
    }
}
