using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-1)]
	public class SpriteGizmoManager : MonoBehaviour, ISpriteGizmoManager
	{
		private readonly Dictionary<Type, string> m_builtIn = new Dictionary<Type, string>
		{
			{
				typeof(Light),
				"BattlehubLightGizmo"
			},
			{
				typeof(Camera),
				"BattlehubCameraGizmo"
			},
			{
				typeof(AudioSource),
				"BattlehubAudioSourceGizmo"
			}
		};

		private Dictionary<Type, Tuple<Mesh, Material>> m_registered = new Dictionary<Type, Tuple<Mesh, Material>>();

		private Dictionary<Type, Tuple<Mesh, Material>> m_typeToMeshAndMaterial;

		private Type[] m_types;

		private IRTE m_editor;

		private IRTEGraphics m_graphics;

		private IMeshesCache m_meshesCache;

		[SerializeField]
		private float m_gizmoScale = 1f;

		public float GizmoScale
		{
			get
			{
				return m_gizmoScale;
			}
			set
			{
				m_gizmoScale = value;
			}
		}

		private void Awake()
		{
			m_editor = IOC.Resolve<IRTE>();
			if (m_editor == null)
			{
				Debug.LogError("RTE is null");
			}
			IOC.RegisterFallback((ISpriteGizmoManager)this);
			AwakeOverride();
		}

		private void Start()
		{
			m_graphics = IOC.Resolve<IRTEGraphics>();
			m_meshesCache = m_graphics.CreateSharedMeshesCache(CameraEvent.BeforeImageEffects);
			m_meshesCache.RefreshMode = CacheRefreshMode.OnTransformChange;
			Refresh();
			StartOverride();
		}

		private void OnDestroy()
		{
			Cleanup();
			Type[] array = m_registered.Keys.ToArray();
			foreach (Type type in array)
			{
				Unregister(type);
			}
			if (m_graphics != null)
			{
				m_graphics.DestroySharedMeshesCache(m_meshesCache);
			}
			m_typeToMeshAndMaterial = null;
			m_types = null;
			OnDestroyOverride();
			IOC.UnregisterFallback((ISpriteGizmoManager)this);
		}

		protected virtual void AwakeOverride()
		{
		}

		protected virtual void StartOverride()
		{
		}

		protected virtual void OnDestroyOverride()
		{
		}

		protected virtual Type[] GetTypes(Type[] types)
		{
			return types;
		}

		public void Register(Type type, Material material)
		{
			if (!material.enableInstancing)
			{
				Debug.LogWarning("material enableInstance == false");
			}
			else
			{
				m_registered[type] = new Tuple<Mesh, Material>(GraphicsUtility.CreateQuad(), material);
			}
		}

		public Material Unregister(Type type)
		{
			if (!m_registered.TryGetValue(type, out var value))
			{
				return null;
			}
			if (value.Item1 != null)
			{
				UnityEngine.Object.Destroy(value.Item1);
			}
			return value.Item2;
		}

		public void Refresh()
		{
			Cleanup();
			Initialize();
		}

		protected virtual void GreateGizmo(GameObject go, Component component, Type type)
		{
			if (m_typeToMeshAndMaterial.TryGetValue(type, out var value))
			{
				SpriteGizmo spriteGizmo = go.GetComponent<SpriteGizmo>();
				if (!spriteGizmo)
				{
					spriteGizmo = go.AddComponent<SpriteGizmo>();
					spriteGizmo.Component = component;
					spriteGizmo.ComponentDestroyed += OnComponentDestroyed;
				}
				spriteGizmo.Mesh = value.Item1;
				m_meshesCache.Add(spriteGizmo.Mesh, spriteGizmo.transform);
				m_meshesCache.SetMaterial(value.Item1, value.Item2);
			}
		}

		protected virtual void DestroyGizmo(GameObject go)
		{
			SpriteGizmo component = go.GetComponent<SpriteGizmo>();
			if ((bool)component)
			{
				UnityEngine.Object.Destroy(component);
				m_meshesCache.Remove(component.Mesh, component.transform);
			}
		}

		private void OnComponentDestroyed(SpriteGizmo gizmo)
		{
			gizmo.ComponentDestroyed -= OnComponentDestroyed;
			UnityEngine.Object.Destroy(gizmo);
			m_meshesCache.Remove(gizmo.Mesh, gizmo.transform);
			m_meshesCache.Refresh();
		}

		private void Initialize()
		{
			if (m_types != null)
			{
				Debug.LogWarning("Already initialized");
				return;
			}
			m_typeToMeshAndMaterial = new Dictionary<Type, Tuple<Mesh, Material>>();
			foreach (KeyValuePair<Type, Tuple<Mesh, Material>> item in m_registered)
			{
				if (item.Value != null)
				{
					m_typeToMeshAndMaterial.Add(item.Key, item.Value);
				}
			}
			foreach (KeyValuePair<Type, string> item2 in m_builtIn)
			{
				if (!m_typeToMeshAndMaterial.ContainsKey(item2.Key))
				{
					Material material = Resources.Load<Material>(item2.Value);
					if (material != null)
					{
						m_typeToMeshAndMaterial.Add(item2.Key, new Tuple<Mesh, Material>(GraphicsUtility.CreateQuad(), material));
					}
				}
			}
			int num = 0;
			m_types = new Type[m_typeToMeshAndMaterial.Count];
			foreach (Type key in m_typeToMeshAndMaterial.Keys)
			{
				m_types[num] = key;
				num++;
			}
			m_types = GetTypes(m_types);
			OnIsOpenedChanged();
			m_editor.IsOpenedChanged += OnIsOpenedChanged;
			m_editor.BeforePlaymodeStateChange += OnBeforePlayModeStateChange;
			m_editor.PlaymodeStateChanged += OnPlayModeStateChanged;
		}

		private void Cleanup()
		{
			if (m_typeToMeshAndMaterial != null)
			{
				foreach (KeyValuePair<Type, Tuple<Mesh, Material>> item2 in m_typeToMeshAndMaterial)
				{
					if (!m_registered.ContainsKey(item2.Key))
					{
						Mesh item = item2.Value.Item1;
						if (item != null)
						{
							UnityEngine.Object.Destroy(item);
						}
					}
				}
			}
			m_types = null;
			m_typeToMeshAndMaterial = null;
			if (m_editor != null)
			{
				m_editor.BeforePlaymodeStateChange -= OnBeforePlayModeStateChange;
				m_editor.IsOpenedChanged -= OnIsOpenedChanged;
				m_editor.PlaymodeStateChanged -= OnPlayModeStateChanged;
			}
			UnsubscribeAndDestroy();
		}

		private void UnsubscribeAndDestroy()
		{
			Unsubscribe();
			SpriteGizmo[] array = Resources.FindObjectsOfTypeAll<SpriteGizmo>();
			foreach (SpriteGizmo spriteGizmo in array)
			{
				if (!spriteGizmo.gameObject.IsPrefab())
				{
					DestroyGizmo(spriteGizmo.gameObject);
				}
			}
		}

		private void OnBeforePlayModeStateChange()
		{
			UnsubscribeAndDestroy();
		}

		private void OnPlayModeStateChanged()
		{
			SubscribeAndCreate();
		}

		private void OnIsOpenedChanged()
		{
			if (m_editor.IsOpened)
			{
				SubscribeAndCreate();
			}
			else
			{
				UnsubscribeAndDestroy();
			}
		}

		private void SubscribeAndCreate()
		{
			IEnumerable<ExposeToEditor> enumerable = m_editor.Object.Get(rootsOnly: false);
			for (int i = 0; i < m_types.Length; i++)
			{
				foreach (ExposeToEditor item in enumerable)
				{
					if (!(item == null))
					{
						Component component = item.GetComponent(m_types[i]);
						if (component != null && item.gameObject.activeInHierarchy)
						{
							GreateGizmo(item.gameObject, component, m_types[i]);
						}
					}
				}
			}
			m_meshesCache.Refresh();
			Subscribe();
		}

		private void Subscribe()
		{
			m_editor.Object.Enabled += OnEnabled;
			m_editor.Object.Disabled += OnDisabled;
			m_editor.Object.ComponentAdded += OnComponentAdded;
		}

		private void Unsubscribe()
		{
			if (m_editor != null && m_editor.Object != null)
			{
				m_editor.Object.Enabled -= OnEnabled;
				m_editor.Object.Disabled -= OnDisabled;
				m_editor.Object.ComponentAdded -= OnComponentAdded;
			}
		}

		private void OnEnabled(ExposeToEditor obj)
		{
			bool flag = false;
			for (int i = 0; i < m_types.Length; i++)
			{
				Component component = obj.GetComponent(m_types[i]);
				if (component != null)
				{
					GreateGizmo(obj.gameObject, component, m_types[i]);
					flag = true;
				}
			}
			if (flag)
			{
				m_meshesCache.Refresh();
			}
		}

		private void OnDisabled(ExposeToEditor obj)
		{
			bool flag = false;
			for (int i = 0; i < m_types.Length; i++)
			{
				if (obj.GetComponent(m_types[i]) != null)
				{
					DestroyGizmo(obj.gameObject);
					flag = true;
				}
			}
			if (flag)
			{
				m_meshesCache.Refresh();
			}
		}

		private void OnComponentAdded(ExposeToEditor obj, Component component)
		{
			if (!(component == null) && Array.IndexOf(m_types, component.GetType()) >= 0)
			{
				GreateGizmo(obj.gameObject, component, component.GetType());
				m_meshesCache.Refresh();
			}
		}
	}
}
