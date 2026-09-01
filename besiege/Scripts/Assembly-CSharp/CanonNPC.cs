using UnityEngine;

public class CanonNPC : SimBehaviour
{
	public float ShootSpeed = 10000f;

	public float ShootRateMin = 3f;

	public float ShootRateMax = 5f;

	public Transform BufferLookAt;

	public Transform CanonBody;

	public Transform Frame;

	public Rigidbody CanonBall;

	public ParticleSystem ShotParticle;

	public Transform BallSporn;

	public float heightOffset;

	public bool shouldShoot = true;

	public int maxShots = 10000;

	public float lastShot;

	public float shotInterval;

	private Transform targetBlock;

	private Vector3 targetPos;

	private int currentShots;

	private bool hasNewPos;

	protected override void Start()
	{
		base.Start();
		if (!base.SimPhysics)
		{
			return;
		}
		if (base.isSimulating)
		{
			currentShots = 0;
			GetTarget();
		}
		else
		{
			BlockBehaviour randomBlock = Machine.Active().GetRandomBlock();
			if (Machine.Active() != null && (bool)randomBlock)
			{
				shouldShoot = true;
				targetPos = randomBlock.transform.position;
			}
			else
			{
				shouldShoot = false;
			}
		}
		hasNewPos = true;
		shotInterval = Random.Range(ShootRateMin, ShootRateMax);
		lastShot = Random.Range(ShootRateMin, ShootRateMax);
	}

	private bool GetTarget()
	{
		if (Machine.Active() == null)
		{
			return false;
		}
		BlockBehaviour randomBlock = Machine.Active().GetRandomBlock();
		if (randomBlock != null)
		{
			targetBlock = randomBlock.transform;
		}
		else
		{
			targetBlock = null;
		}
		return targetBlock != null;
	}

	private void Update()
	{
		if (!base.SimPhysics || !StatMaster.levelSimulating || (targetBlock == null && !GetTarget()))
		{
			return;
		}
		Vector3 position = targetBlock.position;
		if (targetPos.sqrMagnitude != position.sqrMagnitude)
		{
			targetPos = targetBlock.position;
			hasNewPos = true;
		}
		if (currentShots < maxShots)
		{
			lastShot += Time.deltaTime;
			if (lastShot >= shotInterval)
			{
				if (shouldShoot)
				{
					Shoot();
				}
				shotInterval = Random.Range(ShootRateMin, ShootRateMax);
				lastShot = 0f;
			}
		}
		if (hasNewPos && currentShots < maxShots)
		{
			BufferLookAt.position = new Vector3(targetPos.x, BufferLookAt.position.y, targetPos.z);
			Frame.LookAt(BufferLookAt);
			CanonBody.LookAt(new Vector3(targetPos.x, targetPos.y + heightOffset, targetPos.z));
			hasNewPos = false;
		}
	}

	private void Shoot()
	{
		currentShots++;
		ShotParticle.Play();
		GetComponent<AudioSource>().Play();
		Rigidbody rigidbody = Object.Instantiate(CanonBall, BallSporn.position, BallSporn.rotation) as Rigidbody;
		rigidbody.AddForce(CanonBody.forward * ShootSpeed);
		GetTarget();
	}
}
