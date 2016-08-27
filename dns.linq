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
	var param = new
	{
		NumberOfUser = 64,
		NumberOfRandomDomain = 100,
		ClientLocalTimeList = MyExtensions.TypeList(new { IpAddress = "", BrowserInfo = "", TimeResult = 0 },true),
		ClientTimeFastList = MyExtensions.TypeList(new { IpAddress = "", BrowserInfo = "", TimeResult = 0 },true),
		ClientTimeSlowList = MyExtensions.TypeList(new { IpAddress = "", BrowserInfo = "", TimeResultList = MyExtensions.TypeList(new {BitNo=0,TimeResult=0}, true)},true),
		Round1DomainSeed="0a8719c312",
		Round2DomainSeed="0a8719c313",
		WarmUpDomainCount=50,
	};
param.Dump();
var dt = new DataTable();
	
}

// Define other methods and classes here