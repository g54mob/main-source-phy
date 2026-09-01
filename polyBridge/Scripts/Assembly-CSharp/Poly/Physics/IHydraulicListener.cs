using System;

namespace Poly.Physics
{
	public interface IHydraulicListener
	{
		void OnNodeSplit(Node originalNode, Node additionalNewNode);

		[Obsolete]
		void OnNodeJoint__NeverTriggered(Node nodeAboutToBeRemoved, Node existingJointNode);

		void OnEdgeReattached(Edge edge, Node oldEndpoint, Node newEndpoint);

		void OnPhaseStart();

		void OnPhaseComplete(Node[] mergedNodes_duringLastPhaseOnly);

		void OnNodesMergedEarly(Node a, Node b);

		void ClearAndReset();
	}
}
