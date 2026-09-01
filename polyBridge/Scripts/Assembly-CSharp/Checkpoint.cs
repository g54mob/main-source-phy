using System;
using Dreamteck.Splines;
using UnityEngine;

public class Checkpoint : MonoBehaviour, ITriggerHandler
{
	public BoxCollider m_Hotspot;

	[Header("Star")]
	public GameObject m_Star;

	public MeshRenderer m_StarMeshRenderer;

	public SplineComputer m_StarSpline;

	public GameObject m_StarPickupFX;

	[Header("Stop")]
	public GameObject m_Stop;

	public MeshRenderer m_StopMeshRenderer;

	public SplineComputer m_StopSpline;

	public GameObject m_StopPickupFX;

	[Header("Reverse")]
	public GameObject m_Reverse;

	public MeshRenderer m_ReverseMeshRenderer;

	public GameObject m_ReversePickupFX;

	public SplineComputer[] m_ReverseSplines;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public string m_VehicleGuid;

	[NonSerialized]
	public string m_VehicleRestartPhaseGuid;

	[NonSerialized]
	public bool m_TriggerTimeline;

	[NonSerialized]
	public bool m_StopVehicle;

	[NonSerialized]
	public bool m_ReverseVehicleOnRestart;

	[NonSerialized]
	public bool m_InvisibleInSim;

	[NonSerialized]
	public bool m_ChangeSpeed;

	[NonSerialized]
	public EventTimeline m_Timeline;

	[NonSerialized]
	public Color m_Color;

	[NonSerialized]
	public float m_SpeedMultiplier;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	private Outline m_OutlineA;

	private Outline m_OutlineB;

	private bool m_HasCreatedOutline;

	private ParticleSystem m_PickupParticleSystem;

	private Vector3 m_StarOriginalScale;

	private Vector3 m_StopOriginalScale;

	private Vector3 m_ReverseOriginalScale;

	private Color m_LastOutlineColor;

	internal int m_IndexInScene = -1;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private MaterialPropertyBlock m_FXMaterialPropertyBlock;

	public int indexInScene => m_IndexInScene;

	public UnityEngine.Object asObject => this;

