using System;
using PocMarketInterface.Enum;
using PocMarketInterface.Module;
using PocMarketInterface.Matching;
using Xunit;

namespace PocMarketInterface.Tests
{
    public class NoMatchTest
    {
        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_Always()
        {
            var matchData = new MatchData
            {
                MatchId = 20,
                Data = (new Instruction
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 5,
                    Amount = 500m,
                    IsCancelled = false,
                    IsSettled = false,
                    IsMatched = false
                }, new Allegement
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 5,
                    Amount = 500m
                })
            };

            var sut = new NoMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_WhenInputIsNull()
        {
            var matchData = new MatchData { MatchId = 21, Data = (null, null) };
            var sut = new NoMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }
    }
}
