<Query Kind="Program">
  <Connection>
    <ID>65424e6a-4304-421a-91e4-50010fc5a853</ID>
    <Persist>true</Persist>
    <Server>10.10.1.15</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>yayintesvik</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAZpAC5HrLAEqnJrXWCi5R4gAAAAACAAAAAAAQZgAAAAEAACAAAAAoQbUL8yUzGRZwo5g4CGzrhJtpI+qezZohYcWW2L9ZcgAAAAAOgAAAAAIAACAAAABa3fohziWpQ3pkX71awMZsuagSkIqK4R9OpJgRzRBvNxAAAACrNM6NRbvXEp18Fzjsdx5LQAAAAFBbYUNmvV1xyAXBIHlNCnvFB0ORYslxxaPdVrKDshSMiuLPjFLBmyN+hIbVo4+Eby1D3+0XrxH4PZ2IDPIqvLs=</Password>
    <Database>MuhYayinTesvik</Database>
    <ShowServer>true</ShowServer>
  </Connection>
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
	var list = File.ReadAllLines(@"C:\Users\ufuko\Desktop\a.txt");
	var res = list.Select(o => o.Split(new[] {"\t"},StringSplitOptions.RemoveEmptyEntries)).Select(o => new {Title=o[0],ISSN=o[1],Point=decimal.Parse(o[2]),Incenv=decimal.Parse(o[3].Replace(",",".").Replace("TL",""))});
	//res.Dump();
	var sts=STT_UlakBims.ToList();
	var notEx=new List<object>();
	foreach (var item in res)
	{
		var old = sts.SingleOrDefault(o => o.ArticleTitle == item.Title);
	
		if (old == null)
		{
			old = new STT_UlakBim { ArticleTitle = item.Title, ISSN = item.ISSN };
			STT_UlakBims.InsertOnSubmit(old);
		}		
		old.UlakBimSTT_UlakBimIncentives.Add(new STT_UlakBimIncentive {ArticlePoint=item.Point,IncentiveAmount=item.Incenv,Year=2014});
	}
	SubmitChanges();
	1.Dump();
}

// Define other methods and classes here
