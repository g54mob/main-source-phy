using System;
using System.Collections.Generic;
using HighlightingSystem;
using Landfall.TABS;
using Landfall.TABS.AI;
using Landfall.TABS.AI.Components;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Services;
using TFBGames;
using Unity.Entities;
using UnityEngine;

public class CameraAbilityPossess : GameStateListener, IPossessController
{
	public enum CameraMode
	{
		ThirdPerson = 0,
		FirstPerson = 1
	}

	public LayerMask m_MainRigLayer;

	public CameraMode cameraMode;

	private bool isPossessing;

	private Transform cam;

	private MouseLook mouseLook;

	private PossesionCamera possessionCamera;

	public Unit currentUnit;

	private CameraMovement cameraMove;

	private Rigidbody targetRig;

	private Vector3 thirdPersonOffset;

	private bool holdingPossess;

	private float sincePossess;

	private Quaternion lerpedDeathRotation;

	private Unit m_previouslyHighlightedUnit;

	[SerializeField]
	private Transform possessUIRoot;

	[SerializeField]
	private GameObject possessUIPrefab;

	private CameraAbilityPossessUI possessUI;

	private RectTransform possessUIRect;

	private TeamSystem teamSystem;

	private bool m_rotatingMouseLook;

	private InputService m_inputService;

	private LocalMultiplayerGameRules gameModeSettings;

	private ITimeService m_timeService;

	private INetworkService m_networkService;

	private Action m_OnPossessEnterAction;

	private Action m_OnPossessExitAction;

	private HighlightingRenderer m_highlighterRenderer;

	private PlayerCamera m_playerCamera;

	private BaseGameMode m_gameMode;

	private bool m_slowdownActive;

	public bool IsPossessing
	{
		get
		{
			return isPossessing;
		}
		private set
		{
			isPossessing = value;
			this.OnPossessUpdate?.Invoke(value);
		}
	}

	public event Action<bool> OnPossessUpdate;

	private new void Awake()
	{
		base.Awake();
		m_inputService = ServiceLocator.GetService<InputService>();
		gameModeSettings = ServiceLocator.GetService<LocalMultiplayerGameRules>();
		m_playerCamera = GetComponent<PlayerCamera>();
		mouseLook = GetComponent<MouseLook>();
		cameraMove = GetComponent<CameraMovement>();
		Camera componentInChildren = GetComponentInChildren<Camera>();
		m_highlighterRenderer = componentInChildren.GetComponent<HighlightingRenderer>();
		cam = componentInChildren.transform;
		m_gameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		teamSystem = World.Active.GetExistingManager<TeamSystem>();
		GameObject gameObject = UnityEngine.Object.Instantiate(possessUIPrefab, possessUIRoot.position, possessUIRoot.rotation, possessUIRoot);
		possessUI = gameObject.GetComponent<CameraAbilityPossessUI>();
		possessUIRect = possessUI.GetComponent<RectTransform>();
		m_timeService = ServiceLocator.GetService<ITimeService>();
		m_networkService = ServiceLocator.GetService<INetworkService>();
		if (ServiceLocator.GetService<GameModeService>().CurrentGameMode is LocalMultiplayerGameMode localMultiplayerGameMode)
		{
			localMultiplayerGameMode.AddPossessController(this);
		}
	}

	private Unit FindUnitToEnter()
	{
		if (!targetRig)
		{
			return null;
		}
		Unit componentInParent = targetRig.GetComponentInParent<Unit>();
		if ((bool)componentInParent)
		{
			return componentInParent;
		}
		return null;
	}

	public void AddOnPossessEnterAction(Action a)
	{
		m_OnPossessEnterAction = (Action)Delegate.Combine(m_OnPossessEnterAction, a);
	}

	public void AddOnPossessExitAction(Action a)
	{
		m_OnPossessExitAction = (Action)Delegate.Combine(m_OnPossessExitAction, a);
	}

