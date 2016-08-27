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
	var dt=new DataTable();
	dt.Columns.Add("Name",typeof(object));
	var fileNames=Directory.GetFiles(@"C:\Users\ufuko\Desktop\Temp\New Folder").ToList();	
	fileNames.ForEach(o => dt.Columns.Add(Path.GetFileNameWithoutExtension(o), typeof(object)));
	fileNames.ForEach(o =>
	{
		var r=dt.NewRow();
		r.SetField(0,Path.GetFileNameWithoutExtension(o));
		dt.Rows.Add(r);
    });
	
	for (int i = 0; i < fileNames.Count; i++)
	{
		var f1=fileNames[i];			
		var str1=File.ReadAllText(f1);
		for (int j = i+1; j < fileNames.Count; j++)
		{
			var f2=fileNames[j];			
			var str2=File.ReadAllText(f2);
			try
			{	        
				var val=LevenshteinDistance.Compute(str1,str2).Dump();
				dt.Rows[i][j+1]=val;
			}
			catch (Exception ex)
			{
				(i+""+j).Dump();
			}
			
			
			
		}
	}
	
	dt.Dump();
	
}
static class LevenshteinDistance
{
	public static int Compute(string s, string t)
	{
		if (string.IsNullOrEmpty(s))
		{
			if (string.IsNullOrEmpty(t))
				return 0;
			return t.Length;
		}

		if (string.IsNullOrEmpty(t))
		{
			return s.Length;
		}

		int n = s.Length;
		int m = t.Length;
		int[,] d = new int[n + 1, m + 1];

		// initialize the top and right of the table to 0, 1, 2, ...
		for (int i = 0; i <= n; d[i, 0] = i++) ;
		for (int j = 1; j <= m; d[0, j] = j++) ;

		for (int i = 1; i <= n; i++)
		{
			for (int j = 1; j <= m; j++)
			{
				int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
				int min1 = d[i - 1, j] + 1;
				int min2 = d[i, j - 1] + 1;
				int min3 = d[i - 1, j - 1] + cost;
				d[i, j] = Math.Min(Math.Min(min1, min2), min3);
			}
		}
		return d[n, m];
	}
}
// Define other methods and classes here
