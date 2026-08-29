using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	public class RenderData : IDisposable, ITransform
	{
		private HashSet<ClothProcess> useProcessSet = new HashSet<ClothProcess>();

		private bool isSkipWriting;

		internal RenderSetupData setupData;

		internal RenderSetupData.UniqueSerializationData preBuildUniqueSerializeData;

		private Renderer renderer;

		private SkinnedMeshRenderer skinnedMeshRendere;

		private MeshFilter meshFilter;

		public int ReferenceCount { get; private set; }

		internal string Name => setupData?.name ?? "(empty)";

		internal bool HasSkinnedMesh => setupData?.hasSkinnedMesh ?? false;

		internal bool HasBoneWeight => setupData?.hasBoneWeight ?? false;

		internal Mesh originalMesh { get; private set; }

		internal List<Transform> transformList { get; private set; }

		internal Mesh customMesh { get; private set; }

		internal int renderDataWorkIndex { get; private set; } = -1;

		internal ResultCode Result => setupData?.result ?? ResultCode.None;

		public void Dispose()
		{
			SwapOriginalMesh(null);
			setupData?.Dispose();
			preBuildUniqueSerializeData = null;
			MagicaManager.Render.RemoveRenderDataWork(renderDataWorkIndex);
			if ((bool)customMesh)
			{
				UnityEngine.Object.Destroy(customMesh);
			}
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			setupData?.GetUsedTransform(transformSet);
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			setupData?.ReplaceTransform(replaceDict);
		}

		internal void Initialize(Renderer ren, RenderSetupData referenceSetupData, RenderSetupData.UniqueSerializationData referencePreBuildUniqueSetupData, RenderSetupSerializeData referenceInitSetupData)
		{
			if (referenceSetupData != null && referencePreBuildUniqueSetupData != null)
			{
				setupData = referenceSetupData;
				preBuildUniqueSerializeData = referencePreBuildUniqueSetupData;
				originalMesh = preBuildUniqueSerializeData.originalMesh;
				renderer = preBuildUniqueSerializeData.renderer;
				skinnedMeshRendere = preBuildUniqueSerializeData.skinRenderer;
				meshFilter = preBuildUniqueSerializeData.meshFilter;
				transformList = preBuildUniqueSerializeData.transformList;
			}
			else
			{
				setupData = new RenderSetupData(referenceInitSetupData, ren);
				preBuildUniqueSerializeData = null;
				originalMesh = setupData.originalMesh;
				renderer = setupData.renderer;
				skinnedMeshRendere = setupData.skinRenderer;
				meshFilter = setupData.meshFilter;
				transformList = setupData.transformList;
			}
			renderDataWorkIndex = MagicaManager.Render.AddRenderDataWork(this);
		}

		internal int AddReferenceCount()
		{
			ReferenceCount++;
			return ReferenceCount;
		}

		internal int RemoveReferenceCount()
		{
			ReferenceCount--;
			return ReferenceCount;
		}

		private void SwapCustomMesh(ClothProcess process)
		{
			if (setupData.IsFaild() || originalMesh == null || MagicaManager.Render.IsSetRenderDataWorkFlag(renderDataWorkIndex, 0))
			{
				return;
			}
			if (customMesh == null)
			{
				customMesh = UnityEngine.Object.Instantiate(originalMesh);
				customMesh.MarkDynamic();
				if (HasBoneWeight)
				{
					int num = ((preBuildUniqueSerializeData != null) ? preBuildUniqueSerializeData.transformList.Count : setupData.TransformCount);
					List<Matrix4x4> list = new List<Matrix4x4>(num);
					list.AddRange(setupData.bindPoseList);
					while (list.Count < num)
					{
						list.Add(Matrix4x4.identity);
					}
					customMesh.bindposes = list.ToArray();
					skinnedMeshRendere.bones = transformList.ToArray();
				}
			}
			ResetCustomMeshWorkData();
			SetMesh(customMesh);
			MagicaManager.Render.SetBitsRenderDataWorkFlag(renderDataWorkIndex, 0, sw: true);
			process?.cloth?.OnRendererMeshChange?.Invoke(process.cloth, renderer, arg3: true);
		}

		private void ResetCustomMeshWorkData()
		{
			RenderManager render = MagicaManager.Render;
			ref RenderManager.RenderDataWork renderDataWorkRef = ref render.GetRenderDataWorkRef(renderDataWorkIndex);
			int vertexCount = setupData.vertexCount;
			if (setupData.HasMeshDataArray)
			{
				Mesh.MeshData meshData = setupData.meshDataArray[0];
				using NativeArray<Vector3> nativeArray = new NativeArray<Vector3>(vertexCount, Allocator.TempJob);
				using NativeArray<Vector3> nativeArray2 = new NativeArray<Vector3>(vertexCount, Allocator.TempJob);
				meshData.GetVertices(nativeArray);
				meshData.GetNormals(nativeArray2);
				render.renderMeshPositions.CopyFrom(nativeArray, renderDataWorkRef.renderMeshPositionAndNormalChunk.startIndex, vertexCount);
				render.renderMeshNormals.CopyFrom(nativeArray2, renderDataWorkRef.renderMeshPositionAndNormalChunk.startIndex, vertexCount);
				if (renderDataWorkRef.HasMeshTangent)
				{
					using NativeArray<Vector4> nativeArray3 = new NativeArray<Vector4>(vertexCount, Allocator.TempJob);
					meshData.GetTangents(nativeArray3);
					render.renderMeshTangents.CopyFrom(nativeArray3, renderDataWorkRef.renderMeshTangentChunk.startIndex, vertexCount);
					renderDataWorkRef.flag.SetBits(5, value: true);
				}
			}
			else
			{
				render.renderMeshPositions.CopyFrom(setupData.localPositions, renderDataWorkRef.renderMeshPositionAndNormalChunk.startIndex, vertexCount);
				render.renderMeshNormals.CopyFrom(setupData.localNormals, renderDataWorkRef.renderMeshPositionAndNormalChunk.startIndex, vertexCount);
				if (renderDataWorkRef.HasMeshTangent && setupData.HasTangent)
				{
					render.renderMeshTangents.CopyFrom(setupData.localTangents, renderDataWorkRef.renderMeshTangentChunk.startIndex, vertexCount);
					renderDataWorkRef.flag.SetBits(5, value: true);
				}
			}
			if (HasBoneWeight && renderDataWorkRef.HasBoneWeight)
			{
				using (NativeArray<BoneWeight> nativeArray4 = new NativeArray<BoneWeight>(vertexCount, Allocator.TempJob))
				{
					setupData.GetBoneWeightsRun(nativeArray4);
					render.renderMeshBoneWeights.CopyFrom(nativeArray4, renderDataWorkRef.renderMeshBoneWeightChunk.startIndex, vertexCount);
				}
			}
		}

		private void SwapOriginalMesh(ClothProcess process)
		{
			RenderManager render = MagicaManager.Render;
			if (render.IsSetRenderDataWorkFlag(renderDataWorkIndex, 0) && setupData != null)
			{
				SetMesh(originalMesh);
				if (skinnedMeshRendere != null)
				{
					skinnedMeshRendere.bones = transformList.ToArray();
				}
			}
			render.SetBitsRenderDataWorkFlag(renderDataWorkIndex, 0, sw: false);
			process?.cloth?.OnRendererMeshChange?.Invoke(process.cloth, renderer, arg3: false);
		}

		private void SetMesh(Mesh mesh)
		{
			if (!(mesh == null) && setupData != null)
			{
				if (meshFilter != null)
				{
					meshFilter.mesh = mesh;
				}
				else if (skinnedMeshRendere != null)
				{
					skinnedMeshRendere.sharedMesh = mesh;
				}
			}
		}

		public void StartUse(ClothProcess cprocess)
		{
			UpdateUse(cprocess, 1);
		}

		public void EndUse(ClothProcess cprocess)
		{
			UpdateUse(cprocess, -1);
		}

		internal void UpdateUse(ClothProcess cprocess, int add)
		{
			if (add > 0)
			{
				useProcessSet.Add(cprocess);
			}
			else if (add < 0)
			{
				if (!useProcessSet.Contains(cprocess))
				{
					return;
				}
				useProcessSet.Remove(cprocess);
			}
			bool num = useProcessSet.Any((ClothProcess x) => (x.IsCameraCullingInvisible() && !x.IsCameraCullingKeep()) || x.IsDistanceCullingInvisible());
			bool flag = false;
			if (num || useProcessSet.Count == 0)
			{
				SwapOriginalMesh(cprocess);
			}
			else if (add == 0 && useProcessSet.Count > 0)
			{
				SwapCustomMesh(cprocess);
				flag = true;
			}
			else if (add > 0 && useProcessSet.Count == 1)
			{
				SwapCustomMesh(cprocess);
				flag = true;
			}
			else if (add != 0)
			{
				ResetCustomMeshWorkData();
				flag = true;
			}
			if (flag)
			{
				ref RenderManager.RenderDataWork renderDataWorkRef = ref MagicaManager.Render.GetRenderDataWorkRef(renderDataWorkIndex);
				int length = renderDataWorkRef.mappingDataIndexList.Length;
				for (int num2 = 0; num2 < length; num2++)
				{
					int mindex = renderDataWorkRef.mappingDataIndexList[num2];
					MagicaManager.Team.GetMappingDataRef(mindex).flag.SetBits(3, value: true);
				}
			}
		}

		internal void UpdateSkipWriting()
		{
			isSkipWriting = false;
			foreach (ClothProcess item in useProcessSet)
			{
				if (item.IsSkipWriting())
				{
					isSkipWriting = true;
				}
			}
		}

		internal void WriteMesh()
		{
			RenderManager render = MagicaManager.Render;
			ref RenderManager.RenderDataWork renderDataWorkRef = ref render.GetRenderDataWorkRef(renderDataWorkIndex);
			if (renderDataWorkRef.UseCustomMesh && useProcessSet.Count != 0 && !isSkipWriting)
			{
				if (renderDataWorkRef.flag.IsSet(1))
				{
					customMesh.SetVertices(render.renderMeshPositions.GetNativeArray(), renderDataWorkRef.renderMeshPositionAndNormalChunk.startIndex, renderDataWorkRef.renderMeshPositionAndNormalChunk.dataLength);
					customMesh.SetNormals(render.renderMeshNormals.GetNativeArray(), renderDataWorkRef.renderMeshPositionAndNormalChunk.startIndex, renderDataWorkRef.renderMeshPositionAndNormalChunk.dataLength);
					renderDataWorkRef.flag.SetBits(1, value: false);
				}
				if (renderDataWorkRef.flag.IsSet(6))
				{
					customMesh.SetTangents(render.renderMeshTangents.GetNativeArray(), renderDataWorkRef.renderMeshTangentChunk.startIndex, renderDataWorkRef.renderMeshTangentChunk.dataLength);
					renderDataWorkRef.flag.SetBits(6, value: false);
				}
				if (renderDataWorkRef.flag.IsSet(2))
				{
					NativeSlice<BoneWeight> nativeSlice = new NativeSlice<BoneWeight>(render.renderMeshBoneWeights.GetNativeArray(), renderDataWorkRef.renderMeshBoneWeightChunk.startIndex, renderDataWorkRef.renderMeshBoneWeightChunk.dataLength);
					customMesh.boneWeights = nativeSlice.ToArray();
					renderDataWorkRef.flag.SetBits(2, value: false);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($">>> [{Name}] ref:{ReferenceCount}, useProcess:{useProcessSet.Count}, HasSkinnedMesh:{HasSkinnedMesh}, HasBoneWeight:{HasBoneWeight}");
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}
	}
}
