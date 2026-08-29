using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RuntimeHighlightComponent : MonoBehaviour, IRuntimeHighlightComponent
	{
		private class PointerMovementTracker
		{
			private Vector2 m_prevPointer;

			private IRTE m_editor;

			private bool m_isMoving;

			public IRTE Editor
			{
				get
				{
					return m_editor;
				}
				set
				{
					m_editor = value;
				}
			}

			public bool IsMoving => m_isMoving;

			public void Track()
			{
				Vector2 vector = m_editor.Input.GetPointerXY(0);
				if (m_prevPointer != vector)
				{
					m_isMoving = true;
					m_prevPointer = vector;
				}
				else
				{
					m_isMoving = false;
				}
			}
		}

		private class CameraMovementTracker
		{
			private Ray m_prevRay;

			private RuntimeWindow m_activeWindow;

			private IRTE m_editor;

			private float m_cooldownTime;

			private bool m_isMoving;

			public RuntimeWindow ActiveWindow
			{
				get
				{
					return m_activeWindow;
				}
				set
				{
					if (m_activeWindow != value)
					{
						m_activeWindow = value;
						if (m_activeWindow != null)
						{
							m_prevRay = Ray;
						}
					}
				}
			}

			private Ray Ray => new Ray(m_activeWindow.Camera.transform.position, m_activeWindow.Camera.transform.forward);

			public IRTE Editor
			{
				get
				{
					return m_editor;
				}
				set
				{
					m_editor = value;
				}
			}

			public bool IsMoving => m_isMoving;

			public void Track()
			{
				if (!(m_activeWindow == null))
				{
					Ray ray = Ray;
					if (m_prevRay.origin != ray.origin || m_prevRay.direction != ray.direction || Editor.Tools.IsViewing || !m_activeWindow.IsPointerOver)
					{
						m_isMoving = true;
						m_prevRay = ray;
						m_cooldownTime = Time.time + 0.2f;
					}
					else if (m_cooldownTime <= Time.time)
					{
						m_isMoving = false;
					}
				}
			}
		}

		[SerializeField]
		private bool m_useColliders = true;

		private LayerMask m_raycastMask;

		private IRTE m_editor;

		private bool m_updateRenderers;

		private bool m_allowPickRenderers;

		private readonly Renderer[] m_noRenderers = new Renderer[0];

		private Renderer[] m_allRenderers;

		private Renderer[] m_pickedRenderers;

		private IRuntimeSelectionComponent m_selectionComponent;

		private CameraMovementTracker m_cameraTracker;

		private PointerMovementTracker m_pointerMovementTracker;

		private Color32[] m_texPixels;

		private Vector2Int m_texSize;

		private IRenderersCache m_cache;

		public bool Enabled
		{
			get
			{
				return base.enabled;
			}
			set
			{
				base.enabled = value;
			}
		}

		public IEnumerable<Renderer> Rendererers
		{
			get
			{
				if (m_cache == null)
				{
					return new Renderer[0];
				}
				return m_cache.Renderers;
			}
		}

		public event EventHandler HighlightChanged;

		private void Awake()
		{
			IOC.RegisterFallback((IRuntimeHighlightComponent)this);
		}

		private void Start()
		{
			IOC.Register("HighlightRenderers", m_cache = base.gameObject.AddComponent<RenderersCache>());
			m_editor = IOC.Resolve<IRTE>();
			m_pointerMovementTracker = new PointerMovementTracker();
			m_pointerMovementTracker.Editor = m_editor;
			m_cameraTracker = new CameraMovementTracker();
			m_cameraTracker.Editor = m_editor;
			m_cameraTracker.ActiveWindow = m_editor.ActiveWindow;
			m_raycastMask = m_editor.CameraLayerSettings.RaycastMask;
			if (m_cameraTracker.ActiveWindow != null && m_editor.ActiveWindow.WindowType == RuntimeWindowType.Scene)
			{
				m_selectionComponent = m_cameraTracker.ActiveWindow.IOCContainer.Resolve<IRuntimeSelectionComponent>();
			}
			m_editor.Object.Enabled += OnObjectEnabled;
			m_editor.Object.Disabled += OnObjectDisabled;
			m_editor.Object.ComponentAdded += OnComponentAdded;
			m_editor.Selection.SelectionChanged += OnSelectionChanged;
			m_editor.ActiveWindowChanged += OnActiveWindowChanged;
			if (m_useColliders)
			{
				m_pickedRenderers = new Renderer[1];
			}
		}

		private void OnDestroy()
		{
			IOC.UnregisterFallback((IRuntimeHighlightComponent)this);
			IOC.Unregister("HighlightRenderers", m_cache);
			if (m_editor != null)
			{
				if (m_editor.Object != null)
				{
					m_editor.Object.Enabled -= OnObjectEnabled;
					m_editor.Object.Disabled -= OnObjectDisabled;
					m_editor.Object.ComponentAdded -= OnComponentAdded;
				}
				if (m_editor.Selection != null)
				{
					m_editor.Selection.SelectionChanged -= OnSelectionChanged;
				}
				m_editor.ActiveWindowChanged -= OnActiveWindowChanged;
			}
		}

		private void Update()
		{
			if (m_selectionComponent != null)
			{
				if (m_useColliders)
				{
					TryUpdateHighlightUsingColliders();
				}
				else
				{
					TryUpdateHighlight();
				}
			}
		}

		private void RaiseHighlightChanged()
		{
			this.HighlightChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Highlight(GameObject go)
		{
			m_cache.Clear();
			Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
			m_cache.Add(componentsInChildren, forceRender: true, forceMatrixRecalculationPerRender: true);
			RaiseHighlightChanged();
		}

		public void ClearHighlight()
		{
			m_cache.Clear();
			RaiseHighlightChanged();
		}

		private void TryUpdateHighlightUsingColliders()
		{
			m_cameraTracker.Track();
			m_pointerMovementTracker.Track();
			m_updateRenderers |= m_cameraTracker.IsMoving;
			m_updateRenderers |= m_pointerMovementTracker.IsMoving;
			BaseHandle currentHandle = GetCurrentHandle();
			if (m_cameraTracker.IsMoving || IsPointerOver(currentHandle) || (m_editor.Tools.ActiveTool == m_selectionComponent.BoxSelection && m_selectionComponent.BoxSelection != null))
			{
				m_pickedRenderers[0] = null;
				if (!m_cache.IsEmpty)
				{
					m_cache.Clear();
					RaiseHighlightChanged();
				}
			}
			else
			{
				if (!m_updateRenderers)
				{
					return;
				}
				m_updateRenderers = false;
				if (Physics.Raycast(m_cameraTracker.ActiveWindow.Pointer.Ray, out var hitInfo, float.MaxValue, m_raycastMask.value) && (bool)hitInfo.collider.GetComponent<ExposeToEditor>())
				{
					Renderer component = hitInfo.collider.GetComponent<Renderer>();
					if (!(m_pickedRenderers[0] != component))
					{
						return;
					}
					m_cache.Clear();
					m_pickedRenderers[0] = component;
					if (m_pickedRenderers[0] != null)
					{
						Renderer[] renderers = ((m_editor.Tools.SelectionMode != SelectionMode.Root) ? m_pickedRenderers : RuntimeSelectionUtil.GetRoots(m_pickedRenderers).OfType<GameObject>().SelectMany((GameObject go) => go.GetComponentsInChildren<Renderer>())
							.ToArray());
						m_cache.Add(renderers, forceRender: true, forceMatrixRecalculationPerRender: true);
					}
					RaiseHighlightChanged();
				}
				else
				{
					m_pickedRenderers[0] = null;
					if (!m_cache.IsEmpty)
					{
						m_cache.Clear();
						RaiseHighlightChanged();
					}
				}
			}
		}

		private void TryUpdateHighlight()
		{
			bool isMoving = m_cameraTracker.IsMoving;
			m_cameraTracker.Track();
			m_pointerMovementTracker.Track();
			m_allowPickRenderers |= m_pointerMovementTracker.IsMoving;
			if (!m_cameraTracker.IsMoving)
			{
				m_updateRenderers |= isMoving;
				if (m_updateRenderers)
				{
					m_updateRenderers = false;
					m_allRenderers = (from go in m_editor.Object.Get(rootsOnly: true)
						where go.ActiveSelf
						select go).SelectMany((ExposeToEditor go) => go.GetComponentsInChildren<Renderer>()).ToArray();
					m_texPixels = m_selectionComponent.BoxSelection.BeginPick(out m_texSize, m_allRenderers);
					m_pickedRenderers = null;
				}
				BaseHandle currentHandle = GetCurrentHandle();
				if (IsPointerOver(currentHandle) || (m_editor.Tools.ActiveTool == m_selectionComponent.BoxSelection && m_selectionComponent.BoxSelection != null))
				{
					m_pickedRenderers = null;
					m_cache.Clear();
					RaiseHighlightChanged();
				}
				else
				{
					if (!m_allowPickRenderers)
					{
						return;
					}
					Renderer[] noRenderers = m_noRenderers;
					m_allowPickRenderers = false;
					noRenderers = m_selectionComponent.BoxSelection.EndPick(m_texPixels, m_texSize, m_allRenderers);
					bool flag = false;
					if (m_pickedRenderers == null || !AreEqual(m_pickedRenderers, noRenderers))
					{
						m_pickedRenderers = noRenderers;
						if (m_editor.Tools.SelectionMode == SelectionMode.Root)
						{
							noRenderers = RuntimeSelectionUtil.GetRoots(noRenderers).OfType<GameObject>().SelectMany((GameObject go) => go.GetComponentsInChildren<Renderer>())
								.ToArray();
						}
						flag = true;
					}
					if (flag)
					{
						m_cache.Clear();
						m_cache.Add(noRenderers, forceRender: true, forceMatrixRecalculationPerRender: true);
						RaiseHighlightChanged();
					}
				}
			}
			else
			{
				m_pickedRenderers = null;
				m_cache.Clear();
				RaiseHighlightChanged();
			}
		}

		private bool AreEqual(Renderer[] pickedRenderers, Renderer[] renderers)
		{
			if (pickedRenderers.Length != renderers.Length)
			{
				return false;
			}
			for (int i = 0; i < pickedRenderers.Length; i++)
			{
				if (pickedRenderers[i] != renderers[i])
				{
					return false;
				}
			}
			return true;
		}

		private BaseHandle GetCurrentHandle()
		{
			BaseHandle result = null;
			switch (m_editor.Tools.Current)
			{
			case RuntimeTool.Move:
				result = m_selectionComponent.PositionHandle;
				break;
			case RuntimeTool.Rotate:
				result = m_selectionComponent.RotationHandle;
				break;
			case RuntimeTool.Scale:
				result = m_selectionComponent.ScaleHandle;
				break;
			case RuntimeTool.Rect:
				result = m_selectionComponent.RectTool;
				break;
			case RuntimeTool.Custom:
				result = m_selectionComponent.CustomHandle;
				break;
			}
			return result;
		}

		private bool IsPointerOver(BaseHandle handle)
		{
			if (handle != null)
			{
				return handle.SelectedAxis != RuntimeHandleAxis.None;
			}
			return false;
		}

		private void OnObjectEnabled(ExposeToEditor obj)
		{
			m_updateRenderers = true;
		}

		private void OnObjectDisabled(ExposeToEditor obj)
		{
			m_updateRenderers = true;
		}

		private void OnComponentAdded(ExposeToEditor obj, Component arg)
		{
			m_updateRenderers = true;
		}

		private void OnSelectionChanged(UnityEngine.Object[] unselectedObjects)
		{
			m_updateRenderers = true;
		}

		private void OnActiveWindowChanged(RuntimeWindow window)
		{
			if (m_editor.ActiveWindow != null && m_editor.ActiveWindow.WindowType == RuntimeWindowType.Scene)
			{
				m_updateRenderers = true;
				m_cameraTracker.ActiveWindow = m_editor.ActiveWindow;
				m_selectionComponent = m_editor.ActiveWindow.IOCContainer.Resolve<IRuntimeSelectionComponent>();
			}
			else
			{
				m_cameraTracker.ActiveWindow = null;
				m_selectionComponent = null;
				m_allRenderers = null;
			}
		}
	}
}
