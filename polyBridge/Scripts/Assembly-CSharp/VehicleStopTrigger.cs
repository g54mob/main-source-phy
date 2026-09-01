using System;
using Dreamteck.Splines;
using UnityEngine;

public class VehicleStopTrigger : MonoBehaviour, ITriggerHandler
{
	[Header("Meshes")]
	public GameObject m_PoleAndFlag;

	public GameObject m_Pole;

	public GameObject m_PoleTop;

	public GameObject m_Flag;

	[Header("Fx")]
	public GameObject m_PickupFX;

	[Header("Collision")]
	public BoxCollider m_Collider;

	public BoxCollider m_HotSpot;

	[Header("Outline")]
	public MeshRenderer m_FlagMeshRenderer;

	public SplineComputer m_SplineComputer;

	[NonSerialized]
	public string m_VehicleGuid;

	[NonSerialized]
	public float m_Height;

	[NonSerialized]
	public float m_RotationDegrees;

	[NonSerialized]
	public bool m_Flipped;

	[NonSerialized]
	public bool m_InvisibleInSim;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	private Color m_FlagColor;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private bool m_HasWarnedAboutMissedCheckpoint;

	private ParticleSystem m_PickupParticleSystem;

	internal int m_IndexInScene = -1;

	private MeshRenderer[] m_MeshRenderers;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private MaterialPropertyBlock m_FXMaterialPropertyBlock;

	public int indexInScene => m_IndexInScene;

	public UnityEngine.Object asObject => this;

