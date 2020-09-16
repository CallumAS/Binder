using Binder.Core.Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Core.Models
{
    public class Rule
    {
        [JsonIgnore]
        public bool Active { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("contains")]
        public bool Contains { get; set; }
        [JsonProperty("site")]
        public string Site { get; set; }
        [JsonProperty("type")]
        public ContentType Type { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }

    }
}
