using UnityEngine;

namespace Selectors
{
	public class KeyChangeSelector : KeySelector
	{
		public delegate void KeyboardKeyPressed(int index, KeyCode keyCode);

		public event KeysChangeHandler KeysChanged;

		public event KeyboardKeyPressed KeyModified;

		protected override void OnEdit()
		{
			KeysChangeHandler keysChanged = this.KeysChanged;
			if (keysChanged != null)
			{
				keysChanged();
			}
		}

		protected override void OnModifyKey(int index, KeyCode keyCode)
		{
			KeyboardKeyPressed keyModified = this.KeyModified;
			if (keyModified != null)
			{
				keyModified(index, keyCode);
			}
		}
	}
}
