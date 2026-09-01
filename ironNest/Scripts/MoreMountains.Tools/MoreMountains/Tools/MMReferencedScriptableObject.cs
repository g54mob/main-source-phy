using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMReferencedScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		private MMReferenceHolder<T> _instances;

		private T _typed;

		protected virtual T Typed => null;

		protected virtual void OnReferenced()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisposed()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
