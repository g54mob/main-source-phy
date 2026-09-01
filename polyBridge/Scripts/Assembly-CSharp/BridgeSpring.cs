using System;
using Poly.Game;
using Poly.Physics;
using UnityEngine;

public class BridgeSpring : MonoBehaviour
{
	[Header("UI")]
	public BridgeSpringSlider m_Slider;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public BridgeEdge m_ParentEdge;

	[NonSerialized]
	public float m_FreeLengthOverrideMultiplier;

	[NonSerialized]
	public GameObject m_LinkPrefab;

	[NonSerialized]
	public BridgeSpringLink m_FrontLink;

	[NonSerialized]
	public BridgeSpringLink m_BackLink;

	[NonSerialized]
	public ToolTip m_ToolTip;

	private Mesh m_sharedLinkMesh;

	private static readonly float TOOLTIP_LINGER_TIME_SECONDS = 0.5f;

	private float m_TooltipLingerUntil;

	private bool isDebris { get; set; }

	public void Init(BridgeEdge edge, GameObject linkPrefab, float normalizedValue, string guid)
	{
		m_ParentEdge = edge;
		m_ParentEdge.m_MeshRenderer.enabled = false;
		m_Slider = edge.GetComponentInChildren<BridgeSpringSlider>();
		m_Slider.SetNormalizedValue(normalizedValue);
		edge.m_SpringCoilVisualization = this;
		m_LinkPrefab = linkPrefab;
		m_FreeLengthOverrideMultiplier = (float)GetTargetFreeLengthPercent() / 100f;
		m_Guid = guid;
		UpdateLinks();
		CreateToolTip();
	}

	private void OnDisable()
	{
		if (m_ToolTip != null)
		{
			m_ToolTip.gameObject.SetActive(value: false);
		}
	}

