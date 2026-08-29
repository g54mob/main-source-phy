using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	public static class CustomIntegrationConfig
	{
		public delegate Camera GetCameraDelegate();

		public delegate Transform GetLeftControllerTransformDelegate();

		public delegate Transform GetRightControllerTransformDelegate();

		public static event GetCameraDelegate GetCameraHandler;

		public static event GetLeftControllerTransformDelegate GetLeftControllerTransformHandler;

		public static event GetRightControllerTransformDelegate GetRightControllerTransformHandler;

		public static void SetupAllConfig(ICustomIntegrationConfig customConfig)
		{
			GetCameraHandler += customConfig.GetCamera;
			GetLeftControllerTransformHandler += customConfig.GetLeftControllerTransform;
			GetRightControllerTransformHandler += customConfig.GetRightControllerTransform;
		}

		public static void ClearAllConfig(ICustomIntegrationConfig customConfig)
		{
			GetCameraHandler -= customConfig.GetCamera;
			GetLeftControllerTransformHandler -= customConfig.GetLeftControllerTransform;
			GetRightControllerTransformHandler -= customConfig.GetRightControllerTransform;
		}

		public static Camera GetCamera()
		{
			return CustomIntegrationConfig.GetCameraHandler?.Invoke();
		}

		public static Transform GetLeftControllerTransform()
		{
			return CustomIntegrationConfig.GetLeftControllerTransformHandler?.Invoke();
		}

		public static Transform GetRightControllerTransform()
		{
			return CustomIntegrationConfig.GetRightControllerTransformHandler?.Invoke();
		}
	}
}
