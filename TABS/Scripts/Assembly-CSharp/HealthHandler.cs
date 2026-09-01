using System;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Photon.Bolt;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandler : Damagable
{
	public UnityEvent deathEvent;

	private EyeSpawner eyeSpawner;

	private DataHandler data;

	private WeaponHandler weaponHandler;

	private Unit unit;

	private Action DieAction;

	public bool willBeRewived;

	private UnitSounds sounds;

	private bool isInvulnerable;

	private GameModeService gameModeService;

	private bool isRestrictedInGameMode;

	private static Collider[] colliders = new Collider[256];

	private GameStateManager m_gameStateManager;

	private SettingsInstance m_bugUnitsDying;

	private Action takeDamageAction;

	public bool WasKilledRemotely { get; private set; }

	public bool DiedLocally { get; set; }

	public event Action<Unit> UnitDied;

	private void Awake()
	{
		sounds = base.transform.root.GetComponentInChildren<UnitSounds>();
		unit = GetComponentInParent<Unit>();
		data = GetComponent<DataHandler>();
		eyeSpawner = GetComponentInParent<EyeSpawner>();
		weaponHandler = GetComponent<WeaponHandler>();
		m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		m_bugUnitsDying = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("BUG_UNITS_NOT_DYING");
		gameModeService = ServiceLocator.GetService<GameModeService>();
		if (gameModeService != null)
		{
			isRestrictedInGameMode = gameModeService.IsGameModeRestricted();
		}
	}

	public void Update()
	{
		if ((bool)data.mainRig && (data.mainRig.position.magnitude > 2000f || float.IsNaN(data.mainRig.position.x)))
		{
			bool flag = false;
			if (!data.Dead)
			{
				flag = Die();
			}
			if (flag)
			{
				UnityEngine.Object.Destroy(base.transform.root.gameObject);
			}
		}
		if (!data.Dead && (bool)data.mainRig && data.mainRig.position.y < MapSettings.KillHeight)
		{
			Die();
		}
	}

	public override void TakeDamage(float damage, Vector3 direction, Unit damager = null, DamageType damageType = DamageType.Default)
	{
		if (m_gameStateManager == null)
		{
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		}
		if ((m_gameStateManager == null || m_gameStateManager.GameState == GameState.BattleState) && !isInvulnerable && (!(data.immunityForSeconds > 0f) || !(damage > 0f)) && !(data.lifeTime < 0.3f) && !unit.WasDamaged(null, null))
		{
			if (takeDamageAction != null)
			{
				takeDamageAction();
			}
			unit.WasDamaged(damage);
			if ((bool)damager)
			{
				damager.DealDamage(damage);
			}
			bool flag = true;
			if (Bugs._DLC_ACTIVATED && m_bugUnitsDying.currentValue == 1)
			{
				flag = isRestrictedInGameMode;
			}
			if (flag)
			{
				data.health -= damage;
				data.health = Mathf.Clamp(data.health, float.NegativeInfinity, data.maxHealth);
			}
			if (data.health <= 0f)
			{
				Die(damager);
			}
			else
			{
				DamageFeedback(damage);
			}
		}
	}

	private void DamageFeedback(float damage)
	{
		float num = damage / data.maxHealth;
		if (num < 0.1f)
		{
			return;
		}
		if ((bool)sounds)
		{
			sounds.PlayVocalSound(4, num, data.mainRig.position);
		}
		if ((bool)eyeSpawner && (bool)GooglyEyes.instance)
		{
			for (int i = 0; i < eyeSpawner.spawnedEyes.Count; i++)
			{
				GooglyEyes.instance.Startle(eyeSpawner.spawnedEyes[i], num);
			}
		}
	}

	public bool Die(Unit damager = null)
	{
		bool result = true;
		if (BoltNetwork.IsClient && !WasKilledRemotely)
		{
			result = false;
		}
		if (m_gameStateManager == null)
		{
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		}
		if (m_gameStateManager != null && m_gameStateManager.GameState != GameState.BattleState)
		{
			return result;
		}
		if (data.Dead)
		{
			return result;
		}
		if (BoltNetwork.IsClient && !WasKilledRemotely && BoltNetwork.IsConnected)
		{
			return false;
		}
		if (DieAction != null)
		{
			DieAction();
		}
		if (deathEvent != null)
		{
			deathEvent.Invoke();
		}
		this.UnitDied?.Invoke(unit);
		CheckDeathAchievements(damager);
		data.Dead = true;
		if ((bool)sounds)
		{
			sounds.PlayDeathSound(0.5f, data.mainRig.position);
		}
		HoldingHandler component = GetComponent<HoldingHandler>();
		if ((bool)component)
		{
			component.LetGoOfAll();
		}
		if (!willBeRewived)
		{
			GroundChecker[] componentsInChildren = GetComponentsInChildren<GroundChecker>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				UnityEngine.Object.Destroy(componentsInChildren[i]);
			}
			Rigidbody[] allRigs = GetComponent<RigidbodyHolder>().AllRigs;
			for (int j = 0; j < allRigs.Length; j++)
			{
				allRigs[j].gameObject.layer = 9;
			}
		}
		if (UnityEngine.Random.value > 0.995f)
		{
			int num = Physics.OverlapSphereNonAlloc(data.mainRig.position, UnityEngine.Random.Range(1f, 2f), colliders);
			for (int k = 0; k < num; k++)
			{
				GeneralInput componentInParent = colliders[k].GetComponentInParent<GeneralInput>();
				if ((bool)componentInParent && componentInParent.GetComponentInParent<Unit>().Team == base.transform.root.GetComponent<Unit>().Team)
				{
					componentInParent.shift = true;
					break;
				}
			}
		}
		return true;
	}

	public void AssignDamageAction(Action action)
	{
		takeDamageAction = (Action)Delegate.Combine(takeDamageAction, action);
	}

	public void AddDieAction(Action action)
	{
		DieAction = (Action)Delegate.Combine(DieAction, action);
	}

	public void RemoveDieAction(Action action)
	{
		DieAction = (Action)Delegate.Remove(DieAction, action);
	}

	public void SetInvulnerable(bool invulnerable)
	{
		isInvulnerable = invulnerable;
	}

	private void CheckDeathAchievements(Unit damager)
	{
		BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		if (currentGameMode is MainMenuGameMode || currentGameMode is OnlineMultiplayerGameMode || currentGameMode is LocalMultiplayerGameMode)
		{
			return;
		}
		CameraAbilityPossess componentInParent = MainCam.instance.GetComponentInParent<CameraAbilityPossess>();
		if (!(componentInParent == null) && !(damager == null))
		{
			AchievementService service = ServiceLocator.GetService<AchievementService>();
			int iD = unit.unitBlueprint.Entity.GUID.m_ID;
			int iD2 = damager.unitBlueprint.Entity.GUID.m_ID;
			bool flag = componentInParent.currentUnit != null && damager == componentInParent.currentUnit;
			int num = 1725768104;
			int num2 = 1782150434;
			if ((iD == num && iD2 == num2) || (iD == num2 && iD2 == num))
			{
				service.UnlockAchievement("KILL_GODS");
			}
			int num3 = 1186604180;
			int num4 = 164120656;
			bool flag2 = componentInParent.currentUnit != null && componentInParent.currentUnit.unitBlueprint.Entity.GUID.m_ID == num3;
			if (iD == num4 && iD2 == num3 && flag2 && flag)
			{
				service.AdvanceAchievementProgress("KNIGHT_KILL_VAMPIRE", 1);
			}
			int num5 = 2109442664;
			bool flag3 = componentInParent.currentUnit != null && componentInParent.currentUnit.unitBlueprint.Entity.GUID.m_ID == num5;
			if (iD2 == num5 && flag3 && flag)
			{
				service.AdvanceAchievementProgress("QUICKDRAW_KILL_UNIT", 1);
			}
			int num6 = 517591390;
			if (iD2 == num6)
			{
				service.AdvanceAchievementProgress("CLUBBER_KILL_UNIT", 1);
			}
			int num7 = 840974400;
			int num8 = 1380042766;
			if (iD == num8 && iD2 == num7)
			{
				service.AdvanceAchievementProgress("HWACHA_KILL_MAMMOTH", 1);
			}
			int num9 = 1594878680;
			if (iD2 == num9 && iD != num9)
			{
				service.AdvanceAchievementProgress("FLY_UNIT", 1);
			}
			if (iD == num8 && iD2 == num9)
			{
				service.UnlockAchievement("FLY_MAMMOTH");
			}
		}
	}

	public bool Die(bool wasKilledRemotely)
	{
		WasKilledRemotely = wasKilledRemotely;
		return Die();
	}
}
