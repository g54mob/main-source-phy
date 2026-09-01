namespace Poly.Physics
{
	public interface INodeListener
	{
		void OnNodeAdded(NodeHandle n);

		void OnNodeRemoved(NodeHandle n);
	}
}
