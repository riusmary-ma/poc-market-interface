namespace PocMarketInterface.Matching
{
    public class TryMatching
    {
        private readonly ExactMatch exactMatch = new ExactMatch();
        private readonly CloseMatch closeMatch = new CloseMatch();
        private readonly NoMatch noMatch = new NoMatch();

        public MatchResult CompareInstructionAndAllegement(MatchData matchData)
        {
            var exactMatchResult = exactMatch.CompareInstructionAndAllegement(matchData);
            if (exactMatchResult.MatchData == EMatchResult.ExactMatch)
            {
                UpdateMatchResult(matchData, exactMatchResult);
                return exactMatchResult;
            }

            var closeMatchResult = closeMatch.CompareInstructionAndAllegement(matchData);
            if (closeMatchResult.MatchData == EMatchResult.CloseMatch)
            {
                UpdateMatchResult(matchData, closeMatchResult);
                return closeMatchResult;
            }

            return noMatch.CompareInstructionAndAllegement(matchData);
        }

        public void UpdateMatchResult(MatchData matchData, MatchResult matchResult)
        {
            // Update the match result in the database or any storage mechanism
            // This is a placeholder for the actual implementation
        }
    }
}