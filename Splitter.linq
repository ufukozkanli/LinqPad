<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	var str = @" ";
    var res = str.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
	.Select(o => o.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries))
	//.Select((o, i) => o[2].Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0]).Distinct()
	//.Select((o, i) => new { Color = o[0], HexColor = "#" + o[1] + o[2] + o[3] }).OrderBy(o => Guid.NewGuid())
	.Select((o, i) => new { Color = o[0].TrimEnd('.') }).Distinct()
	.Dump();
	//JsonConvert.SerializeObject(res).Dump();
}

// Define other methods and classes here