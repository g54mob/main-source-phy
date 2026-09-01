using InControl;
using UnityEngine;

namespace TFBGames
{
	public class DMActionGlyphPlatformSpecificOverride : PlatformSpecificOverride
	{
		[Header("What To Override")]
		[SerializeField]
		[Tooltip("Should we override the glyph size percent?")]
		protected bool m_OverrideGlyphSizePercent;

		[SerializeField]
		[Tooltip("The glyph size percent (100=default size).")]
		protected float m_GlyphSizePercent;

		[Header("Search Criteria")]
		[SerializeField]
		[Tooltip("Only override if the binding source matches m_BindingSourceType")]
		protected bool m_MatchBindingSourceType;

		[SerializeField]
		[Tooltip("Binding source type to search for. (Use \"DeviceBindingSource\" for controllers.)")]
		protected BindingSourceType m_BindingSourceType;

		[Space]
		[SerializeField]
		[Tooltip("Only override if the icon index matches m_IconIndex")]
		protected bool m_MatchIconIndex;

		[SerializeField]
		[Tooltip("The icon/sprite index to search for.")]
		protected int m_IconIndex;

		private bool m_IsPlatformToOverride;

		protected override void ApplyPlatformOverride()
		{
			m_IsPlatformToOverride = true;
		}

		public float? GetGlyphSizePercent(GlyphServiceExtraInfo extraInfo)
		{
			if (extraInfo == null || !m_IsPlatformToOverride || !m_OverrideGlyphSizePercent)
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			if (m_MatchBindingSourceType)
			{
				num++;
				if (extraInfo.BindingSourceType.HasValue && m_BindingSourceType == extraInfo.BindingSourceType)
				{
					num2++;
				}
			}
			if (m_MatchIconIndex)
			{
				num++;
				if (extraInfo.IconIndex.HasValue && m_IconIndex == extraInfo.IconIndex.Value)
				{
					num2++;
				}
			}
			if (num <= 0 || num2 != num)
			{
				return null;
			}
			return m_GlyphSizePercent;
		}
	}
}
