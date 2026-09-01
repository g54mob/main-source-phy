using UnityEngine;

namespace Landfall.TABC
{
	public class MainCam : MonoBehaviour
	{
		public static MainCam instance;

		public Camera cam;

		private void Awake()
		{
			cam = GetComponent<Camera>();
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Object.Destroy(this);
			}
			if (!cam.depthTextureMode.HasFlag(DepthTextureMode.MotionVectors))
			{
				cam.depthTextureMode |= DepthTextureMode.MotionVectors;
			}
		}
	}
}
