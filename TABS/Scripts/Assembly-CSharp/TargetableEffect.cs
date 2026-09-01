using UnityEngine;

public abstract class TargetableEffect : MonoBehaviour
{
	public float damageMultiplier = 1f;

	public abstract void DoEffect(Transform startPoint, Transform endPoint);

	public abstract void DoEffect(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig);
}
