using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PlumGuide.Rover.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompassDirection
    {
        North,
        East,
        South,
        West
    }
}
