using System.Collections.Generic;
using UnityEngine;

public class PointOfView
{
	public Vector3 m_Pivot;

	public Vector3 m_Pos;

	public Quaternion m_Rot;

	public float m_Yaw;

	public float m_Pitch;

	public float m_OrthographicsSize;

	public PointOfViewType m_Type;

	private float DEFAULT_ORTHOGRAPHIC_SIZE = 6f;

	public PointOfView(PointOfViewType type, Vector3 pivot, float yaw, float pitch)
	{
		m_Type = type;
		m_Yaw = yaw;
		m_Pitch = pitch;
		m_OrthographicsSize = DEFAULT_ORTHOGRAPHIC_SIZE;
		SetPivot(pivot);
	}

	public void UpdateForAngles(float yaw, float pitch)
	{
		m_Yaw = yaw;
		m_Pitch = pitch;
		float num = ((IsSimulationView() && GameStateManager.GetState() == GameState.SIM) ? PointsOfView.EXTRA_Y_PIVOT_OFFSET_FOR_SIM : 0f);
		Vector3 pivot = PointsOfView.CalculatePivot() + new Vector3(0f, GameSettings.PivotOffsetY() + num, 0f);
		SetPivot(pivot);
	}

	public void SetPivot(Vector3 pivot)
	{
		m_Pivot = pivot;
		m_Pos = CalculateCamPosBasedOnPivot();
		m_Rot = CalculateCamRotBasedOnPivot();
	}

	public void CopyFrom(PointOfView source)
	{
		m_Yaw = source.m_Yaw;
		m_Pitch = source.m_Pitch;
		m_Pos = source.m_Pos;
		m_Rot = source.m_Rot;
		m_Pivot = source.m_Pivot;
		m_OrthographicsSize = source.m_OrthographicsSize;
	}

	public bool Is2D()
	{
		if (m_Rot.eulerAngles.x <= 0.01001f)
		{
			return Mathf.Abs(m_Rot.eulerAngles.y) < 0.01f;
		}
		return false;
	}

	public void FrameObjects(string levelID)
	{
		Vector3 position = Cameras.MainCamera().transform.position;
		Quaternion rotation = Cameras.MainCamera().transform.rotation;
		float orthographicSize = Cameras.GetOrthographicSize();
		_ = m_Pivot;
		bool flag = m_Type == PointOfViewType.BUILD || m_Type == PointOfViewType.BUILD_CUSTOM || Game.m_TakingScreenshotForAutoSave || PolyTwitch.m_IsTakingScreenshot || DumpReplays.m_Dumping;
		float num = ((IsSimulationView() && GameStateManager.GetState() == GameState.SIM && !flag) ? PointsOfView.EXTRA_Y_PIVOT_OFFSET_FOR_SIM : 0f);
		SetPivot(PointsOfView.CalculatePivot() + new Vector3(0f, GameSettings.PivotOffsetY() + num, 0f));
		PointsOfView.SetCameraImmediate(this);
		List<Bounds> boundsList = new List<Bounds>();
		AddVehiclesToBoundsList(boundsList);
		AddVictoryFlagsToBoundsList(boundsList);
		if (flag && (float)Screen.width / (float)Screen.height > 2f)
		{
			AddTerrainIslandsToBoundsList(boundsList, Theme.m_Instance.m_ThemeStub.m_TerrainBoundsScaleX * 0.8f);
		}
		if ((IsSimulationView() && !PolyTwitch.m_IsTakingScreenshot && !Game.m_TakingScreenshotForAutoSave && !DumpReplays.m_Dumping) || DumpPreviewImages.m_Dumping)
		{
			if (!DumpPreviewImages.m_Dumping && !Theme.m_Instance.m_ThemeStub.m_IgnoreDecor)
			{
				AddDecorToBoundsList(boundsList);
			}
			float terrainScaleX = (DumpPreviewImages.m_Dumping ? 0.5f : Theme.m_Instance.m_ThemeStub.m_TerrainBoundsScaleX);
			AddTerrainIslandsToBoundsList(boundsList, terrainScaleX);
		}
		AddBookEndSpawnPointsToBoundsList(boundsList);
		AddAnchorsToBoundsList(boundsList);
		AddFoundationsToBoundsList(boundsList);
		Cameras.AdjustOrthographicSizeToFrameBounds(boundsList, (IsSimulationView() && !flag) ? 0f : 1.5f);
		if (levelID == "3031598950")
		{
			Cameras.SetOrthographicSize(21.931f);
		}
		if (m_Type == PointOfViewType.BUILD && levelID == "203")
		{
			Cameras.MainCamera().transform.position += new Vector3(15.8f, 11.1f, 0f);
		}
		m_Pos = Cameras.MainCamera().transform.position;
		m_OrthographicsSize = Cameras.GetOrthographicSize();
		_ = DumpReplays.m_Dumping;
		Cameras.MainCamera().transform.position = position;
		Cameras.MainCamera().transform.rotation = rotation;
		Cameras.SetOrthographicSize(orthographicSize);
	}

