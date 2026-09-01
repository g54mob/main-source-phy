using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class Rock : MonoBehaviour
{
	public Sprite m_Sprite;

	public MeshRenderer m_MeshRenderer;

	public SplineComputer m_OutlineSplineComputer;

	public PlaceableCollisionInfo m_CollisionInfo;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public bool m_UniformScale;

	[NonSerialized]
	public bool m_LockToBottom;

	private Outline m_Outline;

	private bool m_HasCreatedOutline;

	internal List<PolygonShape> m_PolygonShapes = new List<PolygonShape>();

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
	}

	private void Start()
	{
		if (!Rocks.m_Rocks.Contains(this))
		{
			Rocks.m_Rocks.Add(this);
		}
	}

	private void OnDestroy()
	{
		if (Rocks.m_Rocks.Contains(this))
		{
			Rocks.m_Rocks.Remove(this);
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

	public void Flip(bool flipped)
	{
		m_MeshRenderer.transform.localScale = new Vector3(flipped ? (0f - Mathf.Abs(m_MeshRenderer.transform.localScale.x)) : Mathf.Abs(m_MeshRenderer.transform.localScale.x), m_MeshRenderer.transform.localScale.y, m_MeshRenderer.transform.localScale.z);
		m_OutlineSplineComputer.transform.localScale = new Vector3(flipped ? (0f - Mathf.Abs(m_OutlineSplineComputer.transform.localScale.x)) : Mathf.Abs(m_OutlineSplineComputer.transform.localScale.x), m_OutlineSplineComputer.transform.localScale.y, m_OutlineSplineComputer.transform.localScale.z);
		m_CollisionInfo.isFlipped = flipped;
		UpdatePolygonShapes();
	}

	public void UpdateOutline()
	{
		m_MeshRenderer.gameObject.SetActive(GameStateManager.GetState() != GameState.SANDBOX);
		if (!m_HasCreatedOutline)
		{
			m_Outline = m_SandboxItem.m_OutlineGroup.CreateOutline(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox);
			m_HasCreatedOutline = true;
			m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		if (m_SandboxItem.IsOutlineDirty())
		{
			m_SandboxItem.UpdateOutlineFromSpline(m_Outline, m_OutlineSplineComputer);
			m_SandboxItem.SetOutlineDirty(dirty: false);
			m_Outline.m_VectorLine.Draw3DAuto();
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

	public Rock Duplicate(GameObject prefab, Vector3 offset)
	{
		Rock rock = Rocks.CreateRock(prefab, base.transform.position, Quaternion.identity);
		if (!rock)
		{
			return null;
		}
		rock.transform.localScale = base.transform.localScale;
		if (m_CollisionInfo.isFlipped != rock.m_CollisionInfo.isFlipped)
		{
			rock.Flip(m_CollisionInfo.isFlipped);
		}
		rock.transform.position += offset;
		rock.m_LockToBottom = m_LockToBottom;
		rock.m_UniformScale = m_UniformScale;
		rock.UpdatePolygonShapes();
		return rock;
	}

	public void UpdateShaderProperties(bool buildMode)
	{
		m_MaterialPropertyBlock.SetFloat(ShaderVariables_SimpleLitCollidable.BUILD_MODE_SHADER_ID, buildMode ? 1f : 0f);
		m_MaterialPropertyBlock.SetColor(ShaderVariables_SimpleLitCollidable.BUILD_MODE_TINT_SHADER_ID, PostFX.m_Instance.m_BuildModeCollideTint);
		m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}
}
