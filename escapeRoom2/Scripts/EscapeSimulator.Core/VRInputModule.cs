using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.XR;

public class VRInputModule : BaseInputModule
{
	public InputSystemUIInputModule rightHandInputModule;

	public InputSystemUIInputModule leftHandInputModule;

	public override void Process()
	{
		if (EventSystem.current == null)
		{
			Debug.LogError("There is no event system, cannot process VRInputModule.", this);
			return;
		}
		if (rightHandInputModule != null)
		{
			rightHandInputModule.Process();
		}
		if (leftHandInputModule != null)
		{
			leftHandInputModule.Process();
		}
	}

	public bool tryGetLastRaycastResult(int controllerId, out RaycastResult raycastResult)
	{
		bool flag = VR.isLeftControllerId(controllerId);
		XRController xRController = (flag ? XRController.leftHand : XRController.rightHand);
		if (xRController == null)
		{
			raycastResult = default(RaycastResult);
			return false;
		}
		InputSystemUIInputModule inputSystemUIInputModule = (flag ? leftHandInputModule : rightHandInputModule);
		raycastResult = inputSystemUIInputModule.GetLastRaycastResult(xRController.deviceId);
		if (raycastResult.gameObject != null)
		{
			return raycastResult.module is VRRaycaster;
		}
		return false;
	}
}
