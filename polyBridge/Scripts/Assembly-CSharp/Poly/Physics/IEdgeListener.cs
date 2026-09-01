namespace Poly.Physics
{
	public interface IEdgeListener
	{
		void OnEdgeAdded(EdgeHandle e);

		void OnEdgeRemoved(EdgeHandle e);

		void OnEdgeDetachedFromNode(EdgeHandle e, NodeHandle oldNode);

		void OnEdgeAttachedToNode(EdgeHandle e, NodeHandle newNode);
	}
}
