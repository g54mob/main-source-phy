using System.Collections.Generic;
using UnityEngine;

public class BMWidgetPool : MonoBehaviour
{
	public class Pool
	{
		public Transform holder;

		public Queue<GameObject> pool = new Queue<GameObject>();

		private bool hasInitialTransform;

		private string initialName;

		private Vector3 initialPos;

		private Quaternion initialRot;

		private Vector3 initialScale;

		private readonly GameObject prototype;

		public Pool(GameObject p, Transform h)
		{
			holder = h;
			prototype = p;
			if (prototype == null)
			{
				Debug.LogError("[BMWidgetPool] created pool with missing prototype");
			}
		}

		public Pool(string p, Transform h)
		{
			holder = h;
			prototype = Resources.Load<GameObject>(p);
			if (prototype == null)
			{
				Debug.LogError("[BMWidgetPool] created pool but could not find prototype");
			}
		}

		public GameObject Create()
		{
			if (prototype == null)
			{
				Debug.LogError("[BMWidgetPool] tried instantiating from missing prototype");
				return null;
			}
			GameObject gameObject = Object.Instantiate(prototype);
			if (!hasInitialTransform)
			{
				Transform transform = gameObject.transform;
				initialName = gameObject.name;
				initialPos = transform.position;
				initialRot = transform.rotation;
				initialScale = transform.localScale;
				hasInitialTransform = true;
			}
			return gameObject;
		}

		public GameObject Get()
		{
			if (pool.Count == 0)
			{
				return Create();
			}
			return pool.Dequeue();
		}

		public void Add(GameObject go)
		{
			pool.Enqueue(go);
			Transform transform = go.transform;
			go.name = initialName;
			transform.position = initialPos;
			transform.rotation = initialRot;
			transform.localScale = initialScale;
			transform.parent = holder;
		}
	}

	public static BMWidgetPool Instance;

	private Dictionary<string, Pool> objectPool;

	private Transform poolHolder;

	public void Awake()
	{
		Instance = this;
		objectPool = new Dictionary<string, Pool>();
		GameObject gameObject = new GameObject("BMWidgetPool");
		gameObject.transform.parent = base.transform;
		poolHolder = gameObject.transform;
		gameObject.SetActive(false);
	}

	private Transform CreateHolder(string name)
	{
		GameObject gameObject = new GameObject(name);
		Transform transform = gameObject.transform;
		transform.parent = poolHolder;
		return transform;
	}

	public Pool GetPool(GameObject prototype)
	{
		Pool value;
		if (Instance.objectPool.TryGetValue(prototype.name, out value))
		{
			return value;
		}
		Transform h = Instance.CreateHolder(prototype.name);
		value = new Pool(prototype, h);
		Instance.objectPool.Add(prototype.name, value);
		return value;
	}

	public Pool GetPool(string objectLocation)
	{
		Pool value;
		if (Instance.objectPool.TryGetValue(objectLocation, out value))
		{
			return value;
		}
		Transform h = Instance.CreateHolder(objectLocation);
		value = new Pool(objectLocation, h);
		Instance.objectPool.Add(objectLocation, value);
		return value;
	}
}
