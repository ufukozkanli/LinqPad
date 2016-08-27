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
	Main2();
}
	void Main2()
{
	var folder = @"C:\Users\ufuko\Desktop\GoogleDrive\_Cyber Security\Books\PenetrationTest";
	var keyword = "hack";
	var newFolder="Hacking";
	var list = Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly).Where(o=>Path.GetFileNameWithoutExtension(o).ToLowerInvariant().Contains(keyword)).Select(o => new
	{
		FullFileName = o,
		FileName=Path.GetFileName(o),
		Link = LINQPad.Util.OnDemand<object>("Move",() =>
		{
			var newFilePath= Path.Combine(folder, newFolder,Path.GetFileName(o));
			if(File.Exists(newFilePath))
				return "File Already Exist...";
			//return newFilePath;
			File.Move(o, newFilePath);
			return newFilePath;
		})
    }).ToList().Dump();
}
void Main1()
{

	var list = Directory.GetFiles(@"C:\Users\ufuko\Desktop\GoogleDrive\_Cyber Security\Books\PenetrationTest", "*", SearchOption.AllDirectories).Select(o => new
	{
		FullFileName = o,
		Length = new FileInfo(o).Length
	}).ToList();
	list.Join(list, o => o.Length, t => t.Length, (o, t) => new
	{
		Length = new FileInfo(o.FullFileName).Length,
		FullFileName1 = o.FullFileName,
		FullFileName2 = t.FullFileName,
		//FileName1 = Path.GetFileName(o.FullFileName),
		//FileName2 = Path.GetFileName(t.FullFileName),		
		Link = LINQPad.Util.OnDemand<object>("FileLink",() =>
		 {
			 Func<object, string> f = (filePath) =>
			 {
				 string argument = "/select, \"" + filePath + "\"";
				 System.Diagnostics.Process.Start("explorer.exe", argument);
				 return null;
			 };
			 f(o.FullFileName);
			 f(t.FullFileName);
			 return null;
		 }),
		Delete1 = LINQPad.Util.OnDemand<object>("Delete",() =>
		  {
			  File.Delete(o.FullFileName);
			  return null;
		  }),
		Delete2 = LINQPad.Util.OnDemand<object>("Delete",() =>
		  {
			  File.Delete(t.FullFileName);
			  return null;
		  })
	}).Where(o => o.FullFileName1 != o.FullFileName2).ToList().Dump();
}

// Define other methods and classes here
