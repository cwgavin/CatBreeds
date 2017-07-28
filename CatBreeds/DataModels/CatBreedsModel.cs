using Newtonsoft.Json;

namespace CatBreeds.DataModels
{
    public class CatBreedsModel
    {
		[JsonProperty(PropertyName = "Id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "Longitude")]
		public float Longitude { get; set; }

		[JsonProperty(PropertyName = "Latitude")]
		public float Latitude { get; set; }
    }
}
