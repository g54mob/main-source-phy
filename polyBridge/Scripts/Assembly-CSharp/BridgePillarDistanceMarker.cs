using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BridgePillarDistanceMarker
{
	public Vector3 m_Pos;

	private ToolTip m_ToolTip;

	private VectorLine m_Line;

	public BridgePillarDistanceMarker()
	{
		m_ToolTip = CreateToolTip();
		m_Line = CreateLine();
		Hide(hide: true);
	}

	public void Hide(bool hide)
	{
		m_Line.active = !hide;
		m_ToolTip.gameObject.SetActive(!hide);
	}

	public void UpdateManual(Vector3 start, Vector3 end, bool startIsTerrain, bool endIsTerrain)
	{
		m_Pos = (start + end) / 2f;
		float length = Vector3.Distance(start, end);
		UpdateLine(m_Line, start, end, startIsTerrain, endIsTerrain);
		UpdateToolTip(m_ToolTip, m_Pos, length);
	}

	private ToolTip CreateToolTip()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_ToolTip, GameUI.m_Instance.m_RulerText.transform);
		if (!gameObject)
		{
			return null;
		}
		ToolTip component = gameObject.GetComponent<ToolTip>();
		if (!component)
		{
			return null;
		}
		component.gameObject.SetActive(value: false);
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			component.SetColors(GameUI.m_Instance.m_RulerTextColor, GameUI.m_Instance.m_RulerTextOutlineColor);
		}
		component.name = "Distance Marker ToolTip";
		return component;
	}

	private VectorLine CreateLine()
	{
		VectorLine vectorLine = new VectorLine("Distance Marker Line", new List<Vector3>(), GameUI.m_Instance.m_ChalkLine2D, 20f);
		if (vectorLine == null)
		{
			return null;
		}
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.endCap = BridgePillarDistanceMarkers.ENDCAP_NAME;
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.layer = Utils.RENDER_LAST_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = Color.white;
		vectorLine.AddNormals();
		return vectorLine;
	}

	private void UpdateLine(VectorLine line, Vector3 start, Vector3 end, bool startIsTerrain, bool endIsTerrain)
	{
		float num = 0f;
		if (startIsTerrain)
		{
			num = ((start.x < end.x) ? GameSettings.TerrainOverhang() : (0f - GameSettings.TerrainOverhang()));
		}
		line.points3[0] = new Vector3(start.x + num, start.y, -10f);
		if (endIsTerrain)
		{
			num = ((end.x > start.x) ? (0f - GameSettings.TerrainOverhang()) : GameSettings.TerrainOverhang());
		}
		line.points3[1] = new Vector3(end.x + num, end.y, -10f);
	}

	private void UpdateToolTip(ToolTip toolTip, Vector3 pos, float length)
	{
		Vector2 screenPos = Cameras.MainCamera().WorldToScreenPoint(pos);
		GameUI.SetScreenPos(toolTip.gameObject, screenPos, (0f - toolTip.m_RectTransform.rect.width) / 2f, (0f - toolTip.m_RectTransform.rect.height) / 2f);
		toolTip.Set($"{length:F2}m", null);
	}
}
