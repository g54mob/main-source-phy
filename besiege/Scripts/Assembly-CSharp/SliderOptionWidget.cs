using System;
using Localisation;
using UnityEngine;

public class SliderOptionWidget : BaseOptionWidget
{
	private MainOptionsMenu.OptionsCategory.ValueOption valueOption;

	[SerializeField]
	private MouseEventHook mouseEvtHook;

	[SerializeField]
	private Transform sliderTransform;

	[SerializeField]
	private DynamicText minText;

	[SerializeField]
	private DynamicText maxText;

	[SerializeField]
	private DynamicText valueText;

	[SerializeField]
	private BoxCollider widgetCollider;

	[SerializeField]
	private UIButton prevButton;

	[SerializeField]
	private UIButton nextButton;

	private GameObject prevGO;

	private GameObject nextGO;

	private Vector3 sliderMin;

	private Vector3 sliderMax;

	private Camera cam;

	protected void Awake()
	{
		MouseEventHook mouseEventHook = mouseEvtHook;
		mouseEventHook.onMouseDown = (Action)Delegate.Combine(mouseEventHook.onMouseDown, new Action(SliderMouseDown));
		MouseEventHook mouseEventHook2 = mouseEvtHook;
		mouseEventHook2.onMouseDrag = (Action)Delegate.Combine(mouseEventHook2.onMouseDrag, new Action(SliderMouseDrag));
		MouseEventHook mouseEventHook3 = mouseEvtHook;
		mouseEventHook3.onMouseUp = (Action)Delegate.Combine(mouseEventHook3.onMouseUp, new Action(SliderMouseUp));
		prevButton.Click += OnPrevious;
		nextButton.Click += OnNext;
		prevGO = prevButton.gameObject;
		nextGO = nextButton.gameObject;
	}

	private void OnPrevious()
	{
		valueOption.setFunc(Mathf.Max(valueOption.getFunc() - valueOption.getIncrement(), valueOption.Min));
		UpdateVisual();
	}

	private void OnNext()
	{
		valueOption.setFunc(Mathf.Min(valueOption.getFunc() + valueOption.getIncrement(), valueOption.Max));
		UpdateVisual();
	}

	protected void OnEnable()
	{
		cam = GameObject.Find("HUD Cam").GetComponent<Camera>();
	}

	protected void OnDestroy()
	{
		MouseEventHook mouseEventHook = mouseEvtHook;
		mouseEventHook.onMouseDown = (Action)Delegate.Remove(mouseEventHook.onMouseDown, new Action(SliderMouseDown));
		MouseEventHook mouseEventHook2 = mouseEvtHook;
		mouseEventHook2.onMouseDrag = (Action)Delegate.Remove(mouseEventHook2.onMouseDrag, new Action(SliderMouseDrag));
		MouseEventHook mouseEventHook3 = mouseEvtHook;
		mouseEventHook3.onMouseUp = (Action)Delegate.Remove(mouseEventHook3.onMouseUp, new Action(SliderMouseUp));
	}

	private void SliderMouseDown()
	{
		Bounds bounds = widgetCollider.bounds;
		sliderMin = cam.WorldToScreenPoint(bounds.min);
		sliderMax = cam.WorldToScreenPoint(bounds.max);
		float percentage = GetPercentage(InputManager.CursorPosition().x);
		SetSliderPerc(percentage);
	}

	private void SliderMouseDrag()
	{
		float percentage = GetPercentage(InputManager.CursorPosition().x);
		SetSliderPerc(percentage);
	}

	private void SliderMouseUp()
	{
		float percentage = GetPercentage(InputManager.CursorPosition().x);
		SetSliderPerc(percentage);
		valueOption.setFunc(GetValue(percentage));
	}

	private float GetValue(float perc)
	{
		return valueOption.Min + (valueOption.Max - valueOption.Min) * perc;
	}

	private float GetPercentage(float mouseX)
	{
		return Mathf.Clamp01((mouseX - sliderMin.x) / (sliderMax.x - sliderMin.x));
	}

	private void SetSlider(float val)
	{
		SetSliderPerc((val - valueOption.Min) / (valueOption.Max - valueOption.Min));
	}

	public void SetSliderPerc(float perc)
	{
		sliderTransform.localScale = new Vector3(perc, 1f, 1f);
		ReferenceMaster.SetDynamicText(valueText, Mathf.RoundToInt(GetValue(perc)).ToString());
		prevGO.SetActive(perc > 0f);
		nextGO.SetActive(perc < 1f);
	}

	public override void Set(MainOptionsMenu.OptionsCategory.MenuOption option)
	{
		valueOption = option as MainOptionsMenu.OptionsCategory.ValueOption;
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		float slider = valueOption.getFunc();
		ReferenceMaster.SetDynamicText(minText, (valueOption.MinTextID != -1) ? LocalisationManager.GetTranslation(valueOption.MinTextID) : string.Empty);
		ReferenceMaster.SetDynamicText(maxText, (valueOption.MaxTextID != -1) ? LocalisationManager.GetTranslation(valueOption.MaxTextID) : string.Empty);
		SetSlider(slider);
	}
}
