using UnityEngine;

namespace Koenigz.PerfectCulling.SamplingProviders
{
	[RequireComponent(typeof(PerfectCullingBakingBehaviour))]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	public class ExcludeFloatingSamplingProvider : SamplingProviderBase
	{
		[SerializeField]
		private LayerMask layerMask;

		[SerializeField]
		private float distance;

		public override string Name => null;

		public override void InitializeSamplingProvider()
		{
		}

		public override bool IsSamplingPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos)
		{
			return false;
		}
	}
}
