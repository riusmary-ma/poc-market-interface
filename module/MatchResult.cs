using PocMarketInterface.Enum;

namespace PocMarketInterface.Module
{
    public class MatchResult
    {
        public long MatchId { get; set; }
        public EMatchResult Result { get; set; }
    }
}