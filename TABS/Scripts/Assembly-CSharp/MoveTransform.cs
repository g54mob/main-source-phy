using System;
using Photon.Bolt;
using UnityEngine;

public class MoveTransform : MonoBehaviour, GameObjectPooling.IPoolable
{
	public Vector3 worldImpulse;

	public Vector3 selfImpulse;

	public float gravity = 15f;

	public Vector3 velocity;

	public float drag;

	public float randomVelocitySpread;

	public float sprea;

	public bool rotationFollowVelocity = true;

	public bool UseCappedDeltaTime;

	[HideInInspector]
	public bool useYDrag;

	private bool didStart;

	private float yVelocity;

	private float velMultiplier = 1f;

	private bool velocityInitialized;

	private float scale = 10f;

	public bool lateVelocityInit;

	public float projectileSpeedSpread { get; set; }

	public byte? RandomSeed { get; set; }

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void Update()
	{
		if (!didStart)
		{
			return;
		}
		float num = Time.deltaTime;
		if (UseCappedDeltaTime)
		{
			num = Mathf.Clamp(num, 0f, 0.02f);
		}
		velocity += num * gravity * Vector3.down;
		if (velocityInitialized)
		{
			base.transform.position += velocity * num * velMultiplier;
		}
		if (Math.Abs(drag) > 0.01f)
		{
			if (useYDrag)
			{
				yVelocity = velocity.y - velocity.y * num * drag;
			}
			else
			{
				yVelocity = velocity.y;
			}
			velocity = new Vector3(velocity.x - velocity.x * num * drag, yVelocity, velocity.z - velocity.z * num * drag);
		}
		if (rotationFollowVelocity)
		{
			base.transform.rotation = Quaternion.LookRotation(velocity);
		}
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
		didStart = false;
		velocity *= 0f;
		RandomSeed = null;
	}

	public void Release()
	{
	}

	private void SetRandomSeed()
	{
		if (BoltNetwork.IsRunning && RandomSeed.HasValue)
		{
			UnityEngine.Random.InitState(RandomSeed.Value);
		}
	}

	private void RestoreRandomSeed()
	{
		if (BoltNetwork.IsRunning && RandomSeed.HasValue)
		{
			UnityEngine.Random.InitState(Environment.TickCount);
		}
	}

	public void UpdateVelMultiplier(float givenVelMultiplier)
	{
		velMultiplier = givenVelMultiplier;
	}

	public void InitializeVelocity()
	{
		velocity += worldImpulse + base.transform.TransformDirection(selfImpulse);
		SetRandomSeed();
		if (Math.Abs(randomVelocitySpread) > 0.01f)
		{
			velocity *= 1f - UnityEngine.Random.Range(0f - randomVelocitySpread, randomVelocitySpread);
		}
		velocity *= UnityEngine.Random.Range(1f - projectileSpeedSpread * 0.01f, 1f + projectileSpeedSpread * 0.01f);
		if (sprea > 0f)
		{
			base.transform.rotation = Quaternion.LookRotation(base.transform.forward + UnityEngine.Random.insideUnitSphere * sprea * 0.01f);
		}
		RestoreRandomSeed();
		if (lateVelocityInit)
		{
			base.transform.parent = null;
		}
		velocityInitialized = true;
	}

	private void InitializeOnSpawn()
	{
		didStart = true;
		velocity *= 0f;
		if (!lateVelocityInit)
		{
			InitializeVelocity();
		}
	}
}
