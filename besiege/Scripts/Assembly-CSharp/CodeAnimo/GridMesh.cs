using System;
using CodeAnimo.UnityExtensionMethods;
using UnityEngine;

namespace CodeAnimo
{
	public class GridMesh : MonoBehaviour
	{
		private const int maxMeshSize = 65535;

		public int gridUnitCountU = 64;

		public int gridUnitCountV = 64;

		public int groupUnitCountU = 512;

		public int groupUnitCountV = 512;

		public int offsetU;

		public int offsetV;

		public float gridUnitSizeU = 1f;

		public float gridUnitSizeV = 1f;

		public string meshName = "Custom Grid Mesh";

		public bool defaultNormalGeneration;

		[HideInInspector]
		[SerializeField]
		protected Mesh generatedMesh;

		public GridHeightData heightData;

		public bool hasMesh
		{
			get
			{
				return generatedMesh != null;
			}
		}

		public event EventHandler meshGenerated;

		protected void Reset()
		{
			AddMissingComponents();
			SetupEvents();
		}

		private void OnEnable()
		{
			SetupEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		protected virtual void OnDestroy()
		{
		}

		protected void AddMissingComponents()
		{
			base.gameObject.AddComponentIfMissing<MeshFilter>();
			base.gameObject.AddComponentIfMissing<MeshRenderer>();
		}

		public void SetupEvents()
		{
			if (!(heightData == null))
			{
				heightData.subscribeToHeightDataUpdated(HandleHeightDataUpdated);
			}
		}

		public void UnsubscribeEvents()
		{
			if (!(heightData == null))
			{
				heightData.unsubscribeFromHeightDataUpdated(HandleHeightDataUpdated);
			}
		}

		private void HandleHeightDataUpdated(object sender, EventArgs e)
		{
			GenerateGrid();
		}

		public virtual void GenerateGrid()
		{
			int num = Mathf.Min(groupUnitCountU - offsetU, gridUnitCountU);
			int num2 = Mathf.Min(groupUnitCountV - offsetV, gridUnitCountV);
			if (num <= 0 || num2 <= 0)
			{
				throw new ArgumentOutOfRangeException("A mesh that has no width is a bit useless...");
			}
			int num3 = num + 1;
			int num4 = num2 + 1;
			if (num3 * num4 > 65535)
			{
				throw new UnityException("Mesh is too large: " + num3 * num4 + " vertices. Maximum number of vertices in Unity is " + 65535);
			}
			int num5 = num3 * num4;
			Vector3[] array = new Vector3[num5];
			Vector2[] array2 = new Vector2[num5];
			Vector3[] array3 = new Vector3[num5];
			Vector4[] array4 = new Vector4[num5];
			float num6 = 1f / (float)groupUnitCountU;
			float num7 = 1f / (float)groupUnitCountV;
			float num8 = num6 * (float)offsetU;
			float num9 = num7 * (float)offsetV;
			for (int i = 0; i < num3; i++)
			{
				for (int j = 0; j < num4; j++)
				{
					int num10 = j * num3 + i;
					array[num10] = CalculateVertex(i, j);
					array2[num10] = new Vector2(num8 + (float)i * num6, num9 + (float)j * num7);
					array4[num10] = CalculateTangent(i, j);
				}
			}
			int[] triangles = ConstructTriangles(num3, num4);
			array3 = CalculateNormals(num3, num4);
			Mesh mesh = CreateMesh(array, array2, triangles, array3, array4);
			if (defaultNormalGeneration)
			{
				mesh.RecalculateNormals();
			}
			GetComponent<MeshFilter>().mesh = mesh;
			generatedMesh = mesh;
			OnMeshGenerated(null);
		}

		public void DestroyGeneratedMesh()
		{
			if (!(generatedMesh == null))
			{
				UnityEngine.Object.DestroyImmediate(generatedMesh);
			}
		}

		private Mesh CreateMesh(Vector3[] vertices, Vector2[] uvCoords, int[] triangles, Vector3[] normals, Vector4[] tangents)
		{
			Mesh mesh;
			if (generatedMesh != null)
			{
				mesh = generatedMesh;
				mesh.Clear();
			}
			else
			{
				mesh = new Mesh();
			}
			mesh.vertices = vertices;
			mesh.uv = uvCoords;
			mesh.triangles = triangles;
			mesh.normals = normals;
			mesh.tangents = tangents;
			mesh.name = meshName;
			return mesh;
		}

		private Vector3 CalculateVertex(int u, int v)
		{
			float vertexHeight = GetVertexHeight(u, v);
			return new Vector3(gridUnitSizeU * (float)u, vertexHeight, gridUnitSizeV * (float)v);
		}

		private float GetVertexHeight(int u, int v)
		{
			float result = 0f;
			if (heightData != null)
			{
				u += offsetU;
				v += offsetV;
				if (u < 0)
				{
					u = 0;
				}
				if (v < 0)
				{
					v = 0;
				}
				if (u > heightData.maximumU)
				{
					u = heightData.maximumU;
				}
				if (v > heightData.maximumV)
				{
					v = heightData.maximumV;
				}
				result = heightData.getGridHeight(u, v);
			}
			return result;
		}

		private Vector4 CalculateTangent(int u, int v)
		{
			return new Vector4(0f, 0f, 0f, 0f);
		}

		private int[] ConstructTriangles(int uCount, int vCount)
		{
			int num = 2 * ((uCount - 1) * (vCount - 1));
			int[] array = new int[3 * num];
			int num2 = 0;
			for (int i = 0; i < vCount - 1; i++)
			{
				for (int j = 0; j < uCount - 1; j++)
				{
					array[num2] = i * uCount + j;
					array[num2 + 1] = (i + 1) * uCount + j;
					array[num2 + 2] = i * uCount + j + 1;
					array[num2 + 3] = (i + 1) * uCount + j;
					array[num2 + 4] = (i + 1) * uCount + j + 1;
					array[num2 + 5] = i * uCount + j + 1;
					num2 += 6;
				}
			}
			return array;
		}

		private Vector3[] CalculateNormals(int vertexCountU, int vertexCountV)
		{
			int num = vertexCountU * vertexCountV;
			Vector3[] array = new Vector3[num];
			for (int i = 0; i < vertexCountU; i++)
			{
				for (int j = 0; j < vertexCountV; j++)
				{
					int num2 = j * vertexCountU + i;
					float vertexHeight = GetVertexHeight(i, j);
					float vertexHeight2 = GetVertexHeight(i + 1, j + 1);
					float vertexHeight3 = GetVertexHeight(i, j + 1);
					float vertexHeight4 = GetVertexHeight(i - 1, j);
					float vertexHeight5 = GetVertexHeight(i + 1, j);
					float vertexHeight6 = GetVertexHeight(i, j - 1);
					float vertexHeight7 = GetVertexHeight(i + 1, j - 1);
					Vector3 vector = new Vector3(vertexHeight2 - vertexHeight3, 1f, vertexHeight - vertexHeight3);
					Vector3 vector2 = new Vector3(vertexHeight4 - vertexHeight, 1f, vertexHeight4 - vertexHeight2);
					Vector3 vector3 = new Vector3(vertexHeight - vertexHeight5, 1f, vertexHeight - vertexHeight3);
					Vector3 vector4 = new Vector3(vertexHeight4 - vertexHeight, 1f, vertexHeight6 - vertexHeight);
					Vector3 vector5 = new Vector3(vertexHeight6 - vertexHeight7, 1f, vertexHeight6 - vertexHeight);
					Vector3 vector6 = new Vector3(vertexHeight - vertexHeight5, 1f, vertexHeight7 - vertexHeight5);
					Vector3 vector7 = vector + vector2 + vector3 + vector4 + vector5 + vector6;
					vector7.Normalize();
					array[num2] = vector7;
				}
			}
			return array;
		}

		private void OnMeshGenerated(EventArgs e)
		{
			if (this.meshGenerated != null)
			{
				this.meshGenerated(this, e);
			}
		}

		public void AddMeshGeneratedHandler(EventHandler listener)
		{
			this.meshGenerated = (EventHandler)Delegate.Remove(this.meshGenerated, listener);
			this.meshGenerated = (EventHandler)Delegate.Combine(this.meshGenerated, listener);
		}

		public void RemoveMeshGeneratedHandler(EventHandler listener)
		{
			this.meshGenerated = (EventHandler)Delegate.Remove(this.meshGenerated, listener);
		}
	}
}
