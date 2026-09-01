using Landfall.TABS;
using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
	public virtual void TakeDamage(float damage, Vector3 direction, Unit unit, DamageType damageType = DamageType.Default)
	{
	}
}
