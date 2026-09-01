using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPersistentSingleton<T> : MonoBehaviour where T : Component
	{
		[Header("Persistent Singleton")]
		[Tooltip("if this is true, this singleton will auto detach if it finds itself parented on awake")]
		public bool AutomaticallyUnparentOnAwake;

		protected static T _instance;

		protected bool _enabled;

		public static bool HasInstance => false;

		public static T Current => null;

		public static T Instance => null;

		protected virtual void Awake()
		{
		}

		protected virtual void InitializeSingleton()
		{
		}
	}
}
