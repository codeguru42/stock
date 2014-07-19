using Gtk;
using System;
using System.IO;
using System.Net;

namespace stock
{
	public partial class MainWindow : Gtk.Window
	{
		public static void Main ()
		{
			WebRequest request = WebRequest.Create("http://www.google.com");
			WebResponse response = request.GetResponse();
			StreamReader stream = new StreamReader(response.GetResponseStream());
			Console.Write(stream.ReadToEnd());
		}

		static void deleteEvent (object obj, DeleteEventArgs args)
        {
	        Application.Quit ();
        }

		public MainWindow () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
