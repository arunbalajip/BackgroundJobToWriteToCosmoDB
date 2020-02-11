using Newtonsoft.Json;
using System;

namespace BackgroundJob
{
    public class Company
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CompanyName { get; set; }
        public double RequestCharge { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}