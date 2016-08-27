<Query Kind="Program">
  <NuGetReference>iTextSharp</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>iTextSharp.text.pdf</Namespace>
  <Namespace>iTextSharp.text.pdf.parser</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

static string  dirPath = @"C:\Users\ufuko\Desktop\GoogleDrive\TimingAttackDnsPrivateBrowsing";
static string[] keywords = File.ReadAllLines(System.IO.Path.Combine(dirPath, "keywords.txt")).Select(o=>o.Trim().ToLowerInvariant()).Where(o=>!string.IsNullOrEmpty(o)).ToArray();
static DataTable dt = new System.Data.DataTable("Transpose");
void Main()
{
	//"psedo test pse do my pse-do ds".Split(new[] {"pse do", "psedo","pse-do"},StringSplitOptions.RemoveEmptyEntries).Dump();	
	//Directory.GetFiles(dirPath).Where(o => o.EndsWith(".pdf")).Dump();
	
	var dc2 = new System.Data.DataColumn("Id") { DefaultValue = 0 };
	dt.Columns.AddRange(new System.Data.DataColumn[] { dc2 });
	foreach (var c in keywords)
	{
		var dc1 = new System.Data.DataColumn(c, typeof(object)) { DefaultValue = 0 };
		dt.Columns.AddRange(new System.Data.DataColumn[] { dc1 });
	}


	//DirectoryInfo di = new DirectoryInfo(dirPath);	
	Directory.GetFiles(dirPath).Where(o => o.EndsWith(".pdf")).ToList()
	.ForEach(o =>
	{
		try
		{
			GetPdf(o);
		}
		catch (Exception ex)
		{	
			LINQPad.Util.WithStyle(o,"color:red").Dump();
			ex.Dump();
			
		}
	});
	
	dt.Dump();


}
public static bool GetPdf(string fileName)
{

	var row = dt.NewRow();
	row["Id"]=System.IO.Path.GetFileNameWithoutExtension(fileName);
	var text = GetTextFromAllPages(System.IO.Path.Combine(dirPath, fileName)).ToLowerInvariant()
		;//.Dump();
		 //var res = text.Split(' ').Select(o => o.Trim()).Where(o => o.Length != 0).GroupBy(o => o).Where(o => keywords.Contains(o.Key))
	var res = keywords.Select(o => new { Key=o, Count=text.Split(new[] { o,o.Replace(" ",""),o.Replace(" ","-") }, StringSplitOptions.RemoveEmptyEntries).Length-1})
	;//.Dump();
	foreach (var item in res)
	{
		row[item.Key] = item.Count;
	}
	dt.Rows.Add(row);
	return true;
}
public static string GetTextFromAllPages(String pdfPath)
{
	PdfReader reader = new PdfReader(pdfPath);

	StringWriter output = new StringWriter();

	for (int i = 1; i <= reader.NumberOfPages; i++)
		output.WriteLine(PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy()));

	return output.ToString();
}