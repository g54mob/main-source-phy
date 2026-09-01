using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(CanvasScaler))]
	public class CanvasScalerPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private CanvasScaler.ScreenMatchMode screenMatchMode;

		[SerializeField]
		[Range(0f, 1f)]
		private float matchWidthOrHeight;

		protected override void ApplyPlatformOverride()
		{
			CanvasScaler component = GetComponent<CanvasScaler>();
			component.screenMatchMode = screenMatchMode;
			if (component.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
			{
				component.matchWidthOrHeight = matchWidthOrHeight;
			}
		}
	}
}
