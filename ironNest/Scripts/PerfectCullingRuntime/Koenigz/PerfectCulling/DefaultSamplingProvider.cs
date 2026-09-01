using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public static class DefaultSamplingProvider
	{
		public enum Result
		{
			None = 0,
			IncludeCell = 1,
			ExcludeCell = 2
		}

		private static PerfectCullingExcludeVolume[] m_excludeVolumes;

		private static PerfectCullingAlwaysIncludeVolume[] m_alwaysIncludeVolumes;

		public static void InitializeSamplingProvider()
		{
		}

		public static Result IsSamplingPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos)
		{
			return default(Result);
		}
	}
}
