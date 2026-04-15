using System;
using PocMarketInterface.Enum;
using PocMarketInterface.Module;
using PocMarketInterface.Matching;
using Xunit;

namespace PocMarketInterface.Tests
{
    public class CloseMatchTest
    {
        [Fact]
        public void CompareInstructionAndAllegement_ReturnsCloseMatch_WhenOnlyAmountDiffers()
        {
            var matchData = new MatchData
            {
                MatchId = 10,
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
                    Amount = 510m
                })
            };

            var sut = new CloseMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.CloseMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_WhenPriceDiffers()
        {
            var matchData = new MatchData
            {
                MatchId = 11,
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
                    Price = 101m,
                    Quantity = 5,
                    Amount = 500m
                })
            };

            var sut = new CloseMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_WhenInstructionIsSettled()
        {
            var matchData = new MatchData
            {
                MatchId = 12,
                Data = (new Instruction
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 5,
                    Amount = 500m,
                    IsCancelled = false,
                    IsSettled = true,
                    IsMatched = false
                }, new Allegement
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 5,
                    Amount = 510m
                })
            };

            var sut = new CloseMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegementForExactOrCloseMatch_ReturnsTwo_WhenOnlyAmountDiffers()
        {
            var sut = new CloseMatch();
            var result = sut.CompareInstructionAndAllegementForExactOrCloseMatch(
                new Instruction
                {
                    MarketCode = "XYZ",
                    Price = 20m,
                    Quantity = 2,
                    Amount = 40m
                },
                new Allegement
                {
                    MarketCode = "XYZ",
                    Price = 20m,
                    Quantity = 2,
                    Amount = 45m
                });

            Assert.Equal(2, result);
        }

        [Fact]
        public void CompareInstructionAndAllegementForExactOrCloseMatch_ReturnsThree_WhenNotCloseMatch()
        {
            var sut = new CloseMatch();
            var result = sut.CompareInstructionAndAllegementForExactOrCloseMatch(
                new Instruction
                {
                    MarketCode = "XYZ",
                    Price = 20m,
                    Quantity = 2,
                    Amount = 40m
                },
                new Allegement
                {
                    MarketCode = "XYZ",
                    Price = 21m,
                    Quantity = 2,
                    Amount = 45m
                });

            Assert.Equal(3, result);
        }
    }
}
