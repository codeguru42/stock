using System;

namespace stock
{
	public class Quote
	{
		public const string FORMAT = "yyyy-MM-dd";

		public DateTime date { get; set; }
		public double open { get; set; }
		public double close { get; set; }

		public override String ToString()
		{
			return "{date=" + date + ",open=" + open + ",close=" + close +"}";
		}

		public void setDate(String dateStr)
		{
			date = DateTime.ParseExact (dateStr, FORMAT, null);
		}

		public String getDate() {
			return date.ToString (FORMAT);
		}
	}
}
