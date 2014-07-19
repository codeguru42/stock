using Gtk;
using System;

namespace stock
{
	public partial class MainWindow : Gtk.Window
	{
		public static void Main ()
		{
			Stock google = new Stock("YHOO");
			google.getHistory("2013-07-18", "2014-07-18");
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
