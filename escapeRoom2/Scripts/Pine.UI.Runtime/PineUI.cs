using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class PineUI
{
	public static PineUIState state;

	public static void init()
	{
		state = new PineUIState();
		state.buttonClickDelegates = new List<ButtonDelegate>();
		state.buttonClickPostDelegates = new List<ButtonDelegate>();
		state.buttonDownDelegates = new List<ButtonDelegate>();
		state.buttonUpDelegates = new List<ButtonDelegate>();
		state.buttonInteractionDelegates = new List<ButtonInteractionDelegate>();
		state.toggleDelegates = new List<ToggleDelegate>();
		state.sliderDelegates = new List<SliderDelegate>();
		state.dropdownDelegates = new List<DropdownDelegate>();
	}

	public static void addButtonClickDelegate(ButtonDelegate call)
	{
		state.buttonClickDelegates.Add(call);
	}

	public static void addButtonClickPostDelegate(ButtonDelegate call)
	{
		state.buttonClickPostDelegates.Add(call);
	}

	public static void addButtonDownDelegate(ButtonDelegate call)
	{
		state.buttonDownDelegates.Add(call);
	}

	public static void addButtonUpDelegate(ButtonDelegate call)
	{
		state.buttonUpDelegates.Add(call);
	}

	public static void addButtonInteractionDelegate(ButtonInteractionDelegate call)
	{
		state.buttonInteractionDelegates.Add(call);
	}

	public static void addToggleDelegate(ToggleDelegate callback)
	{
		state.toggleDelegates.Add(callback);
	}

	public static void addSliderDelegate(SliderDelegate callback)
	{
		state.sliderDelegates.Add(callback);
	}

	public static void addDropdownDelegate(DropdownDelegate callback)
	{
		state.dropdownDelegates.Add(callback);
	}

	public static RectTransform rectT(this UIBehaviour m)
	{
		return (RectTransform)m.transform;
	}

	private static bool checkIsInteractable(Selectable obj)
	{
		bool interactable = obj.interactable;
		Transform parent = obj.transform.parent;
		while (interactable && parent != null)
		{
			if (parent.TryGetComponent<CanvasGroup>(out var component))
			{
				interactable = component.interactable;
			}
			parent = parent.parent;
		}
		return interactable;
	}

	public static void addButtonListeners(Button button)
	{
		Button buttonId;
		PineUITrigger trigger;
		if (!(button == null))
		{
			buttonId = button;
			trigger = button.gameObject.GetComponent<PineUITrigger>();
			if (trigger == null)
			{
				trigger = button.gameObject.AddComponent<PineUITrigger>();
			}
			addEventTriggerEntry(EventTriggerType.PointerEnter);
			addEventTriggerEntry(EventTriggerType.PointerExit);
			addEventTriggerEntry(EventTriggerType.PointerDown);
			addEventTriggerEntry(EventTriggerType.PointerUp);
			addEventTriggerEntry(EventTriggerType.PointerClick);
		}
		void addEventTriggerEntry(EventTriggerType type)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry
			{
				eventID = type
			};
			entry.callback.AddListener(delegate(BaseEventData data)
			{
				if (checkIsInteractable(buttonId))
				{
					foreach (ButtonInteractionDelegate buttonInteractionDelegate in state.buttonInteractionDelegates)
					{
						buttonInteractionDelegate(buttonId, type, (PointerEventData)data);
					}
					PointerEventData pointerEventData = data as PointerEventData;
					if (type == EventTriggerType.PointerDown && pointerEventData.button == PointerEventData.InputButton.Left)
					{
						foreach (ButtonDelegate buttonDownDelegate in state.buttonDownDelegates)
						{
							buttonDownDelegate(buttonId);
						}
					}
					if (type == EventTriggerType.PointerUp && pointerEventData.button == PointerEventData.InputButton.Left)
					{
						foreach (ButtonDelegate buttonUpDelegate in state.buttonUpDelegates)
						{
							buttonUpDelegate(buttonId);
						}
					}
					if (type == EventTriggerType.PointerClick && pointerEventData.button == PointerEventData.InputButton.Left)
					{
						foreach (ButtonDelegate item in new List<ButtonDelegate>(state.buttonClickDelegates))
						{
							item(buttonId);
						}
						foreach (ButtonDelegate buttonClickPostDelegate in state.buttonClickPostDelegates)
						{
							buttonClickPostDelegate(buttonId);
						}
					}
				}
			});
			trigger.triggers.Add(entry);
		}
	}

	public static void addToggleListeners(Toggle toggle)
	{
		if (toggle == null)
		{
			return;
		}
		toggle.onValueChanged.RemoveAllListeners();
		toggle.onValueChanged.AddListener(delegate(bool value)
		{
			if (state != null)
			{
				foreach (ToggleDelegate toggleDelegate in state.toggleDelegates)
				{
					toggleDelegate(toggle, value);
				}
			}
		});
	}

	public static void addSliderListeners(Slider slider)
	{
		if (slider == null)
		{
			return;
		}
		slider.onValueChanged.RemoveAllListeners();
		slider.onValueChanged.AddListener(delegate(float value)
		{
			foreach (SliderDelegate sliderDelegate in state.sliderDelegates)
			{
				sliderDelegate(slider, value);
			}
		});
	}

	public static void addDropdownListeners(Dropdown dropdown)
	{
		if (dropdown == null || (dropdown.TryGetComponent<PineUIHint>(out var component) && component.tags.Contains(PineUITag.NoListeners)))
		{
			return;
		}
		dropdown.onValueChanged.RemoveAllListeners();
		dropdown.onValueChanged.AddListener(delegate(int value)
		{
			foreach (DropdownDelegate dropdownDelegate in state.dropdownDelegates)
			{
				dropdownDelegate(dropdown, value);
			}
		});
	}

	public static void interact(Button button, EventTriggerType eventTrigger)
	{
		if (!button.TryGetComponent<PineUITrigger>(out var component))
		{
			Debug.LogError("Cannot interact with button via pineUI since it has no PineUITrigger componenet. The component is automatically aded when the button is subscribed to PineUI.");
			return;
		}
		switch (eventTrigger)
		{
		case EventTriggerType.PointerDown:
			component.OnPointerDown(new PointerEventData(EventSystem.current));
			break;
		case EventTriggerType.PointerUp:
			component.OnPointerUp(new PointerEventData(EventSystem.current));
			break;
		case EventTriggerType.PointerClick:
			component.OnPointerClick(new PointerEventData(EventSystem.current));
			break;
		}
	}
}
