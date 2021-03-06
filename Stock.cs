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
		const String FORMAT = "yyyy-MM-dd";
		private String mSymbol;

		public Stock (String symbol)
		{
			mSymbol = symbol;
		}

		public List<Quote> getHistory (DateTime startDate, DateTime endDate)
		{
			Stream json = doQuery (startDate.ToString (FORMAT), endDate.ToString (FORMAT));
			return parse (json);
		}

		public Quote getQuote (DateTime date)
		{
			String dateStr = date.ToString (FORMAT);
			Stream json = doQuery (dateStr, dateStr);
			JObject obj = (JObject)JToken.ReadFrom (new JsonTextReader (new StreamReader (json)));
			JToken results = obj ["query"] ["results"];

			if (!results.HasValues) {
				return null;
			} else {
				return parseQuote (((JObject)results) ["quote"]);
			}
		}

		private Stream doQuery (String startDate, String endDate)
		{
			String yqlQuery = "select Date, Open, Close from yahoo.finance.historicaldata where symbol = \""
				+ mSymbol + "\"  and startDate = \"" + startDate + "\" and endDate = \"" + endDate + "\"";
			String url = "http://query.yahooapis.com/v1/public/yql?q=" + System.Uri.EscapeDataString (yqlQuery)
				+ "&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

			WebRequest request = WebRequest.Create (url);
			WebResponse response = request.GetResponse ();
			return response.GetResponseStream ();
		}

		private List<Quote> parse (Stream json)
		{
			JObject obj = (JObject)JToken.ReadFrom (new JsonTextReader (new StreamReader (json)));
			JArray jsonQuotes = (JArray)obj ["query"] ["results"] ["quote"];

			List<Quote> quotes = new List<Quote> ();

			foreach (JToken jsonQuote in jsonQuotes) {
				quotes.Add (parseQuote (jsonQuote));
			}

			return quotes;
		}

		private Quote parseQuote (JToken jsonQuote)
		{
			Quote quote = new Quote ();
			quote.setDate ((string)jsonQuote ["Date"]);
			quote.open = (double)jsonQuote ["Open"];
			quote.close = (double)jsonQuote ["Close"];

			return quote;
		}
	}
}
