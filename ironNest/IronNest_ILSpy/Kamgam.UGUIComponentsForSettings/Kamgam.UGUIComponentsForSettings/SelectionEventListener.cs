using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class SelectionEventListener : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	public delegate void OnSelectionChangedDelegate(bool isSelected);

	public UnityEvent<bool> OnSelectionChangedEvent;

	public OnSelectionChangedDelegate OnSelectionChanged;

	protected Selectable selectable;

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

	public bool IsSelected
	{
		get
		{
			//IL_0128: Expected I4, but got O
			EventSystem current = EventSystem.current;
			if (!(current != null))
			{
				goto IL_0114;
			}
			EventSystem current2 = EventSystem.current;
			if ((object)current2 != null)
			{
				if (!(current2.m_CurrentSelected != null))
				{
					goto IL_0114;
				}
				EventSystem current3 = EventSystem.current;
				if ((object)current3 != null)
				{
					if (this.selectable == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						Selectable selectable = default(Selectable);
						this.selectable = selectable;
					}
					return current3.m_CurrentSelected == this.selectable;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0114:
			return false;
		}
	}

	public unsafe void OnSelect(BaseEventData eventData)
	{
		OnSelectionChangedDelegate onSelectionChanged = OnSelectionChanged;
		if (OnSelectionChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v27.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (OnSelectionChangedEvent != null)
		{
			object obj = default(object);
			OnSelectionChangedEvent.Invoke((byte)(&obj) != 0);
		}
	}

	public unsafe void OnDeselect(BaseEventData eventData)
	{
		OnSelectionChangedDelegate onSelectionChanged = OnSelectionChanged;
		if (OnSelectionChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v27.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (OnSelectionChangedEvent != null)
		{
			object obj = default(object);
			OnSelectionChangedEvent.Invoke((byte)(&obj) != 0);
		}
	}
}
