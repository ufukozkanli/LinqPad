<Query Kind="Program">
  <Connection>
    <ID>497fab8e-e7d7-4b44-9357-a8a450d4cf40</ID>
    <Persist>true</Persist>
    <Server>(LocalDB)\MSSQLLocalDB</Server>
    <AttachFile>true</AttachFile>
    <AttachFileName>&lt;MyDocuments&gt;\Visual Studio 2015\Projects\DNS_EXP\DNS_EXP\App_Data\DNS_EXP.mdf</AttachFileName>
  </Connection>
  <Output>DataGrids</Output>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
</Query>

void Main()
{

	RoundStates.Dump();
	Round1BrowserData.Dump();

	Round1BrowserData.Where(o=>o.Round2BrowserData!=null).ToList().Join(Round1BrowserData.Where(o=>o.Round2BrowserData!=null).ToList(), a => a.UserName[0], b => b.UserName[0], (a, b) => new
	{
		X = a.UserName,
		XT = JsonConvert.DeserializeObject<List<double>>(a.Round2BrowserData),
		Y = b.UserName,
		YT = JsonConvert.DeserializeObject<List<double>>(b.Round2BrowserData),
	}).Where(o => o.Y.StartsWith(o.X+"_"))
	.Dump();
}

// Define other methods and classes here