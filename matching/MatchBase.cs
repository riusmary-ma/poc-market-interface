namespace PocMarketInterface.Matching
{

public abstract class MatchBase
{
   public abstract MatchResult CompareInstructionAndAllegement(MatchData matchData)
    {
        var instruction = matchData.Data.Item1;
        var allegement = matchData.Data.Item2;

         if (instruction == null || allegement == null)
        {
            return new MatchResult
            {
                ResultId = 0,
                MatchData = EMatchResult.NoMatch
            };
        }

        int matchResult = CompareInstructionAndAllegementForExactOrCloseMatch(instruction, allegement);

        return new MatchResult
        {
            ResultId = matchResult,
            MatchData = EMatchResult.NoMatch
        };
    }

    public virtual int CompareInstructionAndAllegementForExactOrCloseMatch(Instruction instruction, Allegement allegement)
    {
            return 3; // Return 3 for no match by default
    }
}
}