	public void EnterUnit(Unit unitAPI)
	{
		ServiceLocator.GetService<GameModeService>().CurrentGameMode.OnEnterPossession();
		currentUnit = unitAPI;
		possessionCamera = unitAPI.GetComponentInChildren<PossesionCamera>();
		if (!possessionCamera)
		{
			currentUnit = null;
			return;
		}
		Balance component = currentUnit.GetComponent<Balance>();
		if ((bool)component)
		{
			component.allowedFootDistance *= 2f;
			component.allowedLegAngle *= 2f;
			component.allowedTorsoAngle *= 2f;
		}
		unitAPI.GetComponent<UnitAPI>().SetActive(active: false, true);
		unitAPI.GetComponentInChildren<GeneralInput>().hasControl = true;
		Transform transform = unitAPI.data.mainRig.transform;
		m_rotatingMouseLook = true;
		mouseLook.RotateTowards(0f, transform.eulerAngles.y, delegate
		{
			m_rotatingMouseLook = false;
		});
		possessionCamera.Enter();
		IsPossessing = true;
		holdingPossess = false;
		cameraMove.enabled = false;
		cameraMove.Velocity = Vector3.zero;
		SetEnterUI(unitAPI);
		if (cameraMode == CameraMode.FirstPerson)
		{
			EnterFPS();
		}
		RigidbodyHolder componentInChildren = currentUnit.GetComponentInChildren<RigidbodyHolder>();
		if ((bool)componentInChildren)
		{
			componentInChildren.forceInterpolation = true;
		}
		WeaponHandler componentInChildren2 = currentUnit.GetComponentInChildren<WeaponHandler>();
		if ((bool)componentInChildren2)
		{
			if ((bool)componentInChildren2.leftWeapon)
			{
				componentInChildren2.leftWeapon.randomCooldown = false;
			}
			if ((bool)componentInChildren2.rightWeapon)
			{
				componentInChildren2.rightWeapon.randomCooldown = false;
			}
		}
		_ = (bool)currentUnit.GetComponentInChildren<ConditionalEvent>();
		m_OnPossessEnterAction?.Invoke();
		unitAPI.GetComponentInChildren<GeneralInput>().SetPlayerActions(m_playerCamera.Actions);
		possessUI.SetPlayerActions(m_playerCamera.Actions);
	}

	private void SetEnterUI(Unit unit)
	{
		if (!currentUnit)
		{
			return;
		}
		CameraAbilityPossessUI.WeaponCooldownType leftWeapon = CameraAbilityPossessUI.WeaponCooldownType.None;
		CameraAbilityPossessUI.WeaponCooldownType rightWeapon = CameraAbilityPossessUI.WeaponCooldownType.None;
		CameraAbilityPossessUI.WeaponCooldownType combatMove = CameraAbilityPossessUI.WeaponCooldownType.None;
		WeaponHandler componentInChildren = currentUnit.GetComponentInChildren<WeaponHandler>();
		if ((bool)componentInChildren)
		{
			HandleWeaponChangeSubscription(componentInChildren);
			if ((bool)componentInChildren.rightWeapon && componentInChildren.rightWeapon.internalCooldown < 100f && !componentInChildren.leftWeapon)
			{
				rightWeapon = CameraAbilityPossessUI.WeaponCooldownType.Active;
			}
			else
			{
				if ((bool)componentInChildren.leftWeapon && componentInChildren.leftWeapon.internalCooldown < 100f)
				{
					leftWeapon = CameraAbilityPossessUI.WeaponCooldownType.Active;
				}
				if ((bool)componentInChildren.rightWeapon && componentInChildren.rightWeapon.internalCooldown < 100f)
				{
					rightWeapon = CameraAbilityPossessUI.WeaponCooldownType.Active;
				}
			}
		}
		ConditionalEvent componentInChildren2 = unit.GetComponentInChildren<ConditionalEvent>();
		if ((bool)componentInChildren2 && componentInChildren2.controllableByPlayer)
		{
			componentInChildren2.extraRange = 2f;
			for (int i = 0; i < componentInChildren2.events.Length; i++)
			{
				combatMove = CameraAbilityPossessUI.WeaponCooldownType.Active;
				for (int j = 0; j < componentInChildren2.events[i].conditions.Length; j++)
				{
					if (componentInChildren2.events[i].conditions[j].conditionType == EventCondition.ConditionType.Cooldown)
					{
						combatMove = CameraAbilityPossessUI.WeaponCooldownType.Active;
					}
				}
			}
		}
		possessUI.StartPossess(rightWeapon, leftWeapon, combatMove);
	}

