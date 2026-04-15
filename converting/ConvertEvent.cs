using System;
using System.Text.Json;
using PocMarketInterface.Module;

namespace PocMarketInterface.Converting
{
    public static class ConvertEvent
    {
        public static object? ConvertEventData(Event evt)
        {
            if (evt is null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            return evt.EventType switch
            {
                "102" => JsonSerializer.Deserialize<Allegement>(evt.Data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }),
                "194" => JsonSerializer.Deserialize<Instruction>(evt.Data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }),
                _ => null,
            };
        }
    }
}
