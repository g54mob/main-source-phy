using UnityEngine;

public class WorldBounds
{
	public static Bounds m_Bounds;

	private static Outline m_Outline;

	private static float m_DisplayBoundsUntilTime;

	private static Transform m_Transform;

	public static void Init(float width, float minY, float maxY)
	{
		m_Transform = new GameObject("World Bounds").GetComponent<Transform>();
		m_Outline = Outlines.Create(GameUI.m_Instance.m_OutlineTextureSandbox, GameUI.m_Instance.m_OutlineTextureScale, GameUI.m_Instance.m_OutlineWidthSandbox, Color.red);
		Calculate(width, minY, maxY);
	}

	public static void Show()
	{
		m_DisplayBoundsUntilTime = float.MaxValue;
		if (m_Outline != null)
		{
			m_Outline.SetActive(active: true);
			m_Outline.SetColor(GameUI.m_Instance.m_WorldBoundsColor);
		}
	}

	public static void Hide()
	{
		m_DisplayBoundsUntilTime = 0f;
		if (m_Outline != null)
		{
			m_Outline.SetActive(active: false);
		}
	}

	public static void ShowBriefly()
	{
		m_DisplayBoundsUntilTime = Time.unscaledTime + 1f;
	}

	public static void Calculate(float width, float minY, float maxY)
	{
		Vector2 centerPointBetweenBookends = GetCenterPointBetweenBookends();
		float num = centerPointBetweenBookends.x - width / 2f;
		float num2 = centerPointBetweenBookends.x + width / 2f;
		Vector2 vector = new Vector2((num + num2) / 2f, (minY + maxY) / 2f);
		m_Bounds = new Bounds(size: new Vector2(num2 - num, maxY - minY), center: vector);
		m_Transform.position = vector;
		m_Transform.rotation = Quaternion.identity;
		if (m_Outline != null)
		{
			m_Outline.UpdateFromBounds(m_Transform, m_Bounds, 0f);
		}
	}

	public static bool Contains(Vector2 pos)
	{
		return m_Bounds.Contains(pos);
	}

	public static bool Overlaps(Bounds b)
	{
		return m_Bounds.Intersects(b);
	}

	public static Vector3 Clamp(Vector3 pos)
	{
		return new Vector3(Mathf.Clamp(pos.x, m_Bounds.min.y, m_Bounds.max.x), Mathf.Clamp(pos.y, m_Bounds.min.y, m_Bounds.max.y), pos.z);
	}

	public static void UpdateOutline()
	{
		if (m_Outline != null && Time.unscaledTime < m_DisplayBoundsUntilTime != m_Outline.m_VectorLine.active)
		{
			m_Outline.SetActive(Time.unscaledTime < m_DisplayBoundsUntilTime);
		}
	}

	public static void DisableOutline()
	{
		if (m_Outline != null)
		{
			m_Outline.SetActive(active: false);
		}
	}

	private static Vector2 GetCenterPointBetweenBookends()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (!leftTerrain || !rightTerrain)
		{
			return Vector2.zero;
		}
		return (leftTerrain.transform.position + rightTerrain.transform.position) / 2f;
	}
}
