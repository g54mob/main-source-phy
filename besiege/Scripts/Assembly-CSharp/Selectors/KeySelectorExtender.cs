using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Selectors
{
	public class KeySelectorExtender : MonoBehaviour
	{
		protected Transform hoverText;

		[SerializeField]
		protected Material hoverMaterial;

		[SerializeField]
		protected Texture ignoredTex;

		[SerializeField]
		protected DynamicText text;

		[SerializeField]
		protected int index = 1;

		[SerializeField]
		protected KeyCode[] ignoredKeys;

		protected KeySelector source;

		public UIButton trashButton;

		[SerializeField]
		protected bool canEdit = true;

		[SerializeField]
		protected GameObject leftMouse;

		[SerializeField]
		protected GameObject middleMouse;

		[SerializeField]
		protected GameObject rightMouse;

		[SerializeField]
		protected GameObject scrollUp;

		[SerializeField]
		protected GameObject scrollDown;

		[SerializeField]
		protected GameObject arrow;

		[SerializeField]
		protected GameObject dpad;

		[SerializeField]
		protected GameObject joypad;

		[SerializeField]
		protected GameObject keypad;

		[SerializeField]
		protected GameObject joystick;

		[SerializeField]
		protected int mask = -1;

		[SerializeField]
		protected bool isBlockMapper = true;

		[SerializeField]
		protected bool isOption;

		[SerializeField]
		protected bool verifyScroll;

		[HideInInspector]
		public KeyCode myKey;

		protected Renderer keyRenderer;

		protected Texture regularTex;

		public bool useClickToEdit = true;

		protected bool clickedToEdit;

		protected bool ready;

		protected bool stoppedHotkeys;

		protected Material normalMaterial;

		public bool isHovered;

		private bool inConflict;

		public bool StopHotkeys
		{
			get
			{
				return stoppedHotkeys;
			}
			set
			{
				if (stoppedHotkeys != value)
				{
					stoppedHotkeys = value;
					StatMaster.StopHotKeys(stoppedHotkeys);
				}
			}
		}

		public int Index
		{
			get
			{
				return index;
			}
			set
			{
				index = value;
			}
		}

		public event Action<bool> Hovered;

		public virtual void Awake()
		{
			if (ignoredKeys == null)
			{
				ignoredKeys = new KeyCode[0];
			}
			keyRenderer = GetComponent<Renderer>();
			regularTex = keyRenderer.material.mainTexture;
			normalMaterial = keyRenderer.material;
			if (trashButton != null)
			{
				trashButton.gameObject.SetActive(false);
				trashButton.Down += Remove;
			}
		}

		public void SetIgnored(bool ignored)
		{
			keyRenderer.material.mainTexture = ((!ignored) ? regularTex : ignoredTex);
		}

		public void SetConflict(bool conflict)
		{
			inConflict = conflict;
			UpdateVisual();
		}

		public void SetNormalMaterial(Material m)
		{
			keyRenderer.material = (normalMaterial = m);
		}

		public virtual void SetUp(KeySelector source, Transform hoverText, int index, KeyCode key)
		{
			this.source = source;
			this.hoverText = hoverText;
			this.index = index;
			myKey = key;
			UpdateVisual();
		}

		public void ForceEditing()
		{
			if (useClickToEdit)
			{
				ready = true;
				clickedToEdit = true;
				OnCursorEnter();
			}
		}

		protected virtual void OnKeysChanged()
		{
			UpdateVisual();
		}

		protected virtual void UpdateVisual()
		{
			KeyCode keyCode = ((!(source != null) || source.Key == null || source.Key.KeysCount <= index) ? myKey : source.Key.GetKey(index));
			if (!(this.text != null))
			{
				return;
			}
			if (inConflict)
			{
				SetText();
				ReferenceMaster.SetDynamicText(this.text, "●●●");
				return;
			}
			if (keyCode == InputManager.Joystick.Face.Y())
			{
				Vector3 vector = new Vector3(0f, 0f, 0f);
				SetJoypad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				return;
			}
			if (keyCode == InputManager.Joystick.Face.B())
			{
				Vector3 vector = new Vector3(0f, 0f, -90f);
				SetJoypad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				return;
			}
			if (keyCode == InputManager.Joystick.Face.A())
			{
				Vector3 vector = new Vector3(0f, 0f, 180f);
				SetJoypad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				return;
			}
			if (keyCode == InputManager.Joystick.Face.X())
			{
				Vector3 vector = new Vector3(0f, 0f, 90f);
				SetJoypad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				return;
			}
			switch (keyCode)
			{
			case KeyCode.Mouse0:
				SetMouse(0);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			case KeyCode.Mouse1:
				SetMouse(1);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			case KeyCode.Mouse2:
				SetMouse(2);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			case KeyCode.DoubleQuote:
				SetMouse(3);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			case KeyCode.Caret:
				SetMouse(4);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			case KeyCode.UpArrow:
			{
				Vector3 vector = new Vector3(0f, 0f, 0f);
				SetArrow(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.RightArrow:
			{
				Vector3 vector = new Vector3(0f, 0f, -90f);
				SetArrow(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.DownArrow:
			{
				Vector3 vector = new Vector3(0f, 0f, 180f);
				SetArrow(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.LeftArrow:
			{
				Vector3 vector = new Vector3(0f, 0f, 90f);
				SetArrow(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.Joystick8Button9:
			{
				Vector3 vector = new Vector3(0f, 0f, 90f);
				SetDpad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.Joystick8Button8:
			{
				Vector3 vector = new Vector3(0f, 0f, -90f);
				SetDpad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.Joystick8Button7:
			{
				Vector3 vector = new Vector3(0f, 0f, 0f);
				SetDpad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.Joystick8Button6:
			{
				Vector3 vector = new Vector3(0f, 0f, 180f);
				SetDpad(vector);
				ReferenceMaster.SetDynamicText(this.text, string.Empty);
				break;
			}
			case KeyCode.Keypad0:
			case KeyCode.Keypad1:
			case KeyCode.Keypad2:
			case KeyCode.Keypad3:
			case KeyCode.Keypad4:
			case KeyCode.Keypad5:
			case KeyCode.Keypad6:
			case KeyCode.Keypad7:
			case KeyCode.Keypad8:
			case KeyCode.Keypad9:
			case KeyCode.KeypadPeriod:
			case KeyCode.KeypadDivide:
			case KeyCode.KeypadMultiply:
			case KeyCode.KeypadMinus:
			case KeyCode.KeypadPlus:
			case KeyCode.KeypadEquals:
			{
				string text = ReferenceMaster.TranslateKeyCode(keyCode);
				string text2 = Regex.Replace(text, "[^0-9,./*=+-]", string.Empty);
				text2 = ((!(text2 == string.Empty)) ? text2 : text);
				AssignText(text2);
				SetKeypadText(text2);
				break;
			}
			case KeyCode.JoystickButton0:
			case KeyCode.JoystickButton1:
			case KeyCode.JoystickButton2:
			case KeyCode.JoystickButton3:
			case KeyCode.JoystickButton4:
			case KeyCode.JoystickButton5:
			case KeyCode.JoystickButton6:
			case KeyCode.JoystickButton7:
			case KeyCode.JoystickButton8:
			case KeyCode.JoystickButton9:
			case KeyCode.JoystickButton10:
			case KeyCode.JoystickButton11:
			case KeyCode.JoystickButton12:
			case KeyCode.JoystickButton13:
			case KeyCode.JoystickButton14:
			case KeyCode.JoystickButton15:
			case KeyCode.JoystickButton16:
			case KeyCode.JoystickButton17:
			case KeyCode.JoystickButton18:
			case KeyCode.JoystickButton19:
			case KeyCode.Joystick1Button0:
			case KeyCode.Joystick1Button1:
			case KeyCode.Joystick1Button2:
			case KeyCode.Joystick1Button3:
			case KeyCode.Joystick1Button4:
			case KeyCode.Joystick1Button5:
			case KeyCode.Joystick1Button6:
			case KeyCode.Joystick1Button7:
			case KeyCode.Joystick1Button8:
			case KeyCode.Joystick1Button9:
			case KeyCode.Joystick1Button10:
			case KeyCode.Joystick1Button11:
			case KeyCode.Joystick1Button12:
			case KeyCode.Joystick1Button13:
			case KeyCode.Joystick1Button14:
			case KeyCode.Joystick1Button15:
			case KeyCode.Joystick1Button16:
			case KeyCode.Joystick1Button17:
			case KeyCode.Joystick1Button18:
			case KeyCode.Joystick1Button19:
			case KeyCode.Joystick2Button0:
			case KeyCode.Joystick2Button1:
			case KeyCode.Joystick2Button2:
			case KeyCode.Joystick2Button3:
			case KeyCode.Joystick2Button4:
			case KeyCode.Joystick2Button5:
			case KeyCode.Joystick2Button6:
			case KeyCode.Joystick2Button7:
			case KeyCode.Joystick2Button8:
			case KeyCode.Joystick2Button9:
			case KeyCode.Joystick2Button10:
			case KeyCode.Joystick2Button11:
			case KeyCode.Joystick2Button12:
			case KeyCode.Joystick2Button13:
			case KeyCode.Joystick2Button14:
			case KeyCode.Joystick2Button15:
			case KeyCode.Joystick2Button16:
			case KeyCode.Joystick2Button17:
			case KeyCode.Joystick2Button18:
			case KeyCode.Joystick2Button19:
			case KeyCode.Joystick3Button0:
			case KeyCode.Joystick3Button1:
			case KeyCode.Joystick3Button2:
			case KeyCode.Joystick3Button3:
			case KeyCode.Joystick3Button4:
			case KeyCode.Joystick3Button5:
			case KeyCode.Joystick3Button6:
			case KeyCode.Joystick3Button7:
			case KeyCode.Joystick3Button8:
			case KeyCode.Joystick3Button9:
			case KeyCode.Joystick3Button10:
			case KeyCode.Joystick3Button11:
			case KeyCode.Joystick3Button12:
			case KeyCode.Joystick3Button13:
			case KeyCode.Joystick3Button14:
			case KeyCode.Joystick3Button15:
			case KeyCode.Joystick3Button16:
			case KeyCode.Joystick3Button17:
			case KeyCode.Joystick3Button18:
			case KeyCode.Joystick3Button19:
			case KeyCode.Joystick8Button18:
			case KeyCode.Joystick8Button19:
			{
				string text = ReferenceMaster.TranslateKeyCode(keyCode);
				string text2 = Regex.Replace(text, "[^0-9 ,./*=+-]", string.Empty);
				string[] array = text2.Split(null);
				text2 = array[array.Length - 1];
				text2 = ((!(text2 == string.Empty)) ? text2 : text);
				AssignText(text2);
				SetJoystickText(text2);
				break;
			}
			default:
				SetText();
				AssignText(ReferenceMaster.TranslateKeyCode(keyCode));
				break;
			}
		}

		protected virtual void LateUpdate()
		{
			if (trashButton != null)
			{
				if (useClickToEdit)
				{
					trashButton.gameObject.SetActive(clickedToEdit || trashButton.IsHovered);
				}
				else
				{
					trashButton.gameObject.SetActive(isHovered || trashButton.IsHovered);
				}
			}
			if (!useClickToEdit || !InputManager.LeftMouseButton())
			{
				return;
			}
			if (ready)
			{
				if (!clickedToEdit)
				{
					clickedToEdit = true;
					OnCursorEnter();
				}
			}
			else if (clickedToEdit)
			{
				clickedToEdit = false;
				OnCursorExit();
			}
		}

		protected virtual void SetText()
		{
			SetMouse();
			text.transform.localPosition = new Vector3(0f, text.transform.localPosition.y, text.transform.localPosition.z);
			arrow.SetActive(false);
			dpad.SetActive(false);
			joypad.SetActive(false);
			keypad.SetActive(false);
			joystick.SetActive(false);
		}

		protected virtual void SetKeypadText(string s)
		{
			SetMouse();
			if (s.Length > 1)
			{
				SetText();
				return;
			}
			keypad.SetActive(true);
			joystick.SetActive(false);
			text.transform.localPosition = new Vector3(0.165f, text.transform.localPosition.y, text.transform.localPosition.z);
			arrow.SetActive(false);
			dpad.SetActive(false);
			joypad.SetActive(false);
		}

		protected virtual void SetJoystickText(string s)
		{
			SetMouse();
			if (s.Length > 2)
			{
				SetText();
				return;
			}
			keypad.SetActive(false);
			joystick.SetActive(true);
			text.transform.localPosition = new Vector3(0.165f, text.transform.localPosition.y, text.transform.localPosition.z);
			arrow.SetActive(false);
			dpad.SetActive(false);
			joypad.SetActive(false);
		}

		protected virtual string AssignText(string str)
		{
			string[] array = str.Split(' ');
			string result = array[0].ToUpper() + ((array.Length <= 1) ? "\n" : ("\n" + array[1].ToUpper()));
			text.transform.localScale = Vector3.one;
			ReferenceMaster.SetDynamicText(text, result);
			Bounds bounds = GetComponent<Renderer>().bounds;
			Bounds bounds2 = text.GetComponent<Renderer>().bounds;
			float num = 0.1f;
			float num2 = bounds.extents.x * (1f - num);
			if (bounds2.extents.x > num2)
			{
				text.transform.localScale = Vector3.one * (num2 / bounds2.extents.x);
			}
			return result;
		}

		protected virtual void SetMouse(int i = -1)
		{
			switch (i)
			{
			case 0:
				scrollUp.SetActive(false);
				scrollDown.SetActive(false);
				leftMouse.SetActive(true);
				middleMouse.SetActive(false);
				rightMouse.SetActive(false);
				break;
			case 1:
				scrollUp.SetActive(false);
				scrollDown.SetActive(false);
				leftMouse.SetActive(false);
				middleMouse.SetActive(false);
				rightMouse.SetActive(true);
				break;
			case 2:
				scrollUp.SetActive(false);
				scrollDown.SetActive(false);
				leftMouse.SetActive(false);
				middleMouse.SetActive(true);
				rightMouse.SetActive(false);
				break;
			case 3:
				scrollUp.SetActive(true);
				scrollDown.SetActive(false);
				leftMouse.SetActive(false);
				middleMouse.SetActive(false);
				rightMouse.SetActive(false);
				break;
			case 4:
				scrollUp.SetActive(false);
				scrollDown.SetActive(true);
				leftMouse.SetActive(false);
				middleMouse.SetActive(false);
				rightMouse.SetActive(false);
				break;
			default:
				scrollUp.SetActive(false);
				scrollDown.SetActive(false);
				leftMouse.SetActive(false);
				middleMouse.SetActive(false);
				rightMouse.SetActive(false);
				break;
			}
			if (i != -1)
			{
				arrow.SetActive(false);
				dpad.SetActive(false);
				joypad.SetActive(false);
				keypad.SetActive(false);
				joystick.SetActive(false);
			}
		}

		protected virtual void SetArrow(Vector3 euler)
		{
			SetMouse();
			arrow.transform.localEulerAngles = euler;
			arrow.SetActive(true);
			dpad.SetActive(false);
			joypad.SetActive(false);
			keypad.SetActive(false);
			joystick.SetActive(false);
		}

		protected virtual void SetDpad(Vector3 euler)
		{
			SetMouse();
			dpad.transform.localEulerAngles = euler;
			arrow.SetActive(false);
			dpad.SetActive(true);
			joypad.SetActive(false);
			keypad.SetActive(false);
			joystick.SetActive(false);
		}

		protected virtual void SetJoypad(Vector3 euler)
		{
			SetMouse();
			joypad.transform.localEulerAngles = euler;
			arrow.SetActive(false);
			dpad.SetActive(false);
			joypad.SetActive(true);
			keypad.SetActive(false);
			joystick.SetActive(false);
		}

		protected virtual void OnDisable()
		{
			if (source != null && source.Key != null)
			{
				source.Key.KeysChanged -= OnKeysChanged;
			}
			isHovered = false;
			if (InputManager.KeyTarget == base.gameObject)
			{
				InputManager.KeyTarget = null;
			}
			InputManager.KeyboardKeyPressed -= OnKeyboardKeyPressed;
			InputManager.KeyboardKeysReleased -= OnKeyboardKeyReleased;
			StopAllCoroutines();
			if (stoppedHotkeys)
			{
				stoppedHotkeys = false;
				StatMaster.DelayStopHotKeys(false);
			}
		}

		protected bool IsIgnored(KeyCode k)
		{
			for (int i = 0; i < ignoredKeys.Length; i++)
			{
				if (ignoredKeys[i] == k)
				{
					return true;
				}
			}
			return false;
		}

		protected virtual void OnKeyboardKeyPressed(KeyCode keyCode)
		{
			if (UIMask.InsideMask(mask, base.transform.position) && !IsIgnored(keyCode) && (!verifyScroll || (keyCode != KeyCode.DoubleQuote && keyCode != KeyCode.Caret) || (OptionsMaster.scrollBindingEnabled && StatMaster.allowScrollRebind)))
			{
				ChangeKey(keyCode);
			}
		}

		protected virtual void OnKeyboardKeyReleased(HashSet<KeyCode> keyCodes)
		{
		}

		private void OnMouseEnter()
		{
			ready = true;
			if (!useClickToEdit)
			{
				OnCursorEnter();
			}
		}

		protected virtual bool OnCursorEnter()
		{
			if (StatMaster.textFieldSelected || !canEdit || !UIMask.InsideMask(mask, base.transform.position))
			{
				if (isHovered)
				{
					OnCursorExit();
				}
				return false;
			}
			if (InputManager.KeyTarget != base.gameObject)
			{
				InputManager.KeyTarget = base.gameObject;
			}
			InputManager.KeyboardKeyPressed += OnKeyboardKeyPressed;
			InputManager.KeyboardKeysReleased += OnKeyboardKeyReleased;
			if (trashButton != null)
			{
				if (useClickToEdit)
				{
					trashButton.gameObject.SetActive(clickedToEdit);
				}
				else
				{
					trashButton.gameObject.SetActive(true);
				}
			}
			if (hoverText != null)
			{
				hoverText.gameObject.SetActive(true);
			}
			Hover(true);
			isHovered = true;
			StopHotkeys = true;
			if (this.Hovered != null)
			{
				this.Hovered(true);
			}
			return true;
		}

		protected virtual void OnMouseOver()
		{
			if ((StatMaster.textFieldSelected || !canEdit || !UIMask.InsideMask(mask, base.transform.position)) && isHovered)
			{
				OnCursorExit();
			}
		}

		private void OnMouseExit()
		{
			ready = false;
			if (!useClickToEdit || !isOption)
			{
				OnCursorExit();
			}
		}

		protected virtual bool OnCursorExit()
		{
			if (!canEdit)
			{
				return false;
			}
			if ((bool)hoverText)
			{
				hoverText.gameObject.SetActive(false);
			}
			Hover(false);
			StartCoroutine(LateCursorExit());
			if (InputManager.KeyTarget == base.gameObject)
			{
				InputManager.KeyTarget = null;
			}
			InputManager.KeyboardKeyPressed -= OnKeyboardKeyPressed;
			InputManager.KeyboardKeysReleased -= OnKeyboardKeyReleased;
			isHovered = false;
			StopHotkeys = false;
			if (!isOption)
			{
				clickedToEdit = false;
			}
			if (this.Hovered != null)
			{
				this.Hovered(false);
			}
			return true;
		}

		protected IEnumerator LateCursorExit()
		{
			if (trashButton != null)
			{
				yield return new WaitForEndOfFrame();
				if (useClickToEdit)
				{
					trashButton.gameObject.SetActive(clickedToEdit || trashButton.IsHovered);
				}
				else
				{
					trashButton.gameObject.SetActive(isHovered || trashButton.IsHovered);
				}
			}
		}

		public virtual void Hover(bool hovered)
		{
			if (keyRenderer != null)
			{
				keyRenderer.material = ((!hovered) ? normalMaterial : hoverMaterial);
			}
		}

		protected virtual void ChangeKey(KeyCode keyCode)
		{
			if (source.Key.GetKey(index) != keyCode)
			{
				myKey = keyCode;
				index = source.ChangeKey(index, keyCode);
				UpdateVisual();
			}
			else
			{
				source.UpdateAll();
			}
			if (useClickToEdit && !isOption)
			{
				OnMouseExit();
				ready = true;
				isHovered = true;
			}
		}

		public virtual void Remove()
		{
			source.RemoveKey(index);
			if (source.KeyObjCount <= 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		public void Display(bool b)
		{
			base.gameObject.SetActive(b);
		}
	}
}
