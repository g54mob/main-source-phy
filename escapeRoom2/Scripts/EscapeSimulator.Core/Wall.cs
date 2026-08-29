using System;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Polygon
{
	private class LinkedWallData
	{
		public Floor floor;

		public int floorVertexIndexA;

		public int floorVertexIndexB;

		public bool hasAdjacentWallA;

		public bool hasAdjacentWallB;
	}

	private LinkedWallData linkedWallData;

	protected override void prepareMeshRecalculation()
	{
		if (!tryGetFloor(out var floor) || !floor.connectWalls)
		{
			linkedWallData = null;
			return;
		}
		int wallIndex = floor.getWallIndex(this);
		if (wallIndex == -1)
		{
			linkedWallData = null;
			return;
		}
		linkedWallData = new LinkedWallData();
		linkedWallData.floor = floor;
		int edgeIndex = (wallIndex + floor.vertices.Count - 1) % floor.vertices.Count;
		int num = (wallIndex + 1) % floor.vertices.Count;
		List<Wall> walls = floor.getWalls();
		linkedWallData.hasAdjacentWallA = floor.getWall(edgeIndex, walls) != null;
		linkedWallData.hasAdjacentWallB = floor.getWall(num, walls) != null;
		linkedWallData.floorVertexIndexA = wallIndex;
		linkedWallData.floorVertexIndexB = num;
	}

	protected override Vector3 calculateExtrudedVertex(Vector3 vertex)
	{
		if (linkedWallData == null)
		{
			return base.calculateExtrudedVertex(vertex);
		}
		Transform obj = base.transform;
		Vector3 vector = vertex;
		float? y = 0f;
		Vector3 vertex2 = obj.TransformPoint(vector.With(null, y));
		Floor floor = linkedWallData.floor;
		if (linkedWallData.hasAdjacentWallA)
		{
			Vector3 vertex3 = floor.polygonToWorldPosition(floor.vertices[linkedWallData.floorVertexIndexA]);
			if (Polygon.isSameVertex(vertex2, vertex3))
			{
				return calculateConnectionPoint(linkedWallData.floorVertexIndexA);
			}
		}
		if (linkedWallData.hasAdjacentWallB)
		{
			Vector3 vertex4 = floor.polygonToWorldPosition(floor.vertices[linkedWallData.floorVertexIndexB]);
			if (Polygon.isSameVertex(vertex2, vertex4))
			{
				return calculateConnectionPoint(linkedWallData.floorVertexIndexB);
			}
		}
		return base.calculateExtrudedVertex(vertex);
		Vector3 calculateConnectionPoint(int vertexIndex)
		{
			float angle = floor.getAngle(vertexIndex);
			if (Mathf.Approximately(angle, 180f))
			{
				return base.calculateExtrudedVertex(vertex);
			}
			int count = floor.vertices.Count;
			Vector3 vector2 = floor.polygonToWorldPosition(floor.vertices[vertexIndex]);
			Vector3 vector3 = floor.polygonToWorldPosition(floor.vertices[(vertexIndex + 1) % count]);
			Vector3 vector4 = floor.polygonToWorldPosition(floor.vertices[(vertexIndex + count - 1) % count]);
			float num = 90f - angle / 2f;
			float num2 = base.thickness / Mathf.Cos(num * (MathF.PI / 180f));
			Vector3 normalized = (vector3 - vector2).normalized;
			Vector3 normalized2 = (vector4 - vector2).normalized;
			Vector3 position = vector2 + (normalized + normalized2).normalized * (num2 * (float)((!(angle <= 180f)) ? 1 : (-1)));
			Vector3 vector5 = base.transform.InverseTransformPoint(position);
			float? y2 = vertex.y;
			return vector5.With(null, y2);
		}
	}

	public bool tryGetFloor(out Floor floor)
	{
		floor = getFloor();
		return floor != null;
	}

	public Floor getFloor()
	{
		return GetComponentInParent<Floor>();
	}
}
