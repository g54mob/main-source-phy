using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(-60)]
	public class RuntimeSelectionInputBase : MonoBehaviour
	{
		protected RuntimeSelectionComponent m_component;

		protected bool m_isPointerPressed;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			m_component = GetComponent<RuntimeSelectionComponent>();
			if (m_component != null && m_component.BoxSelection != null)
			{
				m_component.BoxSelection.Selection += OnBoxSelection;
			}
		}

		protected virtual void OnDestroy()
		{
			if (m_component != null && m_component.BoxSelection != null)
			{
				m_component.BoxSelection.Selection -= OnBoxSelection;
			}
		}

		protected virtual void LateUpdate()
		{
			if (!m_component.IsWindowActive)
			{
				m_isPointerPressed = false;
				return;
			}
			if (m_component.Window.IsPointerOver)
			{
				BeginSelectAction();
			}
			if (SelectAction())
			{
				SelectGO();
			}
		}

		protected virtual void BeginSelectAction()
		{
			if (m_component.Editor.Input.GetPointerDown(0))
			{
				m_isPointerPressed = true;
			}
		}

		protected virtual bool SelectAction()
		{
			int num;
			if (m_isPointerPressed)
			{
				num = (m_component.Editor.Input.GetPointerUp(0) ? 1 : 0);
				if (num != 0)
				{
					m_isPointerPressed = false;
				}
			}
			else
			{
				num = 0;
			}
			return (byte)num != 0;
		}

		protected virtual void SelectGO()
		{
			RuntimeTools tools = m_component.Editor.Tools;
			IRuntimeSelection selection = m_component.Selection;
			if ((!(tools.ActiveTool != null) || !(tools.ActiveTool != m_component.BoxSelection)) && !tools.IsViewing && selection.Enabled)
			{
				OnSelectGO();
			}
		}

		protected virtual void OnSelectGO()
		{
			m_component.SelectGO(multiselect: false, allowUnselect: false);
		}

		protected virtual void SelectAll()
		{
			m_component.SelectAll();
		}

		protected virtual void OnBoxSelection(object sender, BoxSelectionArgs e)
		{
			m_component.BoxSelect(e.GameObjects, multiselect: false);
		}
	}
}
