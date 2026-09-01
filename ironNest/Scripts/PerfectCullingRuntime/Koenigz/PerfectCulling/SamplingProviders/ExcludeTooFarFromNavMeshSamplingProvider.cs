using UnityEngine;

namespace Koenigz.PerfectCulling.SamplingProviders
{
	[RequireComponent(typeof(PerfectCullingBakingBehaviour))]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	public class ExcludeTooFarFromNavMeshSamplingProvider : SamplingProviderBase
	{
		[SerializeField]
		private float distance;

		[Header("Maximum allowance on XZ plane (makes sure only straight up hits are allowed)")]
		[SerializeField]
		[Range(0f, 1f)]
		private float maxDistanceX;

		[SerializeField]
		[Range(0f, 1f)]
		private float maxDistanceZ;

		[Header("Allows to exclude cells below NavMesh (assumes Y is up)")]
		[SerializeField]
		private bool excludeBelowNavMesh;

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
