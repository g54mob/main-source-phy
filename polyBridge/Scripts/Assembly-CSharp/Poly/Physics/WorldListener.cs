using Poly.Base;

namespace Poly.Physics
{
	public class WorldListener : PolyBehaviour, IWorldListener
	{
		protected void OnEnable()
		{
			SingletonBehaviour<World>.instance.worldListeners.Add(this);
		}

		protected void OnDisable()
		{
			if (SingletonBehaviour<World>.instanceExists && SingletonBehaviour<World>.instance.worldListeners != null)
			{
				SingletonBehaviour<World>.instance.worldListeners.Remove(this);
			}
		}

		public virtual void BeforeStep()
		{
		}

		public virtual void AfterWorldCleared()
		{
		}

		public virtual void AfterWorldFrameUpdate()
		{
		}

		public virtual void AfterWorldFixedUpdate()
		{
		}
	}
}
