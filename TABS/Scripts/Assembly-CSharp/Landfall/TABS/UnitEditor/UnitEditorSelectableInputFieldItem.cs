using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorSelectableInputFieldItem : UnitEditorSelectableItem
	{
		[SerializeField]
		private NavigableTMPTextInput inputField;

		protected override void Start()
		{
			base.Start();
			if (inputField != null)
			{
				inputField.InputDisabled += OnTextInputDisabled;
			}
		}

		private void OnTextInputDisabled()
		{
			Select();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (inputField != null)
			{
				inputField.InputDisabled -= OnTextInputDisabled;
			}
		}
	}
}
