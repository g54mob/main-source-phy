using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-60)]
	public class RuntimeWindow : DragDropTarget
	{
		private bool m_isActivated;

		[SerializeField]
		private bool m_canActivate = true;

		[SerializeField]
		private bool m_activateOnAnyKey;

		[SerializeField]
		internal Pointer m_pointer;

		private IOCContainer m_container = new IOCContainer();

		[SerializeField]
		private RuntimeWindowType m_windowType = RuntimeWindowType.Scene;

		private int m_index;

		private int m_depth;

		private CanvasGroup m_canvasGroup;

		private Canvas m_canvas;

		[SerializeField]
		private Image m_background;

		[SerializeField]
		private RectTransform m_viewRoot;

		public bool CanActivate
		{
			get
			{
				return m_canActivate;
			}
			set
			{
				m_canActivate = value;
			}
		}

		public bool ActivateOnAnyKey
		{
			get
			{
				return m_activateOnAnyKey;
			}
			set
			{
				m_activateOnAnyKey = true;
			}
		}

		public virtual Camera Camera
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public virtual Pointer Pointer => m_pointer;

		public IOCContainer IOCContainer => m_container;

		public virtual RuntimeWindowType WindowType
		{
			get
			{
				return m_windowType;
			}
			set
			{
				if (m_windowType != value)
				{
					m_index = base.Editor.GetIndex(value);
					m_windowType = value;
				}
			}
		}

		public virtual int Index => m_index;

		public virtual int Depth
		{
			get
			{
				return m_depth;
			}
			set
			{
				m_depth = value;
			}
		}

		protected CanvasGroup CanvasGroup => m_canvasGroup;

		protected Canvas Canvas => m_canvas;

		public Image Background => m_background;

		public override bool IsPointerOver
		{
			get
			{
				return base.IsPointerOver;
			}
			set
			{
				if (base.IsPointerOver != value)
				{
					if (value)
					{
						base.Editor.SetPointerOverWindow(this);
					}
					else
					{
						base.Editor.SetPointerOverWindow(null);
					}
					base.IsPointerOver = value;
				}
			}
		}

		public RectTransform ViewRoot
		{
			get
			{
				if (!(m_viewRoot != null))
				{
					return (RectTransform)base.transform;
				}
				return m_viewRoot;
			}
		}

		public string Args { get; set; }

		public event EventHandler Activated;

		public event EventHandler Deactivated;

		protected override void AwakeOverride()
		{
			base.AwakeOverride();
			if (m_viewRoot == null)
			{
				m_viewRoot = GetComponent<RectTransform>();
			}
			if (m_background == null && !base.Editor.IsVR)
			{
				m_background = GetComponent<Image>();
				if (m_background == null)
				{
					m_background = base.gameObject.AddComponent<Image>();
					m_background.color = new Color(0f, 0f, 0f, 0f);
					m_background.raycastTarget = true;
				}
				else
				{
					m_background.raycastTarget = true;
				}
			}
			m_canvas = GetComponentInParent<Canvas>();
			m_canvasGroup = GetComponent<CanvasGroup>();
			if (m_canvasGroup == null)
			{
				m_canvasGroup = base.gameObject.AddComponent<CanvasGroup>();
			}
			if (m_canvasGroup != null)
			{
				m_canvasGroup.blocksRaycasts = true;
				m_canvasGroup.ignoreParentGroups = true;
			}
			base.Editor.ActiveWindowChanged += OnActiveWindowChanged;
			if (WindowType != RuntimeWindowType.Custom)
			{
				m_index = base.Editor.GetIndex(WindowType);
			}
			else
			{
				m_index = 0;
			}
			if (m_pointer == null)
			{
				m_pointer = base.gameObject.AddComponent<Pointer>();
			}
			base.Editor.RegisterWindow(this);
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void UpdateOverride()
		{
		}

		protected override void OnDestroyOverride()
		{
			base.OnDestroyOverride();
			if (base.Editor != null)
			{
				base.Editor.ActiveWindowChanged -= OnActiveWindowChanged;
				base.Editor.UnregisterWindow(this);
			}
		}

		protected virtual void OnTransformParentChanged()
		{
			EnableRaycasts();
		}

		public void EnableRaycasts()
		{
			if (m_canvasGroup != null)
			{
				m_canvasGroup.blocksRaycasts = true;
			}
		}

		public void DisableRaycasts()
		{
			if (!m_isActivated && m_canvasGroup != null)
			{
				m_canvasGroup.blocksRaycasts = false;
			}
		}

		public virtual void HandleResize()
		{
		}

		protected virtual void OnActiveWindowChanged(RuntimeWindow deactivatedWindow)
		{
			if (base.Editor.ActiveWindow == this)
			{
				if (!m_isActivated)
				{
					m_isActivated = true;
					if (WindowType == RuntimeWindowType.Game && m_background != null)
					{
						m_background.raycastTarget = false;
					}
					OnActivated();
					this.Activated?.Invoke(this, EventArgs.Empty);
				}
			}
			else if (m_isActivated)
			{
				m_isActivated = false;
				if (m_background != null)
				{
					m_background.raycastTarget = true;
				}
				OnDeactivated();
				this.Deactivated?.Invoke(this, EventArgs.Empty);
			}
		}

		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		protected virtual void OnActivated()
		{
		}

		protected virtual void OnDeactivated()
		{
		}
	}
}
