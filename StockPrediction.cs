using System;
using System.Collections.Generic;

namespace stock
{
	public class StockPrediction
	{
		public static void Main ()
		{
			DateTime today = DateTime.Today;
			Stock stock = new Stock ("MSFT");
			List<Quote> quotes = stock.getHistory (today.AddYears(-1), today);
			int range = 7;

			for (int i = 0; i < quotes.Count - range - 1; i++) {
				IPredictor predictor = new LeastSquaresPredictor (quotes.GetRange(i, range));

				double prediction = predictor.getPrediction ();
				Quote actual = quotes[i + range + 1];
				double error = actual .close - prediction;

				Console.WriteLine ("prediction=" + prediction + " actual=" + actual.close + " error=" + error
				                   + " %error=" + (error/actual.close));
			}
		}
	}
}
