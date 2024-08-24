namespace LoggingSystem.Core.Domain.Interfaces
{
    public interface ISensitiveDataFiler
    {
        public string ApplyCensorship(string message, bool isCensored);
    }
}