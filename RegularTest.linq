<Query Kind="Program">
  <Connection>
    <ID>71cffb82-448c-4d1c-8ebc-666c3c2050be</ID>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Users\ufuko\Source\Repos\tigem\Tigem.Business\bin\Debug\Tigem.Business.dll</CustomAssemblyPath>
    <CustomTypeName>Tigem.Business.Data.Model.TigemDBEntities</CustomTypeName>
    <AppConfigPath>C:\Users\ufuko\Source\Repos\tigem\Tigem.Business\bin\Debug\Tigem.Business.dll.config</AppConfigPath>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.RegularExpressions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.Protocols.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Caching.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Utilities.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Framework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Tasks.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>Tigem.Tools</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
//####
	//https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx
//####
	var rawString="";
	
	//if (Clipboard.ContainsText(TextDataFormat.Text))
	{
		Clipboard.GetText().Dump();		
		rawString = Clipboard.GetText();
		//Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
	}
	
	
	Regex linkParser = new Regex(@">\w*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	//string rawString = File.ReadAllText("H:/a.txt");
	foreach (Match m in linkParser.Matches(rawString))
	{
		//new WebClient().DownloadFile(m.Value,G
		m.Value.Dump();
		var val = m.Value.Split('/').Last();
		try
		{
			val = val.Substring(0, val.IndexOf("?"));
			//if (val.Contains(".pptx"))
			{

				(HttpUtility.UrlDecode(val)).Dump();

			}
		}
		catch (Exception ex)
		{

			
		}


	};
}

// Define other methods and classes here