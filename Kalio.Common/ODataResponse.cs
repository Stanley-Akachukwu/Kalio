using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kalio.Common
{
    public class ODataResponse<T>
    {

        [JsonPropertyName("@odata.context")]
        public string Context { get; set; }


        [JsonPropertyName("@odata.count")]
        public int TotalRows { get; set; }


        [JsonPropertyName("value")]
        public List<T> Data { get; set; }

    }
}
