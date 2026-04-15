using System;
using PocMarketInterface.Enum;
using PocMarketInterface.Module;
using PocMarketInterface.Matching;
using Xunit;

namespace PocMarketInterface.Tests
{
    public class ExactMatchTest
    {
        [Fact]
        public void CompareInstructionAndAllegement_ReturnsExactMatch_WhenAllFieldsMatch()
        {
            var matchData = new MatchData
            {
                MatchId = 1,
                Data = (new Instruction
                {
                    DataId = 101,
                    CreatedTime = DateTime.UtcNow,
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 10,
                    Amount = 1000m,
                    IsCancelled = false,
                    IsSettled = false,
                    IsMatched = false
                }, new Allegement
                {
                    DataId = 202,
                    CreatedTime = DateTime.UtcNow,
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 10,
                    Amount = 1000m
                })
            };

            var sut = new ExactMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.ExactMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_WhenInstructionIsCancelled()
        {
            var matchData = new MatchData
            {
                MatchId = 2,
                Data = (new Instruction
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 10,
                    Amount = 1000m,
                    IsCancelled = true,
                    IsSettled = false,
                    IsMatched = false
                }, new Allegement
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 10,
                    Amount = 1000m
                })
            };

            var sut = new ExactMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegement_ReturnsNoMatch_WhenInstructionAndAllegementDiffer()
        {
            var matchData = new MatchData
            {
                MatchId = 3,
                Data = (new Instruction
                {
                    MarketCode = "ABC",
                    Price = 100m,
                    Quantity = 10,
                    Amount = 1000m,
                    IsCancelled = false,
                    IsSettled = false,
                    IsMatched = false
                }, new Allegement
                {
                    MarketCode = "ABC",
                    Price = 101m,
                    Quantity = 10,
                    Amount = 1000m
                })
            };

            var sut = new ExactMatch();
            var result = sut.CompareInstructionAndAllegement(matchData);

            Assert.Equal(matchData.MatchId, result.MatchId);
            Assert.Equal(EMatchResult.NoMatch, result.MatchData);
        }

        [Fact]
        public void CompareInstructionAndAllegementForExactOrCloseMatch_ReturnsOne_ForExactMatch()
        {
            var sut = new ExactMatch();
            var result = sut.CompareInstructionAndAllegementForExactOrCloseMatch(
                new Instruction
                {
                    MarketCode = "XYZ",
                    Price = 55m,
                    Quantity = 2,
                    Amount = 110m
                },
                new Allegement
                {
                    MarketCode = "XYZ",
                    Price = 55m,
                    Quantity = 2,
                    Amount = 110m
                });

            Assert.Equal(1, result);
        }

        [Fact]
        public void CompareInstructionAndAllegementForExactOrCloseMatch_ReturnsThree_WhenAnyFieldDiffers()
        {
            var sut = new ExactMatch();
            var result = sut.CompareInstructionAndAllegementForExactOrCloseMatch(
                new Instruction
                {
                    MarketCode = "XYZ",
                    Price = 55m,
                    Quantity = 2,
                    Amount = 110m
                },
                new Allegement
                {
                    MarketCode = "XYZ",
                    Price = 56m,
                    Quantity = 2,
                    Amount = 112m
                });

            Assert.Equal(3, result);
        }
    }
}
