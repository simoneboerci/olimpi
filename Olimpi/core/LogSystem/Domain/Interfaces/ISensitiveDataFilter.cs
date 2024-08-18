namespace Olimpi.Core.LogSystem.Domain.Interfaces{
    public interface ISensitiveDataFiler{
        public string ApplyCensorship(string message, bool isCensored);
    }
}