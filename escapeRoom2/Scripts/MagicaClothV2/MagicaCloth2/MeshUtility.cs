using UnityEngine;

namespace MagicaCloth2
{
	public static class MeshUtility
	{
		public static Mesh GetSharedMesh(Renderer ren)
		{
			if (ren == null)
			{
				return null;
			}
			if (ren is SkinnedMeshRenderer)
			{
				return (ren as SkinnedMeshRenderer).sharedMesh;
			}
			MeshFilter component = ren.GetComponent<MeshFilter>();
			if (component == null)
			{
				Debug.LogError("Not found MeshFilter!");
				return null;
			}
			return component.sharedMesh;
		}

		public static bool SetMesh(Renderer ren, Mesh mesh, Transform[] skinBones = null)
		{
			if (ren is SkinnedMeshRenderer)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
				skinnedMeshRenderer.sharedMesh = mesh;
				if (skinBones != null && skinBones.Length != 0)
				{
					skinnedMeshRenderer.bones = skinBones;
				}
			}
			else
			{
				MeshFilter component = ren.GetComponent<MeshFilter>();
				if (component == null)
				{
					Debug.LogError("Not found MeshFilter!");
					return false;
				}
				component.mesh = mesh;
			}
			return true;
		}

		public static int GetTransformCount(Renderer ren)
		{
			int num = 0;
			if ((bool)ren)
			{
				num++;
				if (ren is SkinnedMeshRenderer)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
					num++;
					int num2 = num;
					Transform[] bones = skinnedMeshRenderer.bones;
					num = num2 + ((bones != null) ? bones.Length : 0);
				}
			}
			return num;
		}
	}
}
