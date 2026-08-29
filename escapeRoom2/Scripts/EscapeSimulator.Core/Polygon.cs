using System;
using System.Collections.Generic;
using Clipper2Lib;
using Poly2Tri;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class Polygon : MonoBehaviour
{
	public enum ExtrusionType
	{
		Center = 0,
		Front = 1,
		Back = 2
	}

	public enum CollisionType
	{
		Static = 0,
		Carving = 1,
		None = 2
	}

	[Header("References")]
	public MeshFilter meshFilter;

	public MeshCollider meshCollider;

	[Header("Shape")]
	public PlaneSystem planeSystem;

	public List<Vector2> vertices = new List<Vector2>();

	public List<Hole> holes = new List<Hole>();

	[Header("Extrusion")]
	[Min(0f)]
	public float baseThickness;

	[Min(0f)]
	public float extrusionThickness;

	public ExtrusionType extrusionType;

	[Header("Collision")]
	public CollisionType collisionType;

	public bool hasCollider = true;

	public bool forceConvexCollider;

	private float baseThicknessLastFrame;

	private float extrusionThicknessLastFrame;

	private ExtrusionType extrusionTypeLastFrame;

	private PlaneSystem planeSystemLastFrame;

	private List<Vector2> verticesInLastBuiltMesh = new List<Vector2>();

	private List<Hole> holesInLastBuiltMesh = new List<Hole>();

	private List<Hole> carvingsInLastBuiltMesh = new List<Hole>();

	private bool forceConvexColliderLastFrame;

	private readonly Dictionary<Vector3, int> vertexToIndex = new Dictionary<Vector3, int>();

	private readonly Dictionary<Vector3, Vector3> vertexToNextVertex = new Dictionary<Vector3, Vector3>();

	public float thickness => Mathf.Max(0f, baseThickness + extrusionThickness);

	public Vector3 planeWorldNormal => planeSystem switch
	{
		PlaneSystem.XY => base.transform.forward, 
		PlaneSystem.XZ => base.transform.up, 
		PlaneSystem.YZ => base.transform.right, 
		_ => Vector3.zero, 
	};

	public Vector3 planeWorldUp => planeSystem switch
	{
		PlaneSystem.XY => base.transform.up, 
		PlaneSystem.XZ => base.transform.forward, 
		PlaneSystem.YZ => base.transform.right, 
		_ => Vector3.zero, 
	};

	private Vector3 planeLocalNormal => planeSystem switch
	{
		PlaneSystem.XY => Vector3.back, 
		PlaneSystem.XZ => Vector3.up, 
		PlaneSystem.YZ => Vector3.left, 
		_ => Vector3.zero, 
	};

	protected virtual void LateUpdate()
	{
		bool num = (baseThickness != baseThicknessLastFrame) | (extrusionThickness != extrusionThicknessLastFrame) | (extrusionType != extrusionTypeLastFrame) | (planeSystem != planeSystemLastFrame) | !compareCarvings() | !compareHoles(holes, holesInLastBuiltMesh) | !compareVertices(vertices, verticesInLastBuiltMesh);
		if (num)
		{
			recalculateMesh();
		}
		if (num || forceConvexCollider != forceConvexColliderLastFrame)
		{
			recalculateCollision();
		}
	}

	public void recalculate()
	{
		recalculateMesh();
		recalculateCollision();
	}

	public void turnIntoCube(Vector3 size)
	{
		vertices = new List<Vector2>
		{
			new Vector2((0f - size.x) / 2f, (0f - size.y) / 2f),
			new Vector2((0f - size.x) / 2f, size.y / 2f),
			new Vector2(size.x / 2f, size.y / 2f),
			new Vector2(size.x / 2f, (0f - size.y) / 2f)
		};
		extrusionThickness = size.z;
	}

	private bool compareCarvings()
	{
		List<Carving> carvings = getCarvings();
		if (carvings.Count != carvingsInLastBuiltMesh.Count)
		{
			return false;
		}
		for (int i = 0; i < carvings.Count; i++)
		{
			if (!compareVertices(convertCarvingToHole(carvings[i]).vertices, carvingsInLastBuiltMesh[i].vertices))
			{
				return false;
			}
		}
		return true;
	}

	public static bool compareHoles(List<Hole> holes1, List<Hole> holes2)
	{
		if (holes1.Count != holes2.Count)
		{
			return false;
		}
		for (int i = 0; i < holes1.Count; i++)
		{
			if (!compareVertices(holes1[i].vertices, holes2[i].vertices))
			{
				return false;
			}
		}
		return true;
	}

	public static bool compareVertices(List<Vector2> vertices1, List<Vector2> vertices2)
	{
		if (vertices1.Count != vertices2.Count)
		{
			return false;
		}
		for (int i = 0; i < vertices1.Count; i++)
		{
			if (!(vertices1[i] == vertices2[i]))
			{
				return false;
			}
		}
		return true;
	}

	protected static bool isSameVertex(Vector3 vertex1, Vector3 vertex2)
	{
		return UnityUtils.closeEnough(vertex1, vertex2, 0.001f);
	}

	private void recalculateMesh()
	{
		if (meshCollider != null)
		{
			meshCollider.sharedMesh = null;
		}
		verticesInLastBuiltMesh = copyVertices();
		holesInLastBuiltMesh = copyHoles();
		carvingsInLastBuiltMesh = copyCarvings();
		baseThicknessLastFrame = baseThickness;
		extrusionThicknessLastFrame = extrusionThickness;
		extrusionTypeLastFrame = extrusionType;
		planeSystemLastFrame = planeSystem;
		if (Maths.isPolygonSelfIntersecting(vertices))
		{
			meshFilter.sharedMesh = null;
			return;
		}
		try
		{
			prepareMeshRecalculation();
			PathsD solutionPolygons = calculatePolygons();
			List<Vector3> list = new List<Vector3>();
			List<int> list2 = new List<int>();
			triangulatePolygons(solutionPolygons, list, list2);
			List<Vector3> list3 = new List<Vector3>();
			foreach (Vector3 item in list)
			{
				list3.Add(polygonToLocalPosition(item));
			}
			List<int> sideFaceTriangles = new List<int>();
			List<int> backFaceTriangles = new List<int>();
			if (thickness > 0f)
			{
				extrudePolygons(list3, list2, sideFaceTriangles, backFaceTriangles);
			}
			Vector2[] uvs = calculateUVs(list3);
			generateMesh(list3, uvs, list2, sideFaceTriangles, backFaceTriangles);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			meshFilter.sharedMesh = null;
		}
	}

	private void recalculateCollision()
	{
		forceConvexColliderLastFrame = forceConvexCollider;
		if (meshCollider == null)
		{
			return;
		}
		meshCollider.sharedMesh = meshFilter.sharedMesh;
		CollisionType collisionType = this.collisionType;
		if (hasCollider)
		{
			meshCollider.isTrigger = false;
			meshCollider.convex = false;
		}
		else
		{
			collisionType = CollisionType.None;
			meshCollider.convex = true;
			meshCollider.isTrigger = true;
		}
		switch (collisionType)
		{
		case CollisionType.Static:
		{
			if (!meshCollider.TryGetComponent<NavMeshModifier>(out var _))
			{
				meshCollider.gameObject.AddComponent<NavMeshModifier>();
			}
			if (meshCollider.TryGetComponent<NavMeshObstacle>(out var component6))
			{
				UnityEngine.Object.Destroy(component6);
			}
			break;
		}
		case CollisionType.Carving:
		{
			if (meshCollider.TryGetComponent<NavMeshModifier>(out var component3))
			{
				UnityEngine.Object.Destroy(component3);
			}
			if (meshCollider.TryGetComponent<NavMeshObstacle>(out var component4))
			{
				UnityEngine.Object.DestroyImmediate(component4);
			}
			NavMeshObstacle navMeshObstacle = meshCollider.gameObject.AddComponent<NavMeshObstacle>();
			navMeshObstacle.carving = true;
			navMeshObstacle.carveOnlyStationary = false;
			break;
		}
		case CollisionType.None:
		{
			if (meshCollider.TryGetComponent<NavMeshModifier>(out var component))
			{
				UnityEngine.Object.Destroy(component);
			}
			if (meshCollider.TryGetComponent<NavMeshObstacle>(out var component2))
			{
				UnityEngine.Object.Destroy(component2);
			}
			break;
		}
		}
		if (forceConvexCollider)
		{
			meshCollider.convex = true;
		}
	}

	protected virtual void prepareMeshRecalculation()
	{
	}

	private PathsD calculatePolygons()
	{
		PathsD subject = getMainPolygon();
		PathsD clip = getHolePolygons();
		return Clipper.Difference(subject, clip, FillRule.NonZero, 7);
		static PathD convertVerticesToPath(List<Vector2> polygonVertices, bool isClockwise)
		{
			if (Maths.isPolygonClockwise(polygonVertices) != isClockwise)
			{
				polygonVertices.Reverse();
			}
			PathD pathD = new PathD();
			foreach (Vector2 polygonVertex in polygonVertices)
			{
				pathD.Add(new PointD(polygonVertex.x, polygonVertex.y));
			}
			return pathD;
		}
		PathsD getHolePolygons()
		{
			PathsD pathsD = new PathsD();
			List<Hole> list = copyHoles();
			foreach (Carving carving in getCarvings())
			{
				list.Add(convertCarvingToHole(carving));
			}
			foreach (Hole item in list)
			{
				if (item.isValidPolygon())
				{
					pathsD.Add(convertVerticesToPath(item.vertices, isClockwise: false));
				}
			}
			return pathsD;
		}
		PathsD getMainPolygon()
		{
			return new PathsD { convertVerticesToPath(copyVertices(), isClockwise: true) };
		}
	}

	private void triangulatePolygons(PathsD solutionPolygons, List<Vector3> vertices, List<int> triangles)
	{
		vertexToIndex.Clear();
		foreach (PathD solutionPolygon in solutionPolygons)
		{
			if (isPolygonHole(solutionPolygon))
			{
				continue;
			}
			Poly2Tri.Polygon polygon = convertPathToPolygon(solutionPolygon);
			foreach (PathD solutionPolygon2 in solutionPolygons)
			{
				if (isPolygonHole(solutionPolygon2) && Clipper.PointInPolygon(solutionPolygon2[0], solutionPolygon) == PointInPolygonResult.IsInside)
				{
					polygon.AddHole(convertPathToPolygon(solutionPolygon2));
				}
			}
			DTSweepContext dTSweepContext = new DTSweepContext();
			dTSweepContext.PrepareTriangulation(polygon);
			DTSweep.Triangulate(dTSweepContext);
			foreach (DelaunayTriangle triangle in polygon.Triangles)
			{
				FixedArray3<TriangulationPoint> points = triangle.Points;
				foreach (TriangulationPoint item in points)
				{
					Vector3 vector = new Vector3((float)item.X, (float)item.Y, 0f);
					if (!vertexToIndex.ContainsKey(vector))
					{
						vertexToIndex[vector] = vertices.Count;
						vertices.Add(vector);
					}
				}
				triangles.Add(vertexToIndex[new Vector3((float)points[1].X, (float)points[1].Y, 0f)]);
				triangles.Add(vertexToIndex[new Vector3((float)points[0].X, (float)points[0].Y, 0f)]);
				triangles.Add(vertexToIndex[new Vector3((float)points[2].X, (float)points[2].Y, 0f)]);
			}
		}
		vertexToNextVertex.Clear();
		foreach (PathD solutionPolygon3 in solutionPolygons)
		{
			for (int i = 0; i < solutionPolygon3.Count; i++)
			{
				PointD pointD = solutionPolygon3[i];
				Vector3 key = polygonToLocalPosition(new Vector2((float)pointD.x, (float)pointD.y));
				if (!vertexToNextVertex.ContainsKey(key))
				{
					PointD pointD2 = solutionPolygon3[(i + 1) % solutionPolygon3.Count];
					Vector3 value = polygonToLocalPosition(new Vector2((float)pointD2.x, (float)pointD2.y));
					vertexToNextVertex[key] = value;
				}
			}
		}
		static Poly2Tri.Polygon convertPathToPolygon(PathD path)
		{
			List<PolygonPoint> list = new List<PolygonPoint>();
			foreach (PointD item2 in path)
			{
				list.Add(new PolygonPoint(item2.x, item2.y));
			}
			return new Poly2Tri.Polygon(list);
		}
		static bool isPolygonHole(PathD polygonPath)
		{
			List<Vector2> list = new List<Vector2>();
			foreach (PointD item3 in polygonPath)
			{
				list.Add(new Vector2((float)item3.x, (float)item3.y));
			}
			return Maths.isPolygonClockwise(list);
		}
	}

	private void extrudePolygons(List<Vector3> verts, List<int> frontFaceTriangles, List<int> sideFaceTriangles, List<int> backFaceTriangles)
	{
		int count = verts.Count;
		for (int i = 0; i < count; i++)
		{
			verts.Add(calculateExtrudedVertex(verts[i]));
		}
		for (int j = 0; j < frontFaceTriangles.Count; j += 3)
		{
			backFaceTriangles.Add(frontFaceTriangles[j + 2] + count);
			backFaceTriangles.Add(frontFaceTriangles[j + 1] + count);
			backFaceTriangles.Add(frontFaceTriangles[j] + count);
		}
		for (int k = 0; k < count; k++)
		{
			Vector3 vector = verts[k];
			Vector3 vertex = vertexToNextVertex[vector];
			Vector3 vertex2 = calculateExtrudedVertex(vector);
			Vector3 vertex3 = calculateExtrudedVertex(vertex);
			addVertex(vector);
			addVertex(vertex);
			addVertex(vertex2);
			addVertex(vertex);
			addVertex(vertex3);
			addVertex(vertex2);
		}
		void addVertex(Vector3 item)
		{
			sideFaceTriangles.Add(verts.Count);
			verts.Add(item);
		}
	}

	private Vector2[] calculateUVs(List<Vector3> verts)
	{
		Vector2[] array = new Vector2[verts.Count];
		int num = verts.Count;
		bool flag = thickness > 0f;
		if (flag)
		{
			num /= 8;
		}
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = localToPolygonPosition(verts[i]);
			float x = vector.x;
			float y = vector.y;
			array[i] = new Vector2(x, y);
		}
		if (flag)
		{
			for (int j = num; j < num * 2; j++)
			{
				Vector2 vector2 = localToPolygonPosition(verts[j]);
				float x2 = vector2.x;
				float y2 = vector2.y;
				array[j] = new Vector2(x2, y2);
			}
			for (int k = 0; k < num; k++)
			{
				Vector3 vector3 = verts[k];
				Vector3 vector4 = vertexToNextVertex[vector3];
				Vector3 vector5 = calculateExtrudedVertex(vector3);
				Vector3 vector6 = calculateExtrudedVertex(vector4);
				Vector3 normalized = Vector3.Cross(vector4 - vector3, vector5 - vector3).normalized;
				Vector3 normalized2 = (vector4 - vector3).normalized;
				Vector3 rhs = Vector3.Cross(normalized, normalized2);
				Vector2 vector7 = new Vector2(0f, 0f);
				Vector2 vector8 = new Vector2(Vector3.Dot(vector4 - vector3, normalized2), Vector3.Dot(vector4 - vector3, rhs));
				Vector2 vector9 = new Vector2(Vector3.Dot(vector5 - vector3, normalized2), Vector3.Dot(vector5 - vector3, rhs));
				Vector2 vector10 = new Vector2(Vector3.Dot(vector6 - vector3, normalized2), Vector3.Dot(vector6 - vector3, rhs));
				int num2 = num * 2 + k * 6;
				array[num2] = vector7;
				array[num2 + 1] = vector8;
				array[num2 + 2] = vector9;
				array[num2 + 3] = vector8;
				array[num2 + 4] = vector10;
				array[num2 + 5] = vector9;
			}
		}
		return array;
	}

	protected virtual Vector3 calculateExtrudedVertex(Vector3 vertex)
	{
		return vertex - planeLocalNormal * thickness;
	}

	private void generateMesh(List<Vector3> verts, Vector2[] uvs, List<int> frontFaceTriangles, List<int> sideFaceTriangles, List<int> backFaceTriangles)
	{
		if (meshFilter.sharedMesh == null)
		{
			Mesh mesh = new Mesh();
			mesh.MarkDynamic();
			meshFilter.sharedMesh = mesh;
		}
		offsetVertices(verts);
		meshFilter.sharedMesh.Clear();
		meshFilter.sharedMesh.SetVertices(verts);
		meshFilter.sharedMesh.SetUVs(0, uvs);
		meshFilter.sharedMesh.name = $"Polygon Mesh ({verts.Count} vertices)";
		meshFilter.sharedMesh.subMeshCount = 3;
		meshFilter.sharedMesh.SetTriangles(frontFaceTriangles, 0);
		meshFilter.sharedMesh.SetTriangles(sideFaceTriangles, 1);
		meshFilter.sharedMesh.SetTriangles(backFaceTriangles, 2);
		meshFilter.sharedMesh.RecalculateNormals();
		void offsetVertices(List<Vector3> list)
		{
			if (!(thickness <= 0f) && this.extrusionType != ExtrusionType.Back)
			{
				ExtrusionType extrusionType = this.extrusionType;
				float num = default(float);
				switch (extrusionType)
				{
				case ExtrusionType.Center:
					num = thickness / 2f;
					break;
				case ExtrusionType.Front:
					num = thickness;
					break;
				default:
					global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(extrusionType);
					break;
				}
				float num2 = num;
				Vector3 vector = planeLocalNormal * num2;
				for (int i = 0; i < list.Count; i++)
				{
					list[i] += vector;
				}
			}
		}
	}

	public Vector2 worldToPolygonPosition(Vector3 worldPosition)
	{
		Vector3 vector = base.transform.InverseTransformPoint(worldPosition);
		return planeSystem switch
		{
			PlaneSystem.XY => new Vector2(vector.x, vector.y), 
			PlaneSystem.XZ => new Vector2(vector.x, vector.z), 
			PlaneSystem.YZ => new Vector2(vector.y, vector.z), 
			_ => Vector2.zero, 
		};
	}

	public Vector3 polygonToWorldPosition(Vector2 polygonPosition)
	{
		Vector3 position = polygonToLocalPosition(polygonPosition);
		return base.transform.TransformPoint(position);
	}

	private Vector2 localToPolygonPosition(Vector3 localPosition)
	{
		return planeSystem switch
		{
			PlaneSystem.XY => new Vector2(localPosition.x, localPosition.y), 
			PlaneSystem.XZ => new Vector2(localPosition.x, localPosition.z), 
			PlaneSystem.YZ => new Vector2(localPosition.y, localPosition.z), 
			_ => Vector3.zero, 
		};
	}

	private Vector3 polygonToLocalPosition(Vector2 polygonPosition)
	{
		return planeSystem switch
		{
			PlaneSystem.XY => new Vector3(polygonPosition.x, polygonPosition.y, 0f), 
			PlaneSystem.XZ => new Vector3(polygonPosition.x, 0f, polygonPosition.y), 
			PlaneSystem.YZ => new Vector3(0f, polygonPosition.x, polygonPosition.y), 
			_ => Vector3.zero, 
		};
	}

	public float getAngle(int vertexIndex)
	{
		if (vertexIndex < 0 || vertexIndex >= vertices.Count)
		{
			Debug.LogError($"Invalid out-of-bounds vertex index: {vertexIndex}");
			return 0f;
		}
		List<Vector2> list = vertices;
		int count = list.Count;
		Vector2 vector = list[vertexIndex];
		Vector2 vector2 = list[(vertexIndex + 1) % count];
		Vector2 vector3 = list[(vertexIndex + count - 1) % count];
		if (!Maths.isPolygonClockwise(list))
		{
			Vector2 vector4 = vector3;
			Vector2 vector5 = vector2;
			vector2 = vector4;
			vector3 = vector5;
		}
		return (Vector2.SignedAngle(vector3 - vector, vector2 - vector) + 360f) % 360f;
	}

	public List<Vector2> copyVertices()
	{
		return new List<Vector2>(vertices);
	}

	public List<Hole> copyHoles()
	{
		List<Hole> list = new List<Hole>(holes.Count);
		foreach (Hole hole in holes)
		{
			list.Add(hole.copy());
		}
		return list;
	}

	public bool hasHole(Hole hole)
	{
		foreach (Hole hole2 in holes)
		{
			if (compareVertices(hole2.vertices, hole.vertices))
			{
				return true;
			}
		}
		return false;
	}

	private List<Hole> copyCarvings()
	{
		List<Hole> list = new List<Hole>();
		foreach (Carving carving in getCarvings())
		{
			list.Add(convertCarvingToHole(carving));
		}
		return list;
	}

	private Hole convertCarvingToHole(Carving carving)
	{
		List<Vector2> list = new List<Vector2>(carving.verticesParent.childCount);
		Plane plane = new Plane(planeWorldNormal, base.transform.position);
		foreach (Transform item in carving.verticesParent)
		{
			Vector3 worldPosition = plane.ClosestPointOnPlane(item.position);
			list.Add(worldToPolygonPosition(worldPosition));
		}
		return new Hole
		{
			vertices = list
		};
	}

	private List<Carving> getCarvings()
	{
		return new List<Carving>(base.transform.GetComponentsInChildren<Carving>(includeInactive: true));
	}
}
