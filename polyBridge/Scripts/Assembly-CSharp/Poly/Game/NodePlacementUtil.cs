using System.Collections.Generic;
using Poly.Base;
using Poly.Collide;
using Poly.Collide.Unity;
using Poly.Draw;
using Poly.Math;
using Poly.Physics;
using Poly.PortToJS;
using UnityEngine;

namespace Poly.Game
{
	public class NodePlacementUtil : PolyBehaviour
	{
		public bool visualize;

		public int updateFrequency = 5;

		public bool test;

		public bool testJS;

		public float segmentLength;

		public bool testBox;

		public bool freezeMouseShape;

		private PolygonShape mouseShape;

		private float timeSinceLastUpdate = float.MaxValue;

		private Vector2 lastNodePosition;

		private List<PolygonShape> shapes = new List<PolygonShape>();

		private List<PolygonShape> nonTerrainShapes = new List<PolygonShape>();

		public void Init()
		{
			TerrainIsland[] terrainIslands = Object.FindObjectsOfType<TerrainIsland>();
			CacheTerrainVolumes(terrainIslands);
			PlaceableCollisionInfo[] placeableInfos = Object.FindObjectsOfType<PlaceableCollisionInfo>();
			CachePlaceableVolumes(placeableInfos);
			CustomShapeCollisionInfo[] csInfos = Object.FindObjectsOfType<CustomShapeCollisionInfo>();
			CacheCustomShapeVolumes(csInfos);
			Vehicle[] vehicles = Object.FindObjectsOfType<Vehicle>();
			CacheVehicleAndOtherVolumes(vehicles);
		}

		public void Clear()
		{
			shapes.Clear();
			nonTerrainShapes.Clear();
		}

		public bool OverlapShape(PolygonShape overlapShape, bool testAgainstTerrain)
		{
			List<PolygonShape> list = (testAgainstTerrain ? shapes : nonTerrainShapes);
			bool flag = false;
			int num = 0;
			while (!flag && num < list.Count)
			{
				PolygonShape polyB = list[num];
				PolygonCollisionProcess.Init(ref overlapShape, ref Transform2.identity, ref polyB, ref Transform2.identity, out var process);
				PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint, doAveragePointPositions: false);
				flag = closestPoint.distance < 0f;
				num++;
			}
			return flag;
		}

		private void OnEnable()
		{
			Init();
		}

		private void OnDisable()
		{
			Clear();
		}

		private void Update()
		{
			Visualize();
			Test();
			TestJS();
		}

		private void Visualize()
		{
			if (visualize && Time.timeScale == 0f)
			{
				timeSinceLastUpdate += Time.unscaledDeltaTime;
				if (updateFrequency != 0 && 1f / (float)updateFrequency < timeSinceLastUpdate)
				{
					Clear();
					Init();
					timeSinceLastUpdate = 0f;
				}
				GlDrawer.Clear();
				{
					foreach (PolygonShape shape in shapes)
					{
						shape.DrawGizmos(Transform2.identity);
					}
					return;
				}
			}
			timeSinceLastUpdate = float.MaxValue;
		}

		private void Test()
		{
			if (!test)
			{
				return;
			}
			float radius = 0.1f;
			bool flag = segmentLength <= 0f || !testBox;
			if (!freezeMouseShape)
			{
				if (segmentLength <= 0f)
				{
					Vector2 vector = Cameras.MainCamera().ScreenToWorldPoint(Input.mousePosition);
					PolygonShape polygonShape = PolygonShape.FromCircle(vector, radius);
					lastNodePosition = vector;
					mouseShape = polygonShape;
				}
				else
				{
					Vector2 vector2 = lastNodePosition;
					Vector2 vector3 = Cameras.MainCamera().ScreenToWorldPoint(Input.mousePosition);
					float num = Vector2.Distance(vector2, vector3);
					vector2 = (lastNodePosition = ((!(num > 1E-06f)) ? (vector3 + Vector2.up * segmentLength) : (vector3 + (vector2 - vector3) * segmentLength / num)));
					if (testBox)
					{
						Vec2 center = 0.5f * (vector2 + vector3);
						Vec2 size = Vec2.CoordAbs(vector2 - vector3);
						PolygonShape polygonShape2 = PolygonShape.FromRect(center, size);
						polygonShape2.radius = 0f;
						mouseShape = polygonShape2;
					}
					else
					{
						PolygonShape polygonShape3 = PolygonShape.FromSegment(vector2, vector3, radius);
						mouseShape = polygonShape3;
						flag = true;
					}
				}
			}
			Color tint = (OverlapShape(mouseShape, !flag) ? Color.red : Color.white);
			mouseShape.DrawGizmos(Transform2.identity, tint);
		}

