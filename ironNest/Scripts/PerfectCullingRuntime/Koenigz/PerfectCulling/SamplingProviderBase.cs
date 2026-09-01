using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public abstract class SamplingProviderBase : MonoBehaviour, IActiveSamplingProvider
	{
		private PerfectCullingBakingBehaviour m_behaviour;

		public abstract string Name { get; }

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public abstract void InitializeSamplingProvider();

		public abstract bool IsSamplingPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos);
	}
}
