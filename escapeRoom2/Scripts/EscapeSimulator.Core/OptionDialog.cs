using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionDialog : MonoBehaviour
{
	public Image awardImage;

	public Text titleText;

	public Text messageText;

	public InputField inputField;

	public OptionDialogButtonTemplateUI buttonTemplate;

	public HorizontalLayoutGroup buttonParent;

	public VisualControlUI visualControlTemplate;

	private Selectable currentSelectable;

	private VisualController visualController = new VisualController();

	private Action<string> onClick;

	private string enterButtonId;

	public void showAsAward(string title, string message, Sprite awardSprite)
	{
		show(title, message, null, new VisualControl("ignore", ControllerButtonActionType.UIConfirmPrimary, "%ok%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter));
		awardImage.sprite = awardSprite;
		awardImage.gameObject.SetActive(value: true);
		buttonParent.childAlignment = TextAnchor.LowerCenter;
	}

	public void showAsAward(string title, string message, Sprite awardSprite, Action<string> onClick, params VisualControl[] visualControls)
	{
		show(title, message, onClick, visualControls);
		awardImage.sprite = awardSprite;
		awardImage.gameObject.SetActive(value: true);
		buttonParent.childAlignment = TextAnchor.LowerCenter;
	}

	public void show(string title, string message, Action<string> onClick, params VisualControl[] visualControls)
	{
		this.onClick = onClick;
		visualController.setup(null, null, onButtonClick);
		string message2 = Localization.lookupInDictionary(message);
		string title2 = Localization.lookupInDictionary(title, "");
		showNoTranslate(title2, message2, onClick, visualControls);
	}

	public void showNoTranslate(string title, string message, Action<string> onClick, params VisualControl[] visualControls)
	{
		hide();
		this.onClick = onClick;
		visualController.setup(null, null, onButtonClick);
		inputField.text = "";
		messageText.text = message;
		titleText.text = title;
		titleText.gameObject.SetActive(!string.IsNullOrEmpty(title));
		awardImage.gameObject.SetActive(value: false);
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		currentSelectable = ((currentSelectedGameObject == null) ? null : currentSelectedGameObject.GetComponent<Selectable>());
		if (currentSelectable != null)
		{
			Controller.selectSelectable(null);
		}
		Debug.Log(string.Format("[{0}] title: {1}, message: {2}, selected: {3}", "showNoTranslate", title, message, currentSelectable));
		visualController.addOrUpdateControl(visualControls, visualControlTemplate, buttonTemplate.transform.parent);
		base.gameObject.SetActive(value: true);
		HorizontalOrVerticalLayoutGroup[] componentsInChildren = base.gameObject.GetComponentsInChildren<HorizontalOrVerticalLayoutGroup>(includeInactive: true);
		foreach (HorizontalOrVerticalLayoutGroup horizontalOrVerticalLayoutGroup in componentsInChildren)
		{
			if (horizontalOrVerticalLayoutGroup.enabled)
			{
				horizontalOrVerticalLayoutGroup.enabled = false;
				horizontalOrVerticalLayoutGroup.enabled = true;
			}
		}
		ContentSizeFitter[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<ContentSizeFitter>(includeInactive: true);
		foreach (ContentSizeFitter contentSizeFitter in componentsInChildren2)
		{
			if (contentSizeFitter.enabled)
			{
				contentSizeFitter.enabled = false;
				contentSizeFitter.enabled = true;
			}
		}
		buttonParent.childAlignment = TextAnchor.LowerRight;
		visualController.updateVisuals();
		Game.rebuildTexts(base.transform);
	}

	public void useInputField(string buttonIdToInvokeWithEnter, string inputFieldText = "")
	{
		enterButtonId = buttonIdToInvokeWithEnter;
		inputField.gameObject.SetActive(value: true);
		inputField.SetTextWithoutNotify(inputFieldText);
		inputField.ActivateInputField();
		Game.rebuildTexts(base.transform);
	}

	public void hide()
	{
		if (currentSelectable != null)
		{
			Controller.selectSelectable(currentSelectable);
		}
		currentSelectable = null;
		visualController.removeAll();
		enterButtonId = null;
		inputField.gameObject.SetActive(value: false);
		base.gameObject.SetActive(value: false);
	}

	public void update(InputDispatcher inputDispatcher)
	{
		if (base.gameObject.activeSelf)
		{
			visualController.update(inputDispatcher);
			if (enterButtonId != null && Input.GetKeyDown(KeyCode.Return))
			{
				onButtonClick(enterButtonId);
			}
		}
	}

	private void onButtonClick(string id)
	{
		PineFmod.playOneShotSound("event:/Sound Effects/00 General/Menu Sound Effects/Small/UI_Small_03");
		hide();
		onClick?.Invoke(id);
	}
}
