using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
	private static T instance;

	public static T Instance => instance;

	public static bool IsInitialized => instance != null;

	protected virtual void Awake()
	{
		if (instance != null && instance != this)
		{
			Debug.LogErrorFormat("[Singleton] Trying to instantiate a second instance of singleton class {0}", GetType().Name);
			Object.Destroy(base.gameObject);
		}
		else
		{
			instance = (T)this;
		}
	}

	protected virtual void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}
