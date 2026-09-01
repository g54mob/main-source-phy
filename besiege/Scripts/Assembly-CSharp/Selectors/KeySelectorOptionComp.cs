using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Selectors
{
	public class KeySelectorOptionComp : KeySelectorExtender
	{
		public ControlSelector sauce;

		public ControlScheme.ControlEntry entry;

		public ControlScheme.ControlOption option;

		public int keyIndex;

		public int optionIndex;

		private bool changing;

		private string lastText = "N/A!";

		private string currentText = string.Empty;

		private int lastMouse = -1;

		private int currentMouse = -1;

		private bool lastArrow;

		private HashSet<KeyCode> keys = new HashSet<KeyCode>();

		protected bool Rebindable
		{
			get
			{
				return StatMaster.allowFullRebinding || entry.Rebindable;
			}
		}

		public void SetUp(ControlSelector source, Transform hoverText, int index, KeyCode key, ControlScheme.ControlEntry entry, ControlScheme.ControlOption option, int optionIndex, int keyIndex)
		{
			sauce = source;
			this.entry = entry;
			this.option = option;
			this.keyIndex = keyIndex;
			this.optionIndex = optionIndex;
			if (!entry.Rebindable)
			{
				Renderer component = GetComponent<Renderer>();
				Color color = new Color(1f, 1f, 1f, 0.6f);
				Color color2 = component.material.GetColor("_TintColor");
				float num = 0.6f;
				component.material.SetColor("_TintColor", new Color(color2.r * num, color2.g * num, color2.b * num, color2.a));
				text.GetComponent<Renderer>().material.color = color;
				leftMouse.GetComponent<Renderer>().material.SetColor("_TintColor", color * 0.6f);
				middleMouse.GetComponent<Renderer>().material.SetColor("_TintColor", color * 0.6f);
				rightMouse.GetComponent<Renderer>().material.SetColor("_TintColor", color * 0.6f);
				scrollUp.GetComponent<Renderer>().material.SetColor("_TintColor", color * 0.6f);
				scrollDown.GetComponent<Renderer>().material.SetColor("_TintColor", color * 0.6f);
				arrow.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", color * 0.7f);
			}
			base.SetUp(source, hoverText, index, key);
		}

		protected override void OnKeyboardKeyPressed(KeyCode keyCode)
		{
			if (UIMask.InsideMask(mask, base.transform.position) && !IsIgnored(keyCode) && (!verifyScroll || (keyCode != KeyCode.DoubleQuote && keyCode != KeyCode.Caret) || (OptionsMaster.scrollBindingEnabled && StatMaster.allowScrollRebind)))
			{
				sauce.SetAllChanging(option);
				AddKey(keyCode);
			}
		}

		protected override void OnKeyboardKeyReleased(HashSet<KeyCode> keyCodes)
		{
			if (UIMask.InsideMask(mask, base.transform.position) && changing)
			{
				ChangeKeys(keys);
			}
			changing = false;
			keys.Clear();
		}

		protected override bool OnCursorEnter()
		{
			if (!Rebindable)
			{
				return false;
			}
			if (base.OnCursorEnter())
			{
				sauce.SetAllHovered(option, true);
				return true;
			}
			return false;
		}

		protected override bool OnCursorExit()
		{
			if (!Rebindable)
			{
				return false;
			}
			if (base.OnCursorExit())
			{
				sauce.SetAllHovered(option, false);
				return true;
			}
			return false;
		}

		public void Changing()
		{
			if (Rebindable)
			{
				lastText = currentText;
				lastMouse = currentMouse;
				lastArrow = arrow.activeSelf;
				changing = true;
				text.transform.localPosition = new Vector3(0f, text.transform.localPosition.y, text.transform.localPosition.z);
				arrow.SetActive(false);
				keypad.SetActive(false);
				ReferenceMaster.SetDynamicText(text, "◦ ◦ ◦");
			}
		}

		public override void Hover(bool hovered)
		{
			if (!Rebindable)
			{
				return;
			}
			base.Hover(hovered);
			if (hovered)
			{
				return;
			}
			if (changing)
			{
				if (lastText != "N/A!")
				{
					ReferenceMaster.SetDynamicText(text, lastText);
				}
				SetMouse(lastMouse);
				arrow.SetActive(lastArrow);
			}
			keys.Clear();
			changing = false;
		}

		protected override void SetMouse(int i = -1)
		{
			base.SetMouse(i);
			currentMouse = i;
		}

		protected override string AssignText(string str)
		{
			currentText = base.AssignText(str);
			return currentText;
		}

		protected void AddKey(KeyCode keyCode)
		{
			if (!keys.Contains(keyCode))
			{
				keys.Add(keyCode);
			}
		}

		protected override void ChangeKey(KeyCode keyCode)
		{
		}

		protected void ChangeKeys(HashSet<KeyCode> keyCode)
		{
			KeyCode[] array = keyCode.ToArray();
			option.Set(array);
			sauce.UpdateOptions();
		}

		public override void Remove()
		{
		}
	}
}
