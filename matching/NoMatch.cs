namespace PocMarketInterface.Matching
{
public class NoMatch: MatchBase
    {
        public MatchResult CompareInstructionAndAllegement(MatchData matchData)
    {
            return new MatchResult
            {
                MatchId = matchData.MatchId,
                MatchData = EMatchResult.NoMatch
            };
    }
    }
}