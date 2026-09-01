using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[RequireComponent(typeof(PoolManager))]
	public class PrefabPool : MonoBehaviour, IPool
	{
		[FieldCondition("m_Identifier", "", false, ActionAttribute.ActionEnum.ShowWarning, "Please enter an identifier! (Select a prefab to set automatically)", ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private string m_Identifier;

		[SerializeField]
		private List<GameObject> m_Prefabs = new List<GameObject>();

		[Inline]
		[SerializeField]
		private PoolSettings m_Settings;

		private PoolManager mManager;

		private List<GameObject> mObjects = new List<GameObject>();

		private double mLastTime;

		private double mDeltaTime;

		public string Identifier
		{
			get
			{
				return m_Identifier;
			}
			set
			{
				if (m_Identifier != value)
				{
					string ident = value;
					if (string.IsNullOrEmpty(m_Identifier))
					{
						ident = Manager.GetUniqueIdentifier(ident);
					}
					m_Identifier = value;
				}
			}
		}

		public List<GameObject> Prefabs
		{
			get
			{
				return m_Prefabs;
			}
			set
			{
				if (m_Prefabs != value)
				{
					m_Prefabs = value;
				}
			}
		}

		public PoolSettings Settings
		{
			get
			{
				return m_Settings;
			}
			set
			{
				if (m_Settings != value)
				{
					m_Settings = value;
				}
				if (m_Settings != null)
				{
					m_Settings.OnValidate();
				}
			}
		}

		public PoolManager Manager
		{
			get
			{
				if (mManager == null)
				{
					mManager = GetComponent<PoolManager>();
				}
				return mManager;
			}
		}

		public int Count => mObjects.Count;

		private void Awake()
		{
		}

		private void Start()
		{
			if (Settings.Prewarm)
			{
				Reset();
			}
		}

		public void Initialize(string ident, PoolSettings settings, params GameObject[] prefabs)
		{
			Identifier = ident;
			m_Settings = settings;
			Prefabs = new List<GameObject>(prefabs);
			mLastTime = DTTime.TimeSinceStartup + (double)UnityEngine.Random.Range(0f, Settings.Speed);
			if (Settings.Prewarm)
			{
				Reset();
			}
		}

		public void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			mDeltaTime += DTTime.TimeSinceStartup - mLastTime;
			mLastTime = DTTime.TimeSinceStartup;
			if (Settings.Speed > 0f)
			{
				int num = (int)(mDeltaTime / (double)Settings.Speed);
				mDeltaTime -= num;
				if (Count > Settings.Threshold)
				{
					num = Mathf.Min(num, Count - Settings.Threshold);
					while (num-- > 0)
					{
						if (Settings.Debug)
						{
							log("Threshold exceeded: Deleting item");
						}
						destroy(mObjects[0]);
						mObjects.RemoveAt(0);
					}
				}
				else
				{
					if (Count >= Settings.MinItems)
					{
						return;
					}
					num = Mathf.Min(num, Settings.MinItems - Count);
					while (num-- > 0)
					{
						if (Settings.Debug)
						{
							log("Below MinItems: Adding item");
						}
						mObjects.Add(create());
					}
				}
			}
			else
			{
				mDeltaTime = 0.0;
			}
		}

		public void Reset()
		{
			if (Application.isPlaying)
			{
				while (Count < Settings.MinItems)
				{
					mObjects.Add(create());
				}
				while (Count > Settings.Threshold)
				{
					destroy(mObjects[0]);
					mObjects.RemoveAt(0);
				}
				if (Settings.Debug)
				{
					log("Prewarm/Reset");
				}
			}
		}

		public void Clear()
		{
			if (Settings.Debug)
			{
				log("Clear");
			}
			for (int i = 0; i < Count; i++)
			{
				destroy(mObjects[i]);
			}
			mObjects.Clear();
		}

		public GameObject Pop(Transform parent = null)
		{
			GameObject gameObject = null;
			if (Count > 0)
			{
				gameObject = mObjects[0];
				mObjects.RemoveAt(0);
			}
			else if (Settings.AutoCreate || !Application.isPlaying)
			{
				if (Settings.Debug)
				{
					log("Auto create item");
				}
				gameObject = create();
			}
			if ((bool)gameObject)
			{
				gameObject.gameObject.hideFlags = HideFlags.None;
				gameObject.transform.parent = parent;
				if (Settings.AutoEnableDisable)
				{
					gameObject.SetActive(value: true);
				}
				sendAfterPop(gameObject);
				if (Settings.Debug)
				{
					log("Pop " + gameObject);
				}
			}
			return gameObject;
		}

		public virtual void Push(GameObject item)
		{
			if (Settings.Debug)
			{
				log("Push " + item);
			}
			if (item != null)
			{
				sendBeforePush(item);
				mObjects.Add(item);
				item.transform.parent = base.transform;
				item.gameObject.hideFlags = (Settings.Debug ? HideFlags.DontSave : HideFlags.HideAndDontSave);
				if (Settings.AutoEnableDisable)
				{
					item.SetActive(value: false);
				}
			}
		}

		private GameObject create()
		{
			if (Prefabs.Count == 0)
			{
				throw new InvalidOperationException($"[Curvy] The Prefab Pool '{Identifier}' in game object '{base.gameObject.name}' could not create a pool element because its Prefabs list is empty");
			}
			GameObject gameObject = Prefabs[UnityEngine.Random.Range(0, Prefabs.Count)];
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
			gameObject2.name = gameObject.name;
			gameObject2.transform.parent = base.transform;
			if (Settings.AutoEnableDisable)
			{
				gameObject2.SetActive(value: false);
			}
			return gameObject2;
		}

		private void destroy(GameObject go)
		{
			UnityEngine.Object.Destroy(go);
		}

		private void log(string msg)
		{
			Debug.Log($"[{Identifier}] ({Count} items) {msg}");
		}

		private void setParent(Transform item, Transform parent)
		{
			if (item != null)
			{
				item.parent = parent;
			}
		}

		private void sendAfterPop(GameObject item)
		{
			item.SendMessage("OnAfterPop", SendMessageOptions.DontRequireReceiver);
		}

		private void sendBeforePush(GameObject item)
		{
			item.SendMessage("OnBeforePush", SendMessageOptions.DontRequireReceiver);
		}
	}
}
