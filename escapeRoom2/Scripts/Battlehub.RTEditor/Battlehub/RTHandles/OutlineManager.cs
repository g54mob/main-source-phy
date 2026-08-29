using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class OutlineManager : MonoBehaviour, IOutlineManager
	{
		private IRTE m_editor;

		private RuntimeWindow m_sceneWindow;

		private OutlineEffect m_outlineEffect;

		private IEnumerator m_coUpdate;

		private IRuntimeSelection m_selectionOverride;

		public Camera Camera { private get; set; }

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
			if (RenderPipelineInfo.Type != RPType.Standard)
			{
				Object.Destroy(this);
				return;
			}
			if (Camera == null)
			{
				Camera = GetComponent<Camera>();
			}
			if (Camera == null)
			{
				Camera = Camera.main;
			}
			m_outlineEffect = Camera.gameObject.AddComponent<OutlineEffect>();
			m_editor = IOC.Resolve<IRTE>();
			TryToAddRenderers(m_editor.Selection);
			m_editor.Selection.SelectionChanged += OnRuntimeEditorSelectionChanged;
			m_editor.Object.Enabled += OnObjectEnabled;
			m_editor.Object.Disabled += OnObjectDisabled;
			RTEComponent componentInParent = GetComponentInParent<RTEComponent>();
			if (componentInParent != null)
			{
				m_sceneWindow = componentInParent.Window;
				m_sceneWindow.IOCContainer.RegisterFallback((IOutlineManager)this);
			}
		}

		private void OnDestroy()
		{
			if (m_sceneWindow != null)
			{
				m_sceneWindow.IOCContainer.UnregisterFallback((IOutlineManager)this);
			}
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
			if (m_outlineEffect != null)
			{
				Object.Destroy(m_outlineEffect);
			}
			if (m_coUpdate != null)
			{
				StopCoroutine(m_coUpdate);
				m_coUpdate = null;
			}
		}

		private void OnObjectEnabled(ExposeToEditor obj)
		{
			if (m_coUpdate == null && base.gameObject != null && base.gameObject.activeInHierarchy)
			{
				m_coUpdate = CoUpdate();
				StartCoroutine(m_coUpdate);
			}
		}

		private void OnObjectDisabled(ExposeToEditor obj)
		{
			if (m_coUpdate == null && base.gameObject != null && base.gameObject.activeInHierarchy)
			{
				m_coUpdate = CoUpdate();
				StartCoroutine(m_coUpdate);
			}
		}

		private IEnumerator CoUpdate()
		{
			yield return null;
			if (m_selectionOverride != null)
			{
				OnSelectionChanged(m_selectionOverride.objects);
			}
			else
			{
				OnRuntimeEditorSelectionChanged(m_editor.Selection.objects);
			}
			m_coUpdate = null;
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
				Renderer[] renderers = (from go in unselectedObjects
					select go as GameObject into go
					where go != null
					select go).SelectMany((GameObject go) => go.GetComponentsInChildren<Renderer>(includeInactive: true)).ToArray();
				m_outlineEffect.RemoveRenderers(renderers);
				ICustomOutlinePrepass[] renderers2 = (from go in unselectedObjects
					select go as GameObject into go
					where go != null
					select go).SelectMany((GameObject go) => go.GetComponentsInChildren<ICustomOutlinePrepass>(includeInactive: true)).ToArray();
				m_outlineEffect.RemoveRenderers(renderers2);
			}
		}

		private void TryToAddRenderers(IRuntimeSelection selection)
		{
			if (selection.gameObjects != null)
			{
				IList<Renderer> renderers = GetRenderers(selection.gameObjects);
				m_outlineEffect.AddRenderers(renderers.ToArray());
				IList<ICustomOutlinePrepass> customRenderers = GetCustomRenderers(selection.gameObjects);
				m_outlineEffect.AddRenderers(customRenderers.ToArray());
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

		private IList<ICustomOutlinePrepass> GetCustomRenderers(IList<GameObject> gameObjects)
		{
			List<ICustomOutlinePrepass> list = new List<ICustomOutlinePrepass>();
			gameObjects = FilterSelection(gameObjects);
			for (int i = 0; i < gameObjects.Count; i++)
			{
				ICustomOutlinePrepass[] componentsInChildren = gameObjects[i].GetComponentsInChildren<ICustomOutlinePrepass>();
				foreach (ICustomOutlinePrepass customOutlinePrepass in componentsInChildren)
				{
					Renderer renderer = customOutlinePrepass.GetRenderer();
					if (renderer.gameObject.activeInHierarchy && (renderer.gameObject.hideFlags & HideFlags.HideInHierarchy) == 0)
					{
						list.Add(customOutlinePrepass);
					}
				}
			}
			return list;
		}

		public bool ContainsRenderer(Renderer renderer)
		{
			return m_outlineEffect.ContainsRenderer(renderer);
		}

		public void AddRenderers(Renderer[] renderers)
		{
			m_outlineEffect.AddRenderers(renderers);
		}

		public void RemoveRenderers(Renderer[] renderers)
		{
			m_outlineEffect.RemoveRenderers(renderers);
		}

		public void RecreateCommandBuffer()
		{
			m_outlineEffect.RecreateCommandBuffer();
		}
	}
}
