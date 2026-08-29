using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace Battlehub.RTHandles.HDRP
{
	public class OutlineManagerHDRP : MonoBehaviour, IOutlineManager
	{
		private IRenderersCache m_cache;

		private IRTE m_editor;

		private IRTEGraphics m_graphics;

		[SerializeField]
		private Material m_selectionMaterial;

		private IRuntimeSelection m_selectionOverride;

		public Material SelectionMaterial
		{
			get
			{
				return m_selectionMaterial;
			}
			set
			{
				m_selectionMaterial = value;
			}
		}

		public IRuntimeSelection Selection
		{
			get
			{
				if (m_selectionOverride != null)
				{
					return m_selectionOverride;
				}
				return m_editor.Selection;
			}
			set
			{
				if (m_selectionOverride != value)
				{
					if (m_selectionOverride != null)
					{
						m_selectionOverride.SelectionChanged -= OnSelectionChanged;
					}
					m_selectionOverride = value;
					if (m_selectionOverride == m_editor.Selection)
					{
						m_selectionOverride = null;
					}
					if (m_selectionOverride != null)
					{
						m_selectionOverride.SelectionChanged += OnSelectionChanged;
					}
				}
			}
		}

		private void Start()
		{
			m_graphics = IOC.Resolve<IRTEGraphics>();
			m_cache = m_graphics.CreateSharedRenderersCache(CameraEvent.AfterEverything);
			m_cache.MaterialOverride = m_selectionMaterial;
			m_editor = IOC.Resolve<IRTE>();
			TryToAddRenderers(m_editor.Selection);
			m_editor.Selection.SelectionChanged += OnRuntimeEditorSelectionChanged;
			IOC.RegisterFallback((IOutlineManager)this);
			StartCoroutine(EnableSelectionFullScreenPass());
		}

		private void OnDestroy()
		{
			if (m_editor != null)
			{
				if (m_editor.Selection != null)
				{
					m_editor.Selection.SelectionChanged -= OnRuntimeEditorSelectionChanged;
				}
				if (m_editor.Object != null)
				{
					m_editor.Object.Enabled -= OnObjectEnabled;
					m_editor.Object.Disabled -= OnObjectDisabled;
				}
			}
			if (m_selectionOverride != null)
			{
				m_selectionOverride.SelectionChanged -= OnSelectionChanged;
			}
			if (m_graphics != null)
			{
				m_graphics.DestroySharedRenderersCache(m_cache);
			}
			IOC.UnregisterFallback((IOutlineManager)this);
		}

		private IEnumerator EnableSelectionFullScreenPass()
		{
			yield return new WaitForEndOfFrame();
			CustomPassVolume[] components = GetComponents<CustomPassVolume>();
			for (int i = 0; i < components.Length; i++)
			{
				foreach (CustomPass customPass in components[i].customPasses)
				{
					if (customPass.name == "SelectionFullScreenPass")
					{
						customPass.enabled = true;
					}
				}
			}
		}

		private void OnObjectEnabled(ExposeToEditor obj)
		{
			if (m_selectionOverride != null)
			{
				OnSelectionChanged(m_selectionOverride.objects);
			}
			else
			{
				OnRuntimeEditorSelectionChanged(m_editor.Selection.objects);
			}
		}

		private void OnObjectDisabled(ExposeToEditor obj)
		{
			if (m_selectionOverride != null)
			{
				OnSelectionChanged(m_selectionOverride.objects);
			}
			else
			{
				OnRuntimeEditorSelectionChanged(m_editor.Selection.objects);
			}
		}

		private void OnRuntimeEditorSelectionChanged(Object[] unselectedObject)
		{
			OnSelectionChanged(m_editor.Selection, unselectedObject);
		}

		private void OnSelectionChanged(Object[] unselectedObjects)
		{
			OnSelectionChanged(m_selectionOverride, unselectedObjects);
		}

		private void OnSelectionChanged(IRuntimeSelection selection, Object[] unselectedObjects)
		{
			TryToRemoveRenderers(unselectedObjects);
			TryToAddRenderers(selection);
		}

		private void TryToRemoveRenderers(Object[] unselectedObjects)
		{
			if (unselectedObjects != null)
			{
				Renderer[] array = (from go in unselectedObjects
					select go as GameObject into go
					where go != null
					select go).SelectMany((GameObject go) => go.GetComponentsInChildren<Renderer>(includeInactive: true)).ToArray();
				foreach (Renderer renderer in array)
				{
					m_cache.Remove(renderer);
				}
			}
		}

		private void TryToAddRenderers(IRuntimeSelection selection)
		{
			if (selection.gameObjects != null)
			{
				IList<Renderer> renderers = GetRenderers(selection.gameObjects);
				for (int i = 0; i < renderers.Count; i++)
				{
					Renderer renderer = renderers[i];
					m_cache.Add(renderer);
				}
			}
		}

		private IList<GameObject> FilterSelection(IList<GameObject> gameObjects)
		{
			IList<GameObject> list = new List<GameObject>();
			for (int i = 0; i < gameObjects.Count; i++)
			{
				GameObject gameObject = gameObjects[i];
				if (!(gameObject == null) && !gameObject.IsPrefab() && (gameObject.hideFlags & HideFlags.HideInHierarchy) == 0)
				{
					ExposeToEditor component = gameObject.GetComponent<ExposeToEditor>();
					if (component == null || component.ShowSelectionGizmo)
					{
						list.Add(gameObject);
					}
				}
			}
			return list;
		}

		private IList<Renderer> GetRenderers(IList<GameObject> gameObjects)
		{
			List<Renderer> list = new List<Renderer>();
			gameObjects = FilterSelection(gameObjects);
			for (int i = 0; i < gameObjects.Count; i++)
			{
				Renderer[] componentsInChildren = gameObjects[i].GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer in componentsInChildren)
				{
					if (renderer.gameObject.activeInHierarchy && (renderer.gameObject.hideFlags & HideFlags.HideInHierarchy) == 0)
					{
						list.Add(renderer);
					}
				}
			}
			return list;
		}

		public void AddRenderers(Renderer[] renderers)
		{
			foreach (Renderer renderer in renderers)
			{
				m_cache.Add(renderer);
			}
		}

		public void RemoveRenderers(Renderer[] renderers)
		{
			foreach (Renderer renderer in renderers)
			{
				m_cache.Remove(renderer);
			}
		}

		public void RecreateCommandBuffer()
		{
		}

		public bool ContainsRenderer(Renderer renderer)
		{
			return m_cache.Renderers.Contains(renderer);
		}
	}
}
