using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public class PerfectCullingExcludeVolume : MonoBehaviour, CustomHandle.IResizableByHandle
	{
		private static readonly Bounds UniformBounds;

		[SerializeField]
		public Vector3 volumeSize;

		[SerializeField]
		public PerfectCullingBakingBehaviour[] restrictToBehaviours;

		public Bounds volumeExcludeBounds
		{
			get
			{
				return default(Bounds);
			}
			set
			{
			}
		}

		public Vector3 HandleSized
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public bool IsPositionActive(PerfectCullingBakingBehaviour bakingBehaviour, Vector3 pos)
		{
			return false;
		}
	}
}
