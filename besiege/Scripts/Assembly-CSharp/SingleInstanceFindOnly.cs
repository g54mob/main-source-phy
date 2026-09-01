using UnityEngine;

public abstract class SingleInstanceFindOnly<T> : MonoBehaviour where T : SingleInstanceFindOnly<T>
{
	private static T instance;

	protected bool setUp;

	public abstract string Name { get; }

	public static T Instance
	{
		get
		{
			if (!hasInstance())
			{
				if (Find() && !instance.setUp)
				{
					instance.setUp = true;
					instance.SetUp();
				}
			}
			else if (!instance.setUp)
			{
				instance.setUp = true;
				instance.SetUp();
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		instance = this as T;
		if (!instance.setUp)
		{
			instance.SetUp();
			instance.setUp = true;
		}
	}

	public static void Initialize()
	{
		if (object.ReferenceEquals(instance, null))
		{
			Find();
		}
	}

	public static bool hasInstance()
	{
		return instance != null;
	}

	private static bool Find()
	{
		T[] array = Object.FindObjectsOfType<T>();
		if (array.Length > 1)
		{
			Debug.LogWarning("Too many instances of " + typeof(T).Name + ".");
		}
		if (array.Length > 0)
		{
			instance = array[0];
			return true;
		}
		return false;
	}

	public virtual void SetUp()
	{
	}
}
