using System.Collections.Generic;

namespace IPMatchingApp
{
    public class MatchingMethod
    {
        public const string Random = "random"; 
        public const string Predefined = "predefined"; 
    }

    public interface IIPMatcher
    {
        public List<MatchResult> Match(string filePath, string method);
    }

    public class MatchResult
    {
        public string IPAddress { get; set; }
        public string Country { get; set; }
    }
}
