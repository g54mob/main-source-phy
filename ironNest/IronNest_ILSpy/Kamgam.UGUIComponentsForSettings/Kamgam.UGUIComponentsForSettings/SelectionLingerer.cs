using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class SelectionLingerer : MonoBehaviour
{
	protected Selectable selectable;

	protected bool _selectableIsInteractable;

	public Selectable Selectable
	{
		get
		{
			if (this.selectable == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Selectable selectable = default(Selectable);
				this.selectable = selectable;
			}
			return this.selectable;
		}
	}

	public void OnEnable()
	{
		Selectable selectable = Selectable;
		if (selectable != null)
		{
			Selectable selectable2 = Selectable;
			_selectableIsInteractable = selectable2.m_Interactable;
		}
	}

	public void Update()
	{
		Selectable selectable = Selectable;
		if (!(selectable != null))
		{
			return;
		}
		Selectable selectable2 = Selectable;
		if (_selectableIsInteractable == selectable2.m_Interactable)
		{
			return;
		}
		Selectable selectable3 = Selectable;
		_selectableIsInteractable = selectable3.m_Interactable;
		if (selectable3.m_Interactable)
		{
			return;
		}
		EventSystem current = EventSystem.current;
		if (current != null)
		{
			EventSystem current2 = EventSystem.current;
			if (current2.m_CurrentSelected == null)
			{
				Selectable selectable4 = Selectable;
				GameObject go = selectable4.gameObject;
				SelectionUtils.SetSelected(go);
			}
		}
	}
}
