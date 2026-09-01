using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecalHandler : SingleInstance<DecalHandler>
{
	private static Renderer[] affectedObjects;

	public LayerMask affectedLayers = -1;

	private bool hasFinishCallback;

	private static List<Vector3> bufVertices = new List<Vector3>();

	private static List<Vector3> bufNormals = new List<Vector3>();

	private static List<Vector2> bufTexCoords = new List<Vector2>();

	private static List<int> bufIndices = new List<int>();

	private static Vector3 vRight = Vector3.right;

	private static Vector3 vUp = Vector3.up;

	private static Vector3 vForward = Vector3.forward;

	private static Vector3 rvRight = -vRight;

	private static Vector3 rvUp = -vUp;

	private static Vector3 rvForward = -vForward;

	private static DecalPolygon poly = new DecalPolygon();

	private static Vector3[] vertices;

	private static int[] triangles;

	private static int startVertexCount;

	public override string Name
	{
		get
		{
			return "DecalGandler";
		}
	}

	protected void Start()
	{
		if (SingleInstance<DecalHandler>.Instance == this)
		{
			Object.DontDestroyOnLoad(SingleInstance<DecalHandler>.Instance);
			SceneManager.sceneLoaded += OnSceneLoaded;
			hasFinishCallback = true;
		}
		else
		{
			Object.DestroyImmediate(this);
		}
	}

	protected void OnDestroy()
	{
		if (hasFinishCallback)
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			hasFinishCallback = false;
		}
	}

	protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		ResetAffectedObjects();
	}

	public static void ResetAffectedObjects()
	{
		affectedObjects = null;
	}

	public static Renderer[] GetAffectedObjects()
	{
		if (!object.ReferenceEquals(affectedObjects, null) && affectedObjects.Length > 0)
		{
			return affectedObjects;
		}
		MeshRenderer[] componentsInChildren = WinCondition.Instance.GetComponentsInChildren<MeshRenderer>();
		List<Renderer> list = new List<Renderer>();
		MeshRenderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			if (!renderer.enabled || renderer.transform.IsChildOf(ReferenceMaster.physicsGoalInstance))
			{
				continue;
			}
			bool flag = IsLayerContains(SingleInstance<DecalHandler>.Instance.affectedLayers, renderer.gameObject.layer);
			if (!StatMaster.isMP && !flag)
			{
				continue;
			}
			if (StatMaster.isMP)
			{
				if (!flag)
				{
					continue;
				}
				GenericEntity componentInParent = renderer.GetComponentInParent<GenericEntity>();
				if (componentInParent != null && componentInParent.prefab.ignoreDecal)
				{
					continue;
				}
			}
			if (!(renderer.GetComponent<Decal>() != null))
			{
				list.Add(renderer);
			}
		}
		affectedObjects = list.ToArray();
		return affectedObjects;
	}

	private static bool IsLayerContains(LayerMask mask, int layer)
	{
		return (mask.value & (1 << layer)) != 0;
	}

	public static bool BuildDecalForObject(Decal decal, GameObject affectedObject)
	{
		Mesh sharedMesh = affectedObject.GetComponent<MeshFilter>().sharedMesh;
		if (object.ReferenceEquals(sharedMesh, null) || !sharedMesh.isReadable)
		{
			return false;
		}
		float maxAngle = decal.maxAngle;
		Plane plane = new Plane(vRight, vRight / 2f);
		Plane plane2 = new Plane(rvRight, rvRight / 2f);
		Plane plane3 = new Plane(vUp, vUp / 2f);
		Plane plane4 = new Plane(rvUp, rvUp / 2f);
		Plane plane5 = new Plane(vForward, vForward / 2f);
		Plane plane6 = new Plane(rvForward, rvForward / 2f);
		vertices = sharedMesh.vertices;
		triangles = sharedMesh.triangles;
		startVertexCount = bufVertices.Count;
		Matrix4x4 matrix4x = decal.transform.worldToLocalMatrix * affectedObject.transform.localToWorldMatrix;
		int count = bufIndices.Count;
		for (int i = 0; i < triangles.Length; i += 3)
		{
			int num = triangles[i];
			int num2 = triangles[i + 1];
			int num3 = triangles[i + 2];
			Vector3 vector = matrix4x.MultiplyPoint3x4(vertices[num]);
			Vector3 vector2 = matrix4x.MultiplyPoint3x4(vertices[num2]);
			Vector3 vector3 = matrix4x.MultiplyPoint3x4(vertices[num3]);
			Vector3 lhs = vector2 - vector;
			Vector3 rhs = vector3 - vector;
			Vector3 normalized = Vector3.Cross(lhs, rhs).normalized;
			if (!(Vector3.Dot(rvForward, normalized) <= maxAngle))
			{
				if (object.ReferenceEquals(poly, null))
				{
					poly = new DecalPolygon(vector, vector2, vector3);
				}
				else
				{
					poly.vertices[0] = vector;
					poly.vertices[1] = vector2;
					poly.vertices[2] = vector3;
					poly.Count = 3;
				}
				if (DecalPolygon.ClipPolygon(poly, plane) && DecalPolygon.ClipPolygon(poly, plane2) && DecalPolygon.ClipPolygon(poly, plane3) && DecalPolygon.ClipPolygon(poly, plane4) && DecalPolygon.ClipPolygon(poly, plane5) && DecalPolygon.ClipPolygon(poly, plane6))
				{
					AddPolygon(poly, normalized);
				}
			}
		}
		GenerateTexCoords(startVertexCount, decal.sprite);
		if (count == bufIndices.Count)
		{
			return false;
		}
		return true;
	}

	private static void AddPolygon(DecalPolygon poly, Vector3 normal)
	{
		int item = AddVertex(poly.vertices[0], normal);
		for (int i = 1; i < poly.Count - 1; i++)
		{
			int item2 = AddVertex(poly.vertices[i], normal);
			int item3 = AddVertex(poly.vertices[i + 1], normal);
			bufIndices.Add(item);
			bufIndices.Add(item2);
			bufIndices.Add(item3);
		}
	}

	private static int AddVertex(Vector3 vertex, Vector3 normal)
	{
		int num = FindVertex(vertex);
		if (num == -1)
		{
			bufVertices.Add(vertex);
			bufNormals.Add(normal);
			num = bufVertices.Count - 1;
		}
		else
		{
			Vector3 vector = bufNormals[num] + normal;
			bufNormals[num] = vector.normalized;
		}
		return num;
	}

	private static int FindVertex(Vector3 vertex)
	{
		for (int i = 0; i < bufVertices.Count; i++)
		{
			if (Vector3.Distance(bufVertices[i], vertex) < 0.01f)
			{
				return i;
			}
		}
		return -1;
	}

	private static void GenerateTexCoords(int start, Sprite sprite)
	{
		Rect rect = sprite.rect;
		rect.x /= sprite.texture.width;
		rect.y /= sprite.texture.height;
		rect.width /= sprite.texture.width;
		rect.height /= sprite.texture.height;
		for (int i = start; i < bufVertices.Count; i++)
		{
			Vector3 vector = bufVertices[i];
			Vector2 item = new Vector2(vector.x + 0.5f, vector.y + 0.5f);
			item.x = Mathf.Lerp(rect.xMin, rect.xMax, item.x);
			item.y = Mathf.Lerp(rect.yMin, rect.yMax, item.y);
			bufTexCoords.Add(item);
		}
	}

	public static void Push(float distance)
	{
		for (int i = 0; i < bufVertices.Count; i++)
		{
			Vector3 vector = bufNormals[i];
			List<Vector3> list2;
			List<Vector3> list = (list2 = bufVertices);
			int index2;
			int index = (index2 = i);
			Vector3 vector2 = list2[index2];
			list[index] = vector2 + vector * distance;
		}
	}

	public static Mesh CreateMesh()
	{
		if (bufIndices.Count == 0)
		{
			return null;
		}
		Mesh mesh = new Mesh();
		mesh.vertices = bufVertices.ToArray();
		mesh.normals = bufNormals.ToArray();
		mesh.uv = bufTexCoords.ToArray();
		mesh.uv2 = bufTexCoords.ToArray();
		mesh.triangles = bufIndices.ToArray();
		bufVertices.Clear();
		bufNormals.Clear();
		bufTexCoords.Clear();
		bufIndices.Clear();
		return mesh;
	}
}