	public void CreateToolTip()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_ToolTip, GameUI.m_Instance.transform);
		if ((bool)gameObject)
		{
			m_ToolTip = gameObject.GetComponent<ToolTip>();
			if ((bool)m_ToolTip)
			{
				m_ToolTip.gameObject.SetActive(value: false);
				m_ToolTip.name = "Spring ToolTip";
			}
		}
	}

	public void UpdateManual()
	{
		m_Slider.UpdateManual();
	}

	public void LateUpdate()
	{
		UpdateToolTip();
	}

	public void SetStressColor(Color stressColor)
	{
		m_FrontLink.SetStressColor(stressColor);
		m_BackLink.SetStressColor(stressColor);
	}

	public void Desaturate(bool desaturate)
	{
		m_FrontLink.Desaturate(desaturate);
		m_BackLink.Desaturate(desaturate);
	}

	public void DestroyManual()
	{
		DestroyLinks();
		if ((bool)m_ToolTip)
		{
			UnityEngine.Object.Destroy(m_ToolTip.gameObject);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void DestroyLinks()
	{
		m_FrontLink.Destroy();
		m_BackLink.Destroy();
		UnityEngine.Object.Destroy(m_sharedLinkMesh);
		m_sharedLinkMesh = null;
	}

	public bool MaybeRecreateLinks()
	{
		if (m_ParentEdge == null || m_ParentEdge.m_SpringCoilVisualization == null)
		{
			return false;
		}
		float b = CalcNumLoops();
		if (m_FrontLink == null || !m_FrontLink.m_meshGenerator || !Mathf.Approximately(m_FrontLink.m_meshGenerator.numLoopsToGenerate, b))
		{
			m_FrontLink.Destroy();
			m_BackLink.Destroy();
			UnityEngine.Object.Destroy(m_sharedLinkMesh);
			m_sharedLinkMesh = null;
			UpdateLinks();
			return true;
		}
		return false;
	}

	public void UpdateLinks()
	{
		if (m_ParentEdge == null)
		{
			return;
		}
		Transform parent = m_ParentEdge.transform;
		if (m_FrontLink == null || !m_FrontLink.m_Link || m_BackLink == null || !m_BackLink.m_Link)
		{
			UnityEngine.Object.Destroy(m_sharedLinkMesh);
			if (m_FrontLink != null)
			{
				m_FrontLink.Destroy();
			}
			if (m_BackLink != null)
			{
				m_BackLink.Destroy();
			}
			m_FrontLink = CreateLink(null, parent, out m_sharedLinkMesh);
			m_BackLink = CreateLink(m_sharedLinkMesh, parent, out m_sharedLinkMesh);
		}
		SetPositionsOfLinks(m_FrontLink, -2.11f * Vector3.forward);
		SetPositionsOfLinks(m_BackLink, -2.11f * Vector3.back);
	}

	public void UpdateFreeLengthFromSliderPos()
	{
		m_FreeLengthOverrideMultiplier = (float)GetTargetFreeLengthPercent() / 100f;
	}

	public void RefreshVisualization()
	{
		UpdateFreeLengthFromSliderPos();
		if (!MaybeRecreateLinks())
		{
			UpdateLinks();
		}
	}

	private float CalcNumLoops()
	{
		float num2;
		if (m_FrontLink != null)
		{
			SpringCoilMeshGenerator meshGenerator = m_FrontLink.m_meshGenerator;
			float num = Mathf.Min(meshGenerator.separationFromNodeCenter, float.MaxValue);
			num2 = (GetSpringFreeLength() - (isDebris ? 1f : 2f) * num) / meshGenerator.singleCoilLength;
			if (!isDebris)
			{
				num2 = Mathf.Round(num2 - 0.5f) + 0.5f;
			}
		}
		else
		{
			num2 = float.PositiveInfinity;
		}
		return num2;
	}

	private BridgeSpringLink CreateLink(Mesh sharedMesh, Transform parent, out Mesh resultMesh)
	{
		isDebris = (bool)m_ParentEdge.m_PhysicsEdge && m_ParentEdge.m_PhysicsEdge.material.isDebris;
		BridgeSpringLink bridgeSpringLink = new BridgeSpringLink(m_LinkPrefab, parent);
		SpringCoilMeshGenerator meshGenerator = bridgeSpringLink.m_meshGenerator;
		float num = Mathf.Min(meshGenerator.separationFromNodeCenter, float.MaxValue);
		meshGenerator.numLoopsToGenerate = (GetSpringFreeLength() - (isDebris ? 1f : 2f) * num) / meshGenerator.singleCoilLength;
		if (!isDebris)
		{
			meshGenerator.numLoopsToGenerate = Mathf.Round(meshGenerator.numLoopsToGenerate - 0.5f) + 0.5f;
		}
		if (sharedMesh == null)
		{
			sharedMesh = meshGenerator.GenerateCoilMesh();
		}
		meshGenerator.AssignMesh(sharedMesh);
		resultMesh = sharedMesh;
		return bridgeSpringLink;
	}

	private void SetPositionsOfLinks(BridgeSpringLink link, Vector3 offset)
	{
		Vector2 vector = m_ParentEdge.m_JointA.m_Transform.position;
		Vector2 vector2 = m_ParentEdge.m_JointB.m_Transform.position;
		if ((bool)m_ParentEdge.m_PhysicsEdge && (bool)m_ParentEdge.m_PhysicsEdge.handle)
		{
			Edge physicsEdge = m_ParentEdge.m_PhysicsEdge;
			vector = physicsEdge.node0.cachedSmoothPos;
			vector2 = physicsEdge.node1.cachedSmoothPos;
		}
		float num = Mathf.Min(link.m_meshGenerator.separationFromNodeCenter, float.MaxValue);
		Vector2 normalized = (vector2 - vector).normalized;
		vector += normalized * num;
		vector2 -= normalized * num * (isDebris ? 0.3f : 1f);
		link.m_meshGenerator.SetPositionFromTo(vector, vector2, Vector3.forward, offset);
	}

	private float GetSpringFreeLength()
	{
		if (!m_ParentEdge.m_PhysicsEdge || !m_ParentEdge.m_PhysicsEdge.handle)
		{
			return m_ParentEdge.GetLength() * m_ParentEdge.m_SpringCoilVisualization.m_FreeLengthOverrideMultiplier;
		}
		return m_ParentEdge.m_PhysicsEdge.handle.length;
	}

	private void UpdateToolTip()
	{
		if (!m_ToolTip)
		{
			return;
		}
		bool flag = Time.unscaledTime < m_TooltipLingerUntil;
		if ((GameStateManager.GetState() != GameState.BUILD || GameStateCommonInput.IgnoreKeyboardInput() || (!flag && !GameInput.IsDown(BindingType.SHOW_ALL_TOOLTIPS)) || !SandboxSettings.m_SpringAdjustmentsAllowed || m_ParentEdge.IsLocked()) && !ShouldShowToolTip())
		{
			m_ToolTip.gameObject.SetActive(value: false);
			return;
		}
		m_ToolTip.gameObject.SetActive(value: true);
		int displayPercent = GetDisplayPercent();
		if (displayPercent == 0)
		{
			m_ToolTip.Set(Localize.Get("SPRING_NEUTRAL"), null);
		}
		else if (displayPercent > 0)
		{
			m_ToolTip.Set(string.Format(Localize.Get("SPRING_STRETCH_BY"), displayPercent), null);
		}
		else
		{
			m_ToolTip.Set(string.Format(Localize.Get("SPRING_COMPRESS_BY"), -displayPercent), null);
		}
		Vector2 vector = Cameras.MainCamera().WorldToScreenPoint(m_Slider.m_Handle.transform.position);
		if (Utils.PointIsOffscreen(vector))
		{
			m_ToolTip.gameObject.SetActive(value: false);
		}
		else
		{
			GameUI.SetScreenPosClamped(m_ToolTip.gameObject, vector, 20f, (0f - m_ToolTip.m_RectTransform.sizeDelta.y) / 2f);
		}
	}

	public int GetDisplayPercent()
	{
		float normalizedValue = m_Slider.GetNormalizedValue();
		if (normalizedValue >= 0.5f)
		{
			return Mathf.RoundToInt(Mathf.Lerp(0f, 1f, (normalizedValue - 0.5f) / 0.5f) * 100f);
		}
		return -Mathf.RoundToInt(Mathf.Lerp(1f, 0f, normalizedValue / 0.5f) * 100f);
	}

	public int GetTargetFreeLengthPercent()
	{
		float normalizedValue = m_Slider.GetNormalizedValue();
		float num = 0f;
		num = (Mathf.Approximately(normalizedValue, 0.5f) ? 1f : ((!(normalizedValue > 0.5f)) ? Mathf.Lerp(BridgeSprings.MAX_FREELENGTH_MULTIPLIER, 1f, Mathf.Clamp01(normalizedValue / 0.5f)) : Mathf.Lerp(1f, BridgeSprings.MIN_FREELENGTH_MULTIPLIER, Mathf.Clamp01((normalizedValue - 0.5f) / 0.5f))));
		return Mathf.RoundToInt(num * 100f);
	}

	private bool ShouldShowToolTip()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameStateManager.GetState() != GameState.BUILD || GameStateBuild.m_CameraInTransition)
		{
			return false;
		}
		if (GameUI.SaveLoadPanelIsActive() || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (!Profiles.m_ActiveProfile.m_DisableBuildHelpTooltips && GameInput.GetActiveGameDevice() != GameDevice.Gamepad && !GameUI.m_Instance.m_PointerToolTip.gameObject.activeInHierarchy && !BridgeSelectionSet.ContainsEdge(m_ParentEdge))
		{
			return false;
		}
		if ((bool)BridgeSprings.m_SliderFollowingMouse && BridgeSprings.m_SliderFollowingMouse.m_BridgeSpring == this && BridgeSprings.m_SliderFollowingMouse.m_Handle.gameObject.activeInHierarchy)
		{
			return true;
		}
		if (GameStateBuild.m_HoverBridgeSpringSlider == m_Slider || GameStateBuild.m_HoverLockedEdge == m_Slider.m_BridgeSpring.m_ParentEdge)
		{
			return true;
		}
		if (m_Slider.m_Handle.gameObject.activeInHierarchy && BridgeSprings.GetSpringSliderUnderMouse() == this)
		{
			return true;
		}
		if ((GameInput.IsDown(BindingType.NUDGE_HYDRO_UP) || GameInput.IsDown(BindingType.NUDGE_HYDRO_DOWN)) && (BridgeSelectionSet.ContainsEdge(m_ParentEdge) || GameStateBuild.m_HoverEdge == m_ParentEdge))
		{
			m_TooltipLingerUntil = Time.unscaledTime + TOOLTIP_LINGER_TIME_SECONDS;
			return true;
		}
		if (Time.unscaledTime < m_TooltipLingerUntil)
		{
			return true;
		}
		return false;
	}

	private bool IsConnectedToJoint(BridgeJoint joint)
	{
		if (!(m_ParentEdge.m_JointA == joint))
		{
			return m_ParentEdge.m_JointB == joint;
		}
		return true;
	}

	public static implicit operator bool(BridgeSpring s)
	{
		return s != null;
	}
}
