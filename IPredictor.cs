using System;
using System.Collections.Generic;

namespace stock
{
	public interface IPredictor
	{
		double getPrediction(IList<Quote> quotes);
	}
}
