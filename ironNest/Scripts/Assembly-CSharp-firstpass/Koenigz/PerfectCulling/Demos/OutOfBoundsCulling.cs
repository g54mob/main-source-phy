using UnityEngine;

namespace Koenigz.PerfectCulling.Demos
{
	public class OutOfBoundsCulling : MonoBehaviour
	{
		[Header("Allows for some margin of error")]
		public Vector3 Margin;

		[Header("Camera reference, automatically populated at run-time when null")]
		public Transform CameraTransformReference;

		private PerfectCullingVolume volume;

		private void Awake()
		{
		}

		private void LateUpdate()
		{
		}
	}
}
