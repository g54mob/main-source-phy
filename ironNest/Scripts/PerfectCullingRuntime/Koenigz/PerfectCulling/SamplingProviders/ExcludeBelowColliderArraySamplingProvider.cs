using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling.SamplingProviders
{
	[RequireComponent(typeof(PerfectCullingBakingBehaviour))]
	[ExecuteAlways]
	public class ExcludeBelowColliderArraySamplingProvider : SamplingProviderBase
	{
		private static readonly Collider[] OverlapCollidersNonAllocBuffer;

		[Header("Exclude cells below colliders")]
		[SerializeField]
		private Collider[] excludeBelow;

		[Header("Offset")]
		[SerializeField]
		[Range(-10f, 10f)]
		private float offsetY;

		[Header("Exclude cells inside colliders")]
		[SerializeField]
		private bool excludeInsideColliders;

		public override string Name => null;

		public override void InitializeSamplingProvider()
		{
		}

		public override bool IsSamplingPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos)
		{
			return false;
		}

		public void Migrate(float kvKey, HashSet<Collider> kvValue)
		{
		}
	}
}
