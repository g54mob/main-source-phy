using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChargeUpBow : MonoBehaviour
{
	private GameObject defPro;

	public GameObject coolPro;

	public ParticleSystem[] parts;

	private bool charging;

	public UnityEvent eventToCall;

	public UnityEvent eventToCall1;

	private RangeWeapon rw;

	private FPSWeapon w;

	private Charges charges;

	private float chargeAmount;

	private Coroutine cor;

	public float del;

	internal bool isCharging;

	public void SetParts(GameObject target)
	{
		parts = target.GetComponentsInChildren<ParticleSystem>(includeInactive: true);
	}

	private void Start()
	{
		charges = GetComponentInChildren<Charges>();
		parts = GetComponentsInChildren<ParticleSystem>();
		w = GetComponent<FPSWeapon>();
		rw = GetComponent<RangeWeapon>();
		rw.AddShootAction(Shoot);
		defPro = rw.ObjectToSpawn;
	}

	private void Update()
	{
		if (!(w.currentChargeUp >= 1f))
		{
			return;
		}
		if (!charging && chargeAmount > 0.1f && charges.ChargeEnabled())
		{
			charging = true;
			for (int i = 0; i < parts.Length; i++)
			{
				parts[i].Play();
			}
			if (cor != null)
			{
				StopCoroutine(cor);
			}
			eventToCall1.Invoke();
			cor = StartCoroutine(Del());
		}
		chargeAmount += Time.deltaTime;
	}

	private void Shoot()
	{
		if (cor != null)
		{
			StopCoroutine(cor);
		}
		charging = false;
		for (int i = 0; i < parts.Length; i++)
		{
			parts[i].Stop();
		}
		rw.ObjectToSpawn = defPro;
		chargeAmount = 0f;
		isCharging = false;
	}

	private IEnumerator Del()
	{
		isCharging = true;
		yield return new WaitForSeconds(del);
		charges.UseCharge();
		eventToCall.Invoke();
		rw.ObjectToSpawn = coolPro;
		isCharging = false;
	}
}
