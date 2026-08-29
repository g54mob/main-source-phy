using UnityEngine;
using UnityEngine.InputSystem;

public class TrackedRotationProcessor : InputProcessor<Quaternion>
{
	public bool isRightController;

	public bool isSplitscreen;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void registerProcessor()
	{
		InputSystem.RegisterProcessor<TrackedRotationProcessor>();
	}

	public override Quaternion Process(Quaternion value, InputControl control)
	{
		VR vR = (isSplitscreen ? VR.instanceSplitscreen : VR.instance);
		if (vR == null)
		{
			return value;
		}
		return (isRightController ? vR.rig.rightController : vR.rig.leftController).raycastSource.rotation;
	}
}