	private void HandleWeaponChangeSubscription(WeaponHandler weaponHandler)
	{
		weaponHandler.ChangeWeapons -= SetEnterUI;
		weaponHandler.ChangeWeapons += SetEnterUI;
	}

	private void UpdateUnitUI()
	{
		float leftCounter = 0f;
		float leftMax = 0f;
		float rightCounter = 0f;
		float rightMax = 0f;
		float moveCounter = 0f;
		float num = 0f;
		bool attackLeftIsActive = true;
		bool attackRightIsActive = true;
		if ((bool)currentUnit.data)
		{
			possessUI.UpdateHealth(currentUnit.data.health / currentUnit.data.maxHealth);
		}
		WeaponHandler componentInChildren = currentUnit.GetComponentInChildren<WeaponHandler>();
		if ((bool)componentInChildren)
		{
			if ((bool)componentInChildren.rightWeapon && !componentInChildren.leftWeapon)
			{
				rightCounter = componentInChildren.rightWeapon.internalCounter;
				rightMax = componentInChildren.rightWeapon.internalCooldown;
				if (componentInChildren.rightWeapon.onlyCountInRange && currentUnit.data.distanceToTarget > componentInChildren.rightWeapon.maxRange)
				{
					attackRightIsActive = false;
				}
			}
			else
			{
				if ((bool)componentInChildren.leftWeapon)
				{
					leftCounter = componentInChildren.leftWeapon.internalCounter;
					leftMax = componentInChildren.leftWeapon.internalCooldown;
					if (componentInChildren.leftWeapon.onlyCountInRange && currentUnit.data.distanceToTarget > componentInChildren.leftWeapon.maxRange)
					{
						attackLeftIsActive = false;
					}
				}
				if ((bool)componentInChildren.rightWeapon)
				{
					rightCounter = componentInChildren.rightWeapon.internalCounter;
					rightMax = componentInChildren.rightWeapon.internalCooldown;
					if (componentInChildren.rightWeapon.onlyCountInRange && currentUnit.data.distanceToTarget > componentInChildren.rightWeapon.maxRange)
					{
						attackRightIsActive = false;
					}
				}
			}
		}
		bool moveIsInRange = true;
		ConditionalEvent componentInChildren2 = currentUnit.GetComponentInChildren<ConditionalEvent>();
		if ((bool)componentInChildren2)
		{
			for (int i = 0; i < componentInChildren2.events.Length; i++)
			{
				for (int j = 0; j < componentInChildren2.events[i].conditions.Length; j++)
				{
					if (componentInChildren2.events[i].conditions[j].conditionType == EventCondition.ConditionType.Cooldown)
					{
						num = componentInChildren2.events[i].conditions[j].value;
						moveCounter = componentInChildren2.events[i].conditions[j].counter;
					}
				}
				if (!componentInChildren2.CheckConditions(componentInChildren2.events[i], EventCondition.ConditionType.None, justCheck: true))
				{
					moveIsInRange = false;
				}
			}
			if (num == 0f)
			{
				moveCounter = 1f;
				num = 1f;
			}
		}
		if (!targetRig)
		{
			moveIsInRange = false;
		}
		possessUI.UpdateCooldowns(leftCounter, leftMax, rightCounter, rightMax, moveCounter, num, moveIsInRange, attackLeftIsActive, attackRightIsActive);
	}

