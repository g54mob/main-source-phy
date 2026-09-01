using Poly.Base;
using UnityEngine;

namespace Poly.Physics
{
	public class HydraulicListener : PolyBehaviour, IHydraulicListener
	{
		private void OnEnable()
		{
			SingletonBehaviour<World>.instance.hydraulicListeners.Add(this);
		}

		private void OnDisable()
		{
			if ((bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.hydraulicListeners != null)
			{
				SingletonBehaviour<World>.instance.hydraulicListeners.Remove(this);
			}
		}

		public virtual void OnNodeSplit(Node originalNode, Node additionalNewNode)
		{
			Debug.Log($"Node {originalNode.nameWithId} splits off {additionalNewNode.nameWithId}");
		}

		public virtual void OnNodeJoint__NeverTriggered(Node nodeAboutToBeRemoved, Node existingJointNode)
		{
			Debug.Log($"Node {nodeAboutToBeRemoved.nameWithId} joins into {existingJointNode.nameWithId}");
		}

		public virtual void OnEdgeReattached(Edge edge, Node oldEndpoint, Node newEndpoint)
		{
			Debug.Log($"Edge {edge.nameWithId} reattached from {oldEndpoint.nameWithId} to {newEndpoint.nameWithId}");
		}

		public virtual void OnPhaseStart()
		{
			Debug.Log("Hydraulics Phase Start");
		}

		public virtual void OnPhaseComplete(Node[] mergedNodes_duringLastPhaseOnly)
		{
			Debug.Log("Hydraulics Phase Complete");
		}

		public virtual void OnNodesMergedEarly(Node a, Node b)
		{
			Debug.Log("Hydraulics Early Merge of two nodes");
		}

		public virtual void ClearAndReset()
		{
			Debug.Log("Scene cleared before restarting simulation.");
		}
	}
}
