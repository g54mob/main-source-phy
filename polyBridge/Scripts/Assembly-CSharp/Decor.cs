using System;
using EPOOutline;
using UnityEngine;

public class Decor : MonoBehaviour
{
	public bool m_UsesCuttingPlane;

	public bool m_StickToGround;

	public bool m_AlignTopWithHighestTerrain;

	public float m_MeshHeight;

	public float m_DefaultZOffset;

	[NonSerialized]
	public Outlinable m_Outline;

	[NonSerialized]
	public MeshRenderer[] m_MeshRenderers;

	[NonSerialized]
	public string m_Id;

	[NonSerialized]
	public string m_ModId;

	[NonSerialized]
	public float m_HeadingRotationDegrees;

	[NonSerialized]
	public float m_PitchRotationDegrees;

	[NonSerialized]
	public float m_RollRotationDegrees;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public bool m_ShowInBuildMode;

	[NonSerialized]
	public bool m_UniformScale;

	[NonSerialized]
	public bool m_NoSave;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private void Awake()
	{
		m_MeshRenderers = GetComponentsInChildren<MeshRenderer>();
		m_SandboxItem = GetComponent<SandboxItem>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		SetLayerOnAllRecursive(base.gameObject, Utils.DECOR_LAYER);
	}

	private static void SetLayerOnAllRecursive(GameObject obj, int layer)
	{
		if (obj.layer != Utils.TRANSPARENT_FX_LAYER)
		{
			obj.layer = layer;
		}
		foreach (Transform item in obj.transform)
		{
			SetLayerOnAllRecursive(item.gameObject, layer);
		}
	}

	private void Start()
	{
		if (!Decors.m_Decors.Contains(this))
		{
			Decors.m_Decors.Add(this);
		}
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ParticleSystem.MainModule main = componentsInChildren[i].main;
			main.useUnscaledTime = true;
		}
	}

	private void OnDestroy()
	{
		if (Decors.m_Decors.Contains(this))
		{
			Decors.m_Decors.Remove(this);
		}
	}

	public void SetVisibility(GameState gameState)
	{
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			if (gameState == GameState.BUILD)
			{
				meshRenderer.gameObject.SetActive(m_ShowInBuildMode);
			}
			else
			{
				meshRenderer.gameObject.SetActive(gameState != GameState.SANDBOX || Profiles.m_ActiveProfile.m_ShowDecor);
			}
		}
	}

	public void UpdateShaderProperties(bool buildMode, MeshRenderer cuttingPlane)
	{
		if (m_UsesCuttingPlane && cuttingPlane != null)
		{
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_NORMAL_1, cuttingPlane.transform.up);
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_POSITION_1, cuttingPlane.transform.position);
		}
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_Common.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_Common.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeNoCollideTint);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void Hide(bool hide)
	{
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].gameObject.SetActive(!hide);
		}
	}

	public string GetId()
	{
		return m_Id;
	}

	public DecorStub GetStub()
	{
		return DecorStubs.GetStubFromId(m_Id);
	}

	public string GetLocalizedName()
	{
		DecorStub stub = GetStub();
		if (!(stub != null))
		{
			return string.Empty;
		}
		return Localize.Get(stub.m_DisplayNameLocID);
	}

	public Decor Duplicate(GameObject prefab, string id, string modId, Vector3 offset)
	{
		Decor decor = Decors.Create(prefab, id, modId, base.transform.position, Quaternion.identity);
		if (!decor)
		{
			return null;
		}
		DecorProxy decorProxy = new DecorProxy(this);
		decorProxy.m_Pos += offset;
		Decors.ApplyProxyToDecor(decor, decorProxy);
		decor.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
		return decor;
	}

	public void AdjustPlacementPosition()
	{
		float num = (m_StickToGround ? (0f - base.transform.position.y) : 0f);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num, base.transform.position.z + m_DefaultZOffset);
	}

	public float GetDuplicateOffset()
	{
		float num = 0f;
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			if (meshRenderer.bounds.size.x > num)
			{
				num = meshRenderer.bounds.size.x;
			}
		}
		return num;
	}
}
