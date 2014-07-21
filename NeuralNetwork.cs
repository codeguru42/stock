using System;
using System.Collections.Generic;

namespace stock
{
	public class NeuralNetwork
	{
		private IList<IList<double>> inputEdges;
		private IList<IList<double>> outputEdges;
		private IList<IList<double>> hiddenEdges;

		public void setInputEdges(IList<IList<double>> inputEdges)
		{
			this.inputEdges = deepCopy (inputEdges);
		}

		public void setOutputEdges(IList<IList<double>> outputEdges)
		{
			this.outputEdges = deepCopy (outputEdges);
		}

		public void setHiddenEdges(IList<IList<double>> hiddenEdges)
		{
			this.hiddenEdges = deepCopy (hiddenEdges);
		}

		public IList<double> evaluate(IList<double> inputs) {
		}

		private IList<IList<double>> deepCopy(IList<IList<double>> original)
		{
			IList<IList<double>> copy = new List<IList<double>> ();

			foreach (IList<double> row in hiddenEdges) {
				copy.Add (new List<double> (row));
			}
		}
	}
}
