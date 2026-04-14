using System;
using PocMarketInterface.Enum;
using PocMarketInterface.Module;
using PocMarketInterface.Matching;
using Xunit;

namespace PocMarketInterface.Tests
{
    public class TryMatchingTest
    {
        [Fact]
        public void CompareInstructionAndAllegement_ReturnsExactMatch_WhenExactMatchExists()
        {
            var instruction = new Instruction
            {
                MarketCode = "ABC",
                Price = 100m,
                Quantity = 5,
                Amount = 500m,
                IsBuy = true,
                IsSell = false,
                IsCancelled = false,
                IsSettled = false,
                IsMatched = false
            };
            var allegement = new Allegement
            {
                MarketCode = "ABC",
                Price = 100m,
                Quantity = 5,
                Amount = 500m,
                IsBuy = true,
                IsSell = false
            };

            var matchData = new MatchData
            {
                MatchId = 30,
                Data = (instruction, allegement)
            };

            var sut = new TryMatching();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.ExactMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsCloseMatch_WhenOnlyAmountDiffers()
        {
            var instruction = new Instruction
            {
                MarketCode = "ABC",
                Price = 100m,
                Quantity = 5,
                Amount = 500m,
                IsBuy = true,
                IsSell = false,
                IsCancelled = false,
                IsSettled = false,
                IsMatched = false
            };
            var allegement = new Allegement
            {
                MarketCode = "ABC",
                Price = 100m,
                Quantity = 5,
                Amount = 510m,
                IsBuy = true,
                IsSell = false
            };

            var matchData = new MatchData
            {
                MatchId = 31,
                Data = (instruction, allegement)
            };

            var sut = new TryMatching();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.CloseMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_WhenNoMatchExists()
        {
            var instruction = new Instruction
            {
                MarketCode = "ABC",
                Price = 100m,
                Quantity = 5,
                Amount = 500m,
                IsBuy = true,
                IsSell = false,
                IsCancelled = false,
                IsSettled = false,
                IsMatched = false
            };
            var allegement = new Allegement
            {
                MarketCode = "ABC",
                Price = 101m,
                Quantity = 5,
                Amount = 500m,
                IsBuy = true,
                IsSell = false
            };

            var matchData = new MatchData
            {
                MatchId = 32,
                Data = (instruction, allegement)
            };

            var sut = new TryMatching();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }
    }
}
