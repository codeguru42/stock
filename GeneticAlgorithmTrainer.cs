using System;
using System.Collections.Generic;

namespace stock
{
	public class GeneticAlgorithmTrainer : ITrainer
	{
		const int DEFAULT_GENE_COUNT = 100;
		const int DEFAULT_GENERATION_COUNT = 1000;
		const double DEFAULT_MUTATION_RATE = 0.01;

		const int DEFAULT_HIDDEN_COUNT = 10;
		const int DEFAULT_LAYER_COUNT = 3;

		public int geneCount { get; set; }
		public int generationCount { get; set; }
		public int survivorCount { get; set; }
		public double mutationRate { get; set; }

		public int hiddenCount { get; set; }
		public int layerCount { get; set; }

		private int geneLength;
		private Random randy = new Random ();
		NeuralNetwork ann;
		IList<IList<double>> inputs;
		IList<double> outputs;
		private List<List<double>> genes;
		IDictionary<IList<double>, double> fitness;

		int inputCount = -1;
		int outputCount = -1;
		int inputEdgeCount = -1;
		int outputEdgeCount = -1;
		int hiddenEdgeCount = -1;

		public GeneticAlgorithmTrainer (int geneLength)
		{
			this.geneLength = geneLength;
			geneCount = DEFAULT_GENE_COUNT;
			generationCount = DEFAULT_GENERATION_COUNT;
			survivorCount = geneCount / 2;

			hiddenCount = DEFAULT_HIDDEN_COUNT;
			layerCount = DEFAULT_LAYER_COUNT;
		}

		public NeuralNetwork train(IList<IList<double>> inputs, IList<double> outputs)
		{
			ann = new NeuralNetwork();
			init ();

			for (int i = 0; i < generationCount; ++i) {
				nextGeneration ();
			}

			return ann;
		}

		private void init ()
		{
			genes = new List<List<double>> ();

			for (int i = 0; i < geneCount; ++i) {
				List<double> gene = new List<double> ();

				for (int j = 0; j < geneLength; ++j) {
					gene.Add (randy.NextDouble ());
				}

				genes.Add (gene);
			}
		}

		private void nextGeneration ()
		{
			getAllFitness ();
			breed ();
			mutate ();
		}

		private void getAllFitness ()
		{
			fitness = new Dictionary<IList<double>, double> ();

			foreach (List<double> gene in genes) {
				fitness.Add (gene, getFitness (gene));
			}
		}

		private void breed ()
		{
			genes.Sort (delegate(List<double> lhs, List<double> rhs) {
				// Sort in descending order by fitness
				return fitness [rhs].CompareTo (fitness [lhs]);
			});

			int lastParent = survivorCount;
			genes = genes.GetRange (0, lastParent);

			for (int i = genes.Count; i < geneCount; i++) {
				int momIndex = randy.Next (lastParent);
				int dadIndex = randy.Next (lastParent);

				List<double> child = genes [momIndex].GetRange (0, geneLength / 2);
				child.AddRange(genes[dadIndex].GetRange(geneLength / 2, geneLength - geneLength / 2));

				genes.Add (child);
			}
		}

		private void mutate ()
		{
			foreach (List<double> gene in genes) {
				double mutationChance = randy.NextDouble ();

				if (mutationChance < mutationRate) {
					int mutationIndex = randy.Next (geneLength);
					double mutation = randy.NextDouble ();
					gene [mutationIndex] = mutation;
				}
			}
		}

		private double getFitness (List<double> gene)
		{
			inputEdgeCount = inputCount * hiddenCount;
			outputEdgeCount = outputCount * hiddenCount;
			hiddenEdgeCount = hiddenCount * hiddenCount;

			IList<IList<double>> inputEdges = getInputEdges (gene);
			IList<IList<IList<double>>> hiddenEdges = getHiddenEdges (gene);
			IList<IList<double>> outputEdges = getOutputEdges (gene);

			ann.setInputEdges (inputEdges);
			ann.setOutputEdges (outputEdges);
			ann.setHiddenEdges (hiddenEdges);

			IList<double> actualOutputs = getOutputs ();

			return distance(actualOutputs, outputs);
		}

		private IList<IList<double>> getInputEdges(List<double> gene)
		{
			IList<IList<double>> inputEdges = new List<IList<double>> (hiddenCount);
			for (int i = 0; i < inputEdgeCount; i += inputCount) {
				inputEdges.Add (gene.GetRange(i, inputCount));
			}
			return inputEdges;
		}

		private IList<IList<IList<double>>> getHiddenEdges(List<double> gene)
		{
			IList<IList<IList<double>>> hiddenEdges = new List<IList<IList<double>>> (hiddenEdgeCount);

			int currIndex = inputEdgeCount;
			for (int i = 0; i < layerCount; ++i) {
				IList<IList<double>> layerEdges = new List<IList<double>> ();

				for (int j = 0; j < hiddenCount; ++j) {
					layerEdges.Add(gene.GetRange(currIndex, hiddenCount));
					currIndex += hiddenCount;
				}

				hiddenEdges.Add (layerEdges);
			}

			return hiddenEdges;
		}

		private IList<IList<double>> getOutputEdges(List<double> gene)
		{
			IList<IList<double>> outputEdges = new List<IList<double>> (hiddenCount);
			for (int i = 0; i < outputEdgeCount; i += outputCount) {
				outputEdges.Add (gene.GetRange(i + inputEdgeCount + hiddenEdgeCount, outputCount));
			}
			return outputEdges;
		}

		private IList<double> getOutputs()
		{
			IList<double> outputs = new List<double>(inputs.Count);
			for (int i = 0; i < inputs.Count; ++i) {
				IList<double> output = ann.evaluate (inputs [i]);
				outputs.Add (output[0]);
			}

			return outputs;
		}

		private double distance(IList<double> x1, IList<double> x2)
		{
			double sum = 0.0;
			for (int i = 0; i < x1.Count; ++i) {
				double diff = x1 [i] - x2 [i];
				sum += diff * diff;
			}

			return Math.Sqrt (sum);
		}
	}
}
