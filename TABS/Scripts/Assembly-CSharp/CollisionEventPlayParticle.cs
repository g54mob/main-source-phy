using System;
using UnityEngine;

public class CollisionEventPlayParticle : MonoBehaviour
{
	public enum SpawnDir
	{
		Normal = 0,
		LerpedVel = 1
	}

	public enum SpawnPos
	{
		HitPoint = 0,
		OwnPos = 1
	}

	public ParticleSystem[] parts;

	public SpawnDir spawnDir;

	public SpawnPos spawnPos;

	private Vector3 lerpedVel;

	private Rigidbody rig;

	private void Start()
	{
		CollisionEvent component = GetComponent<CollisionEvent>();
		if ((bool)component)
		{
			component.collisionAction = (Action<Collision>)Delegate.Combine(component.collisionAction, new Action<Collision>(DoCollision));
		}
		GetComponent<CollisionWeapon>()?.AddDealDamageAction(DoWeaponCollision);
		rig = GetComponent<Rigidbody>();
	}

	public void DoCollision(Collision collision)
	{
		for (int i = 0; i < parts.Length; i++)
		{
			if (spawnPos == SpawnPos.HitPoint)
			{
				parts[i].transform.position = collision.GetContact(0).point;
			}
			if (spawnDir == SpawnDir.Normal)
			{
				parts[i].transform.rotation = Quaternion.LookRotation(collision.GetContact(0).normal);
			}
			else
			{
				parts[i].transform.rotation = Quaternion.LookRotation(lerpedVel);
			}
			parts[i].Play();
		}
	}

	public void DoWeaponCollision(Collision collision, float damage, Vector3 dir)
	{
		DoCollision(collision);
	}

	public void Update()
	{
		if (spawnDir == SpawnDir.LerpedVel && (bool)rig)
		{
			lerpedVel = Vector3.Lerp(lerpedVel, rig.velocity, Time.deltaTime * 15f);
		}
	}
}
