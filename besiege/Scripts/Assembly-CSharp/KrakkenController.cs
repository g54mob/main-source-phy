using System;
using UnityEngine;

[AddComponentMenu("Physics/AI/KrakkenController")]
public class KrakkenController : SimBehaviour
{
	public LimbBodyJointHandler[] tentacles = new LimbBodyJointHandler[0];

	public ExplodeOnTriggerEnter explodeTrigger;

	public AudioSource ambientSfx;

	public AudioSource deathSfx;

	public Color bloodColor = Color.green;

	public static Color BloodColor = Color.green;

	protected override void Start()
	{
		BloodColor = bloodColor * 1.1f;
		if (base.isSimulating)
		{
			for (int i = 0; i < tentacles.Length; i++)
			{
				LimbBodyJointHandler obj = tentacles[i];
				obj.OnDeath = (Action<LimbBodyJointHandler>)Delegate.Combine(obj.OnDeath, new Action<LimbBodyJointHandler>(TentacleDied));
			}
			ExplodeOnTriggerEnter explodeOnTriggerEnter = explodeTrigger;
			explodeOnTriggerEnter.OnExplode = (Action<Rigidbody, bool>)Delegate.Combine(explodeOnTriggerEnter.OnExplode, new Action<Rigidbody, bool>(DamageClosestTentacle));
			ReferenceMaster.onLevelWon = (Action)Delegate.Combine(ReferenceMaster.onLevelWon, new Action(GameOver));
		}
		base.Start();
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelWon = (Action)Delegate.Remove(ReferenceMaster.onLevelWon, new Action(GameOver));
	}

	private void DamageClosestTentacle(Rigidbody b, bool exploded)
	{
		if (!exploded)
		{
			return;
		}
		Vector3 position = b.transform.position;
		int num = 0;
		float num2 = float.MaxValue;
		for (int i = 0; i < tentacles.Length; i++)
		{
			float sqrMagnitude = (tentacles[i].Pos - position).sqrMagnitude;
			if (sqrMagnitude < num2)
			{
				num = i;
				num2 = sqrMagnitude;
			}
		}
		if (tentacles[num].Alive)
		{
			tentacles[num].TakeDamage(6f);
		}
	}

	private void TentacleDied(LimbBodyJointHandler tentacle)
	{
	}

	private void GameOver()
	{
		ambientSfx.Stop();
		deathSfx.Play();
		for (int i = 0; i < tentacles.Length; i++)
		{
			if (tentacles[i].Alive)
			{
				tentacles[i].sfx.Play();
			}
		}
	}
}
