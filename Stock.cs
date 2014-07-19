using System;
using System.IO;
using System.Net;

namespace stock
{
	public class Stock
	{
		private String mSymbol;

		public Stock (String symbol)
		{
			mSymbol = symbol;
		}

		public void getHistory (String startDate, String endDate)
		{
			String yqlQuery = "select Date, Open, Close from yahoo.finance.historicaldata where symbol = \""
				+ mSymbol + "\"  and startDate = \"" + startDate + "\" and endDate = \"" + endDate + "\"";
			String url = "http://query.yahooapis.com/v1/public/yql?q=" + System.Uri.EscapeDataString(yqlQuery)
				+ "&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
			Console.WriteLine(url);

			WebRequest request = WebRequest.Create(url);
			WebResponse response = request.GetResponse();
			StreamReader stream = new StreamReader(response.GetResponseStream());
			Console.WriteLine(stream.ReadToEnd());
			Console.WriteLine("done");
		}
	}
}
