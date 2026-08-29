using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerKeyboardData : MonoBehaviour
{
	public enum KeyboardType
	{
		Alphanumeric = 0,
		AlphanumericWithSymbols = 1,
		SignedNumeric = 2,
		Numeric = 3,
		Decimal = 4
	}

	private static Vector3[] worldCornersCache = new Vector3[4];

	public KeyboardType type;

	public InputField inputField;

	public RectTransform keyboardPointer;

	public Button keyboardKey;

	public Button keyboardSpace;

	public bool usePointer = true;

	[NonSerialized]
	public Button firstButton;

	[NonSerialized]
	public bool joystickPointerDoTeleport;

	private List<Button> keyboard = new List<Button>();

	private PineTweenSystemEnableNoHandles menuTween;

	public void Awake()
	{
		keyboardKey.gameObject.SetActive(value: false);
		keyboardPointer.gameObject.SetActive(usePointer);
		configureKeys(type, forceUpdate: true);
	}

	public void initKeyboard(PineTweenSystemEnableNoHandles tween)
	{
		menuTween = tween;
	}

	private void configureKeys(KeyboardType newType, bool forceUpdate = false)
	{
		if (!forceUpdate && newType == type)
		{
			return;
		}
		bool flag = getKeyboardValues(type).Contains(' ');
		for (int i = 0; i < keyboard.Count; i++)
		{
			UnityEngine.Object.Destroy((flag && i == keyboard.Count - 1) ? keyboard[i].transform.parent.gameObject : keyboard[i].gameObject);
		}
		keyboard.Clear();
		type = newType;
		Transform parent = keyboardKey.transform.parent;
		string keyboardValues = getKeyboardValues(type);
		firstButton = null;
		for (int j = 0; j < keyboardValues.Length; j++)
		{
			bool isSpace = keyboardValues[j].Equals(' ');
			Button newButton;
			RectTransform rectTransform;
			if (isSpace)
			{
				Transform transform = UnityEngine.Object.Instantiate(keyboardSpace.transform.parent, Vector3.zero, Quaternion.identity, parent);
				transform.transform.SetAsLastSibling();
				transform.gameObject.SetActive(value: true);
				newButton = transform.GetComponentInChildren<Button>();
				rectTransform = (RectTransform)transform;
			}
			else
			{
				newButton = UnityEngine.Object.Instantiate(keyboardKey, Vector3.zero, Quaternion.identity, parent);
				newButton.GetComponentInChildren<Text>().text = keyboardValues[j].ToString();
				rectTransform = (RectTransform)newButton.transform;
			}
			newButton.gameObject.SetActive(value: true);
			newButton.onClick.RemoveAllListeners();
			Image image = newButton.GetComponent<Image>();
			Color startingColor = image.color;
			newButton.onClick.AddListener(delegate
			{
				string text = (isSpace ? " " : newButton.GetComponentInChildren<Text>().text);
				inputField.text += text;
				PineFmod.playOneShotSound("event:/SFX/GENERAL/uiToggle");
				if (menuTween != null)
				{
					menuTween.destroyTweens(newButton.gameObject);
					image.color = newButton.colors.highlightedColor;
					PineTweenSystemEnableNoHandles pineTweenSystemEnableNoHandles = menuTween;
					GameObject obj = newButton.gameObject;
					Color? colorTo = startingColor;
					pineTweenSystemEnableNoHandles.tween(obj, 0.3f, 0f, deactivate: false, activate: false, destroy: false, null, null, colorTo);
				}
			});
			keyboard.Add(newButton);
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			anchoredPosition3D.z = 0f;
			rectTransform.anchoredPosition3D = anchoredPosition3D;
			if (firstButton == null && !isSpace)
			{
				firstButton = newButton;
			}
		}
		keyboardPointer.transform.SetAsLastSibling();
		int num = 12;
		for (int num2 = 0; num2 < keyboard.Count; num2++)
		{
			int num3 = num2 / 12;
			if (num2 > 0)
			{
				Menu.connectSelectablesHorizontal(keyboard[num2 - 1].gameObject, keyboard[num2].gameObject);
			}
			if (num3 > 0)
			{
				Menu.connectSelectablesVertical(keyboard[num2 - num].gameObject, keyboard[num2].gameObject);
			}
		}
	}

	private string getKeyboardValues(KeyboardType type)
	{
		return type switch
		{
			KeyboardType.Alphanumeric => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 
			KeyboardType.AlphanumericWithSymbols => "0123456789,-ABCDEFGHIJKLMNOPQRSTUVWXYZ ", 
			KeyboardType.SignedNumeric => "0123456789-", 
			KeyboardType.Numeric => "0123456789", 
			KeyboardType.Decimal => "0123456789,-", 
			_ => "", 
		};
	}

	public void changeSettings(PineTweenSystemEnableNoHandles tween, InputField newInputField, KeyboardType newType)
	{
		menuTween = tween;
		inputField = newInputField;
		configureKeys(newType);
	}

	public void Update()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (keyboardPointer.gameObject.activeSelf != usePointer)
		{
			keyboardPointer.gameObject.SetActive(usePointer && currentSelectedGameObject != null && !joystickPointerDoTeleport);
		}
		if (currentSelectedGameObject != null && usePointer)
		{
			currentSelectedGameObject.GetComponent<RectTransform>().GetWorldCorners(worldCornersCache);
			Vector3 vector = worldCornersCache[0];
			if (joystickPointerDoTeleport)
			{
				keyboardPointer.position = vector;
				joystickPointerDoTeleport = false;
			}
			else
			{
				keyboardPointer.position = Vector3.MoveTowards(keyboardPointer.position, vector, Time.deltaTime * 500f);
			}
		}
	}
}
