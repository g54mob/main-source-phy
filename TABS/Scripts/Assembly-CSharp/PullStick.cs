using System;
using TFBGames;
using UnityEngine;

public class PullStick : MonoBehaviour, GameObjectPooling.IPoolable
{
	private Rope rope;

	private TeamHolder team;

	private ProjectileStick stick;

	public float force;

	public float lowMass = 100f;

	private Rigidbody rig;

	private float counter;

	public float projectilePosOffset = -0.2f;

	public float basePosOffset = 0.3f;

	public bool useMaxDistance;

	public float maxDistance = 10f;

	public float forceAmount;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		stick = GetComponent<ProjectileStick>();
		team = GetComponent<TeamHolder>();
		rope = GetComponentInChildren<Rope>();
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void LateUpdate()
	{
		Transform transform = ((team != null && team.spawnerWeapon != null) ? team.spawnerWeapon.transform : null);
		if ((bool)rope && transform != null)
		{
			rope.position1 = base.transform.position + base.transform.forward * projectilePosOffset;
			rope.Position2 = transform.position + transform.forward * basePosOffset;
			rope.middleVelocity += Vector3.up * Mathf.Clamp(base.transform.forward.y, 0f, 1f) * Time.deltaTime * 250f;
		}
		if (stick.stuck)
		{
			counter += Time.deltaTime;
			if (counter > 4f)
			{
				rope.done = true;
				return;
			}
		}
		if (transform == null)
		{
			return;
		}
		float num = Vector3.Distance(transform.position, base.transform.position);
		float num2 = 1f;
		if (num < 1f)
		{
			num2 = 0.1f;
		}
		Vector3 vector = (transform.position - base.transform.position).normalized * rope.stiffnes;
		vector *= num2;
		if ((bool)stick.targetRig)
		{
			if (CanPull())
			{
				rope.stiffnes = Mathf.Lerp(rope.stiffnes, 1f, Time.deltaTime * 1f);
				float num3 = 1f;
				if ((bool)rig)
				{
					num3 = rig.mass * 10f / (stick.targetRig.mass + rig.mass * 10f);
					WilhelmPhysicsFunctions.AddForceWithMinWeight(rig, -vector * (10f * (1f - num3) * Time.deltaTime * force * FixedTimeStepService.SmallForceCoefficient), ForceMode.Force, lowMass);
					rig.velocity += rig.velocity * (1f - num3) * Time.deltaTime * -3f * rope.stiffnes;
				}
				DataHandler componentInChildren = stick.targetRig.transform.root.GetComponentInChildren<DataHandler>();
				if ((bool)componentInChildren)
				{
					componentInChildren.sinceGrounded = 0f;
					WilhelmPhysicsFunctions.AddForceWithMinWeight(stick.targetRig, vector * (Time.deltaTime * num3 * force * FixedTimeStepService.SmallForceCoefficient), ForceMode.Force, lowMass);
					stick.targetRig.velocity += stick.targetRig.velocity * Time.deltaTime * num3 * -3f * rope.stiffnes;
				}
				WilhelmPhysicsFunctions.AddForceWithMinWeight(stick.targetRig, vector * (num3 * Time.deltaTime * force * 0.5f * FixedTimeStepService.SmallForceCoefficient), ForceMode.Force, lowMass);
				stick.targetRig.velocity += stick.targetRig.velocity * Time.deltaTime * num3 * -3f * rope.stiffnes * 0.5f;
				forceAmount = num2 * rope.stiffnes;
			}
		}
		else if (stick.stuck)
		{
			rope.done = true;
		}
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
	}

	public bool CanPull()
	{
		if (team == null || team.spawnerWeapon == null)
		{
			return false;
		}
		float num = Vector3.Distance(team.spawnerWeapon.transform.position, stick.StickPoint);
		if (useMaxDistance)
		{
			if (num > maxDistance)
			{
				return true;
			}
			return false;
		}
		return true;
	}

	private void InitializeOnSpawn()
	{
		rig = ((team != null && team.spawnerWeapon != null) ? team.spawnerWeapon.GetComponent<Rigidbody>() : null);
	}
}
