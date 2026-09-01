using BlockMapperInternal;
using Localisation;
using UnityEngine;
using mattmc3.dotmore.Extensions;

public class OptionHeaderWidget : ParameterWidget
{
	[SerializeField]
	private DynamicText[] categoryText;

	[SerializeField]
	private DynamicText subtitleText;

	[SerializeField]
	private Transform background;

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		MainOptionsMenu.OptionsCategory cat = (MainOptionsMenu.OptionsCategory)parameter;
		string translation = LocalisationManager.GetTranslation(cat.NameLocID);
		for (int j = 0; j < categoryText.Length; j++)
		{
			ReferenceMaster.SetDynamicText(categoryText[j], translation);
		}
		float num = ((cat.SubtitleLocID <= 0) ? 0.35f : 0.53f);
		if (cat.SubtitleLocID > 0)
		{
			subtitleText.gameObject.SetActive(true);
			string translation2 = LocalisationManager.GetTranslation(cat.SubtitleLocID);
			num += 0.12f * (float)(translation2.SplitLines().Length - 1);
			ReferenceMaster.SetDynamicText(subtitleText, translation2);
			subtitleText.GenerateMesh();
			Bounds bounds = subtitleText.bounds;
			if (cat.SubtitleClicked != null)
			{
				UIButton component = subtitleText.GetComponent<UIButton>();
				component.Click += delegate
				{
					cat.SubtitleClicked();
				};
				component.enabled = true;
				subtitleText.GetComponent<ScaleOnMouseOver>().enabled = true;
				BoxCollider component2 = subtitleText.GetComponent<BoxCollider>();
				component2.size = bounds.size;
				component2.center = bounds.center;
				component2.enabled = true;
			}
		}
		else
		{
			subtitleText.gameObject.SetActive(false);
		}
		background.localScale = Vector3.one.WithY(num);
	}
}
