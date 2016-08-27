<Query Kind="Program">
  <NuGetReference>ARSoft.Tools.Net</NuGetReference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>ARSoft.Tools.Net</Namespace>
  <Namespace>ARSoft.Tools.Net.Dns</Namespace>
  <Namespace>ARSoft.Tools.Net.Dns.DynamicUpdate</Namespace>
  <Namespace>ARSoft.Tools.Net.Net</Namespace>
  <Namespace>ARSoft.Tools.Net.Spf</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net.NetworkInformation</Namespace>
</Query>

void Main()
{
	MyExtensions.GetLocalIPAddress().Dump("IpAddress");
	var dnsIp="8.8.8.8";//"172.16.27.10";
	DNSHelper.StartDnsServer(dnsIp);
	return;
	
	var seed = "tMeym9iwL9KjWxjFzKpJ";
	
	DNSHelper.OSCacheClear();
	for (int i = 0; i < 2; i++)
	{
		DNSHelper.OSCacheDisplay();
		DNSTester.TestDns(seed,3000, false).Dump().Sum().Dump();
		//DNSHelper.OSCacheClear();
	}


}

public static class DNSHelper
{
	[DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
	public static extern UInt32 OSCacheClear();

	static DnsClient c;
	
	static DNSHelper()
	{

		var dnsIps = GetOSDnsServerIPs();
		dnsIps.Select(o => o.ToString()).Dump("DNS IPs");
		var dnsIp = dnsIps.First();		
		dnsIp.ToString().Dump("Selected DNS");
		c = new DnsClient(dnsIp, 1000);
	}
	public static List<IPAddress> GetOSDnsServerIPs()
	{
		var ethAdapter = NetworkInterface.GetAllNetworkInterfaces().Where(o => !o.Name.ToLowerInvariant().Contains("vmware") && o.OperationalStatus == OperationalStatus.Up).Select(o => new { o, Val = o.NetworkInterfaceType == NetworkInterfaceType.Ethernet ? 1 : 0 }).OrderByDescending(o => o.Val).Select(o => o.o).First();
		ethAdapter.Description.Dump("Selected Adapter");
		var ethAdapterProperties = ethAdapter.GetIPProperties();
		return ethAdapterProperties.DnsAddresses.ToList();
	}
	public static string Resolve(string dns)
	{
		var m = new DnsMessage() { IsRecursionDesired = true };
		m.Questions.Add(new DnsQuestion(new DomainName(dns.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)), RecordType.A, RecordClass.INet));
		var response = c.SendMessage(m);
		if (response == null)
		{
			return null;
		}
		if (response.ReturnCode == ReturnCode.NxDomain)
			return "";
		if(response.AnswerRecords.Count==0)
			return "";
		return response.AnswerRecords.OfType<ARecord>().First().Address.ToString();
	}

	public static void OSCacheDisplay()
	{
		var process = new System.Diagnostics.Process();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.FileName = "cmd.exe";
		process.StartInfo.Arguments = "/C ipconfig /displaydns";
		process.StartInfo.RedirectStandardOutput = true;
		process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
		process.Start();
		process.BeginOutputReadLine();
		process.WaitForExit();
	}

	public static void StartDnsServer(string dnsIp)
	{
		int i = 0;
		var s = new DnsServer(10, 0);
		s.Start();
		DnsClient cServer=new DnsClient(IPAddress.Parse(dnsIp),1000);
		s.QueryReceived += (sender, args) =>
		{
			return new TaskFactory().StartNew(() =>
			{
				var message = (DnsMessage)args.Query;

				var domainName = message.Questions.First().Name.ToString().Dump();//.Select(o => new { Name = o.Name.ToString(), o.RecordType, o.RecordClass }).First().Dump();
				if (domainName.Contains("asdtest"))
					message.Dump();
				var response = cServer.SendMessage(message);
				//response.AnswerRecords.Dump();
				//"Answer Send".Dump();
				//Select(o => new {Name=o.Name.ToString(),o.RecordType,o.RecordClass,Address=o.Address.ToString()}).Dump();
				args.Response = response;
			});
		};


	}



}

