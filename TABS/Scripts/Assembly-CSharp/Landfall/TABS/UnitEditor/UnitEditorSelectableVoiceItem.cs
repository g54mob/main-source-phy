using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorSelectableVoiceItem : UnitEditorSelectableItem
	{
		[SerializeField]
		protected Slider pitchSlider;

		[SerializeField]
		private Button previewButton;

		public void SetSliderValue(float value)
		{
			pitchSlider.value = value;
		}

		public void PreviewVoice()
		{
			previewButton.onClick.Invoke();
		}
	}
}
