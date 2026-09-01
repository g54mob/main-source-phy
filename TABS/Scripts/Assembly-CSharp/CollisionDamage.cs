using Landfall.TABS;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
	private Damagable health;

	private float impact;

	private HoldingHandler holding;

	private DataHandler ownData;

	private void Start()
	{
		health = base.transform.GetComponentInParent<Damagable>();
		holding = base.transform.GetComponentInParent<HoldingHandler>();
		if ((bool)holding)
		{
			ownData = holding.GetComponent<DataHandler>();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.root == base.transform.root)
		{
			return;
		}
		impact = collision.impulse.magnitude;
		impact *= 0.1f;
		if ((bool)collision.rigidbody)
		{
			if ((bool)holding && (bool)holding.rightObject && holding.rightObject.rig == collision.rigidbody)
			{
				return;
			}
			DamageMultiplier component = collision.rigidbody.GetComponent<DamageMultiplier>();
			if ((bool)component)
			{
				impact *= component.multiplier;
			}
		}
		if (impact < 25f)
		{
			return;
		}
		Unit unit = null;
		if ((bool)ownData)
		{
			unit = ownData.unit;
		}
		else
		{
			TeamHolder componentInParent = GetComponentInParent<TeamHolder>();
			if ((bool)componentInParent && (bool)componentInParent.spawner)
			{
				unit = componentInParent.spawner.GetComponentInParent<Unit>();
			}
		}
		health.TakeDamage(impact, Vector3.zero, unit);
	}
}
