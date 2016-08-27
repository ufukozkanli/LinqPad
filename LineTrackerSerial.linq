<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.DataVisualization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.IO.Ports</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	var portName="COM7";
	var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), string.Format("arunio{0}.txt", Environment.TickCount));
	
	var frm = new Form() { StartPosition = FormStartPosition.Manual, Left = 0, Top = 0, Size = new Size(800, 800) };	
	var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
	{
		Name = "Series1" + Environment.TickCount,
		Color = System.Drawing.Color.Green,
		IsVisibleInLegend = false,
		IsXValueIndexed = true,
		ChartType = SeriesChartType.Line,

	};
	{
		var chart1 = new Chart() { Dock = DockStyle.Fill };
		frm.Controls.Add(chart1);
		chart1.ChartAreas.Add(new ChartArea("Test1") { });
		chart1.ChartAreas[0].AxisY.Maximum = 1000;
		chart1.ChartAreas[0].AxisY.Minimum = -1000;
		chart1.Series.Clear();
		chart1.Series.Add(series1);
	}
	


	
	var btn = new Button() { Dock = DockStyle.Left };
	btn.Size = new Size(100, 100);
	frm.Controls.Add(btn);
	btn.Click += (o2, e2) =>
	{			
		var r=new Random();	
		for (int i = 0; i < 100; i++)
		{
			series1.Points.AddXY(r.Next(0,1000), r.Next(0,1000));
		}			
	};

	var port = new SerialPort(portName);
	port.DataReceived += (o, e) =>
	{
		var res = port.ReadExisting().Dump();
		File.AppendAllText(filePath, res);
	};
	try
	{
		port.Open();
	}
	catch (Exception ex)
	{
		ex.Dump();
	}

	Application.Run(frm);

}

// Define other methods and classes here