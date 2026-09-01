using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
	public enum ForceDirection
	{
		Outward = 0,
		Forward = 1
	}

	public ForceDirection forceDirection;

	[HideInInspector]
	public List<Rigidbody> protectedRigs = new List<Rigidbody>();

	public LayerMask layerMask;

	public float damage = 60f;

	public float percentDamage;

	public float radius = 4f;

	public float force;

	public float minMassCap = 25f;

	public float stopAttacksFor;

	public bool automatic;

	public bool deltaTime;

	public bool randomForce;

	public bool ignoreTeamMates;

	public bool onlyTeamMates;

	public bool ignoreRoot = true;

	public bool onlyRoot;

	public bool forceOnlyOncePerRig;

	public UnityEvent hitEvent;

	private ExplosionEffect[] effects;

	private ObjectException[] exceptions;

	private TeamHolder teamHolder;

	private bool eventsInvoked;

	private Team team;

	private bool inited;

	private Unit ownUnit;

	public bool addEffectFromParent;

	private AddExplosionEffectToChild explosionEffectToAdd;

	public void Start()
	{
		if (inited)
		{
			return;
		}
		Level componentInParent = GetComponentInParent<Level>();
		if ((bool)componentInParent)
		{
			damage *= componentInParent.levelMultiplier;
			force *= componentInParent.levelMultiplier;
			minMassCap *= componentInParent.levelMultiplier;
			if (componentInParent.ignoreTeam)
			{
				ignoreTeamMates = true;
			}
		}
		inited = true;
		exceptions = GetComponents<ObjectException>();
		effects = GetComponents<ExplosionEffect>();
		teamHolder = GetComponentInParent<TeamHolder>();
		if (addEffectFromParent)
		{
			explosionEffectToAdd = GetComponentInParent<AddExplosionEffectToChild>();
		}
		if (randomForce)
		{
			force *= Random.Range(0.8f, 1.2f);
		}
		if (automatic)
		{
			Explode();
		}
	}

	public void AddProtectedRig(Rigidbody rigToProtect)
	{
		protectedRigs.Add(rigToProtect);
	}

	public void Explode()
	{
		Start();
		float num = (deltaTime ? (Time.deltaTime * 100f) : 1f);
		if ((bool)teamHolder)
		{
			team = teamHolder.team;
		}
		else
		{
			Unit component = base.transform.root.GetComponent<Unit>();
			if ((bool)component)
			{
				team = component.Team;
			}
		}
		List<Rigidbody> list = new List<Rigidbody>();
		List<Damagable> list2 = new List<Damagable>();
		Collider[] array = Physics.OverlapSphere(base.transform.position, radius, layerMask);
		foreach (Collider collider in array)
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if (!(attachedRigidbody != null) || (attachedRigidbody.transform.root == base.transform.root && ignoreRoot) || (attachedRigidbody.transform.root != base.transform.root && onlyRoot))
			{
				continue;
			}
			if (forceOnlyOncePerRig)
			{
				if (list.Contains(attachedRigidbody))
				{
					continue;
				}
				list.Add(attachedRigidbody);
			}
			bool flag = true;
			if (exceptions != null)
			{
				for (int j = 0; j < exceptions.Length; j++)
				{
					flag = exceptions[j].TestException(attachedRigidbody.gameObject);
				}
			}
			if (!flag || protectedRigs.Contains(attachedRigidbody))
			{
				continue;
			}
			if (ignoreTeamMates || onlyTeamMates)
			{
				Unit component2 = attachedRigidbody.transform.root.GetComponent<Unit>();
				if ((bool)component2 && ((ignoreTeamMates && component2.Team == team) || (onlyTeamMates && component2.Team != team)))
				{
					continue;
				}
			}
			DataHandler componentInChildren = attachedRigidbody.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)componentInChildren && !componentInChildren.Dead && !eventsInvoked)
			{
				hitEvent.Invoke();
				eventsInvoked = true;
			}
			if (addEffectFromParent)
			{
				if (explosionEffectToAdd != null)
				{
					explosionEffectToAdd.DoExplosionEffect(attachedRigidbody.gameObject);
				}
			}
			else if (!addEffectFromParent && effects != null)
			{
				for (int k = 0; k < effects.Length; k++)
				{
					effects[k].DoEffect(attachedRigidbody.gameObject);
				}
			}
			float num2 = 1f;
			num2 = Mathf.Clamp(attachedRigidbody.drag / 3f, 0.1f, 1f);
			if (forceDirection == ForceDirection.Outward)
			{
				WilhelmPhysicsFunctions.AddAxplosionForceWithMinWeight(attachedRigidbody, num * force * num2, base.transform.position, radius, ForceMode.Impulse, minMassCap);
			}
			else if (forceDirection == ForceDirection.Forward)
			{
				WilhelmPhysicsFunctions.AddForceWithMinWeight(attachedRigidbody, num * force * num2 * base.transform.forward, ForceMode.Impulse, minMassCap);
			}
			attachedRigidbody.velocity *= 0.9f;
			Damagable componentInParent = attachedRigidbody.GetComponentInParent<Damagable>();
			if ((bool)componentInParent)
			{
				if (!componentInParent.GetComponent<DataHandler>().Dead && !list2.Contains(componentInParent))
				{
					list2.Add(componentInParent);
				}
				if (!ownUnit && (bool)teamHolder && (bool)teamHolder.spawner)
				{
					ownUnit = teamHolder.spawner.GetComponentInParent<Unit>();
				}
				if (!ownUnit)
				{
					ownUnit = GetComponentInParent<Unit>();
				}
				float num3 = num * Mathf.Clamp(1f - Vector3.Distance(base.transform.position, collider.ClosestPoint(base.transform.position)) / radius, 0f, 1f);
				Vector3 direction = collider.transform.position - base.transform.position;
				componentInParent.TakeDamage(damage * num3, direction, ownUnit);
				if (percentDamage > 0f)
				{
					componentInParent.TakeDamage(componentInChildren.maxHealth * percentDamage * num3, direction, ownUnit);
				}
			}
			if (!(stopAttacksFor > 0f))
			{
				continue;
			}
			WeaponHandler componentInChildren2 = attachedRigidbody.transform.root.GetComponentInChildren<WeaponHandler>();
			if ((bool)componentInChildren2)
			{
				componentInChildren2.StopAttacksFor(stopAttacksFor);
				continue;
			}
			MultipleWeaponHandler componentInChildren3 = attachedRigidbody.transform.root.GetComponentInChildren<MultipleWeaponHandler>();
			if ((bool)componentInChildren3)
			{
				componentInChildren3.StopAttacksFor(stopAttacksFor);
			}
		}
		eventsInvoked = false;
		CheckAchivements(list2);
	}

	private void CheckAchivements(List<Damagable> damagables)
	{
		if (damagables != null && !(ownUnit == null))
		{
			StartCoroutine(Delay(damagables));
		}
	}

	private IEnumerator Delay(List<Damagable> damagables)
	{
		yield return null;
		AchievementService service = ServiceLocator.GetService<AchievementService>();
		int iD = ownUnit.unitBlueprint.Entity.GUID.m_ID;
		int num = 0;
		foreach (Damagable damagable in damagables)
		{
			if (damagable != null && damagable.GetComponent<DataHandler>().Dead)
			{
				num++;
			}
		}
		int num2 = 1769010781;
		if (num >= 10 && iD == num2)
		{
			service.UnlockAchievement("CATAPULT_KILL_UNIT");
		}
		int num3 = 478392247;
		if (num >= 9 && iD == num3)
		{
			service.UnlockAchievement("TREEGIANT_KILL_UNIT");
		}
	}
}
