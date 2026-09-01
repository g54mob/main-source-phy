using System.Collections.Generic;
using System.Linq;
using Poly.Collide;

namespace Poly.PortToJS
{
	public static class PolygonDataUtil
	{
		public static PolygonData[] GatherPolygonDataFromScene()
		{
			List<PolygonData> list = new List<PolygonData>();
			foreach (PolygonShape item6 in TerrainIslands.m_Terrains.SelectMany((TerrainIsland t) => t.m_PolygonShapes))
			{
				PolygonData item = CreatePolygonDataFromShape(item6, QueryFilter.TestAgainstNodes);
				list.Add(item);
			}
			foreach (PolygonShape item7 in Rocks.m_Rocks.SelectMany((Rock r) => r.m_PolygonShapes))
			{
				PolygonData item2 = CreatePolygonDataFromShape(item7, QueryFilter.TestAgainstNodes);
				list.Add(item2);
			}
			foreach (PolygonShape item8 in FlyingObjects.m_FlyingObjects.SelectMany((FlyingObject f) => f.m_PolygonShapes))
			{
				PolygonData item3 = CreatePolygonDataFromShape(item8, QueryFilter.TestAgainstNodes);
				list.Add(item3);
			}
			foreach (PolygonShape item9 in Vehicles.m_Vehicles.SelectMany((Vehicle v) => v.m_PolygonShapes))
			{
				PolygonData item4 = CreatePolygonDataFromShape(item9, QueryFilter.TestAgainstRoadEdges);
				list.Add(item4);
			}
			foreach (CustomShape shape in CustomShapes.m_Shapes)
			{
				foreach (PolygonShape polygonShape in shape.m_PolygonShapes)
				{
					QueryFilter filter = (QueryFilter)((shape.m_CollidesWithNodes ? 1 : 0) + (shape.m_CollidesWithRoad ? 2 : 0));
					PolygonData item5 = CreatePolygonDataFromShape(polygonShape, filter);
					list.Add(item5);
				}
			}
			return list.ToArray();
		}

		private static PolygonData CreatePolygonDataFromShape(PolygonShape shape, QueryFilter filter)
		{
			return new PolygonData
			{
				verts = shape.verts,
				invLengths = shape.invLengths,
				radius = shape.radius,
				filter = filter
			};
		}

		public static bool DoesNodeOverlapAnyShape(PolygonData[] polygons, Vec2 nodePosition)
		{
			float radius = GameSettings.NodeRadius() - 0.001f;
			PolygonData polyB = PolygonData.FromCircle(nodePosition, radius);
			foreach (PolygonData polygonData in polygons)
			{
				if ((polygonData.filter & QueryFilter.TestAgainstNodes) == QueryFilter.TestAgainstNodes)
				{
					PolygonCollisionProcess process = PolygonData.CreateCollisionProcess(polygonData, polyB);
					PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint, doAveragePointPositions: false);
					if (closestPoint.distance < 0f)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool DoesRoadEdgeOverlapAnyShape(PolygonData[] polygons, Vec2 node0, Vec2 node1)
		{
			float radius = GameSettings.NodeRadius() - 0.001f;
			PolygonData polyB = PolygonData.FromSegment(node0, node1, radius);
			foreach (PolygonData polygonData in polygons)
			{
				if ((polygonData.filter & QueryFilter.TestAgainstRoadEdges) == QueryFilter.TestAgainstRoadEdges)
				{
					PolygonCollisionProcess process = PolygonData.CreateCollisionProcess(polygonData, polyB);
					PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint, doAveragePointPositions: false);
					if (closestPoint.distance < 0f)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
