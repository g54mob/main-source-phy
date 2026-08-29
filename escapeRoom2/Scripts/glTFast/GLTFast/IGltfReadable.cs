using System;
using GLTFast.Schema;
using Unity.Collections;
using UnityEngine;

namespace GLTFast
{
	public interface IGltfReadable<out TRoot> : IGltfReadable, IMaterialProvider, IMaterialsVariantsProvider where TRoot : RootBase
	{
		TRoot GetSourceRoot();
	}
	public interface IGltfReadable : IMaterialProvider, IMaterialsVariantsProvider
	{
		int MaterialCount { get; }

		int ImageCount { get; }

		int TextureCount { get; }

		UnityEngine.Material GetMaterial(int index = 0);

		UnityEngine.Material GetDefaultMaterial();

		Texture2D GetImage(int index = 0);

		Texture2D GetTexture(int index = 0);

		bool IsTextureYFlipped(int index = 0);

		CameraBase GetSourceCamera(uint index);

		MaterialBase GetSourceMaterial(int index = 0);

		MeshBase GetSourceMesh(int meshIndex);

		MeshPrimitiveBase GetSourceMeshPrimitive(int meshIndex, int primitiveIndex);

		NodeBase GetSourceNode(int index = 0);

		Scene GetSourceScene(int index = 0);

		TextureBase GetSourceTexture(int index = 0);

		Image GetSourceImage(int index = 0);

		LightPunctual GetSourceLightPunctual(uint index);

		Matrix4x4[] GetBindPoses(int skinId);

		[Obsolete("Use GetAccessorData instead.")]
		NativeSlice<byte> GetAccessor(int accessorIndex);

		NativeSlice<byte> GetAccessorData(int accessorIndex);
	}
}
