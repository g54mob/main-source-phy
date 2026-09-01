using Landfall.TABS;
using UnityEngine;

public class PlayerHealth : Damagable
{
	public float health = 100f;

	[HideInInspector]
	public float maxHealth;

	private CharacterData data;

	private void Start()
	{
		data = GetComponent<CharacterData>();
		maxHealth = health;
	}

	public override void TakeDamage(float damage, Vector3 direction, Unit unit = null, DamageType damageType = DamageType.Default)
	{
		data.screenShake.AddForce(direction.normalized * damage * 0.05f, data.camera.transform.position - direction * 2f);
		health -= damage;
		if (damage <= 0f)
		{
			base.transform.position += Vector3.up * 50f;
		}
	}
}
