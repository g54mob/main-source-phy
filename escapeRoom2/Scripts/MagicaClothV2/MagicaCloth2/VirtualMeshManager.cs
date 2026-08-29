using System;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class VirtualMeshManager : IManager, IDisposable, IValid
	{
		[BurstCompile]
		private struct CalcMeshConvert_A_Job : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			public NativeArray<TeamManager.MappingData> mappingDataArray;

			[ReadOnly]
			public NativeArray<RenderManager.RenderDataWork> renderDataWorkArray;

			public void Execute(int index)
			{
				TeamManager.MappingData value = mappingDataArray[index];
				if (!value.IsValid())
				{
					return;
				}
				TeamManager.TeamData teamData = teamDataArray[value.teamId];
				if (teamData.IsProcess)
				{
					RenderManager.RenderDataWork renderDataWork = renderDataWorkArray[value.renderDataWorkIndex];
					if (renderDataWork.UseCustomMesh)
					{
						float3 pos = transformPositionArray[value.centerTransformIndex];
						quaternion rot = transformRotationArray[value.centerTransformIndex];
						float3 scl = transformScaleArray[value.centerTransformIndex];
						quaternion toMappingRotation = math.inverse(rot);
						float3 pos2 = transformPositionArray[teamData.centerTransformIndex];
						quaternion rot2 = transformRotationArray[teamData.centerTransformIndex];
						float3 scl2 = transformScaleArray[teamData.centerTransformIndex];
						bool sameSpace = MathUtility.CompareTransform(in pos, in rot, in scl, in pos2, in rot2, in scl2);
						value.sameSpace = sameSpace;
						value.toMappingMatrix = math.inverse(MathUtility.LocalToWorldMatrix(in pos, in rot, in scl));
						value.toMappingRotation = toMappingRotation;
						float num = math.length(teamData.initScale);
						value.scaleRatio = math.length(scl2) / num;
						bool isTangent = teamData.IsTangent;
						bool value2 = renderDataWork.HasBoneWeight && value.flag.IsSet(3);
						value.flag.SetBits(0, value: true);
						value.flag.SetBits(1, isTangent);
						value.flag.SetBits(2, value2);
						mappingDataArray[index] = value;
					}
				}
			}
		}

		[BurstCompile]
		private struct CalcMeshConvert_B_Job : IJobParallelFor
		{
			public int workerCount;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<TeamManager.MappingData> mappingDataArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> mappingAttributes;

			[ReadOnly]
			public NativeArray<float3> mappingLocalPositions;

			[ReadOnly]
			public NativeArray<float3> mappingLocalNormals;

			[ReadOnly]
			public NativeArray<float3> mappingLocalTangents;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> mappingBoneWeights;

			[ReadOnly]
			public NativeArray<int> mappingReferenceIndices;

			[ReadOnly]
			public NativeArray<float3> proxyPositions;

			[ReadOnly]
			public NativeArray<quaternion> proxyRotations;

			[ReadOnly]
			public NativeArray<float3> proxyVertexBindPosePositions;

			[ReadOnly]
			public NativeArray<quaternion> proxyVertexBindPoseRotations;

			[ReadOnly]
			public NativeArray<RenderManager.RenderDataWork> renderDataWorkArray;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> renderMeshPositions;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> renderMeshNormals;

			[NativeDisableParallelForRestriction]
			public NativeArray<float4> renderMeshTangents;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<BoneWeight> renderMeshBoneWeights;

			public void Execute(int dataIndex)
			{
				int index = dataIndex / workerCount;
				int workerIndex = dataIndex % workerCount;
				TeamManager.MappingData mappingData = mappingDataArray[index];
				if (!mappingData.IsValid())
				{
					return;
				}
				TeamManager.TeamData teamData = teamDataArray[mappingData.teamId];
				if (!teamData.IsProcess)
				{
					return;
				}
				RenderManager.RenderDataWork renderDataWork = renderDataWorkArray[mappingData.renderDataWorkIndex];
				if (!renderDataWork.UseCustomMesh)
				{
					return;
				}
				DataChunk workerChunk = MathUtility.GetWorkerChunk(mappingData.mappingCommonChunk.dataLength, workerCount, workerIndex);
				if (!workerChunk.IsValid)
				{
					return;
				}
				bool isTangent = teamData.IsTangent;
				float4 float5 = new float4(teamData.negativeScaleDirection, 1f);
				float3 float6 = teamData.initScale * mappingData.scaleRatio;
				bool flag = renderDataWork.HasBoneWeight && mappingData.flag.IsSet(3);
				int num = mappingData.mappingCommonChunk.startIndex + workerChunk.startIndex;
				int num2 = 0;
				while (num2 < workerChunk.dataLength)
				{
					VertexAttribute vertexAttribute = mappingAttributes[num];
					if (!vertexAttribute.IsInvalid() && !vertexAttribute.IsFixed())
					{
						float4x3 b = 0;
						b.c0 = new float4(mappingLocalPositions[num], 1f);
						b.c1 = new float4(mappingLocalNormals[num], 0f);
						b.c2 = math.select(0, new float4(mappingLocalTangents[num], 0f), isTangent);
						if (!mappingData.sameSpace)
						{
							b = math.mul(mappingData.toProxyMatrix, b);
						}
						b *= new float4x3(float5, float5, float5);
						VirtualMeshBoneWeight virtualMeshBoneWeight = mappingBoneWeights[num];
						int count = virtualMeshBoneWeight.Count;
						float4x3 b2 = 0;
						for (int i = 0; i < count; i++)
						{
							float num3 = virtualMeshBoneWeight.weights[i];
							int index2 = virtualMeshBoneWeight.boneIndices[i] + teamData.proxyCommonChunk.startIndex;
							float3 float7 = proxyVertexBindPosePositions[index2];
							quaternion obj = proxyVertexBindPoseRotations[index2];
							float7 *= teamData.negativeScaleDirection;
							quaternion q = obj.value * teamData.negativeScaleQuaternionValue;
							float3 float8 = math.mul(q, b.c0.xyz + float7);
							float3 v = math.mul(q, b.c1.xyz);
							float3 v2 = math.mul(q, b.c2.xyz);
							float3 float9 = proxyPositions[index2];
							quaternion q2 = proxyRotations[index2];
							float8 = math.mul(q2, float8 * float6) + float9;
							v = math.mul(q2, v);
							v2 = math.mul(q2, v2);
							b2.c0.xyz += float8 * num3;
							b2.c1.xyz += v * num3;
							b2.c2.xyz += v2 * num3;
						}
						b2.c0.w = 1f;
						b2 = math.mul(mappingData.toMappingMatrix, b2);
						int num4 = mappingReferenceIndices[num];
						int index3 = renderDataWork.renderMeshPositionAndNormalChunk.startIndex + num4;
						renderMeshPositions[index3] = b2.c0.xyz;
						renderMeshNormals[index3] = math.normalize(b2.c1.xyz);
						if (isTangent)
						{
							index3 = renderDataWork.renderMeshTangentChunk.startIndex + num4;
							float4 float10 = renderMeshTangents[index3];
							renderMeshTangents[index3] = new float4(math.normalize(b2.c2.xyz), float10.w);
						}
						if (flag)
						{
							index3 = renderDataWork.renderMeshBoneWeightChunk.startIndex + num4;
							renderMeshBoneWeights[index3] = renderDataWork.centerBoneWeight;
						}
					}
					num2++;
					num++;
				}
			}
		}

		[BurstCompile]
		private struct PostRenderMeshWorkDataBatchJob : IJobParallelFor
		{
			public NativeArray<RenderManager.RenderDataWork> renderDataWorkArray;

			[NativeDisableParallelForRestriction]
			public NativeArray<TeamManager.MappingData> mappingDataArray;

			public void Execute(int windex)
			{
				RenderManager.RenderDataWork value = renderDataWorkArray[windex];
				if (!value.IsValid() || !value.UseCustomMesh)
				{
					return;
				}
				bool value2 = false;
				bool value3 = false;
				bool value4 = false;
				int length = value.mappingDataIndexList.Length;
				for (int i = 0; i < length; i++)
				{
					int index = value.mappingDataIndexList[i];
					TeamManager.MappingData value5 = mappingDataArray[index];
					if (value5.flag.IsSet(0))
					{
						value2 = true;
						value5.flag.SetBits(0, value: false);
					}
					if (value5.flag.IsSet(1))
					{
						value3 = true;
						value5.flag.SetBits(1, value: false);
					}
					if (value5.flag.IsSet(2))
					{
						value4 = true;
						value5.flag.SetBits(2, value: false);
						value5.flag.SetBits(3, value: false);
					}
					mappingDataArray[index] = value5;
				}
				value.flag.SetBits(1, value2);
				value.flag.SetBits(6, value3);
				value.flag.SetBits(2, value4);
				renderDataWorkArray[windex] = value;
			}
		}

		public ExNativeArray<short> teamIds;

		public ExNativeArray<VertexAttribute> attributes;

		public ExNativeArray<FixedList32Bytes<uint>> vertexToTriangles;

		public ExNativeArray<float3> vertexBindPosePositions;

		public ExNativeArray<quaternion> vertexBindPoseRotations;

		public ExNativeArray<float> vertexDepths;

		public ExNativeArray<int> vertexRootIndices;

		public ExNativeArray<float3> vertexLocalPositions;

		public ExNativeArray<quaternion> vertexLocalRotations;

		public ExNativeArray<int> vertexParentIndices;

		public ExNativeArray<uint> vertexChildIndexArray;

		public ExNativeArray<ushort> vertexChildDataArray;

		public ExNativeArray<quaternion> normalAdjustmentRotations;

		public ExNativeArray<float2> uv;

		public ExNativeArray<short> triangleTeamIdArray;

		public ExNativeArray<int3> triangles;

		public ExNativeArray<float3> triangleNormals;

		public ExNativeArray<float3> triangleTangents;

		public ExNativeArray<short> edgeTeamIdArray;

		public ExNativeArray<int2> edges;

		public ExNativeArray<ExBitFlag8> edgeFlags;

		public ExNativeArray<ExBitFlag8> baseLineFlags;

		public ExNativeArray<short> baseLineTeamIds;

		public ExNativeArray<ushort> baseLineStartDataIndices;

		public ExNativeArray<ushort> baseLineDataCounts;

		public ExNativeArray<ushort> baseLineData;

		public ExNativeArray<float3> localPositions;

		public ExNativeArray<float3> localNormals;

		public ExNativeArray<float3> localTangents;

		public ExNativeArray<VirtualMeshBoneWeight> boneWeights;

		public ExNativeArray<int> skinBoneTransformIndices;

		public ExNativeArray<float4x4> skinBoneBindPoses;

		public ExNativeArray<quaternion> vertexToTransformRotations;

		public ExNativeArray<float3> positions;

		public ExNativeArray<quaternion> rotations;

		public ExNativeArray<short> mappingIdArray;

		public ExNativeArray<int> mappingReferenceIndices;

		public ExNativeArray<VertexAttribute> mappingAttributes;

		public ExNativeArray<float3> mappingLocalPositins;

		public ExNativeArray<float3> mappingLocalNormals;

		public ExNativeArray<float3> mappingLocalTangents;

		public ExNativeArray<VirtualMeshBoneWeight> mappingBoneWeights;

		private bool isValid;

		public int ProxyVertexCount => teamIds?.Count ?? 0;

		public int ProxyTriangleCount => triangles?.Count ?? 0;

		public int ProxyEdgeCount => edges?.Count ?? 0;

		public int ProxyBaseLineCount => baseLineFlags?.Count ?? 0;

		public int ProxyLocalPositionCount => localPositions?.Count ?? 0;

		public int MappingVertexCount => mappingIdArray?.Count ?? 0;

		public void Dispose()
		{
			isValid = false;
			teamIds?.Dispose();
			attributes?.Dispose();
			vertexToTriangles?.Dispose();
			vertexBindPosePositions?.Dispose();
			vertexBindPoseRotations?.Dispose();
			vertexDepths?.Dispose();
			vertexRootIndices?.Dispose();
			vertexLocalPositions?.Dispose();
			vertexLocalRotations?.Dispose();
			vertexParentIndices?.Dispose();
			vertexChildIndexArray?.Dispose();
			vertexChildDataArray?.Dispose();
			normalAdjustmentRotations?.Dispose();
			uv?.Dispose();
			teamIds = null;
			attributes = null;
			vertexToTriangles = null;
			vertexBindPosePositions = null;
			vertexBindPoseRotations = null;
			vertexDepths = null;
			vertexRootIndices = null;
			vertexLocalPositions = null;
			vertexLocalRotations = null;
			vertexParentIndices = null;
			vertexChildIndexArray = null;
			vertexChildDataArray = null;
			normalAdjustmentRotations = null;
			uv = null;
			triangleTeamIdArray?.Dispose();
			triangles?.Dispose();
			triangleNormals?.Dispose();
			triangleTangents?.Dispose();
			triangleTeamIdArray = null;
			triangles = null;
			triangleNormals = null;
			triangleTangents = null;
			edgeTeamIdArray?.Dispose();
			edges?.Dispose();
			edgeFlags?.Dispose();
			edgeTeamIdArray = null;
			edges = null;
			edgeFlags = null;
			positions?.Dispose();
			rotations?.Dispose();
			positions = null;
			rotations = null;
			baseLineFlags?.Dispose();
			baseLineTeamIds?.Dispose();
			baseLineStartDataIndices?.Dispose();
			baseLineDataCounts?.Dispose();
			baseLineData?.Dispose();
			baseLineFlags = null;
			baseLineTeamIds = null;
			baseLineStartDataIndices = null;
			baseLineDataCounts = null;
			baseLineData = null;
			localPositions?.Dispose();
			localNormals?.Dispose();
			localTangents?.Dispose();
			boneWeights?.Dispose();
			skinBoneTransformIndices?.Dispose();
			skinBoneBindPoses?.Dispose();
			localPositions = null;
			localNormals = null;
			localTangents = null;
			boneWeights = null;
			skinBoneTransformIndices = null;
			skinBoneBindPoses = null;
			vertexToTransformRotations?.Dispose();
			vertexToTransformRotations = null;
			mappingIdArray?.Dispose();
			mappingReferenceIndices?.Dispose();
			mappingAttributes?.Dispose();
			mappingLocalPositins?.Dispose();
			mappingLocalNormals?.Dispose();
			mappingLocalTangents?.Dispose();
			mappingBoneWeights?.Dispose();
			mappingIdArray = null;
			mappingReferenceIndices = null;
			mappingAttributes = null;
			mappingLocalPositins = null;
			mappingLocalNormals = null;
			mappingLocalTangents = null;
			mappingBoneWeights = null;
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			Dispose();
			teamIds = new ExNativeArray<short>(0, create: true);
			attributes = new ExNativeArray<VertexAttribute>(0, create: true);
			vertexToTriangles = new ExNativeArray<FixedList32Bytes<uint>>(0, create: true);
			vertexBindPosePositions = new ExNativeArray<float3>(0, create: true);
			vertexBindPoseRotations = new ExNativeArray<quaternion>(0, create: true);
			vertexDepths = new ExNativeArray<float>(0, create: true);
			vertexRootIndices = new ExNativeArray<int>(0, create: true);
			vertexLocalPositions = new ExNativeArray<float3>(0, create: true);
			vertexLocalRotations = new ExNativeArray<quaternion>(0, create: true);
			vertexParentIndices = new ExNativeArray<int>(0, create: true);
			vertexChildIndexArray = new ExNativeArray<uint>(0, create: true);
			vertexChildDataArray = new ExNativeArray<ushort>(0, create: true);
			normalAdjustmentRotations = new ExNativeArray<quaternion>(0, create: true);
			uv = new ExNativeArray<float2>(0, create: true);
			triangleTeamIdArray = new ExNativeArray<short>(0, create: true);
			triangles = new ExNativeArray<int3>(0, create: true);
			triangleNormals = new ExNativeArray<float3>(0, create: true);
			triangleTangents = new ExNativeArray<float3>(0, create: true);
			edgeTeamIdArray = new ExNativeArray<short>(0, create: true);
			edges = new ExNativeArray<int2>(0, create: true);
			edgeFlags = new ExNativeArray<ExBitFlag8>(0, create: true);
			positions = new ExNativeArray<float3>(0, create: true);
			rotations = new ExNativeArray<quaternion>(0, create: true);
			baseLineFlags = new ExNativeArray<ExBitFlag8>(0, create: true);
			baseLineTeamIds = new ExNativeArray<short>(0, create: true);
			baseLineStartDataIndices = new ExNativeArray<ushort>(0, create: true);
			baseLineDataCounts = new ExNativeArray<ushort>(0, create: true);
			baseLineData = new ExNativeArray<ushort>(0, create: true);
			localPositions = new ExNativeArray<float3>(0, create: true);
			localNormals = new ExNativeArray<float3>(0, create: true);
			localTangents = new ExNativeArray<float3>(0, create: true);
			boneWeights = new ExNativeArray<VirtualMeshBoneWeight>(0, create: true);
			skinBoneTransformIndices = new ExNativeArray<int>(0, create: true);
			skinBoneBindPoses = new ExNativeArray<float4x4>(0, create: true);
			vertexToTransformRotations = new ExNativeArray<quaternion>(0, create: true);
			mappingIdArray = new ExNativeArray<short>(0, create: true);
			mappingReferenceIndices = new ExNativeArray<int>(0, create: true);
			mappingAttributes = new ExNativeArray<VertexAttribute>(0, create: true);
			mappingLocalPositins = new ExNativeArray<float3>(0, create: true);
			mappingLocalNormals = new ExNativeArray<float3>(0, create: true);
			mappingLocalTangents = new ExNativeArray<float3>(0, create: true);
			mappingBoneWeights = new ExNativeArray<VirtualMeshBoneWeight>(0, create: true);
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		public void RegisterProxyMesh(int teamId, VirtualMeshContainer proxyMeshContainer)
		{
			if (isValid)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
				VirtualMesh shareVirtualMesh = proxyMeshContainer.shareVirtualMesh;
				teamDataRef.proxyMeshType = shareVirtualMesh.meshType;
				teamDataRef.proxyTransformChunk = MagicaManager.Bone.AddTransform(proxyMeshContainer, teamId);
				teamDataRef.centerTransformIndex = shareVirtualMesh.centerTransformIndex + teamDataRef.proxyTransformChunk.startIndex;
				int vertexCount = shareVirtualMesh.VertexCount;
				teamDataRef.proxyCommonChunk = teamIds.AddRange(vertexCount, (short)teamId);
				attributes.AddRange(shareVirtualMesh.attributes);
				vertexToTriangles.AddRange<FixedList32Bytes<uint>>(shareVirtualMesh.vertexToTriangles);
				vertexBindPosePositions.AddRange<float3>(shareVirtualMesh.vertexBindPosePositions);
				vertexBindPoseRotations.AddRange<quaternion>(shareVirtualMesh.vertexBindPoseRotations);
				vertexDepths.AddRange<float>(shareVirtualMesh.vertexDepths);
				vertexRootIndices.AddRange<int>(shareVirtualMesh.vertexRootIndices);
				vertexLocalPositions.AddRange<float3>(shareVirtualMesh.vertexLocalPositions);
				vertexLocalRotations.AddRange<quaternion>(shareVirtualMesh.vertexLocalRotations);
				vertexParentIndices.AddRange<int>(shareVirtualMesh.vertexParentIndices);
				vertexChildIndexArray.AddRange<uint>(shareVirtualMesh.vertexChildIndexArray);
				normalAdjustmentRotations.AddRange<quaternion>(shareVirtualMesh.normalAdjustmentRotations);
				uv.AddRange(shareVirtualMesh.uv);
				positions.AddRange(vertexCount);
				rotations.AddRange(vertexCount);
				teamDataRef.proxyVertexChildDataChunk = vertexChildDataArray.AddRange<ushort>(shareVirtualMesh.vertexChildDataArray);
				if (shareVirtualMesh.TriangleCount > 0)
				{
					teamDataRef.proxyTriangleChunk = triangleTeamIdArray.AddRange(shareVirtualMesh.TriangleCount, (short)teamId);
					triangles.AddRange(shareVirtualMesh.triangles);
					triangleNormals.AddRange(shareVirtualMesh.TriangleCount);
					triangleTangents.AddRange(shareVirtualMesh.TriangleCount);
				}
				if (shareVirtualMesh.EdgeCount > 0)
				{
					teamDataRef.proxyEdgeChunk = edgeTeamIdArray.AddRange(shareVirtualMesh.EdgeCount, (short)teamId);
					edges.AddRange<int2>(shareVirtualMesh.edges);
					edgeFlags.AddRange<ExBitFlag8>(shareVirtualMesh.edgeFlags);
				}
				if (shareVirtualMesh.BaseLineCount > 0)
				{
					teamDataRef.baseLineChunk = baseLineFlags.AddRange<ExBitFlag8>(shareVirtualMesh.baseLineFlags);
					baseLineStartDataIndices.AddRange<ushort>(shareVirtualMesh.baseLineStartDataIndices);
					baseLineDataCounts.AddRange<ushort>(shareVirtualMesh.baseLineDataCounts);
					baseLineTeamIds.AddRange(shareVirtualMesh.BaseLineCount, (short)teamId);
					teamDataRef.baseLineDataChunk = baseLineData.AddRange<ushort>(shareVirtualMesh.baseLineData);
				}
				teamDataRef.proxyMeshChunk = localPositions.AddRange(shareVirtualMesh.localPositions);
				localNormals.AddRange(shareVirtualMesh.localNormals);
				localTangents.AddRange(shareVirtualMesh.localTangents);
				boneWeights.AddRange(shareVirtualMesh.boneWeights);
				teamDataRef.proxySkinBoneChunk = skinBoneTransformIndices.AddRange(shareVirtualMesh.skinBoneTransformIndices);
				skinBoneBindPoses.AddRange(shareVirtualMesh.skinBoneBindPoses);
				if (shareVirtualMesh.meshType == VirtualMesh.MeshType.ProxyBoneMesh)
				{
					teamDataRef.proxyBoneChunk = vertexToTransformRotations.AddRange<quaternion>(shareVirtualMesh.vertexToTransformRotations);
				}
			}
		}

		public void ExitProxyMesh(int teamId)
		{
			if (isValid)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
				MagicaManager.Bone.RemoveTransform(teamDataRef.proxyTransformChunk);
				teamDataRef.proxyTransformChunk.Clear();
				teamIds.RemoveAndFill(teamDataRef.proxyCommonChunk, 0);
				attributes.RemoveAndFill(teamDataRef.proxyCommonChunk);
				vertexToTriangles.Remove(teamDataRef.proxyCommonChunk);
				vertexBindPosePositions.Remove(teamDataRef.proxyCommonChunk);
				vertexBindPoseRotations.Remove(teamDataRef.proxyCommonChunk);
				vertexDepths.Remove(teamDataRef.proxyCommonChunk);
				vertexRootIndices.Remove(teamDataRef.proxyCommonChunk);
				vertexLocalPositions.Remove(teamDataRef.proxyCommonChunk);
				vertexLocalRotations.Remove(teamDataRef.proxyCommonChunk);
				vertexParentIndices.Remove(teamDataRef.proxyCommonChunk);
				vertexChildIndexArray.Remove(teamDataRef.proxyCommonChunk);
				normalAdjustmentRotations.Remove(teamDataRef.proxyCommonChunk);
				uv.Remove(teamDataRef.proxyCommonChunk);
				positions.Remove(teamDataRef.proxyCommonChunk);
				rotations.Remove(teamDataRef.proxyCommonChunk);
				teamDataRef.proxyCommonChunk.Clear();
				vertexChildDataArray.Remove(teamDataRef.proxyVertexChildDataChunk);
				teamDataRef.proxyVertexChildDataChunk.Clear();
				triangleTeamIdArray.RemoveAndFill(teamDataRef.proxyTriangleChunk, 0);
				triangles.Remove(teamDataRef.proxyTriangleChunk);
				triangleNormals.Remove(teamDataRef.proxyTriangleChunk);
				triangleTangents.Remove(teamDataRef.proxyTriangleChunk);
				teamDataRef.proxyTriangleChunk.Clear();
				edgeTeamIdArray.RemoveAndFill(teamDataRef.proxyEdgeChunk, 0);
				edges.Remove(teamDataRef.proxyEdgeChunk);
				edgeFlags.Remove(teamDataRef.proxyEdgeChunk);
				baseLineFlags.RemoveAndFill(teamDataRef.baseLineChunk);
				baseLineTeamIds.Remove(teamDataRef.baseLineChunk);
				baseLineStartDataIndices.Remove(teamDataRef.baseLineChunk);
				baseLineDataCounts.Remove(teamDataRef.baseLineChunk);
				teamDataRef.baseLineChunk.Clear();
				baseLineData.Remove(teamDataRef.baseLineDataChunk);
				teamDataRef.baseLineDataChunk.Clear();
				localPositions.Remove(teamDataRef.proxyMeshChunk);
				localNormals.Remove(teamDataRef.proxyMeshChunk);
				localTangents.Remove(teamDataRef.proxyMeshChunk);
				boneWeights.Remove(teamDataRef.proxyMeshChunk);
				teamDataRef.proxyMeshChunk.Clear();
				skinBoneTransformIndices.Remove(teamDataRef.proxySkinBoneChunk);
				skinBoneBindPoses.Remove(teamDataRef.proxySkinBoneChunk);
				teamDataRef.proxySkinBoneChunk.Clear();
				vertexToTransformRotations.Remove(teamDataRef.proxyBoneChunk);
				teamDataRef.proxyBoneChunk.Clear();
				FixedList64Bytes<short> fixedList64Bytes = MagicaManager.Team.teamMappingIndexArray[teamId];
				int length = fixedList64Bytes.Length;
				for (int i = 0; i < length; i++)
				{
					int mappingIndex = fixedList64Bytes[i];
					ExitMappingMesh(teamId, mappingIndex);
				}
			}
		}

		public DataChunk RegisterMappingMesh(int teamId, VirtualMeshContainer mappingMeshContainer, int renderDataWorkIndex)
		{
			if (!isValid)
			{
				return DataChunk.Empty;
			}
			MagicaManager.Team.GetTeamDataRef(teamId);
			ref FixedList64Bytes<short> teamMappingRef = ref MagicaManager.Team.GetTeamMappingRef(teamId);
			TeamManager.MappingData mappingData = new TeamManager.MappingData
			{
				teamId = teamId
			};
			VirtualMesh shareVirtualMesh = mappingMeshContainer.shareVirtualMesh;
			Transform centerTransform = mappingMeshContainer.GetCenterTransform();
			mappingData.centerTransformIndex = MagicaManager.Bone.AddTransform(centerTransform, new ExBitFlag8(17), teamId).startIndex;
			mappingData.toProxyMatrix = shareVirtualMesh.toProxyMatrix;
			mappingData.toProxyRotation = shareVirtualMesh.toProxyRotation;
			int startIndex = MagicaManager.Team.mappingDataArray.Add(mappingData).startIndex;
			int vertexCount = shareVirtualMesh.VertexCount;
			mappingData.mappingCommonChunk = mappingIdArray.AddRange(vertexCount, (short)(startIndex + 1));
			mappingReferenceIndices.AddRange(shareVirtualMesh.referenceIndices);
			mappingAttributes.AddRange(shareVirtualMesh.attributes);
			mappingLocalPositins.AddRange(shareVirtualMesh.localPositions);
			mappingLocalNormals.AddRange(shareVirtualMesh.localNormals);
			mappingLocalTangents.AddRange(shareVirtualMesh.localTangents);
			mappingBoneWeights.AddRange(shareVirtualMesh.boneWeights);
			mappingData.renderDataWorkIndex = renderDataWorkIndex;
			MagicaManager.Render.GetRenderDataWorkRef(renderDataWorkIndex).AddMappingIndex(startIndex);
			MagicaManager.Team.mappingDataArray[startIndex] = mappingData;
			teamMappingRef.MC2Set((short)startIndex);
			shareVirtualMesh.mappingId = startIndex;
			return mappingData.mappingCommonChunk;
		}

		public void ExitMappingMesh(int teamId, int mappingIndex)
		{
			if (isValid)
			{
				MagicaManager.Team.GetTeamDataRef(teamId);
				ref TeamManager.MappingData reference = ref MagicaManager.Team.mappingDataArray.GetRef(mappingIndex);
				ref FixedList64Bytes<short> teamMappingRef = ref MagicaManager.Team.GetTeamMappingRef(teamId);
				MagicaManager.Bone.RemoveTransform(new DataChunk(reference.centerTransformIndex, 1));
				mappingIdArray.RemoveAndFill(reference.mappingCommonChunk, 0);
				mappingReferenceIndices.Remove(reference.mappingCommonChunk);
				mappingAttributes.Remove(reference.mappingCommonChunk);
				mappingLocalPositins.Remove(reference.mappingCommonChunk);
				mappingLocalNormals.Remove(reference.mappingCommonChunk);
				mappingLocalTangents.Remove(reference.mappingCommonChunk);
				mappingBoneWeights.Remove(reference.mappingCommonChunk);
				MagicaManager.Render.GetRenderDataWorkRef(reference.renderDataWorkIndex).RemoveMappingIndex(mappingIndex);
				teamMappingRef.MC2RemoveItemAtSwapBack((short)mappingIndex);
				MagicaManager.Team.mappingDataArray.RemoveAndFill(new DataChunk(mappingIndex, 1));
			}
		}

		internal static void SimulationPreProxyMeshUpdate(DataChunk chunk, int teamId, ref TeamManager.TeamData tdata, in NativeArray<VertexAttribute> attributes, in NativeArray<float3> localPositions, in NativeArray<float3> localNormals, in NativeArray<float3> localTangents, in NativeArray<VirtualMeshBoneWeight> boneWeights, in NativeArray<int> skinBoneTransformIndices, in NativeArray<float4x4> skinBoneBindPoses, ref NativeArray<float3> positions, ref NativeArray<quaternion> rotations, in NativeArray<float4x4> transformLocalToWorldMatrixArray)
		{
			DataChunk proxyCommonChunk = tdata.proxyCommonChunk;
			if (proxyCommonChunk.dataLength == 0)
			{
				return;
			}
			int num = proxyCommonChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyMeshChunk.startIndex + chunk.startIndex;
			int startIndex = tdata.proxySkinBoneChunk.startIndex;
			int startIndex2 = tdata.proxyTransformChunk.startIndex;
			float4x3 float4x5 = 0;
			float4x3 float4x6 = 0;
			float4x3 float4x7 = 0;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				VirtualMeshBoneWeight virtualMeshBoneWeight = boneWeights[num2];
				int count = virtualMeshBoneWeight.Count;
				float4x5 = 0;
				float4x6 = 0;
				float4x7.c0 = new float4(localPositions[num2], 1f);
				float4x7.c1 = new float4(localNormals[num2], 0f);
				float4x7.c2 = new float4(localTangents[num2], 0f);
				for (int i = 0; i < count; i++)
				{
					float num4 = virtualMeshBoneWeight.weights[i];
					float4x6 = float4x7;
					int num5 = virtualMeshBoneWeight.boneIndices[i];
					float4x6 = math.mul(skinBoneBindPoses[startIndex + num5], float4x6);
					int index = skinBoneTransformIndices[startIndex + num5] + startIndex2;
					float4x6 = math.mul(transformLocalToWorldMatrixArray[index], float4x6);
					float4x5 += float4x6 * num4;
				}
				float3 xyz = float4x5.c0.xyz;
				float3 xyz2 = float4x5.c1.xyz;
				float3 xyz3 = float4x5.c2.xyz;
				quaternion value = MathUtility.ToRotation(math.normalize(xyz2), math.normalize(xyz3));
				positions[num] = xyz;
				rotations[num] = value;
				num3++;
				num++;
				num2++;
			}
		}

		internal static void SimulationPostProxyMeshUpdateLine(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float3> positions, ref NativeArray<quaternion> rotations, ref NativeArray<float3> vertexLocalPositions, ref NativeArray<quaternion> vertexLocalRotations, ref NativeArray<uint> childIndexArray, ref NativeArray<ushort> childDataArray, ref NativeArray<ExBitFlag8> baseLineFlags, ref NativeArray<ushort> baseLineStartIndices, ref NativeArray<ushort> baseLineDataCounts, ref NativeArray<ushort> baseLineData)
		{
			if (!tdata.baseLineChunk.IsValid)
			{
				return;
			}
			float rotationalInterpolation = param.rotationalInterpolation;
			float rootRotation = param.rootRotation;
			int startIndex = tdata.proxyCommonChunk.startIndex;
			int startIndex2 = tdata.baseLineDataChunk.startIndex;
			int startIndex3 = tdata.proxyVertexChildDataChunk.startIndex;
			int num = tdata.baseLineChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				if (baseLineFlags[num].IsSet(1))
				{
					int num3 = baseLineStartIndices[num] + startIndex2;
					int num4 = baseLineDataCounts[num];
					int num5 = 0;
					while (num5 < num4)
					{
						int index = baseLineData[num3] + startIndex;
						float3 float5 = positions[index];
						quaternion quaternion2 = rotations[index];
						VertexAttribute vertexAttribute = attributes[index];
						uint pack = childIndexArray[index];
						int num6 = DataUtility.Unpack12_20Low(pack);
						int num7 = DataUtility.Unpack12_20Hi(pack);
						int num8 = 0;
						if (num7 > 0)
						{
							float3 from = 0;
							float3 to = 0;
							for (int i = 0; i < num7; i++)
							{
								int index2 = childDataArray[startIndex3 + num6 + i] + startIndex;
								VertexAttribute vertexAttribute2 = attributes[index2];
								float3 float6 = positions[index2];
								float3 from2 = math.mul(quaternion2, vertexLocalPositions[index2] * tdata.negativeScaleDirection);
								from += from2;
								if (vertexAttribute2.IsMove())
								{
									float3 to2 = float6 - float5;
									to += to2;
									quaternion a = MathUtility.FromToRotation(in from2, in to2);
									quaternion b = math.mul(quaternion2, vertexLocalRotations[index2].value * tdata.negativeScaleQuaternionValue);
									b = math.mul(a, b);
									rotations[index2] = b;
									num8++;
								}
								else
								{
									to += from2;
								}
							}
							if (num8 != 0)
							{
								float t = (vertexAttribute.IsMove() ? rotationalInterpolation : rootRotation);
								quaternion2 = math.mul(MathUtility.FromToRotation(in from, in to, t), quaternion2);
								rotations[index] = quaternion2;
							}
						}
						num5++;
						num3++;
					}
				}
				num2++;
				num++;
			}
		}

		internal static void SimulationPostProxyMeshUpdateTriangle(DataChunk chunk, ref TeamManager.TeamData tdata, ref NativeArray<float3> positions, ref NativeArray<int3> triangles, ref NativeArray<float3> triangleNormals, ref NativeArray<float3> triangleTangents, ref NativeArray<float2> uvs)
		{
			if (tdata.TriangleCount <= 0)
			{
				return;
			}
			int num = tdata.proxyTriangleChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				int3 int5 = triangles[num];
				int startIndex = tdata.proxyCommonChunk.startIndex;
				float3 p = positions[startIndex + int5.x];
				float3 p2 = positions[startIndex + int5.y];
				float3 p3 = positions[startIndex + int5.z];
				float3 float5 = math.cross(p2 - p, p3 - p);
				float num3 = math.length(float5);
				if (num3 > 1E-08f)
				{
					float3 value = float5 / num3;
					value *= tdata.negativeScaleTriangleSign.x;
					triangleNormals[num] = value;
				}
				float3 float6 = MathUtility.TriangleTangent(in p, in p2, in p3, uvs[startIndex + int5.x], uvs[startIndex + int5.y], uvs[startIndex + int5.z]);
				if (math.lengthsq(float6) > 0f)
				{
					float6 *= tdata.negativeScaleTriangleSign.y;
					triangleTangents[num] = float6;
				}
				num2++;
				num++;
			}
		}

		internal static void SimulationPostProxyMeshUpdateTriangleSum(DataChunk chunk, ref TeamManager.TeamData tdata, ref NativeArray<quaternion> rotations, ref NativeArray<float3> triangleNormals, ref NativeArray<float3> triangleTangents, ref NativeArray<FixedList32Bytes<uint>> vertexToTriangles, ref NativeArray<quaternion> normalAdjustmentRotations)
		{
			if (tdata.TriangleCount <= 0)
			{
				return;
			}
			int num = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				FixedList32Bytes<uint> fixedList32Bytes = vertexToTriangles[num];
				if (fixedList32Bytes.Length > 0)
				{
					float3 float5 = 0;
					float3 float6 = 0;
					for (int i = 0; i < fixedList32Bytes.Length; i++)
					{
						uint pack = fixedList32Bytes[i];
						int num3 = DataUtility.Unpack12_20Hi(pack);
						int num4 = DataUtility.Unpack12_20Low(pack);
						num4 += tdata.proxyTriangleChunk.startIndex;
						float5 += triangleNormals[num4] * (((num3 & 1) == 0) ? 1 : (-1));
						float6 += triangleTangents[num4] * (((num3 & 2) == 0) ? 1 : (-1));
					}
					float num5 = math.length(float5);
					float num6 = math.length(float6);
					if (num5 > 1E-06f && num6 > 1E-06f)
					{
						float5 /= num5;
						float6 /= num6;
						float num7 = math.dot(float5, float6);
						if (num7 != 1f && num7 != -1f)
						{
							quaternion a = quaternion.LookRotation(math.normalize(math.cross(float5, float6)), float5);
							a = math.mul(a, normalAdjustmentRotations[num].value * tdata.negativeScaleQuaternionValue);
							rotations[num] = a;
						}
					}
				}
				num2++;
				num++;
			}
		}

		internal static void SimulationPostProxyMeshUpdateTransform(DataChunk chunk, ref TeamManager.TeamData tdata, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float3> positions, ref NativeArray<quaternion> rotations, ref NativeArray<int> parentIndices, ref NativeArray<quaternion> vertexToTransformRotations, ref NativeArray<float3> transformPositionArray, ref NativeArray<quaternion> transformRotationArray, ref NativeArray<float3> transformScaleArray, ref NativeArray<float3> transformLocalPositionArray, ref NativeArray<quaternion> transformLocalRotationArray)
		{
			if (tdata.proxyMeshType != VirtualMesh.MeshType.ProxyBoneMesh)
			{
				return;
			}
			int num = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				float3 value = positions[num];
				quaternion a = rotations[num];
				int index = tdata.proxyBoneChunk.startIndex + chunk.startIndex + num2;
				a = math.mul(a, vertexToTransformRotations[index].value * tdata.negativeScaleQuaternionValue);
				int index2 = tdata.proxyTransformChunk.startIndex + chunk.startIndex + num2;
				transformPositionArray[index2] = value;
				transformRotationArray[index2] = a;
				num2++;
				num++;
			}
			num = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				int num4 = parentIndices[num];
				if (num4 >= 0 && attributes[num].IsMove())
				{
					int index3 = tdata.proxyTransformChunk.startIndex + chunk.startIndex + num3;
					int index4 = tdata.proxyTransformChunk.startIndex + num4;
					float3 float5 = transformPositionArray[index4];
					quaternion q = transformRotationArray[index4];
					float3 float6 = transformScaleArray[index4];
					float3 float7 = transformPositionArray[index3];
					quaternion b = transformRotationArray[index3];
					quaternion obj = math.inverse(q);
					float3 v = float7 - float5;
					float3 value2 = math.mul(obj, v);
					value2 /= float6;
					quaternion value3 = math.mul(obj, b).value * tdata.negativeScaleQuaternionValue;
					transformLocalPositionArray[index3] = value2;
					transformLocalRotationArray[index3] = value3;
				}
				num3++;
				num++;
			}
		}

		internal JobHandle PostMappingMeshUpdateBatchSchedule(JobHandle jobHandle, int workerCount)
		{
			if (MagicaManager.Team.MappingCount == 0)
			{
				return jobHandle;
			}
			TeamManager team = MagicaManager.Team;
			TransformManager bone = MagicaManager.Bone;
			RenderManager render = MagicaManager.Render;
			jobHandle = IJobParallelForExtensions.Schedule(new CalcMeshConvert_A_Job
			{
				teamDataArray = team.teamDataArray.GetNativeArray(),
				transformPositionArray = bone.positionArray.GetNativeArray(),
				transformRotationArray = bone.rotationArray.GetNativeArray(),
				transformScaleArray = bone.scaleArray.GetNativeArray(),
				mappingDataArray = team.mappingDataArray.GetNativeArray(),
				renderDataWorkArray = render.renderDataWorkArray.GetNativeArray()
			}, team.MappingCount, 1, jobHandle);
			jobHandle = IJobParallelForExtensions.Schedule(new CalcMeshConvert_B_Job
			{
				workerCount = workerCount,
				teamDataArray = team.teamDataArray.GetNativeArray(),
				mappingDataArray = team.mappingDataArray.GetNativeArray(),
				mappingAttributes = mappingAttributes.GetNativeArray(),
				mappingLocalPositions = mappingLocalPositins.GetNativeArray(),
				mappingLocalNormals = mappingLocalNormals.GetNativeArray(),
				mappingLocalTangents = mappingLocalTangents.GetNativeArray(),
				mappingBoneWeights = mappingBoneWeights.GetNativeArray(),
				mappingReferenceIndices = mappingReferenceIndices.GetNativeArray(),
				proxyPositions = positions.GetNativeArray(),
				proxyRotations = rotations.GetNativeArray(),
				proxyVertexBindPosePositions = vertexBindPosePositions.GetNativeArray(),
				proxyVertexBindPoseRotations = vertexBindPoseRotations.GetNativeArray(),
				renderDataWorkArray = render.renderDataWorkArray.GetNativeArray(),
				renderMeshPositions = render.renderMeshPositions.GetNativeArray(),
				renderMeshNormals = render.renderMeshNormals.GetNativeArray(),
				renderMeshTangents = render.renderMeshTangents.GetNativeArray(),
				renderMeshBoneWeights = render.renderMeshBoneWeights.GetNativeArray()
			}, team.MappingCount * workerCount, 1, jobHandle);
			jobHandle = IJobParallelForExtensions.Schedule(new PostRenderMeshWorkDataBatchJob
			{
				renderDataWorkArray = render.renderDataWorkArray.GetNativeArray(),
				mappingDataArray = team.mappingDataArray.GetNativeArray()
			}, render.RenderDataWorkCount, 8, jobHandle);
			return jobHandle;
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== VMesh Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("VirtualMesh Manager. Invalid.");
			}
			else
			{
				stringBuilder.AppendLine("VirtualMesh Manager.");
				stringBuilder.AppendLine($"  -ProxyVertexCount:{ProxyVertexCount}");
				stringBuilder.AppendLine($"  -ProxyEdgeCount:{ProxyEdgeCount}");
				stringBuilder.AppendLine($"  -ProxyTriangleCount:{ProxyTriangleCount}");
				stringBuilder.AppendLine($"  -ProxyBaseLineCount:{ProxyBaseLineCount}");
				stringBuilder.AppendLine($"  -ProxyLocalPositionCount:{ProxyLocalPositionCount}");
				stringBuilder.AppendLine("  [ProxyMesh]");
				stringBuilder.AppendLine("    -teamIds:" + teamIds.ToSummary());
				stringBuilder.AppendLine("    -attributes:" + attributes.ToSummary());
				stringBuilder.AppendLine("    -vertexToTriangles:" + vertexToTriangles.ToSummary());
				stringBuilder.AppendLine("    -vertexBindPosePositions:" + vertexBindPosePositions.ToSummary());
				stringBuilder.AppendLine("    -vertexBindPoseRotations:" + vertexBindPoseRotations.ToSummary());
				stringBuilder.AppendLine("    -vertexDepths:" + vertexDepths.ToSummary());
				stringBuilder.AppendLine("    -vertexRootIndices:" + vertexRootIndices.ToSummary());
				stringBuilder.AppendLine("    -vertexLocalPositions:" + vertexLocalPositions.ToSummary());
				stringBuilder.AppendLine("    -vertexLocalRotations:" + vertexLocalRotations.ToSummary());
				stringBuilder.AppendLine("    -vertexParentIndices:" + vertexParentIndices.ToSummary());
				stringBuilder.AppendLine("    -vertexChildIndexArray:" + vertexChildIndexArray.ToSummary());
				stringBuilder.AppendLine("    -vertexChildDataArray:" + vertexChildDataArray.ToSummary());
				stringBuilder.AppendLine("    -normalAdjustmentRotations:" + normalAdjustmentRotations.ToSummary());
				stringBuilder.AppendLine("    -uv:" + uv.ToSummary());
				stringBuilder.AppendLine("    -triangleTeamIdArray:" + triangleTeamIdArray.ToSummary());
				stringBuilder.AppendLine("    -triangles:" + triangles.ToSummary());
				stringBuilder.AppendLine("    -triangleNormals:" + triangleNormals.ToSummary());
				stringBuilder.AppendLine("    -triangleTangents:" + triangleTangents.ToSummary());
				stringBuilder.AppendLine("    -edgeTeamIdArray:" + edgeTeamIdArray.ToSummary());
				stringBuilder.AppendLine("    -edges:" + edges.ToSummary());
				stringBuilder.AppendLine("    -edgeFlags:" + edgeFlags.ToSummary());
				stringBuilder.AppendLine("    -baseLineFlags:" + baseLineFlags.ToSummary());
				stringBuilder.AppendLine("    -baseLineTeamIds:" + baseLineTeamIds.ToSummary());
				stringBuilder.AppendLine("    -baseLineStartDataIndices:" + baseLineStartDataIndices.ToSummary());
				stringBuilder.AppendLine("    -baseLineDataCounts:" + baseLineDataCounts.ToSummary());
				stringBuilder.AppendLine("    -baseLineData:" + baseLineData.ToSummary());
				stringBuilder.AppendLine("  [Mesh Common]");
				stringBuilder.AppendLine("    -localPositions:" + localPositions.ToSummary());
				stringBuilder.AppendLine("    -localNormals:" + localNormals.ToSummary());
				stringBuilder.AppendLine("    -localTangents:" + localTangents.ToSummary());
				stringBuilder.AppendLine("    -boneWeights:" + boneWeights.ToSummary());
				stringBuilder.AppendLine("    -skinBoneTransformIndices:" + skinBoneTransformIndices.ToSummary());
				stringBuilder.AppendLine("    -skinBoneBindPoses:" + skinBoneBindPoses.ToSummary());
				stringBuilder.AppendLine("  [Mesh Other]");
				stringBuilder.AppendLine("    -vertexToTransformRotations:" + vertexToTransformRotations.ToSummary());
				stringBuilder.AppendLine("    -positions:" + positions.ToSummary());
				stringBuilder.AppendLine("    -rotations:" + rotations.ToSummary());
				stringBuilder.AppendLine("  [Mapping]");
				stringBuilder.AppendLine($"    -MappingVertexCount:{MappingVertexCount}");
				stringBuilder.AppendLine("    -mappingReferenceIndices:" + mappingReferenceIndices.ToSummary());
				stringBuilder.AppendLine("    -mappingAttributes:" + mappingAttributes.ToSummary());
				stringBuilder.AppendLine("    -mappingLocalPositins:" + mappingLocalPositins.ToSummary());
				stringBuilder.AppendLine("    -mappingLocalNormals:" + mappingLocalNormals.ToSummary());
				stringBuilder.AppendLine("    -mappingBoneWeights:" + mappingBoneWeights.ToSummary());
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
