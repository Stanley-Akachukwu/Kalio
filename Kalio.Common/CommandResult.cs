using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kalio.Common
{
    public class CommandResult<T>  
    {

        public CommandResult()
        {
            StatusCode = 200;
            ErrorFlag = false;
        }

        [JsonPropertyName("response")]
        public T Response { get; set; }

        [JsonPropertyName("errorFlag")]
        public bool ErrorFlag { get; set; }


        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("statusCode")]
        public int? StatusCode { get; set; }

       
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

      
        [JsonPropertyName("instance")]
        public string Instance { get; set; }




        [JsonPropertyName("validationErrors")]
        public List<ModelValidationError> ValidationErrors { get; set; }
    }
}
