using System;
namespace ApiForClients.Models.Request
{
	public class ReqGoogleProfiles
	{
		public string kind { get; set; }
		public string etag { get; set; }
		public string objectType { get; set; }
		public string id { get; set; }
		public string displayName { get; set; }
		public Nname name { get; set; }
		public Image image { get; set; }
		public Organizations[] organizations { get; set; }
		public PlacesLived[] placesLived { get; set; }

		public class Nname
		{
			public string familyName { get; set; }
			public string givenName { get; set; }
		}
		public class Image
		{
			public string url { get; set; }
			public string isDefault { get; set; }
		}
		public class PlacesLived
		{
			public string value { get; set; }
			public string primary { get; set; }
		}
		public class Organizations
		{
			public string name { get; set; }
			public string type { get; set; }
			public string endDate { get; set; }
			public string primary { get; set; }
		}

	}
}
