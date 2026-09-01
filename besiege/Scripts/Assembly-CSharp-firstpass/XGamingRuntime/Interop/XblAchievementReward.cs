using System;

namespace XGamingRuntime.Interop
{
	internal struct XblAchievementReward
	{
		internal readonly UTF8StringPtr name;

		internal readonly UTF8StringPtr description;

		internal readonly UTF8StringPtr value;

		internal readonly XblAchievementRewardType rewardType;

		internal readonly UTF8StringPtr valueType;

		private readonly IntPtr mediaAsset;

		internal T GetMediaAsset<T>(Func<XblAchievementMediaAsset, T> ctor) where T : class
		{
			return Converters.PtrToClass(mediaAsset, ctor);
		}
	}
}
