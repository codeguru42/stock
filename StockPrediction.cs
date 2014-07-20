using System;
using System.Collections.Generic;

namespace stock
{
	public class StockPrediction
	{
		public static void Main ()
		{
			DateTime now = DateTime.Today;
			DateTime then = now.AddDays(-5);

			Stock stock = new Stock("MSFT");
			IList<Quote> quotes = stock.getHistory(then, now);

			foreach (Quote quote in quotes) {
				Console.WriteLine (quote);
			}
		}
	}
}
