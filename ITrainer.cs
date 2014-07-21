using System;
using System.Collections.Generic;

namespace stock
{
	public interface ITrainer
	{
		NeuralNetwork train(IList<IList<double>> inputs, IList<double> outputs);
	}
}
