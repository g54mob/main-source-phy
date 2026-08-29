using System;
using System.Collections.Generic;
using System.IO;
using Clipper2Lib;
using GLTFast.Export;
using Poly2Tri;
using UnityEngine;

[ExecuteInEditMode]
public class WallTool : MonoBehaviour
{
	public enum ExtrusionType
	{
		FromCenter = 0,
		FrontFace = 1,
		BackFace = 2
	}

	[Header("Extrusion")]
	[Min(0f)]
	public float extrudeThickness;

	public ExtrusionType extrusionType;

	private float frontFaceZ;

	[Header("Texturing")]
	public Vector2 textureScale = new Vector2(1f, 1f);

	public Vector2 textureOffset = new Vector2(0f, 0f);

	[Header("References")]
	public MeshFilter polygonMesh;

	public Transform subjectPolygonParent;

	public Transform subtractPolygonsParent;

	[Header("Export")]
	public bool pressThisToExport;

	public string exportedModelName = "Wall.glb";

	private PathsD solutionPolygons = new PathsD();

	private readonly Dictionary<Vector3, int> vertexToIndex = new Dictionary<Vector3, int>();

	private readonly Dictionary<Vector3, Vector3> vertexToNextVertex = new Dictionary<Vector3, Vector3>();

	private void Update()
	{
		PathsD subject = getSubjectPolygonPaths();
		PathsD clip = getSubtractPolygonPaths();
		solutionPolygons = Clipper.Difference(subject, clip, FillRule.NonZero);
		ExtrusionType extrusionType = this.extrusionType;
		float num = default(float);
		switch (extrusionType)
		{
		case ExtrusionType.FromCenter:
			num = (0f - extrudeThickness) / 2f;
			break;
		case ExtrusionType.FrontFace:
			num = 0f - extrudeThickness;
			break;
		case ExtrusionType.BackFace:
			num = 0f;
			break;
		default:
			global::_003CPrivateImplementationDetails_003E.ThrowSwitchExpressionException(extrusionType);
			break;
		}
		frontFaceZ = num;
		List<Vector3> vertices = new List<Vector3>();
		List<int> list = new List<int>();
		triangulatePolygons(vertices, list);
		if (extrudeThickness <= 0f)
		{
			Vector2[] uvs = calculateUVs(vertices);
			generateMesh(vertices, uvs, list, null, null);
		}
		else
		{
			List<int> sideFaceTriangles = new List<int>();
			List<int> backFaceTriangles = new List<int>();
			extrudePolygons(vertices, list, sideFaceTriangles, backFaceTriangles);
			Vector2[] uvs2 = calculateUVs(vertices);
			generateMesh(vertices, uvs2, list, sideFaceTriangles, backFaceTriangles);
		}
		if (pressThisToExport)
		{
			pressThisToExport = false;
			exportMesh();
		}
		PathsD getSubjectPolygonPaths()
		{
			PathsD pathsD = new PathsD();
			PathD pathD = new PathD();
			foreach (Vector3 polygonPoint in getPolygonPoints(subjectPolygonParent))
			{
				pathD.Add(new PointD(polygonPoint.x, polygonPoint.y));
			}
			pathsD.Add(pathD);
			return pathsD;
		}
		PathsD getSubtractPolygonPaths()
		{
			PathsD pathsD = new PathsD();
			foreach (Transform item in subtractPolygonsParent)
			{
				PathD pathD = new PathD();
				foreach (Vector3 polygonPoint2 in getPolygonPoints(item))
				{
					pathD.Add(new PointD(polygonPoint2.x, polygonPoint2.y));
				}
				pathsD.Add(pathD);
			}
			return pathsD;
		}
	}

