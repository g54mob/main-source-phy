using Poly.Base;

namespace Poly.Physics
{
	public class ActionListener : PolyBehaviour, IActionListener
	{
		private void OnEnable()
		{
			SingletonBehaviour<World>.instance.actionListeners.Add(this);
		}

		private void OnDisable()
		{
			if ((bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.actionListeners != null)
			{
				SingletonBehaviour<World>.instance.actionListeners.Remove(this);
			}
		}

		public virtual void OnActionAdded(Action a)
		{
		}

		public virtual void OnActionRemoved(Action a)
		{
		}
	}
}
