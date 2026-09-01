using System;
using Kamgam.UGUIComponentsForSettings;

namespace Kamgam.SettingsGenerator;

[Serializable]
public struct KeyCombination
{
	public UniversalKeyCode Key;

	public UniversalKeyCode ModifierKey;

	public bool HasModifier
	{
		get
		{
			bool flag = ModifierKey < UniversalKeyCode.None;
			bool flag2 = ModifierKey == UniversalKeyCode.None;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public KeyCombination(UniversalKeyCode key)
	{
		Key = key;
		ModifierKey = UniversalKeyCode.None;
	}

	public KeyCombination(UniversalKeyCode key, UniversalKeyCode modifierKey)
	{
		Key = key;
		ModifierKey = modifierKey;
	}

	public bool Equals(KeyCombination combination)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		if ((nint)Key != (nint)combination)
		{
			return false;
		}
		object obj = (object)combination >> 32;
		object obj2 = ModifierKey - obj;
		return obj2 == null;
	}

	public override string ToString()
	{
		//IL_0013: Expected I4, but got O
		//IL_0020: Expected I4, but got O
		object obj = default(object);
		object arg = (UniversalKeyCode)obj;
		object obj2 = default(object);
		object arg2 = (UniversalKeyCode)obj2;
		return $"KeyCombination: (mod: {arg}, key: {arg2})";
	}
}