	private void triangulatePolygons(List<Vector3> vertices, List<int> triangles)
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
			try
			{
				DTSweepContext dTSweepContext = new DTSweepContext();
				dTSweepContext.PrepareTriangulation(polygon);
				DTSweep.Triangulate(dTSweepContext);
				foreach (DelaunayTriangle triangle in polygon.Triangles)
				{
					FixedArray3<TriangulationPoint> points = triangle.Points;
					foreach (TriangulationPoint item in points)
					{
						Vector3 vector = new Vector3((float)item.X, (float)item.Y, frontFaceZ);
						if (!vertexToIndex.ContainsKey(vector))
						{
							vertexToIndex[vector] = vertices.Count;
							vertices.Add(vector);
						}
					}
					triangles.Add(vertexToIndex[new Vector3((float)points[1].X, (float)points[1].Y, frontFaceZ)]);
					triangles.Add(vertexToIndex[new Vector3((float)points[0].X, (float)points[0].Y, frontFaceZ)]);
					triangles.Add(vertexToIndex[new Vector3((float)points[2].X, (float)points[2].Y, frontFaceZ)]);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
		vertexToNextVertex.Clear();
		foreach (PathD solutionPolygon3 in solutionPolygons)
		{
			for (int i = 0; i < solutionPolygon3.Count; i++)
			{
				PointD pointD = solutionPolygon3[i];
				Vector3 key = new Vector3((float)pointD.x, (float)pointD.y, frontFaceZ);
				if (!vertexToNextVertex.ContainsKey(key))
				{
					PointD pointD2 = solutionPolygon3[(i + 1) % solutionPolygon3.Count];
					Vector3 value = new Vector3((float)pointD2.x, (float)pointD2.y, frontFaceZ);
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

	private void extrudePolygons(List<Vector3> vertices, List<int> frontFaceTriangles, List<int> sideFaceTriangles, List<int> backFaceTriangles)
	{
		int count = vertices.Count;
		for (int i = 0; i < count; i++)
		{
			vertices.Add(vertices[i] + Vector3.forward * extrudeThickness);
		}
		for (int j = 0; j < frontFaceTriangles.Count; j += 3)
		{
			backFaceTriangles.Add(frontFaceTriangles[j + 2] + count);
			backFaceTriangles.Add(frontFaceTriangles[j + 1] + count);
			backFaceTriangles.Add(frontFaceTriangles[j] + count);
		}
		for (int k = 0; k < count; k++)
		{
			Vector3 vector = vertices[k];
			Vector3 vector2 = vertexToNextVertex[vector];
			Vector3 vertex = vector + Vector3.forward * extrudeThickness;
			Vector3 vertex2 = vector2 + Vector3.forward * extrudeThickness;
			addVertex(vector);
			addVertex(vector2);
			addVertex(vertex);
			addVertex(vector2);
			addVertex(vertex2);
			addVertex(vertex);
		}
		void addVertex(Vector3 item)
		{
			sideFaceTriangles.Add(vertices.Count);
			vertices.Add(item);
		}
	}

	private Vector2[] calculateUVs(List<Vector3> vertices)
	{
		Vector2[] array = new Vector2[vertices.Count];
		int num = vertices.Count;
		bool flag = extrudeThickness > 0f;
		if (flag)
		{
			num /= 8;
		}
		for (int i = 0; i < num; i++)
		{
			float x = vertices[i].x * textureScale.x + textureOffset.x;
			float y = vertices[i].y * textureScale.y + textureOffset.y;
			array[i] = new Vector2(x, y);
		}
		if (flag)
		{
			for (int j = num; j < num * 2; j++)
			{
				float x2 = vertices[j].x * textureScale.x + textureOffset.x;
				float y2 = vertices[j].y * textureScale.y + textureOffset.y;
				array[j] = new Vector2(x2, y2);
			}
			for (int k = 0; k < num; k++)
			{
				Vector3 vector = vertices[k];
				Vector3 vector2 = vertexToNextVertex[vector];
				Vector3 vector3 = vector + Vector3.forward * extrudeThickness;
				Vector3 vector4 = vector2 + Vector3.forward * extrudeThickness;
				Vector3 normalized = Vector3.Cross(vector2 - vector, vector3 - vector).normalized;
				Vector3 normalized2 = (vector2 - vector).normalized;
				Vector3 rhs = Vector3.Cross(normalized, normalized2);
				Vector2 vector5 = new Vector2(0f, 0f);
				Vector2 vector6 = new Vector2(Vector3.Dot(vector2 - vector, normalized2), Vector3.Dot(vector2 - vector, rhs));
				Vector2 vector7 = new Vector2(Vector3.Dot(vector3 - vector, normalized2), Vector3.Dot(vector3 - vector, rhs));
				Vector2 vector8 = new Vector2(Vector3.Dot(vector4 - vector, normalized2), Vector3.Dot(vector4 - vector, rhs));
				int num2 = num * 2 + k * 6;
				array[num2] = vector5;
				array[num2 + 1] = vector6;
				array[num2 + 2] = vector7;
				array[num2 + 3] = vector6;
				array[num2 + 4] = vector8;
				array[num2 + 5] = vector7;
			}
		}
		return array;
	}

	private void generateMesh(List<Vector3> vertices, Vector2[] uvs, List<int> frontFaceTriangles, List<int> sideFaceTriangles, List<int> backFaceTriangles)
	{
		if (polygonMesh.sharedMesh == null)
		{
			polygonMesh.mesh = new Mesh();
		}
		polygonMesh.mesh.Clear();
		polygonMesh.mesh.SetVertices(vertices);
		polygonMesh.mesh.SetUVs(0, uvs);
		polygonMesh.mesh.name = "Polygon Mesh";
		polygonMesh.mesh.subMeshCount = 3;
		polygonMesh.mesh.SetTriangles(frontFaceTriangles, 0);
		polygonMesh.mesh.SetTriangles(sideFaceTriangles, 1);
		polygonMesh.mesh.SetTriangles(backFaceTriangles, 2);
	}

	private async void exportMesh()
	{
		try
		{
			GameObjectExport gameObjectExport = new GameObjectExport(new ExportSettings
			{
				Format = GltfFormat.Binary
			});
			gameObjectExport.AddScene(new GameObject[1] { polygonMesh.gameObject });
			string filePath = Path.Combine(Application.persistentDataPath, exportedModelName);
			if (await gameObjectExport.SaveToFileAndDispose(filePath))
			{
				Debug.Log("Successfully exported GLTF to '" + filePath + "'.");
			}
			else
			{
				Debug.LogError("Something went wrong while exporting GLTF '" + filePath + "'.");
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	private void OnDrawGizmos()
	{
		foreach (Transform item in subtractPolygonsParent)
		{
			drawPolygon(getPolygonPoints(item), Color.red);
		}
		drawSolutionPolygons();
	}

	private void drawPolygon(List<Vector3> polygonPoints, Color color)
	{
		Gizmos.color = color;
		for (int i = 0; i < polygonPoints.Count; i++)
		{
			Vector3 vector = polygonPoints[i];
			Gizmos.DrawSphere(vector, 0.05f);
			Vector3 to = polygonPoints[(i + 1) % polygonPoints.Count];
			Gizmos.DrawLine(vector, to);
		}
	}

	private void drawSolutionPolygons()
	{
		foreach (PathD solutionPolygon in solutionPolygons)
		{
			List<Vector3> list = new List<Vector3>();
			foreach (PointD item in solutionPolygon)
			{
				list.Add(new Vector3((float)item.x, (float)item.y, 0f));
			}
			drawPolygon(list, Color.deepSkyBlue);
		}
	}

	private static List<Vector3> getPolygonPoints(Transform polygonParent)
	{
		List<Vector3> list = new List<Vector3>();
		foreach (Transform item in polygonParent)
		{
			list.Add(item.position);
		}
		return list;
	}
}
