using System.Collections.Generic;

namespace Olimpi.Core.LogSystem.Infrastructure.Filters{
    public class SensitiveDataFilter : ISensitiveDataFiler
    {
        private readonly HashSet<string> _sensitiveWords;

        public SensitiveDataFilter(HashSet<string> sensitiveWords = null){
            _sensitiveWords = sensitiveWords ?? new(){ "password", "apiKey", "secret" };
        }

        public string ApplyCensorship(string message, bool isCensored)
        {
            if (!isCensored) return message;

            foreach(var word in _sensitiveWords){
                message = System.Text.RegularExpressions.Regex.Replace(message,
                    $@"(?<={word}=)[^\s&]+",
                    m => new string('*', m.Length),
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }

            return message;
        }
    }
}