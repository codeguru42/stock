using Gtk;
using System;

namespace stock
{
	public partial class MainWindow : Gtk.Window
	{
		public static void Main ()
		{
			Application.Init();
			new MainWindow();
			Application.Run();
		}

		public MainWindow () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
