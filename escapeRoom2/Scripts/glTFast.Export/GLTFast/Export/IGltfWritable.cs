using System;
using System.IO;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Export
{
	public interface IGltfWritable
	{
		uint AddNode(float3? translation = null, quaternion? rotation = null, float3? scale = null, uint[] children = null, string name = null);

		[Obsolete("Use overload with skinning parameter.")]
		void AddMeshToNode(int nodeId, Mesh uMesh, int[] materialIds);

		[Obsolete("Use overload with joints parameter.")]
		void AddMeshToNode(int nodeId, Mesh uMesh, int[] materialIds, bool skinning);

		void AddMeshToNode(int nodeId, Mesh uMesh, int[] materialIds, uint[] joints);

		void AddCameraToNode(int nodeId, int cameraId);

		void AddLightToNode(int nodeId, int lightId);

		bool AddMaterial(Material uMaterial, out int materialId, IMaterialExport materialExport);

		int AddImage(ImageExportBase imageExport);

		int AddTexture(int imageId, int samplerId);

		int AddSampler(FilterMode filterMode, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV);

		bool AddCamera(Camera uCamera, out int cameraId);

		bool AddLight(Light uLight, out int lightId);

		uint AddScene(uint[] nodes, string name = null);

		void RegisterExtensionUsage(Extension extension, bool required = true);

		Task<bool> SaveToFileAndDispose(string path);

		Task<bool> SaveToStreamAndDispose(Stream stream);
	}
}
