using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class WaterLine
{
	private static VectorLine m_Line;

	public static void Init()
	{
		m_Line = CreateLine();
	}

	public static void Generate()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		float startX = leftTerrain.transform.position.x - leftTerrain.m_BoxCollider.size.x - 10f;
		float endX = rightTerrain.transform.position.x + rightTerrain.m_BoxCollider.size.x + 10f;
		WaterLineMaker.GenerateSimple(m_Line, startX, endX, WaterBlocks.GetHeight());
	}

	public static void Enable(bool enable)
	{
		m_Line.active = enable;
	}

	public static void RefreshAfterOrthographicSizeChange()
	{
		Outlines.UpdateWidthForOrthographicChange(m_Line, WaterLineMaker.THICKNESS);
	}

	private static VectorLine CreateLine()
	{
		VectorLine vectorLine = new VectorLine("Water Line", new List<Vector3>(), GameUI.m_Instance.m_ChalkLine2D, WaterLineMaker.THICKNESS, LineType.Continuous, Joins.Weld);
		if (vectorLine == null)
		{
			return null;
		}
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.layer = Utils.RENDER_LAST_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = Color.white;
		vectorLine.material.renderQueue = 3100;
		vectorLine.AddNormals();
		return vectorLine;
	}
}
