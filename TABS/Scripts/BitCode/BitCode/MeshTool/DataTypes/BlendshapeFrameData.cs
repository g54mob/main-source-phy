using UnityEngine;

namespace BitCode.MeshTool.DataTypes
{
	public struct BlendshapeFrameData
	{
		public readonly string Name;

		public readonly int FrameNumber;

		public readonly float Weight;

		public readonly Vector3[] VertexDelta;

		public readonly Vector3[] NormalDelta;

		public readonly Vector3[] TangentDelta;

		public BlendshapeFrameData(string name, int frameNumber, float weight, Vector3[] vertexDeltas, Vector3[] normalDeltas, Vector3[] tangentDeltas)
		{
			Name = name;
			Weight = weight;
			FrameNumber = frameNumber;
			VertexDelta = vertexDeltas;
			NormalDelta = normalDeltas;
			TangentDelta = tangentDeltas;
		}
	}
}
