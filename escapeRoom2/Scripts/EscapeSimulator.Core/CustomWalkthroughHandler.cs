using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CustomWalkthroughUI))]
public class CustomWalkthroughHandler : MonoBehaviour
{
	public struct Header
	{
		public CustomWalkthroughStepUI ui;

		public string headerTitle;

		public List<CustomWalkthroughSingleStepUI> stepUIs;
	}

	public static char separator = '$';

	public static string headerPrefix = "H";

	public static string stepPrefix = "S";

	private PineTweenSystemEnableNoHandles tween = new PineTweenSystemEnableNoHandles();

	private CustomWalkthroughUI ui;

	private RectTransform content;

	private RectTransform mask;

	private List<Header> headers = new List<Header>();

	private List<Selectable> allSelectables;

	private Selectable lastSelectedStep;

	private ScrollRect scrollView;

	private Action onClose;

	private Header initHeader(string text)
	{
		Header header = default(Header);
		CustomWalkthroughStepUI safeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI = ui.SafeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI;
		header.ui = UnityEngine.Object.Instantiate(safeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI, safeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI.transform.parent);
		header.ui.gameObject.SetActive(value: true);
		header.ui.StepTitle_Text.text = text;
		header.stepUIs = new List<CustomWalkthroughSingleStepUI>();
		headers.Add(header);
		return header;
	}

	private CustomWalkthroughSingleStepUI initSingleStep(Header header, CustomWalkthroughSingleStepUI previousStep, string text)
	{
		CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI = UnityEngine.Object.Instantiate(ui.SafeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI.CustomWalkthroughSingleStepUI, header.ui.CustomWalkthroughSingleStepUI.transform.parent);
		customWalkthroughSingleStepUI.StepText.text = text;
		customWalkthroughSingleStepUI.StepNumber_Text.text = (header.stepUIs.Count + 1).ToString();
		customWalkthroughSingleStepUI.gameObject.SetActive(value: true);
		allSelectables.Add(customWalkthroughSingleStepUI.root);
		if (previousStep != null)
		{
			Navigation navigation = previousStep.root.navigation;
			navigation.selectOnDown = customWalkthroughSingleStepUI.root;
			previousStep.root.navigation = navigation;
			Navigation navigation2 = customWalkthroughSingleStepUI.root.navigation;
			navigation2.mode = Navigation.Mode.Explicit;
			navigation2.selectOnUp = previousStep.root;
			customWalkthroughSingleStepUI.root.navigation = navigation2;
		}
		else
		{
			Navigation navigation3 = customWalkthroughSingleStepUI.root.navigation;
			navigation3.mode = Navigation.Mode.Explicit;
			customWalkthroughSingleStepUI.root.navigation = navigation3;
		}
		header.stepUIs.Add(customWalkthroughSingleStepUI);
		hideStep(customWalkthroughSingleStepUI);
		return customWalkthroughSingleStepUI;
	}

	private void showStep(CustomWalkthroughSingleStepUI step)
	{
		step.HideText.gameObject.SetActive(value: false);
		step.StepText.transform.parent.gameObject.SetActive(value: true);
		LayoutRebuilder.MarkLayoutForRebuild(ui.SafeAreas_Modal_ScrollView.GetComponent<RectTransform>());
	}

	private void hideStep(CustomWalkthroughSingleStepUI step)
	{
		step.HideText.gameObject.SetActive(value: true);
		step.StepText.transform.parent.gameObject.SetActive(value: false);
		LayoutRebuilder.MarkLayoutForRebuild(ui.SafeAreas_Modal_ScrollView.GetComponent<RectTransform>());
	}

	private void onButtonClick(Button button)
	{
		if (button == ui.SafeAreas_Modal_ExitToMenu)
		{
			deactivate();
		}
	}

	private void onToggleChange(Toggle toggle, bool value)
	{
		if (!isActive())
		{
			return;
		}
		foreach (Header header in headers)
		{
			foreach (CustomWalkthroughSingleStepUI stepUI in header.stepUIs)
			{
				if (stepUI.root == toggle)
				{
					lastSelectedStep = toggle;
					if (value)
					{
						showStep(stepUI);
					}
					else
					{
						hideStep(stepUI);
					}
				}
			}
		}
	}

