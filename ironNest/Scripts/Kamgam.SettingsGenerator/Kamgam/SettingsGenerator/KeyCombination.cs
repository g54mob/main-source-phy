using System;
using Kamgam.UGUIComponentsForSettings;

namespace Kamgam.SettingsGenerator
{
	[Serializable]
	public struct KeyCombination
	{
		public UniversalKeyCode Key;

		public UniversalKeyCode ModifierKey;

		public bool HasModifier => false;

		public KeyCombination(UniversalKeyCode key)
		{
			Key = default(UniversalKeyCode);
			ModifierKey = default(UniversalKeyCode);
		}

		public KeyCombination(UniversalKeyCode key, UniversalKeyCode modifierKey)
		{
			Key = default(UniversalKeyCode);
			ModifierKey = default(UniversalKeyCode);
		}

		public bool Equals(KeyCombination combination)
		{
			return false;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
