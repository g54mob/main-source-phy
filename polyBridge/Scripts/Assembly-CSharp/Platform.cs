using System;
using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public Collider m_Collider;

	public GameObject m_PlankPrefab;

	public GameObject[] m_PlankVariantPrefabs;

	public GameObject m_PolePrefab;

	public Transform m_PlanksParent;

	public Transform m_PolesParent;

	[NonSerialized]
	public float m_Width;

	[NonSerialized]
	public float m_Height;

	[NonSerialized]
	public bool m_Flipped;

	[NonSerialized]
	public bool m_Solid;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public List<MeshRenderer> m_Planks = new List<MeshRenderer>();

	[NonSerialized]
	public List<MeshRenderer> m_Poles = new List<MeshRenderer>();

	private Outline m_PlatformOutline;

	private Outline m_LeftStiltOutline;

	private Outline m_RightStiltOutline;

	private List<Outline> m_MiddleStiltOutlines = new List<Outline>();

	private bool m_HasCreatedOutline;

	private List<Vector3> m_TempPointsBuffer = new List<Vector3>();

	private readonly float PLANK_WIDTH = 0.56f;

	private readonly float PLANK_ZSCALE = 1.2f;

	private readonly float POLE_SCALE_X = 0.7f;

	private readonly float POLE_WIDTH = 0.25f;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private bool m_isRenderingEnabled = true;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_Width = Platforms.DEFAULT_WIDTH;
		m_Height = Platforms.DEFAULT_HEIGHT;
		m_Solid = false;
	}

	private void Start()
	{
		RefreshMesh();
	}

	private void OnDestroy()
	{
		foreach (MeshRenderer plank in m_Planks)
		{
			UnityEngine.Object.Destroy(plank.gameObject);
		}
		foreach (MeshRenderer pole in m_Poles)
		{
			UnityEngine.Object.Destroy(pole.gameObject);
		}
		if (Platforms.m_Platforms.Contains(this))
		{
			Platforms.m_Platforms.Remove(this);
		}
		m_Planks.Clear();
		m_Poles.Clear();
	}

	public void SetHeight(float newHeight)
	{
		m_Height = newHeight;
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void RefreshMesh()
	{
		PositionPlanks();
		PositionPoles();
		RefreshCollider();
		m_SandboxItem.m_OutlineGroup.ClearCachedSplinePoints();
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public void UpdateShaderProperties(bool buildMode)
	{
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_SimpleLitCollidable.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		foreach (MeshRenderer plank in m_Planks)
		{
			m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeCollideTint);
			plank.SetPropertyBlock(m_MaterialPropertyBlock);
		}
		foreach (MeshRenderer pole in m_Poles)
		{
			m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeSupportCollideTint);
			pole.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public Platform Duplicate(Vector3 offset)
	{
		Platform platform = Platforms.CreatePlatform(base.transform.position, base.transform.rotation);
		if ((bool)platform)
		{
			platform.m_Width = m_Width;
			platform.m_Height = m_Height;
			platform.m_Flipped = m_Flipped;
			platform.m_Solid = m_Solid;
			platform.transform.position += offset;
			platform.RefreshMesh();
		}
		return platform;
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		GetPlanksParent().gameObject.SetActive(value: true);
		m_PolesParent.gameObject.SetActive(!Mathf.Approximately(m_Height, 0f));
	}

	public void DisableMeshRendering()
	{
		GetPlanksParent().gameObject.SetActive(value: false);
		m_PolesParent.gameObject.SetActive(value: false);
	}

	public void UpdateOutline()
	{
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			if (m_isRenderingEnabled)
			{
				DisableMeshRendering();
			}
		}
		else if (!m_isRenderingEnabled)
		{
			EnableMeshRendering();
		}
		if (!m_HasCreatedOutline)
		{
			m_PlatformOutline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_LeftStiltOutline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_RightStiltOutline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (!m_SandboxItem.IsOutlineDirty())
		{
			return;
		}
		if (m_PlatformOutline != null)
		{
			Vector3 size = new Vector3(m_Width, Platforms.THICKNESS, 0f);
			m_SandboxItem.UpdateOutlineFromBounds(m_PlatformOutline, new Bounds(base.transform.position, size));
			m_PlatformOutline.SetTexture((GameStateManager.GetState() == GameState.BUILD) ? GameUI.m_Instance.m_OutlineTextureDashedBuildMode : GameUI.m_Instance.m_OutlineTextureSandbox);
			m_PlatformOutline.SetTextureScale(GameUI.m_Instance.m_OutlineTextureScale);
		}
		int numMiddlePoles = GetNumMiddlePoles();
		if (m_MiddleStiltOutlines.Count < numMiddlePoles)
		{
			int num = numMiddlePoles - m_MiddleStiltOutlines.Count;
			for (int i = 0; i < num; i++)
			{
				m_MiddleStiltOutlines.Add(m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox));
			}
		}
		if (m_Height > 0f && GameStateManager.GetState() == GameState.SANDBOX)
		{
			UpdateStiltsOutline();
		}
		else
		{
			DisableStiltsOutline();
		}
		m_SandboxItem.SetOutlineDirty(dirty: false);
	}

	public void EnterBuildMode()
	{
		EnableMeshRendering();
		if (m_PlatformOutline != null)
		{
			m_PlatformOutline.SetTexture(GameUI.m_Instance.m_OutlineTextureDashedBuildMode);
		}
		DisableStiltsOutline();
	}

	public void EnterSandboxMode()
	{
		DisableMeshRendering();
		if (Mathf.Approximately(m_Height, 0f))
		{
			DisableStiltsOutline();
		}
		else
		{
			UpdateStiltsOutline();
		}
	}

	private void DisableStiltsOutline()
	{
		if (m_LeftStiltOutline != null)
		{
			m_LeftStiltOutline.SetActive(active: false);
		}
		if (m_RightStiltOutline != null)
		{
			m_RightStiltOutline.SetActive(active: false);
		}
		foreach (Outline middleStiltOutline in m_MiddleStiltOutlines)
		{
			middleStiltOutline.SetActive(active: false);
		}
	}

	private void UpdateStiltsOutline()
	{
		if (m_LeftStiltOutline != null && m_Poles.Count > 0)
		{
			UpdateStiltOutline(m_LeftStiltOutline, m_Poles[0]);
		}
		if (m_RightStiltOutline != null && m_Poles.Count > 1)
		{
			UpdateStiltOutline(m_RightStiltOutline, m_Poles[1]);
		}
		for (int i = 0; i < m_MiddleStiltOutlines.Count; i++)
		{
			Outline outline = m_MiddleStiltOutlines[i];
			outline.SetActive(active: false);
			if (i < GetNumMiddlePoles() && 2 + i < m_Poles.Count)
			{
				UpdateStiltOutline(outline, m_Poles[2 + i]);
			}
		}
	}

	private void UpdateStiltOutline(Outline outline, MeshRenderer pole)
	{
		_ = m_Width / (float)(GetNumMiddlePoles() + 1);
		_ = m_Width / 2f;
		float num = POLE_WIDTH * POLE_SCALE_X;
		float num2 = Platforms.THICKNESS / 2f;
		outline.SetActive(active: false);
		Vector3 vector = pole.transform.position + new Vector3((0f - num) / 2f, m_Flipped ? num2 : 0f, 0f);
		Vector3 vector2 = vector + new Vector3(num, m_Height - num2, 0f);
		m_TempPointsBuffer.Clear();
		Vector3 item = vector2 - new Vector3(num, 0f, 0f);
		Vector3 item2 = vector2;
		Vector3 item3 = vector;
		Vector3 item4 = vector + new Vector3(num, 0f, 0f);
		if (m_Flipped)
		{
			m_TempPointsBuffer.Add(item3);
			m_TempPointsBuffer.Add(item);
			m_TempPointsBuffer.Add(item2);
			m_TempPointsBuffer.Add(item4);
		}
		else
		{
			m_TempPointsBuffer.Add(item);
			m_TempPointsBuffer.Add(item3);
			m_TempPointsBuffer.Add(item4);
			m_TempPointsBuffer.Add(item2);
		}
		m_SandboxItem.UpdateOutlinePoints(outline, m_TempPointsBuffer);
	}

	public void AddToSimulation()
	{
		float num = 0.2f;
		float num2 = m_Width - num;
		float num3 = m_Height - num / 2f;
		Node node = Spline.AddPhysicsNode(base.transform.position - new Vector3(num2 / 2f, 0f, 0f));
		Node node2 = Spline.AddPhysicsNode(base.transform.position + new Vector3(num2 / 2f, 0f, 0f));
		Spline.AddPhysicsEdge(node, node2);
		if (m_Solid && !Mathf.Approximately(num3, 0f))
		{
			Node node3 = Spline.AddPhysicsNode(node.transform.position + new Vector3(0f, m_Flipped ? num3 : (0f - num3), 0f));
			Node b = Spline.AddPhysicsNode(node2.transform.position + new Vector3(0f, m_Flipped ? num3 : (0f - num3), 0f));
			Spline.AddPhysicsEdge(node, node3);
			Spline.AddPhysicsEdge(node2, b);
			Spline.AddPhysicsEdge(node3, b);
		}
	}

	private void RefreshCollider()
	{
		if (Mathf.Approximately(m_Height, 0f) || !m_Solid)
		{
			m_Collider.transform.localScale = new Vector3(m_Width, 0.2f, 1f) + new Vector3(0.2f, 0.2f, 0f);
			return;
		}
		m_Collider.transform.localScale = new Vector3(m_Width, m_Height, 1f) + new Vector3(0.2f, 0.2f, 0f);
		float num = m_Height / 2f + Platforms.THICKNESS / 2f;
		m_Collider.transform.localPosition = new Vector3(m_Collider.transform.localPosition.x, m_Flipped ? (0f - num) : num, m_Collider.transform.localPosition.z);
	}

	private void PositionPlanks()
	{
		int num = Mathf.RoundToInt(m_Width / PLANK_WIDTH);
		for (int i = m_Planks.Count; i < num; i++)
		{
			GameObject gameObject = InstantiatePlank(GetPlanksParent());
			if (gameObject != null)
			{
				m_Planks.Add(gameObject.GetComponent<MeshRenderer>());
			}
		}
		float num2 = (m_Width - (float)num * PLANK_WIDTH) / (float)num / PLANK_WIDTH;
		float num3 = PLANK_WIDTH * (1f + num2);
		Vector3 localScale = new Vector3(1f + num2, 2.1f, PLANK_ZSCALE);
		for (int j = 0; j < m_Planks.Count; j++)
		{
			m_Planks[j].gameObject.SetActive(j < num);
			m_Planks[j].transform.localScale = localScale;
		}
		float num4 = GetPlanksParent().transform.position.x - m_Width / 2f + num3 / 2f;
		float y = GetPlanksParent().transform.position.y - 0.0001f;
		float z = 0f;
		for (int k = 0; k < num; k++)
		{
			m_Planks[k].transform.position = new Vector3(num4, y, z);
			num4 += num3;
		}
	}

	private void PositionPoles()
	{
		int num = 2 + Mathf.FloorToInt(m_Width / 6f);
		for (int i = m_Poles.Count; i < num; i++)
		{
			GameObject gameObject = InstantiatePole(m_PolesParent);
			if (gameObject != null)
			{
				m_Poles.Add(gameObject.GetComponent<MeshRenderer>());
			}
		}
		Vector3 localScale = new Vector3(POLE_SCALE_X, m_Height, 0.99f);
		for (int j = 0; j < m_Poles.Count; j++)
		{
			m_Poles[j].gameObject.SetActive(j < num);
			m_Poles[j].transform.localScale = localScale;
		}
		float num2 = POLE_WIDTH * POLE_SCALE_X;
		float num3 = m_PolesParent.transform.position.x - m_Width / 2f + num2 / 2f;
		float y = (m_Flipped ? m_PolesParent.transform.position.y : (m_PolesParent.transform.position.y - m_Height));
		float z = 0f;
		m_Poles[0].transform.position = new Vector3(num3 + 0.01f, y, z);
		num3 = m_PolesParent.transform.position.x + m_Width / 2f - num2 / 2f;
		m_Poles[1].transform.position = new Vector3(num3 - 0.01f, y, z);
		int num4 = num - 2;
		if (num4 > 0)
		{
			float num5 = m_Width / (float)(num4 + 1);
			for (int k = 0; k < num4; k++)
			{
				float num6 = m_PolesParent.transform.position.x - m_Width / 2f;
				m_Poles[2 + k].transform.position = new Vector3(num6 + num5 * (float)(k + 1), y, z);
			}
		}
	}

	private GameObject InstantiatePlank(Transform parent)
	{
		return UnityEngine.Object.Instantiate(m_PlankVariantPrefabs[UnityEngine.Random.Range(0, m_PlankVariantPrefabs.Length)], parent);
	}

	private GameObject InstantiatePole(Transform parent)
	{
		return UnityEngine.Object.Instantiate(m_PolePrefab, parent);
	}

	private int GetNumMiddlePoles()
	{
		int num = 0;
		for (int i = 0; i < m_Poles.Count; i++)
		{
			if (m_Poles[i].gameObject.activeSelf)
			{
				num++;
			}
		}
		return num - 2;
	}

	private Transform GetPlanksParent()
	{
		return m_PlanksParent;
	}
}
