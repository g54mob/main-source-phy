using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(LayoutElement))]
	public class LayoutElementPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private float overrideMinWidth;

		[SerializeField]
		private float overrideMinHeight;

		private LayoutElement layoutElement;

		protected override void ApplyPlatformOverride()
		{
			layoutElement = GetComponent<LayoutElement>();
			layoutElement.minWidth = overrideMinWidth;
			layoutElement.minHeight = overrideMinHeight;
		}
	}
}
