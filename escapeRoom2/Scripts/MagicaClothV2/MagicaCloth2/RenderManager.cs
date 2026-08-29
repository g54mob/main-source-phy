using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;

namespace MagicaCloth2
{
	public class RenderManager : IManager, IDisposable, IValid
	{
		public struct RenderDataWork : IValid
		{
			public BitField32 flag;

			public DataChunk renderMeshPositionAndNormalChunk;

			public DataChunk renderMeshTangentChunk;

			public DataChunk renderMeshBoneWeightChunk;

			public BoneWeight centerBoneWeight;

			public FixedList32Bytes<short> mappingDataIndexList;

			public bool UseCustomMesh => flag.IsSet(0);

			public bool HasMeshTangent => flag.IsSet(4);

			public bool HasTangent => flag.IsSet(5);

			public bool HasBoneWeight => flag.IsSet(8);

			public bool IsValid()
			{
				return renderMeshPositionAndNormalChunk.IsValid;
			}

			public void AddMappingIndex(int mindex)
			{
				mappingDataIndexList.Add((short)mindex);
			}

			public void RemoveMappingIndex(int mindex)
			{
				mappingDataIndexList.MC2RemoveItemAtSwapBack((short)mindex);
			}
		}

		private Dictionary<int, RenderData> renderDataDict = new Dictionary<int, RenderData>();

		public const int RenderDataFlag_UseCustomMesh = 0;

		public const int RenderDataFlag_WritePositionNormal = 1;

		public const int RenderDataFlag_WriteBoneWeight = 2;

		public const int RenderDataFlag_HasMeshTangent = 4;

		public const int RenderDataFlag_HasTangent = 5;

		public const int RenderDataFlag_WriteTangent = 6;

		public const int RenderDataFlag_HasSkinnedMesh = 7;

		public const int RenderDataFlag_HasBoneWeight = 8;

		public ExNativeArray<RenderDataWork> renderDataWorkArray;

		public ExNativeArray<float3> renderMeshPositions;

		public ExNativeArray<float3> renderMeshNormals;

		public ExNativeArray<float4> renderMeshTangents;

		public ExNativeArray<BoneWeight> renderMeshBoneWeights;

		private bool isValid;

		private static readonly ProfilerMarker writeMeshTimeProfiler = new ProfilerMarker("WriteMesh");

		public int RenderDataWorkCount => renderDataWorkArray?.Count ?? 0;

		public void Initialize()
		{
			Dispose();
			renderDataWorkArray = new ExNativeArray<RenderDataWork>(0, create: true);
			renderMeshPositions = new ExNativeArray<float3>(0, create: true);
			renderMeshNormals = new ExNativeArray<float3>(0, create: true);
			renderMeshTangents = new ExNativeArray<float4>(0, create: true);
			renderMeshBoneWeights = new ExNativeArray<BoneWeight>(0, create: true);
			MagicaManager.afterDelayedDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterDelayedDelegate, new MagicaManager.UpdateMethod(PreRenderingUpdate));
			isValid = true;
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Dispose()
		{
			isValid = false;
			lock (renderDataDict)
			{
				foreach (RenderData value in renderDataDict.Values)
				{
					value?.Dispose();
				}
			}
			renderDataDict.Clear();
			renderDataWorkArray?.Dispose();
			renderMeshPositions?.Dispose();
			renderMeshNormals?.Dispose();
			renderMeshTangents?.Dispose();
			renderMeshBoneWeights?.Dispose();
			renderDataWorkArray = null;
			renderMeshPositions = null;
			renderMeshNormals = null;
			renderMeshBoneWeights = null;
			renderMeshTangents = null;
			MagicaManager.afterDelayedDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterDelayedDelegate, new MagicaManager.UpdateMethod(PreRenderingUpdate));
		}

		public bool IsValid()
		{
			return isValid;
		}

		public int AddRenderer(Renderer ren, RenderSetupData referenceSetupData, RenderSetupData.UniqueSerializationData referenceUniqueSetupData, RenderSetupSerializeData referenceInitSetupData)
		{
			if (!isValid)
			{
				return 0;
			}
			int instanceID = ren.GetInstanceID();
			lock (renderDataDict)
			{
				if (!renderDataDict.ContainsKey(instanceID))
				{
					RenderData renderData = new RenderData();
					renderData.Initialize(ren, referenceSetupData, referenceUniqueSetupData, referenceInitSetupData);
					renderDataDict.Add(instanceID, renderData);
				}
				renderDataDict[instanceID].AddReferenceCount();
				return instanceID;
			}
		}

		public bool RemoveRenderer(int handle)
		{
			if (!isValid)
			{
				return false;
			}
			bool result = false;
			lock (renderDataDict)
			{
				if (renderDataDict.ContainsKey(handle))
				{
					RenderData renderData = renderDataDict[handle];
					if (renderData.RemoveReferenceCount() == 0)
					{
						renderData.Dispose();
						renderDataDict.Remove(handle);
						result = true;
					}
				}
			}
			return result;
		}

