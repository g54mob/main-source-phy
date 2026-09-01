using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPersistentHumbleSingleton<T> : MonoBehaviour where T : Component
	{
		protected static T _instance;

		[MMReadOnly]
		public float InitializationTime;

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
