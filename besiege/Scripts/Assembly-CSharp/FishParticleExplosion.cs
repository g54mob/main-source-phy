using System;
using UnityEngine;

[AddComponentMenu("Water/VFX/FishParticleExplosion")]
public class FishParticleExplosion : SimBehaviour, IExplosionEffect
{
	public ParticleSystem fishSystem;

	private ParticleSystem.CollisionModule colModule;

	private ParticleSystem.Particle[] fishParticles;

	protected override void Start()
	{
		if (StatMaster.levelSimulating)
		{
			OnSimulationToggled(true);
			fishSystem.subEmitters.collision0.gameObject.SetActive(OptionsMaster.BesiegeConfig.BloodEnabled);
			fishSystem.subEmitters.death0.gameObject.SetActive(OptionsMaster.BesiegeConfig.BloodEnabled);
		}
		else
		{
			OnSimulationToggled(false);
		}
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
	}

	private void OnSimulationToggled(bool toggle)
	{
		colModule = fishSystem.collision;
		colModule.enabled = toggle;
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		fishParticles = new ParticleSystem.Particle[fishSystem.particleCount];
		fishSystem.GetParticles(fishParticles);
		Vector3 vector = base.transform.InverseTransformPoint(explosionPos);
		float num = radius / base.transform.lossyScale.x / 3f;
		for (int i = 0; i < fishParticles.Length; i++)
		{
			if ((fishParticles[i].position - vector).sqrMagnitude < num * num)
			{
				fishParticles[i].lifetime = 0.1f;
			}
		}
		fishSystem.SetParticles(fishParticles, fishParticles.Length);
		return true;
	}
}
