using Dreamteck.Splines;
using Poly.Collide.Unity;
using UnityEngine;

public class TerrainRig : MonoBehaviour
{
	[Header("Terrain")]
	public Object m_FBX;

	public float m_Height;

	[Header("Options")]
	public bool m_IsBookEnd = true;

	public bool m_UsePlaceholderMaterial;

	[Header("Assets")]
	public Material m_PlaceholderMaterial;

	public Material m_SecondPassMaterial;

	public GameObject m_CuttingPlanePrefab;

	public GameObject m_OutlinePrefab;

	private readonly float DEFAULT_TERRAIN_HEIGHT = 20.1f;

	private readonly string CUTTING_PLANE_NAME = "CuttingPlane";

	private readonly string OUTLINE_NAME = "Outline";

	private SplinePoint[] m_OriginalOutlinePoints;

	public void Rig()
	{
		if (Mathf.Approximately(m_Height, 0f))
		{
			m_Height = DEFAULT_TERRAIN_HEIGHT;
		}
		StoreOriginalOutlinePoints();
		Clean();
		AddSandboxItem();
		AddTerrain();
	}

	private void StoreOriginalOutlinePoints()
	{
		TerrainIsland component = base.gameObject.GetComponent<TerrainIsland>();
		if (!(component == null) && !(component.m_OutlineSplineComputer == null))
		{
			m_OriginalOutlinePoints = component.m_OutlineSplineComputer.GetPoints(SplineComputer.Space.Local);
		}
	}

