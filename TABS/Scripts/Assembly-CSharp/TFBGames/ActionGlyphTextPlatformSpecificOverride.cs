using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(ActionGlyphText))]
	public class ActionGlyphTextPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private int overrideGlyphSize;

		[SerializeField]
		private int overrideVerticalAlign;

		[SerializeField]
		private int overrideGlyphVerticalAlign;

		[SerializeField]
		private int overrideSpaceBetweenGlyphAndText;

		[SerializeField]
		private int overrideLineIndent;

		[SerializeField]
		private VerticalLayoutGroup parentLayoutGroup;

		[SerializeField]
		private int overrideLayoutSpacing;

		[SerializeField]
		private int overrideTextSize;

		public void OverrideSettings(ref int glyphSize, ref int verticalAlign, ref int glyphVerticalAlign, ref int spaceBetweenGlyphAndText, ref int overrideIndent, ref int overrideTextSize)
		{
			if (platformsToOverride.HasFlag(GlobalSettingsHandler.CurrentPlatform))
			{
				glyphSize = overrideGlyphSize;
				verticalAlign = overrideVerticalAlign;
				spaceBetweenGlyphAndText = overrideSpaceBetweenGlyphAndText;
				glyphVerticalAlign = overrideGlyphVerticalAlign;
				overrideIndent = overrideLineIndent;
				if (this.overrideTextSize != 0)
				{
					overrideTextSize = this.overrideTextSize;
				}
				if (parentLayoutGroup != null)
				{
					parentLayoutGroup.spacing = overrideLayoutSpacing;
				}
			}
		}

		protected override void ApplyPlatformOverride()
		{
		}
	}
}
