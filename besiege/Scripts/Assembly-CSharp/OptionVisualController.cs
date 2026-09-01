using UnityEngine;

public class OptionVisualController : MonoBehaviour
{
	private enum OptionWidgetType
	{
		EnumNormal = 0,
		EnumDropdown = 1,
		EnumFPS = 2,
		Value = 3,
		Controls = 4,
		None = 5
	}

	public BaseOptionWidget[] widgets;

	private BaseOptionWidget currentWidget;

	public void Init(MainOptionsMenu.OptionsCategory.MenuOption option)
	{
		if (option is MainOptionsMenu.OptionsCategory.EnumOption)
		{
			MainOptionsMenu.OptionsCategory.EnumOption enumOption = option as MainOptionsMenu.OptionsCategory.EnumOption;
			switch (enumOption.display)
			{
			case MainOptionsMenu.OptionsCategory.EnumOption.DisplayOption.Dropdown:
				SetWidget(OptionWidgetType.EnumDropdown, option);
				break;
			case MainOptionsMenu.OptionsCategory.EnumOption.DisplayOption.FPS:
				SetWidget(OptionWidgetType.EnumFPS, option);
				break;
			default:
				SetWidget(OptionWidgetType.EnumNormal, option);
				break;
			}
		}
		else if (option is MainOptionsMenu.OptionsCategory.ValueOption)
		{
			SetWidget(OptionWidgetType.Value, option);
		}
		else if (option is MainOptionsMenu.OptionsCategory.ControlsOption)
		{
			SetWidget(OptionWidgetType.Controls, option);
		}
		else
		{
			SetWidget(OptionWidgetType.None, option);
		}
	}

	public void UpdateVisual()
	{
		if (!(currentWidget == null))
		{
			currentWidget.UpdateVisual();
		}
	}

	private void SetWidget(OptionWidgetType type, MainOptionsMenu.OptionsCategory.MenuOption option)
	{
		for (int i = 0; i < widgets.Length; i++)
		{
			BaseOptionWidget baseOptionWidget = widgets[i];
			if (i == (int)type)
			{
				baseOptionWidget.gameObject.SetActive(true);
				baseOptionWidget.Set(option);
				currentWidget = baseOptionWidget;
			}
			else if (baseOptionWidget.gameObject.activeInHierarchy)
			{
				baseOptionWidget.gameObject.SetActive(false);
			}
		}
	}
}
