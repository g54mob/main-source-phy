using System.Collections;
using Landfall.TABS;
using Landfall.TABS.GameState;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class WeaponRaycastTrigger : MonoBehaviour
{
	public LayerMask mask;

	public bool getRangeFromUnit = true;

	public float minDelay;

	public float maxDelay = 0.1f;

	private Transform rayPos;

	private Weapon weapon;

	private MeleeWeapon meleeWeapon;

	private float range = 25f;

	private Unit ownUnit;

	private bool isWaitingToAttack;

	private GameStateManager stateListener;

	private NativeArray<RaycastCommand> raycastCommands;

	private NativeArray<RaycastHit> raycastHits;

	private JobHandle jobHandle;

	private void Awake()
	{
		raycastCommands = new NativeArray<RaycastCommand>(1, Allocator.Persistent);
		raycastHits = new NativeArray<RaycastHit>(1, Allocator.Persistent);
	}

	private void Start()
	{
		rayPos = base.transform.Find("RayPos");
		if (!rayPos)
		{
			rayPos = base.transform;
		}
		if (getRangeFromUnit)
		{
			Unit componentInParent = GetComponentInParent<Unit>();
			if ((bool)componentInParent)
			{
				range = componentInParent.m_AttackDistance;
			}
		}
		ownUnit = GetComponentInParent<Unit>();
		stateListener = ServiceLocator.GetService<GameStateManager>();
	}

	private void Update()
	{
		if (stateListener.GameState != GameState.BattleState || ownUnit.data.Dead)
		{
			return;
		}
		if (!weapon)
		{
			SpawnAndConnectRigidbody componentInChildren = GetComponentInChildren<SpawnAndConnectRigidbody>();
			if ((bool)componentInChildren && (bool)componentInChildren.spawnedObject)
			{
				weapon = componentInChildren.spawnedObject.GetComponent<Weapon>();
				if ((bool)weapon)
				{
					meleeWeapon = weapon.GetComponent<MeleeWeapon>();
				}
			}
			return;
		}
		weapon.internalCounter += Time.deltaTime;
		if ((bool)meleeWeapon)
		{
			meleeWeapon.canDealDamage = true;
		}
		jobHandle.Complete();
		RaycastHit rayCastHit = raycastHits[0];
		bool flag = rayCastHit.collider != null;
		try
		{
			if (flag && !(rayCastHit.rigidbody == null) && (object)rayCastHit.transform.root != base.transform.root)
			{
				Unit componentInParent = rayCastHit.transform.GetComponentInParent<Unit>();
				if (componentInParent != null && !componentInParent.dead && ownUnit.Team == componentInParent.Team == ownUnit.targetYourFriends && !weapon.IsOnCooldown())
				{
					Attack(rayCastHit);
				}
			}
		}
		finally
		{
			raycastCommands[0] = new RaycastCommand(rayPos.position, rayPos.forward, range, mask);
			jobHandle = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, 1);
		}
	}

	private void OnDestroy()
	{
		jobHandle.Complete();
		raycastCommands.Dispose();
		raycastHits.Dispose();
	}

	private void Attack(RaycastHit rayCastHit)
	{
		if (!isWaitingToAttack)
		{
			StartCoroutine(DelayAttack(rayCastHit));
		}
	}

	private IEnumerator DelayAttack(RaycastHit rayCastHit)
	{
		isWaitingToAttack = true;
		float seconds = Random.Range(minDelay, maxDelay);
		yield return new WaitForSeconds(seconds);
		weapon.Attack(rayCastHit.point, rayCastHit.rigidbody, Vector3.zero);
		weapon.internalCounter = 0f;
		isWaitingToAttack = false;
	}
}