		private void TestJS()
		{
			if (testJS)
			{
				PolygonData[] polygons = PolygonDataUtil.GatherPolygonDataFromScene();
				float radius = GameSettings.NodeRadius() - 0.001f;
				PolygonShape polygonShape;
				bool flag;
				if (segmentLength <= 0f)
				{
					Vector2 vector = Cameras.MainCamera().ScreenToWorldPoint(Input.mousePosition);
					polygonShape = PolygonShape.FromCircle(vector, radius);
					flag = PolygonDataUtil.DoesNodeOverlapAnyShape(polygons, vector);
					lastNodePosition = vector;
				}
				else
				{
					Vector2 vector2 = lastNodePosition;
					Vector2 vector3 = Cameras.MainCamera().ScreenToWorldPoint(Input.mousePosition);
					float num = Vector2.Distance(vector2, vector3);
					vector2 = ((!(num > 1E-06f)) ? (vector3 + Vector2.up * segmentLength) : (vector3 + (vector2 - vector3) * segmentLength / num));
					polygonShape = PolygonShape.FromSegment(vector2, vector3, radius);
					flag = PolygonDataUtil.DoesRoadEdgeOverlapAnyShape(polygons, vector2, vector3);
					lastNodePosition = vector2;
				}
				Color tint = (flag ? Color.red : Color.white);
				polygonShape.DrawGizmos(Transform2.identity, tint);
			}
		}

		private void CacheTerrainVolumes(TerrainIsland[] terrainIslands)
		{
			foreach (TerrainIsland terrainIsland in terrainIslands)
			{
				TerrainCollisionInfo componentInChildren = terrainIsland.GetComponentInChildren<TerrainCollisionInfo>(includeInactive: true);
				if ((bool)componentInChildren)
				{
					PolygonShape[] collection = componentInChildren.CreatePolygonShapes_ForBuildMode(terrainIsland.m_Flipped);
					shapes.AddRange(collection);
				}
			}
		}

		private void CachePlaceableVolumes(PlaceableCollisionInfo[] placeableInfos)
		{
			for (int i = 0; i < placeableInfos.Length; i++)
			{
				PolygonShape[] collection = placeableInfos[i].CreatePolygonShapes_ForBuildMode();
				shapes.AddRange(collection);
				nonTerrainShapes.AddRange(collection);
			}
		}

		private void CacheCustomShapeVolumes(CustomShapeCollisionInfo[] csInfos)
		{
			for (int i = 0; i < csInfos.Length; i++)
			{
				PolygonShape[] collection = csInfos[i].CreatePolygonShapes_ForBuildMode(calculateMinimumStrengthHint: false);
				shapes.AddRange(collection);
				nonTerrainShapes.AddRange(collection);
			}
		}

		private void CacheVehicleAndOtherVolumes(Vehicle[] vehicles)
		{
			foreach (Vehicle vehicle in vehicles)
			{
				PolygonCollider[] componentsInChildren = vehicle.m_PhysicsPrefabInstantiated.GetComponent<Poly.Physics.Vehicle>().GetComponentsInChildren<PolygonCollider>();
				Transform2 shapeOrigin = ((Transform2)vehicle.transform).inverse_unoptimized;
				PolygonCollider[] array = componentsInChildren;
				for (int j = 0; j < array.Length; j++)
				{
					PolygonShape[] collection = array[j].CreateConvexPolygons(in shapeOrigin, vehicle.m_Flipped);
					shapes.AddRange(collection);
					nonTerrainShapes.AddRange(collection);
				}
			}
		}
	}
}
