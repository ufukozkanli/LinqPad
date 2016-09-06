<Query Kind="Program">
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Diagnostics</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Sockets</Namespace>
  <Namespace>System.Text</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	Application.Run(new Form1());
}
public class Form1 : System.Windows.Forms.Form
{
	private System.Windows.Forms.NotifyIcon notifyIcon1;
	private System.Windows.Forms.ContextMenu contextMenu1;
	private System.Windows.Forms.MenuItem menuItem1;
	private System.ComponentModel.IContainer components;


	public Form1()
	{
		var timer = new System.Windows.Forms.Timer() { Enabled = true, Interval = 5000 };
        timer.Tick += (o, e) =>
		{
			timer.Interval=timer.Interval*2;
			notifyIcon1.ShowBalloonTip(0,"Course Based Studying","Like Book Indexing",ToolTipIcon.Info);
		};
        this.components = new System.ComponentModel.Container();
		this.contextMenu1 = new System.Windows.Forms.ContextMenu();
		this.menuItem1 = new System.Windows.Forms.MenuItem();

		// Initialize contextMenu1
		this.contextMenu1.MenuItems.AddRange(
					new System.Windows.Forms.MenuItem[] { this.menuItem1 });

		// Initialize menuItem1
		this.menuItem1.Index = 0;
		this.menuItem1.Text = "E&xit";
		this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

		// Set up how the form should be displayed.
		this.ClientSize = new System.Drawing.Size(292, 266);
		this.Text = "Notify Icon Example";

		// Create the NotifyIcon.
		this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

		// The Icon property sets the icon that will appear
		// in the systray for this application.
		notifyIcon1.Icon = this.Icon;

		// The ContextMenu property sets the menu that will
		// appear when the systray icon is right clicked.
		notifyIcon1.ContextMenu = this.contextMenu1;

		// The Text property sets the text that will be displayed,
		// in a tooltip, when the mouse hovers over the systray icon.
		notifyIcon1.Text = "Form1 (NotifyIcon example)";
		notifyIcon1.Visible = true;

		// Handle the DoubleClick event to activate the form.
		notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);

	}

	protected override void Dispose(bool disposing)
	{
		// Clean up any components being used.
		if (disposing)
			if (components != null)
				components.Dispose();

		base.Dispose(disposing);
	}

	private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
	{
		// Show the form when the user double clicks on the notify icon.

		// Set the WindowState to normal if the form is minimized.
		if (this.WindowState == FormWindowState.Minimized)
			this.WindowState = FormWindowState.Normal;

		// Activate the form.
		this.Activate();
	}

	private void menuItem1_Click(object Sender, EventArgs e)
	{
		// Close the form, which closes the application.
		this.Close();
	}
}