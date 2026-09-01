using BlockMapperInternal;
using Localisation;
using UnityEngine;

public class MainOptionWidget : ParameterWidget
{
	[SerializeField]
	private DynamicText optionName;

	[SerializeField]
	private OptionVisualController optionController;

	[SerializeField]
	private UIButton resetBtn;

	private MainOptionsMenu.OptionsCategory.MenuOption option;

	private ContainerDetails container;

	public void Awake()
	{
		resetBtn.Click += OnReset;
		container = GetComponent<ContainerDetails>();
	}

	private void OnReset()
	{
		option.Reset();
		optionController.UpdateVisual();
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		option = parameter as MainOptionsMenu.OptionsCategory.MenuOption;
		resetBtn.gameObject.SetActive(option.ShowReset());
		string text = ((option.NameLocID > 0) ? LocalisationManager.GetTranslation(option.NameLocID).Replace("\n", " ").ToUpper() : ReferenceMaster.CamelCaseToSpaces(option.name).ToUpper());
		ReferenceMaster.SetDynamicText(optionName, text);
		int num = 22;
		int num2 = 32;
		optionName.letterSpacing = Mathf.Lerp(0.2f, 0f, Mathf.InverseLerp(num, num2, text.Length));
		optionController.Init(option);
		Vector3 localScale = container.Background.localScale;
		container.Background.localScale = new Vector3(localScale.x, option.sizeY, localScale.z);
	}
}
