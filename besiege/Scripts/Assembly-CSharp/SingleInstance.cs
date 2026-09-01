using UnityEngine;

public abstract class SingleInstance<T> : SingleInstanceBase where T : SingleInstance<T>
{
	private static T instance;

	protected bool setUp;

	public abstract string Name { get; }

	public static T Instance
	{
		get
		{
			if (SingleInstanceBase.isQuitting && instance == null)
			{
				Debug.LogWarningFormat("Tried to access a null SingleInstance({0}) while quitting.", typeof(T).ToString());
				return (T)null;
			}
			if (instance == null)
			{
				instance = CreateOrFind();
			}
			if (!instance.setUp)
			{
				instance.setUp = true;
				instance.SetUp();
			}
			return instance;
		}
	}

	public static void Initialize()
	{
		if (instance == null)
		{
			instance = CreateOrFind();
		}
	}

	public static void Initialize(T inst)
	{
		if (inst != null)
		{
			instance = inst;
			if (!instance.setUp)
			{
				instance.setUp = true;
				instance.SetUp();
			}
		}
	}

	public static bool hasInstance()
	{
		return !SingleInstanceBase.isQuitting && instance != null;
	}

	private static T CreateOrFind()
	{
		T[] array = Object.FindObjectsOfType<T>();
		if (array.Length > 1)
		{
			Debug.LogWarning("Too many instances of " + typeof(T).Name + ".");
		}
		if (array.Length > 0)
		{
			return array[0];
		}
		T result = new GameObject("SingleInstance<" + typeof(T).Name + "> temp").AddComponent<T>();
		result.gameObject.name = result.Name;
		return result;
	}

	public virtual void SetUp()
	{
	}

	private void OnApplicationQuit()
	{
		SingleInstanceBase.isQuitting = true;
	}
}
