using System;
using System.Collections.Generic;

namespace stock
{
	public class NeuralNetwork
	{
		// inputEdges[hidden][input] is the weight of the edge from the input node to the hidden node
		private IList<IList<double>> inputEdges;
		private IList<IList<double>> outputEdges;
		private IList<IList<IList<double>>> hiddenEdges;

		public void setInputEdges (IList<IList<double>> inputEdges)
		{
			this.inputEdges = deepCopy (inputEdges);
		}

		public void setOutputEdges (IList<IList<double>> outputEdges)
		{
			this.outputEdges = deepCopy (outputEdges);
		}

		public void setHiddenEdges (IList<IList<IList<double>>> hiddenEdges)
		{
			this.hiddenEdges = deepCopy (hiddenEdges);
		}

		public IList<double> evaluate (IList<double> inputs)
		{
			int outputCount = outputEdges.Count;
			int neuronsPerLayer = hiddenEdges [0].Count;

			IList<double> currentLayer = new List<double> (neuronsPerLayer);
			foreach (IList<double> edges in inputEdges) {
				double value = neuron (inputs, edges);
				currentLayer.Add(value);
			}

			foreach (IList<IList<double>> layerEdges in hiddenEdges) {
				IList<double> nextLayer = new List<double> (neuronsPerLayer);
				foreach (IList<double> edges in layerEdges) {
					double value = neuron (currentLayer, edges);
					nextLayer.Add (value);
				}

				currentLayer = nextLayer;
			}

			IList<double> outputs = new List<double> (outputCount);
			foreach (IList<double> edges in outputEdges) {
				double value = neuron (currentLayer, edges);
				outputs.Add (value);
			}

			return outputs;
		}

		private IList<IList<double>> deepCopy (IList<IList<double>> original)
		{
			IList<IList<double>> copy = new List<IList<double>> ();

			foreach (IList<double> row in original) {
				copy.Add (new List<double> (row));
			}

			return copy;
		}

		private IList<IList<IList<double>>> deepCopy (IList<IList<IList<double>>> original)
		{
			IList<IList<IList<double>>> copy = new List<IList<IList<double>>> ();

			foreach (IList<IList<double>> set in original) {
				copy.Add (deepCopy (set));
			}

			return copy;
		}

		private double neuron(IList<double> inputs, IList<double> weights)
		{
			double sum = 0.0;
			for (int i = 0; i < inputs.Count; ++i) {
				sum += inputs [i] * weights [i];
			}

			return activationFunction (sum);
		}

		private double activationFunction(double t)
		{
			return 1.0 / (1 + Math.Exp (-t));
		}
	}
}
