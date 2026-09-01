using UnityEngine;

namespace Koenigz.PerfectCulling.SamplingProviders
{
	[RequireComponent(typeof(PerfectCullingBakingBehaviour))]
	[ExecuteAlways]
	public class ExcludeInsideCollidersSamplingProvider : SamplingProviderBase
	{
		private static readonly Collider[] OverlapCollidersNonAllocBuffer;

		[Header("Notice: Concave MeshColliders are unsupported by PhysX and thus ignored")]
		public LayerMask layerMask;

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
