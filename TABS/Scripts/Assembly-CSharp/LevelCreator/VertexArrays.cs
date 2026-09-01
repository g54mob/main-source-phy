using UnityEngine;

namespace LevelCreator
{
	public class VertexArrays
	{
		public int noOfVertices;

		public Vector3[] positions;

		public Vector3[] normals;

		public Vector2[] materials;

		public void CopyFrom(MeshData meshData)
		{
			noOfVertices = 0;
			if (positions == null || positions.Length < meshData.vertices.Count)
			{
				positions = new Vector3[meshData.vertices.Count];
			}
			if (normals == null || normals.Length < meshData.vertices.Count)
			{
				normals = new Vector3[meshData.vertices.Count];
			}
			if (materials == null || materials.Length < meshData.vertices.Count)
			{
				materials = new Vector2[meshData.vertices.Count];
			}
			foreach (MeshData.Vertex vertex in meshData.vertices)
			{
				positions[noOfVertices] = vertex.position;
				normals[noOfVertices] = vertex.normal;
				materials[noOfVertices] = vertex.material;
				noOfVertices++;
			}
		}
	}
}
