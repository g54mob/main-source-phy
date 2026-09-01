using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
	public Sprite m_Sprite;

	public MeshRenderer m_MeshRenderer;

	public MeshCollider m_MeshCollider;

	public PlaceableCollisionInfo m_CollisionInfo;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public Vector3 m_OriginalScale;

	private List<Outline> m_Outlines = new List<Outline>();

	private bool m_HasCreatedOutlines;

	private SplineComputer[] m_CollisionSplines;

	internal List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_CollisionSplines = m_CollisionInfo.GetComponents<SplineComputer>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_OriginalScale = base.transform.localScale;
	}

	private void Start()
	{
		if (!FlyingObjects.m_FlyingObjects.Contains(this))
		{
			FlyingObjects.m_FlyingObjects.Add(this);
		}
	}

	private void OnDestroy()
	{
		if (FlyingObjects.m_FlyingObjects.Contains(this))
		{
			FlyingObjects.m_FlyingObjects.Remove(this);
		}
	}

	public void DisableOutline()
	{
		m_SandboxItem.m_OutlineGroup.DisableOutline();
	}

	public void EnableMeshRendering()
	{
		m_MeshRenderer.gameObject.SetActive(value: true);
	}

	public void UpdateOutline()
	{
		m_MeshRenderer.gameObject.SetActive(GameStateManager.GetState() != GameState.SANDBOX);
		if (!m_HasCreatedOutlines)
		{
			SplineComputer[] collisionSplines = m_CollisionSplines;
			for (int i = 0; i < collisionSplines.Length; i++)
			{
				_ = collisionSplines[i];
				m_Outlines.Add(m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox));
			}
			for (int j = 0; j < m_Outlines.Count; j++)
			{
				m_Outlines[j].SetTextureScale(GameUI.m_Instance.m_OutlineTextureScale);
			}
			m_HasCreatedOutlines = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (m_SandboxItem.IsOutlineDirty())
		{
			for (int k = 0; k < m_Outlines.Count; k++)
			{
				m_SandboxItem.UpdateOutlineFromSpline(m_Outlines[k], m_CollisionSplines[k]);
				m_Outlines[k].SetTexture((GameStateManager.GetState() == GameState.BUILD) ? GameUI.m_Instance.m_OutlineTextureDashedBuildMode : GameUI.m_Instance.m_OutlineTextureSandbox);
				m_Outlines[k].m_VectorLine.Draw3DAuto();
			}
			m_SandboxItem.SetOutlineDirty(dirty: false);
		}
	}

	public void EnterBuildMode()
	{
		for (int i = 0; i < m_Outlines.Count; i++)
		{
			m_Outlines[i].SetTexture(GameUI.m_Instance.m_OutlineTextureDashedBuildMode);
		}
	}

	public void UpdatePolygonShapes()
	{
		m_PolygonShapes.Clear();
		m_PolygonShapes.AddRange(m_CollisionInfo.CreatePolygonShapes_ForBuildMode());
		m_SandboxItem.SetOutlineDirty(dirty: true);
	}

	public bool OverlapsPolygonShape(PolygonShape shape)
	{
		return Utils.PolygonShapeOverlapsShapes(shape, m_PolygonShapes);
	}

	public bool OverlapsRect(Rect rect)
	{
		PolygonShape polygonShape = PolygonShape.FromRect(rect.center, rect.size);
		polygonShape.radius = 0f;
		return Utils.PolygonShapeOverlapsShapes(polygonShape, m_PolygonShapes);
	}

	public FlyingObject Duplicate(GameObject prefab, Vector3 offset)
	{
		FlyingObject flyingObject = FlyingObjects.CreateFlyingObject(prefab, base.transform.position, Quaternion.identity);
		if (!flyingObject)
		{
			return null;
		}
		flyingObject.transform.localScale = base.transform.localScale;
		flyingObject.transform.position += offset;
		flyingObject.UpdatePolygonShapes();
		return flyingObject;
	}

	public void UpdateShaderProperties(bool buildMode)
	{
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_SimpleLitCollidable.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeCollideTint);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public float GetUniformScaleNormalized()
	{
		return base.transform.localScale.x / m_OriginalScale.x;
	}
}
