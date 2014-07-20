using System;
using System.Collections.Generic;

namespace stock
{
	public class LeastSquaresPredictor : IPredictor
	{
		private List<Quote> quotes;

		public LeastSquaresPredictor (IList<Quote> quotes)
		{
			this.quotes = new List<Quote> (quotes);
			this.quotes.Sort ();
		}

		public double getPrediction ()
		{
			IList<Point> points = getPoints ();

			double xsum = 0.0;
			double ysum = 0.0;
			foreach (Point point in points) {
				xsum += point.x;
				ysum += point.y;
			}

			double n = points.Count;
			double xbar = xsum / n;
			double ybar = ysum / n;

			double SSxy = 0.0;
			double SSxx = 0.0;
			foreach (Point point in points) {
				SSxy += (point.x - xbar) * (point.y - xbar);
				SSxx += (point.x - xbar) * (point.x - xbar);
			}

			double b = SSxy / SSxx;
			double a = ybar - b * xbar;

			double x = points [points.Count - 1].x + 1;

			return a + b * x;
		}

		private IList<Point> getPoints ()
		{
			IList<Point> points = new List<Point> ();
			long min = quotes [0].date.Ticks;

			foreach (Quote quote in quotes) {
				Point p = new Point ();
				p.x = quote.date.Ticks - min;
				p.y = quote.close;
				points.Add (p);
			}

			return points;
		}

		private class Point
		{
			public double x { get; set; }

			public double y { get; set; }
		}
	}
}

