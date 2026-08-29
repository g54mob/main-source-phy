using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	public class CustomIntegrationConfigBase : MonoBehaviour, ICustomIntegrationConfig
	{
		private void Awake()
		{
			CustomIntegrationConfig.SetupAllConfig(this);
		}

		private void OnDestroy()
		{
			CustomIntegrationConfig.ClearAllConfig(this);
		}

		public virtual Camera GetCamera()
		{
			return null;
		}

		public virtual Transform GetLeftControllerTransform()
		{
			return null;
		}

		public virtual Transform GetRightControllerTransform()
		{
			return null;
		}
	}
}
