<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	
	new[] { "BIL", "ZYBS","END","ING","MAT" }.ToList().ForEach(t =>
	 {
	 	Enumerable.Range(100, 600).ToList().ForEach(o =>
		 {
		 	var url = string.Format("http://uzakegitim.zirve.edu.tr/depo/{0}{1}", t,o);
			 var client = new WebClient();
			 client.DownloadStringAsync(new Uri(url));
			 client.DownloadStringCompleted += (u, y) =>
			 {
				 if (y.Error == null)
					 (t+o).Dump();
			 };
		 });
	 });
}

// Define other methods and classes here