using System;
using UnityEngine;

public class TubeRenderer : MonoBehaviour
{
	public class TubeVertex
	{
		public Vector3 point = Vector3.zero;

		public float radius = 1f;

		public Color color = Color.white;

		public TubeVertex(Vector3 pt, float r, Color c)
		{
			point = pt;
			radius = r;
			color = c;
		}
	}

	public TubeVertex[] vertices;

	public Material material;

	public int crossSegments = 3;

	public float flatAtDistance = -1f;

	public float movePixelsForRebuild = 6f;

	public float maxRebuildTime = 0.1f;

	private Vector3[] crossPoints;

	private int lastCrossSegments;

	private Vector3 lastCameraPosition1;

	private Vector3 lastCameraPosition2;

	private float lastRebuildTime;

	private void Reset()
	{
		vertices = new TubeVertex[2]
		{
			new TubeVertex(Vector3.zero, 1f, Color.white),
			new TubeVertex(new Vector3(1f, 0f, 0f), 1f, Color.white)
		};
	}

	private void Start()
	{
		base.gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
	}

	private void LateUpdate()
	{
		if (vertices == null || vertices.Length <= 1)
		{
			GetComponent<Renderer>().enabled = false;
			return;
		}
		GetComponent<Renderer>().enabled = true;
		bool flag = false;
		if (vertices.Length > 1)
		{
			Vector3 vector = Camera.main.WorldToScreenPoint(vertices[0].point);
			lastCameraPosition1.z = 0f;
			Vector3 vector2 = Camera.main.WorldToScreenPoint(vertices[vertices.Length - 1].point);
			lastCameraPosition2.z = 0f;
			float magnitude = (lastCameraPosition1 - vector).magnitude;
			magnitude += (lastCameraPosition2 - vector2).magnitude;
			if (magnitude > movePixelsForRebuild || Time.time - lastRebuildTime > maxRebuildTime)
			{
				flag = true;
				lastCameraPosition1 = vector;
				lastCameraPosition2 = vector2;
			}
		}
		if (!flag)
		{
			return;
		}
		if (crossSegments != lastCrossSegments)
		{
			crossPoints = new Vector3[crossSegments];
			float num = (float)Math.PI * 2f / (float)crossSegments;
			for (int i = 0; i < crossSegments; i++)
			{
				crossPoints[i] = new Vector3(Mathf.Cos(num * (float)i), Mathf.Sin(num * (float)i), 0f);
			}
			lastCrossSegments = crossSegments;
		}
		Vector3[] array = new Vector3[vertices.Length * crossSegments];
		Vector2[] array2 = new Vector2[vertices.Length * crossSegments];
		Color[] array3 = new Color[vertices.Length * crossSegments];
		int[] array4 = new int[vertices.Length * crossSegments * 6];
		int[] array5 = new int[crossSegments];
		int[] array6 = new int[crossSegments];
		Quaternion quaternion = Quaternion.identity;
		for (int j = 0; j < vertices.Length; j++)
		{
			if (j < vertices.Length - 1)
			{
				quaternion = Quaternion.FromToRotation(Vector3.forward, vertices[j + 1].point - vertices[j].point);
			}
			for (int i = 0; i < crossSegments; i++)
			{
				int num2 = j * crossSegments + i;
				array[num2] = vertices[j].point + quaternion * crossPoints[i] * vertices[j].radius;
				array2[num2] = new Vector2((0f + (float)i) / (float)crossSegments, (0f + (float)j) / (float)vertices.Length);
				array3[num2] = vertices[j].color;
				array5[i] = array6[i];
				array6[i] = j * crossSegments + i;
			}
			if (j > 0)
			{
				for (int k = 0; k < crossSegments; k++)
				{
					int num3 = (j * crossSegments + k) * 6;
					array4[num3] = array5[k];
					array4[num3 + 1] = array5[(k + 1) % crossSegments];
					array4[num3 + 2] = array6[k];
					array4[num3 + 3] = array4[num3 + 2];
					array4[num3 + 4] = array4[num3 + 1];
					array4[num3 + 5] = array6[(k + 1) % crossSegments];
				}
			}
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		if (mesh == null)
		{
			mesh = new Mesh();
		}
		mesh.vertices = array;
		mesh.triangles = array4;
		mesh.RecalculateNormals();
		mesh.uv = array2;
	}

	private void SetPoints(Vector3[] points, float radius, Color col)
	{
		if (points.Length >= 2)
		{
			vertices = new TubeVertex[points.Length + 2];
			Vector3 vector = (points[0] - points[1]) * 0.01f;
			vertices[0] = new TubeVertex(vector + points[0], 0f, col);
			Vector3 vector2 = (points[points.Length - 1] - points[points.Length - 2]) * 0.01f;
			vertices[vertices.Length - 1] = new TubeVertex(vector2 + points[points.Length - 1], 0f, col);
			for (int i = 0; i < points.Length; i++)
			{
				vertices[i + 1] = new TubeVertex(points[i], radius, col);
			}
		}
	}
}
