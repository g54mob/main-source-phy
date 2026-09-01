using UnityEngine;
using UnityEngine.EventSystems;

namespace TFBGames
{
	public class UISliderItem : UISettingsItem
	{
		[SerializeField]
		private UIScaleJiggle scaleJiggle;

		public bool isSelected { get; private set; }

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			scaleJiggle.AddClickForce();
			isSelected = true;
		}

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			scaleJiggle.ResetTargetScale();
			isSelected = false;
		}
	}
}
