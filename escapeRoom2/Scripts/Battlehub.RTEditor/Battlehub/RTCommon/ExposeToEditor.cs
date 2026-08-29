using System;
using System.Collections.Generic;
using System.ComponentModel;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battlehub.RTCommon
{
	[DisallowMultipleComponent]
	public class ExposeToEditor : MonoBehaviour, INotifyPropertyChanged
	{
		public static string HierarchyRootTag = "Respawn";

		[SerializeField]
		[HideInInspector]
		private Collider[] m_colliders;

		private SpriteRenderer m_spriteRenderer;

		private MeshFilter m_filter;

		private SkinnedMeshRenderer m_skinned;

		private static readonly Bounds m_none = default(Bounds);

		public ExposeToEditorUnityEvent Selected;

		public ExposeToEditorUnityEvent Unselected;

		[SerializeField]
		[FormerlySerializedAs("BoundsObject")]
		private GameObject m_boundsObject;

		public BoundsType BoundsType;

		public Bounds CustomBounds;

		public bool CanTransform = true;

		public bool CanInspect = true;

		public bool CanDuplicate = true;

		public bool CanDelete = true;

		public bool CanRename = true;

		public bool CanCreatePrefab = true;

		public bool ShowSelectionGizmo = true;

		[HideInInspector]
		[Obsolete]
		public bool CanSelect = true;

		[HideInInspector]
		public bool CanSnap = true;

		public bool AddColliders = true;

		[SerializeField]
		[HideInInspector]
		private bool m_markAsDestroyed;

		private BoundsType m_effectiveBoundsType;

		private Vector3 m_localEulerAngles;

		private bool m_isDestroyed;

		private bool m_isPaused;

		private ExposeToEditor m_oldParent;

		public static bool _EnableParentChangedEvent { get; set; } = true;

		public Collider[] Colliders
		{
			get
			{
				return m_colliders;
			}
			set
			{
				m_colliders = value;
			}
		}

		public SpriteRenderer SpriteRenderer => m_spriteRenderer;

		public MeshFilter MeshFilter => m_filter;

		public SkinnedMeshRenderer SkinnedMeshRenderer => m_skinned;

		public GameObject BoundsObject
		{
			get
			{
				if (!(m_boundsObject == null))
				{
					return m_boundsObject;
				}
				return base.gameObject;
			}
			set
			{
				m_boundsObject = value;
			}
		}

		public bool MarkAsDestroyed
		{
			get
			{
				if (m_markAsDestroyed)
				{
					return true;
				}
				ExposeToEditor parent = GetParent();
				if (parent != null && parent.m_markAsDestroyed)
				{
					return true;
				}
				return false;
			}
			set
			{
				if (m_markAsDestroyed != value)
				{
					m_markAsDestroyed = value;
					SetHideFlags(this, value);
					if (ExposeToEditor._MarkAsDestroyedChanging != null)
					{
						ExposeToEditor._MarkAsDestroyedChanging(this);
					}
					base.gameObject.SetActive(!m_markAsDestroyed);
					if (ExposeToEditor._MarkAsDestroyedChanged != null)
					{
						ExposeToEditor._MarkAsDestroyedChanged(this);
					}
				}
			}
		}

		public BoundsType EffectiveBoundsType => m_effectiveBoundsType;

		public Bounds Bounds
		{
			get
			{
				if (m_effectiveBoundsType == BoundsType.Any)
				{
					if (m_filter != null && m_filter.sharedMesh != null)
					{
						return m_filter.sharedMesh.bounds;
					}
					if (m_skinned != null && m_skinned.sharedMesh != null)
					{
						return m_skinned.sharedMesh.bounds;
					}
					if (m_spriteRenderer != null)
					{
						return m_spriteRenderer.sprite.bounds;
					}
					if (BoundsObject != null && BoundsObject.transform is RectTransform)
					{
						return BoundsObject.transform.CalculateRelativeRectTransformBounds();
					}
					return CustomBounds;
				}
				if (m_effectiveBoundsType == BoundsType.Mesh)
				{
					if (m_filter != null && m_filter.sharedMesh != null)
					{
						return m_filter.sharedMesh.bounds;
					}
					return m_none;
				}
				if (m_effectiveBoundsType == BoundsType.SkinnedMesh)
				{
					if (m_skinned != null && m_skinned.sharedMesh != null)
					{
						return m_skinned.sharedMesh.bounds;
					}
				}
				else if (m_effectiveBoundsType == BoundsType.Sprite)
				{
					if (m_spriteRenderer != null)
					{
						return m_spriteRenderer.sprite.bounds;
					}
				}
				else if (m_effectiveBoundsType == BoundsType.RectTransform)
				{
					if (BoundsObject != null && BoundsObject.transform is RectTransform)
					{
						return BoundsObject.transform.CalculateRelativeRectTransformBounds();
					}
				}
				else if (m_effectiveBoundsType == BoundsType.Custom)
				{
					return CustomBounds;
				}
				return m_none;
			}
		}

		public Vector3 LocalPosition
		{
			get
			{
				return base.transform.localPosition;
			}
			set
			{
				base.transform.localPosition = value;
			}
		}

		public Vector3 LocalScale
		{
			get
			{
				return base.transform.localScale;
			}
			set
			{
				base.transform.localScale = value;
			}
		}

		public Vector3 LocalEuler
		{
			get
			{
				return GetLocalEulerAngles(syncWithTransform: true);
			}
			set
			{
				SetLocalEulerAngles(value, updateLocalRotation: true);
			}
		}

		public bool ActiveInHierarchy => base.gameObject.activeInHierarchy;

		public bool ActiveSelf
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				if (ActiveSelf != value)
				{
					base.gameObject.SetActive(value);
					RaisePropertyChanged("ActiveSelf");
					RaisePropertyChanged("ActiveInHierarchy");
					RaisePropertyChanged("gameObject");
				}
			}
		}

		public string Name
		{
			get
			{
				if (!(base.gameObject != null))
				{
					return null;
				}
				return base.gameObject.name;
			}
			set
			{
				SetName(value);
			}
		}

		public bool IsAwaked { get; private set; }

		public static event ExposeToEditorEvent _Awaked;

		public static event ExposeToEditorEvent _Destroying;

		public static event ExposeToEditorEvent _Destroyed;

		public static event ExposeToEditorEvent _MarkAsDestroyedChanging;

		public static event ExposeToEditorEvent _MarkAsDestroyedChanged;

		public static event ExposeToEditorEvent _NameChanged;

		public static event ExposeToEditorEvent _TransformChanged;

		public static event ExposeToEditorEvent _Started;

		public static event ExposeToEditorEvent _Enabled;

		public static event ExposeToEditorEvent _Disabled;

		public static event ExposeToEditorChangeEvent<ExposeToEditor> _ParentChanged;

		public static event ExposeToEditorEvent<UnityEngine.Component> _ComponentAdded;

		public static event ExposeToEditorEvent<UnityEngine.Component> _ComponentDestroyed;

		public static event ExposeToEditorEvent<UnityEngine.Component, bool> _ReloadComponentEditor;

		private event PropertyChangedEventHandler m_propertyChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				m_propertyChanged += value;
			}
			remove
			{
				m_propertyChanged -= value;
			}
		}

		private void SetHideFlags(ExposeToEditor obj, bool value)
		{
			if (value)
			{
				obj.gameObject.hideFlags |= HideFlags.DontSave;
				obj.hideFlags |= HideFlags.DontSave;
			}
			else if (obj.gameObject.hideFlags != HideFlags.HideAndDontSave)
			{
				obj.gameObject.hideFlags &= ~HideFlags.DontSave;
				obj.hideFlags &= ~HideFlags.DontSave;
			}
			foreach (ExposeToEditor child in obj.GetChildren(includeMarkedAsDestroyed: true))
			{
				SetHideFlags(child, value);
			}
		}

		public Vector3 GetLocalEulerAngles(bool syncWithTransform = false)
		{
			float num = Quaternion.Angle(base.transform.localRotation, Quaternion.Euler(m_localEulerAngles));
			if (syncWithTransform && num != 0f)
			{
				m_localEulerAngles = base.transform.localEulerAngles;
			}
			return m_localEulerAngles;
		}

		public void SetLocalEulerAngles(Vector3 value, bool updateLocalRotation = false)
		{
			bool flag = m_localEulerAngles != value;
			m_localEulerAngles = value;
			if (updateLocalRotation && flag)
			{
				base.transform.localRotation = Quaternion.Euler(m_localEulerAngles);
			}
		}

		public void SetName(string name, bool ignoreHideFlags = false)
		{
			if (base.gameObject.name != name)
			{
				if (!string.IsNullOrEmpty(name))
				{
					base.gameObject.name = name;
				}
				if ((base.hideFlags & HideFlags.HideInHierarchy) == 0 || ignoreHideFlags)
				{
					ExposeToEditor._NameChanged?.Invoke(this);
					RaisePropertyChanged("Name");
				}
			}
		}

		internal void RaisePropertyChanged(string name)
		{
			this.m_propertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private void Awake()
		{
			IsAwaked = true;
			m_effectiveBoundsType = BoundsType;
			m_filter = BoundsObject.GetComponent<MeshFilter>();
			m_skinned = BoundsObject.GetComponent<SkinnedMeshRenderer>();
			if (m_filter == null && m_skinned == null)
			{
				m_spriteRenderer = BoundsObject.GetComponent<SpriteRenderer>();
			}
			bool flag = (base.hideFlags & HideFlags.HideInHierarchy) == 0;
			if (flag && base.transform.parent != null && base.transform.parent.GetComponent<ExposeToEditor>() == null && base.transform.parent.tag != HierarchyRootTag)
			{
				flag = false;
				Debug.LogWarning(base.gameObject.name + ": parent GameObject is not exposed to editor");
			}
			if (flag)
			{
				ExposeToEditor._Awaked?.Invoke(this);
			}
		}

		private void Start()
		{
			if ((base.hideFlags & HideFlags.HideInHierarchy) == 0)
			{
				ExposeToEditor._Started?.Invoke(this);
			}
		}

		private void OnEnable()
		{
			base.transform.hasChanged = false;
			if ((base.hideFlags & HideFlags.HideInHierarchy) == 0)
			{
				ExposeToEditor._Enabled?.Invoke(this);
			}
		}

		private void OnDisable()
		{
			if ((base.hideFlags & HideFlags.HideInHierarchy) == 0)
			{
				ExposeToEditor._Disabled?.Invoke(this);
			}
		}

		private void OnDestroy()
		{
			m_isDestroyed = true;
			if (!m_isPaused)
			{
				if ((base.hideFlags & HideFlags.HideInHierarchy) == 0)
				{
					ExposeToEditor._Destroying?.Invoke(this);
				}
				if ((base.hideFlags & HideFlags.HideInHierarchy) == 0)
				{
					ExposeToEditor._Destroyed?.Invoke(this);
				}
			}
		}

		private void OnApplicationQuit()
		{
			m_isPaused = true;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (!Application.isEditor)
			{
				m_isPaused = !hasFocus;
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			m_isPaused = pauseStatus;
		}

		private void Update()
		{
			if (ExposeToEditor._TransformChanged != null && base.transform.hasChanged)
			{
				base.transform.hasChanged = false;
				if ((base.hideFlags & HideFlags.HideInHierarchy) == 0 && ExposeToEditor._TransformChanged != null)
				{
					ExposeToEditor._TransformChanged(this);
				}
			}
		}

		private void OnBeforeTransformParentChanged()
		{
			if (_EnableParentChangedEvent && IsAwaked)
			{
				m_oldParent = GetParent(includeMarkedAsDestroyed: true);
			}
		}

		private void OnTransformParentChanged()
		{
			if (_EnableParentChangedEvent && IsAwaked)
			{
				ExposeToEditor parent = GetParent(includeMarkedAsDestroyed: true);
				if (m_oldParent != parent && ExposeToEditor._ParentChanged != null)
				{
					ExposeToEditor._ParentChanged(this, m_oldParent, parent);
				}
				m_oldParent = null;
			}
		}

		public UnityEngine.Component AddComponent(Type type)
		{
			UnityEngine.Component component = base.gameObject.AddComponent(type);
			if (ExposeToEditor._ComponentAdded != null)
			{
				ExposeToEditor._ComponentAdded(this, component);
			}
			return component;
		}

		public void DestroyComponent(UnityEngine.Component component)
		{
			if (component != null)
			{
				UnityEngine.Object.Destroy(component);
				ExposeToEditor._ComponentDestroyed(this, component);
			}
		}

		public void ReloadComponentEditor(UnityEngine.Component component, bool force)
		{
			if (ExposeToEditor._ReloadComponentEditor != null)
			{
				ExposeToEditor._ReloadComponentEditor(this, component, force);
			}
		}

		public ExposeToEditor NextSibling(List<GameObject> rootGameObjects)
		{
			int siblingIndex = base.transform.GetSiblingIndex();
			Transform parent = base.transform.parent;
			if (parent == null)
			{
				for (int i = siblingIndex + 1; i < rootGameObjects.Count; i++)
				{
					if (rootGameObjects[i] != null)
					{
						ExposeToEditor component = rootGameObjects[i].transform.GetComponent<ExposeToEditor>();
						if (component != null && !component.MarkAsDestroyed)
						{
							return component;
						}
					}
				}
			}
			else
			{
				int childCount = parent.childCount;
				for (int j = siblingIndex + 1; j < childCount; j++)
				{
					ExposeToEditor component2 = parent.GetChild(j).GetComponent<ExposeToEditor>();
					if (component2 != null && !component2.MarkAsDestroyed)
					{
						return component2;
					}
				}
			}
			return null;
		}

		public bool IsDescendantOf(GameObject ancestor)
		{
			if (ancestor == null)
			{
				return true;
			}
			return IsDescendantOf(ancestor.transform);
		}

		public bool IsDescendantOf(Transform ancestor)
		{
			if (ancestor == null)
			{
				return true;
			}
			Transform transform = base.transform;
			transform = transform.parent;
			while (transform != null)
			{
				if (transform == ancestor)
				{
					return true;
				}
				transform = transform.parent;
			}
			return false;
		}

		public ExposeToEditor GetParent(bool includeMarkedAsDestroyed = false)
		{
			if (base.transform.parent != null)
			{
				ExposeToEditor component = base.transform.parent.GetComponent<ExposeToEditor>();
				if (component != null && !component.m_isDestroyed && (includeMarkedAsDestroyed || !component.MarkAsDestroyed))
				{
					return component;
				}
			}
			return null;
		}

		public List<ExposeToEditor> GetChildren(bool includeMarkedAsDestroyed = false)
		{
			List<ExposeToEditor> list = new List<ExposeToEditor>();
			foreach (Transform item in base.transform)
			{
				ExposeToEditor component = item.GetComponent<ExposeToEditor>();
				if (component != null && !component.m_isDestroyed && (includeMarkedAsDestroyed || !component.MarkAsDestroyed))
				{
					list.Add(component);
				}
			}
			return list;
		}

		public bool HasChildren(bool includeMarkedAsDestroyed = false)
		{
			foreach (Transform item in base.transform)
			{
				ExposeToEditor component = item.GetComponent<ExposeToEditor>();
				if (component != null && !component.m_isDestroyed && (includeMarkedAsDestroyed || !component.MarkAsDestroyed))
				{
					return true;
				}
			}
			return false;
		}

		public Bounds CalculateBounds(float minBoundsSize = 0.1f)
		{
			Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
			Vector3 localScale = base.gameObject.transform.localScale;
			base.gameObject.transform.localScale = Vector3.one;
			if (componentsInChildren.Length == 0)
			{
				return new Bounds(base.transform.position, Vector2.one * minBoundsSize);
			}
			Bounds bounds = componentsInChildren[0].bounds;
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				bounds.Encapsulate(renderer.bounds);
			}
			base.gameObject.transform.localScale = localScale;
			return bounds;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
