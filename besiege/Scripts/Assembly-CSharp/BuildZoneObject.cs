using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

public class BuildZoneObject : GenericEntity
{
	public bool hasZone;

	public PlayerBuildZone buildZone;

	public Collider visCollider;

	public MeshRenderer visRenderer;

	public BuildZoneDialogTrigger dialogTrigger;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private ServerMachine machine;

	private Transform machineTransform;

	private Transform zoneTransform;

	private NetworkAddPiece addPiece;

	private MTeam zoneTeam;

	private MHealthType zoneHealthType;

	private Color initialZoneColor;

	private float lastDamage;

	private Renderer initialRenderer;

	private Material initialMaterial;

	private Material zoneMaterial;

	private Vector3 lastSentPosition = Vector3.zero;

	private Quaternion lastSentRotation = Quaternion.identity;

	private bool networkedTransform;

	[HideInInspector]
	public float healthBarScale = 1f;

	public MPTeam Team
	{
		get
		{
			return zoneTeam.Team;
		}
	}

	public bool RegisterDamage
	{
		get
		{
			return HasDamageTrigger();
		}
	}

	public override bool DisplayNameWidget()
	{
		return false;
	}

	public override string GetStartString()
	{
		return LocalisationManager.GetTranslation(3253);
	}

	public override string GetEndString()
	{
		return LocalisationManager.GetTranslation(3254);
	}

