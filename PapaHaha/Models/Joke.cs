using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PapaHaha.Models
{
    public class Joke
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("joke")]
        public string JokeText { get; set; }

        [JsonPropertyName("status")]
        public HttpStatusCode Status { get; set; }
    }
}
