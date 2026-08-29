using System.Collections.Generic;
using UnityEngine;

public class Floor : Polygon
{
	private enum WallUpdateType
	{
		Height = 0,
		Thickness = 1,
		ConnectWalls = 2
	}

	public float wallHeight = 3f;

	public float wallThickness;

	public bool connectWalls;

	private float wallHeightLastFrame;

	private float wallThicknessLastFrame;

	private bool connectWallsLastFrame;

	private List<(Wall wall, Vector3 localPosition)> wallsLastFrame = new List<(Wall, Vector3)>();

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (wallHeight != wallHeightLastFrame)
		{
			updateWalls(WallUpdateType.Height);
			updateCeilings();
			wallHeightLastFrame = wallHeight;
		}
		if (wallThickness != wallThicknessLastFrame)
		{
			updateWalls(WallUpdateType.Thickness);
			wallThicknessLastFrame = wallThickness;
		}
		if (connectWalls != connectWallsLastFrame || shouldUpdateWallConnections())
		{
			updateWalls(WallUpdateType.ConnectWalls);
			connectWallsLastFrame = connectWalls;
		}
	}

	private bool shouldUpdateWallConnections()
	{
		if (!connectWalls || GetComponentsInParent<Floor>().Length > 1)
		{
			return false;
		}
		List<Wall> wallsThisFrame = getWalls();
		if (wallsThisFrame.Count != wallsLastFrame.Count)
		{
			saveWallsState();
			return true;
		}
		for (int i = 0; i < wallsThisFrame.Count; i++)
		{
			if (!(wallsThisFrame[i].transform.localPosition == wallsLastFrame[i].localPosition))
			{
				saveWallsState();
				return true;
			}
		}
		return false;
		void saveWallsState()
		{
			wallsLastFrame.Clear();
			foreach (Wall item in wallsThisFrame)
			{
				wallsLastFrame.Add((item, item.transform.localPosition));
			}
		}
	}

	private void updateWalls(WallUpdateType wallUpdateType)
	{
		List<Wall> walls = getWalls();
		for (int i = 0; i < vertices.Count; i++)
		{
			Wall wall = getWall(i, walls);
			if (!(wall == null))
			{
				switch (wallUpdateType)
				{
				case WallUpdateType.Height:
					setWall(i, wall);
					break;
				case WallUpdateType.Thickness:
					wall.baseThickness = wallThickness;
					break;
				case WallUpdateType.ConnectWalls:
					wall.recalculate();
					break;
				}
			}
		}
	}

	private void updateCeilings()
	{
		foreach (Floor ceiling in getCeilings())
		{
			ceiling.transform.localPosition = Vector3.up * wallHeight;
		}
	}

	public bool tryGetWall(int edgeIndex, out Wall wall, List<Wall> walls = null)
	{
		wall = getWall(edgeIndex, walls);
		return wall != null;
	}

	public Wall getWall(int edgeIndex, List<Wall> walls = null)
	{
		if (walls == null)
		{
			walls = getWalls();
		}
		Vector3 vertex = polygonToWorldPosition(vertices[edgeIndex]);
		Vector3 vertex2 = polygonToWorldPosition(vertices[(edgeIndex + 1) % vertices.Count]);
		foreach (Wall wall in walls)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (Vector2 vertex4 in wall.vertices)
			{
				Vector3 vertex3 = wall.polygonToWorldPosition(vertex4);
				flag |= Polygon.isSameVertex(vertex3, vertex);
				flag2 |= Polygon.isSameVertex(vertex3, vertex2);
			}
			if (flag && flag2)
			{
				return wall;
			}
		}
		return null;
	}

	public Wall getClosestWall(int edgeIndex, List<Wall> walls = null)
	{
		int num = 0;
		int count = vertices.Count;
		for (int i = 0; i < count; i++)
		{
			int edgeIndex2 = (edgeIndex + num + count) % count;
			Wall wall = getWall(edgeIndex2, walls);
			if (wall != null)
			{
				return wall;
			}
			if (num == 0)
			{
				num++;
				continue;
			}
			if (num > 0)
			{
				num = -num;
				continue;
			}
			num = -num;
			num++;
		}
		return null;
	}

	public int getWallIndex(Wall wall)
	{
		if (wall == null)
		{
			return -1;
		}
		for (int i = 0; i < vertices.Count; i++)
		{
			Vector3 vertex = polygonToWorldPosition(vertices[i]);
			Vector3 vertex2 = polygonToWorldPosition(vertices[(i + 1) % vertices.Count]);
			bool flag = false;
			bool flag2 = false;
			foreach (Vector2 vertex4 in wall.vertices)
			{
				Vector3 vertex3 = wall.polygonToWorldPosition(vertex4);
				flag |= Polygon.isSameVertex(vertex3, vertex);
				flag2 |= Polygon.isSameVertex(vertex3, vertex2);
			}
			if (flag && flag2)
			{
				return i;
			}
		}
		return -1;
	}

	public void setWall(int edgeIndex, Wall wall)
	{
		Quaternion rotation = base.transform.rotation;
		Vector3 localScale = base.transform.localScale;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		Vector3 vector = polygonToWorldPosition(vertices[edgeIndex]);
		Vector3 vector2 = polygonToWorldPosition(vertices[(edgeIndex + 1) % vertices.Count]);
		wall.transform.position = (vector + vector2) / 2f;
		wall.transform.rotation = Quaternion.LookRotation(-Vector3.Cross(base.planeWorldNormal, vector2 - vector));
		wall.vertices = new List<Vector2>
		{
			wall.worldToPolygonPosition(vector),
			wall.worldToPolygonPosition(vector + Vector3.up * wallHeight),
			wall.worldToPolygonPosition(vector2 + Vector3.up * wallHeight),
			wall.worldToPolygonPosition(vector2)
		};
		base.transform.rotation = rotation;
		base.transform.localScale = localScale;
	}

	public List<Wall> getWalls()
	{
		return new List<Wall>(GetComponentsInChildren<Wall>());
	}

	public bool isCeiling()
	{
		return GetComponentsInParent<Floor>().Length > 1;
	}

	public List<Floor> getCeilings()
	{
		List<Floor> list = new List<Floor>(GetComponentsInChildren<Floor>());
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (!(list[num] != this))
			{
				list.RemoveAt(num);
				break;
			}
		}
		return list;
	}
}
