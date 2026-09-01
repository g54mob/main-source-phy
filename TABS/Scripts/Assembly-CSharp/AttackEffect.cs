using UnityEngine;

public abstract class AttackEffect : MonoBehaviour
{
	public abstract void DoEffect(Rigidbody target, Vector3 targetDir);
}
