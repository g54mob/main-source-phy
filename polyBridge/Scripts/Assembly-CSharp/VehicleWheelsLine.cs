using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class VehicleWheelsLine
{
	private VectorLine m_Line;

	private const float LINE_WIDTH = 5f;

	public VehicleWheelsLine()
	{
		m_Line = CreateLine();
	}

	public void SyncToVehicle(Vehicle vehicle)
	{
		Vector3 vector = vehicle.m_ScalingTransform.localScale.x * vehicle.transform.right;
		m_Line.points3[0] = vehicle.transform.position - vector * (0f - vehicle.m_StaticBoundingBox.center.x + vehicle.m_StaticBoundingBox.size.x / 2f);
		m_Line.points3[1] = vehicle.transform.position + vector * (vehicle.m_StaticBoundingBox.center.x + vehicle.m_StaticBoundingBox.size.x / 2f);
		UpdateWidth();
	}

	public void SetActive(bool active)
	{
		if (m_Line != null)
		{
			m_Line.active = active;
		}
	}

	public void Destroy()
	{
		if (m_Line != null)
		{
			VectorLine.Destroy(ref m_Line);
		}
	}

	public void UpdateWidth()
	{
		if (m_Line != null)
		{
			Outlines.UpdateWidthForOrthographicChange(m_Line, 5f);
		}
	}

	private VectorLine CreateLine()
	{
		VectorLine vectorLine = new VectorLine("Wheels Line", new List<Vector3>(), GameUI.m_Instance.m_ChalkLine2D, 5f);
		if (vectorLine == null)
		{
			return null;
		}
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.layer = Utils.VEHICLE_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = Color.white;
		vectorLine.AddNormals();
		return vectorLine;
	}
}
