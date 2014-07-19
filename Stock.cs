using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
			Stream json = doQuery (startDate, endDate);
			JArray quotes = parse (json);

			Console.WriteLine (quotes);
		}

		private Stream doQuery (String startDate, String endDate)
		{
			String yqlQuery = "select Date, Open, Close from yahoo.finance.historicaldata where symbol = \""
				+ mSymbol + "\"  and startDate = \"" + startDate + "\" and endDate = \"" + endDate + "\"";
			String url = "http://query.yahooapis.com/v1/public/yql?q=" + System.Uri.EscapeDataString(yqlQuery)
				+ "&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

			WebRequest request = WebRequest.Create(url);
			WebResponse response = request.GetResponse();
			return response.GetResponseStream();
		}

		private JArray parse (Stream json)
		{
			JObject obj = (JObject)JToken.ReadFrom(new JsonTextReader(new StreamReader(json)));
			JArray quote = (JArray)obj["query"]["results"]["quote"];

			return quote;
		}
	}
}
