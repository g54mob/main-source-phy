using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public interface IActiveSamplingProvider
	{
		string Name { get; }

		void InitializeSamplingProvider();

		bool IsSamplingPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos);
	}
}
