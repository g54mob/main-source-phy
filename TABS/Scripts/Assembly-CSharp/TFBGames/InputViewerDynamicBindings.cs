using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class InputViewerDynamicBindings : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The Localised ID for this label, Leave blank if there is no label for this input mode")]
		private string traditionalLocalID;

		[SerializeField]
		[Tooltip("The Localised ID for this label, Leave blank if there is no label for this input mode")]
		private string radialInputLocalID;

		private bool useRadialMenuInputBindings;

		private bool hasSetBinding;

		private LocalizeText localizeText;

		private TextMeshProUGUI textMeshProText;

		private void Awake()
		{
			int currentValue = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("UI_INPUT_MODE").currentValue;
			useRadialMenuInputBindings = currentValue == 0;
			localizeText = GetComponent<LocalizeText>();
			textMeshProText = GetComponent<TextMeshProUGUI>();
			if (localizeText == null || textMeshProText == null)
			{
				base.enabled = false;
			}
		}

		private void Update()
		{
			if (hasSetBinding)
			{
				base.enabled = false;
			}
			SetBinding();
		}

		private void SetBinding()
		{
			localizeText.LocaleID = (useRadialMenuInputBindings ? radialInputLocalID : traditionalLocalID);
			textMeshProText.text = textMeshProText.text.ToUpper();
			hasSetBinding = true;
		}
	}
}
