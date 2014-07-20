using System;
using System.Collections.Generic;

namespace stock
{
	public class StockPrediction
	{
		public static void Main (String[] args)
		{
			DateTime today = DateTime.Today;
			Stock stock = new Stock (args[0]);
			List<Quote> quotes = stock.getHistory (today.AddYears(-1), today);
			quotes.Sort ();
			int range = 5;
			IPredictor predictor = new LeastSquaresPredictor ();

			Console.WriteLine("date,prediction,actual,error,%error");
			for (int i = 0; i < quotes.Count - range - 1; i++) {
				double prediction = predictor.getPrediction (quotes.GetRange(i, range));
				Quote actual = quotes[i + range + 1];
				double error = actual .close - prediction;

				Console.WriteLine (actual.date + "," + prediction + "," + actual.close + ","
				                   + error + "," + (error/actual.close));
			}
		}
	}
}