	public void ExitUnit()
	{
		ServiceLocator.GetService<GameModeService>().CurrentGameMode.OnLeavePossession();
		IsPossessing = false;
		if ((bool)currentUnit)
		{
			RigidbodyHolder componentInChildren = currentUnit.GetComponentInChildren<RigidbodyHolder>();
			if ((bool)componentInChildren)
			{
				componentInChildren.forceInterpolation = false;
			}
			Balance component = currentUnit.GetComponent<Balance>();
			if ((bool)component)
			{
				component.allowedFootDistance /= 2f;
				component.allowedLegAngle /= 2f;
				component.allowedTorsoAngle /= 2f;
			}
			currentUnit.GetComponent<UnitAPI>().SetActive(active: true, false);
			currentUnit.GetComponentInChildren<GeneralInput>().hasControl = false;
			WeaponHandler componentInChildren2 = currentUnit.GetComponentInChildren<WeaponHandler>();
			if ((bool)componentInChildren2)
			{
				componentInChildren2.ChangeWeapons -= SetEnterUI;
				if ((bool)componentInChildren2.leftWeapon)
				{
					componentInChildren2.leftWeapon.randomCooldown = true;
				}
				if ((bool)componentInChildren2.rightWeapon)
				{
					componentInChildren2.rightWeapon.randomCooldown = true;
				}
			}
			LeaveFPS();
			ConditionalEvent componentInChildren3 = currentUnit.GetComponentInChildren<ConditionalEvent>();
			if ((bool)componentInChildren3)
			{
				componentInChildren3.extraRange = 2f;
			}
		}
		if ((bool)possessionCamera)
		{
			possessionCamera.Leave();
		}
		possessionCamera = null;
		currentUnit = null;
		cameraMove.enabled = true;
		cameraMove.Velocity = Vector3.zero;
		GetComponent<CameraMovementCollision>().enabled = true;
		possessUI.EndPossess();
		m_OnPossessExitAction?.Invoke();
	}

	private Rigidbody FindTargetRig(float maxAngle = 180f)
	{
		float num = float.PositiveInfinity;
		targetRig = null;
		float distanceToTarget = 0f;
		List<Unit> list = null;
		GameModeState currentGameModeState = CampaignPlayerDataHolder.CurrentGameModeState;
		if (currentGameModeState == GameModeState.Campaign && !isPossessing)
		{
			list = teamSystem.GetTeamUnits(Landfall.TABS.Team.Red);
		}
		else if (currentGameModeState == GameModeState.LocalMultiplayer && !isPossessing)
		{
			switch (m_playerCamera.CurrentPlayer)
			{
			case TFBGames.Player.One:
				list = teamSystem.GetTeamUnits(Landfall.TABS.Team.Red);
				break;
			case TFBGames.Player.Two:
				list = teamSystem.GetTeamUnits(Landfall.TABS.Team.Blue);
				break;
			default:
				list = teamSystem.GetAllUnits();
				break;
			}
		}
		else if (currentGameModeState == GameModeState.OnlineMultiplayer && !isPossessing)
		{
			if (m_networkService == null)
			{
				return null;
			}
			list = teamSystem.GetTeamUnits(m_networkService.PlayerTeam);
		}
		else
		{
			list = teamSystem.GetAllUnits();
		}
		if (list == null || list.Count == 0)
		{
			return null;
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (!list[i].data.mainRig)
			{
				return null;
			}
			if ((bool)currentUnit && currentUnit.transform.root == list[i].transform.root)
			{
				continue;
			}
			float num2 = Vector3.Distance(cam.position, list[i].data.mainRig.position);
			float num3 = Vector3.Angle(cam.forward, list[i].data.mainRig.position - cam.position);
			if (!(num3 > maxAngle))
			{
				float num4 = num3 + num2 * 0.1f;
				if ((bool)currentUnit && currentUnit.Team == list[i].Team)
				{
					num4 = ((!currentUnit.targetYourFriends) ? (num4 * 10f) : (num4 / 10f));
				}
				if (num4 < num && (IsPossessing || !holdingPossess || (bool)list[i].GetComponentInChildren<PossesionCamera>()))
				{
					targetRig = list[i].data.mainRig;
					num = num4;
					distanceToTarget = num2;
				}
			}
		}
		if (isPossessing && (bool)targetRig)
		{
			float magnitude = (targetRig.transform.position - currentUnit.PredicedPosition).magnitude;
			TargetData targetData = new TargetData
			{
				DistanceToTarget = distanceToTarget,
				TargetInAttackRange = ((magnitude <= currentUnit.m_AttackDistance) ? 1 : 0),
				TargetInPreferredRange = ((magnitude <= currentUnit.m_PreferedDistance) ? 1 : 0)
			};
			currentUnit.api.SetAttackTarget(targetRig.position, targetRig, targetRig.GetComponentInParent<DataHandler>(), targetData, canSeeTarget: true);
		}
		return targetRig;
	}

