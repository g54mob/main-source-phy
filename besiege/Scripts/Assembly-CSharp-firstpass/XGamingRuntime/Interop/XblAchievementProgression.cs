using System;

namespace XGamingRuntime.Interop
{
	internal struct XblAchievementProgression
	{
		private readonly IntPtr requirements;

		private readonly SizeT requirementsCount;

		internal readonly TimeT timeUnlocked;

		internal T[] GetRequirements<T>(Func<XblAchievementRequirement, T> ctor)
		{
			return Converters.PtrToClassArray(requirements, requirementsCount, ctor);
		}
	}
}
