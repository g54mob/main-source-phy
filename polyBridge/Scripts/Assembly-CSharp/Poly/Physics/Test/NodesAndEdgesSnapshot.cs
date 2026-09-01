using System;

namespace Poly.Physics.Test
{
	[Serializable]
	public struct NodesAndEdgesSnapshot
	{
		public NodeDef[] nodes;

		public EdgeDef[] edges;
	}
}