	private void HightlightTarget()
	{
		if ((bool)targetRig)
		{
			Unit component = targetRig.transform.root.GetComponent<Unit>();
			if (component != m_previouslyHighlightedUnit)
			{
				if (m_previouslyHighlightedUnit != null)
				{
					m_previouslyHighlightedUnit.RemoveHighlight();
				}
				component.SetHighlight(Color.white);
				m_previouslyHighlightedUnit = component;
			}
		}
		else if (m_previouslyHighlightedUnit != null)
		{
			m_previouslyHighlightedUnit.RemoveHighlight();
			m_previouslyHighlightedUnit = null;
		}
	}

	private void ToggleFPS()
	{
		if (cameraMode == CameraMode.ThirdPerson)
		{
			EnterFPS();
			cameraMode = CameraMode.FirstPerson;
		}
		else
		{
			LeaveFPS();
			cameraMode = CameraMode.ThirdPerson;
		}
	}

	private void LeaveFPS()
	{
		UnitRig component = currentUnit.GetComponent<UnitRig>();
		if ((bool)component)
		{
			component.SetGearVisability(UnitRig.GearType.HEAD, visability: true);
			component.SetGearVisability(UnitRig.GearType.SHOULDER, visability: true);
		}
		EyeSpawner component2 = currentUnit.GetComponent<EyeSpawner>();
		if ((bool)component2)
		{
			component2.SetEyesVisable(active: true);
		}
		GetComponent<CameraMovementCollision>().enabled = true;
	}

	private void EnterFPS()
	{
		GetComponent<CameraMovementCollision>().enabled = false;
		UnitRig component = currentUnit.GetComponent<UnitRig>();
		if ((bool)component)
		{
			component.SetGearVisability(UnitRig.GearType.HEAD, visability: false);
			component.SetGearVisability(UnitRig.GearType.SHOULDER, visability: false);
		}
		EyeSpawner component2 = currentUnit.GetComponent<EyeSpawner>();
		if ((bool)component2)
		{
			component2.SetEyesVisable(active: false);
		}
	}

