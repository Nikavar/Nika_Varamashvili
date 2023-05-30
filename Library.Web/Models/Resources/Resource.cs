using Newtonsoft.Json;

namespace Library.Web.Models.Resources
{
    public abstract class Resource
    {
        [JsonProperty(Order = -2)]
        public string Href { get; set; }
    }
}
