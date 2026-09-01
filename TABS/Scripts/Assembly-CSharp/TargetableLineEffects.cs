using UnityEngine;

public class TargetableLineEffects : TargetableEffect
{
	public override void DoEffect(Transform startPoint, Transform endPoint)
	{
		GetComponent<LineEffects>().Play(startPoint, endPoint);
	}

	public override void DoEffect(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig)
	{
	}
}
