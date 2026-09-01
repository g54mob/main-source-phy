using System;
using System.Collections.Generic;
using Poly;
using Poly.Extension;
using Poly.Game;
using Poly.Math;
using UnityEngine;

public class PointsOfView
{
	public static Dictionary<PointOfViewType, PointOfView> m_PointsOfView = new Dictionary<PointOfViewType, PointOfView>();

	public static Vector3 m_Pivot;

	public static bool m_Locked2D = false;

	public static float TERRAIN_Y_MAX_SIZE_FOR_FRAMING = 5f;

	public static float EXTRA_Y_PIVOT_OFFSET_FOR_SIM = 3f;

	public static void OnLayoutLoaded(string levelID)
	{
		m_PointsOfView.Clear();
		m_Pivot = CalculatePivot() + new Vector3(0f, GameSettings.PivotOffsetY(), 0f);
		m_PointsOfView.Add(PointOfViewType.BUILD, new PointOfView(PointOfViewType.BUILD, m_Pivot, 0f, 0f));
		m_PointsOfView.Add(PointOfViewType.SIM_CENTER, new PointOfView(PointOfViewType.SIM_CENTER, m_Pivot, 0f, 0f));
		m_PointsOfView.Add(PointOfViewType.DECOR_TOP, new PointOfView(PointOfViewType.DECOR_TOP, m_Pivot, 0f, 89.99f));
		m_PointsOfView.Add(PointOfViewType.DECOR_CENTER, new PointOfView(PointOfViewType.DECOR_CENTER, m_Pivot, 0f, 0f));
		m_PointsOfView.Add(PointOfViewType.DECOR_CUSTOM, new PointOfView(PointOfViewType.DECOR_CUSTOM, Vector3.zero, 0f, 0f));
		m_PointsOfView.Add(PointOfViewType.SIM_CENTER_PITCHED_DOWN, new PointOfView(PointOfViewType.SIM_CENTER_PITCHED_DOWN, m_Pivot, GameSettings.CenterViewYaw(), GameSettings.CenterViewPitch()));
		m_PointsOfView.Add(PointOfViewType.SIM_RIGHT, new PointOfView(PointOfViewType.SIM_RIGHT, m_Pivot, 0f - GameSettings.AngleViewYaw(), GameSettings.AngleViewPitch()));
		m_PointsOfView.Add(PointOfViewType.SIM_LEFT, new PointOfView(PointOfViewType.SIM_LEFT, m_Pivot, GameSettings.AngleViewYaw(), GameSettings.AngleViewPitch()));
		m_PointsOfView.Add(PointOfViewType.BUILD_CUSTOM, new PointOfView(PointOfViewType.BUILD_CUSTOM, Vector3.zero, 0f, 0f));
		m_PointsOfView.Add(PointOfViewType.SIM_CUSTOM, new PointOfView(PointOfViewType.SIM_CUSTOM, Vector3.zero, 0f, 0f));
		m_PointsOfView.Add(PointOfViewType.PHOTO, new PointOfView(PointOfViewType.PHOTO, m_Pivot, GameSettings.AngleViewYaw(), GameSettings.AngleViewPitch()));
		FrameObjects(levelID);
	}