	private void Clean()
	{
		SandboxItem component = base.gameObject.GetComponent<SandboxItem>();
		if (component != null)
		{
			Object.DestroyImmediate(component);
		}
		TerrainIsland component2 = base.gameObject.GetComponent<TerrainIsland>();
		if (component2 != null)
		{
			Object.DestroyImmediate(component2);
		}
		CuttingController_OnePlane component3 = base.gameObject.GetComponent<CuttingController_OnePlane>();
		if (component3 != null)
		{
			Object.DestroyImmediate(component3);
		}
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Object.DestroyImmediate(base.transform.GetChild(i).gameObject);
		}
	}

	private void AddSandboxItem()
	{
		SandboxItem sandboxItem = base.gameObject.GetComponent<SandboxItem>();
		if (sandboxItem == null)
		{
			sandboxItem = base.gameObject.AddComponent<SandboxItem>();
		}
		if (sandboxItem != null)
		{
			sandboxItem.m_Type = SandboxItemType.TERRAIN;
		}
	}

	private void AddTerrain()
	{
		TerrainIsland terrainIsland = base.gameObject.GetComponent<TerrainIsland>();
		if (terrainIsland == null)
		{
			terrainIsland = base.gameObject.AddComponent<TerrainIsland>();
		}
		if (terrainIsland != null)
		{
			GameObject gameObject = ConfigureMainMesh(terrainIsland.gameObject);
			GameObject secondPassMesh = ConfigureSecondPassMesh(gameObject);
			ConfigureTerrainIsland(terrainIsland, gameObject, secondPassMesh);
			if (m_IsBookEnd)
			{
				AddCuttingPlane(terrainIsland);
			}
		}
	}

	private void ConfigureTerrainIsland(TerrainIsland terrainIsland, GameObject mainMesh, GameObject secondPassMesh)
	{
		terrainIsland.transform.position = Vector3.zero;
		terrainIsland.transform.rotation = Quaternion.identity;
		terrainIsland.m_TerrainIslandType = ((!m_IsBookEnd) ? TerrainIslandType.Middle : TerrainIslandType.Bookend);
		terrainIsland.m_MeshHeight = m_Height;
		terrainIsland.m_MeshRenderer = mainMesh.GetComponent<MeshRenderer>();
		terrainIsland.m_MeshFilter = mainMesh.GetComponent<MeshFilter>();
		terrainIsland.m_MeshFilterSecondPass = secondPassMesh.GetComponent<MeshFilter>();
		if (m_IsBookEnd)
		{
			terrainIsland.m_SpawnPoint = AddDefaultSpawnPoint(mainMesh);
		}
		terrainIsland.m_BoxCollider = AddBoxCollider(terrainIsland.gameObject, terrainIsland.m_MeshRenderer);
		GameObject gameObject = AddOutline(mainMesh);
		terrainIsland.m_OutlineSplineComputer = gameObject.GetComponent<SplineComputer>();
		terrainIsland.m_CollisionInfoNew = gameObject.GetComponent<PlaceableCollisionInfo>();
		if ((m_OriginalOutlinePoints != null) & (m_OriginalOutlinePoints.Length != 0))
		{
			terrainIsland.m_OutlineSplineComputer.SetPoints(m_OriginalOutlinePoints, SplineComputer.Space.Local);
			return;
		}
		SplinePoint[] points = terrainIsland.m_OutlineSplineComputer.GetPoints(SplineComputer.Space.Local);
		points[0].position = new Vector3(0f - terrainIsland.m_BoxCollider.size.x, m_Height, 0f);
		points[1].position = new Vector3(0f, m_Height, 0f);
		points[2].position = new Vector3(0f, 0f, 0f);
		points[3].position = new Vector3(0f - terrainIsland.m_BoxCollider.size.x, 0f, 0f);
		terrainIsland.m_OutlineSplineComputer.SetPoints(points, SplineComputer.Space.Local);
	}

	private void AddCuttingPlane(TerrainIsland terrainIsland)
	{
		GameObject gameObject = LocateCuttingPlane(terrainIsland.transform);
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(m_CuttingPlanePrefab);
		}
		gameObject.name = CUTTING_PLANE_NAME;
		gameObject.transform.parent = terrainIsland.transform;
		gameObject.layer = Utils.DEFAULT_LAYER;
		if (terrainIsland.gameObject.GetComponent<CuttingController_OnePlane>() == null)
		{
			terrainIsland.gameObject.AddComponent<CuttingController_OnePlane>();
		}
		CuttingController_OnePlane component = terrainIsland.gameObject.GetComponent<CuttingController_OnePlane>();
		component.m_Renderer = terrainIsland.m_MeshRenderer;
		component.m_Plane = gameObject;
		component.enabled = false;
	}

	private BoxCollider AddBoxCollider(GameObject root, MeshRenderer mainMeshRenderer)
	{
		GameObject gameObject = LocateBoxCollider(root.transform);
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.AddComponent<BoxCollider>();
		}
		gameObject.name = "BoxCollider";
		gameObject.transform.parent = root.transform;
		gameObject.layer = Utils.TERRAIN_LAYER;
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		component.size = new Vector3(mainMeshRenderer.bounds.size.x, m_Height, mainMeshRenderer.bounds.size.z);
		component.center = new Vector3(mainMeshRenderer.bounds.center.x, m_Height / 2f, mainMeshRenderer.bounds.center.z);
		return component;
	}

	private TerrainIslandSpawnPoint AddDefaultSpawnPoint(GameObject root)
	{
		GameObject gameObject = LocateDefaultSpawnPoint(root.transform);
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.AddComponent<TerrainIslandSpawnPoint>();
		}
		gameObject.name = "DefaultSpawnPoint";
		gameObject.transform.parent = root.transform;
		gameObject.layer = Utils.TERRAIN_LAYER;
		gameObject.transform.localPosition = new Vector3(-5f, m_Height, 0f);
		return gameObject.GetComponent<TerrainIslandSpawnPoint>();
	}

	private GameObject AddOutline(GameObject root)
	{
		GameObject gameObject = LocateOutline(root.transform);
		if (gameObject == null)
		{
			gameObject = Object.Instantiate(m_OutlinePrefab);
		}
		gameObject.name = OUTLINE_NAME;
		gameObject.transform.parent = root.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.layer = Utils.TERRAIN_LAYER;
		return gameObject;
	}

	private GameObject ConfigureMainMesh(GameObject root)
	{
		return LocateMainMeshGameObject(root.transform);
	}

	private GameObject ConfigureSecondPassMesh(GameObject root)
	{
		return LocateSecondPassMeshGameObject(root.transform);
	}

	private void ReplaceMaterials(MeshRenderer meshRenderer, Material withMaterial)
	{
		if (meshRenderer != null)
		{
			Material[] sharedMaterials = meshRenderer.sharedMaterials;
			for (int i = 0; i < sharedMaterials.Length; i++)
			{
				sharedMaterials[i] = withMaterial;
			}
			meshRenderer.sharedMaterials = sharedMaterials;
		}
	}

	private GameObject LocateMainMeshGameObject(Transform root)
	{
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.gameObject.layer == Utils.TERRAIN_LAYER && child.gameObject.GetComponent<MeshRenderer>() != null)
			{
				return child.gameObject;
			}
		}
		return null;
	}

	private GameObject LocateSecondPassMeshGameObject(Transform root)
	{
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.gameObject.layer == Utils.FOREGROUND_LAYER && child.gameObject.GetComponent<MeshRenderer>() != null)
			{
				return child.gameObject;
			}
		}
		return null;
	}

	private GameObject LocateDefaultSpawnPoint(Transform root)
	{
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.gameObject.GetComponent<TerrainIslandSpawnPoint>() != null)
			{
				return child.gameObject;
			}
		}
		return null;
	}

	private GameObject LocateBoxCollider(Transform root)
	{
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.gameObject.GetComponent<BoxCollider>() != null)
			{
				return child.gameObject;
			}
		}
		return null;
	}

	private GameObject LocateCuttingPlane(Transform root)
	{
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.gameObject.name == CUTTING_PLANE_NAME)
			{
				return child.gameObject;
			}
		}
		return null;
	}

	private GameObject LocateOutline(Transform root)
	{
		for (int i = 0; i < root.childCount; i++)
		{
			Transform child = root.GetChild(i);
			if (child.gameObject.GetComponent<SplineComputer>() != null)
			{
				return child.gameObject;
			}
		}
		return null;
	}
}