public class DNSTester
{
	public static List<long> TestDnsParallel(string seed, int count, bool flushEachRepeat)
	{
		var list = new List<long>();
		Parallel.For(0, count, (i) =>
		{
			var sw = new Stopwatch();
			sw.Restart();
			try
			{
				/*
				var response = DNSHelper.Resolve(string.Format("www.{0}{1}.com", seed, list.Count));
				if (response == null)
				{					
					continue;
				}
				*/
				var ipaddreses = Dns.GetHostAddresses(string.Format("www.{0}{1}.com", seed, i));
				var res = ipaddreses.First().ToString().Dump();

			}
			catch (Exception ex)
			{
				//ex.Message.Dump();
			}
			list.Add(sw.ElapsedMilliseconds);
		});
		

		return list;
	}

	public static List<long> TestDns(string seed, int count, bool flushEachRepeat)
	{
		var list = new List<long>();
		var sw = new Stopwatch();
		while (list.Count < count)
		{
			if (flushEachRepeat)
				DNSHelper.OSCacheClear();
			sw.Restart();
			try
			{
				/*
				var response = DNSHelper.Resolve(string.Format("www.{0}{1}.com", seed, list.Count));
				if (response == null)
				{					
					continue;
				}
				*/
				var ipaddreses = Dns.GetHostAddresses(string.Format("www.{0}{1}.com", seed, list.Count));
				var res = ipaddreses.First().ToString().Dump();
				
			}
			catch (Exception ex)
			{
				//ex.Message.Dump();
			}
			list.Add(sw.ElapsedMilliseconds);
		}

		return list;
	}
	public static List<long> Old(bool flushEachRepeat)
	{
		var seed = "w0a8719c3121001";
		var list = new List<long>();
		var sw = new Stopwatch();
		while (list.Count < 1000)
		{
			if (flushEachRepeat)
				DNSHelper.OSCacheClear();
			sw.Restart();
			try
			{
				//var ipaddreses=Dns.GetHostAddresses(domainList[list.Count % domainList.Count]).First().ToString().Dump();			
				var ipaddreses = Dns.GetHostAddresses(string.Format("www.{0}{1}.com", seed, list.Count));
				var res = ipaddreses.First().ToString().Dump();
			}
			catch (Exception)
			{

			}
			list.Add(sw.ElapsedMilliseconds);
		}

		return list;
	}
	public static object TestDnsForADomain()
	{
		var testDomains = MyExtensions.TypeList(new
		{
			RequestDomainName = "",
			result = new List<long>()
		}, true);

		testDomains.Add(new { RequestDomainName = string.Format("{0}.com", Guid.NewGuid().ToString().Replace("-", "")), result = new List<long>() });

		var sw = new Stopwatch();

		testDomains.ForEach(o =>
		{
			while (o.result.Count < 100)
			{
				DNSHelper.OSCacheClear();
				sw.Restart();
				var response = DNSHelper.Resolve(o.RequestDomainName);
				if (response == null)
				{
					//o.result.Add(-1);
					continue;
				}
				o.result.Add(sw.ElapsedMilliseconds.Dump());
				response.Dump();
				//Thread.Sleep(5000);
			}
		});


		return testDomains;
		//Thread.Sleep(int.MaxValue);
	}
	public static void Test1()
	{

		var domainList = new List<string> {
"Google.com"
,"Youtube.com"
,"Facebook.com"
,"Baidu.com"
,"Yahoo.com"
,"Amazon.com"
,"Twitter.com"
,"Qq.com"
,"Live.com"
,"Taobao.com"
,"Bing.com"
,"Msn.com"
,"Linkedin.com"
,"Weibo.com"
,"Vk.com"
,"Instagram.com"
,"Hao123.com"
};
		var testDomains = MyExtensions.TypeList(new
		{
			RequestDomainName = "",
			result = new List<long>()
		}, true);
		testDomains.Add(new { RequestDomainName = "google.com", result = new List<long>() });
		//testDomains.Add(new {RequestDomainName="zirve.edu.tr",result=new List<long>()});
		//testDomains.Add(new {RequestDomainName="stackoverflow.com",result=new List<long>()});
		//testDomains.Add(new {RequestDomainName="facebook.com",result=new List<long>()});
		//testDomains.Add(new {RequestDomainName="tubitak.gov.tr",result=new List<long>()});
	}

}