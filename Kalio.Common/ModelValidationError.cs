using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kalio.Common
{
     
    public class ModelValidationError
    {

        public ModelValidationError()
        {
        }

        [JsonPropertyName("entity")]
        public string Entity { get; set; }

        [JsonPropertyName("field")]
        public string FieldName { get; set; }

        [JsonPropertyName("type")]
        public string FieldType { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("rawValue")]
        public object RawValue { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }




    }
}
