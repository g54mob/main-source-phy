using UnityEngine;

public class DisableCannonJointBreak : MonoBehaviour
{
	public CanonNPCv2 cannon;

	public void OnJointBreak(float breakForce)
	{
		cannon.jointBroken = true;
		cannon.basicInfo.Rigidbody.useGravity = true;
	}
}
