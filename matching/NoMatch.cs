using PocMarketInterface.Enum;
using PocMarketInterface.Module;

namespace PocMarketInterface.Matching
{
    public class NoMatch : MatchBase
    {
        public override MatchResult CompareInstructionAndAllegement(MatchData matchData)
        {
            return new MatchResult
            {
                MatchId = matchData.MatchId,
                Result = EMatchResult.NoMatch
            };
        }
    }
}
