using InControl;
using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	[CreateAssetMenu(menuName = "DataTables/ActionIconTable")]
	public class BindingIconTable : DataTable<BindingIconRow>
	{
		public Sprite GetIcon(string actionName)
		{
			PlayerAction playerActionByName = PlayerActions.Instance.GetPlayerActionByName(actionName);
			InputType inputType = PlayerActions.Instance.InputType;
			if (playerActionByName == null)
			{
				return null;
			}
			Sprite sprite = null;
			for (int i = 0; i < playerActionByName.Bindings.Count; i++)
			{
				BindingSource bindingSource = playerActionByName.Bindings[i];
				if (bindingSource == null)
				{
					continue;
				}
				string text = bindingSource.Name;
				BindingSourceType bindingSourceType = bindingSource.BindingSourceType;
				if (inputType == InputType.Keyboard || inputType == InputType.Any)
				{
					switch (bindingSourceType)
					{
					}
				}
				else if (bindingSourceType == BindingSourceType.DeviceBindingSource)
				{
					if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.XboxOne)
					{
						text += "_XBOX";
					}
					else if (Application.platform == RuntimePlatform.Switch)
					{
						text += "_SWITCH";
					}
					else if (Application.platform == RuntimePlatform.PS4)
					{
						text += "_PS4";
					}
				}
				if (GetRowValue(text) != null)
				{
					sprite = GetRowValue(text).Icon;
				}
				if (sprite != null)
				{
					return sprite;
				}
			}
			return sprite;
		}
	}
}
