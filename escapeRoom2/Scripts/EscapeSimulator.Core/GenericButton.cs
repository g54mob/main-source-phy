using System;
using UnityEngine;
using UnityEngine.UI;

public class GenericButton : MonoBehaviour
{
	public delegate void OnInput();

	public delegate void OnInputPassButton(GenericButton button);

	public LockArgument[] locks;

	[Space(10f)]
	public ButtonType buttonType;

	[Space(10f)]
	public int buttonValue;

	public LockInputVisualizer inputVisualizer;

	[HideInInspector]
	public Text labelText;

	[NonSerialized]
	public BoxCollider boxCollider;

	public OnInput onInput;

	public OnInputPassButton onInputPassButton;

	private Vector3 startLocalPos;

	private float animationTime = float.MaxValue;

	private void Awake()
	{
		startLocalPos = base.transform.localPosition;
		labelText = GetComponentInChildren<Text>(includeInactive: true);
		boxCollider = GetComponentInChildren<BoxCollider>(includeInactive: true);
	}

	private void Update()
	{
		if (animationTime <= 1f)
		{
			animationTime += Time.deltaTime * 2f;
			base.transform.localPosition = Vector3.Lerp(startLocalPos + base.transform.forward * 0.01f, startLocalPos, Mathf.Abs(animationTime));
		}
		else if (animationTime != float.MaxValue)
		{
			animationTime = float.MaxValue;
		}
	}

	public void registerInput()
	{
		switch (buttonType)
		{
		case ButtonType.ReplicatorNumber:
			buttonValue = (buttonValue + 1) % 10;
			if (labelText != null)
			{
				labelText.text = buttonValue.ToString();
			}
			break;
		case ButtonType.ReplicatorPattern:
			buttonValue = (buttonValue + 1) % 3;
			if (labelText != null)
			{
				switch (buttonValue)
				{
				case 0:
					labelText.text = "--";
					break;
				case 1:
					labelText.text = "+-";
					break;
				case 2:
					labelText.text = "++";
					break;
				}
			}
			break;
		case ButtonType.Keypad:
			animationTime = -1f;
			if (inputVisualizer != null)
			{
				inputVisualizer.updateForNewInput();
			}
			break;
		case ButtonType.SpaceCardReaderInput:
			buttonValue = (buttonValue + 1) % 3;
			if (buttonValue == 2)
			{
				labelText.text = "-";
			}
			else
			{
				labelText.text = buttonValue.ToString();
			}
			break;
		}
		if (onInput != null)
		{
			onInput();
		}
		if (onInputPassButton != null)
		{
			onInputPassButton(this);
		}
	}
}
