using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(-60)]
	public class BaseHandleInput : MonoBehaviour
	{
		[SerializeField]
		protected BaseHandle m_handle;

		protected IRTE m_editor;

		public virtual BaseHandle Handle
		{
			get
			{
				return m_handle;
			}
			set
			{
				m_handle = value;
			}
		}

		private void OnEnable()
		{
			if (m_handle == null)
			{
				m_handle = GetComponent<BaseHandle>();
			}
			m_editor = m_handle.Editor;
			if (m_editor != null && BeginDragAction())
			{
				m_handle.BeginDrag();
			}
		}

		protected virtual void Start()
		{
			if (m_editor == null)
			{
				m_editor = m_handle.Editor;
			}
		}

		protected virtual void Update()
		{
			if (m_handle == null)
			{
				Object.Destroy(this);
			}
			else if (m_handle.enabled)
			{
				if (BeginDragAction())
				{
					m_handle.BeginDrag();
				}
				else if (EndDragAction())
				{
					m_handle.EndDrag();
				}
				if (m_handle != null && m_handle.IsDragging)
				{
					m_handle.UnitSnapping = UnitSnappingAction();
				}
			}
		}

		protected virtual bool BeginDragAction()
		{
			if (m_editor.Input.GetPointerDown(0))
			{
				return m_editor.TouchInput.TouchCount < 2;
			}
			return false;
		}

		protected virtual bool EndDragAction()
		{
			if (!m_editor.Input.GetPointerUp(0))
			{
				return m_editor.TouchInput.TouchCount >= 2;
			}
			return true;
		}

		protected virtual bool UnitSnappingAction()
		{
			if (!m_editor.Input.GetKey(KeyCode.LeftShift))
			{
				return m_editor.Tools.UnitSnapping;
			}
			return true;
		}
	}
}
