using UnityEngine;
using UnityEngine.Events;

public class AssassEffect : UnitEffectBase
{
	public float damagePerStack;

	private HealthHandler health;

	private ParticleSystem part;

	private ParticleSystem.EmissionModule emiss;

	public UnityEvent unityEvent;

	private int stacks;

	private void Awake()
	{
		part = GetComponentInChildren<ParticleSystem>();
		emiss = part.emission;
	}

	public override void DoEffect()
	{
		health = base.transform.root.GetComponentInChildren<HealthHandler>();
		Ping();
	}

	public override void Ping()
	{
		stacks++;
		part.Emit(stacks);
		unityEvent.Invoke();
		health.TakeDamage(damagePerStack * damageMultiplier * (float)stacks, Vector3.up, null);
	}
}
