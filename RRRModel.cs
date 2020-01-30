using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace daviva_backend_5
{
    class RRRModel
    {
        public class MarkesJson
        {
            [JsonProperty("list")]
            public List<Markes> Markes { get; set; }
        }

        public class Markes
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
            public string Modelis { get; set; }
        }
    }
}
