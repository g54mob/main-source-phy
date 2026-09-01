using UnityEngine;

public class SpawnedAttack : MonoBehaviour
{
	[HideInInspector]
	public Vector3 direction;

	[HideInInspector]
	public Vector3 targetVelocity;

	public void SetData(Vector3 attackDirection, Vector3 attackedTargetVelocity)
	{
		direction = attackDirection;
		targetVelocity = attackedTargetVelocity;
	}
}
