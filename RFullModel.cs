using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace daviva_backend_5
{
    class RFullModel
    {
        public class RFullJson
        {
            [JsonProperty("list")]
            public List<RFull> RFull { get; set; }
        }

        public class RFull //{"id":"1","brand":"3","name":"A4, S4 (B5- 8D)","year_start":"1994","year_end":"1999"}
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("brand")]
            public int Brand { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("year_start")]
            public string Year_start { get; set; }
            [JsonProperty("year_end")]
            public string Year_end { get; set; }
        }
    }
}