	private bool HasDamageTrigger()
	{
		bool result = false;
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.triggerType == TriggerType.MachineDamage)
			{
				result = true;
			}
		}
		return result;
	}

	public float GetHealthScale()
	{
		float result = 1f;
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (entityLogic.triggerType == TriggerType.MachineDamage && entityLogic.useHPRangeToggle)
			{
				float damageIncrement = entityLogic.damageIncrement;
				result = ((damageIncrement == 0f) ? 0f : (100f / damageIncrement));
			}
		}
		return result;
	}

	public override void Init()
	{
		if (!isInitialized)
		{
			zoneTeam = AddTeam(2479, GenericEntity.LOGIC_PREFIX + "team", MPTeam.None);
			zoneTeam.TeamChanged += SetTeam;
			initialZoneColor = visRenderer.material.GetColor("_TintColor");
			addPiece = NetworkAddPiece.Instance;
			levelEditor = LevelEditor.Instance;
			networkAuxAddPiece = NetworkAuxAddPiece.Instance;
			visualController = GetComponent<EntityVisualController>();
			initialRenderer = visualController.renderers[0];
			initialMaterial = initialRenderer.material;
			hasZone = false;
			level = CustomLevel.Instance;
			NetBlock = entity;
			isInitialized = true;
		}
	}

	public List<KeyCode> GetLogicKeys()
	{
		List<KeyCode> list = new List<KeyCode>();
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if ((entityLogic.triggerType == TriggerType.KeyPressed || entityLogic.triggerType == TriggerType.KeyReleased) && !list.Contains(entityLogic.keyPressCode))
			{
				list.Add(entityLogic.keyPressCode);
			}
		}
		return list;
	}

	public void TriggerKey(KeyCode currentKey, bool isPressed)
	{
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			if (((isPressed && entityLogic.triggerType == TriggerType.KeyPressed) || (!isPressed && entityLogic.triggerType == TriggerType.KeyReleased)) && entityLogic.keyPressCode == currentKey)
			{
				ExecuteLogic(entityLogic);
			}
		}
	}

	public override string LogicName()
	{
		if (!hasZone)
		{
			return base.LogicName();
		}
		PlayerData player = buildZone.player;
		return player.name;
	}

	public override bool ActiveOnStart()
	{
		return true;
	}

	public override void SetupDefault()
	{
	}

	public void SetTeam(MPTeam team)
	{
		Color color = ((team != MPTeam.None) ? ReferenceMaster.Instance.zoneColors[(int)team] : initialZoneColor);
		color.a = initialZoneColor.a;
		visRenderer.material.SetColor("_TintColor", color);
		if (hasZone)
		{
			buildZone.UpdateTeam(team, buildZone.currentEnv);
		}
		else
		{
			dialogTrigger.UpdateTeam(team);
		}
	}

	public void OnStartSim()
	{
		if (hasZone && machine.SimPhysics)
		{
			TriggerEvent(TriggerType.Start);
		}
	}

	public void ResetDamage()
	{
		lastDamage = 0f;
	}

	public void OnStopSim()
	{
		if (hasZone && StatMaster.levelSimulating && machine.SimPhysics)
		{
			TriggerEvent(TriggerType.End);
		}
	}

	public void OnMachineDamage(float totalDamage)
	{
		float num = totalDamage * 100f;
		List<EntityLogic> logic = GetLogic();
		for (int i = 0; i < logic.Count; i++)
		{
			EntityLogic entityLogic = logic[i];
			float damageIncrement = entityLogic.damageIncrement;
			if (entityLogic.triggerType == TriggerType.MachineDamage && lastDamage < damageIncrement && num >= damageIncrement)
			{
				ExecuteLogic(entityLogic);
			}
		}
		lastDamage = num;
	}

	public void SetBuildZone(PlayerBuildZone zone, bool sendRPC)
	{
		if (zone.hasSpawnZone)
		{
			zone.spawnZone.RemoveBuildZone();
		}
		Renderer renderer = zone.teamVisRenderers[0];
		zoneMaterial = renderer.material;
		initialRenderer.material = initialMaterial;
		initialRenderer.enabled = false;
		SetPrimaryVisual(renderer);
		hasZone = true;
		dialogTrigger.HideDialog();
		buildZone = zone;
		machine = buildZone.player.machine;
		machineTransform = machine.BuildingMachine.transform;
		zone.SetSpawnZone(this);
		zoneTransform = buildZone.transform;
		OnChange();
		UpdateZone();
		if (StatMaster.isHosting && sendRPC)
		{
			byte[] bytes = BitConverter.GetBytes(zone.player.networkId);
			byte[] identifierBytes = GetIdentifierBytes();
			networkAuxAddPiece.SendNetworkMessage(RPCMessageType.SetSpawnZone, NetworkCompression.Combine(bytes, identifierBytes));
		}
		NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
		if (machine.isLocalMachine)
		{
			MouseOrbit instance2 = SingleInstanceFindOnly<MouseOrbit>.Instance;
			if (instance2.introPlayed)
			{
				instance2.ResetCam();
			}
			if (StatMaster.limitMachines)
			{
				instance.hud.ShowAllowedMachines(machine);
			}
			else
			{
				if (buildZone.player.allowedMachineIndex != -1)
				{
					buildZone.player.allowedMachineIndex = -1;
				}
				else if (buildZone.player.prevMachine && !sendRPC && instance.hud.prevBuild != null)
				{
					instance.LoadLocalMachine(instance.hud.prevBuild);
				}
				else if (StatMaster.isClient && NetworkScene.IsReconnect)
				{
					instance.hud.OnReconnect();
				}
				machine.ToggleModification(true);
			}
			instance.hud.ApplyMachineRules(machine);
			NetworkScene.IsReconnect = false;
			buildZone.player.prevMachine = false;
		}
		machine.ToggleCurtain(StatMaster.Mode.curtainMode, true);
	}

	public void RemoveBuildZone()
	{
		if (hasZone && (bool)machine)
		{
			Renderer renderer = buildZone.teamVisRenderers[0];
			renderer.material = zoneMaterial;
			initialRenderer.enabled = true;
			initialRenderer.material = initialMaterial;
			MPTeam team = zoneTeam.Team;
			Color color = ((team != MPTeam.None) ? ReferenceMaster.Instance.zoneColors[(int)team] : initialZoneColor);
			color.a = initialZoneColor.a;
			initialRenderer.material.SetColor("_TintColor", color);
			SetPrimaryVisual(initialRenderer);
			networkedTransform = false;
			buildZone.RemoveSpawnZone();
			hasZone = false;
			OnChange();
		}
	}

	private void SetPrimaryVisual(Renderer r)
	{
		visualController.renderers[0] = r;
	}

	public override void OnAdd()
	{
		base.OnAdd();
		networkAuxAddPiece.RegisterSpawn(this);
	}

	public override void OnRemove()
	{
		base.OnRemove();
		RemoveBuildZone();
		dialogTrigger.HideDialog();
		networkAuxAddPiece.UnregisterSpawn(this);
	}

	public override void OnPositionChanged(Vector3 pos)
	{
		UpdateZone();
	}

	public void UpdateZone()
	{
		if (!hasZone || machine.isSimulating)
		{
			return;
		}
		if (!machine.isLocalMachine)
		{
			zoneTransform.position = entity.Position;
			zoneTransform.rotation = entity.Rotation;
			return;
		}
		Transform parent = null;
		if (StatMaster.Mode.LevelEditor.moveMachineWithZone)
		{
			machine.SetRigidInterpolation(RigidbodyInterpolation.None);
			parent = machineTransform.parent;
			machineTransform.SetParent(zoneTransform, true);
		}
		Vector3 position = entity.Position;
		Quaternion rotation = entity.Rotation;
		zoneTransform.position = position;
		zoneTransform.rotation = rotation;
		machine.boundingBoxController.SetFloorPos(StatMaster.Bounding.Enabled);
		machine.boundingBoxController.BoundCheck(machine.GetBounds(false));
		networkAuxAddPiece.UpdateBuildZoneTransform(position, rotation);
		if (StatMaster.Mode.LevelEditor.moveMachineWithZone)
		{
			machineTransform.SetParent(parent, true);
			addPiece.UpdateMiddleOfObject();
		}
		Vector3 position2 = machineTransform.position;
		if (!networkedTransform || lastSentPosition != position2)
		{
			machine.SetPosition(position2, false);
			lastSentPosition = position2;
		}
		Quaternion rotation2 = machineTransform.rotation;
		if (!networkedTransform || lastSentRotation != rotation2)
		{
			machine.SetRotation(rotation2, false);
			lastSentRotation = rotation2;
		}
		if (StatMaster.Mode.LevelEditor.moveMachineWithZone)
		{
			machine.RestoreRigidInterpolation();
		}
		networkedTransform = true;
	}

	public void ToggleCollider(bool toggle)
	{
		visCollider.enabled = toggle;
	}

	protected override void LateUpdate()
	{
		bool flag = hasZone && (!AddPiece.isEditingLevel || machine.isSimulating);
		visCollider.enabled = !StatMaster.levelSimulating && !flag;
		visRenderer.enabled = AddPiece.isEditingLevel && !StatMaster.levelSimulating;
	}

	protected override void OnEnable()
	{
		UpdateSimState();
	}
}
