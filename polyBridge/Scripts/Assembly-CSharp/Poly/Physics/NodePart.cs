using System;

namespace Poly.Physics
{
	[Serializable]
	public struct NodePart
	{
		public Node node;

		public Part part;

		public NodePart(Node node, Part part)
		{
			this.node = node;
			this.part = part;
		}
	}
}
