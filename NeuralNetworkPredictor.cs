using System;
using System.Collections.Generic;

namespace stock
{
	public class NeuralNetworkPredictor : IPredictor
	{
		const int DEFAULT_LAYER_COUNT = 3;

		private IList<Quote> training;
		private int inputCount;
		private int hiddenCount;
		private int layerCount = DEFAULT_LAYER_COUNT;

		public NeuralNetworkPredictor (IList<Quote> training, int inputCount)
		{
			this.training = training;
			this.inputCount = inputCount;
			this.hiddenCount = inputCount;
			this.train ();
		}

		public double getPrediction (IList<Quote> quotes)
		{
			return -1.0;
		}

		private void train()
		{
		}
	}
}