	private void selectFirstActive()
	{
		if (allSelectables == null)
		{
			return;
		}
		foreach (Selectable allSelectable in allSelectables)
		{
			if (allSelectable.transform.parent.parent.gameObject.activeSelf)
			{
				if (isActive() && Controller.isActive())
				{
					Controller.selectSelectable(allSelectable);
				}
				lastSelectedStep = allSelectable;
				break;
			}
		}
	}

	private void controllerSelectCurrentStep()
	{
		if (Controller.isActive() && allSelectables.Count > 0)
		{
			if (lastSelectedStep == null)
			{
				selectFirstActive();
			}
			else
			{
				Controller.selectSelectable(lastSelectedStep);
			}
		}
	}

	private void centerScrollOnStep(RectTransform target)
	{
		Vector2 vector = mask.rect.size * 0.5f;
		Vector2 size = content.rect.size;
		Vector3 vector2 = content.InverseTransformPoint(target.position);
		Vector2 normalizedPosition = new Vector2(0f, 1f - Mathf.Clamp01((vector2 + new Vector3(target.rect.size.x, target.rect.size.y, 0f) * 0.25f).y / (0f - (size.y - vector.y))));
		Vector2 vector3 = new Vector2(vector.x / size.x, vector.y / size.y);
		normalizedPosition.y += normalizedPosition.y * vector3.y;
		normalizedPosition.y = Mathf.Clamp01(normalizedPosition.y);
		scrollView.normalizedPosition = normalizedPosition;
	}

	private void onChangeControls()
	{
		foreach (Header header in headers)
		{
			foreach (CustomWalkthroughSingleStepUI stepUI in header.stepUIs)
			{
				stepUI.SelectedHighlight.gameObject.SetActive(Controller.isActive());
			}
		}
	}

	public void init(List<string> walkthrough)
	{
		ui = GetComponent<CustomWalkthroughUI>();
		content = ui.SafeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI.transform.parent.GetComponent<RectTransform>();
		mask = ui.SafeAreas_Modal_ScrollView.GetComponentInChildren<Mask>().GetComponent<RectTransform>();
		ui.SafeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI.gameObject.SetActive(value: false);
		ui.SafeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI.CustomWalkthroughSingleStepUI.gameObject.SetActive(value: false);
		scrollView = ui.SafeAreas_Modal_ScrollView.GetComponent<ScrollRect>();
		allSelectables = new List<Selectable>();
		if (walkthrough != null)
		{
			Header header = default(Header);
			CustomWalkthroughSingleStepUI previousStep = null;
			foreach (string item in walkthrough)
			{
				bool isHeader;
				string text = processText(item, out isHeader);
				if (!string.IsNullOrEmpty(text))
				{
					if (isHeader)
					{
						header = initHeader(text);
					}
					else if (header.stepUIs != null)
					{
						previousStep = initSingleStep(header, previousStep, text);
					}
				}
			}
		}
		PineUI.addButtonClickDelegate(onButtonClick);
		PineUI.addToggleDelegate(onToggleChange);
	}

