using System;
using UnityEngine;

public class Pillar : MonoBehaviour
{
	public GameObject m_AllMeshes;

	public MeshRenderer m_MeshRendererTop;

	public MeshRenderer m_MeshRendererMiddle;

	public MeshRenderer m_MeshRendererBottom;

	public BoxCollider m_BoxCollider;

	[NonSerialized]
	public float m_Height;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	private float DEFAULT_HEIGHT = 4f;

	private float m_DefaultMiddleHeight;

	private float m_CapHeight;

	private float m_MiddleScaleY = 1f;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_Height = DEFAULT_HEIGHT;
		m_DefaultMiddleHeight = m_MeshRendererMiddle.bounds.size.y;
		m_CapHeight = m_MeshRendererBottom.bounds.size.y;
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
	}

	private void Start()
	{
		if (!Pillars.m_Pillars.Contains(this))
		{
			Pillars.m_Pillars.Add(this);
		}
	}

	private void OnDestroy()
	{
		if (Pillars.m_Pillars.Contains(this))
		{
			Pillars.m_Pillars.Remove(this);
		}
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		m_AllMeshes.SetActive(value: true);
	}

	public void UpdateOutline()
	{
		m_AllMeshes.SetActive(GameStateManager.GetState() != GameState.SANDBOX);
		if (GameStateManager.GetState() != GameState.SANDBOX)
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
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (m_Outline != null && m_SandboxItem.IsOutlineDirty())
		{
			m_SandboxItem.UpdateOutlineFromBounds(m_Outline, m_BoxCollider.bounds);
			m_SandboxItem.SetOutlineDirty(dirty: false);
		}
	}

	public void UpdateShaderProperties(bool buildMode)
	{
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_SimpleLitCollidable.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeCustomShapeNoCollide);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.TILING_SHADER_ID, Vector2.one);
		m_MeshRendererTop.SetPropertyBlock(m_MaterialPropertyBlock);
		m_MeshRendererBottom.SetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.TILING_SHADER_ID, new Vector2(1f, m_MiddleScaleY));
		m_MeshRendererMiddle.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public void SetHeight(float height)
	{
		m_Height = height;
		RefreshMeshes();
		RefreshCollider();
		m_SandboxItem.m_OutlineGroup.ClearCachedSplinePoints();
		m_SandboxItem.SetOutlineDirty(dirty: true);
		UpdateShaderProperties(GameStateManager.GetState() == GameState.BUILD);
	}

	public Pillar Duplicate(GameObject prefab, Vector3 offset)
	{
		Pillar pillar = Pillars.CreatePillar(prefab, base.transform.position, Quaternion.identity);
		if (!pillar)
		{
			return null;
		}
		pillar.transform.localScale = base.transform.localScale;
		pillar.SetHeight(m_Height);
		pillar.transform.position += offset;
		return pillar;
	}

	private void RefreshMeshes()
	{
		float y = m_Height - DEFAULT_HEIGHT;
		m_MeshRendererTop.gameObject.transform.localPosition = new Vector3(m_MeshRendererTop.gameObject.transform.localPosition.x, y, m_MeshRendererTop.gameObject.transform.localPosition.z);
		float num = (m_Height - (DEFAULT_HEIGHT - m_DefaultMiddleHeight)) / m_DefaultMiddleHeight;
		m_MeshRendererMiddle.gameObject.transform.localScale = new Vector3(1f, num, 1f);
		float capHeight = m_CapHeight;
		float num2 = m_CapHeight * num;
		float y2 = capHeight - num2;
		m_MeshRendererMiddle.gameObject.transform.localPosition = new Vector3(m_MeshRendererMiddle.gameObject.transform.localPosition.x, y2, m_MeshRendererMiddle.gameObject.transform.localPosition.z);
		m_MiddleScaleY = num;
	}

	private void RefreshCollider()
	{
		m_BoxCollider.size = new Vector3(m_BoxCollider.size.x, m_Height, m_BoxCollider.size.z);
		m_BoxCollider.center = new Vector3(m_BoxCollider.center.x, m_Height / 2f, m_BoxCollider.center.z);
	}
}
