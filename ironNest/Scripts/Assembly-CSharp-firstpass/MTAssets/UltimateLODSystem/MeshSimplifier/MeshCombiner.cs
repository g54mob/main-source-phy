using System.Collections.Generic;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	public static class MeshCombiner
	{
		public static Mesh CombineMeshes(Transform rootTransform, MeshRenderer[] renderers, out Material[] resultMaterials)
		{
			resultMaterials = null;
			return null;
		}

		public static Mesh CombineMeshes(Transform rootTransform, SkinnedMeshRenderer[] renderers, out Material[] resultMaterials, out Transform[] resultBones)
		{
			resultMaterials = null;
			resultBones = null;
			return null;
		}

		public static Mesh CombineMeshes(Mesh[] meshes, Matrix4x4[] transforms, Material[][] materials, out Material[] resultMaterials)
		{
			resultMaterials = null;
			return null;
		}

		public static Mesh CombineMeshes(Mesh[] meshes, Matrix4x4[] transforms, Material[][] materials, Transform[][] bones, out Material[] resultMaterials, out Transform[] resultBones)
		{
			resultMaterials = null;
			resultBones = null;
			return null;
		}

		private static void CopyVertexPositions(ICollection<Vector3> list, Vector3[] arr)
		{
		}

		private static void CopyVertexAttributes<T>(ref List<T> dest, IEnumerable<T> src, int previousVertexCount, int meshVertexCount, int totalVertexCount, T defaultValue)
		{
		}

		private static T[] MergeArrays<T>(T[] arr1, T[] arr2)
		{
			return null;
		}

		private static void TransformVertices(Vector3[] vertices, ref Matrix4x4 transform)
		{
		}

		private static void TransformNormals(Vector3[] normals, ref Matrix4x4 transform)
		{
		}

		private static void TransformTangents(Vector4[] tangents, ref Matrix4x4 transform)
		{
		}

		private static void RemapBones(BoneWeight[] boneWeights, int[] boneIndices)
		{
		}

		private static bool CanReadMesh(Mesh mesh)
		{
			return false;
		}
	}
}
