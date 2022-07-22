using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrezziSubito.Models.Request
{
    public class CalculateRequest
    {
        [JsonProperty("urls")]
        public List<string> Urls { get; set; }
    }
}
