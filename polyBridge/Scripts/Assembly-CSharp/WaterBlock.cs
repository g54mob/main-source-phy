using System;
using UnityEngine;

public class WaterBlock : MonoBehaviour
{
	[Header("Combined Mesh Renderers")]
	public MeshRenderer m_SurfaceMeshRenderer;

	public MeshRenderer m_SidesMeshRenderer;

	public MeshRenderer m_FloorMeshRenderer;

	[Header("Base Meshes")]
	public MeshFilter m_SurfaceMesh;

	public MeshFilter m_SidesMesh;

	public MeshFilter m_FloorMesh;

	public MeshFilter m_LeftSideMesh;

	public MeshFilter m_RightSideMesh;

	[Header("Collision")]
	public BoxCollider m_BoxCollider;

	[NonSerialized]
	public float m_Width;

	[NonSerialized]
	public float m_Height;

	[NonSerialized]
	public TerrainIsland m_LeftTerrain;

	[NonSerialized]
	public TerrainIsland m_RightTerrain;

	[NonSerialized]
	public bool m_LockPosition;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	private Mesh m_Mesh;

	private float m_LastBuiltWidth;

	private float m_LastBuiltHeight;

	private WaterGrid m_WaterGrid;

	private Texture2D m_RippleTexture;

	private Color[] m_RippleTextureBuffer;

	private int m_TextureWidth;

	private int m_TextureHeight;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private Camera m_mainCamera;

	private int m_depthHorizontalPropertyId;

	private float m_defaultDepthHorizontal;

	private Bounds m_cachedBounds;

	private void Awake()
	{
		m_LockPosition = false;
		m_Height = WaterBlocks.DEFAULT_HEIGHT;
		m_SandboxItem = GetComponent<SandboxItem>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_mainCamera = Camera.main;
		m_depthHorizontalPropertyId = Shader.PropertyToID("_DepthHorizontal");
		m_defaultDepthHorizontal = m_SurfaceMeshRenderer.material.GetFloat(m_depthHorizontalPropertyId);
		if (!WaterBlocks.m_WaterBlocks.Contains(this))
		{
			WaterBlocks.m_WaterBlocks.Add(this);
		}
	}

	private void Start()
	{
		m_FloorMeshRenderer.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		Vector3 eulerAngles = m_mainCamera.transform.localRotation.eulerAngles;
		if (eulerAngles.x < 5f)
		{
			float num = (5f - eulerAngles.x) / 5f;
			m_SurfaceMeshRenderer.material.SetFloat(m_depthHorizontalPropertyId, m_defaultDepthHorizontal + num * 3f);
		}
		else
		{
			m_SurfaceMeshRenderer.material.SetFloat(m_depthHorizontalPropertyId, m_defaultDepthHorizontal);
		}
	}

	public void StartSimulation()
	{
		m_cachedBounds = m_BoxCollider.bounds;
	}

	private void OnDestroy()
	{
		DestroyMeshInstances();
		if (WaterBlocks.m_WaterBlocks.Contains(this))
		{
			WaterBlocks.m_WaterBlocks.Remove(this);
		}
		if ((bool)m_RippleTexture)
		{
			UnityEngine.Object.Destroy(m_RippleTexture);
			m_RippleTexture = null;
		}
	}

	private void OnDisable()
	{
	}

	public void UpdateHeight(float goalHeight)
	{
		float num = goalHeight - m_Height;
		if (!Mathf.Approximately(num, 0f))
		{
			GameUI.m_Instance.m_SandboxEditWater.m_SliderHeight.m_SandboxInputField.AddHeight(base.gameObject, num);
		}
	}

	public void RefreshPosition()
	{
		m_Height = Mathf.Clamp(m_Height, WaterBlocks.MIN_HEIGHT, GetMaxHeight());
		base.transform.position = new Vector3(base.transform.position.x, m_Height / 2f, base.transform.position.z);
		m_SurfaceMeshRenderer.transform.position = new Vector3(base.transform.position.x - (float)Mathf.CeilToInt(m_Width) / 2f + 1f, m_Height - WaterBlockMesh.DEFAULT_HEIGHT, m_SurfaceMeshRenderer.transform.position.z);
		m_SidesMeshRenderer.transform.position = m_SurfaceMeshRenderer.transform.position;
		m_FloorMeshRenderer.transform.position = new Vector3(base.transform.position.x - (float)Mathf.CeilToInt(m_Width) / 2f + 1f, 0f, m_FloorMeshRenderer.transform.position.z);
		RefreshScale();
	}

