using System;
using Poly.Math;

namespace Poly.Physics
{
	public class FastAabbTrigger : WorldObjectImpl, IComparable<FastAabbTrigger>
	{
		public delegate void NodeOverlapCallback(NodeHandle node);

		public delegate void BodyOverlapCallback(Rigidbody body);

		public Bounds2 bounds;

		public Layer layer;

		public NodeOverlapCallback nodeOverlapCallback = delegate
		{
		};

		public BodyOverlapCallback bodyOverlapCallback = delegate
		{
		};

		public string name = DefaultName;

		private static string DefaultName = "AabbTrigger";

		public override bool isDynamic => false;

		public FastAabbTrigger(Bounds2 bounds)
		{
			this.bounds = bounds;
		}

		public int CompareTo(FastAabbTrigger other)
		{
			return bounds.CompareTo(other.bounds);
		}
	}
}
