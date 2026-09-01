using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class CameraDetector : MonoBehaviour
	{
		public delegate void OnNewCameraFoundDelegate(Camera cam);

		public OnNewCameraFoundDelegate OnNewCameraFound;

		private static CameraDetector _instance;

		protected Camera[] _previousCameras;

		protected Camera[] _cameras;

		public static CameraDetector Instance => null;

		public Camera[] Cameras => null;

		private CameraDetector()
		{
		}

		private void Update()
		{
		}

		protected void increaseCapacity()
		{
		}

		protected bool contains(Camera[] cameras, Camera cam)
		{
			return false;
		}
	}
}
