using System.Collections;
using Landfall.TABS;
using UnityEngine;

public class StunEffect : UnitEffectBase
{
	public float stunDelay = 0.2f;

	public float stunTime = 0.5f;

	public float damage = 100f;

	public float force;

	public float forceUp;

	public float scalingForce;

	public bool scaleStunWithMaxHealth;

	public UnitColorInstance color;

	public Gradient gradient;

	public AnimationCurve colorCurve;

	private float currentColorValue;

	private float currentTime = 1f;

	private Rigidbody[] rigs;

	private Rigidbody mainRig;

	private HealthHandler health;

	private UnitColorHandler colorHandler;

	private ParticleSystem[] part;

	private DataHandler data;

	private bool effectDone;

	private bool removedColor;

	public float effectDuration = 0.35f;

	private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

	private float speed = 1f;

	public override void DoEffect()
	{
		speed *= Random.Range(0.85f, 1.5f);
		stunTime *= Random.Range(0.85f, 1.5f);
		stunDelay *= Random.Range(0.85f, 1.5f);
		force *= Random.Range(0.5f, 3f);
		forceUp *= Random.Range(0.5f, 3f);
		scalingForce *= Random.Range(0.5f, 3f);
		colorHandler = base.transform.root.GetComponentInChildren<UnitColorHandler>();
		part = GetComponentsInChildren<ParticleSystem>();
		rigs = base.transform.root.GetComponentsInChildren<Rigidbody>();
		mainRig = base.transform.root.GetComponent<Unit>().data.mainRig;
		health = base.transform.root.GetComponent<Unit>().data.healthHandler;
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		if (scaleStunWithMaxHealth && (bool)data)
		{
			stunTime *= Mathf.Clamp(250f / data.maxHealth, 0f, 1.5f);
		}
		Ping();
	}

	public override void Ping()
	{
		for (int i = 0; i < part.Length; i++)
		{
			part[i].transform.position = mainRig.transform.position;
			part[i].Play();
		}
		StopAllCoroutines();
		StartCoroutine(DoStun());
		currentTime = 0f;
		effectDone = false;
		removedColor = false;
	}

	private IEnumerator DoStun()
	{
		mainRig.AddForceAtPosition(base.transform.forward * force + Vector3.up * forceUp, mainRig.position + Random.insideUnitSphere * 0.3f, ForceMode.VelocityChange);
		mainRig.AddForceAtPosition(base.transform.forward * scalingForce, mainRig.position + Random.insideUnitSphere * 0.3f, ForceMode.Impulse);
		health.TakeDamage(damage, base.transform.forward, null);
		TeamHolder componentInParent = GetComponentInParent<TeamHolder>();
		if ((bool)componentInParent && (bool)componentInParent.spawner)
		{
			Unit component = componentInParent.spawner.GetComponent<Unit>();
			if ((bool)component)
			{
				component.DealDamage(damage);
			}
		}
		yield return new WaitForSeconds(stunDelay);
		health.GetComponent<GravityHandler>().enabled = false;
		health.GetComponent<DataHandler>().fallTime = 0.5f;
		float c = 0f;
		while (c < stunTime)
		{
			c += Time.fixedDeltaTime;
			yield return waitForFixedUpdate;
			for (int i = 0; i < rigs.Length; i++)
			{
				rigs[i].velocity *= 0f;
				rigs[i].angularVelocity *= 0f;
				rigs[i].useGravity = false;
			}
		}
		float f = 0f;
		while (f < 1f * stunTime)
		{
			f += Time.fixedDeltaTime;
			yield return waitForFixedUpdate;
			for (int j = 0; j < rigs.Length; j++)
			{
				rigs[j].velocity *= f / stunTime;
				rigs[j].angularVelocity *= f / stunTime;
			}
		}
		for (int k = 0; k < rigs.Length; k++)
		{
			rigs[k].useGravity = true;
		}
		health.GetComponent<GravityHandler>().enabled = true;
	}

	private void Update()
	{
		currentTime += Time.deltaTime * speed;
		currentColorValue = Mathf.Lerp(currentColorValue, colorCurve.Evaluate(currentTime), Time.deltaTime * 5f);
		if ((bool)colorHandler && currentTime < effectDuration)
		{
			color.color = gradient.Evaluate(currentTime);
			if (!effectDone)
			{
				colorHandler.SetColor(color, colorCurve.Evaluate(currentTime));
			}
		}
		if (currentTime >= effectDuration)
		{
			effectDone = true;
		}
		if (!effectDone || removedColor)
		{
			return;
		}
		for (int i = 0; i < colorHandler.colors.Count; i++)
		{
			if (color.colorName == colorHandler.colors[i].colorName)
			{
				colorHandler.colors.RemoveAt(i);
			}
		}
		removedColor = true;
	}
}
