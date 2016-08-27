<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Framework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Tasks.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Utilities.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.Protocols.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Caching.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.RegularExpressions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Web.UI</Namespace>
  <Namespace>System.Data.SqlTypes</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Sockets</Namespace>
</Query>

void Main()
{
	// Write code to test your extensions here. Press F5 to compile and run.
}

public static class MyExtensions
{
	public static string LOGFilePath = "fklog.txt";
	//public static object Dump(this object t)
	//{
	//	Debug.WriteLine(t.ToString());
	//	return t;
	//}
	
	static MyExtensions()
	{		
		JsonConvert.DefaultSettings = () => new JsonSerializerSettings
		{
			DateTimeZoneHandling = DateTimeZoneHandling.Local

		};
		"MyExtension Loaded...".Dump();
	}
	public static List<T> TypeList<T>(T t,bool createInstance=false)
	{
		return createInstance?new List<T>():(List<T>) null;
	}
	public static DataTable ToDataTable<T>(this IList<T> data)
	{
		var props =
			TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>().Where(o => o.PropertyType.IsSerializable&&o.PropertyType.IsValueType).ToList();
		DataTable table = new DataTable(typeof(T).Name);
		for (int i = 0; i < props.Count; i++)
		{
			PropertyDescriptor prop = props[i];
			
			table.Columns.Add(prop.Name, prop.PropertyType.IsGenericType&&prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)?Nullable.GetUnderlyingType(prop.PropertyType):prop.PropertyType);
		}
		object[] values = new object[props.Count];
		foreach (T item in data)
		{
			for (int i = 0; i < values.Length; i++)
			{
				values[i] = props[i].GetValue(item);
			}
			table.Rows.Add(values);
		}
		return table;
	}
	public static T ChangeType<T>(object obj)
	{
		if (obj == null || string.IsNullOrEmpty(obj.ToString()))
			return default(T);
		try
		{
			if (typeof(T).IsEnum)
				return (T)Enum.Parse(typeof(T), obj.ToString());
			return (T)Convert.ChangeType(obj, typeof(T));
		}
		catch (Exception ex)
		{
			ex.Dump();
			return default(T);
		}
	}
	public static double RandNormal(Random rand, double mean, double stdDev)
	{		
		//reuse this if you are generating many
		double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
		double u2 = rand.NextDouble();
		double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
					 Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
		double randNormal =
					 mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
		return randNormal;
	}
	public static string GetName<TSource, TField>(Expression<Func<TSource, TField>> Field)
	{
		return (Field.Body as MemberExpression ?? ((UnaryExpression)Field.Body).Operand as MemberExpression).Member.Name;
	}
	public static T Clone<T>(T obj)
	{

		var newObj = Activator.CreateInstance(typeof(T));
		foreach (var pi in obj.GetType().GetProperties())
		{
			if (pi.CanRead && pi.CanWrite && pi.PropertyType.IsSerializable && pi.PropertyType.IsValueType)
			{
				pi.SetValue(newObj, pi.GetValue(obj, null), null);
			}
		}
		return (T)newObj;

	}

	
	public static string Log(string msg)
	{
		
		try
		{	        
			File.AppendAllText(LOGFilePath,string.Format("{0},{1}\r\n",DateTime.Now,msg));	
		}
		catch (Exception ex)
		{
			ex.Dump();			
		}
		
		return msg;
	}
	public static object Dump(string msg)
	{		
		
		
		StringWriter stringWriter = new StringWriter();

		// Put HtmlTextWriter in using block because it needs to call Dispose.
		using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
		{
			// Loop over some strings.
			foreach (var word in new int[1])
			{
				// Some strings for the attributes.
				string classValue = "ClassName";
				string urlValue = "http://www.dotnetperls.com/";
				string imageValue = "image.jpg";

				// The important part:
				writer.AddAttribute(HtmlTextWriterAttribute.Class, classValue);
				writer.RenderBeginTag(HtmlTextWriterTag.Strong); // Begin #1

				writer.AddAttribute(HtmlTextWriterAttribute.Href, urlValue);
				writer.RenderBeginTag(HtmlTextWriterTag.A); // Begin #2

				writer.AddAttribute(HtmlTextWriterAttribute.Src, imageValue);
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "60");
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "60");
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");

				writer.RenderBeginTag(HtmlTextWriterTag.Img); // Begin #3
				writer.RenderEndTag(); // End #3

				writer.Write(word);

				writer.RenderEndTag(); // End #2
				writer.RenderEndTag(); // End #1
			}
		}
		// Return the result.
		return stringWriter.ToString();
		LINQPad.Util.RawHtml("<strong style='color:red'></strong>").Dump();
	}

	public static void HandleException(Exception ex)
	{

		var baseEx = ex.GetBaseException();
		//		if (baseEx is DbEntityValidationException)
		//		{
		//			var dbEx = baseEx as DbEntityValidationException;
		//			result.Message =
		//			 JsonConvert.SerializeObject(dbEx.EntityValidationErrors.SelectMany(o => o.ValidationErrors).Select(o => new { o.PropertyName, o.ErrorMessage }));
		//		}
		//		else if (baseEx is SqlException)
		//		{
		//			var sqlEx = baseEx as SqlException;
		//			if (sqlEx.Errors.Count > 0)
		//			{
		//				//if(sqlEx.Errors[0].Number==547)
		//			}
		//			result.Message = JsonConvert.SerializeObject(sqlEx.Message);
		//		}
		//		else
		//		{
		//			result.Message = baseEx.Message;
		//		}


		var st = new StackTrace(ex, true); // create the stack trace
		var query = st.GetFrames().Select(frame => new
		{
			FileName = frame.GetFileName(),
			LineNumber = frame.GetFileLineNumber(),
			ColumnNumber = frame.GetFileColumnNumber(),
			Method = frame.GetMethod(),
			Class = frame.GetMethod().DeclaringType,
		}).Where(o => o.FileName != null).Select(o => new
		{
			FileName = o.FileName,
			o.LineNumber,
			o.ColumnNumber,
			o.Method,
			o.Class
		}).ToList();
		var msg=new { Message=baseEx.Message,StackTrace=query};	
		Log(JsonConvert.SerializeObject(msg));
	}

	public static void BulkInsert(string constr,DataTable table)
	{
		

		using (SqlBulkCopy bulkCopy = new SqlBulkCopy(constr))
		{
			bulkCopy.BulkCopyTimeout = 600; // in seconds
			bulkCopy.DestinationTableName = table.TableName;
			bulkCopy.WriteToServer(table);
		}
	}
	public static string GetLocalIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}
		}
		throw new Exception("Local IP Address Not Found!");
	}

	public static class CsvExport
	{
		public static void Export(IList objectList, string fileName)
		{
			File.WriteAllText(fileName, Export(objectList, true)); ;
		}
		private static string Export(IList objectList, bool includeHeaderLine)
		{
			if (objectList == null) throw new ArgumentNullException(nameof(objectList));
			var firstItem = objectList.Count > 0;
			var sb = new StringBuilder();
			//Get properties using reflection.
			IList<PropertyInfo> propertyInfos = objectList[0].GetType().GetProperties();

			if (includeHeaderLine)
			{
				//add header line.
				foreach (var propertyInfo in propertyInfos)
				{
					sb.Append(propertyInfo.Name).Append(",");
				}
				sb.Remove(sb.Length - 1, 1).AppendLine();
			}

			//add value for each property.
			foreach (var obj in objectList)
			{
				foreach (var propertyInfo in propertyInfos)
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
			var output = value.ToString();

			if (output.Contains(",") || output.Contains("\""))
				output = '"' + output.Replace("\"", "\"\"") + '"';

			return output;

		}
		
	}

}


// You can also define non-static classes, enums, etc.