using System.Collections.Generic;
using BlockMapperInternal;
using UnityEngine;

public class MenuOptionsContainer : MonoBehaviour, IWidgetContainer
{
	public const float COMPONENT_Z = 0.1f;

	[HideInInspector]
	public WidgetController optionsCategory;

	public float TopValue()
	{
		return base.transform.position.y;
	}

	public float ZValue()
	{
		return base.transform.position.z - 0.1f;
	}

	public void Initialize(string optionsFolder)
	{
		optionsCategory = new WidgetController(optionsFolder + "OptionsCategory");
	}

	public void Rebuild(List<MainOptionsMenu.OptionsCategory> categories)
	{
		optionsCategory.Clear();
		for (int i = 0; i < categories.Count; i++)
		{
			optionsCategory.RegisterToggle(categories[i]);
		}
		optionsCategory.Display(this, 0f);
	}
}
