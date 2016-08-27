<Query Kind="Program">
  <Reference Relative="..\Visual Studio 2015\Projects\MAMO\MAMO.DAL\bin\Debug\MAMO.DAL.dll">&lt;MyDocuments&gt;\Visual Studio 2015\Projects\MAMO\MAMO.DAL\bin\Debug\MAMO.DAL.dll</Reference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>EntityFramework.BulkInsert-ef6</NuGetReference>
  <NuGetReference>NBuilder</NuGetReference>
  <Namespace>EntityFramework.BulkInsert</Namespace>
  <Namespace>EntityFramework.BulkInsert.Exceptions</Namespace>
  <Namespace>EntityFramework.BulkInsert.Extensions</Namespace>
  <Namespace>EntityFramework.BulkInsert.Helpers</Namespace>
  <Namespace>EntityFramework.BulkInsert.Providers</Namespace>
  <Namespace>FizzWare.NBuilder</Namespace>
  <Namespace>FizzWare.NBuilder.Dates</Namespace>
  <Namespace>FizzWare.NBuilder.Extensions</Namespace>
  <Namespace>FizzWare.NBuilder.Generators</Namespace>
  <Namespace>FizzWare.NBuilder.Implementation</Namespace>
  <Namespace>FizzWare.NBuilder.PropertyNaming</Namespace>
  <Namespace>MAMO.DAL</Namespace>
  <Namespace>MAMO.DAL.MNT</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.Core</Namespace>
  <Namespace>System.Data.Entity.Core.Common</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial</Namespace>
  <Namespace>System.Data.Entity.Core.Common.EntitySql</Namespace>
  <Namespace>System.Data.Entity.Core.EntityClient</Namespace>
  <Namespace>System.Data.Entity.Core.Mapping</Namespace>
  <Namespace>System.Data.Entity.Core.Metadata.Edm</Namespace>
  <Namespace>System.Data.Entity.Core.Objects</Namespace>
  <Namespace>System.Data.Entity.Core.Objects.DataClasses</Namespace>
  <Namespace>System.Data.Entity.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Annotations</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.DependencyResolution</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Design</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Interception</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.MappingViews</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Pluralization</Namespace>
  <Namespace>System.Data.Entity.Migrations</Namespace>
  <Namespace>System.Data.Entity.Migrations.Builders</Namespace>
  <Namespace>System.Data.Entity.Migrations.Design</Namespace>
  <Namespace>System.Data.Entity.Migrations.History</Namespace>
  <Namespace>System.Data.Entity.Migrations.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Migrations.Model</Namespace>
  <Namespace>System.Data.Entity.Migrations.Sql</Namespace>
  <Namespace>System.Data.Entity.Migrations.Utilities</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Configuration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.Entity.Spatial</Namespace>
  <Namespace>System.Data.Entity.Utilities</Namespace>
  <Namespace>System.Data.Entity.Validation</Namespace>
  <Namespace>System.IO.Ports</Namespace>
  <Namespace>System.Timers</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
var personName="????";
	var y = new Thread(t =>
	 {
		 var list = new List<string> { "http://akademik.yok.gov.tr/AkademikArama/", "http://akademik.yok.gov.tr/AkademikArama/view/searchResultview.jsp" };
		 var frm = new Form();
		 frm.Size = new Size(500, 500);
		 var webBrowser = new WebBrowser() { Dock = DockStyle.Fill };
		 frm.Controls.Add(webBrowser);
		 frm.Load += (a, b) =>
		 {
			 webBrowser.Navigate(list[0]);
			 WebBrowserDocumentCompletedEventHandler act = null;
			 Action<Action> act2 = null;
			 act2 = (o) => {
			 	webBrowser.DocumentCompleted -= act;
				o();
				webBrowser.DocumentCompleted += act;
			 };
			 act = (o, e) =>
			 {
				 webBrowser.DocumentCompleted -= act;				 
				 act = (a1, b1) =>
				 {
					 webBrowser.DocumentCompleted -= act;
					 var u1 = webBrowser.Document.InvokeScript("eval", new[] {  "$('.caption a')[0].href" }).Dump();					 
					 act = (a2, b2) =>
					 {
					 	webBrowser.DocumentCompleted -= act;
						 var u2 = webBrowser.Document.InvokeScript("eval", new[] { "$('.table a')[0].href"}).Dump();						 
						 act = (a3, b3) =>
						 {
							 webBrowser.DocumentCompleted -= act;
							 webBrowser.DocumentCompleted += act;
						 };						 
						 webBrowser.DocumentCompleted += act;
						 webBrowser.Navigate(u2.ToString());
					 };					 
					 webBrowser.DocumentCompleted += act;
					 webBrowser.Navigate(u1.ToString());
				 };
				 webBrowser.Document.GetElementById("aramaTerim").SetAttribute("value", personName);
				 webBrowser.Document.GetElementById("searchButton").InvokeMember("click");
				 webBrowser.DocumentCompleted += act;
			 };
			 webBrowser.DocumentCompleted += act;
		 };

		 //frm.Show();
		 //Application.EnableVisualStyles();
		 Application.Run(frm);

	 }
	);
	y.SetApartmentState(ApartmentState.STA);
	y.Start();

	while (true)
	{
		Application.DoEvents();
	}
	//
	var request = (HttpWebRequest)WebRequest.Create("http://akademik.yok.gov.tr/AkademikArama/AkademisyenArama");

	var postData = "aramaTerim="+personName+"&islem=1&yazarCheckbox=on";
	var data = Encoding.ASCII.GetBytes(postData);

	request.Method = "POST";
	request.ContentType = "application/x-www-form-urlencoded";
	request.ContentLength = data.Length;

	using (var stream = request.GetRequestStream())
	{
		stream.Write(data, 0, data.Length);
	}

	var response = (HttpWebResponse)request.GetResponse();

	var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().Dump();


}

// Define other methods and classes here