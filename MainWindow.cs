using Gtk;
using System;

namespace stock
{
	public partial class MainWindow : Gtk.Window
	{
		public static void Main ()
		{
			Stock google = new Stock("GOOG");
			google.getHistory();
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
