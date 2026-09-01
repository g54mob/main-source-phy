using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class Outlines
{
	public static List<Outline> m_Outlines = new List<Outline>();

	public static Outline Create(Texture texture, float textureScale, float lineWidth, Color color)
	{
		Outline outline = new Outline(texture, textureScale, lineWidth, color, Utils.FOREGROUND_LAYER);
		if (outline != null)
		{
			m_Outlines.Add(outline);
		}
		return outline;
	}

	public static void Disable()
	{
		ZedAxisVehicles.DisableOutlines();
		BridgeJoints.DisableOutlines();
		BuildZones.DisableOutlines();
		Checkpoints.DisableOutlines();
		CustomShapes.DisableOutlines();
		FlyingObjects.DisableOutlines();
		Pillars.DisableOutlines();
		Platforms.DisableOutlines();
		Ramps.DisableOutlines();
		Rocks.DisableOutlines();
		TerrainIslands.DisableOutlines();
		Vehicles.DisableOutlines();
		VehicleStopTriggers.DisableOutlines();
		WorldBounds.DisableOutline();
	}

	public static void ManualUpdate()
	{
		BridgeJoints.UpdateOutlines();
		BuildZones.UpdateOutlines();
		Checkpoints.UpdateOutlines();
		CustomShapes.UpdateOutlines();
		FlyingObjects.UpdateOutlines();
		Pillars.UpdateOutlines();
		Platforms.UpdateOutlines();
		Ramps.UpdateOutlines();
		Rocks.UpdateOutlines();
		TerrainIslands.UpdateOutlines();
		Vehicles.UpdateOutlines();
		VehicleStopTriggers.UpdateOutlines();
		WorldBounds.UpdateOutline();
		ZedAxisVehicles.UpdateOutlines();
	}

	public static void UpdateOutlinesForStateChange(GameState gameState)
	{
		foreach (Outline outline in m_Outlines)
		{
			outline?.UpdateForGameState(gameState, GameUI.m_Instance.GetOutlineWidth(gameState));
		}
	}

	public static void Remove(Outline outline)
	{
		if (m_Outlines.Contains(outline))
		{
			m_Outlines.Remove(outline);
		}
	}

	public static void RefreshAfterOrthographicSizeChange()
	{
		foreach (Outline outline in m_Outlines)
		{
			if (outline != null)
			{
				UpdateWidthForOrthographicChange(outline.m_VectorLine, outline.m_Width);
				outline.SetWidth(outline.m_Width);
			}
		}
	}

	public static void UpdateWidthForOrthographicChange(VectorLine line, float defaultWidth)
	{
		float num = 1f / Cameras.MainCamera().orthographicSize * 10f * defaultWidth;
		line.SetWidth(Mathf.Max(3f, num));
		line.maxWeldDistance = 2f * num;
	}
}