	public void RefreshScale()
	{
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
			TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
			if ((bool)leftTerrain && (bool)rightTerrain)
			{
				float x = rightTerrain.transform.position.x - leftTerrain.transform.position.x;
				m_BoxCollider.transform.localScale = new Vector3(x, m_Height, m_BoxCollider.transform.localScale.z);
			}
			else
			{
				m_BoxCollider.transform.localScale = new Vector3(m_Width, m_Height, m_BoxCollider.transform.localScale.z);
			}
		}
		else
		{
			m_BoxCollider.transform.localScale = new Vector3(m_Width, m_Height, m_BoxCollider.transform.localScale.z);
		}
	}

	public void RebuildMesh()
	{
		if (!Mathf.Approximately(m_Height, m_LastBuiltHeight) || !Mathf.Approximately(m_Width, m_LastBuiltWidth))
		{
			m_SurfaceMeshRenderer.GetComponent<MeshFilter>().sharedMesh = WaterBlockMesh.Create(m_SurfaceMesh.mesh, null, null, m_Width, m_Height);
			m_SurfaceMeshRenderer.transform.position = new Vector3(m_SurfaceMeshRenderer.transform.position.x, m_Height - WaterBlockMesh.DEFAULT_HEIGHT, m_SurfaceMeshRenderer.transform.position.z);
			m_SidesMeshRenderer.GetComponent<MeshFilter>().sharedMesh = WaterBlockMesh.Create(m_SidesMesh.mesh, m_LeftSideMesh.mesh, m_RightSideMesh.mesh, m_Width, m_Height);
			m_SidesMeshRenderer.transform.position = new Vector3(m_SidesMeshRenderer.transform.position.x, m_Height - WaterBlockMesh.DEFAULT_HEIGHT, m_SidesMeshRenderer.transform.position.z);
			m_FloorMeshRenderer.GetComponent<MeshFilter>().sharedMesh = WaterBlockMesh.Create(m_FloorMesh.mesh, null, null, m_Width, m_Height);
			m_FloorMeshRenderer.transform.position = new Vector3(m_FloorMeshRenderer.transform.position.x, 0f, m_FloorMeshRenderer.transform.position.z);
			m_LastBuiltWidth = m_Width;
			m_LastBuiltHeight = m_Height;
		}
	}

	public float GetMaxHeight()
	{
		if (!m_LeftTerrain || !m_RightTerrain)
		{
			return m_Height;
		}
		float minHeight = TerrainIslands.GetMinHeight();
		return GameGrid.RoundToNearestGridSquareForced(Mathf.Max(WaterBlocks.MIN_HEIGHT, minHeight - WaterBlocks.MAX_DISTANCE_BELOW_TERRAIN));
	}

	public bool PositionInWater(Vector3 pos)
	{
		return m_cachedBounds.Contains(pos);
	}

	public void SetWaveHeight(float height)
	{
		m_MaterialPropertyBlock.SetFloat("_WaveHeight", height);
		m_SurfaceMeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		m_SidesMeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	public void CreateSplash(float x, float force)
	{
	}

	public void EnableMeshRenderers(bool enable)
	{
		Theme.m_Instance.m_WaterPlane.gameObject.SetActive(enable);
		m_SurfaceMeshRenderer.enabled = false;
		m_SidesMeshRenderer.enabled = false;
		m_FloorMeshRenderer.enabled = false;
	}

	public void SetScaleZ(float scale)
	{
		m_SurfaceMeshRenderer.transform.localScale = new Vector3(m_SurfaceMeshRenderer.transform.localScale.x, m_SurfaceMeshRenderer.transform.localScale.y, scale);
		m_SidesMeshRenderer.transform.localScale = new Vector3(m_SidesMeshRenderer.transform.localScale.x, m_SidesMeshRenderer.transform.localScale.y, scale);
		m_FloorMeshRenderer.transform.localScale = new Vector3(m_FloorMeshRenderer.transform.localScale.x, m_FloorMeshRenderer.transform.localScale.y, scale);
	}

	public void SetOffsetZ(float offset)
	{
		m_SurfaceMeshRenderer.transform.position = new Vector3(m_SurfaceMeshRenderer.transform.position.x, m_SurfaceMeshRenderer.transform.position.y, offset);
		m_SidesMeshRenderer.transform.position = new Vector3(m_SidesMeshRenderer.transform.position.x, m_SidesMeshRenderer.transform.position.y, offset);
		m_FloorMeshRenderer.transform.position = new Vector3(m_FloorMeshRenderer.transform.position.x, m_FloorMeshRenderer.transform.position.y, offset);
	}

	private WaterRuler InstantiateRuler()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_WaterRuler, GameUI.m_Instance.m_RulerText.transform);
		if (!gameObject)
		{
			return null;
		}
		return gameObject.GetComponent<WaterRuler>();
	}

	private void DestroyMeshInstances()
	{
		UnityEngine.Object.Destroy(m_SurfaceMeshRenderer.GetComponent<MeshFilter>().sharedMesh);
		UnityEngine.Object.Destroy(m_SidesMeshRenderer.GetComponent<MeshFilter>().sharedMesh);
		UnityEngine.Object.Destroy(m_FloorMeshRenderer.GetComponent<MeshFilter>().sharedMesh);
		UnityEngine.Object.Destroy(m_SurfaceMesh.sharedMesh);
		UnityEngine.Object.Destroy(m_SidesMesh.sharedMesh);
		UnityEngine.Object.Destroy(m_FloorMesh.sharedMesh);
		UnityEngine.Object.Destroy(m_LeftSideMesh.sharedMesh);
		UnityEngine.Object.Destroy(m_RightSideMesh.sharedMesh);
	}
}
