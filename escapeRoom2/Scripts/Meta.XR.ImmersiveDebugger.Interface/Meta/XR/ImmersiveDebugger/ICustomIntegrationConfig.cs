using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	public interface ICustomIntegrationConfig
	{
		Camera GetCamera();

		Transform GetLeftControllerTransform();

		Transform GetRightControllerTransform();
	}
}
