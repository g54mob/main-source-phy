using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	internal class ProxyCameraRig
	{
		public Camera Camera { get; private set; }

		public Transform CameraTransform { get; private set; }

		public Transform LeftControllerTransform { get; private set; }

		public Transform RightControllerTransform { get; private set; }

		public bool Refresh()
		{
			if (Camera != null && Camera.isActiveAndEnabled)
			{
				return true;
			}
			SearchForCamera();
			return Camera;
		}

		public void SearchForCamera()
		{
			if (RuntimeSettings.Instance.UseCustomIntegrationConfig)
			{
				Camera = CustomIntegrationConfig.GetCamera();
				CameraTransform = Camera?.gameObject.transform;
				LeftControllerTransform = CustomIntegrationConfig.GetLeftControllerTransform();
				RightControllerTransform = CustomIntegrationConfig.GetRightControllerTransform();
				return;
			}
			OVRCameraRig oVRCameraRig = Object.FindAnyObjectByType<OVRCameraRig>();
			if ((bool)oVRCameraRig)
			{
				Camera = oVRCameraRig.leftEyeCamera;
				CameraTransform = oVRCameraRig.leftEyeAnchor;
				LeftControllerTransform = oVRCameraRig.leftControllerAnchor;
				RightControllerTransform = oVRCameraRig.rightControllerAnchor;
			}
		}
	}
}