	Transform ITriggerHandler.transform => base.transform;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_StarOriginalScale = m_StarMeshRenderer.transform.localScale;
		m_StopOriginalScale = m_StopMeshRenderer.transform.localScale;
		m_ReverseOriginalScale = m_ReverseMeshRenderer.transform.localScale;
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_FXMaterialPropertyBlock = new MaterialPropertyBlock();
	}

	private void OnDestroy()
	{
		DestroyPickupFX();
	}

	private void Start()
	{
	}

	public void ResetScale()
	{
		m_StarMeshRenderer.transform.localScale = m_StarOriginalScale;
		m_StopMeshRenderer.transform.localScale = m_StopOriginalScale;
		m_ReverseMeshRenderer.transform.localScale = m_ReverseOriginalScale;
	}

	public void DestroyManual()
	{
		RemoveCheckPointFromVehicle();
		RemoveVehicleRestartPhase();
		StopPickupFX();
		DestroyPickupFX();
		EventTimelines.DestroyCheckpointTimeline(this);
		if (Checkpoints.m_Checkpoints.Contains(this))
		{
			m_IndexInScene = -1;
			Checkpoints.m_Checkpoints.Remove(this);
		}
		base.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void DoOnTriggerStay(Collider other, bool enter_unused)
	{
		if (!other || !other.gameObject)
		{
			return;
		}
		Vehicle componentInParent = other.gameObject.GetComponentInParent<Vehicle>();
		if ((bool)componentInParent && !(componentInParent.m_Guid != m_VehicleGuid) && componentInParent.CanPickUpCheckpoint(this))
		{
			componentInParent.PickUpCheckpoint(this);
			DisableMeshes();
			if (!m_InvisibleInSim)
			{
				PlayPickupFX();
				PlayPickupAudio();
			}
			if (m_TriggerTimeline && (bool)m_Timeline)
			{
				m_Timeline.StartSimulation();
			}
			if (m_StopVehicle)
			{
				componentInParent.TouchedStopCheckpoint();
			}
		}
	}

	public void RemoveCheckPointFromVehicle()
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_VehicleGuid);
		if ((bool)vehicle && vehicle.m_Checkpoints.Contains(this))
		{
			vehicle.m_Checkpoints.Remove(this);
		}
	}

	public void UpdateFloatingText()
	{
		SandboxItem component = GetComponent<SandboxItem>();
		if ((bool)component && (bool)component.m_Label)
		{
			component.UpdateFloatingText();
		}
	}

	public string GetTextMeshString()
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_VehicleGuid);
		if (!vehicle)
		{
			return string.Empty;
		}
		string stageLabelForUnit = EventTimelines.GetStageLabelForUnit(vehicle.gameObject);
		return string.Format("{0}.{1}{2}", stageLabelForUnit, vehicle.m_Checkpoints.IndexOf(this) + 1, vehicle.m_OrderedCheckpoints ? "#" : string.Empty);
	}

	public CheckpointType GetCheckpointType()
	{
		if (m_StopVehicle && m_ReverseVehicleOnRestart)
		{
			return CheckpointType.Reverse;
		}
		if (m_StopVehicle)
		{
			return CheckpointType.Stop;
		}
		return CheckpointType.Star;
	}

	public Sprite GetCheckpointSprite()
	{
		CheckpointType checkpointType = GetCheckpointType();
		switch (checkpointType)
		{
		case CheckpointType.Reverse:
			return GameUI.m_Instance.m_EventEditor.m_ReverseSprite;
		case CheckpointType.Stop:
			return GameUI.m_Instance.m_EventEditor.m_StopSprite;
		case CheckpointType.Star:
			return GameUI.m_Instance.m_EventEditor.m_StarFilled;
		default:
			Debug.LogWarning("Unexpected checkpoint type: " + checkpointType);
			return null;
		}
	}

	public void RefreshMesh()
	{
		CheckpointType checkpointType = GetCheckpointType();
		EnableCheckpointType(checkpointType);
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			DisableMeshes();
		}
		else
		{
			EnableCheckpointMesh(checkpointType);
		}
		m_SandboxItem.m_OutlineGroup.ClearCachedSplinePoints();
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void Restore()
	{
		RefreshMesh();
		StopPickupFX();
	}

	public void SetColor(Color color)
	{
		m_Color = color;
		m_MaterialPropertyBlock.Clear();
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, color);
		UploadMaterialPropertyBlock(m_MaterialPropertyBlock);
		SetColorForPickupFX(color);
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		EnableCheckpointMesh(GetCheckpointType());
	}

	public void EnterGameState(GameState gameState)
	{
		CheckpointType checkpointType = GetCheckpointType();
		EnableCheckpointType(checkpointType);
		if (gameState != GameState.SANDBOX)
		{
			EnableCheckpointMesh(checkpointType);
		}
		if (!m_HasCreatedOutline)
		{
			CreateOutlines();
			SetOutlineColor();
			m_HasCreatedOutline = true;
		}
	}

	public void SetOutlineColor()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			CheckpointType checkpointType = GetCheckpointType();
			MaybeUpdateOutline(checkpointType, GameState.BUILD);
		}
		Color color = (m_SandboxItem.m_Desaturated ? Color.grey : Color.black);
		if (color != m_OutlineA.m_VectorLine.color)
		{
			m_OutlineA.SetColor(color);
		}
		if (color != m_OutlineB.m_VectorLine.color)
		{
			m_OutlineB.SetColor(color);
		}
	}

	public void UpdateOutline()
	{
		if (!m_HasCreatedOutline)
		{
			CreateOutlines();
			m_HasCreatedOutline = true;
		}
		CheckpointType checkpointType = GetCheckpointType();
		MaybeUpdateOutline(checkpointType, GameStateManager.GetState());
	}

	public void Desaturate(bool on)
	{
		m_MaterialPropertyBlock.Clear();
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_Common.SATURATION_SHADER_ID, on ? 0f : 1f);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, on ? ((Vector4)Color.gray) : ((Vector4)m_Color));
		UploadMaterialPropertyBlock(m_MaterialPropertyBlock);
	}

	public void ResolveOverlap()
	{
		bool flag = false;
		while (OverlapsOtherCheckpoints())
		{
			base.transform.Translate(GetMeshRenderer().bounds.size.x + GameGrid.m_Spacing * 3f, 0f, 0f, Space.World);
			flag = true;
		}
		if (flag)
		{
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
	}

	public MeshRenderer GetMeshRenderer()
	{
		CheckpointType checkpointType = GetCheckpointType();
		switch (checkpointType)
		{
		case CheckpointType.Star:
			return m_StarMeshRenderer;
		case CheckpointType.Stop:
			return m_StopMeshRenderer;
		case CheckpointType.Reverse:
			return m_ReverseMeshRenderer;
		default:
			Debug.LogWarning($"Could not find MeshRenderer for {checkpointType}");
			return null;
		}
	}

	public void EnableHotspotCollider(bool on)
	{
		m_Hotspot.gameObject.SetActive(on);
	}

	public void DisableMeshes()
	{
		m_StarMeshRenderer.gameObject.SetActive(value: false);
		m_StopMeshRenderer.gameObject.SetActive(value: false);
		m_ReverseMeshRenderer.gameObject.SetActive(value: false);
	}

	public void InstantiatePickupFX()
	{
		DestroyPickupFX();
		GameObject gameObject = UnityEngine.Object.Instantiate(GetEffectPrefab(), base.transform);
		m_PickupParticleSystem = gameObject.GetComponent<ParticleSystem>();
	}

	private void SetColorForPickupFX(Color color)
	{
		if (m_PickupParticleSystem != null)
		{
			Renderer component = m_PickupParticleSystem.GetComponent<Renderer>();
			component.GetPropertyBlock(m_FXMaterialPropertyBlock);
			m_FXMaterialPropertyBlock.SetColor("_BaseColor", color);
			m_FXMaterialPropertyBlock.SetVector("_EmissionColor", color);
			component.SetPropertyBlock(m_FXMaterialPropertyBlock);
		}
	}

	private bool OverlapsOtherCheckpoints()
	{
		foreach (Checkpoint checkpoint in Checkpoints.m_Checkpoints)
		{
			if (checkpoint.gameObject.activeInHierarchy && checkpoint != this && checkpoint.GetMeshRenderer().bounds.Intersects(GetMeshRenderer().bounds))
			{
				return true;
			}
		}
		return false;
	}

	private void UploadMaterialPropertyBlock(MaterialPropertyBlock block)
	{
		GetMeshRenderer()?.SetPropertyBlock(block);
	}

	private void EnableCheckpointMesh(CheckpointType type)
	{
		switch (type)
		{
		case CheckpointType.Star:
			m_StarMeshRenderer.gameObject.SetActive(!IsInvisible());
			SetColor(m_Color);
			break;
		case CheckpointType.Stop:
			m_StopMeshRenderer.gameObject.SetActive(!IsInvisible());
			SetColor(m_Color);
			break;
		case CheckpointType.Reverse:
			m_ReverseMeshRenderer.gameObject.SetActive(!IsInvisible());
			SetColor(m_Color);
			break;
		}
	}

	private bool IsInvisible()
	{
		if (GameStateManager.GetState() == GameState.SIM || GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			return m_InvisibleInSim;
		}
		return false;
	}

	private void EnableCheckpointType(CheckpointType type)
	{
		m_Star.SetActive(type == CheckpointType.Star);
		m_Stop.SetActive(type == CheckpointType.Stop);
		m_Reverse.SetActive(type == CheckpointType.Reverse);
	}

	private void PlayPickupAudio()
	{
		switch (GetCheckpointType())
		{
		case CheckpointType.Star:
			SimAudio.Play("sfx_checkpoint_star", base.transform.position);
			break;
		case CheckpointType.Stop:
			SimAudio.Play("sfx_checkpoint_stop", base.transform.position);
			break;
		case CheckpointType.Reverse:
			SimAudio.Play("sfx_checkpoint_reverse", base.transform.position);
			break;
		default:
			SimAudio.Play("sfx_checkpoint_star", base.transform.position);
			break;
		}
	}

	private GameObject GetEffectPrefab()
	{
		return GetCheckpointType() switch
		{
			CheckpointType.Star => m_StarPickupFX, 
			CheckpointType.Stop => m_StopPickupFX, 
			CheckpointType.Reverse => m_ReversePickupFX, 
			_ => m_StarPickupFX, 
		};
	}

	private void PlayPickupFX()
	{
		if (m_PickupParticleSystem != null)
		{
			m_PickupParticleSystem.Play();
		}
	}

	private void StopPickupFX()
	{
		if ((bool)m_PickupParticleSystem)
		{
			m_PickupParticleSystem.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	private void DestroyPickupFX()
	{
		if (m_PickupParticleSystem != null)
		{
			UnityEngine.Object.Destroy(m_PickupParticleSystem.gameObject);
			m_PickupParticleSystem = null;
		}
	}

	private void RemoveVehicleRestartPhase()
	{
		VehicleRestartPhase vehicleRestartPhase = VehicleRestartPhases.FindByGuid(m_VehicleRestartPhaseGuid);
		if ((bool)vehicleRestartPhase)
		{
			VehicleRestartPhases.DestroyPhase(vehicleRestartPhase);
		}
	}

	private void MaybeUpdateOutline(CheckpointType type, GameState gameState)
	{
		bool active = gameState == GameState.SANDBOX || gameState == GameState.BUILD;
		switch (type)
		{
		case CheckpointType.Reverse:
			if (m_SandboxItem.IsOutlineDirty())
			{
				m_SandboxItem.UpdateOutlineFromSpline(m_OutlineA, m_ReverseSplines[0]);
				m_SandboxItem.UpdateOutlineFromSpline(m_OutlineB, m_ReverseSplines[1]);
			}
			m_OutlineA.SetActive(active);
			m_OutlineB.SetActive(active);
			break;
		case CheckpointType.Stop:
			if (m_SandboxItem.IsOutlineDirty())
			{
				m_SandboxItem.UpdateOutlineFromSpline(m_OutlineA, m_StopSpline);
			}
			m_OutlineA.SetActive(active);
			m_OutlineB.SetActive(active: false);
			break;
		case CheckpointType.Star:
			if (m_SandboxItem.IsOutlineDirty())
			{
				m_SandboxItem.UpdateOutlineFromSpline(m_OutlineA, m_StarSpline);
			}
			m_OutlineA.SetActive(active);
			m_OutlineB.SetActive(active: false);
			break;
		default:
			Debug.LogWarningFormat("Unexpected checkpoint type {0}", type.ToString());
			break;
		}
		if (m_SandboxItem.IsOutlineDirty())
		{
			m_OutlineA.m_VectorLine.Draw3DAuto();
			m_OutlineB.m_VectorLine.Draw3DAuto();
			m_SandboxItem.SetOutlineDirty(dirty: false);
		}
	}

	private void CreateOutlines()
	{
		m_OutlineA = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
		m_OutlineB = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
		m_OutlineA.SetLayer(Utils.RENDER_LAST_LAYER);
		m_OutlineB.SetLayer(Utils.RENDER_LAST_LAYER);
		m_HasCreatedOutline = true;
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}
}
