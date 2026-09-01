using Poly.Base;

namespace Poly.Solver
{
	public struct EdgeSolverInput
	{
		public int numEdges;

		public int numNodes;

		public SolverEdge[] edges;

		public SolverNode[] nodes;

		public SolverSettings settings;

		public bool areEdgesBreakable;

		public EdgeSolverInput(SolverEdge[] edges, FastList<SolverNode> nodes, SolverSettings settings, bool areEdgesBreakable)
		{
			numEdges = edges.Length;
			numNodes = nodes.Count;
			this.edges = edges;
			this.nodes = nodes.array;
			this.settings = settings;
			this.areEdgesBreakable = areEdgesBreakable;
		}
	}
}
