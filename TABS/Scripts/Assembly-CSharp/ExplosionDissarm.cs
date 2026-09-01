using UnityEngine;

public class ExplosionDissarm : ExplosionEffect
{
	public float chance = 1f;

	public override void DoEffect(GameObject target)
	{
		if (Random.value < chance)
		{
			Holdable component = target.GetComponent<Holdable>();
			if ((bool)component && component.held)
			{
				component.Dissarm();
			}
		}
	}
}
