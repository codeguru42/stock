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

		public void getHistory ()
		{
			String yqlQuery = "select * " +
				"from csv " +
				"where url='http://download.finance.yahoo.com/d/quotes.csv?s=" + mSymbol
					+ "&f=sl1d1t1c1ohgv&e=.csv' and columns='symbol,price,date,time,change,col1,high,low,col2'";
			String url = "http://query.yahooapis.com/v1/public/yql?q=" + System.Uri.EscapeDataString(yqlQuery)
				+ "&format=json";
			WebRequest request = WebRequest.Create(url);
			WebResponse response = request.GetResponse();
			StreamReader stream = new StreamReader(response.GetResponseStream());
			Console.Write(stream.ReadToEnd());
		}
	}
}
