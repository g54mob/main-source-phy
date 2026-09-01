using UnityEngine;

public abstract class CollisionWeaponEffect : MonoBehaviour
{
	public abstract void DoEffect(Transform hitTransform, Collision collision);
}