		public RenderData GetRendererData(int handle)
		{
			if (!isValid)
			{
				return null;
			}
			lock (renderDataDict)
			{
				if (renderDataDict.ContainsKey(handle))
				{
					return renderDataDict[handle];
				}
				return null;
			}
		}

		public int AddRenderDataWork(RenderData rdata)
		{
			if (!isValid)
			{
				return -1;
			}
			RenderDataWork data = default(RenderDataWork);
			data.flag.SetBits(4, rdata.originalMesh.HasVertexAttribute(UnityEngine.Rendering.VertexAttribute.Tangent));
			data.flag.SetBits(7, rdata.HasSkinnedMesh);
			data.flag.SetBits(8, rdata.HasBoneWeight);
			data.centerBoneWeight = new BoneWeight
			{
				boneIndex0 = rdata.setupData.renderTransformIndex,
				weight0 = 1f
			};
			int vertexCount = rdata.originalMesh.vertexCount;
			data.renderMeshPositionAndNormalChunk = renderMeshPositions.AddRange(vertexCount);
			renderMeshNormals.AddRange(vertexCount);
			if (data.HasMeshTangent)
			{
				data.renderMeshTangentChunk = renderMeshTangents.AddRange(vertexCount);
			}
			if (data.HasBoneWeight)
			{
				data.renderMeshBoneWeightChunk = renderMeshBoneWeights.AddRange(vertexCount);
			}
			return renderDataWorkArray.Add(data).startIndex;
		}

		public void RemoveRenderDataWork(int index)
		{
			if (isValid && index >= 0)
			{
				ref RenderDataWork renderDataWorkRef = ref GetRenderDataWorkRef(index);
				if (renderDataWorkRef.renderMeshPositionAndNormalChunk.IsValid)
				{
					renderMeshPositions.Remove(renderDataWorkRef.renderMeshPositionAndNormalChunk);
					renderMeshNormals.Remove(renderDataWorkRef.renderMeshPositionAndNormalChunk);
				}
				if (renderDataWorkRef.renderMeshTangentChunk.IsValid)
				{
					renderMeshTangents.Remove(renderDataWorkRef.renderMeshTangentChunk);
				}
				if (renderDataWorkRef.renderMeshBoneWeightChunk.IsValid)
				{
					renderMeshBoneWeights.Remove(renderDataWorkRef.renderMeshBoneWeightChunk);
				}
				renderDataWorkRef.renderMeshPositionAndNormalChunk.Clear();
				renderDataWorkRef.renderMeshTangentChunk.Clear();
				renderDataWorkRef.renderMeshBoneWeightChunk.Clear();
				renderDataWorkRef.flag.Clear();
				renderDataWorkArray.RemoveAndFill(new DataChunk(index));
			}
		}

		public ref RenderDataWork GetRenderDataWorkRef(int index)
		{
			return ref renderDataWorkArray.GetRef(index);
		}

		public bool IsSetRenderDataWorkFlag(int index, int flag)
		{
			return GetRenderDataWorkRef(index).flag.IsSet(flag);
		}

		public void SetBitsRenderDataWorkFlag(int index, int flag, bool sw)
		{
			GetRenderDataWorkRef(index).flag.SetBits(flag, sw);
		}

		public void StartUse(ClothProcess cprocess, int handle)
		{
			GetRendererData(handle)?.StartUse(cprocess);
		}

		public void EndUse(ClothProcess cprocess, int handle)
		{
			GetRendererData(handle)?.EndUse(cprocess);
		}

		private void PreRenderingUpdate()
		{
			if (renderDataDict.Count == 0)
			{
				return;
			}
			foreach (RenderData value in renderDataDict.Values)
			{
				value?.WriteMesh();
			}
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Render Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Render Manager. Invalid.");
			}
			else
			{
				stringBuilder.AppendLine($"Render Manager. Count({renderDataDict.Count})");
				stringBuilder.AppendLine("  [RenderMeshBuffer]");
				stringBuilder.AppendLine("    -renderMeshPositions:" + renderMeshPositions.ToSummary());
				stringBuilder.AppendLine("    -renderMeshNormals:" + renderMeshNormals.ToSummary());
				stringBuilder.AppendLine("    -renderMeshTangents:" + renderMeshTangents.ToSummary());
				stringBuilder.AppendLine("  [RenderDataWork]");
				stringBuilder.AppendLine($"    -Count:{renderDataWorkArray.Count}");
				if (renderDataWorkArray.Count > 0)
				{
					for (int i = 0; i < renderDataWorkArray.Count; i++)
					{
						RenderDataWork renderDataWork = renderDataWorkArray[i];
						if (renderDataWork.IsValid())
						{
							stringBuilder.AppendLine($"    [{i}] MappingListCount:{renderDataWork.mappingDataIndexList.Length}");
						}
					}
				}
				stringBuilder.AppendLine("  [RenderData]");
				foreach (KeyValuePair<int, RenderData> item in renderDataDict)
				{
					stringBuilder.Append(item.Value.ToString());
				}
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
