using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Vectrosity;

public class Outline
{
	public VectorLine m_VectorLine;

	public float m_Width;

	public float m_BuildModeWidthMultiplier = 1f;

	private Dictionary<SplineComputer, SplinePoint[]> m_CachedSplinePoints = new Dictionary<SplineComputer, SplinePoint[]>();

	public Outline(Texture texture, float textureScale, float width, Color color, int layer)
	{
		m_VectorLine = CreateVectorLine(texture, width, layer);
		SetWidth(width);
		SetColor(color);
		SetTextureScale(textureScale);
	}

	public void SetMaterial(Material material)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.material = material;
		}
	}

	public void Destroy()
	{
		if (m_VectorLine != null)
		{
			VectorLine.Destroy(ref m_VectorLine);
		}
		Outlines.Remove(this);
	}

	public void SetLayer(int layer)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.layer = layer;
		}
	}

	public void ClearCachedSplinePoints()
	{
		m_CachedSplinePoints.Clear();
	}

	public void SetActive(bool active)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.active = active;
		}
	}

	public void SetTextureAndTextureScale(Texture texture, float textureScale)
	{
		SetTexture(texture);
		SetTextureScale(textureScale);
	}

	public void SetTexture(Texture texture)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.texture = texture;
		}
	}

	public void SetTextureScale(float textureScale)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.textureScale = textureScale;
		}
	}

	public void SetColor(Color color)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.SetColor(color);
		}
	}

	public void SetWidth(float width)
	{
		if (m_VectorLine != null)
		{
			m_Width = width;
			Outlines.UpdateWidthForOrthographicChange(m_VectorLine, m_Width);
		}
	}

	public void SetDrawTransform(Transform drawTransform)
	{
		if (m_VectorLine != null)
		{
			m_VectorLine.drawTransform = drawTransform;
		}
	}

	public void UpdateForGameState(GameState gameState, float width)
	{
		SetTexture(GameUI.m_Instance.GetOutlineTexture(gameState));
		SetTextureScale(GameUI.m_Instance.m_OutlineTextureScale);
		SetWidth(width * ((gameState == GameState.BUILD) ? m_BuildModeWidthMultiplier : 1f));
		SetColor(GameUI.m_Instance.GetOutlineColor(gameState));
	}

	public void UpdateFromBounds(Transform transform, Bounds bounds, float z)
	{
		if (m_VectorLine != null)
		{
			float num = bounds.size.x / 2f;
			float num2 = bounds.size.y / 2f;
			m_VectorLine.Resize(5);
			m_VectorLine.points3[0] = transform.TransformPoint(new Vector3(0f - num, 0f - num2, z));
			m_VectorLine.points3[1] = transform.TransformPoint(new Vector3(0f - num, num2, z));
			m_VectorLine.points3[2] = transform.TransformPoint(new Vector3(num, num2, z));
			m_VectorLine.points3[3] = transform.TransformPoint(new Vector3(num, 0f - num2, z));
			m_VectorLine.points3[4] = transform.TransformPoint(new Vector3(0f - num, 0f - num2, z));
		}
	}

	public void UpdateFromBounds(Bounds bounds, float z)
	{
		if (m_VectorLine != null)
		{
			float num = bounds.size.x / 2f;
			float num2 = bounds.size.y / 2f;
			Vector3 vector = new Vector3(bounds.center.x, bounds.center.y, 0f);
			m_VectorLine.Resize(5);
			m_VectorLine.points3[0] = vector + new Vector3(0f - num, 0f - num2, z);
			m_VectorLine.points3[1] = vector + new Vector3(0f - num, num2, z);
			m_VectorLine.points3[2] = vector + new Vector3(num, num2, z);
			m_VectorLine.points3[3] = vector + new Vector3(num, 0f - num2, z);
			m_VectorLine.points3[4] = vector + new Vector3(0f - num, 0f - num2, z);
		}
	}

	public void UpdateFromSpline(SplineComputer spline, float z)
	{
		if (!m_CachedSplinePoints.ContainsKey(spline))
		{
			m_CachedSplinePoints.Add(spline, spline.GetPoints(SplineComputer.Space.Local));
		}
		m_VectorLine.Resize(m_CachedSplinePoints[spline].Length);
		int num = 0;
		SplinePoint[] array = m_CachedSplinePoints[spline];
		for (int i = 0; i < array.Length; i++)
		{
			SplinePoint splinePoint = array[i];
			m_VectorLine.points3[num] = spline.transform.TransformPoint(splinePoint.position);
			m_VectorLine.points3[num] = new Vector3(m_VectorLine.points3[num].x, m_VectorLine.points3[num].y, z);
			num++;
		}
	}

	public void UpdateFromPolygonCollider2D(PolygonCollider2D collider)
	{
		if (collider.points.Length != 0)
		{
			m_VectorLine.Resize(collider.points.Length + 1);
			for (int i = 0; i < collider.points.Length; i++)
			{
				Vector2 vector = collider.transform.TransformPoint(collider.points[i]);
				m_VectorLine.points3[i] = new Vector3(vector.x, vector.y, 0f);
			}
			m_VectorLine.points3[collider.points.Length] = m_VectorLine.points3[0];
		}
	}

	public void UpdateOutlineFromSpline(SplineComputer spline, float yOffset, float yThreshold, float z)
	{
		SplinePoint[] points = spline.GetPoints(SplineComputer.Space.Local);
		m_VectorLine.Resize(points.Length);
		int num = 0;
		SplinePoint[] array = points;
		for (int i = 0; i < array.Length; i++)
		{
			SplinePoint splinePoint = array[i];
			if (splinePoint.position.y > yThreshold)
			{
				m_VectorLine.points3[num] = spline.transform.TransformPoint(splinePoint.position + new Vector3(0f, yOffset, z));
			}
			else
			{
				m_VectorLine.points3[num] = spline.transform.TransformPoint(splinePoint.position);
			}
			m_VectorLine.points3[num] = new Vector3(m_VectorLine.points3[num].x, m_VectorLine.points3[num].y, z);
			num++;
		}
	}

	public void UpdateOutlinePointsInWorldSpace(List<Vector3> points)
	{
		m_VectorLine.Resize(points.Count);
		for (int i = 0; i < points.Count; i++)
		{
			m_VectorLine.points3[i] = points[i];
		}
	}

	private VectorLine CreateVectorLine(Texture texture, float lineWidth, int layer)
	{
		VectorLine vectorLine = new VectorLine("Outline", new List<Vector3>(), texture, lineWidth, LineType.Continuous, Joins.Weld);
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.texture = texture;
		vectorLine.active = true;
		vectorLine.Draw3DAuto();
		vectorLine.active = false;
		vectorLine.layer = layer;
		return vectorLine;
	}
}