	Transform ITriggerHandler.transform => base.transform;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_Height = VehicleStopTriggers.DEFAULT_POLE_HEIGHT;
		m_PickupParticleSystem = InstantiatePickupFX(m_PickupFX);
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_FXMaterialPropertyBlock = new MaterialPropertyBlock();
		m_MeshRenderers = m_PoleAndFlag.GetComponentsInChildren<MeshRenderer>();
	}

	private void OnDestroy()
	{
		if (VehicleStopTriggers.m_Triggers.Contains(this))
		{
			m_IndexInScene = -1;
			VehicleStopTriggers.m_Triggers.Remove(this);
		}
		StopPickupFX();
		DestroyPickupFX();
	}

	public void DoOnTriggerStay(Collider other, bool enter)
	{
		if (!m_PoleAndFlag.activeInHierarchy || !other || !other.gameObject)
		{
			return;
		}
		Vehicle componentInParent = other.gameObject.GetComponentInParent<Vehicle>();
		if (!componentInParent || componentInParent.m_Guid != m_VehicleGuid)
		{
			return;
		}
		if (componentInParent.NumCheckpointsRemaining() != 0)
		{
			if (enter && !m_HasWarnedAboutMissedCheckpoint && GameStateManager.GetState() == GameState.SIM)
			{
				GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("UI_VEHICLE_MISSED_CHECKPOINT"), 10f);
				InterfaceAudio.Play("ui_fail_play");
				m_HasWarnedAboutMissedCheckpoint = true;
			}
		}
		else if (componentInParent.IsSimulating())
		{
			componentInParent.TouchedVictoryFlag();
			if (!m_InvisibleInSim)
			{
				PlayPickupFX();
				PlayPickupAudio();
			}
			m_PoleAndFlag.SetActive(value: false);
		}
	}

	public void SetFlagColor(Color color)
	{
		m_FlagColor = color;
		m_FlagMeshRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, color);
		m_FlagMeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		SetColorForPickupFX(color);
	}

	public void Restore()
	{
		m_PoleAndFlag.gameObject.SetActive(value: true);
		m_HasWarnedAboutMissedCheckpoint = false;
		StopPickupFX();
	}

	public string GetTextMeshString()
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_VehicleGuid);
		if (!vehicle)
		{
			return string.Empty;
		}
		return EventTimelines.GetStageLabelForUnit(vehicle.gameObject);
	}

	public void SetPoleScaleForHeight(float height)
	{
		m_HotSpot.center = new Vector3(m_HotSpot.center.x, (height + 0.2f) / 2f, m_HotSpot.center.z);
		m_HotSpot.size = new Vector3(m_HotSpot.size.x, height + 0.2f, m_HotSpot.size.z);
		m_Collider.center = new Vector3(m_Collider.center.x, height / 2f, m_Collider.center.z);
		m_Collider.size = new Vector3(m_Collider.size.x, height, m_Collider.size.z);
		m_Pole.transform.localScale = new Vector3(m_Pole.transform.localScale.x, height / VehicleStopTriggers.DEFAULT_POLE_HEIGHT, m_Pole.transform.localScale.z);
		float num = height - VehicleStopTriggers.DEFAULT_POLE_HEIGHT;
		Vector3 position = m_Pole.transform.position + m_Pole.transform.up * num;
		m_Flag.transform.position = position;
		m_PoleTop.transform.position = position;
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		m_PoleAndFlag.SetActive(value: true);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].enabled = !m_InvisibleInSim;
		}
	}

	public void UpdateOutline()
	{
		m_PoleAndFlag.SetActive(GameStateManager.GetState() != GameState.SANDBOX);
		if (!ShouldShowOutline())
		{
			if (m_Outline != null)
			{
				m_Outline.SetActive(active: false);
			}
			return;
		}
		if (!m_HasCreatedOutline)
		{
			m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_SandboxItem.SetOutlineDirty(dirty: true);
			m_HasCreatedOutline = true;
		}
		if (m_Outline != null && m_SandboxItem.IsOutlineDirty())
		{
			m_SplineComputer.transform.localScale = new Vector3(m_Flipped ? (0f - Mathf.Abs(m_SplineComputer.transform.localScale.x)) : Mathf.Abs(m_SplineComputer.transform.localScale.x), m_SplineComputer.transform.localScale.y, m_SplineComputer.transform.localScale.z);
			m_SandboxItem.UpdateOutlineFromSpline(m_Outline, m_SplineComputer, m_Height - VehicleStopTriggers.DEFAULT_POLE_HEIGHT, GameGrid.m_Spacing);
			m_SandboxItem.SetOutlineDirty(dirty: false);
		}
	}

	public bool ShouldShowOutline()
	{
		return GameStateManager.GetState() == GameState.SANDBOX;
	}

	public void ResolveOverlap()
	{
		while (OverlapsOtherVehicleStopTriggers())
		{
			base.transform.Translate(0f - (m_HotSpot.bounds.size.x + 0.01f), 0f, 0f, Space.World);
		}
	}

	public void Desaturate(bool on)
	{
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		foreach (MeshRenderer obj in meshRenderers)
		{
			obj.GetPropertyBlock(m_MaterialPropertyBlock);
			m_MaterialPropertyBlock.SetFloat(ShaderVariables_Common.SATURATION_SHADER_ID, on ? 0f : 1f);
			if (obj == m_FlagMeshRenderer)
			{
				m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.ALBEDO_COLOR_SHADER_ID, on ? ((Vector4)Color.gray) : ((Vector4)m_FlagColor));
			}
			obj.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void EnableHotspotCollider(bool on)
	{
		m_HotSpot.gameObject.SetActive(on);
	}

	public void SnapToTerrainSurface()
	{
		if (Physics.Raycast(new Vector3(base.transform.position.x, 100000f, 0f), Vector3.down, out var hitInfo, float.MaxValue, Utils.TERRAIN_LAYER_MASK))
		{
			base.transform.position = m_SandboxItem.SnapPosToGrid(hitInfo.point);
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
	}

	private bool OverlapsOtherVehicleStopTriggers()
	{
		foreach (VehicleStopTrigger trigger in VehicleStopTriggers.m_Triggers)
		{
			if (trigger.gameObject.activeInHierarchy && trigger != this && trigger.m_HotSpot.bounds.Intersects(m_HotSpot.bounds))
			{
				return true;
			}
		}
		return false;
	}

	private void PlayPickupAudio(bool final = false)
	{
		if (Vehicles.AllVehiclesHaveCollectedVictoryFlags())
		{
			SimAudio.Play("sfx_simulation_flag_final_pickup", base.transform.position, useSimPitch: false);
		}
		else
		{
			SimAudio.Play("sfx_simulation_flag_pickup", base.transform.position, useSimPitch: false);
		}
	}

	private ParticleSystem InstantiatePickupFX(GameObject effectPrefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(effectPrefab, base.transform);
		if (!(gameObject != null))
		{
			return null;
		}
		return gameObject.GetComponent<ParticleSystem>();
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
}
