using UnityEngine;
using UnityEngine.Serialization;

namespace Koenigz.PerfectCulling
{
	public class PerfectCullingPortalCell : MonoBehaviour
	{
		[FormerlySerializedAs("CullingVolume")]
		public PerfectCullingVolume cullingVolume;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnDrawGizmos()
		{
		}
	}
}
