using UnityEngine;

public abstract class Move : MonoBehaviour
{
	public abstract void DoMove(Rigidbody enemyWeapon, Rigidbody enemyTorso, DataHandler targetData);
}
