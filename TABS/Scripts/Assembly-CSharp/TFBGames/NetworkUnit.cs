using System;
using System.Collections.Generic;
using DM;
using Landfall.TABS;
using Landfall.TABS.AI.Components;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.WinConditions;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class NetworkUnit : EntityEventListener<IUnitState>
	{
		private enum State
		{
			Initializing = 0,
			WaitingForBattleScene = 1,
			LinkingToLocalUnit = 2,
			WaitingForMount = 3,
			WaitingToSpawnLocalCopy = 4,
			LinkingToPooledUnit = 5,
			DoneWithError = 6,
			Done = 7
		}

		private class ConditionalEventInfo
		{
			public ConditionalEvent ConditionalEvent;

			public readonly Dictionary<int, int> DoMovesAndContinuousFrame = new Dictionary<int, int>();

			public ConditionalEventInfo(ConditionalEvent conditionalEvent)
			{
				ConditionalEvent = conditionalEvent;
			}
		}

		public delegate void ReceivedSpecialAttackEventHandler(NetworkUnit networkUnit, UnitSpecialAttackEvent attackEvent);

		private float RepeatedEventIntervals = 0.5f;

		private const int InitializeConditionalsDelay = 2;

		private State m_state;

		private GameStateManager m_gameStateManager;

		private INetworkUnitsManager m_networkUnits;

		private UnitSpawnToken m_spawnToken;

		private NetworkBattleController m_battleController;

		private RuntimeReferenceService m_runtimeReference;

		private readonly Dictionary<int, ConditionalEventInfo> m_conditionalEvents = new Dictionary<int, ConditionalEventInfo>();

		private readonly List<int> m_conditionalEventToSet = new List<int>();

		private int m_initializeConditionalsDelay;

		private Dictionary<Type, float> m_repeatEventsTime = new Dictionary<Type, float>();

		private int m_linkToUnitInstanceId;

		private Vector3? m_targetLookDirection;

		private bool m_shouldSyncTransforms;

		private bool m_didSyncTransforms;

		private NetworkBattleController BattleController
		{
			get
			{
				if (m_battleController == null)
				{
					m_battleController = ServiceLocator.GetService<NetworkBattleController>();
				}
				return m_battleController;
			}
		}

		public Unit Unit { get; private set; }

		public event Action<NetworkUnit> InitializedUnit;

		public event Action<NetworkUnit> NetworkUnitDetached;

		public event ReceivedSpecialAttackEventHandler ReceivedSpecialAttack;

		private void FixedUpdate()
		{
			switch (m_state)
			{
			case State.WaitingForBattleScene:
				UpdateWaitingForBattleScene();
				break;
			case State.WaitingForMount:
				UpdateWaitingForMount();
				break;
			case State.WaitingToSpawnLocalCopy:
				UpdateWaitingToSpawnLocalCopy();
				break;
			case State.LinkingToPooledUnit:
				UpdateLinkingToPooledUnit();
				break;
			case State.Done:
				UpdateDone();
				break;
			case State.LinkingToLocalUnit:
			case State.DoneWithError:
				break;
			}
		}

		public override void Attached()
		{
			base.Attached();
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
			m_runtimeReference = ServiceLocator.GetService<RuntimeReferenceService>();
			m_spawnToken = (UnitSpawnToken)base.entity.AttachToken;
			if (m_spawnToken == null)
			{
				Debug.LogError("Spawn token is null.");
				SetState(State.DoneWithError);
				return;
			}
			base.state.AddCallback("MovementSpeed", OnMovementSpeedChanged);
			base.state.AddCallback("TargetShortNetworkId", OnTargetShortNetworkIdChanged);
			base.state.AddCallback("LookDirectionAngle", OnLookDirectionAngleChanged);
			if (!IsInBattleScene())
			{
				SetState(State.WaitingForBattleScene);
			}
			else
			{
				OnEnteredBattleScene();
			}
		}

		public override void Detached()
		{
			base.Detached();
			RemoveUnitCallbacks();
			foreach (KeyValuePair<int, ConditionalEventInfo> conditionalEvent in m_conditionalEvents)
			{
				conditionalEvent.Value.DoMovesAndContinuousFrame.Clear();
			}
			this.NetworkUnitDetached?.Invoke(this);
		}

		public void Initialize(Unit unit, bool isRemotelyControlled)
		{
			Unit = unit;
			unit.NetworkId = base.entity.NetworkId.PackedValue;
			unit.SmallNetworkId = m_spawnToken.SmallNetworkId;
			SetIsRemotelyControlled(isRemotelyControlled);
			m_initializeConditionalsDelay = 2;
			if (!base.entity.IsOwner)
			{
				unit.RemoteInstanceId = m_spawnToken.InstanceId;
				if (m_spawnToken.IsMounted && m_linkToUnitInstanceId == 0)
				{
					Unit unitBySmallNetworkId = m_networkUnits.GetUnitBySmallNetworkId(m_spawnToken.MountUnitSmallNetworkId);
					if (unitBySmallNetworkId != null)
					{
						unit.gameObject.AddComponent<Mount>().EnterMount(null, unitBySmallNetworkId, m_spawnToken.MountSitId);
						unit.IsRider = true;
					}
				}
				if (m_spawnToken.SpawnSource == 1)
				{
					Unit unitBySmallNetworkId2 = m_networkUnits.GetUnitBySmallNetworkId(m_spawnToken.CopyOfUnitSmallNetworkId);
					if (unitBySmallNetworkId2 != null)
					{
						MeleeWeaponCopySelf componentInChildren = unitBySmallNetworkId2.GetComponentInChildren<MeleeWeaponCopySelf>();
						if (componentInChildren != null)
						{
							componentInChildren.OnSpawnedUnit(unitBySmallNetworkId2, unit.gameObject, m_spawnToken.CopyOfUnitSpawnPosition);
						}
					}
				}
				m_networkUnits.NonOwnerSendUnitIdsEvent(unit.InstanceId, unit.RemoteInstanceId);
			}
			Unit.EnableSyncTransformsChanged += OnUnitEnableSyncTransformsChanged;
			if (unit.WeaponHandler != null)
			{
				unit.WeaponHandler.AttackStarted += OnAttackStarted;
			}
			if (unit.api != null)
			{
				unit.api.MovementSpeedSet += OnSetMovementSpeed;
				unit.api.AttackTargetSet += OnAttackTargetSet;
				unit.api.LookDirectionSet += OnLookDirectionSet;
			}
			if (unit.data != null && BoltNetwork.IsClient && unit.data.setMainRigKinematic)
			{
				unit.data.mainRig.isKinematic = true;
			}
			SetState(State.Done);
			this.InitializedUnit?.Invoke(this);
		}

		public void OnUnitDied()
		{
			SyncTransforms(sync: false);
			if (Unit.data != null && BoltNetwork.IsClient && Unit.data.mainRig.isKinematic)
			{
				Unit.data.mainRig.isKinematic = false;
			}
			if (base.entity.IsOwner && !base.entity.IsFrozen)
			{
				base.entity.Freeze(pause: true);
			}
			RemoveUnitCallbacks();
		}

		public void OnEnterPossession()
		{
			if (BoltNetwork.IsClient)
			{
				SetIsRemotelyControlled(isRemotelyControlled: false);
			}
			else if (BoltNetwork.IsServer)
			{
				SetIsRemotelyControlled(isRemotelyControlled: true);
			}
		}

		public void OnExitPossession()
		{
			if (BoltNetwork.IsClient)
			{
				SetIsRemotelyControlled(isRemotelyControlled: true);
			}
			else if (BoltNetwork.IsServer)
			{
				SetIsRemotelyControlled(isRemotelyControlled: false);
			}
		}

		public void SendSpecialAttackEvent(NetworkUnitSpecialAttackType attackType, IProtocolToken attackToken)
		{
			if (!(base.entity == null) && base.entity.IsAttached && !base.entity.IsFrozen && !(Unit == null) && !Unit.IsRemotelyControlled)
			{
				if (attackType == NetworkUnitSpecialAttackType.AddExplosionEffectToChild || attackType == NetworkUnitSpecialAttackType.SyncProjectileEffect)
				{
					RepeatedEventIntervals = 0f;
				}
				else
				{
					RepeatedEventIntervals = 0.5f;
				}
				if (CanSendRepeatedEvent(typeof(UnitSpecialAttackEvent)))
				{
					UnitSpecialAttackEvent unitSpecialAttackEvent = UnitSpecialAttackEvent.Create(base.entity);
					unitSpecialAttackEvent.AttackType = (int)attackType;
					unitSpecialAttackEvent.AttackToken = attackToken;
					unitSpecialAttackEvent.Send();
				}
			}
		}

		public override void OnEvent(UnitAttackEvent attackEvent)
		{
			base.OnEvent(attackEvent);
			if (!(Unit == null) && !attackEvent.FromSelf && Unit.IsRemotelyControlled && !(Unit.WeaponHandler == null))
			{
				Unit unitBySmallNetworkId = m_networkUnits.GetUnitBySmallNetworkId((ushort)attackEvent.TargetUnitSmallNetworkId);
				Rigidbody targetRig = ((unitBySmallNetworkId != null && unitBySmallNetworkId.data != null) ? unitBySmallNetworkId.data.mainRig : null);
				Unit.WeaponHandler.Attack(attackEvent.Position, targetRig, attackEvent.ForceDirection, (WeaponHandler.ForceWeapon)attackEvent.ForceWeapon);
			}
		}

		public override void OnEvent(UnitSpecialAttackEvent attackEvent)
		{
			base.OnEvent(attackEvent);
			if (!(Unit == null) && !attackEvent.FromSelf && Unit.IsRemotelyControlled)
			{
				this.ReceivedSpecialAttack?.Invoke(this, attackEvent);
			}
		}

		public override void OnEvent(UnitTurnOnConditionalEvent turnOnEvent)
		{
			base.OnEvent(turnOnEvent);
			if (!(Unit == null) && !turnOnEvent.FromSelf && Unit.IsRemotelyControlled && m_conditionalEvents != null && m_conditionalEvents.Count > 0)
			{
				int conditionalEventId = turnOnEvent.ConditionalEventId;
				if (m_conditionalEvents.TryGetValue(conditionalEventId, out var value))
				{
					int instanceEventId = turnOnEvent.InstanceEventId;
					value.ConditionalEvent.TurnOnEvent(instanceEventId);
					value.ConditionalEvent.DoMovesAndContinuousEvent(instanceEventId);
					value.DoMovesAndContinuousFrame[instanceEventId] = Time.frameCount;
				}
			}
		}

		public override void OnEvent(UnitTurnOffConditionalEvent turnOffEvent)
		{
			base.OnEvent(turnOffEvent);
			if (!(Unit == null) && !turnOffEvent.FromSelf && Unit.IsRemotelyControlled && m_conditionalEvents != null && m_conditionalEvents.Count > 0)
			{
				int conditionalEventId = turnOffEvent.ConditionalEventId;
				if (m_conditionalEvents.TryGetValue(conditionalEventId, out var value))
				{
					int instanceEventId = turnOffEvent.InstanceEventId;
					value.ConditionalEvent.TurnOffEvent(instanceEventId);
					value.DoMovesAndContinuousFrame.Remove(instanceEventId);
				}
			}
		}

		private void SetState(State newState)
		{
			m_state = newState;
			switch (newState)
			{
			case State.LinkingToLocalUnit:
				LinkToLocalUnit();
				break;
			case State.WaitingToSpawnLocalCopy:
				SpawnLocalCopyOfRemoteUnit();
				break;
			}
		}

		private bool CanProcessChangedProperty()
		{
			if (Unit != null && Unit.api != null)
			{
				return Unit.IsRemotelyControlled;
			}
			return false;
		}

		private void OnMovementSpeedChanged()
		{
			if (CanProcessChangedProperty())
			{
				Unit.api.SetMovementSpeed(base.state.MovementSpeed, Unit.IsRemotelyControlled);
			}
		}

		private void OnTargetShortNetworkIdChanged()
		{
			if (CanProcessChangedProperty())
			{
				Unit unitBySmallNetworkId = m_networkUnits.GetUnitBySmallNetworkId((ushort)base.state.TargetShortNetworkId);
				if (!(unitBySmallNetworkId == null))
				{
					DataHandler data = unitBySmallNetworkId.data;
					Rigidbody rigidbody = ((data != null) ? data.mainRig : null);
					Vector3 vector = ((rigidbody != null) ? rigidbody.transform.position : unitBySmallNetworkId.transform.position);
					TargetData targetData = TargetData.Null;
					Unit.api.SetAttackTarget(vector, rigidbody, data, targetData, canSeeTarget: false, startAttack: false);
					UpdateTargetData();
				}
			}
		}

		private void OnLookDirectionAngleChanged()
		{
			if (CanProcessChangedProperty())
			{
				m_targetLookDirection = UnpackLookDirection(base.state.LookDirectionAngle);
			}
		}

		private void OnEnteredBattleScene()
		{
			m_networkUnits.OnNetworkUnitInBattleScene(this);
			if (!base.entity.IsOwner)
			{
				m_battleController = ServiceLocator.GetService<NetworkBattleController>();
				if (m_spawnToken.IsMounted)
				{
					SetState(State.WaitingForMount);
				}
				else if (m_spawnToken.LinkToUnitInstanceId != 0)
				{
					m_linkToUnitInstanceId = m_spawnToken.LinkToUnitInstanceId;
					SetState(State.LinkingToLocalUnit);
				}
				else if (m_spawnToken.IsInPool)
				{
					SetState(State.LinkingToPooledUnit);
				}
				else
				{
					SetState(State.WaitingToSpawnLocalCopy);
				}
			}
		}

		private void RemoveUnitCallbacks()
		{
			if (m_conditionalEvents != null && m_conditionalEvents.Count > 0)
			{
				foreach (KeyValuePair<int, ConditionalEventInfo> conditionalEvent in m_conditionalEvents)
				{
					ConditionalEventInfo value = conditionalEvent.Value;
					if (value != null && value.ConditionalEvent != null)
					{
						value.ConditionalEvent.TurnedConditionalEventOn -= OnTurnedConditionalEventOn;
						value.ConditionalEvent.TurnedConditionalEventOff -= OnTurnedConditionalEventOff;
					}
				}
			}
			if (!(Unit == null))
			{
				Unit.EnableSyncTransformsChanged -= OnUnitEnableSyncTransformsChanged;
				if (Unit.WeaponHandler != null)
				{
					Unit.WeaponHandler.AttackStarted -= OnAttackStarted;
				}
				if (Unit.api != null)
				{
					Unit.api.MovementSpeedSet -= OnSetMovementSpeed;
					Unit.api.AttackTargetSet -= OnAttackTargetSet;
					Unit.api.LookDirectionSet -= OnLookDirectionSet;
				}
			}
		}

		private void SetIsRemotelyControlled(bool isRemotelyControlled)
		{
			if (Unit == null)
			{
				return;
			}
			SyncTransforms(!Unit.api.IsPossessed);
			IRemotelyControllable[] componentsInChildren = Unit.GetComponentsInChildren<IRemotelyControllable>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].SetIsRemotelyControlled(isRemotelyControlled);
			}
			WeaponHandler weaponHandler = Unit.WeaponHandler;
			if (!(weaponHandler == null))
			{
				if (weaponHandler.leftWeapon != null)
				{
					weaponHandler.leftWeapon.randomCooldown = !isRemotelyControlled;
				}
				if (weaponHandler.rightWeapon != null)
				{
					weaponHandler.rightWeapon.randomCooldown = !isRemotelyControlled;
				}
			}
		}

		private void SyncTransforms(bool sync)
		{
			m_shouldSyncTransforms = sync;
			bool flag = Unit != null;
			if (flag && !Unit.EnableSyncTransforms)
			{
				sync = false;
			}
			m_didSyncTransforms = sync;
			Transform simulate = ((sync && flag) ? Unit.GetNetworkSyncedTransform() : null);
			base.state.SetTransforms(base.state.MainTransform, simulate);
		}

		private void OnUnitEnableSyncTransformsChanged(Unit unit, bool enableSyncTransforms)
		{
			if (m_shouldSyncTransforms == enableSyncTransforms && m_shouldSyncTransforms != m_didSyncTransforms)
			{
				SyncTransforms(enableSyncTransforms);
			}
		}

		private void UpdateWaitingForBattleScene()
		{
			if (IsInBattleScene())
			{
				OnEnteredBattleScene();
			}
		}

		private void UpdateWaitingForMount()
		{
			Unit unitBySmallNetworkId = m_networkUnits.GetUnitBySmallNetworkId(m_spawnToken.MountUnitSmallNetworkId);
			if (unitBySmallNetworkId == null)
			{
				return;
			}
			if (m_spawnToken.LinkToUnitInstanceId != 0)
			{
				m_linkToUnitInstanceId = m_spawnToken.LinkToUnitInstanceId;
				SetState(State.LinkingToLocalUnit);
				return;
			}
			if (!m_spawnToken.IsRiderWithLinkedMount)
			{
				SetState(State.WaitingToSpawnLocalCopy);
				return;
			}
			if (unitBySmallNetworkId.spawnedObjects != null && unitBySmallNetworkId.spawnedObjects.Length != 0)
			{
				int i = 0;
				for (int num = unitBySmallNetworkId.spawnedObjects.Length; i < num; i++)
				{
					GameObject gameObject = unitBySmallNetworkId.spawnedObjects[i];
					Unit unit = ((gameObject != null) ? gameObject.GetComponent<Unit>() : null);
					if (!(unit == null) && !(unit == unitBySmallNetworkId))
					{
						Mount component = unit.GetComponent<Mount>();
						if (component != null && component.SitId == m_spawnToken.MountSitId)
						{
							m_linkToUnitInstanceId = unit.InstanceId;
							SetState(State.LinkingToLocalUnit);
							return;
						}
					}
				}
			}
			Debug.LogError("Could not find rider to link to.");
			SetState(State.DoneWithError);
		}

		private void UpdateWaitingToSpawnLocalCopy()
		{
			if (IsReadyToSpawnLocalCopyOfRemoteUnit())
			{
				SpawnLocalCopyOfRemoteUnit();
			}
		}

		private void UpdateLinkingToPooledUnit()
		{
			bool hasError;
			Unit unitInPool = m_networkUnits.GetUnitInPool(m_spawnToken.UnitId, m_spawnToken.UnitModId, (Landfall.TABS.Team)m_spawnToken.Team, m_spawnToken.PoolInfo.Value, out hasError);
			if (hasError)
			{
				m_networkUnits.SendFailedToLinkPooledUnitEvent(m_spawnToken.InstanceId, m_spawnToken.PoolInfo.Value);
				SetState(State.DoneWithError);
			}
			else if (!(unitInPool == null))
			{
				bool isClient = BoltNetwork.IsClient;
				Initialize(unitInPool, isClient);
			}
		}

		private void UpdateDone()
		{
			if (m_initializeConditionalsDelay > 0)
			{
				m_initializeConditionalsDelay--;
				if (m_initializeConditionalsDelay <= 0)
				{
					InitializeConditionalEvents();
				}
			}
			UpdateRemotelyControlledUnit();
		}

		private void UpdateRemotelyControlledUnit()
		{
			if (!(Unit == null) && Unit.IsRemotelyControlled && !(Unit.api == null) && IsInBattle() && !(Unit.data == null) && !Unit.data.Dead)
			{
				UpdateLookDirection();
				if (!Unit.IsRider)
				{
					UpdateTargetData();
					UpdateConditionalEvents();
				}
			}
		}

		private void UpdateLookDirection()
		{
			if (Unit.unitBlueprint != null && Unit.unitBlueprint.projectMarsData.RecordLookDirection)
			{
				UpdateLookDirectionSynced();
			}
			else
			{
				UpdateLookDirectionCalculated();
			}
		}

		private void UpdateLookDirectionSynced()
		{
			if (m_targetLookDirection.HasValue && !(Unit == null) && !(Unit.data == null))
			{
				Vector3 value = m_targetLookDirection.Value;
				Vector3 lookDirectionIntent = Unit.data.lookDirectionIntent;
				Vector3 localDirection = Vector3.RotateTowards(lookDirectionIntent, value, 1.7453293f * Time.deltaTime, value.magnitude);
				if (Mathf.Approximately(lookDirectionIntent.x, localDirection.x) && Mathf.Approximately(lookDirectionIntent.y, localDirection.y) && Mathf.Approximately(lookDirectionIntent.z, localDirection.z))
				{
					m_targetLookDirection = null;
				}
				Unit.api.SetLookDirection(localDirection, forceSet: true);
			}
		}

		private void UpdateLookDirectionCalculated()
		{
			Vector3? mainTransformForwardVector = GetMainTransformForwardVector();
			if (mainTransformForwardVector.HasValue)
			{
				Unit.api.SetLookDirection(mainTransformForwardVector.Value, Unit.IsRemotelyControlled);
			}
		}

		private Vector3? GetMainTransformForwardVector()
		{
			Transform networkSyncedTransform = Unit.GetNetworkSyncedTransform();
			if (networkSyncedTransform == null)
			{
				return null;
			}
			Vector3 forward = networkSyncedTransform.forward;
			if (Mathf.Approximately(forward.x, 0f) && Mathf.Approximately(forward.z, 0f))
			{
				return null;
			}
			forward.y = 0f;
			forward.Normalize();
			return forward;
		}

		private void UpdateTargetData()
		{
			if (!(Unit.data.targetData == null) && !(Unit.data.mainRig == null) && !(Unit.data.targetData.mainRig == null))
			{
				Rigidbody mainRig = Unit.data.targetData.mainRig;
				float magnitude = (mainRig.transform.position - Unit.data.mainRig.transform.position).magnitude;
				TargetData targetData = new TargetData
				{
					DistanceToTarget = magnitude,
					TargetInAttackRange = ((magnitude <= Unit.m_AttackDistance) ? 1 : 0),
					TargetInPreferredRange = ((magnitude <= Unit.m_PreferedDistance) ? 1 : 0)
				};
				Unit.api.SetAttackTarget(mainRig.position, mainRig, Unit.data.targetData, targetData, canSeeTarget: false, startAttack: false);
			}
		}

		private void UpdateConditionalEvents()
		{
			int frameCount = Time.frameCount;
			foreach (KeyValuePair<int, ConditionalEventInfo> conditionalEvent2 in m_conditionalEvents)
			{
				ConditionalEventInfo value = conditionalEvent2.Value;
				ConditionalEvent conditionalEvent = value?.ConditionalEvent;
				if (conditionalEvent == null || value.DoMovesAndContinuousFrame == null || value.DoMovesAndContinuousFrame.Count <= 0)
				{
					continue;
				}
				m_conditionalEventToSet.Clear();
				foreach (KeyValuePair<int, int> item in value.DoMovesAndContinuousFrame)
				{
					if (item.Value != frameCount)
					{
						m_conditionalEventToSet.Add(item.Key);
					}
				}
				int i = 0;
				for (int count = m_conditionalEventToSet.Count; i < count; i++)
				{
					int num = m_conditionalEventToSet[i];
					conditionalEvent.DoMovesAndContinuousEvent(num);
					value.DoMovesAndContinuousFrame[num] = frameCount;
				}
			}
		}

		private void InitializeConditionalEvents()
		{
			if (Unit == null)
			{
				return;
			}
			ConditionalEvent[] array = ((Unit.unitBlueprint != null && Unit.unitBlueprint.projectMarsData.RecordConditionals) ? Unit.GetComponentsInChildren<ConditionalEvent>() : null);
			if (array == null || array.Length == 0)
			{
				return;
			}
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				if (array[i] != null)
				{
					ConditionalEvent conditionalEvent = array[i];
					conditionalEvent.NetworkId = i;
					m_conditionalEvents.Add(i, new ConditionalEventInfo(conditionalEvent));
					conditionalEvent.TurnedConditionalEventOn += OnTurnedConditionalEventOn;
					conditionalEvent.TurnedConditionalEventOff += OnTurnedConditionalEventOff;
				}
			}
		}

		private void LinkToLocalUnit()
		{
			Unit unitByInstanceId = m_networkUnits.GetUnitByInstanceId(m_linkToUnitInstanceId);
			if (unitByInstanceId == null)
			{
				Debug.LogError($"Could not find unit to link to: {m_linkToUnitInstanceId}");
				m_networkUnits.ClientSendFailedToLinkUnitEvent(m_spawnToken.InstanceId, (Landfall.TABS.Team)m_spawnToken.Team);
				SetState(State.DoneWithError);
			}
			else
			{
				bool isClient = BoltNetwork.IsClient;
				Initialize(unitByInstanceId, isClient);
			}
		}

		private bool IsInBattleScene()
		{
			if (m_networkUnits == null)
			{
				m_networkUnits = ServiceLocator.GetService<INetworkUnitsManager>();
				if (m_networkUnits == null)
				{
					return false;
				}
			}
			if (ServiceLocator.GetService<NetworkBattleController>() == null)
			{
				return false;
			}
			return true;
		}

		private bool IsInBattle()
		{
			if (m_gameStateManager != null)
			{
				return m_gameStateManager.GameState == GameState.BattleState;
			}
			return false;
		}

		private bool IsReadyToSpawnLocalCopyOfRemoteUnit()
		{
			if (BattleController == null)
			{
				return false;
			}
			bool flag = IsInBattle();
			if (!flag && (m_gameStateManager == null || m_gameStateManager.GameState != GameState.PlacementState))
			{
				return false;
			}
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (!flag && currentGameMode.EnterPlacementStateSequencer != null && currentGameMode.EnterPlacementStateSequencer.IsRunning)
			{
				return false;
			}
			bool num = BattleController.Phase == NetworkGamePhase.Placement && BattleController.RemotePhase == NetworkGamePhase.Placement;
			bool flag2 = BattleController.Phase == NetworkGamePhase.Battle && BattleController.RemotePhase == NetworkGamePhase.Battle;
			bool flag3 = BattleController.Phase == NetworkGamePhase.RequestBattleEnd || BattleController.RemotePhase == NetworkGamePhase.RequestBattleEnd;
			if (!(num || flag2 || flag3))
			{
				return false;
			}
			return true;
		}

		private void SpawnLocalCopyOfRemoteUnit()
		{
			if (base.entity.IsOwner || m_spawnToken == null || m_state != State.WaitingToSpawnLocalCopy || !IsReadyToSpawnLocalCopyOfRemoteUnit())
			{
				return;
			}
			bool flag = IsInBattle();
			BrushBehaviourBase brushBehaviourBase = null;
			if (!flag)
			{
				brushBehaviourBase = ServiceLocator.GetService<GameModeService>().CurrentGameMode.Brush?.BrushBehaviour;
				if (brushBehaviourBase == null)
				{
					Debug.LogError("brush is null");
					m_networkUnits.ClientSendFailedToSpawnUnitEvent(m_spawnToken.InstanceId);
					SetState(State.DoneWithError);
					return;
				}
			}
			UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(new DatabaseID(m_spawnToken.UnitModId, m_spawnToken.UnitId));
			if (unitBlueprint == null)
			{
				Debug.LogError("unit is null");
				m_networkUnits.ClientSendFailedToSpawnUnitEvent(m_spawnToken.InstanceId);
				SetState(State.DoneWithError);
				return;
			}
			bool isClient = BoltNetwork.IsClient;
			Transform transform = base.transform;
			Landfall.TABS.Team team = (Landfall.TABS.Team)m_spawnToken.Team;
			Unit unitToSpawn;
			if (flag)
			{
				unitBlueprint.Spawn(transform.position, transform.rotation, team, out unitToSpawn, 1f, isCampaignUnit: false, spawnRiders: false);
				if (unitToSpawn != null)
				{
					DisableUnitSpawnEffects(unitToSpawn);
					if (unitToSpawn.RuntimeReference == null)
					{
						unitToSpawn.RuntimeReference = m_runtimeReference.CreateReference(unitToSpawn);
					}
				}
			}
			else
			{
				unitToSpawn = brushBehaviourBase.Place(unitBlueprint, team, transform.position, transform.rotation, addToLayout: true, isCampaignUnit: false, null, forMartianPlayer: false, spawnRiders: false, isClient);
			}
			if (unitToSpawn == null)
			{
				Debug.LogErrorFormat("Failed to spawn local unit: unit is null");
				m_networkUnits.ClientSendFailedToSpawnUnitEvent(m_spawnToken.InstanceId);
				SetState(State.DoneWithError);
			}
			else
			{
				Initialize(unitToSpawn, isClient);
			}
		}

		private void DisableUnitSpawnEffects(Unit unit)
		{
			PlacementSpawnEffects componentInChildren = unit.GetComponentInChildren<PlacementSpawnEffects>();
			if (componentInChildren != null)
			{
				UnityEngine.Object.Destroy(componentInChildren.gameObject);
			}
		}

		private bool CanSendRepeatedEvent(Type eventType)
		{
			if (m_repeatEventsTime.ContainsKey(eventType) && m_repeatEventsTime[eventType] > Time.realtimeSinceStartup)
			{
				return false;
			}
			m_repeatEventsTime[eventType] = Time.realtimeSinceStartup + RepeatedEventIntervals;
			return true;
		}

		private void OnAttackStarted(Unit unit, Vector3 position, Rigidbody targetRigidbody, Vector3 forceDirection, WeaponHandler.ForceWeapon forceWeapon)
		{
			if (!(base.entity == null) && base.entity.IsAttached && !(unit == null) && !unit.IsRemotelyControlled && !base.entity.IsFrozen && CanSendRepeatedEvent(typeof(UnitAttackEvent)))
			{
				Unit unit2 = ((targetRigidbody != null) ? targetRigidbody.transform.root.GetComponent<Unit>() : null);
				int targetUnitSmallNetworkId = ((unit2 != null) ? unit2.SmallNetworkId : 0);
				UnitAttackEvent unitAttackEvent = UnitAttackEvent.Create(base.entity);
				unitAttackEvent.TargetUnitSmallNetworkId = targetUnitSmallNetworkId;
				unitAttackEvent.Position = position;
				unitAttackEvent.ForceDirection = forceDirection;
				unitAttackEvent.ForceWeapon = (int)forceWeapon;
				unitAttackEvent.Send();
			}
		}

		private bool CanSetStatePropertyToSendAcrossNetwork(Unit unit)
		{
			if (base.entity != null && base.entity.IsAttached && base.entity.IsOwner && unit != null && !unit.IsRemotelyControlled)
			{
				return !base.entity.IsFrozen;
			}
			return false;
		}

		private void OnSetMovementSpeed(Unit unit, float speed)
		{
			if (CanSetStatePropertyToSendAcrossNetwork(unit))
			{
				base.state.MovementSpeed = (int)speed;
			}
		}

		private void OnAttackTargetSet(Unit unit, Vector3 targetPosition, Rigidbody targetMainRigidbody, DataHandler targetDataHandler, TargetData targetData, bool canSeeTarget)
		{
			if (CanSetStatePropertyToSendAcrossNetwork(unit) && !(targetDataHandler == null) && !(targetDataHandler.unit == null) && !(unit.unitBlueprint == null) && unit.unitBlueprint.projectMarsData.RecordTarget)
			{
				base.state.TargetShortNetworkId = targetDataHandler.unit.SmallNetworkId;
			}
		}

		private void OnLookDirectionSet(Unit unit, Vector3 localDirection)
		{
			if (CanSetStatePropertyToSendAcrossNetwork(unit) && !(unit.unitBlueprint == null) && unit.unitBlueprint.projectMarsData.RecordLookDirection)
			{
				base.state.LookDirectionAngle = PackLookDirection(localDirection);
			}
		}

		private float PackLookDirection(Vector3 lookDirection)
		{
			lookDirection.y = 0f;
			return Vector3.SignedAngle(Vector3.forward, lookDirection, Vector3.up);
		}

		private Vector3 UnpackLookDirection(float angle)
		{
			return Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
		}

		private bool CanUnitSendConditionalEvents(ConditionalEvent conditionalEvent, ConditionalEventInstance eventInstance)
		{
			if (base.entity != null && base.entity.IsAttached && !base.entity.IsFrozen && Unit != null && !Unit.IsRemotelyControlled && conditionalEvent != null && eventInstance != null && m_conditionalEvents != null)
			{
				return m_conditionalEvents.Count > 0;
			}
			return false;
		}

		private void OnTurnedConditionalEventOn(ConditionalEvent conditionalEvent, ConditionalEventInstance eventInstance)
		{
			if (CanUnitSendConditionalEvents(conditionalEvent, eventInstance))
			{
				UnitTurnOnConditionalEvent unitTurnOnConditionalEvent = UnitTurnOnConditionalEvent.Create(base.entity);
				unitTurnOnConditionalEvent.ConditionalEventId = conditionalEvent.NetworkId;
				unitTurnOnConditionalEvent.InstanceEventId = eventInstance.NetworkId;
				unitTurnOnConditionalEvent.Send();
			}
		}

		private void OnTurnedConditionalEventOff(ConditionalEvent conditionalEvent, ConditionalEventInstance eventInstance)
		{
			if (CanUnitSendConditionalEvents(conditionalEvent, eventInstance))
			{
				UnitTurnOffConditionalEvent unitTurnOffConditionalEvent = UnitTurnOffConditionalEvent.Create(base.entity);
				unitTurnOffConditionalEvent.ConditionalEventId = conditionalEvent.NetworkId;
				unitTurnOffConditionalEvent.InstanceEventId = eventInstance.NetworkId;
				unitTurnOffConditionalEvent.Send();
			}
		}
	}
}
