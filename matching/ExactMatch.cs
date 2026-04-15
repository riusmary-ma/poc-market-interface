using PocMarketInterface.Enum;
using PocMarketInterface.Module;

namespace PocMarketInterface.Matching
{
    public class ExactMatch : MatchBase
    {
        public override MatchResult CompareInstructionAndAllegement(MatchData matchData)
        {
            var instruction = matchData.Data.Item1;
            var allegement = matchData.Data.Item2;

            if (instruction == null || allegement == null ||
                instruction.IsCancelled || instruction.IsSettled || instruction.IsMatched)
            {
                return new MatchResult
                {
                    MatchId = matchData.MatchId,
                    Result = EMatchResult.NoMatch
                };
            }

            int matchResult = CompareInstructionAndAllegementForExactOrCloseMatch(instruction, allegement);

            return new MatchResult
            {
                MatchId = matchData.MatchId,
                Result = matchResult == 1 ? EMatchResult.ExactMatch : EMatchResult.NoMatch
            };
        }

        public override int CompareInstructionAndAllegementForExactOrCloseMatch(Instruction instruction, Allegement allegement)
        {
            bool isExactOrCloseMatch = instruction.MarketCode == allegement.MarketCode &&
                                        instruction.Amount == allegement.Amount &&
                                        instruction.Price == allegement.Price &&
                                        instruction.Quantity == allegement.Quantity;

            return isExactOrCloseMatch ? 1 : 3; // Return 1 for exact match, 3 for no match
        }
    }
}