	public void update()
	{
		tween.processTweens(Time.deltaTime);
		if (!isActive())
		{
			return;
		}
		if (Controller.isActive())
		{
			if (Controller.controllerModeChanged)
			{
				controllerSelectCurrentStep();
			}
			if (Controller.getButtonDown(ControllerButtonActionType.UIBack))
			{
				deactivate();
			}
			if (lastSelectedStep != null)
			{
				centerScrollOnStep(lastSelectedStep.GetComponent<RectTransform>());
			}
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		for (int i = 0; i < allSelectables.Count; i++)
		{
			Selectable selectable = allSelectables[i];
			if (selectable.gameObject == currentSelectedGameObject && lastSelectedStep != selectable)
			{
				lastSelectedStep = selectable;
				onChangeControls();
			}
		}
	}

	public void activate(Action onClose)
	{
		this.onClose = onClose;
		bool flag = false;
		foreach (Header header in headers)
		{
			if (header.ui.gameObject.activeSelf)
			{
				flag = true;
				break;
			}
		}
		ui.SafeAreas_Modal_ScrollView_Content_NoStepsText.gameObject.SetActive(!flag);
		PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tween;
		GameObject obj = ui.gameObject;
		float? alphaTo = 1f;
		pineTweenSystemEnableNoHandles.tween(obj, 0.26f, 0f, deactivate: false, activate: true, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo);
		controllerSelectCurrentStep();
		relinkConnections();
	}

	public void deactivate()
	{
		if (isActive())
		{
			PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = tween;
			GameObject obj = ui.gameObject;
			float? alphaTo = 0f;
			pineTweenSystemEnableNoHandles.tween(obj, 0.26f, 0f, deactivate: true, activate: false, destroy: false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, alphaTo);
			onClose?.Invoke();
		}
	}

	public bool isActive()
	{
		if (ui != null)
		{
			return ui.gameObject.activeInHierarchy;
		}
		return false;
	}

	public void hideAndShowHeaders(List<int> hideHeaders, List<int> showHeaders)
	{
		for (int i = 0; i < headers.Count; i++)
		{
			if (hideHeaders != null && hideHeaders.Contains(i))
			{
				hideHeader(headers[i]);
			}
			if (showHeaders != null && showHeaders.Contains(i))
			{
				showHeader(headers[i]);
			}
		}
		reselectIfNeeded();
	}

	public void showOnlyHeaders(HashSet<int> showHeaders)
	{
		if (showHeaders == null)
		{
			return;
		}
		for (int i = 0; i < headers.Count; i++)
		{
			if (!showHeaders.Contains(i))
			{
				hideHeader(headers[i]);
			}
			else
			{
				showHeader(headers[i]);
			}
		}
		relinkConnections();
		reselectIfNeeded();
	}

	private void relinkConnections()
	{
		CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI = null;
		for (int i = 0; i < headers.Count; i++)
		{
			Header header = headers[i];
			if (!header.ui.gameObject.activeSelf)
			{
				continue;
			}
			for (int j = 0; j < header.stepUIs.Count; j++)
			{
				CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI2 = header.stepUIs[j];
				Navigation navigation = customWalkthroughSingleStepUI2.root.navigation;
				CustomWalkthroughSingleStepUI customWalkthroughSingleStepUI3 = null;
				if (header.stepUIs.Count - 1 > j)
				{
					customWalkthroughSingleStepUI3 = header.stepUIs[j + 1];
				}
				else
				{
					for (int k = i + 1; k < headers.Count; k++)
					{
						Header header2 = headers[k];
						if (header2.ui.gameObject.activeSelf)
						{
							customWalkthroughSingleStepUI3 = header2.stepUIs[0];
							break;
						}
					}
				}
				navigation.selectOnDown = customWalkthroughSingleStepUI3?.root;
				navigation.selectOnUp = customWalkthroughSingleStepUI?.root;
				customWalkthroughSingleStepUI2.root.navigation = navigation;
				customWalkthroughSingleStepUI = customWalkthroughSingleStepUI2;
			}
		}
	}

	public void hideAllHeaders()
	{
		for (int i = 0; i < headers.Count; i++)
		{
			hideHeader(headers[i]);
		}
		reselectIfNeeded();
	}

	private void reselectIfNeeded()
	{
		if (lastSelectedStep == null || !lastSelectedStep.gameObject.activeInHierarchy)
		{
			selectFirstActive();
		}
	}

	private void hideHeader(Header header)
	{
		header.ui.gameObject.SetActive(value: false);
	}

	private void showHeader(Header header)
	{
		header.ui.gameObject.SetActive(value: true);
	}

	public static string processText(string rawText, out bool isHeader)
	{
		isHeader = false;
		if (string.IsNullOrEmpty(rawText.Trim()))
		{
			return string.Empty;
		}
		string[] array = rawText.Split(separator, 2);
		if (array.Length < 2)
		{
			return string.Empty;
		}
		string result = array[1];
		isHeader = array[0].Equals(headerPrefix);
		return result;
	}
}
