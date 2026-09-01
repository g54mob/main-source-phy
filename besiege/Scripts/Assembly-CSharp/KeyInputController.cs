using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputController : MonoBehaviour
{
	public class KeyInfo
	{
		public KeyCode keyCode;

		public ushort dictKey;

		public int keyState;

		public bool lastDown;

		public bool hasUpdate;

		public bool IsDown
		{
			get
			{
				return (keyState & 4) != 0;
			}
		}

		public bool IsPressed
		{
			get
			{
				return (keyState & 1) != 0;
			}
		}

		public bool IsReleased
		{
			get
			{
				return (keyState & 2) != 0;
			}
		}

		public KeyInfo(ushort code)
		{
			keyCode = (KeyCode)code;
			dictKey = code;
			keyState = 0;
			lastDown = false;
			hasUpdate = false;
		}

		public override string ToString()
		{
			return string.Format("[{0}] IsDown: {1}, IsPressed: {2}, IsReleased: {3}", keyCode, IsDown, IsPressed, IsReleased);
		}
	}

	public class KeyEntry : IEquatable<KeyEntry>
	{
		public BlockBehaviour Block;

		public MKey Key;

		public bool Equals(KeyEntry other)
		{
			return Key == other.Key;
		}
	}

	protected bool isActive = true;

	protected Dictionary<ushort, KeyInfo> keys;

	protected Dictionary<KeyCode, List<KeyEntry>> usedKeys = new Dictionary<KeyCode, List<KeyEntry>>();

	protected Dictionary<string, List<KeyEntry>> usedMessages = new Dictionary<string, List<KeyEntry>>();

	protected List<ushort> keyCodes;

	protected bool hasAnyEmulation;

	protected bool overrideSimTogglePress;

	protected bool overrideSimToggleHold;

	protected bool overrideSimToggleRelease;

	public virtual void Awake()
	{
		keys = new Dictionary<ushort, KeyInfo>();
		keyCodes = new List<ushort>();
		Toggle(false);
	}

	public bool KeyUsed(KeyCode key)
	{
		return usedKeys.ContainsKey(key);
	}

	public bool IsHeld(KeyCode key)
	{
		if (key == KeyCode.None)
		{
			return false;
		}
		if (hasAnyEmulation && InputManager.IsSimToggle(key))
		{
			return false;
		}
		KeyInfo value;
		if (isActive && keys.TryGetValue((ushort)key, out value) && value.IsDown)
		{
			return true;
		}
		if (overrideSimToggleHold && InputManager.IsSimToggle(key))
		{
			return true;
		}
		if (InputManager.Scrolling(key))
		{
			return true;
		}
		if (InputManager.Joystick.Dpad.Held(key))
		{
			return true;
		}
		if (InputManager.Joystick.Trigger.Held(key))
		{
			return true;
		}
		return false;
	}

	public bool IsPressed(KeyCode key)
	{
		if (key == KeyCode.None)
		{
			return false;
		}
		if (hasAnyEmulation && InputManager.IsSimToggle(key))
		{
			return false;
		}
		KeyInfo value;
		if (isActive && keys.TryGetValue((ushort)key, out value) && value.IsDown && value.IsPressed)
		{
			return true;
		}
		if (overrideSimTogglePress && InputManager.IsSimToggle(key))
		{
			return true;
		}
		if (InputManager.Scrolling(key))
		{
			return true;
		}
		if (InputManager.Joystick.Dpad.Pressed(key))
		{
			return true;
		}
		if (InputManager.Joystick.Trigger.Pressed(key))
		{
			return true;
		}
		return false;
	}

	public bool IsReleased(KeyCode key)
	{
		if (key == KeyCode.None)
		{
			return false;
		}
		if (hasAnyEmulation && InputManager.IsSimToggle(key))
		{
			return false;
		}
		KeyInfo value;
		if (isActive && keys.TryGetValue((ushort)key, out value) && !value.IsDown && value.IsReleased)
		{
			return true;
		}
		if (overrideSimToggleRelease && InputManager.IsSimToggle(key))
		{
			return true;
		}
		if (InputManager.ScrollStopped(key))
		{
			return true;
		}
		if (InputManager.Joystick.Dpad.Released(key))
		{
			return true;
		}
		if (InputManager.Joystick.Trigger.Released(key))
		{
			return true;
		}
		return false;
	}

	protected int GetState(KeyCode key)
	{
		int num = ((InputManager.Scrolling(key) || Input.GetKeyDown(key)) ? 1 : 0);
		int num2 = ((InputManager.ScrollStopped(key) || Input.GetKeyUp(key)) ? 2 : 0);
		int num3 = ((InputManager.Scrolling(key) || Input.GetKey(key)) ? 4 : 0);
		return num | num2 | num3;
	}

	public virtual void ResetKeys()
	{
		for (int i = 0; i < keyCodes.Count; i++)
		{
			KeyInfo keyInfo = keys[keyCodes[i]];
			keyInfo.keyState = 0;
			keyInfo.lastDown = false;
			keyInfo.hasUpdate = false;
		}
	}

	public virtual void Toggle(bool toggle)
	{
		isActive = toggle;
		ResetKeys();
	}

	public virtual void Add(KeyCode key)
	{
		ushort num = (ushort)key;
		KeyInfo value = null;
		if (!keys.TryGetValue(num, out value))
		{
			keys.Add(num, new KeyInfo(num));
			keyCodes.Add(num);
		}
	}

	public virtual void Clear()
	{
		keys.Clear();
		keyCodes.Clear();
		usedKeys.Clear();
		usedMessages.Clear();
	}

	public virtual void SetHasAnyEmulation(bool hasAny)
	{
		hasAnyEmulation = hasAny;
	}

	public virtual void SetSimToggleOverride(bool press, bool hold, bool release)
	{
		overrideSimTogglePress = press;
		overrideSimToggleHold = hold;
		overrideSimToggleRelease = release;
	}

	public virtual void UpdateKeys()
	{
		if (isActive && !StatMaster.inMenu && !StatMaster.stopHotkeys)
		{
			for (int i = 0; i < keyCodes.Count; i++)
			{
				KeyInfo keyInfo = keys[keyCodes[i]];
				keyInfo.keyState = GetState(keyInfo.keyCode);
				bool isDown = keyInfo.IsDown;
				keyInfo.lastDown = isDown;
			}
		}
	}

	public void AddMKey(BlockBehaviour block, MKey key, KeyCode keyCode)
	{
		if (keyCode == KeyCode.None && !key.useMessage)
		{
			return;
		}
		KeyEntry keyEntry = new KeyEntry();
		keyEntry.Block = block;
		keyEntry.Key = key;
		KeyEntry item = keyEntry;
		List<KeyEntry> value2;
		if (key.useMessage)
		{
			string[] message = key.message;
			foreach (string text in message)
			{
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				List<KeyEntry> value;
				if (!usedMessages.TryGetValue(text, out value))
				{
					value = new List<KeyEntry>();
					usedMessages.Add(text, value);
					if (!key.isEmulator)
					{
						value.Add(item);
					}
				}
				else if (!value.Contains(item) && !key.isEmulator)
				{
					value.Add(item);
				}
			}
		}
		else if (!usedKeys.TryGetValue(keyCode, out value2))
		{
			value2 = new List<KeyEntry>();
			usedKeys.Add(keyCode, value2);
			if (!key.isEmulator)
			{
				value2.Add(item);
			}
		}
		else if (!value2.Contains(item) && !key.isEmulator)
		{
			value2.Add(item);
		}
	}

	public void LogMessages()
	{
		string text = string.Empty;
		foreach (KeyValuePair<string, List<KeyEntry>> usedMessage in usedMessages)
		{
			text = text + usedMessage.Key + ": ";
			foreach (KeyEntry item in usedMessage.Value)
			{
				string text2 = text;
				text = string.Concat(text2, item.Block, " ", item.Block.transform.GetSiblingIndex(), ": ", string.Join(", ", item.Key.message), ";\n");
			}
			text += "|\n\n";
		}
		Debug.Log(text);
		Debug.Break();
	}

	public virtual int Emulate(BlockBehaviour block, MKey[] activationKeys, MKey emulateKey, bool emulate)
	{
		int num = 0;
		int num2;
		if (emulateKey.useMessage)
		{
			string[] message = emulateKey.message;
			num2 = message.Length;
			for (int i = 0; i < num2; i++)
			{
				string text = message[i];
				if (text == null || text.Length <= 0)
				{
					continue;
				}
				List<KeyEntry> list = usedMessages[text];
				foreach (KeyEntry item in list)
				{
					MKey key = item.Key;
					if (EmulateEntry(activationKeys, key))
					{
						key.UpdateEmulation(emulate);
						num++;
					}
				}
			}
			return num;
		}
		num2 = emulateKey.KeysCount;
		for (int j = 0; j < num2; j++)
		{
			KeyCode key2 = emulateKey.GetKey(j);
			if (key2 == KeyCode.None)
			{
				continue;
			}
			List<KeyEntry> list = usedKeys[key2];
			foreach (KeyEntry item2 in list)
			{
				MKey key = item2.Key;
				if (EmulateEntry(activationKeys, key))
				{
					key.UpdateEmulation(emulate);
					num++;
				}
			}
		}
		return num;
	}

	public bool HasKey(MKey emulateKey)
	{
		for (int i = 0; i < emulateKey.KeysCount; i++)
		{
			KeyCode key = emulateKey.GetKey(i);
			if (key != KeyCode.None && usedKeys.ContainsKey(key))
			{
				return true;
			}
		}
		return false;
	}

	private bool EmulateEntry(MKey[] activationKeys, MKey key)
	{
		int num = activationKeys.Length;
		for (int i = 0; i < num; i++)
		{
			if (key == activationKeys[i])
			{
				return false;
			}
		}
		return true;
	}

	public virtual bool CheckLoop(MKey[] inputs, MKey emulateKey, bool source = true)
	{
		if (emulateKey.useMessage)
		{
			string[] message = emulateKey.message;
			foreach (string text in message)
			{
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				foreach (KeyEntry item in usedMessages[text])
				{
					if (source)
					{
						BlockBehaviour block = item.Block;
						if (block.BlockID == 68)
						{
							MKey emulateKey2 = (block as LogicGate).EmulateKey;
							if (CheckLoop(inputs, emulateKey2, false))
							{
								return true;
							}
						}
						continue;
					}
					foreach (MKey mKey in inputs)
					{
						for (int k = 0; k < mKey.KeysCount; k++)
						{
							for (int l = 0; l < mKey.message.Length; l++)
							{
								string text2 = mKey.message[l];
								if (!string.IsNullOrEmpty(text2) && text2 == text)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}
		for (int m = 0; m < emulateKey.KeysCount; m++)
		{
			KeyCode key = emulateKey.GetKey(m);
			if (key == KeyCode.None)
			{
				continue;
			}
			foreach (KeyEntry item2 in usedKeys[key])
			{
				if (source)
				{
					BlockBehaviour block2 = item2.Block;
					if (block2.BlockID == 68)
					{
						MKey emulateKey3 = (block2 as LogicGate).EmulateKey;
						if (CheckLoop(inputs, emulateKey3, false))
						{
							return true;
						}
					}
					continue;
				}
				foreach (MKey mKey2 in inputs)
				{
					for (int num = 0; num < mKey2.KeysCount; num++)
					{
						KeyCode key2 = mKey2.GetKey(num);
						if (key2 != KeyCode.None && key2 == key)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}
}
