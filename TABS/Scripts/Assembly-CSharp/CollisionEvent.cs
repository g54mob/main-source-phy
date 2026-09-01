using System;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
	public enum AcceptedCollisionTargets
	{
		All = 0,
		Static = 1,
		Units = 2,
		Rigidbodies = 3,
		EnemyUnits = 4
	}

	public UnityEvent collisionEvent;

	public GameObject objectToSpawn;

	public AcceptedCollisionTargets acceptedCollisionTargets;

	public Action<Collision> collisionAction;

	public float impactMultiplier = 1f;

	public float cd;

	public float startCooldown;

	public bool canHitSelf = true;

	private float cdHolder;

	private float counter;

	private DataHandler data;

	private TeamHolder teamHolder;

	private Team ownTeam;

	private Rigidbody rig;

	private Vector3 spawnPos;

	private Quaternion spawnRotation;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		teamHolder = base.transform.root.GetComponentInChildren<TeamHolder>();
		if ((bool)data)
		{
			ownTeam = data.team;
		}
		else if ((bool)teamHolder)
		{
			ownTeam = teamHolder.team;
		}
		if (startCooldown != 0f)
		{
			cdHolder = startCooldown;
		}
	}

	private void Update()
	{
		counter += Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!rig)
		{
			rig = GetComponent<Rigidbody>();
		}
		if ((bool)teamHolder && collision.transform.root == teamHolder.spawner && !canHitSelf)
		{
			return;
		}
		float num = 0f;
		if ((bool)rig)
		{
			if ((bool)collision.rigidbody)
			{
				_ = collision.rigidbody.mass;
				num = collision.impulse.magnitude / (rig.mass + 10f) * 0.3f;
			}
			else
			{
				num = collision.impulse.magnitude / rig.mass * 0.3f;
			}
		}
		num *= impactMultiplier;
		if (counter < cdHolder || num < 1f || collision.collider.transform.root == base.transform.root)
		{
			return;
		}
		if (acceptedCollisionTargets == AcceptedCollisionTargets.Units)
		{
			if (!collision.transform.root.GetComponent<Unit>())
			{
				return;
			}
		}
		else if (acceptedCollisionTargets == AcceptedCollisionTargets.Rigidbodies)
		{
			if (!collision.rigidbody)
			{
				return;
			}
		}
		else if (acceptedCollisionTargets == AcceptedCollisionTargets.Static)
		{
			if ((bool)collision.rigidbody)
			{
				return;
			}
		}
		else if (acceptedCollisionTargets == AcceptedCollisionTargets.EnemyUnits)
		{
			Unit component = collision.transform.root.GetComponent<Unit>();
			if (!component || component.Team == ownTeam)
			{
				return;
			}
		}
		if ((bool)objectToSpawn)
		{
			spawnPos = collision.GetContact(0).point;
			spawnRotation = Quaternion.LookRotation(collision.GetContact(0).point - base.transform.position);
			UnityEngine.Object.Instantiate(objectToSpawn, spawnPos, spawnRotation);
		}
		collisionAction?.Invoke(collision);
		collisionEvent?.Invoke();
		counter = 0f;
		cdHolder = cd;
	}
}
