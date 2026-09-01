using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class WaterRuler : MonoBehaviour
{
	public ToolTip m_ToolTip;

	public RectTransform m_ToolTipRectTransform;

	private BridgePillarDistanceMarker m_DistanceMarker;

	private VectorLine m_Line;

	private float m_LastStartX;

	private float m_LastEndX;

	private float m_LastHeight;

	private void Start()
	{
		m_ToolTip.SetColors(GameUI.m_Instance.m_RulerTextColor, GameUI.m_Instance.m_RulerTextOutlineColor);
		m_ToolTipRectTransform.gameObject.layer = Utils.WATER_LAYER;
	}

	private void OnDestroy()
	{
		if (m_Line != null)
		{
			VectorLine.Destroy(ref m_Line);
		}
	}

	public void Enable()
	{
		base.gameObject.SetActive(value: true);
		if (m_Line != null)
		{
			m_Line.active = !SandboxSettings.m_NoWater;
		}
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
		if (m_Line != null)
		{
			m_Line.active = false;
		}
		if (m_DistanceMarker != null)
		{
			m_DistanceMarker.Hide(hide: true);
		}
	}

	public void UpdateManual(Vector3 worldPos, float width, float height, Color color)
	{
		if (SandboxSettings.m_NoWater)
		{
			Disable();
			if (m_DistanceMarker == null)
			{
				m_DistanceMarker = new BridgePillarDistanceMarker();
			}
			m_DistanceMarker.Hide(hide: false);
			m_DistanceMarker.UpdateManual(worldPos - Vector3.right * width / 2f, worldPos + Vector3.right * width / 2f, startIsTerrain: true, endIsTerrain: true);
		}
		else
		{
			UpdateLine(worldPos, width, height, color);
		}
		UpdateToolTip(worldPos, width, height);
	}

	public void RefreshAfterOrthographicSizeChange()
	{
		if (m_Line != null)
		{
			Outlines.UpdateWidthForOrthographicChange(m_Line, WaterLineMaker.THICKNESS);
		}
	}

	private void UpdateLine(Vector3 worldPos, float width, float height, Color color)
	{
		float num = worldPos.x - width / 2f;
		float num2 = num + width;
		if (m_Line == null)
		{
			m_Line = CreateLine();
		}
		if (SandboxSelectionSet.m_Items.Contains(WaterBlocks.GetSandboxItem()))
		{
			m_Line.color = GameUI.m_Instance.m_OutlineSelectedColorSandbox;
		}
		else if (SandboxItems.m_Hover == WaterBlocks.GetSandboxItem())
		{
			m_Line.color = GameUI.m_Instance.m_OutlineHoverColorSandbox;
		}
		else
		{
			m_Line.color = Color.white;
		}
		if (!Mathf.Approximately(num, m_LastStartX) || !Mathf.Approximately(num2, m_LastEndX) || !Mathf.Approximately(height, m_LastHeight))
		{
			m_LastStartX = num;
			m_LastEndX = num2;
			m_LastHeight = height;
			WaterLineMaker.GenerateSimple(m_Line, num, num2, height);
		}
	}

	private void UpdateToolTip(Vector3 worldPos, float width, float height)
	{
		if ((bool)m_ToolTip)
		{
			if (!ShouldShowToolTip(height))
			{
				m_ToolTip.gameObject.SetActive(value: false);
				return;
			}
			m_ToolTip.gameObject.SetActive(value: true);
			m_ToolTip.Set(width.ToString("0.00m"), null);
			GameUI.SetScreenPosClamped(m_ToolTip.gameObject, Cameras.MainCamera().WorldToScreenPoint(worldPos), 0f, 0f);
		}
	}

	private bool ShouldShowToolTip(float height)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameStateManager.GetState() != GameState.SANDBOX || GameStateBuild.m_CameraInTransition)
		{
			return false;
		}
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		Vector2 vector = Cameras.MainCamera().WorldToScreenPoint(new Vector3(leftTerrain.transform.position.x, height, 0f));
		Vector2 vector2 = Cameras.MainCamera().WorldToScreenPoint(new Vector3(rightTerrain.transform.position.x, height, 0f));
		if ((vector.x < 0f || vector.x > (float)Screen.width) && (vector2.x < 0f || vector2.x > (float)Screen.width))
		{
			return false;
		}
		if ((vector.y < 0f || vector.y > (float)Screen.height) && (vector2.y < 0f || vector2.y > (float)Screen.height))
		{
			return false;
		}
		return true;
	}

	private static VectorLine CreateLine()
	{
		VectorLine vectorLine = new VectorLine("Water Ruler", new List<Vector3>(), GameUI.m_Instance.m_ChalkLine2D, WaterLineMaker.THICKNESS, LineType.Continuous, Joins.Weld);
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
