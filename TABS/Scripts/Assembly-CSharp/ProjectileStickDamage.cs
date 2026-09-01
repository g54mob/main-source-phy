using UnityEngine;

public class ProjectileStickDamage : MonoBehaviour
{
	private ProjectileStick stick;

	public float damage;

	public void DealDamage()
	{
		if (!stick)
		{
			stick = GetComponent<ProjectileStick>();
		}
		if ((bool)stick && (bool)stick.targetRig)
		{
			HealthHandler componentInParent = stick.targetRig.GetComponentInParent<HealthHandler>();
			if ((bool)componentInParent)
			{
				componentInParent.TakeDamage(damage, Vector3.down, null);
			}
		}
	}
}
