using UnityEngine;
using UnityEngine.UI;

public class CCCategoryTemplateUI : PineUIComponent
{
	public RectTransform root;

	public Transform Spacer;

	public Text Title;

	public GridLayoutGroup VariantsGrid;

	public CCVariantButton VariantsGrid_CCVariant;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
		}
	}
}
