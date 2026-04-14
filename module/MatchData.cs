namespace PocMarketInterface.Module
{
    public class MatchData
    {
        public long MatchId { get; set; }
        public (Instruction, Allegement) Data { get; set; }
    }
}