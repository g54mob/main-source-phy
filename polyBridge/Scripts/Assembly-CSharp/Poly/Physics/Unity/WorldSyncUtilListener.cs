using System.Linq;
using UnityEngine;

namespace Poly.Physics.Unity
{
	public class WorldSyncUtilListener : ListenerBase, INodeListener, IEdgeListener
	{
		public Node nodePrefab;

		public Edge edgePrefab;

		public Transform container;

		public static Edge existingEdgeBeingProcessed;

		private void Awake()
		{
			if (!container)
			{
				container = base.transform;
			}
		}

		public virtual void OnNodeAdded(NodeHandle n)
		{
			if (!n.unityNodeComponent)
			{
				WorldSyncUtil.CreateAndBindNodeObject(n, nodePrefab, container).GetComponentsInChildren<Renderer>().ToList()
					.ForEach(delegate(Renderer r)
					{
						r.enabled = n.world.showNodes;
					});
			}
		}

		public virtual void OnNodeRemoved(NodeHandle n)
		{
			if ((bool)n.unityNodeComponent)
			{
				if (n.unityNodeComponent.doNotDestroy)
				{
					n.unityNodeComponent.doNotDestroy = false;
					return;
				}
				Node unityNodeComponent = n.unityNodeComponent;
				WorldSyncUtil.UnbindNode(n, unityNodeComponent);
				Object.Destroy(unityNodeComponent.gameObject);
			}
		}

		public virtual bool OnEdgeBroken(Edge edge)
		{
			return true;
		}

		public virtual void OnEdgeAdded(EdgeHandle e)
		{
			if (!existingEdgeBeingProcessed)
			{
				Edge edge = WorldSyncUtil.CreateAndBindGameObject(e, edgePrefab, container);
				e.world.AddEdge(edge);
			}
			else
			{
				WorldSyncUtil.Bind(e, existingEdgeBeingProcessed);
				existingEdgeBeingProcessed = null;
			}
		}

		public virtual void OnEdgeRemoved(EdgeHandle e)
		{
			if (!existingEdgeBeingProcessed)
			{
				WorldSyncUtil.TryDestroyGameObject(e);
				return;
			}
			WorldSyncUtil.Unbind(e, existingEdgeBeingProcessed);
			existingEdgeBeingProcessed = null;
		}

		public void OnEdgeDetachedFromNode(EdgeHandle e, NodeHandle oldNode)
		{
		}

		public void OnEdgeAttachedToNode(EdgeHandle e, NodeHandle newNode)
		{
		}
	}
}
