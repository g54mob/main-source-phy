using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class DTSingleton<T> : MonoBehaviour, IDTSingleton where T : MonoBehaviour, IDTSingleton
	{
		private static T _instance;

		private static object _lock = new object();

		private static bool applicationIsQuitting = false;

		private bool isDuplicateInstance;

		public static bool HasInstance => _instance != null;

		public static T Instance
		{
			get
			{
				if (!Application.isPlaying)
				{
					applicationIsQuitting = false;
				}
				if (applicationIsQuitting)
				{
					return null;
				}
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
						{
							Object[] array = Object.FindObjectsOfType(typeof(T));
							_instance = ((array.Length >= 1) ? ((T)array[0]) : new GameObject().AddComponent<T>());
						}
					}
				}
				return _instance;
			}
		}

		public virtual void Awake()
		{
			T instance = Instance;
			lock (_lock)
			{
				if (GetInstanceID() != instance.GetInstanceID())
				{
					instance.MergeDoubleLoaded(this);
					isDuplicateInstance = true;
					Invoke("DestroySelf", 0f);
				}
			}
		}

		protected virtual void OnDestroy()
		{
			lock (_lock)
			{
				if (Application.isPlaying && !isDuplicateInstance)
				{
					applicationIsQuitting = true;
					_instance = null;
				}
			}
		}

		public virtual void MergeDoubleLoaded(IDTSingleton newInstance)
		{
		}

		private void DestroySelf()
		{
			Object.Destroy(base.gameObject);
		}
	}
}
