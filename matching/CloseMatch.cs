namespace PocMarketInterface.Matching
{
    public class CloseMatch: MatchBase
    {
        public MatchResult CompareInstructionAndAllegement(MatchData matchData)
    {
        var instruction = matchData.Data.Item1;
        var allegement = matchData.Data.Item2;

        if (instruction == null || allegement == null ||
         instruction.IsCancelled  || instruction.IsSettled || instruction.IsMatched)
        {
            return new MatchResult
            {
                MatchId = matchData.MatchId,
                MatchData = EMatchResult.NoMatch
            };
        }

        int matchResult = CompareInstructionAndAllegementForExactOrCloseMatch(instruction, allegement);

        return new MatchResult
        {
            MatchId = matchData.MatchId,
            MatchData = matchResult == 2 ? EMatchResult.CloseMatch : EMatchResult.NoMatch
        };
    }

    public override int CompareInstructionAndAllegementForExactOrCloseMatch(Instruction instruction, Allegement allegement)
    {
        bool isExactOrCloseMatch = instruction.MarketCode == allegement.MarketCode &&
                            instruction.Price == allegement.Price &&
                            instruction.Quantity == allegement.Quantity &&
                            instruction.Amount != allegement.Amount;

        return isExactOrCloseMatch ? 2 : 3; // Return 2 for close match, 3 for no match
    }
    }
}