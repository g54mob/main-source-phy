using Poly.Base;

namespace Poly.Physics
{
	public class ListenerBase : PolyBehaviour
	{
		protected void OnEnable()
		{
			World instance = SingletonBehaviour<World>.instance;
			if (this is IWorldListener)
			{
				instance.worldListeners.Add(this as IWorldListener);
			}
			if (this is INodeListener)
			{
				instance.nodeListeners.Add(this as INodeListener);
			}
			if (this is IEdgeListener)
			{
				instance.edgeListeners.Add(this as IEdgeListener);
			}
			if (this is IEdgeBreakListener)
			{
				instance.edgeBreakListeners.Add(this as IEdgeBreakListener);
			}
			if (this is IShapeListener)
			{
				instance.shapeListeners.Add(this as IShapeListener);
			}
			if (this is IHydraulicListener)
			{
				instance.hydraulicListeners.Add(this as IHydraulicListener);
			}
			if (this is IActionListener)
			{
				instance.actionListeners.Add(this as IActionListener);
			}
		}

		protected void OnDisable()
		{
			World instance = SingletonBehaviour<World>.instance;
			if ((bool)instance)
			{
				if (this is IWorldListener)
				{
					instance.worldListeners.Remove(this as IWorldListener);
				}
				if (this is INodeListener)
				{
					instance.nodeListeners.Remove(this as INodeListener);
				}
				if (this is IEdgeListener)
				{
					instance.edgeListeners.Remove(this as IEdgeListener);
				}
				if (this is IEdgeBreakListener)
				{
					instance.edgeBreakListeners.Remove(this as IEdgeBreakListener);
				}
				if (this is IShapeListener)
				{
					instance.shapeListeners.Remove(this as IShapeListener);
				}
				if (this is IHydraulicListener)
				{
					instance.hydraulicListeners.Remove(this as IHydraulicListener);
				}
				if (this is IActionListener)
				{
					instance.actionListeners.Remove(this as IActionListener);
				}
			}
		}
	}
}
