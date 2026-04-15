using PocMarketInterface.Enum;
using PocMarketInterface.Module;

namespace PocMarketInterface.Matching
{
    public abstract class MatchBase
    {
        public abstract MatchResult CompareInstructionAndAllegement(MatchData matchData);

        public virtual int CompareInstructionAndAllegementForExactOrCloseMatch(Instruction instruction, Allegement allegement)
        {
            return 3; // Return 3 for no match by default
        }
    }
}
