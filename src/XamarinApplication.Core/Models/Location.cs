using Newtonsoft.Json;
using System.Text;

namespace XamarinApplication.Core.Models
{
    public class Location
    {
        [JsonConstructor]
        internal Location(string name, string region, string country)
        {
            Name = name;
            Region = region;
            Country = country;
        }

        public string Name { get; }
        public string Region { get; }
        public string Country { get; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Name: {Name}");
            builder.AppendLine($"Region: {Region}");
            builder.AppendLine($"Country: {Country}");
            return builder.ToString();
        }
    }
}