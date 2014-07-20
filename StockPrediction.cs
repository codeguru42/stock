using System;

namespace stock
{
	public class StockPrediction
	{
		public static void Main ()
		{
			Stock google = new Stock("MSFT");
			google.getHistory("2013-07-18", "2014-07-18");
		}
	}
}
