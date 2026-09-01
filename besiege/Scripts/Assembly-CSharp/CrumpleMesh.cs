using UnityEngine;

public class CrumpleMesh : MonoBehaviour
{
	public enum Axes
	{
		One = 0,
		All = 1
	}

	private class VertexPair
	{
		public Vector3 basePos;

		public float nx;

		public float ny;

		public float nz;

		public Vector3 noisePos;
	}

	public MeshFilter[] meshes = new MeshFilter[0];

	public float amount = 1f;

	public float scale = 1f;

	public float speed = 1f;

	public bool recalculateNormals;

	public bool recalculateBounds = true;

	public Axes noiseAxes = Axes.All;

	private Perlin noise;

	private Vector3[] vertices;

	private VertexPair vertex;

	private Vector3 basePos;

	private Vector3 noisePos;

	private float nx;

	private float ny;

	private float nz;

	private VertexPair[] baseVertices = new VertexPair[0];

	private float timex;

	private float timey;

	private float timez;

	private void Start()
	{
		noise = new Perlin();
		if (meshes.Length == 0)
		{
			meshes = new MeshFilter[1] { GetComponent<MeshFilter>() };
		}
		float num = Time.time * speed;
		timex = num + 0.1365143f;
		timey = num + 1.21688f;
		timez = num + 0.5564f;
		Vector3[] array = meshes[0].mesh.vertices;
		baseVertices = new VertexPair[array.Length];
		vertices = new Vector3[array.Length];
		float num2 = scale * base.transform.localScale.sqrMagnitude;
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = array[i];
			baseVertices[i] = new VertexPair
			{
				basePos = vector,
				nx = vector.x * num2,
				ny = vector.y * num2,
				nz = vector.z * num2,
				noisePos = vector * num2
			};
		}
		for (int j = 0; j < meshes.Length; j++)
		{
			meshes[j].mesh.MarkDynamic();
		}
	}

	private void Update()
	{
		float dt = Time.deltaTime * speed;
		if (noiseAxes == Axes.One)
		{
			CrumpleY(dt);
		}
		else
		{
			CrumpleXYZ(dt);
		}
		for (int i = 0; i < meshes.Length; i++)
		{
			meshes[i].mesh.vertices = vertices;
			if (recalculateNormals)
			{
				meshes[i].mesh.RecalculateNormals();
			}
			if (recalculateBounds)
			{
				meshes[i].mesh.RecalculateBounds();
			}
		}
	}

	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	private void CrumpleY(float dt)
	{
		timey += dt;
		for (int i = 0; i < vertices.Length; i++)
		{
			vertex = baseVertices[i];
			basePos = vertex.basePos;
			noisePos = vertex.noisePos;
			float num = noise.Noise(timey + noisePos.x, timey + noisePos.y, timey + noisePos.z) * amount;
			basePos.x += num;
			basePos.y += num;
			basePos.z += num;
			vertices[i] = basePos;
		}
	}

	private void CrumpleXYZ(float dt)
	{
		timex += dt;
		timey += dt;
		timez += dt;
		for (int i = 0; i < vertices.Length; i++)
		{
			vertex = baseVertices[i];
			basePos = vertex.basePos;
			nx = vertex.nx;
			ny = vertex.ny;
			nz = vertex.nz;
			basePos.x += noise.Noise(timex + nx, timex + ny, timex + nz) * amount;
			basePos.y += noise.Noise(timey + nx, timey + ny, timey + nz) * amount;
			basePos.z += noise.Noise(timez + nx, timez + ny, timez + nz) * amount;
			vertices[i] = basePos;
		}
	}
}
