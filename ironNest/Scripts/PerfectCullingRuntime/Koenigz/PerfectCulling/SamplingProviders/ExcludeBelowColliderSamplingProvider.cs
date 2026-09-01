using UnityEngine;

namespace Koenigz.PerfectCulling.SamplingProviders
{
	[RequireComponent(typeof(PerfectCullingBakingBehaviour))]
	[ExecuteAlways]
	public class ExcludeBelowColliderSamplingProvider : SamplingProviderBase
	{
		[Header("Exclude cells below this collider")]
		[SerializeField]
		private Collider excludeBelow;

		[Header("Offset")]
		[SerializeField]
		[Range(-10f, 10f)]
		private float offsetY;

		public override string Name => null;

		public override void InitializeSamplingProvider()
		{
		}

		public override bool IsSamplingPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos)
		{
			return false;
		}

		public float GetOffsetY()
		{
			return 0f;
		}

		public Collider GetCollider()
		{
			return null;
		}
	}
}
