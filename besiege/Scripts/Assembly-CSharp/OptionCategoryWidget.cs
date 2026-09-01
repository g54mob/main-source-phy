using BlockMapperInternal;
using UnityEngine;

public class OptionCategoryWidget : ParameterWidget, IWidgetContainer
{
	protected WidgetController headerController;

	[HideInInspector]
	public WidgetController optionsController;

	[SerializeField]
	protected ContainerDetails container;

	private MainOptionsMenu.OptionsCategory cat;

	public float TopValue()
	{
		return base.transform.position.y;
	}

	public float ZValue()
	{
		return base.transform.position.z;
	}

	protected void Awake()
	{
		headerController = new WidgetController(MainOptionsMenu.OPTIONS_FOLDER + "OptionHeader");
		optionsController = new WidgetController(MainOptionsMenu.OPTIONS_FOLDER + "MainMenuOption");
	}

	public void Rebuild()
	{
		headerController.Clear();
		optionsController.Clear();
		if (cat.NameLocID != -1)
		{
			headerController.RegisterToggle(cat);
		}
		for (int i = 0; i < cat.options.Count; i++)
		{
			MainOptionsMenu.OptionsCategory.MenuOption menuOption = cat.options[i];
			if (!menuOption.hidden)
			{
				optionsController.RegisterToggle(cat.options[i]);
			}
		}
		headerController.Display(this, 0f);
		optionsController.Display(this, headerController.EndPosition);
		float num = 1f / base.transform.lossyScale.y;
		float y = optionsController.EndPosition * num;
		Transform transform = container.Background.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		cat = parameter as MainOptionsMenu.OptionsCategory;
		Rebuild();
	}
}
