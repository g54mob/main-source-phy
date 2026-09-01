using System.Collections.Generic;
using UnityEngine;

namespace Photon.Bolt
{
	[CreateAssetMenu(fileName = "BoltPrefabDatabase", menuName = "Bolt/Create Prefab Database")]
	public class PrefabDatabase : ScriptableObject
	{
		private const string PrefabDatabaseResourceName = "BoltPrefabDatabase";

		private static PrefabDatabase _instance;

		private static Dictionary<PrefabId, GameObject> _lookup;

		[SerializeField]
		internal PrefabDatabaseMode DatabaseMode;

		[SerializeField]
		internal GameObject[] Prefabs = new GameObject[0];

		[SerializeField]
		internal string[] SearchPaths = new string[0];

		private static ResourceRequest resourceRequest;

		public static PrefabDatabase Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Resources.Load<PrefabDatabase>("BoltPrefabDatabase");
					_ = _instance == null;
				}
				return _instance;
			}
			private set
			{
				_instance = value;
			}
		}

		internal static Dictionary<PrefabId, GameObject> LookUp
		{
			get
			{
				if (_lookup == null)
				{
					_lookup = new Dictionary<PrefabId, GameObject>();
					for (int i = 1; i < Instance.Prefabs.Length; i++)
					{
						GameObject gameObject = Instance.Prefabs[i];
						if ((bool)gameObject)
						{
							BoltEntity component = gameObject.GetComponent<BoltEntity>();
							PrefabId prefabId = component.PrefabId;
							if (_lookup.ContainsKey(prefabId))
							{
								Debug.LogErrorFormat("Duplicate Prefab, ignore {0} for {1} and {2}", prefabId, component, _lookup[prefabId].GetComponent<BoltEntity>());
							}
							else
							{
								_lookup.Add(prefabId, gameObject);
							}
						}
					}
				}
				return _lookup;
			}
			private set
			{
				_lookup = value;
			}
		}

		internal static void BuildCacheAsync()
		{
			if (resourceRequest != null)
			{
				return;
			}
			Debug.Log("Started PrefabDatabased Async loading");
			resourceRequest = Resources.LoadAsync<PrefabDatabase>("BoltPrefabDatabase");
			resourceRequest.completed += delegate(AsyncOperation op)
			{
				if (op.isDone)
				{
					Instance = (PrefabDatabase)resourceRequest.asset;
					Debug.Log("PrefabDatabased async loading done.");
				}
			};
		}

		internal static void BuildCache()
		{
			if (resourceRequest != null)
			{
				while (!resourceRequest.isDone || Instance == null)
				{
				}
				resourceRequest = null;
			}
			Instance = Instance;
			LookUp = LookUp;
		}

		public static GameObject Find(PrefabId id)
		{
			BuildCache();
			if (LookUp.TryGetValue(id, out var value))
			{
				return value;
			}
			return null;
		}

		internal static bool Contains(BoltEntity entity)
		{
			GameObject[] prefabs = Instance.Prefabs;
			if (prefabs == null || entity == null || entity._prefabId >= prefabs.Length || entity._prefabId < 0)
			{
				return false;
			}
			return prefabs[entity._prefabId] == entity.gameObject;
		}
	}
}
