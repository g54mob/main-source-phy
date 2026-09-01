using UnityEngine;

public class GameGrid
{
	public static GameObject m_Grid;

	public static float m_Spacing = 0.25f;

	public static void Init()
	{
		m_Grid = Object.Instantiate(Prefabs.m_Instance.m_Grid);
		m_Grid.name = Prefabs.m_Instance.m_Grid.name;
		Utils.SetLayerRecursively(m_Grid, Utils.RENDER_LAST_LAYER);
		Object.DontDestroyOnLoad(m_Grid);
	}

	public static void SetZPos(float zPos)
	{
		m_Grid.transform.position = new Vector3(m_Grid.transform.position.x, m_Grid.transform.position.y, zPos);
	}

	public static void CenterOnTerrainEdge(TerrainIsland terrain)
	{
		m_Grid.transform.position = new Vector3(Utils.RoundToNearestMultipleOf(terrain.transform.position.x, m_Spacing), Utils.RoundToNearestMultipleOf(terrain.GetHeight(), m_Spacing), m_Grid.transform.position.z);
	}

	public static bool IsGridAligned(float value)
	{
		float a = Utils.ApproximateFloat(value - Mathf.Floor(value), 1000);
		if (!Mathf.Approximately(a, 0f) && !Mathf.Approximately(a, 0.25f) && !Mathf.Approximately(a, 0.5f))
		{
			return Mathf.Approximately(a, 0.75f);
		}
		return true;
	}

	public static bool IsGridAligned(float value, float offset)
	{
		float a = Utils.ApproximateFloat(value - Mathf.Floor(value), 1000);
		if (!Mathf.Approximately(a, offset) && !Mathf.Approximately(a, offset + 0.25f) && !Mathf.Approximately(a, offset + 0.5f))
		{
			return Mathf.Approximately(a, offset + 0.75f);
		}
		return true;
	}

	public static Vector3 SnapPosToGrid(Vector3 worldPos)
	{
		if (!m_Grid.activeInHierarchy)
		{
			return worldPos;
		}
		return SnapPosToGridForced(worldPos);
	}

	public static Vector3 SnapPosToGridForced(Vector3 worldPos)
	{
		float x = RoundToNearestGridSquareForced(worldPos.x);
		float y = RoundToNearestGridSquareForced(worldPos.y);
		float z = RoundToNearestGridSquareForced(worldPos.z);
		if (Game.InDecorModeTopView())
		{
			return new Vector3(x, worldPos.y, z);
		}
		return new Vector3(x, y, worldPos.z);
	}

	public static float RoundToNearestGridSquare(float f)
	{
		if (!m_Grid.activeInHierarchy)
		{
			return f;
		}
		return RoundToNearestGridSquareForced(f);
	}

	public static float RoundToNearestGridSquareForced(float f)
	{
		return Utils.RoundToNearestMultipleOf(f, m_Spacing);
	}

	public static void SetGridLayer(int layer)
	{
		Utils.SetLayerRecursively(m_Grid, layer);
	}
}