	private void LateUpdate()
	{
		bool flag = m_gameMode.MatchOver || m_gameMode.CurtainController.AreCurtainsClosed;
		if (isPossessing && !currentUnit)
		{
			ExitUnit();
		}
		if ((isPossessing || holdingPossess) && !flag)
		{
			if (!possessUI.Dot.activeSelf)
			{
				SetUpPossessUIRect();
				possessUI.Dot.SetActive(value: true);
			}
		}
		else if (possessUI.Dot.activeSelf)
		{
			possessUI.Dot.SetActive(value: false);
		}
		if (flag && possessUI.UIActive)
		{
			possessUI.EndPossess();
		}
		bool flag2 = m_gameMode.TimeService.IsPaused();
		if (m_playerCamera.Actions.m_possessToggleCamera.WasPressed && (bool)currentUnit && isPossessing && !flag && !flag2)
		{
			ToggleFPS();
		}
		if (m_playerCamera.Actions.m_possessToggle.IsPressed && !flag && base.GameStateManager.GameState != GameState.PlacementState)
		{
			if (m_gameMode.CanUseSlowMoInCurrentGameMode())
			{
				if (!m_slowdownActive)
				{
					m_timeService.SetState(0.1f, 0.2f);
					m_slowdownActive = true;
				}
				m_timeService.SetState(0.1f, 0.2f);
				cameraMove.multiplier = 0.3f;
			}
		}
		else
		{
			if (m_slowdownActive)
			{
				m_timeService.SetState(1f, 0.2f);
				m_slowdownActive = false;
			}
			cameraMove.multiplier = 1f;
		}
		if (IsPossessing)
		{
			FindTargetRig(15f);
		}
		else if (holdingPossess)
		{
			FindTargetRig(45f);
		}
		if (m_highlighterRenderer != null && base.GameStateManager.GameState != GameState.PlacementState)
		{
			if (holdingPossess)
			{
				if (!m_highlighterRenderer.enabled)
				{
					m_highlighterRenderer.enabled = true;
				}
			}
			else if (m_highlighterRenderer.enabled)
			{
				m_highlighterRenderer.enabled = false;
			}
		}
		if (isPossessing)
		{
			if (m_previouslyHighlightedUnit != null)
			{
				m_previouslyHighlightedUnit.RemoveHighlight();
				m_previouslyHighlightedUnit = null;
			}
			sincePossess = 0f;
			if (!currentUnit)
			{
				holdingPossess = false;
				return;
			}
			UpdateUnitUI();
			if ((m_playerCamera.Actions.m_possessToggle.WasPressed || base.GameStateManager.GameState != GameState.BattleState) && m_inputService.CurrentState != InputService.InputState.UI && !flag2)
			{
				ExitUnit();
			}
			if ((bool)currentUnit && !m_timeService.IsPaused())
			{
				if (m_playerCamera.Actions.m_possessAttack3.WasPressed && (bool)targetRig)
				{
					ConditionalEvent[] componentsInChildren = currentUnit.GetComponentsInChildren<ConditionalEvent>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].controllableByPlayer)
						{
							componentsInChildren[i].CheckConditionsUpdate();
						}
					}
				}
				else if (!m_playerCamera.Actions.m_possessAttack3.IsPressed)
				{
					ConditionalEvent[] componentsInChildren2 = currentUnit.GetComponentsInChildren<ConditionalEvent>();
					for (int j = 0; j < componentsInChildren2.Length; j++)
					{
						if (componentsInChildren2[j].controllableByPlayer)
						{
							componentsInChildren2[j].CheckConditionsUpdate(forceFail: true);
						}
					}
				}
				WeaponHandler componentInChildren = currentUnit.GetComponentInChildren<WeaponHandler>();
				float num = 2f;
				if ((bool)componentInChildren && m_playerCamera.Actions.m_possessAttack2.WasPressed)
				{
					if ((bool)componentInChildren.rightWeapon)
					{
						num = componentInChildren.rightWeapon.maxRange;
					}
					componentInChildren.Attack(possessionCamera.transform.position + cam.forward * num, targetRig, cam.transform.forward, WeaponHandler.ForceWeapon.Right);
				}
				if ((bool)componentInChildren && m_playerCamera.Actions.m_possessAttack1.WasPressed)
				{
					if ((bool)componentInChildren.leftWeapon)
					{
						num = componentInChildren.leftWeapon.maxRange;
					}
					componentInChildren.Attack(possessionCamera.transform.position + cam.forward * num, targetRig, cam.transform.forward, WeaponHandler.ForceWeapon.Left);
				}
				if (!m_rotatingMouseLook)
				{
					Vector3 forward = cam.transform.forward;
					forward.y = 0f;
					currentUnit.GetComponentInChildren<GeneralInput>().SetRotation(Quaternion.LookRotation(forward));
				}
			}
			if (!ShouldUseFixedUpdateForOnlineMultiplayer())
			{
				UpdateMovementAndRotation();
			}
		}
		if (isPossessing || flag || flag2)
		{
			return;
		}
		sincePossess += Time.unscaledDeltaTime;
		if (base.GameStateManager.GameState != GameState.BattleState)
		{
			return;
		}
		if (m_playerCamera.Actions.m_possessToggle.IsPressed && sincePossess > 0.4f)
		{
			holdingPossess = true;
		}
		if (holdingPossess)
		{
			HightlightTarget();
		}
		if (m_playerCamera.Actions.m_possessToggle.WasReleased && holdingPossess && m_inputService.CurrentState != InputService.InputState.UI && !flag2)
		{
			holdingPossess = false;
			Unit unit = FindUnitToEnter();
			if ((bool)unit)
			{
				EnterUnit(unit);
			}
		}
	}

	private void FixedUpdate()
	{
		if (ShouldUseFixedUpdateForOnlineMultiplayer())
		{
			UpdateMovementAndRotation();
		}
	}

	private void OnDisable()
	{
		possessUI?.EndPossess();
	}

	private bool ShouldUseFixedUpdateForOnlineMultiplayer()
	{
		if (false)
		{
			return IsInOnlineMultiplayerGame();
		}
		return false;
	}

	private bool IsInOnlineMultiplayerGame()
	{
		return CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer;
	}

	private void UpdateMovementAndRotation()
	{
		bool flag = m_gameMode.MatchOver || m_gameMode.CurtainController.AreCurtainsClosed;
		if (!isPossessing || flag || possessionCamera == null)
		{
			return;
		}
		if (currentUnit.data.Dead)
		{
			if (cameraMode == CameraMode.FirstPerson)
			{
				lerpedDeathRotation = Quaternion.Lerp(lerpedDeathRotation, currentUnit.data.torso.rotation, Time.deltaTime * 10f);
				mouseLook.transform.rotation = lerpedDeathRotation;
			}
		}
		else
		{
			lerpedDeathRotation = mouseLook.transform.rotation;
		}
		if (cameraMode == CameraMode.FirstPerson)
		{
			base.transform.position = possessionCamera.transform.position;
			return;
		}
		if (!currentUnit.data.Dead)
		{
			thirdPersonOffset = currentUnit.transform.root.localScale.x * (base.transform.forward * possessionCamera.thirdPersonOffset.z + Vector3.up * possessionCamera.thirdPersonOffset.y + currentUnit.data.characterForwardObject.right * possessionCamera.thirdPersonOffset.x);
		}
		base.transform.position = Vector3.Lerp(base.transform.position, currentUnit.data.mainRig.transform.position + thirdPersonOffset, Mathf.Clamp(Time.deltaTime, 0f, 0.03f) * 20f);
	}

	private void SetUpPossessUIRect()
	{
		if (CampaignPlayerDataHolder.CurrentGameModeState != GameModeState.LocalMultiplayer)
		{
			return;
		}
		switch (m_playerCamera.CurrentPlayer)
		{
		case TFBGames.Player.One:
			if (gameModeSettings.SplitScreenStyle == SplitScreenStyle.Horizontal)
			{
				possessUIRect.anchorMax = new Vector2(1f, 0.5f);
				possessUIRect.anchorMin = new Vector2(0f, 0f);
			}
			else if (gameModeSettings.SplitScreenStyle == SplitScreenStyle.Vertical)
			{
				possessUIRect.anchorMax = new Vector2(0.5f, 1f);
				possessUIRect.anchorMin = new Vector2(0f, 0f);
			}
			break;
		case TFBGames.Player.Two:
			if (gameModeSettings.SplitScreenStyle == SplitScreenStyle.Horizontal)
			{
				possessUIRect.anchorMax = new Vector2(1f, 1f);
				possessUIRect.anchorMin = new Vector2(0f, 0.5f);
			}
			else if (gameModeSettings.SplitScreenStyle == SplitScreenStyle.Vertical)
			{
				possessUIRect.anchorMax = new Vector2(1f, 1f);
				possessUIRect.anchorMin = new Vector2(0.5f, 0f);
			}
			break;
		}
	}

	public override void OnEnterPlacementState()
	{
		if (m_highlighterRenderer != null)
		{
			m_highlighterRenderer.enabled = true;
		}
		if (base.enabled)
		{
			holdingPossess = false;
			if (possessUI?.Dot != null)
			{
				possessUI.Dot.SetActive(value: false);
			}
			if ((bool)currentUnit)
			{
				ExitUnit();
			}
		}
	}

	public override void OnEnterBattleState()
	{
		if (m_highlighterRenderer != null)
		{
			m_highlighterRenderer.enabled = false;
		}
		if (base.enabled && (bool)currentUnit)
		{
			ExitUnit();
		}
	}
}
