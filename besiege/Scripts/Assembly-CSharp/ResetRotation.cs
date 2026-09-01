using UnityEngine;

[AddComponentMenu("UI/Tools/Reset Rotation")]
public class ResetRotation : ClickBehaviour
{
	public MachineRotation MachineRotationCode;

	public override void OnClicked()
	{
		MachineRotationCode.ResetRotation();
	}
}
