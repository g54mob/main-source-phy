using System;
using UnityEngine;

public class MoveTransformMissile : MonoBehaviour, GameObjectPooling.IPoolable
{
	private SpellTarget target;

	private MoveTransform move;

	public Transform upTarget;

	public float minRot;

	public float maxRot;

	public float drag;

	public float force;

	public float upForce;

	public AnimationCurve upForceOverRange;

	public AnimationCurve upForceOverTimes;

	public AnimationCurve dragOverTime;

	public AnimationCurve forceOverTime;

	public float preditction;

	private float rot;

	private float counter;

	private Vector3 originalVelocity;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		target = GetComponent<SpellTarget>();
		move = GetComponent<MoveTransform>();
		originalVelocity = move.velocity;
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void Update()
	{
		counter += Time.deltaTime;
		if ((bool)target.rig)
		{
			float time = Vector3.Distance(base.transform.position, target.rig.position);
			move.velocity += force * forceOverTime.Evaluate(counter) * Time.deltaTime * (target.rig.position + target.rig.velocity * preditction - base.transform.position).normalized;
			move.velocity += Time.deltaTime * upForce * upForceOverRange.Evaluate(time) * upForceOverTimes.Evaluate(counter) * upTarget.forward;
			move.velocity -= drag * dragOverTime.Evaluate(counter) * Time.deltaTime * move.velocity;
		}
		upTarget.transform.Rotate(rot * Time.deltaTime * base.transform.forward, Space.World);
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
		counter = 0f;
		move.velocity = originalVelocity;
	}

	private void InitializeOnSpawn()
	{
		rot = UnityEngine.Random.Range(minRot, maxRot);
		upTarget.transform.Rotate(base.transform.forward * UnityEngine.Random.Range(0f, 360f), Space.World);
		if (UnityEngine.Random.value < 0.5f)
		{
			rot *= -1f;
		}
	}
}
