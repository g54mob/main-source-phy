using System;
using UnityEngine;

public class AirshipCannon : SimBehaviour, IParticlePlay
{
	public float ReloadTime = 5f;

	public float power = 10000f;

	public Transform SpawnPos;

	public float distance = 10f;

	public float radius = 4f;

	public ParticleSystem[] particles;

	public RandomSoundController sfx;

	public FreighterAI ai;

	public LayerMask mask;

	private float timer;

	private int phase;

	private ushort playerID;

	private bool useProjManager;

	private Transform projectile;

	public GameObject projectilePrefab;

	protected override void Start()
	{
		useProjManager = StatMaster.levelSimulating && StatMaster.isHosting && !StatMaster.isLocalSim && StatMaster.isMP;
		playerID = (ushort)(StatMaster.isMP ? BesiegeNetworkManager.Instance.PlayerID : 0);
		base.Start();
	}

	private void FixedUpdate()
	{
		if ((StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim) || ai.machineBroken || !base.isSimulating)
		{
			return;
		}
		switch (phase)
		{
		case 0:
			if (timer > ReloadTime + UnityEngine.Random.Range(-0.1f, 0.1f))
			{
				phase = 1;
			}
			timer += Time.fixedDeltaTime;
			break;
		case 1:
			if (Physics.CheckCapsule(SpawnPos.position + SpawnPos.forward * radius, SpawnPos.position + SpawnPos.forward * (distance - radius), radius, mask))
			{
				phase = 2;
			}
			break;
		case 2:
		{
			Collider[] array = Physics.OverlapCapsule(SpawnPos.position + SpawnPos.forward * radius, SpawnPos.position + SpawnPos.forward * (distance - radius), radius, mask);
			for (int i = 0; i < array.Length; i++)
			{
				if ((bool)array[i].attachedRigidbody && !array[i].attachedRigidbody.isKinematic && !(array[i].attachedRigidbody.transform == base.transform.parent))
				{
					phase = 3;
					break;
				}
			}
			if (phase == 2)
			{
				phase = 1;
			}
			break;
		}
		case 3:
			ShootBall();
			timer = 0f;
			phase = 0;
			break;
		}
	}

	private void ShootBall()
	{
		Vector3 vector = Mathf.Min(SpawnPos.lossyScale.x, SpawnPos.lossyScale.y) * projectilePrefab.transform.localScale / ai.transform.localScale.z;
		if (useProjManager)
		{
			byte[] array = new byte[19];
			int num = 0;
			NetworkCompression.CompressPosition(SpawnPos.position, array, num);
			num += 6;
			NetworkCompression.CompressRotation(SpawnPos.rotation, array, num);
			num += 7;
			NetworkCompression.CompressVector(vector, 0f, 100f, array, num);
			projectile = ProjectileManager.Instance.Spawn(NetworkProjectileType.Cannon, NetworkAddPiece.Instance.frame, playerID, array);
			projectile.localScale = vector;
		}
		else
		{
			projectile = (UnityEngine.Object.Instantiate(projectilePrefab, SpawnPos.position, SpawnPos.rotation, ReferenceMaster.physicsGoalInstance) as GameObject).transform;
		}
		Rigidbody component = projectile.GetComponent<Rigidbody>();
		component.AddForce(SpawnPos.forward * power);
		component.AddTorque(SpawnPos.forward * 500f);
		PlayParticles();
	}

	public bool PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
		if ((bool)sfx)
		{
			sfx.Play();
		}
		if (StatMaster.isHosting && base.SimPhysics)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.CannonParticles);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		return true;
	}
}
