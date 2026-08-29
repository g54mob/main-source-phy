using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battlehub.RTCommon
{
	public class RuntimeObjects : MonoBehaviour, IRuntimeObjects
	{
		private IRTE m_editor;

		private ExposeToEditor[] m_enabledObjects;

		private UnityEngine.Object[] m_selectedObjects;

		private HashSet<ExposeToEditor> m_editModeCache;

		private HashSet<ExposeToEditor> m_playModeCache;

		public event ObjectEvent Awaked;

		public event ObjectEvent Started;

		public event ObjectEvent Enabled;

		public event ObjectEvent Disabled;

		public event ObjectEvent Destroying;

		public event ObjectEvent Destroyed;

		public event ObjectEvent MarkAsDestroyedChanging;

		public event ObjectEvent MarkAsDestroyedChanged;

		public event ObjectEvent TransformChanged;

		public event ObjectEvent NameChanged;

		public event ObjectParentChangedEvent ParentChanged;

		public event ObjectEvent<Component> ComponentAdded;

		public event ObjectEvent<Component> ComponentDestroyed;

		public event ObjectEvent<Component, bool> ReloadComponentEditor;

		public IEnumerable<ExposeToEditor> Get(bool rootsOnly, bool useCache)
		{
			if (rootsOnly)
			{
				if (m_editor.IsPlaying)
				{
					if (!useCache)
					{
						throw new InvalidOperationException("Operation is invalid in PlayeMode");
					}
					return m_playModeCache.Where((ExposeToEditor o) => o != null && o.GetParent(includeMarkedAsDestroyed: true) == null);
				}
				if (!useCache)
				{
					List<ExposeToEditor> collection = FindAll();
					m_editModeCache = new HashSet<ExposeToEditor>(collection);
				}
				return m_editModeCache.Where((ExposeToEditor o) => o != null && o.GetParent(includeMarkedAsDestroyed: true) == null);
			}
			if (m_editor.IsPlaying)
			{
				return m_playModeCache;
			}
			return m_editModeCache;
		}

		private void Awake()
		{
			m_editor = IOC.Resolve<IRTE>();
			if (m_editor.IsPlaying || m_editor.IsPlaymodeStateChanging)
			{
				Debug.LogError("Editor should be switched to edit mode");
				return;
			}
			List<ExposeToEditor> collection = FindAll();
			m_editModeCache = new HashSet<ExposeToEditor>(collection);
			m_playModeCache = null;
			OnIsOpenedChanged();
			m_editor.PlaymodeStateChanging += OnPlaymodeStateChanging;
			m_editor.IsOpenedChanged += OnIsOpenedChanged;
			m_editor.ActiveWindowChanged += OnActiveWindowChanged;
			ExposeToEditor._Awaked += OnAwaked;
			ExposeToEditor._Enabled += OnEnabled;
			ExposeToEditor._Started += OnStarted;
			ExposeToEditor._Disabled += OnDisabled;
			ExposeToEditor._Destroying += OnDestroying;
			ExposeToEditor._Destroyed += OnDestroyed;
			ExposeToEditor._MarkAsDestroyedChanging += OnMarkAsDestroyedChanging;
			ExposeToEditor._MarkAsDestroyedChanged += OnMarkAsDestroyedChanged;
			ExposeToEditor._TransformChanged += OnTransformChanged;
			ExposeToEditor._NameChanged += OnNameChanged;
			ExposeToEditor._ParentChanged += OnParentChanged;
			ExposeToEditor._ComponentAdded += OnComponentAdded;
			ExposeToEditor._ComponentDestroyed += OnComponentDestroyed;
			ExposeToEditor._ReloadComponentEditor += OnReloadComponentEditor;
		}

		private void OnDestroy()
		{
			if (m_editor != null)
			{
				m_editor.PlaymodeStateChanging -= OnPlaymodeStateChanging;
				m_editor.IsOpenedChanged -= OnIsOpenedChanged;
				m_editor.ActiveWindowChanged -= OnActiveWindowChanged;
			}
			ExposeToEditor._Awaked -= OnAwaked;
			ExposeToEditor._Enabled -= OnEnabled;
			ExposeToEditor._Started -= OnStarted;
			ExposeToEditor._Disabled -= OnDisabled;
			ExposeToEditor._Destroying -= OnDestroying;
			ExposeToEditor._Destroyed -= OnDestroyed;
			ExposeToEditor._MarkAsDestroyedChanging -= OnMarkAsDestroyedChanging;
			ExposeToEditor._MarkAsDestroyedChanged -= OnMarkAsDestroyedChanged;
			ExposeToEditor._TransformChanged -= OnTransformChanged;
			ExposeToEditor._NameChanged -= OnNameChanged;
			ExposeToEditor._ParentChanged -= OnParentChanged;
			ExposeToEditor._ComponentAdded -= OnComponentAdded;
			ExposeToEditor._ComponentDestroyed -= OnComponentDestroyed;
			ExposeToEditor._ReloadComponentEditor -= OnReloadComponentEditor;
		}

		private void OnIsOpenedChanged()
		{
			if (m_editor.IsOpened)
			{
				if (m_editor.IsApplicationPaused)
				{
					return;
				}
				{
					foreach (ExposeToEditor item in m_editModeCache)
					{
						TryToAddColliders(item);
						item.SendMessage("OnRuntimeEditorOpened", SendMessageOptions.DontRequireReceiver);
					}
					return;
				}
			}
			if (m_editor.IsApplicationPaused)
			{
				return;
			}
			ExposeToEditor[] array = m_editModeCache.ToArray();
			foreach (ExposeToEditor exposeToEditor in array)
			{
				if (exposeToEditor != null)
				{
					TryToDestroyColliders(exposeToEditor);
					exposeToEditor.SendMessage("OnRuntimeEditorClosed", SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		private void OnActiveWindowChanged(RuntimeWindow deactivatedWindow)
		{
			if (!m_editor.IsPlaying)
			{
				return;
			}
			if (m_editor.ActiveWindow != null && m_editor.ActiveWindow.WindowType == RuntimeWindowType.Game)
			{
				foreach (ExposeToEditor item in m_playModeCache)
				{
					item.SendMessage("OnRuntimeActivate", SendMessageOptions.DontRequireReceiver);
				}
				return;
			}
			foreach (ExposeToEditor item2 in m_playModeCache)
			{
				item2.SendMessage("OnRuntimeDeactivate", SendMessageOptions.DontRequireReceiver);
			}
		}

		private void OnPlaymodeStateChanging()
		{
			if (m_editor.IsPlaying)
			{
				m_playModeCache = new HashSet<ExposeToEditor>();
				m_enabledObjects = m_editModeCache.Where((ExposeToEditor eo) => eo != null && eo.gameObject.activeSelf && !eo.MarkAsDestroyed).ToArray();
				m_selectedObjects = m_editor.Selection.objects;
				HashSet<GameObject> hashSet = new HashSet<GameObject>((m_editor.Selection.gameObjects != null) ? m_editor.Selection.gameObjects : new GameObject[0]);
				List<GameObject> list = new List<GameObject>();
				GameObject gameObject = new GameObject("FakeRoot");
				gameObject.SetActive(value: false);
				Transform transform = ((m_editor.SceneRoot != null) ? m_editor.SceneRoot.transform : null);
				IOrderedEnumerable<ExposeToEditor> orderedEnumerable = from eo in m_editModeCache
					where eo != null
					orderby eo.transform.GetSiblingIndex()
					select eo;
				foreach (ExposeToEditor item in orderedEnumerable)
				{
					if (IsRootSceneObject(item, transform))
					{
						item.transform.SetParent(gameObject.transform);
					}
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
				foreach (ExposeToEditor item2 in orderedEnumerable)
				{
					if (IsRootSceneObject(item2, gameObject.transform))
					{
						item2.gameObject.SetActive(value: false);
					}
				}
				for (int num = 0; num < gameObject2.transform.childCount; num++)
				{
					ExposeToEditor component = gameObject2.transform.GetChild(num).GetComponent<ExposeToEditor>();
					ExposeToEditor component2 = gameObject.transform.GetChild(num).GetComponent<ExposeToEditor>();
					component.SetName(component2.name);
					m_playModeCache.Add(component);
					ExposeToEditor[] componentsInChildren = component2.GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
					ExposeToEditor[] componentsInChildren2 = component.GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
					for (int num2 = 0; num2 < componentsInChildren.Length; num2++)
					{
						if (hashSet.Contains(componentsInChildren[num2].gameObject))
						{
							list.Add(componentsInChildren2[num2].gameObject);
						}
					}
				}
				if (m_editor.SceneRoot != null)
				{
					Transform transform2 = gameObject.transform;
					int childCount = transform2.childCount;
					for (int num3 = 0; num3 < childCount; num3++)
					{
						transform2.GetChild(0).SetParent(transform, worldPositionStays: true);
					}
					transform2 = gameObject2.transform;
					childCount = transform2.childCount;
					for (int num4 = 0; num4 < childCount; num4++)
					{
						transform2.GetChild(0).SetParent(transform, worldPositionStays: true);
					}
				}
				gameObject.transform.DetachChildren();
				gameObject2.transform.DetachChildren();
				UnityEngine.Object.Destroy(gameObject);
				UnityEngine.Object.Destroy(gameObject2);
				bool flag = m_editor.Undo.Enabled;
				m_editor.Undo.Enabled = false;
				IRuntimeSelection selection = m_editor.Selection;
				UnityEngine.Object[] objects = list.ToArray();
				selection.objects = objects;
				m_editor.Undo.Enabled = flag;
				m_editor.Undo.Store();
				return;
			}
			foreach (ExposeToEditor item3 in m_playModeCache)
			{
				if (item3 != null)
				{
					item3.SendMessage("OnRuntimeDestroy", SendMessageOptions.DontRequireReceiver);
					UnityEngine.Object.DestroyImmediate(item3.gameObject);
				}
			}
			for (int num5 = 0; num5 < m_enabledObjects.Length; num5++)
			{
				ExposeToEditor exposeToEditor = m_enabledObjects[num5];
				if (exposeToEditor != null)
				{
					exposeToEditor.gameObject.SetActive(value: true);
				}
			}
			bool flag2 = m_editor.Undo.Enabled;
			m_editor.Undo.Enabled = false;
			m_editor.Selection.objects = m_selectedObjects;
			m_editor.Undo.Enabled = flag2;
			m_editor.Undo.Restore();
			m_playModeCache = null;
			m_enabledObjects = null;
			m_selectedObjects = null;
			static bool IsRootSceneObject(ExposeToEditor obj, Transform sceneRoot)
			{
				if (obj.GetParent() == null)
				{
					return obj.transform.parent == sceneRoot;
				}
				return false;
			}
		}

		private static bool HasValidState(ExposeToEditor exposeToEditor)
		{
			if (exposeToEditor != null && !exposeToEditor.MarkAsDestroyed && (exposeToEditor.hideFlags & HideFlags.HideInHierarchy) == 0)
			{
				if (!exposeToEditor.IsAwaked)
				{
					return !exposeToEditor.ActiveInHierarchy;
				}
				return true;
			}
			return false;
		}

		private static List<ExposeToEditor> FindAll()
		{
			if (SceneManager.GetActiveScene().isLoaded)
			{
				return FindAllUsingSceneManagement();
			}
			List<ExposeToEditor> list = new List<ExposeToEditor>();
			ExposeToEditor[] array = Resources.FindObjectsOfTypeAll<ExposeToEditor>();
			foreach (ExposeToEditor exposeToEditor in array)
			{
				if (!(exposeToEditor == null) && HasValidState(exposeToEditor) && !exposeToEditor.gameObject.IsPrefab())
				{
					list.Add(exposeToEditor);
				}
			}
			return list;
		}

		private static List<ExposeToEditor> FindAllUsingSceneManagement()
		{
			List<ExposeToEditor> list = new List<ExposeToEditor>();
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				ExposeToEditor[] componentsInChildren = rootGameObjects[i].GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
				foreach (ExposeToEditor exposeToEditor in componentsInChildren)
				{
					if (HasValidState(exposeToEditor))
					{
						list.Add(exposeToEditor);
					}
				}
			}
			return list;
		}

		private bool CanAddMeshCollider(ExposeToEditor obj, bool isRigidBody)
		{
			if (!obj.AddColliders || isRigidBody)
			{
				return false;
			}
			if (obj.MeshFilter == null && obj.SkinnedMeshRenderer == null)
			{
				return false;
			}
			Mesh mesh = ((obj.MeshFilter != null) ? obj.MeshFilter.sharedMesh : null);
			Mesh mesh2 = ((obj.SkinnedMeshRenderer != null) ? obj.SkinnedMeshRenderer.sharedMesh : null);
			if (mesh != null)
			{
				for (int i = 0; i < mesh.subMeshCount; i++)
				{
					if (mesh.GetTopology(i) != MeshTopology.Triangles)
					{
						return false;
					}
				}
			}
			else if (mesh2 != null)
			{
				for (int j = 0; j < mesh2.subMeshCount; j++)
				{
					if (mesh2.GetTopology(j) != MeshTopology.Triangles)
					{
						return false;
					}
				}
			}
			return true;
		}

		private void TryToAddColliders(ExposeToEditor obj)
		{
			if (obj == null || (obj.Colliders != null && obj.Colliders.Length != 0))
			{
				return;
			}
			List<Collider> list = new List<Collider>();
			bool flag = obj.BoundsObject.GetComponentInParent<Rigidbody>() != null;
			if (obj.EffectiveBoundsType == BoundsType.Any)
			{
				if (obj.MeshFilter != null)
				{
					if (CanAddMeshCollider(obj, flag))
					{
						MeshCollider meshCollider = obj.BoundsObject.AddComponent<MeshCollider>();
						meshCollider.convex = flag;
						meshCollider.sharedMesh = obj.MeshFilter.sharedMesh;
						list.Add(meshCollider);
					}
				}
				else if (obj.SkinnedMeshRenderer != null)
				{
					if (CanAddMeshCollider(obj, flag))
					{
						MeshCollider meshCollider2 = obj.BoundsObject.AddComponent<MeshCollider>();
						meshCollider2.convex = flag;
						meshCollider2.sharedMesh = obj.SkinnedMeshRenderer.sharedMesh;
						list.Add(meshCollider2);
					}
				}
				else if (obj.SpriteRenderer != null && obj.SpriteRenderer.sprite != null && obj.AddColliders && !flag)
				{
					BoxCollider boxCollider = obj.BoundsObject.AddComponent<BoxCollider>();
					boxCollider.size = obj.SpriteRenderer.sprite.bounds.size;
					list.Add(boxCollider);
				}
			}
			else if (obj.EffectiveBoundsType == BoundsType.Mesh)
			{
				if (obj.MeshFilter != null && CanAddMeshCollider(obj, flag))
				{
					MeshCollider meshCollider3 = obj.BoundsObject.AddComponent<MeshCollider>();
					meshCollider3.convex = flag;
					meshCollider3.sharedMesh = obj.MeshFilter.sharedMesh;
					list.Add(meshCollider3);
				}
			}
			else if (obj.EffectiveBoundsType == BoundsType.SkinnedMesh)
			{
				if (obj.SkinnedMeshRenderer != null && CanAddMeshCollider(obj, flag))
				{
					MeshCollider meshCollider4 = obj.BoundsObject.AddComponent<MeshCollider>();
					meshCollider4.convex = flag;
					meshCollider4.sharedMesh = obj.SkinnedMeshRenderer.sharedMesh;
					list.Add(meshCollider4);
				}
			}
			else if (obj.EffectiveBoundsType == BoundsType.Sprite)
			{
				if (obj.SpriteRenderer != null && obj.AddColliders && !flag)
				{
					BoxCollider boxCollider2 = obj.BoundsObject.AddComponent<BoxCollider>();
					boxCollider2.size = obj.SpriteRenderer.sprite.bounds.size;
					list.Add(boxCollider2);
				}
			}
			else if (obj.EffectiveBoundsType == BoundsType.Custom && obj.AddColliders && !flag)
			{
				Mesh sharedMesh = GraphicsUtility.CreateCube(Color.black, obj.CustomBounds.center, 1f, obj.CustomBounds.extents.x * 2f, obj.CustomBounds.extents.y * 2f, obj.CustomBounds.extents.z * 2f);
				MeshCollider meshCollider5 = obj.BoundsObject.AddComponent<MeshCollider>();
				meshCollider5.convex = flag;
				meshCollider5.sharedMesh = sharedMesh;
				list.Add(meshCollider5);
			}
			obj.Colliders = list.ToArray();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].hideFlags = HideFlags.DontSave;
			}
		}

		private void TryToDestroyColliders(ExposeToEditor obj)
		{
			if (!(obj != null) || obj.Colliders == null)
			{
				return;
			}
			for (int i = 0; i < obj.Colliders.Length; i++)
			{
				Collider collider = obj.Colliders[i];
				if (collider != null)
				{
					UnityEngine.Object.Destroy(collider);
				}
			}
			obj.Colliders = null;
		}

		private void OnAwaked(ExposeToEditor obj)
		{
			if (m_editor.IsPlaying || m_editor.IsPlaymodeStateChanging)
			{
				obj.SendMessage("RuntimeAwake", SendMessageOptions.DontRequireReceiver);
				if (!m_playModeCache.Contains(obj))
				{
					m_playModeCache.Add(obj);
				}
			}
			else
			{
				obj.SendMessage("EditorAwake", SendMessageOptions.DontRequireReceiver);
				if (!m_editModeCache.Contains(obj))
				{
					m_editModeCache.Add(obj);
				}
			}
			if (this.Awaked != null)
			{
				this.Awaked(obj);
			}
		}

		private void OnDestroying(ExposeToEditor obj)
		{
			if (this.Destroying != null)
			{
				this.Destroying(obj);
			}
		}

		private void OnDestroyed(ExposeToEditor obj)
		{
			if (m_editor.IsPlaying)
			{
				obj.SendMessage("OnRuntimeDestroy", SendMessageOptions.DontRequireReceiver);
				m_playModeCache?.Remove(obj);
			}
			else
			{
				obj.SendMessage("OnEditorDestroy", SendMessageOptions.DontRequireReceiver);
				if (m_editModeCache.Contains(obj))
				{
					m_editModeCache.Remove(obj);
					TryToDestroyColliders(obj);
				}
			}
			if (m_editor.Selection.IsSelected(obj.gameObject))
			{
				UnityEngine.Object[] array = m_editor.Selection.objects.Where((UnityEngine.Object o) => o != obj.gameObject).ToArray();
				if (array.Length != 0)
				{
					m_editor.Selection.Select(array.First(), array);
				}
				else
				{
					m_editor.Selection.Select(null, null);
				}
			}
			if (this.Destroyed != null)
			{
				this.Destroyed(obj);
			}
		}

		private void OnMarkAsDestroyedChanging(ExposeToEditor obj)
		{
			if (m_editor.IsPlaying)
			{
				if (obj.HasChildren(includeMarkedAsDestroyed: true))
				{
					ExposeToEditor[] componentsInChildren = obj.GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
					if (obj.MarkAsDestroyed)
					{
						foreach (ExposeToEditor exposeToEditor in componentsInChildren)
						{
							m_playModeCache.Remove(exposeToEditor);
							SendMessageTo(exposeToEditor.gameObject, "OnMarkAsDestroyed");
						}
					}
					else
					{
						foreach (ExposeToEditor exposeToEditor2 in componentsInChildren)
						{
							m_playModeCache.Add(exposeToEditor2);
							SendMessageTo(exposeToEditor2.gameObject, "OnMarkAsRestored");
						}
					}
				}
				else if (obj.MarkAsDestroyed)
				{
					m_playModeCache.Remove(obj);
					SendMessageTo(obj.gameObject, "OnMarkAsDestroyed");
				}
				else
				{
					m_playModeCache.Add(obj);
					SendMessageTo(obj.gameObject, "OnMarkAsRestored");
				}
			}
			else if (obj.HasChildren(includeMarkedAsDestroyed: true))
			{
				ExposeToEditor[] componentsInChildren2 = obj.GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
				if (obj.MarkAsDestroyed)
				{
					foreach (ExposeToEditor exposeToEditor3 in componentsInChildren2)
					{
						m_editModeCache.Remove(exposeToEditor3);
						SendMessageTo(exposeToEditor3.gameObject, "OnMarkAsDestroyed");
					}
				}
				else
				{
					foreach (ExposeToEditor exposeToEditor4 in componentsInChildren2)
					{
						m_editModeCache.Add(exposeToEditor4);
						SendMessageTo(exposeToEditor4.gameObject, "OnMarkAsRestored");
					}
				}
			}
			else if (obj.MarkAsDestroyed)
			{
				m_editModeCache.Remove(obj);
				SendMessageTo(obj.gameObject, "OnMarkAsDestroyed");
			}
			else
			{
				m_editModeCache.Add(obj);
				SendMessageTo(obj.gameObject, "OnMarkAsRestored");
			}
			if (this.MarkAsDestroyedChanging != null)
			{
				this.MarkAsDestroyedChanging(obj);
			}
		}

		public void SendMessageTo(GameObject gameobject, string methodName, params object[] parameters)
		{
			MonoBehaviour[] componentsInChildren = gameobject.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
			foreach (MonoBehaviour monoBehaviour in componentsInChildren)
			{
				if (!(monoBehaviour == null))
				{
					InvokeIfExists(monoBehaviour, methodName, parameters);
				}
			}
		}

		private void InvokeIfExists(object objectToCheck, string methodName, params object[] parameters)
		{
			MethodInfo method = objectToCheck.GetType().GetMethod(methodName);
			if (method != null)
			{
				method.Invoke(objectToCheck, parameters);
			}
		}

		private void OnMarkAsDestroyedChanged(ExposeToEditor obj)
		{
			if (this.MarkAsDestroyedChanged != null)
			{
				this.MarkAsDestroyedChanged(obj);
			}
		}

		private void OnEnabled(ExposeToEditor obj)
		{
			if (this.Enabled != null)
			{
				this.Enabled(obj);
			}
		}

		private void OnStarted(ExposeToEditor obj)
		{
			if (m_editor.IsPlaying)
			{
				obj.SendMessage("RuntimeStart", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				obj.SendMessage("EditorStart", SendMessageOptions.DontRequireReceiver);
				TryToDestroyColliders(obj);
				if (m_editor.IsOpened)
				{
					TryToAddColliders(obj);
				}
			}
			if (this.Started != null)
			{
				this.Started(obj);
			}
		}

		private void OnDisabled(ExposeToEditor obj)
		{
			if (this.Disabled != null)
			{
				this.Disabled(obj);
			}
		}

		private void OnTransformChanged(ExposeToEditor obj)
		{
			if (this.TransformChanged != null)
			{
				this.TransformChanged(obj);
			}
		}

		private void OnNameChanged(ExposeToEditor obj)
		{
			if (this.NameChanged != null)
			{
				this.NameChanged(obj);
			}
		}

		private void OnParentChanged(ExposeToEditor obj, ExposeToEditor oldValue, ExposeToEditor newValue)
		{
			if (this.ParentChanged != null)
			{
				this.ParentChanged(obj, oldValue, newValue);
			}
		}

		private void OnComponentAdded(ExposeToEditor obj, Component component)
		{
			if (this.ComponentAdded != null)
			{
				this.ComponentAdded(obj, component);
			}
		}

		private void OnComponentDestroyed(ExposeToEditor obj, Component component)
		{
			if (this.ComponentDestroyed != null)
			{
				this.ComponentDestroyed(obj, component);
			}
		}

		private void OnReloadComponentEditor(ExposeToEditor obj, Component component, bool force)
		{
			if (this.ReloadComponentEditor != null)
			{
				this.ReloadComponentEditor(obj, component, force);
			}
		}
	}
}
