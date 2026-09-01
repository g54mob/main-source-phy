using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMSingleton<T> : MonoBehaviour where T : Component
	{
		protected static T _instance;

		public static bool HasInstance => false;

		public static T Current => null;

		public static T Instance => null;

		public static T TryGetInstance()
		{
			return null;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void InitializeSingleton()
		{
		}
	}
}
