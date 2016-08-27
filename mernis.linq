<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

public static string conStr = @"data source=....;user id=sa;password=.....;Initial catalog=....;persist security info=True;multipleactiveresultsets=True;application name=EntityFramework";
void Main()
{
	
	var table = new DataTable("citizen");
	//var str = "uid, national_identifier, first, last, mother_first, father_first, gender, birth_city, date_of_birth, id_registration_city, id_registration_district, address_city, address_district, address_neighborhood, street_address, door_or_entrance_number, misc";
	var str = @"uid 
national_identifier 
first
last
mother_first
father_first
gender
birth_city
date_of_birth
id_registration_city
id_registration_district
address_city
address_district
address_neighborhood
street_address
door_or_entrance_number
misc";
	str.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(o =>
	{
		table.Columns.Add(new DataColumn(o.Trim()));
		return 0;
	}
	).ToArray();
	table.Dump();
	var lines = File.ReadLines(@"C:\Users\ufuko\Desktop\mernis\data_dump.sql");

	lines.Skip(75).Take(49611830).Select(o => o.Replace("<NULL>", " ").Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(t => (object)t.Trim()))
	.Select((o, i) =>
	{
	
	
		if (i % Math.Pow(2,16) == 0)
		{			
			i.Dump();
			MyExtensions.BulkInsert(conStr,table);
			table.Rows.Clear();	
		}
		var row=table.NewRow();		
		row.ItemArray=o.ToArray();
		table.Rows.Add(row);
		return true;
	})
	.ToArray();
	
}

// Define other methods and classes here