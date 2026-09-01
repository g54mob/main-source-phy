using Poly.Base;

namespace Poly.Physics.Unity
{
	public class EdgeMassManager : ListenerBase, IEdgeListener
	{
		private void Awake()
		{
		}

		public void OnEdgeAdded(EdgeHandle e)
		{
			ApplyDeltaMass(e, e.node0, 1f);
			ApplyDeltaMass(e, e.node1, 1f);
		}

		public void OnEdgeRemoved(EdgeHandle e)
		{
			ApplyDeltaMass(e, e.node0, -1f);
			ApplyDeltaMass(e, e.node1, -1f);
		}

		public void OnEdgeDetachedFromNode(EdgeHandle e, NodeHandle oldNode)
		{
			ApplyDeltaMass(e, oldNode, -1f);
		}

		public void OnEdgeAttachedToNode(EdgeHandle e, NodeHandle newNode)
		{
			ApplyDeltaMass(e, newNode, 1f);
		}

		private void ApplyDeltaMass(EdgeHandle e, NodeHandle n, float deltaSign)
		{
			EdgeMaterial material = e.material;
			if (!material)
			{
				return;
			}
			float num = 0.5f * (material.baseMass + e.originalLength * material.massPerMeter) * deltaSign;
			if (num != 0f)
			{
				n.SetMass(n.GetMassWhenDynamic() + num);
			}
			World instance = SingletonBehaviour<World>.instance;
			instance.dirtyEdges.Add(e);
			if (num == 0f)
			{
				return;
			}
			foreach (EdgeHandle edge in n.edges)
			{
				instance.dirtyEdges.Add(edge);
			}
		}
	}
}
