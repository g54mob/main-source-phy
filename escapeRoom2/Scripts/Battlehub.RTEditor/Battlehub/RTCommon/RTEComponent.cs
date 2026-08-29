using System;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RTEComponent : MonoBehaviour, IRTEComponent
	{
		private IRTE m_editor;

		[SerializeField]
		private RuntimeWindow m_window;

		private bool m_isStarted;

		public IRTE Editor => m_editor;

		public virtual RuntimeWindow Window
		{
			get
			{
				return m_window;
			}
			set
			{
				if (m_window != value)
				{
					if (m_isStarted)
					{
						throw new NotSupportedException("window change is not supported");
					}
					m_editor = IOC.Resolve<IRTE>();
					m_window = value;
				}
			}
		}

		public bool IsWindowActive => Window == m_editor.ActiveWindow;

		protected bool IsStarted => m_isStarted;

		protected virtual void Awake()
		{
			m_editor = IOC.Resolve<IRTE>();
			if (Window == null)
			{
				Window = GetDefaultWindow();
				if (Window == null)
				{
					Debug.LogError("m_window == null");
					base.enabled = false;
					return;
				}
			}
			AwakeOverride();
		}

		protected virtual void Start()
		{
			if (IsWindowActive)
			{
				OnWindowActivating();
				OnWindowActivated();
			}
			m_editor.ActiveWindowChanging += OnActiveWindowChanging;
			m_editor.ActiveWindowChanged += OnActiveWindowChanged;
			m_isStarted = true;
		}

		protected virtual RuntimeWindow GetDefaultWindow()
		{
			return m_editor.GetWindow(RuntimeWindowType.Scene);
		}

		protected virtual void OnDestroy()
		{
			if (m_editor != null)
			{
				m_editor.ActiveWindowChanging -= OnActiveWindowChanging;
				m_editor.ActiveWindowChanged -= OnActiveWindowChanged;
			}
			OnDestroyOverride();
		}

		protected virtual void OnActiveWindowChanging(RuntimeWindow activatedWindow)
		{
			if (activatedWindow == Window)
			{
				OnWindowActivating();
			}
			else
			{
				OnWindowDeactivating();
			}
		}

		protected virtual void OnActiveWindowChanged(RuntimeWindow deactivatedWindow)
		{
			if (m_editor.ActiveWindow == Window)
			{
				OnWindowActivated();
			}
			else
			{
				OnWindowDeactivated();
			}
		}

		protected virtual void OnWindowActivating()
		{
		}

		protected virtual void OnWindowDeactivating()
		{
		}

		protected virtual void OnWindowActivated()
		{
		}

		protected virtual void OnWindowDeactivated()
		{
		}

		[Obsolete("Override Awake method instead")]
		protected virtual void AwakeOverride()
		{
		}

		[Obsolete("Override OnDestroy method instead")]
		protected virtual void OnDestroyOverride()
		{
		}
	}
}
