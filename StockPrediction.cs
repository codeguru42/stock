using System;
using System.Collections.Generic;

namespace stock
{
	public class StockPrediction
	{
		public static void Main ()
		{
			Stock google = new Stock("MSFT");
			IList<Quote> quotes = google.getHistory("2013-07-18", "2014-07-18");

			foreach (Quote quote in quotes) {
				Console.WriteLine (quote);
			}
		}
	}
}
