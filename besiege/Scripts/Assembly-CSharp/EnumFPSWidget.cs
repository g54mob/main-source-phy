using Localisation;
using UnityEngine;

public class EnumFPSWidget : EnumNormalWidget
{
	[SerializeField]
	private GameObject Tooltip;

	private void OnClicked()
	{
		int num = enumOption.getFunc();
		if (num < enumOption.optionLocIDs.Length - 1)
		{
			OnNext();
			return;
		}
		enumOption.setFunc(0);
		UpdateVisual();
	}

	private void OnPrev()
	{
		int num = enumOption.getFunc();
		if (num != 0)
		{
			enumOption.setFunc(num - 1);
			UpdateVisual();
		}
	}

	private void OnNext()
	{
		int num = enumOption.getFunc();
		if (num < enumOption.optionLocIDs.Length - 1)
		{
			enumOption.setFunc(num + 1);
			UpdateVisual();
		}
	}

	public override void UpdateVisual()
	{
		int num = enumOption.getFunc();
		string text = string.Format(LocalisationManager.GetTranslation(enumOption.optionLocIDs[num]), FrameRate.GetFPSLock(OptionsMaster.BesiegeConfig));
		ReferenceMaster.SetDynamicText(optionText, text);
		prevRenderer.material.SetColor("_TintColor", (num <= 0) ? inactiveColor : initialColor);
		nextRenderer.material.SetColor("_TintColor", (num >= enumOption.optionLocIDs.Length - 1) ? inactiveColor : initialColor);
		Tooltip.SetActive(num == 0);
	}
}
