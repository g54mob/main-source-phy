using UnityEngine;

public class VisualControl
{
	public enum ControlType
	{
		Button = 0,
		Text = 1,
		Gear = 2
	}

	public enum Position
	{
		TopLeft = 0,
		TopRight = 1,
		BotLeft = 2,
		BotCenter = 3,
		BotRight = 4
	}

	public string id;

	public bool hideOnMouseAndKeyboard;

	public bool hideOnController;

	public ControllerButtonActionType controllerHint;

	public Sprite keyboardHintGlyph;

	public KeyCode keyboardHint;

	public bool hiddenEscape;

	public KeyBindingAction keybindingAction;

	public string text;

	public Position position;

	public ControlType type;

	public float timeToClick = -1f;

	public bool isTimedExternallyControlled;

	public float currentDownTimestamp;

	public bool didTimedClick;

	public VisualControlUI ui;

	public bool isTimed => timeToClick != -1f;

	public VisualControl(string id, ControllerButtonActionType controllerHint, string text, ControlType type, Position position)
	{
		this.id = id;
		this.controllerHint = controllerHint;
		this.text = text;
		this.position = position;
		this.type = type;
	}

	public void addEscape(bool hidden = false)
	{
		hiddenEscape = hidden;
		keyboardHint = KeyCode.Escape;
	}

	public void addKeybindAction(KeyBindingAction action, MenuOptions menuOptions)
	{
		keybindingAction = action;
		keyboardHintGlyph = menuOptions.getKeyBindingGlyph(action);
	}

	public void setTimeToClick(float time, bool isExternallyControlled)
	{
		timeToClick = time;
		isTimedExternallyControlled = isExternallyControlled;
	}

	public void setCurrentDownTimestamp(float timestamp)
	{
		currentDownTimestamp = timestamp;
	}
}
