using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class MeshData
	{
		public struct Vertex
		{
			public Vector3 position;

			public Vector3 normal;

			public Vector2 material;
		}

		public List<Vertex> vertices = new List<Vertex>();

		public List<int> indices = new List<int>();
	}
}
