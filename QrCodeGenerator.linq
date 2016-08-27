<Query Kind="Program">
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>MessagingToolkit.QRCode</NuGetReference>
  <Namespace>MessagingToolkit.QRCode.Codec</Namespace>
  <Namespace>MessagingToolkit.QRCode.Codec.Data</Namespace>
  <Namespace>MessagingToolkit.QRCode.Codec.Ecc</Namespace>
  <Namespace>MessagingToolkit.QRCode.Codec.Reader</Namespace>
  <Namespace>MessagingToolkit.QRCode.Codec.Reader.Pattern</Namespace>
  <Namespace>MessagingToolkit.QRCode.Crypt</Namespace>
  <Namespace>MessagingToolkit.QRCode.ExceptionHandler</Namespace>
  <Namespace>MessagingToolkit.QRCode.Geom</Namespace>
  <Namespace>MessagingToolkit.QRCode.Helper</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	QRCodeEncoder encoder = new QRCodeEncoder();
	var hi = encoder.Encode(@"det Test 
Personal Details
");
	//hi.Save(Server.MapPath("~/imageFolder/ji.jpg"), ImageFormat.Jpeg);
	hi.Dump();
}

// Define other methods and classes here
