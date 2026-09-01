using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MTAssets.UltimateLODSystem.MeshSimplifier.Internal;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	public sealed class MeshSimplifier
	{
		private const int TriangleEdgeCount = 3;

		private const int TriangleVertexCount = 3;

		private const double DoubleEpsilon = 0.001;

		private const double DenomEpilson = 1E-08;

		private static readonly int UVChannelCount;

		private SimplificationOptions simplificationOptions;

		private bool verbose;

		private int subMeshCount;

		private int[] subMeshOffsets;

		private ResizableArray<Triangle> triangles;

		private ResizableArray<Vertex> vertices;

		private ResizableArray<Ref> refs;

		private ResizableArray<Vector3> vertNormals;

		private ResizableArray<Vector4> vertTangents;

		private UVChannels<Vector2> vertUV2D;

		private UVChannels<Vector3> vertUV3D;

		private UVChannels<Vector4> vertUV4D;

		private ResizableArray<Color> vertColors;

		private ResizableArray<BoneWeight> vertBoneWeights;

		private ResizableArray<BlendShapeContainer> blendShapes;

		private Matrix4x4[] bindposes;

		private readonly double[] errArr;

		private readonly int[] attributeIndexArr;

		private readonly HashSet<Triangle> triangleHashSet1;

		private readonly HashSet<Triangle> triangleHashSet2;

		public SimplificationOptions SimplificationOptions
		{
			get
			{
				return default(SimplificationOptions);
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public bool PreserveBorderEdges
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public bool PreserveUVSeamEdges
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public bool PreserveUVFoldoverEdges
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public bool PreserveSurfaceCurvature
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public bool EnableSmartLink
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public int MaxIterationCount
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public double Agressiveness
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		public bool Verbose
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public double VertexLinkDistance
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		[Obsolete("Use MeshSimplifier.SimplificationOptions instead.", false)]
		public double VertexLinkDistanceSqr
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		public Vector3[] Vertices
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int SubMeshCount => 0;

		public int BlendShapeCount => 0;

		public Vector3[] Normals
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector4[] Tangents
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV1
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV2
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV3
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV4
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV5
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV6
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV7
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Vector2[] UV8
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Color[] Colors
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public BoneWeight[] BoneWeights
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public MeshSimplifier()
		{
		}

		public MeshSimplifier(Mesh mesh)
		{
		}

		private void InitializeVertexAttribute<T>(T[] attributeValues, ref ResizableArray<T> attributeArray, string attributeName)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static double VertexError(ref SymmetricMatrix q, double x, double y, double z)
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double CurvatureError(ref Vertex vert0, ref Vertex vert1)
		{
			return 0.0;
		}

		private double CalculateError(ref Vertex vert0, ref Vertex vert1, out Vector3d result)
		{
			result = default(Vector3d);
			return 0.0;
		}

		private static void CalculateBarycentricCoords(ref Vector3d point, ref Vector3d a, ref Vector3d b, ref Vector3d c, out Vector3 result)
		{
			result = default(Vector3);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector4 NormalizeTangent(Vector4 tangent)
		{
			return default(Vector4);
		}

		private bool Flipped(ref Vector3d p, int i0, int i1, ref Vertex v0, bool[] deleted)
		{
			return false;
		}

		private void UpdateTriangles(int i0, int ia0, ref Vertex v, ResizableArray<bool> deleted, ref int deletedTriangles)
		{
		}

		private void InterpolateVertexAttributes(int dst, int i0, int i1, int i2, ref Vector3 barycentricCoord)
		{
		}

		private bool AreUVsTheSame(int channel, int indexA, int indexB)
		{
			return false;
		}

		private void RemoveVertexPass(int startTrisCount, int targetTrisCount, double threshold, ResizableArray<bool> deleted0, ResizableArray<bool> deleted1, ref int deletedTris)
		{
		}

		private void UpdateMesh(int iteration)
		{
		}

		private void UpdateReferences()
		{
		}

		private void CompactMesh()
		{
		}

		private void CalculateSubMeshOffsets()
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void GetTrianglesContainingVertex(ref Vertex vert, HashSet<Triangle> tris)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void GetTrianglesContainingBothVertices(ref Vertex vert0, ref Vertex vert1, HashSet<Triangle> tris)
		{
		}

		public int[][] GetAllSubMeshTriangles()
		{
			return null;
		}

		public int[] GetSubMeshTriangles(int subMeshIndex)
		{
			return null;
		}

		public void ClearSubMeshes()
		{
		}

		public void AddSubMeshTriangles(int[] triangles)
		{
		}

		public void AddSubMeshTriangles(int[][] triangles)
		{
		}

		public Vector2[] GetUVs2D(int channel)
		{
			return null;
		}

		public Vector3[] GetUVs3D(int channel)
		{
			return null;
		}

		public Vector4[] GetUVs4D(int channel)
		{
			return null;
		}

		public void GetUVs(int channel, List<Vector2> uvs)
		{
		}

		public void GetUVs(int channel, List<Vector3> uvs)
		{
		}

		public void GetUVs(int channel, List<Vector4> uvs)
		{
		}

		public void SetUVs(int channel, IList<Vector2> uvs)
		{
		}

		public void SetUVs(int channel, IList<Vector3> uvs)
		{
		}

		public void SetUVs(int channel, IList<Vector4> uvs)
		{
		}

		public void SetUVs(int channel, IList<Vector4> uvs, int uvComponentCount)
		{
		}

		public void SetUVsAuto(int channel, IList<Vector4> uvs)
		{
		}

		public BlendShape[] GetAllBlendShapes()
		{
			return null;
		}

		public BlendShape GetBlendShape(int blendShapeIndex)
		{
			return default(BlendShape);
		}

		public void ClearBlendShapes()
		{
		}

		public void AddBlendShape(BlendShape blendShape)
		{
		}

		public void AddBlendShapes(BlendShape[] blendShapes)
		{
		}

		public void Initialize(Mesh mesh)
		{
		}

		public void SimplifyMesh(float quality)
		{
		}

		public void SimplifyMeshLossless()
		{
		}

		public Mesh ToMesh()
		{
			return null;
		}

		public static void ValidateOptions(SimplificationOptions options)
		{
		}
	}
}
