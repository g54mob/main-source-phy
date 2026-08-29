using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	public class ExampleCustomIntegrationConfig : CustomIntegrationConfigBase
	{
		public override Camera GetCamera()
		{
			return GameObject.Find("MainCamera").GetComponent<Camera>();
		}

		public override Transform GetLeftControllerTransform()
		{
			return GameObject.Find("LeftController").transform;
		}

		public override Transform GetRightControllerTransform()
		{
			return GameObject.Find("RightController").transform;
		}
	}
}
