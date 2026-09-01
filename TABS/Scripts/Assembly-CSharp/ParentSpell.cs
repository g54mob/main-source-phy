using System.Collections;
using UnityEngine;

public class ParentSpell : MonoBehaviour
{
	public float damage;

	public float force;

	public float minMass = 50f;

	public float delay;

	private void Start()
	{
		StartCoroutine(DoEffect());
	}

	private IEnumerator DoEffect()
	{
		Rigidbody rig = base.transform.root.GetComponentInChildren<DataHandler>().mainRig;
		HealthHandler health = base.transform.root.GetComponentInChildren<HealthHandler>();
		if ((bool)rig)
		{
			base.transform.rotation = Quaternion.LookRotation(rig.transform.position - base.transform.position, Random.onUnitSphere);
			base.transform.position = rig.transform.position;
		}
		base.transform.SetParent(null);
		yield return new WaitForSeconds(delay);
		if ((bool)rig)
		{
			force *= Mathf.Clamp(rig.mass / minMass, 0f, 1f);
			rig.AddForce(force * base.transform.forward, ForceMode.Impulse);
		}
		if ((bool)health)
		{
			health.TakeDamage(damage, base.transform.forward, null);
		}
	}
}
