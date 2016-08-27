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
	System.Timers.Timer saveTimer = new System.Timers.Timer(3000);
	saveTimer.Elapsed += (o, e) => {
		saveTimer.Enabled=false;
		e.Dump();
		Thread.Sleep(1000);
		saveTimer.Enabled=true;
	};
	saveTimer.Enabled=true;
	
	
	Func<int,int> a =(t) => {
		"a".Dump();
		return 1;
	};
	a(5);
	LINQPad.Util.OnDemand("dsadasasdd", () => {return 6;}).Dump();
	LINQPad.Util.WithStyle("das","color:green").Dump();
	LINQPad.Util.Highlight("das").Dump();
	LINQPad.Util.RawHtml("<strong style='color:red'>sda</strong>").Dump();
	//new WebClient().DownloadData("http://www.google.com").Dump();
	new List<int>(){0,0,0}.LastOrDefault(o => o != 0); 
}

// Define other methods and classes here