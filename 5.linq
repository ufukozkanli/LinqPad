<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Data.SqlTypes</Namespace>
</Query>

void Main()
{
	var r=Enumerable.Range(1, 10).Select(o => new {Id=o,Name=o+"-"});
	
	CsvExport.Export(r,true).Dump();
	return;
	
	for (int i = 0; i < 100; i++)
	{
	LINQPad.Util.ClearResults();
		"sa".Dump();
		
	}
	var deferred = Task.Factory.StartNew(() => Console.WriteLine("Hello"));

	// done
	deferred.ContinueWith(d => Console.WriteLine("dsa"), TaskContinuationOptions.OnlyOnRanToCompletion);

	// fail
	deferred
		.ContinueWith(d => Console.WriteLine(d.Exception), TaskContinuationOptions.OnlyOnFaulted);

	// always
	deferred
		.ContinueWith(d => Console.WriteLine("Do something"));
		deferred.Wait();
}

public static class CsvExport
{
	public static void Export<T>(IEnumerable<T> objectList ,string fileName)
	{
		File.WriteAllText(fileName, Export(objectList,true));;
	}

	public static string Export<T>(IEnumerable<T> objectList,bool includeHeaderLine)
	{

		StringBuilder sb = new StringBuilder();
		//Get properties using reflection.
		IList<PropertyInfo> propertyInfos = typeof(T).GetProperties();

		if (includeHeaderLine)
		{
			//add header line.
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				sb.Append(propertyInfo.Name).Append(",");
			}
			sb.Remove(sb.Length - 1, 1).AppendLine();
		}

		//add value for each property.
		foreach (T obj in objectList)
		{
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				sb.Append(MakeValueCsvFriendly(propertyInfo.GetValue(obj, null))).Append(",");
			}
			sb.Remove(sb.Length - 1, 1).AppendLine();
		}

		return sb.ToString();
	}
	
	//get the csv value for field.
	private static string MakeValueCsvFriendly(object value)
	{
		if (value == null) return "";
		if (value is Nullable && ((INullable)value).IsNull) return "";

		if (value is DateTime)
		{
			if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
				return ((DateTime)value).ToString("yyyy-MM-dd");
			return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
		}
		string output = value.ToString();

		if (output.Contains(",") || output.Contains("\""))
			output = '"' + output.Replace("\"", "\"\"") + '"';

		return output;

	}
}