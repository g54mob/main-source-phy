using UnityEngine;
using UnityEngine.InputSystem;

public class TrackedPositionProcessor : InputProcessor<Vector3>
{
	public bool isRightController;

	public bool isSplitscreen;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void registerProcessor()
	{
		InputSystem.RegisterProcessor<TrackedPositionProcessor>();
	}

	public override Vector3 Process(Vector3 value, InputControl control)
	{
		VR vR = (isSplitscreen ? VR.instanceSplitscreen : VR.instance);
		if (vR == null)
		{
			return value;
		}
		return (isRightController ? vR.rig.rightController : vR.rig.leftController).raycastSource.position;
	}
}
