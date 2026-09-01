using UnityEngine;

public class DealDamageToParentUnit : MonoBehaviour
{
	public float dmg;

	public void Go()
	{
		base.transform.root.GetComponentInChildren<HealthHandler>().TakeDamage(dmg, Vector2.up, null);
	}
}
