using System.Diagnostics;
using UnityEngine;

namespace SRF.Components
{
	public abstract class SRAutoSingleton<T> : SRMonoBehaviour where T : SRAutoSingleton<T>
	{
		private static T _instance;

		public static T Instance
		{
			[DebuggerStepThrough]
			get
			{
				if (_instance == null && Application.isPlaying)
				{
					GameObject gameObject = new GameObject("_" + typeof(T).Name);
					gameObject.AddComponent<T>();
				}
				return _instance;
			}
		}

		public static bool HasInstance
		{
			get
			{
				return _instance != null;
			}
		}

		protected virtual void Awake()
		{
			if (_instance != null)
			{
				UnityEngine.Debug.LogWarning("More than one singleton object of type {0} exists.".Fmt(typeof(T).Name));
			}
			else
			{
				_instance = (T)this;
			}
		}

		private void OnApplicationQuit()
		{
			_instance = (T)null;
		}
	}
}
