using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblAchievement
	{
		internal readonly UTF8StringPtr id;

		internal readonly UTF8StringPtr serviceConfigurationId;

		internal readonly UTF8StringPtr name;

		private readonly IntPtr titleAssociations;

		private readonly SizeT titleAssociationsCount;

		internal readonly XblAchievementProgressState progressState;

		internal readonly XblAchievementProgression progression;

		private readonly IntPtr mediaAssets;

		private readonly SizeT mediaAssetsCount;

		private readonly IntPtr platformsAvailableOn;

		private readonly SizeT platformsAvailableOnCount;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isSecret;

		internal readonly UTF8StringPtr unlockedDescription;

		internal readonly UTF8StringPtr lockedDescription;

		internal readonly UTF8StringPtr productId;

		internal readonly XblAchievementType type;

		internal readonly XblAchievementParticipationType participationType;

		internal readonly XblAchievementTimeWindow available;

		private readonly IntPtr rewards;

		private readonly SizeT rewardsCount;

		internal readonly ulong estimatedUnlockTime;

		internal readonly UTF8StringPtr deepLink;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isRevoked;

		internal T[] GetTitleAssociations<T>(Func<XblAchievementTitleAssociation, T> ctor)
		{
			return Converters.PtrToClassArray(titleAssociations, titleAssociationsCount, ctor);
		}

		internal T[] GetMediaAssets<T>(Func<XblAchievementMediaAsset, T> ctor)
		{
			return Converters.PtrToClassArray(mediaAssets, mediaAssetsCount, ctor);
		}

		internal string[] GetPlatformsAvailableOn()
		{
			return Converters.PtrToClassArray(platformsAvailableOn, platformsAvailableOnCount, (UTF8StringPtr s) => s.GetString());
		}

		internal T[] GetRewards<T>(Func<XblAchievementReward, T> ctor)
		{
			return Converters.PtrToClassArray(rewards, rewardsCount, ctor);
		}
	}
}