	public static void PanPivot(Vector3 initialClickPosition, bool force = false)
	{
		Vector3 vector = Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition());
		if (force || !Mathf.Approximately(initialClickPosition.x, vector.x) || !Mathf.Approximately(initialClickPosition.y, vector.y))
		{
			_ = Cameras.MainCamera().transform.position;
			Cameras.MainCamera().transform.position += initialClickPosition - vector;
			if (GameStateManager.GetState() == GameState.SIM && !Cameras.In2DMode())
			{
				GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
			}
			UpdatePivotBasedOnCamera();
			CameraControl.RegisterTransformUpdate();
		}
	}

	public static void SnapTo(PointOfViewType type)
	{
		RotateTo(type, 0f);
		UpdatePivotBasedOnCamera();
	}

	public static void RotateTo(PointOfViewType type, float durationSeconds)
	{
		PointOfView pointOfView = GetPointOfView(type);
		if (pointOfView != null)
		{
			if (DesiredPointOfViewIsTooDifferentFromCurrentView(pointOfView) || CameraAlreadyAtPointOfView(pointOfView))
			{
				durationSeconds = 0f;
			}
			if (Mathf.Approximately(durationSeconds, 0f))
			{
				SetCameraImmediate(pointOfView);
				CameraControl.RegisterTransformUpdate();
				GameStateCommonInput.m_RefreshClickPositionForPan = true;
			}
			else
			{
				CameraInterpolate.SlerpTo(pointOfView.m_Pivot, pointOfView.m_Pos, pointOfView.m_Rot, pointOfView.m_OrthographicsSize, durationSeconds, ease: true);
			}
		}
	}

	public static void UpdatePivotBasedOnCamera()
	{
		UpdatePivotBasedOnCameraDisplacement(Vector3.zero);
	}

	public static Vector3 UpdatePivotBasedOnCameraDisplacement(Vector3 displacement)
	{
		Vector3 pivot = m_Pivot;
		Vector3 averagePositionOfBookendSpawnPoints = TerrainIslands.GetAveragePositionOfBookendSpawnPoints();
		Plane plane = new Plane(Vector3.forward, averagePositionOfBookendSpawnPoints);
		Ray ray = new Ray(Cameras.MainCamera().transform.position + displacement - Cameras.MainCamera().transform.forward * GameSettings.CamDistFromPivot(), Cameras.MainCamera().transform.forward);
		float enter = 0f;
		bool flag = plane.Raycast(ray, out enter);
		if (!flag)
		{
			flag = new Plane(Vector3.right, averagePositionOfBookendSpawnPoints).Raycast(ray, out enter);
			if (!flag)
			{
				flag = new Plane(Vector3.up, averagePositionOfBookendSpawnPoints).Raycast(ray, out enter);
				if (!flag)
				{
					enter = GameSettings.CamDistFromPivot();
					flag = true;
				}
			}
		}
		enter = Mathf.Min(enter, Mathf.Max(2f * GameSettings.CamDistFromPivot(), ray.origin.magnitude));
		if (flag)
		{
			m_Pivot = ray.GetPoint(enter);
			if (GameStateManager.GetState() == GameState.SIM)
			{
				m_PointsOfView[PointOfViewType.SIM_CUSTOM].m_Pivot = m_Pivot;
			}
			else if (GameStateManager.GetState() == GameState.BUILD)
			{
				m_PointsOfView[PointOfViewType.BUILD_CUSTOM].m_Pivot = m_Pivot;
			}
			else if (GameStateManager.GetState() == GameState.PHOTO)
			{
				m_PointsOfView[PointOfViewType.PHOTO].m_Pivot = m_Pivot;
			}
		}
		return m_Pivot - pivot;
	}

	public static void SetCameraImmediate(PointOfView pointOfView)
	{
		CameraInterpolate.Cancel();
		Cameras.SetOrthographicSize(pointOfView.m_OrthographicsSize);
		Cameras.MainCamera().transform.position = pointOfView.m_Pos;
		Cameras.MainCamera().transform.rotation = pointOfView.m_Rot;
		Game.RefreshAfterOrthographicSizeChange();
		CameraControl.RegisterTransformUpdate();
	}

	public static void Set(PointOfViewType type, Vector3 pivot, Vector3 pos, Quaternion rot, float orthographicSize)
	{
		PointOfView pointOfView = GetPointOfView(type);
		if (pointOfView != null)
		{
			pointOfView.m_Pivot = pivot;
			pointOfView.m_Pos = pos;
			pointOfView.m_Rot = rot;
			pointOfView.m_OrthographicsSize = orthographicSize;
		}
	}

	public static PointOfView GetPointOfView(PointOfViewType type)
	{
		if (!m_PointsOfView.ContainsKey(type))
		{
			Debug.LogWarningFormat("Cannot find Point of View {0} to interpolate to", type.ToString());
			return null;
		}
		return m_PointsOfView[type];
	}

	private static void FrameObjects(string levelID)
	{
		PointOfViewType type = ((GameStateManager.GetState() == GameState.MAIN_MENU) ? PointOfViewType.SIM_RIGHT : GameStateManager.GetDefaultPointOfViewType());
		SnapTo(type);
		UpdatePivotBasedOnCamera();
		m_PointsOfView[PointOfViewType.BUILD].FrameObjects(levelID);
		m_PointsOfView[PointOfViewType.SIM_CENTER].CopyFrom(m_PointsOfView[PointOfViewType.BUILD]);
		m_PointsOfView[PointOfViewType.SIM_CENTER_PITCHED_DOWN].FrameObjects(levelID);
		m_PointsOfView[PointOfViewType.SIM_RIGHT].FrameObjects(levelID);
		m_PointsOfView[PointOfViewType.SIM_LEFT].FrameObjects(levelID);
		m_PointsOfView[PointOfViewType.DECOR_TOP].FrameObjects(levelID);
		m_PointsOfView[PointOfViewType.DECOR_CENTER].FrameObjects(levelID);
		SnapTo(type);
	}

	public static Vector3 CalculatePivot()
	{
		float bookendsMidPointX = GetBookendsMidPointX();
		float bookendsMidPointY = GetBookendsMidPointY();
		return new Vector3(bookendsMidPointX, bookendsMidPointY, 0f);
	}

	public static Bounds2 CalcBoundsForNewCameraController()
	{
		float num = 15f;
		Bounds bounds = default(Bounds);
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			bounds.Encapsulate(terrain.m_BoxCollider.bounds);
		}
		Bounds2 result = bounds;
		result.min.x += 0.5f * num;
		result.max.x -= 0.5f * num;
		if (result.max.x < result.min.x)
		{
			Values.Swap(ref result.min.x, ref result.max.x);
		}
		result.min.y = 5f;
		List<Bounds> list = new List<Bounds>();
		PointOfView.AddVehiclesToBoundsList(list);
		PointOfView.AddVictoryFlagsToBoundsList(list);
		PointOfView.AddBookEndSpawnPointsToBoundsList(list);
		PointOfView.AddAllJointsToBoundsList_UsedByAdrian(list);
		float num2 = float.NegativeInfinity;
		foreach (Bounds item in list)
		{
			num2 = Math.Max(num2, item.max.y);
		}
		result.max.y = num2 + 10f;
		result.max.y = Mathf.Max(result.min.y, result.max.y);
		return result;
	}

	public static Bounds Calc3dBoundsForGameCamera()
	{
		List<Bounds> boundsList = new List<Bounds>();
		TerrainIslands.m_Terrains.ForEach(delegate(TerrainIsland island)
		{
			boundsList.Add(island.m_MeshRenderer.bounds);
		});
		PointOfView.AddVehiclesToBoundsList(boundsList);
		PointOfView.AddVictoryFlagsToBoundsList(boundsList);
		TerrainIslands.GetRightTerrain();
		TerrainIslands.GetLeftTerrain();
		PointOfView.AddAnchorsToBoundsList(boundsList);
		PointOfView.AddAllJointsToBoundsList_UsedByAdrian(boundsList);
		Decors.m_Decors.ForEach(delegate(Decor decor)
		{
			decor.m_MeshRenderers.ForEach(delegate(MeshRenderer r)
			{
				boundsList.Add(r.bounds);
			});
		});
		Platforms.m_Platforms.ForEach(delegate(Platform platform)
		{
			boundsList.Add(platform.m_Collider.bounds);
		});
		Platforms.m_Platforms.ForEach(delegate(Platform platform)
		{
			platform.m_Planks.ForEach(delegate(MeshRenderer r)
			{
				boundsList.Add(r.bounds);
			});
		});
		Platforms.m_Platforms.ForEach(delegate(Platform platform)
		{
			platform.m_Poles.ForEach(delegate(MeshRenderer r)
			{
				boundsList.Add(r.bounds);
			});
		});
		Ramps.m_Ramps.ForEach(delegate(Ramp ramp)
		{
			boundsList.Add(ramp.m_Bounds);
		});
		CustomShapes.m_Shapes.ForEach(delegate(CustomShape shape)
		{
			boundsList.Add(shape.m_MeshRenderer.bounds);
		});
		Rocks.m_Rocks.ForEach(delegate(Rock r)
		{
			boundsList.Add(r.m_MeshRenderer.bounds);
		});
		FlyingObjects.m_FlyingObjects.ForEach(delegate(FlyingObject fo)
		{
			boundsList.Add(fo.m_MeshRenderer.bounds);
		});
		BridgePillars.m_BridgePillars.ForEach(delegate(BridgePillar bp)
		{
			bp.m_MeshRenderers.ForEach(delegate(MeshRenderer r)
			{
				boundsList.Add(r.bounds);
			});
		});
		ZedAxisVehicles.m_Vehicles.ForEach(delegate(ZedAxisVehicle zv)
		{
			boundsList.Add(zv.m_MeshRenderer.bounds);
		});
		Bounds bounds = ((0 < boundsList.Count) ? boundsList[0] : default(Bounds));
		boundsList.ForEach(delegate(Bounds b)
		{
			bounds.Encapsulate(b);
		});
		return bounds;
	}

	private static bool CameraAlreadyAtPointOfView(PointOfView pointOfView)
	{
		if (pointOfView.m_Pos == Cameras.MainCamera().transform.position)
		{
			return pointOfView.m_Rot == Cameras.MainCamera().transform.rotation;
		}
		return false;
	}

	private static bool DesiredPointOfViewIsTooDifferentFromCurrentView(PointOfView pointOfView)
	{
		return false;
	}

	private static float GetBookendsMidPointX()
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!rightTerrain || !leftTerrain)
		{
			Debug.LogWarningFormat("Could not find left and right terrains to determine camera rotation pivot");
			return 0f;
		}
		return (leftTerrain.transform.position.x + rightTerrain.transform.position.x) / 2f;
	}

	private static float GetBookendsMidPointY()
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!rightTerrain || !leftTerrain)
		{
			Debug.LogWarningFormat("Could not find left and right terrains to determine camera rotation pivot");
			return 0f;
		}
		if (!leftTerrain.m_SpawnPoint)
		{
			Debug.LogWarningFormat("Left terrain requires a TerrainIslandSpawnPoint");
			return 0f;
		}
		if (!rightTerrain.m_SpawnPoint)
		{
			Debug.LogWarningFormat("Right terrain requires a TerrainIslandSpawnPoint");
			return 0f;
		}
		return (leftTerrain.m_SpawnPoint.transform.position.y + rightTerrain.m_SpawnPoint.transform.position.y) / 2f;
	}
}
