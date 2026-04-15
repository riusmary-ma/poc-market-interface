using System.Text.Json;
using PocMarketInterface.Converting;
using PocMarketInterface.Module;
using Xunit;

namespace PocMarketInterface.Tests
{
    public class ConvertEventTest
    {
        [Fact]
        public void ConvertEventData_ReturnsAllegement_WhenEventTypeIs102()
        {
            var evt = new Event
            {
                EventType = "102",
                Data = JsonSerializer.Serialize(new
                {
                    marketCode = "ABC",
                    price = 100.5m,
                    quantity = 10,
                    amount = 1005m
                })
            };

            var result = ConvertEvent.ConvertEventData(evt);

            var allegement = Assert.IsType<Allegement>(result);
            Assert.Equal("ABC", allegement.MarketCode);
            Assert.Equal(100.5m, allegement.Price);
            Assert.Equal(10, allegement.Quantity);
            Assert.Equal(1005m, allegement.Amount);
        }

        [Fact]
        public void ConvertEventData_ReturnsInstruction_WhenEventTypeIs194()
        {
            var evt = new Event
            {
                EventType = "194",
                Data = JsonSerializer.Serialize(new
                {
                    marketCode = "XYZ",
                    price = 200.25m,
                    quantity = 3,
                    amount = 600.75m,
                    isCancelled = false,
                    isSettled = false,
                    isMatched = true
                })
            };

            var result = ConvertEvent.ConvertEventData(evt);

            var instruction = Assert.IsType<Instruction>(result);
            Assert.Equal("XYZ", instruction.MarketCode);
            Assert.Equal(200.25m, instruction.Price);
            Assert.Equal(3, instruction.Quantity);
            Assert.Equal(600.75m, instruction.Amount);
            Assert.False(instruction.IsCancelled);
            Assert.False(instruction.IsSettled);
            Assert.True(instruction.IsMatched);
        }

        [Fact]
        public void ConvertEventData_ReturnsNull_ForUnsupportedEventType()
        {
            var evt = new Event
            {
                EventType = "105",
                Data = JsonSerializer.Serialize(new
                {
                    marketCode = "ABC",
                    price = 1,
                    quantity = 1,
                    amount = 1
                })
            };

            var result = ConvertEvent.ConvertEventData(evt);

            Assert.Null(result);
        }
    }
}
