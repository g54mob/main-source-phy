using UnityEngine;

namespace TFBGames
{
	public class UIScalePlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private Vector3 scaleOverride = Vector3.one;

		protected override void ApplyPlatformOverride()
		{
			if (base.transform is RectTransform)
			{
				base.transform.localScale = scaleOverride;
			}
		}
	}
}
