using UnityEngine;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Passes.Shared
{
	internal static class HBlueNoise
	{
		internal static readonly int g_OwenScrambledTexture;

		internal static readonly int g_ScramblingTileXSPP;

		internal static readonly int g_RankingTileXSPP;

		internal static readonly int g_ScramblingTexture;

		private static Texture2D _owenScrambledTexture;

		private static Texture2D _scramblingTileXSPP;

		private static Texture2D _rankingTileXSPP;

		private static Texture2D _scramblingTexture;

		public static Texture2D OwenScrambledTexture => null;

		public static Texture2D ScramblingTileXSPP => null;

		public static Texture2D RankingTileXSPP => null;

		public static Texture2D ScramblingTexture => null;

		public static void SetTextures(CommandBuffer cmd)
		{
		}
	}
}
