using System.Collections;
using UnityEngine;

public class Parry : MonoBehaviour
{
	private ConditionalEvent conditionalEvent;

	private ParticleSystem part;

	private Rigidbody ownWeapon;

	public string soundRef;

	public float parryPower = 1f;

	public float force;

	private void Start()
	{
		part = GetComponentInChildren<ParticleSystem>();
		conditionalEvent = GetComponent<ConditionalEvent>();
		if ((bool)conditionalEvent.data.weaponHandler)
		{
			if ((bool)conditionalEvent.data.weaponHandler.rightWeapon && (bool)conditionalEvent.data.weaponHandler.rightWeapon.rigidbody)
			{
				ownWeapon = conditionalEvent.data.weaponHandler.rightWeapon.rigidbody;
			}
			else if ((bool)conditionalEvent.data.weaponHandler.leftWeapon && (bool)conditionalEvent.data.weaponHandler.leftWeapon.rigidbody)
			{
				ownWeapon = conditionalEvent.data.weaponHandler.leftWeapon.rigidbody;
			}
		}
	}

	public void DoParry()
	{
		StartCoroutine(ExecuteParry(conditionalEvent.cachedEnemyWeapon));
	}

	private IEnumerator ExecuteParry(Rigidbody enemyWeapon)
	{
		yield return new WaitForSeconds(0.15f);
		if (!enemyWeapon)
		{
			yield break;
		}
		MeleeWeapon component = enemyWeapon.GetComponent<MeleeWeapon>();
		if ((bool)component && !(component.requiredPowerToParry > parryPower))
		{
			component.StopSwing();
			enemyWeapon.AddForce((enemyWeapon.transform.position - conditionalEvent.data.mainRig.position).normalized * force, ForceMode.VelocityChange);
			Vector3 position = (enemyWeapon.position + ownWeapon.transform.position) * 0.5f;
			if ((bool)part && (bool)ownWeapon)
			{
				part.transform.position = position;
				part.Play();
			}
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(soundRef, 1f, position, SoundEffectVariations.MaterialType.Metal);
		}
	}
}
