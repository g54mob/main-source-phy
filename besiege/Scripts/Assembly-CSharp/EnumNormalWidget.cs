using Localisation;
using UnityEngine;

public class EnumNormalWidget : BaseOptionWidget
{
	protected MainOptionsMenu.OptionsCategory.EnumOption enumOption;

	[SerializeField]
	protected DynamicText optionText;

	[SerializeField]
	protected UIButton prevBtn;

	[SerializeField]
	protected UIButton nextBtn;

	[SerializeField]
	protected UIButton enumBtn;

	protected MeshRenderer prevRenderer;

	protected MeshRenderer nextRenderer;

	protected Color initialColor;

	protected Color inactiveColor;

	protected bool isStringEnum;

	protected void Awake()
	{
		prevBtn.Click += OnPrev;
		nextBtn.Click += OnNext;
		enumBtn.Click += OnClicked;
		prevRenderer = prevBtn.GetComponentInChildren<MeshRenderer>();
		nextRenderer = nextBtn.GetComponentInChildren<MeshRenderer>();
		initialColor = prevRenderer.material.GetColor("_TintColor");
		inactiveColor = new Color(initialColor.r, initialColor.g, initialColor.b, initialColor.a * 0.25f);
	}

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

	public override void Set(MainOptionsMenu.OptionsCategory.MenuOption option)
	{
		enumOption = option as MainOptionsMenu.OptionsCategory.EnumOption;
		isStringEnum = option is MainOptionsMenu.OptionsCategory.StringEnumOption;
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		int num = enumOption.getFunc();
		string text = ((!isStringEnum) ? LocalisationManager.GetTranslation(enumOption.optionLocIDs[num]) : (enumOption as MainOptionsMenu.OptionsCategory.StringEnumOption).options[num]);
		ReferenceMaster.SetDynamicText(optionText, text.ToUpper());
		prevRenderer.material.SetColor("_TintColor", (num <= 0) ? inactiveColor : initialColor);
		nextRenderer.material.SetColor("_TintColor", (num >= enumOption.optionLocIDs.Length - 1) ? inactiveColor : initialColor);
	}
}
