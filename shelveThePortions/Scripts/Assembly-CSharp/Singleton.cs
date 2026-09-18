using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				return null;
			}
			if (instance.Equals(null))
			{
				instance = null;
				return null;
			}
			return instance;
		}
	}

	public static bool IsInitialized => Instance != null;

	protected virtual void Awake()
	{
		if (instance != null && instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		instance = (T)this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	protected virtual void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}
