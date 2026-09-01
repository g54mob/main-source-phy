using System.Threading.Tasks;
using UnityEngine;

namespace DM
{
	public static class DMProfanityFilter
	{
		private interface IDMProfanityPlatformFilter
		{
			Task<string> MaskCensoredWordsAsync(string inText);
		}

		private class DMNoProfanityFilter : IDMProfanityPlatformFilter
		{
			public async Task<string> MaskCensoredWordsAsync(string inText)
			{
				await Task.Yield();
				return inText;
			}
		}

		private class DMPlaystationProfanityFilter : IDMProfanityPlatformFilter
		{
			private static bool IsOnline;

			public async Task<string> MaskCensoredWordsAsync(string inText)
			{
				return inText;
			}
		}

		private class DMSwitchProfanityFilter : IDMProfanityPlatformFilter
		{
			public async Task<string> MaskCensoredWordsAsync(string inText)
			{
				await Task.Yield();
				return inText;
			}

			private string MaskCensoredWords(string inText)
			{
				return inText;
			}
		}

		public const string PENDING_CENSORED_TEXT = "...";

		private static IDMProfanityPlatformFilter profanityFilter;

		public static async Task<string> MaskCensoredWordsPlatformAsync(string inText)
		{
			if (profanityFilter == null)
			{
				profanityFilter = GetPlatformProfanityFilter();
			}
			return await profanityFilter.MaskCensoredWordsAsync(inText);
		}

		public static bool IsUsingNoProfanityFilter()
		{
			if (profanityFilter == null)
			{
				profanityFilter = GetPlatformProfanityFilter();
			}
			return profanityFilter is DMNoProfanityFilter;
		}

		public static bool ShouldFilter()
		{
			return !IsUsingNoProfanityFilter();
		}

		private static IDMProfanityPlatformFilter GetPlatformProfanityFilter()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.PS4:
			case RuntimePlatform.PS5:
				return new DMPlaystationProfanityFilter();
			case RuntimePlatform.XboxOne:
			case RuntimePlatform.GameCoreScarlett:
			case RuntimePlatform.GameCoreXboxOne:
				return new DMNoProfanityFilter();
			case RuntimePlatform.Switch:
				return new DMSwitchProfanityFilter();
			default:
				return new DMNoProfanityFilter();
			}
		}
	}
}
