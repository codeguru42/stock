using System;

namespace stock
{
	public class Quote
	{
		public String date { get; set; }
		public double open { get; set; }
		public double close { get; set; }

		public override String ToString()
		{
			return "{date=" + date + ",open=" + open + ",close=" + close +"}";
		}
	}
}