	private void AddBookendsToBounds(List<Bounds> boundsList)
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!rightTerrain || !leftTerrain)
		{
			Debug.LogWarningFormat("Could not find left and right terrains");
			return;
		}
		Bounds item = new Bounds(Vector3.zero, Vector3.zero);
		MeshRenderer[] componentsInChildren = rightTerrain.GetComponentsInChildren<MeshRenderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			item.Encapsulate(renderer.GetComponent<Renderer>().bounds);
		}
		componentsInChildren = leftTerrain.GetComponentsInChildren<MeshRenderer>();
		foreach (Renderer renderer2 in componentsInChildren)
		{
			item.Encapsulate(renderer2.GetComponent<Renderer>().bounds);
		}
		boundsList.Add(item);
	}

	internal static void AddVehiclesToBoundsList(List<Bounds> boundsList)
	{
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				Vector3 position = vehicle.transform.position;
				Quaternion rotation = vehicle.transform.rotation;
				vehicle.transform.position = vehicle.m_SpawnPos;
				vehicle.transform.rotation = vehicle.m_SpawnRot;
				boundsList.Add(vehicle.ComputeBounds());
				vehicle.transform.position = position;
				vehicle.transform.rotation = rotation;
			}
		}
	}

	internal static void AddTerrainIslandsToBoundsList(List<Bounds> boundsList, float terrainScaleX)
	{
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			Vector3 vector = new Vector3(terrain.m_MeshRenderer.bounds.size.x * terrainScaleX, PointsOfView.TERRAIN_Y_MAX_SIZE_FOR_FRAMING, terrain.m_MeshRenderer.bounds.size.z);
			float height = terrain.GetHeight();
			if (!(TerrainIslands.GetMaxHeight() - height > 20f))
			{
				Vector3 vector2 = new Vector3(terrain.m_BoxCollider.transform.position.x, Mathf.Max(0.5f, height - PointsOfView.TERRAIN_Y_MAX_SIZE_FOR_FRAMING), 0f);
				Bounds item = new Bounds(vector2, new Vector3(0.1f, 0.1f, 0.1f));
				item.Encapsulate(vector2 + new Vector3(vector.x / 2f, vector.y / 2f, 0f));
				item.Encapsulate(vector2 + new Vector3((0f - vector.x) / 2f, vector.y / 2f, 0f));
				item.Encapsulate(vector2 + new Vector3(vector.x / 2f, vector.y / 2f, 0f));
				boundsList.Add(item);
			}
		}
	}

	internal static void AddVictoryFlagsToBoundsList(List<Bounds> boundsList)
	{
		foreach (VehicleStopTrigger trigger in VehicleStopTriggers.m_Triggers)
		{
			if (trigger.gameObject.activeInHierarchy)
			{
				boundsList.Add(trigger.m_Collider.bounds);
			}
		}
	}

	internal static void AddDecorToBoundsList(List<Bounds> boundsList)
	{
		float num = TerrainIslands.GetMaxHeight() + 20f;
		foreach (Decor decor in Decors.m_Decors)
		{
			if (!decor.gameObject.activeInHierarchy)
			{
				continue;
			}
			MeshRenderer[] meshRenderers = decor.m_MeshRenderers;
			foreach (MeshRenderer meshRenderer in meshRenderers)
			{
				if (meshRenderer.bounds.size.y < 20f && meshRenderer.bounds.center.y < num && meshRenderer.bounds.center.y > -10f)
				{
					boundsList.Add(meshRenderer.bounds);
				}
			}
		}
	}

	internal static void AddBookEndSpawnPointsToBoundsList(List<Bounds> boundsList)
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!rightTerrain || !leftTerrain)
		{
			Debug.LogWarningFormat("Could not find left and right terrains to determine camera bounds");
			return;
		}
		if (!leftTerrain.m_SpawnPoint)
		{
			Debug.LogWarningFormat("Left terrain requires a TerrainIslandSpawnPoint");
			return;
		}
		if (!rightTerrain.m_SpawnPoint)
		{
			Debug.LogWarningFormat("Right terrain requires a TerrainIslandSpawnPoint");
			return;
		}
		boundsList.Add(new Bounds(leftTerrain.m_SpawnPoint.transform.position, new Vector3(0.1f, 0.1f, 0.1f)));
		boundsList.Add(new Bounds(rightTerrain.m_SpawnPoint.transform.position, new Vector3(0.1f, 0.1f, 0.1f)));
	}

	internal static void AddAnchorsToBoundsList(List<Bounds> boundsList)
	{
		Vector2 vector = new Vector3(0.1f, 0.1f, 0.1f);
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				boundsList.Add(new Bounds(joint.transform.position, vector));
			}
		}
	}

	internal static void AddAllJointsToBoundsList_UsedByAdrian(List<Bounds> boundsList)
	{
		Vector2 vector = new Vector3(0.1f, 0.1f, 0.1f);
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				boundsList.Add(new Bounds(joint.transform.position, vector));
			}
		}
	}

	internal static void AddFoundationsToBoundsList(List<Bounds> boundsList)
	{
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				float totalHeight = bridgePillar.GetTotalHeight();
				Bounds item = new Bounds(bridgePillar.transform.position + new Vector3(0f, totalHeight / 2f, 0f), new Vector2(1.43f, totalHeight));
				boundsList.Add(item);
			}
		}
	}

	private Vector3 CalculateCamPosBasedOnPivot()
	{
		Vector3 vector = Quaternion.Euler(m_Pitch, m_Yaw, 0f) * -Vector3.forward;
		return m_Pivot + vector.normalized * GameSettings.CamDistFromPivot();
	}

	private Quaternion CalculateCamRotBasedOnPivot()
	{
		return Quaternion.LookRotation((m_Pivot - m_Pos).normalized);
	}

	private bool IsSimulationView()
	{
		if (m_Type != PointOfViewType.SIM_CENTER && m_Type != PointOfViewType.SIM_CENTER_PITCHED_DOWN && m_Type != PointOfViewType.SIM_CUSTOM && m_Type != PointOfViewType.SIM_LEFT && m_Type != PointOfViewType.SIM_RIGHT)
		{
			return m_Type == PointOfViewType.PHOTO;
		}
		return true;
	}
}
