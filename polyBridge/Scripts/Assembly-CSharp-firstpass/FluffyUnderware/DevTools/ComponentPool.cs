using System;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FluffyUnderware.DevTools
{
	public class ComponentPool : MonoBehaviour, IPool, ISerializationCallbackReceiver
	{
		[SerializeField]
		[HideInInspector]
		private string m_Identifier;

		[Inline]
		[SerializeField]
		private PoolSettings m_Settings;

		private PoolManager mManager;

		private List<Component> mObjects = new List<Component>();

		private double mLastTime;

		private double mDeltaTime;

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

		public string Identifier
		{
			get
			{
				return m_Identifier;
			}
			set
			{
				throw new InvalidOperationException("Component pool's identifier should always indicate the pooled type's assembly qualified name");
			}
		}

		public Type Type
		{
			get
			{
				Type type = Type.GetType(Identifier);
				if (type == null)
				{
					DTLog.LogWarning("[DevTools] ComponentPool's Type is an unknown type " + m_Identifier);
				}
				return type;
			}
		}

		public int Count => mObjects.Count;

		public void Initialize(Type type, PoolSettings settings)
		{
			m_Identifier = type.AssemblyQualifiedName;
			m_Settings = settings;
			mLastTime = DTTime.TimeSinceStartup + (double)UnityEngine.Random.Range(0f, Settings.Speed);
			if (Settings.Prewarm)
			{
				Reset();
			}
		}

		private void Start()
		{
			if (Settings.Prewarm)
			{
				Reset();
			}
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDisable()
		{
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

		public void OnSceneLoaded(Scene scn, LoadSceneMode mode)
		{
			for (int num = mObjects.Count - 1; num >= 0; num--)
			{
				if (mObjects[num] == null)
				{
					mObjects.RemoveAt(num);
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

		public void Push(Component item)
		{
			sendBeforePush(item);
			if (item != null)
			{
				mObjects.Add(item);
				item.transform.parent = Manager.transform;
				item.gameObject.hideFlags = (Settings.Debug ? HideFlags.DontSave : HideFlags.HideAndDontSave);
				if (Settings.AutoEnableDisable)
				{
					item.gameObject.SetActive(value: false);
				}
			}
		}

		public Component Pop(Transform parent = null)
		{
			Component component = null;
			if (Count > 0)
			{
				component = mObjects[0];
				mObjects.RemoveAt(0);
			}
			else if (Settings.AutoCreate || !Application.isPlaying)
			{
				if (Settings.Debug)
				{
					log("Auto create item");
				}
				component = create();
			}
			if ((bool)component)
			{
				component.gameObject.hideFlags = HideFlags.None;
				component.transform.parent = parent;
				if (Settings.AutoEnableDisable)
				{
					component.gameObject.SetActive(value: true);
				}
				sendAfterPop(component);
				if (Settings.Debug)
				{
					log("Pop " + component);
				}
			}
			return component;
		}

		public T Pop<T>(Transform parent) where T : Component
		{
			return Pop(parent) as T;
		}

		private Component create()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = Identifier;
			gameObject.transform.parent = Manager.transform;
			if (Settings.AutoEnableDisable)
			{
				gameObject.SetActive(value: false);
			}
			return gameObject.AddComponent(Type);
		}

		private void destroy(Component item)
		{
			if (item != null)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}

		private void setParent(Component item, Transform parent)
		{
			if (item != null)
			{
				item.transform.parent = parent;
			}
		}

		private void sendAfterPop(Component item)
		{
			GameObject gameObject = item.gameObject;
			if (gameObject.activeSelf && gameObject.activeInHierarchy)
			{
				gameObject.SendMessage("OnAfterPop", SendMessageOptions.DontRequireReceiver);
			}
			else if (item is IPoolable)
			{
				((IPoolable)item).OnAfterPop();
			}
			else
			{
				DTLog.LogWarning("[Curvy] sendAfterPop could not send message because the receiver " + item.name + " is not active");
			}
		}

		private void sendBeforePush(Component item)
		{
			GameObject gameObject = item.gameObject;
			if (gameObject.activeSelf && gameObject.activeInHierarchy)
			{
				gameObject.SendMessage("OnBeforePush", SendMessageOptions.DontRequireReceiver);
			}
			else if (item is IPoolable)
			{
				((IPoolable)item).OnBeforePush();
			}
			else
			{
				DTLog.LogWarning("[Curvy] sendBeforePush could not send message because the receiver " + item.name + " is not active");
			}
		}

		private void log(string msg)
		{
			Debug.Log($"[{Identifier}] ({Count} items) {msg}");
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (!(Type.GetType(m_Identifier) == null))
			{
				return;
			}
			string[] array = m_Identifier.Split(',');
			if (array.Length >= 5)
			{
				string typeName = string.Join(",", array.SubArray(0, array.Length - 4));
				Type type = TypeExt.GetLoadedTypes().FirstOrDefault((Type t) => t.FullName == typeName);
				if (type != null)
				{
					m_Identifier = type.AssemblyQualifiedName;
				}
			}
		}
	}
}
