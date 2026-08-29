using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace MagicaCloth2
{
	public class VirtualMesh : IDisposable, IValid
	{
		[BurstCompile]
		private struct Import_GenerateTangentJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<float3> localNormals;

			[WriteOnly]
			public NativeArray<float3> localTangents;

			public void Execute(int vindex)
			{
				float3 x = localNormals[vindex];
				float3 y = math.up();
				y = ((!((double)math.dot(x, y) < 0.9)) ? math.normalize(math.cross(x, math.right())) : math.normalize(math.cross(x, y)));
				localTangents[vindex] = y;
			}
		}

		[BurstCompile]
		private struct Import_CalcSkinningJob : IJobParallelFor
		{
			public NativeArray<float3> localPositions;

			public NativeArray<float3> localNormals;

			public NativeArray<float3> localTangents;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[ReadOnly]
			public NativeArray<int> skinBoneTransformIndices;

			[ReadOnly]
			public NativeArray<float4x4> bindPoses;

			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			public float4x4 toM;

			public void Execute(int vindex)
			{
				VirtualMeshBoneWeight virtualMeshBoneWeight = boneWeights[vindex];
				int count = virtualMeshBoneWeight.Count;
				float3 pos = 0;
				float3 dir = 0;
				float3 dir2 = 0;
				for (int i = 0; i < count; i++)
				{
					float num = virtualMeshBoneWeight.weights[i];
					int index = virtualMeshBoneWeight.boneIndices[i];
					float4x4 a = bindPoses[index];
					float4 b = new float4(localPositions[vindex], 1f);
					float4 b2 = new float4(localNormals[vindex], 0f);
					float4 b3 = new float4(localTangents[vindex], 0f);
					float3 pos2 = math.mul(a, b).xyz;
					float3 nor = math.mul(a, b2).xyz;
					float3 tan = math.mul(a, b3).xyz;
					int index2 = skinBoneTransformIndices[index];
					MathUtility.TransformPositionNormalTangent(transformPositionArray[index2], transformRotationArray[index2], transformScaleArray[index2], ref pos2, ref nor, ref tan);
					pos += pos2 * num;
					dir += nor * num;
					dir2 += tan * num;
				}
				localPositions[vindex] = MathUtility.TransformPoint(in pos, in toM);
				localNormals[vindex] = MathUtility.TransformDirection(in dir, in toM);
				localTangents[vindex] = MathUtility.TransformDirection(in dir2, in toM);
			}
		}

		[BurstCompile]
		private struct Import_BoneWeightJob1 : IJob
		{
			public int vcnt;

			[ReadOnly]
			public NativeArray<byte> bonesPerVertexArray;

			[WriteOnly]
			public NativeArray<int> startBoneWeightIndices;

			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < vcnt; i++)
				{
					startBoneWeightIndices[i] = num;
					num += bonesPerVertexArray[i];
				}
			}
		}

		[BurstCompile]
		private struct Import_BoneWeightJob2 : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int> startBoneWeightIndices;

			[ReadOnly]
			public NativeArray<BoneWeight1> boneWeightArray;

			[ReadOnly]
			public NativeArray<byte> bonesPerVertexArray;

			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			public void Execute(int vindex)
			{
				int num = startBoneWeightIndices[vindex];
				int num2 = bonesPerVertexArray[vindex];
				VirtualMeshBoneWeight value = default(VirtualMeshBoneWeight);
				int num3 = 0;
				for (int i = 0; i < num2 && i < 4; i++)
				{
					BoneWeight1 boneWeight = boneWeightArray[num + i];
					if (boneWeight.weight > 0f)
					{
						value.weights[num3] = boneWeight.weight;
						value.boneIndices[num3] = boneWeight.boneIndex;
						num3++;
					}
				}
				if (num2 > 4)
				{
					value.AdjustWeight();
				}
				boneWeights[vindex] = value;
			}
		}

		[BurstCompile]
		private struct Import_BoneVertexJob : IJobParallelFor
		{
			public float4x4 WtoL;

			public float4x4 LtoW;

			[ReadOnly]
			public NativeArray<float3> transformPositions;

			[ReadOnly]
			public NativeArray<quaternion> transformRotations;

			[ReadOnly]
			public NativeArray<float3> transformScales;

			[WriteOnly]
			public NativeArray<float3> localPositions;

			[WriteOnly]
			public NativeArray<float3> localNormals;

			[WriteOnly]
			public NativeArray<float3> localTangents;

			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[WriteOnly]
			public NativeArray<float4x4> skinBoneBindPoses;

			public void Execute(int vindex)
			{
				float3 pos = transformPositions[vindex];
				quaternion quaternion2 = transformRotations[vindex];
				float3 scale = transformScales[vindex];
				float3 value = MathUtility.InverseTransformPoint(in pos, in WtoL);
				float3 dir = math.mul(quaternion2, math.up());
				float3 dir2 = math.mul(quaternion2, math.forward());
				dir = MathUtility.InverseTransformDirection(in dir, in WtoL);
				dir2 = MathUtility.InverseTransformDirection(in dir2, in WtoL);
				localPositions[vindex] = value;
				localNormals[vindex] = dir;
				localTangents[vindex] = dir2;
				VirtualMeshBoneWeight value2 = new VirtualMeshBoneWeight(new int4(vindex, 0, 0, 0), new float4(1f, 0f, 0f, 0f));
				boneWeights[vindex] = value2;
				float4x4 value3 = math.mul(math.inverse(float4x4.TRS(pos, quaternion2, scale)), LtoW);
				skinBoneBindPoses[vindex] = value3;
			}
		}

		[BurstCompile]
		private struct Select_PackVertexJob : IJob
		{
			public int vertexCount;

			[ReadOnly]
			public NativeArray<int> newVertexRemapIndices;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<float3> localNormals;

			[ReadOnly]
			public NativeArray<float3> localTangents;

			[ReadOnly]
			public NativeArray<float2> uv;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[WriteOnly]
			public NativeArray<int> newReferenceIndices;

			[WriteOnly]
			public NativeArray<VertexAttribute> newAttributes;

			[WriteOnly]
			public NativeArray<float3> newLocalPositions;

			[WriteOnly]
			public NativeArray<float3> newLocalNormals;

			[WriteOnly]
			public NativeArray<float3> newLocalTangents;

			[WriteOnly]
			public NativeArray<float2> newUv;

			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> newBoneWeights;

			public void Execute()
			{
				for (int i = 0; i < vertexCount; i++)
				{
					int num = newVertexRemapIndices[i];
					if (num >= 0)
					{
						newReferenceIndices[num] = i;
						newAttributes[num] = attributes[i];
						newLocalPositions[num] = localPositions[i];
						newLocalNormals[num] = localNormals[i];
						newLocalTangents[num] = localTangents[i];
						newUv[num] = uv[i];
						newBoneWeights[num] = boneWeights[i];
					}
				}
			}
		}

		[BurstCompile]
		private struct Select_GridJob : IJob
		{
			public float gridSize;

			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			public int selectionCount;

			[ReadOnly]
			public NativeArray<float3> selectionPositions;

			[ReadOnly]
			public NativeArray<VertexAttribute> selectionAttributes;

			public int vertexCount;

			public int triangleCount;

			public float searchRadius;

			[ReadOnly]
			public NativeArray<float3> meshPositions;

			[ReadOnly]
			public NativeArray<int3> meshTriangles;

			public NativeList<int3> newTriangles;

			public NativeArray<int> newVertexRemapIndices;

			[WriteOnly]
			public NativeReference<int> newVertexCount;

			public void Execute()
			{
				for (int i = 0; i < vertexCount; i++)
				{
					float3 float5 = meshPositions[i];
					int num = -1;
					foreach (int3 item in GridMap<int>.GetArea(float5, searchRadius, gridMap, gridSize))
					{
						if (!gridMap.ContainsKey(item))
						{
							continue;
						}
						foreach (int item2 in gridMap.GetValuesForKey(item))
						{
							float3 y = selectionPositions[item2];
							if (!(math.distance(float5, y) > searchRadius))
							{
								num = 1;
								break;
							}
						}
						if (num >= 0)
						{
							break;
						}
					}
					newVertexRemapIndices[i] = num;
				}
				for (int j = 0; j < triangleCount; j++)
				{
					int3 value = meshTriangles[j];
					if (newVertexRemapIndices[value.x] >= 0 || newVertexRemapIndices[value.y] >= 0 || newVertexRemapIndices[value.z] >= 0)
					{
						newTriangles.Add(in value);
					}
				}
				int length = newTriangles.Length;
				for (int k = 0; k < length; k++)
				{
					int3 int5 = newTriangles[k];
					newVertexRemapIndices[int5.x] = 1;
					newVertexRemapIndices[int5.y] = 1;
					newVertexRemapIndices[int5.z] = 1;
				}
				int num2 = 0;
				for (int l = 0; l < vertexCount; l++)
				{
					if (newVertexRemapIndices[l] >= 0)
					{
						newVertexRemapIndices[l] = num2;
						num2++;
					}
				}
				newVertexCount.Value = num2;
				int length2 = newTriangles.Length;
				for (int m = 0; m < length2; m++)
				{
					int3 value2 = newTriangles[m];
					value2.x = newVertexRemapIndices[value2.x];
					value2.y = newVertexRemapIndices[value2.y];
					value2.z = newVertexRemapIndices[value2.z];
					newTriangles[m] = value2;
				}
			}
		}

		[BurstCompile]
		private struct Add_CalcBindPoseJob : IJobParallelFor
		{
			public int skinBoneOffset;

			[ReadOnly]
			public NativeArray<int> srcSkinBoneTransformIndices;

			[ReadOnly]
			public NativeArray<float3> srcTransformPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> srcTransformRotationArray;

			[ReadOnly]
			public NativeArray<float3> srcTransformScaleArray;

			public float4x4 dstCenterLocalToWorldMatrix;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float4x4> dstSkinBoneBindPoses;

			public void Execute(int boneIndex)
			{
				int index = srcSkinBoneTransformIndices[boneIndex];
				float3 translation = srcTransformPositionArray[index];
				quaternion rotation = srcTransformRotationArray[index];
				float3 scale = srcTransformScaleArray[index];
				float4x4 value = math.mul(math.inverse(float4x4.TRS(translation, rotation, scale)), dstCenterLocalToWorldMatrix);
				dstSkinBoneBindPoses[skinBoneOffset + boneIndex] = value;
			}
		}

		[BurstCompile]
		private struct Add_CopyVerticesJob : IJobParallelFor
		{
			public int vertexOffset;

			public int skinBoneOffset;

			public float4x4 toM;

			[ReadOnly]
			public NativeArray<VertexAttribute> srcAttributes;

			[ReadOnly]
			public NativeArray<float3> srclocalPositions;

			[ReadOnly]
			public NativeArray<float3> srclocalNormals;

			[ReadOnly]
			public NativeArray<float3> srclocalTangents;

			[ReadOnly]
			public NativeArray<float2> srcUV;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> srcBoneWeights;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VertexAttribute> dstAttributes;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> dstlocalPositions;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> dstlocalNormals;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> dstlocalTangents;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float2> dstUV;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> dstBoneWeights;

			[ReadOnly]
			public NativeArray<int> dstSkinBoneIndices;

			public void Execute(int vindex)
			{
				int index = vertexOffset + vindex;
				float3 pos = srclocalPositions[vindex];
				float3 dir = srclocalNormals[vindex];
				float3 dir2 = srclocalTangents[vindex];
				pos = MathUtility.TransformPoint(in pos, in toM);
				dir = MathUtility.TransformDirection(in dir, in toM);
				dir2 = MathUtility.TransformDirection(in dir2, in toM);
				dstlocalPositions[index] = pos;
				dstlocalNormals[index] = dir;
				dstlocalTangents[index] = dir2;
				dstUV[index] = srcUV[vindex];
				if (vindex < srcBoneWeights.Length)
				{
					VirtualMeshBoneWeight value = srcBoneWeights[vindex];
					value.boneIndices += skinBoneOffset;
					for (int i = 0; i < 4; i++)
					{
						if (value.weights[i] < 1E-06f)
						{
							continue;
						}
						int num = value.boneIndices[i];
						int num2 = dstSkinBoneIndices[num];
						for (int j = 0; j < num; j++)
						{
							if (dstSkinBoneIndices[j] == num2)
							{
								value.boneIndices[i] = j;
								break;
							}
						}
					}
					dstBoneWeights[index] = value;
				}
				dstAttributes[index] = srcAttributes[vindex];
			}
		}

		private struct MappingWorkData
		{
			public float3 position;

			public int vertexIndex;

			public int proxyVertexIndex;

			public float proxyVertexDistance;
		}

		[BurstCompile]
		private struct Mapping_DirectConnectionVertexDataJob : IJob
		{
			public float4x4 toP;

			public int vcnt;

			public DataChunk mergeChunk;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[WriteOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeArray<VertexAttribute> proxyAttributes;

			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			[WriteOnly]
			public NativeArray<MappingWorkData> mappingWorkData;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					int num = joinIndices[mergeChunk.startIndex + i];
					VertexAttribute value = proxyAttributes[num];
					if (value.IsInvalid())
					{
						attributes[i] = VertexAttribute.Invalid;
						continue;
					}
					float3 float5 = MathUtility.TransformPoint(localPositions[i], in toP);
					float3 y = proxyLocalPositions[num];
					MappingWorkData value2 = new MappingWorkData
					{
						position = float5,
						vertexIndex = i,
						proxyVertexIndex = num,
						proxyVertexDistance = math.distance(float5, y)
					};
					mappingWorkData[i] = value2;
					attributes[i] = value;
				}
			}
		}

		[BurstCompile]
		private struct Mapping_CalcDirectWeightJob : IJob
		{
			public int vcnt;

			public float weightLength;

			[ReadOnly]
			public NativeArray<MappingWorkData> mappingWorkData;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			[ReadOnly]
			public NativeArray<uint> proxyVertexToVertexIndexArray;

			[ReadOnly]
			public NativeArray<ushort> proxyVertexToVertexDataArray;

			public NativeParallelHashSet<ushort> useSet;

			public void Execute()
			{
				FixedList4096Bytes<ushort> fixedList = default(FixedList4096Bytes<ushort>);
				for (int i = 0; i < vcnt; i++)
				{
					if (attributes[i].IsInvalid())
					{
						continue;
					}
					MappingWorkData mappingWorkData = this.mappingWorkData[i];
					ushort num = (ushort)mappingWorkData.proxyVertexIndex;
					useSet.Clear();
					fixedList.Clear();
					ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
					fixedList.MC2Push(num);
					while (!fixedList.IsEmpty)
					{
						num = FixedList4096BytesExtensions.MC2Pop(ref fixedList);
						if (useSet.Contains(num))
						{
							continue;
						}
						useSet.Add(num);
						float num2 = math.distance(mappingWorkData.position, proxyLocalPositions[num]);
						if (num2 > weightLength)
						{
							continue;
						}
						float f = Mathf.Clamp01(1f - num2 / weightLength);
						f = Mathf.Pow(f, 3f);
						exCostSortedList.Add(1f - f, num);
						DataUtility.Unpack12_20(proxyVertexToVertexIndexArray[num], out var hi, out var low);
						for (int j = 0; j < hi; j++)
						{
							if (FixedList4096BytesExtensions.MC2IsCapacity(ref fixedList))
							{
								break;
							}
							ushort num3 = proxyVertexToVertexDataArray[low + j];
							if (!useSet.Contains(num3))
							{
								num2 = math.distance(mappingWorkData.position, proxyLocalPositions[num3]);
								if (!(num2 > weightLength))
								{
									fixedList.MC2Push(num3);
								}
							}
						}
					}
					if (exCostSortedList.Count == 0)
					{
						exCostSortedList.Add(1f, num);
					}
					else
					{
						int count = exCostSortedList.Count;
						for (int k = 0; k < 4; k++)
						{
							if (k < count)
							{
								exCostSortedList.costs[k] = 1f - exCostSortedList.costs[k];
								continue;
							}
							exCostSortedList.costs[k] = 0f;
							exCostSortedList.data[k] = 0;
						}
						float num4 = math.csum(exCostSortedList.costs);
						if (num4 == 0f)
						{
							float value = 1f / (float)count;
							for (int l = 0; l < count; l++)
							{
								exCostSortedList.costs[l] = value;
							}
						}
						else
						{
							exCostSortedList.costs = math.saturate(exCostSortedList.costs / num4);
						}
					}
					boneWeights[i] = new VirtualMeshBoneWeight(exCostSortedList.data, exCostSortedList.costs);
				}
			}
		}

		[BurstCompile]
		private struct Mapping_CalcConnectionVertexDataJob : IJob
		{
			public float gridSize;

			public float searchRadius;

			public float4x4 toP;

			public int vcnt;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[ReadOnly]
			public NativeArray<int> transformIds;

			[WriteOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			[ReadOnly]
			public NativeArray<VertexAttribute> proxyAttributes;

			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> proxyBoneWeights;

			[ReadOnly]
			public NativeArray<int> proxyTransformIds;

			[WriteOnly]
			public NativeArray<MappingWorkData> mappingWorkData;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					float3 float5 = MathUtility.TransformPoint(localPositions[i], in toP);
					VirtualMeshBoneWeight virtualMeshBoneWeight = boneWeights[i];
					int num = transformIds[virtualMeshBoneWeight.boneIndices[0]];
					ExCostSortedList1 exCostSortedList = new ExCostSortedList1(-1f);
					ExCostSortedList1 exCostSortedList2 = new ExCostSortedList1(-1f);
					foreach (int3 item in GridMap<int>.GetArea(float5, searchRadius, gridMap, gridSize))
					{
						if (!gridMap.ContainsKey(item))
						{
							continue;
						}
						foreach (int item2 in gridMap.GetValuesForKey(item))
						{
							float3 y = proxyLocalPositions[item2];
							float num2 = math.distance(float5, y);
							if (num2 > searchRadius)
							{
								continue;
							}
							VirtualMeshBoneWeight virtualMeshBoneWeight2 = proxyBoneWeights[item2];
							bool flag = false;
							for (int j = 0; j < virtualMeshBoneWeight2.Count; j++)
							{
								if (flag)
								{
									break;
								}
								if (proxyTransformIds[virtualMeshBoneWeight2.boneIndices[j]] == num)
								{
									flag = true;
								}
							}
							if (flag)
							{
								exCostSortedList2.Add(num2, item2);
							}
							exCostSortedList.Add(num2, item2);
						}
					}
					ExCostSortedList1 exCostSortedList3 = exCostSortedList;
					if (exCostSortedList2.IsValid && exCostSortedList2.Cost < exCostSortedList.Cost * 3f)
					{
						exCostSortedList3 = exCostSortedList2;
					}
					if (!exCostSortedList3.IsValid)
					{
						attributes[i] = VertexAttribute.Invalid;
						continue;
					}
					VertexAttribute value = proxyAttributes[exCostSortedList3.Data];
					if (value.IsInvalid())
					{
						attributes[i] = VertexAttribute.Invalid;
						continue;
					}
					MappingWorkData value2 = new MappingWorkData
					{
						position = float5,
						vertexIndex = i,
						proxyVertexIndex = exCostSortedList3.Data,
						proxyVertexDistance = exCostSortedList3.Cost
					};
					mappingWorkData[i] = value2;
					attributes[i] = value;
				}
			}
		}

		[BurstCompile]
		private struct Mapping_CalcWeightJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<MappingWorkData> mappingWorkData;

			public NativeArray<VertexAttribute> attributes;

			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[ReadOnly]
			public NativeArray<VertexAttribute> proxyAttributes;

			[ReadOnly]
			public NativeArray<float3> proxyLocalPositions;

			[ReadOnly]
			public NativeArray<float3> proxyLocalNormals;

			[ReadOnly]
			public NativeArray<uint> proxyVertexToVertexIndexArray;

			[ReadOnly]
			public NativeArray<ushort> proxyVertexToVertexDataArray;

			public void Execute(int vindex)
			{
				if (attributes[vindex].IsInvalid())
				{
					return;
				}
				MappingWorkData obj = mappingWorkData[vindex];
				int proxyVertexIndex = obj.proxyVertexIndex;
				float3 position = obj.position;
				float3 float5 = proxyLocalPositions[proxyVertexIndex];
				float3 ontoB = proxyLocalNormals[proxyVertexIndex];
				float3 a = position - float5;
				position -= math.project(a, ontoB);
				float num = math.distance(position, float5);
				float num2 = num * 4f;
				ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
				exCostSortedList.Add(num, proxyVertexIndex);
				DataUtility.Unpack12_20(proxyVertexToVertexIndexArray[proxyVertexIndex], out var hi, out var low);
				for (int i = 0; i < hi; i++)
				{
					int num3 = proxyVertexToVertexDataArray[low + i];
					if (exCostSortedList.Contains(num3))
					{
						continue;
					}
					float3 y = proxyLocalPositions[num3];
					float num4 = math.distance(position, y);
					if (num4 <= num2)
					{
						exCostSortedList.Add(num4, num3);
					}
					DataUtility.Unpack12_20(proxyVertexToVertexIndexArray[num3], out var hi2, out var low2);
					for (int j = 0; j < hi2; j++)
					{
						int num5 = proxyVertexToVertexDataArray[low2 + j];
						if (num5 != proxyVertexIndex && num5 != num3 && !exCostSortedList.Contains(num5))
						{
							y = proxyLocalPositions[num5];
							num4 = math.distance(position, y);
							if (num4 <= num2)
							{
								exCostSortedList.Add(num4, num5);
							}
						}
					}
				}
				float4 weights = CalcVertexWeights(exCostSortedList.costs);
				VirtualMeshBoneWeight value = new VirtualMeshBoneWeight(exCostSortedList.data, weights);
				boneWeights[vindex] = value;
				float num6 = 0f;
				float num7 = 0f;
				int count = value.Count;
				for (int k = 0; k < count; k++)
				{
					proxyVertexIndex = value.boneIndices[k];
					VertexAttribute vertexAttribute = proxyAttributes[proxyVertexIndex];
					if (vertexAttribute.IsMove())
					{
						num7 += value.weights[k];
					}
					else if (vertexAttribute.IsFixed())
					{
						num6 += value.weights[k];
					}
				}
				attributes[vindex] = ((num7 > num6) ? VertexAttribute.Move : VertexAttribute.Fixed);
			}
		}

		[BurstCompile]
		private struct Optimize_EdgeToTrianlgeJob : IJob
		{
			public int tcnt;

			[ReadOnly]
			public NativeArray<int3> triangles;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			public NativeParallelHashMap<int2, FixedList128Bytes<int>> edgeToTriangleList;

			[WriteOnly]
			public NativeList<int3> newTriangles;

			public NativeParallelHashSet<int4> useQuadSet;

			public NativeParallelHashSet<int3> removeTriangleSet;

			public void Execute()
			{
				for (int i = 0; i < tcnt; i++)
				{
					int3 int5 = triangles[i];
					int2x3 int2x5 = new int2x3(int5.xy, int5.yz, int5.zx);
					for (int j = 0; j < 3; j++)
					{
						int2 key = DataUtility.PackInt2(in int2x5[j]);
						if (edgeToTriangleList.ContainsKey(key))
						{
							FixedList128Bytes<int> fixedList = edgeToTriangleList[key];
							fixedList.MC2Set(i);
							edgeToTriangleList[key] = fixedList;
						}
						else
						{
							FixedList128Bytes<int> fixedList2 = default(FixedList128Bytes<int>);
							fixedList2.MC2Set(i);
							edgeToTriangleList.Add(key, fixedList2);
						}
					}
				}
				foreach (KeyValue<int2, FixedList128Bytes<int>> edgeToTriangle in edgeToTriangleList)
				{
					int2 use = edgeToTriangle.Key;
					float3 v = localPositions[use.x];
					float3 v2 = localPositions[use.y];
					FixedList128Bytes<int> value = edgeToTriangle.Value;
					int length = value.Length;
					for (int k = 0; k < length - 1; k++)
					{
						int3 data = triangles[value[k]];
						int num = DataUtility.RemainingData(in data, in use);
						float3 v3 = localPositions[num];
						for (int l = k + 1; l < length; l++)
						{
							int3 data2 = triangles[value[l]];
							int num2 = DataUtility.RemainingData(in data2, in use);
							float3 v4 = localPositions[num2];
							if (math.abs(math.degrees(MathUtility.TriangleAngle(in v, in v2, in v3, in v4))) > 20f)
							{
								continue;
							}
							MathUtility.ClosestPtSegmentSegment(in v, in v2, in v3, in v4, out var s, out var t, out var _, out var _);
							if (s != 0f && s != 1f && t != 0f && t != 1f)
							{
								int4 item = DataUtility.PackInt4(use.x, use.y, num, num2);
								if (useQuadSet.Contains(item))
								{
									removeTriangleSet.Add(data);
									removeTriangleSet.Add(data2);
								}
								else
								{
									useQuadSet.Add(item);
								}
							}
						}
					}
				}
				for (int m = 0; m < tcnt; m++)
				{
					int3 int6 = triangles[m];
					if (!removeTriangleSet.Contains(int6))
					{
						newTriangles.AddNoResize(int6);
					}
				}
			}
		}

		[BurstCompile]
		private struct ProxyNormalRadiationAdjustmentJob : IJobParallelFor
		{
			public float3 center;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			public NativeArray<float3> localNormals;

			public NativeArray<float3> localTangents;

			[WriteOnly]
			public NativeArray<quaternion> normalAdjustmentRotations;

			public void Execute(int vindex)
			{
				float3 x = localPositions[vindex] - center;
				if (!(math.length(x) < 1E-08f))
				{
					float3 nor = math.normalize(x);
					float3 nor2 = localNormals[vindex];
					float3 tan = localTangents[vindex];
					quaternion q = MathUtility.ToRotation(in nor2, in tan);
					tan = ((!(math.dot(nor, tan) < 0.99f)) ? math.normalize(math.cross(math.normalize(math.cross(nor2, tan)), nor)) : math.normalize(math.cross(math.normalize(math.cross(nor, tan)), nor)));
					localNormals[vindex] = nor;
					localTangents[vindex] = tan;
					quaternion b = MathUtility.ToRotation(in nor, in tan);
					normalAdjustmentRotations[vindex] = math.mul(math.inverse(q), b);
				}
			}
		}

		[BurstCompile]
		private struct ProxyCreateFixedListAndAABBJob : IJob
		{
			public int vcnt;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<uint> vertexToVertexIndexArray;

			[ReadOnly]
			public NativeArray<ushort> vertexToVertexDataArray;

			[WriteOnly]
			public NativeReference<AABB> outAABB;

			[WriteOnly]
			public NativeList<ushort> fixedList;

			[WriteOnly]
			public NativeReference<float3> localCenterPosition;

			public void Execute()
			{
				fixedList.Clear();
				float3 value = 0;
				int num = 0;
				int num2 = 0;
				float3 min = float.MaxValue;
				float3 max = float.MinValue;
				for (int i = 0; i < vcnt; i++)
				{
					float3 float5 = localPositions[i];
					if (!attributes[i].IsMove())
					{
						DataUtility.Unpack12_20(vertexToVertexIndexArray[i], out var hi, out var low);
						int j;
						for (j = 0; j < hi; j++)
						{
							int index = vertexToVertexDataArray[low + j];
							if (attributes[index].IsMove())
							{
								break;
							}
						}
						if (j == hi && hi > 0)
						{
							continue;
						}
						fixedList.Add((ushort)i);
						value += float5;
						num++;
					}
					min = math.min(min, float5);
					max = math.max(max, float5);
					num2++;
				}
				outAABB.Value = ((num2 > 0) ? new AABB(in min, in max) : default(AABB));
				if (num > 0)
				{
					value /= (float)num;
				}
				localCenterPosition.Value = value;
			}
		}

		[BurstCompile]
		private struct Proxy_CalcTriangleNormalJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int3> triangles;

			[ReadOnly]
			public NativeArray<float3> localPositins;

			[WriteOnly]
			public NativeArray<float3> triangleNormals;

			public void Execute(int tindex)
			{
				int3 int5 = triangles[tindex];
				float3 value = MathUtility.TriangleNormal(localPositins[int5.x], localPositins[int5.y], localPositins[int5.z]);
				triangleNormals[tindex] = value;
			}
		}

		[BurstCompile]
		private struct Proxy_CalcTriangleTangentJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int3> triangles;

			[ReadOnly]
			public NativeArray<float3> localPositins;

			[ReadOnly]
			public NativeArray<float2> uv;

			[WriteOnly]
			public NativeArray<float3> triangleTangents;

			public void Execute(int tindex)
			{
				int3 int5 = triangles[tindex];
				float3 value = MathUtility.TriangleTangent(localPositins[int5.x], localPositins[int5.y], localPositins[int5.z], uv[int5.x], uv[int5.y], uv[int5.z]);
				triangleTangents[tindex] = value;
			}
		}

		[BurstCompile]
		private struct Proxy_CreateVertexToTrianglesJob : IJob
		{
			[ReadOnly]
			public NativeArray<int3> triangles;

			public NativeArray<FixedList32Bytes<uint>> vertexToTriangles;

			public unsafe void Execute()
			{
				FixedList32Bytes<uint>* unsafePtr = (FixedList32Bytes<uint>*)vertexToTriangles.GetUnsafePtr();
				int length = triangles.Length;
				for (uint num = 0u; num < length; num++)
				{
					int3 int5 = triangles[(int)num];
					FixedList32Bytes<uint>* ptr = unsafePtr + int5.x;
					FixedList32Bytes<uint>* ptr2 = unsafePtr + int5.y;
					FixedList32Bytes<uint>* ptr3 = unsafePtr + int5.z;
					if (ptr->Length < 7)
					{
						(*ptr).MC2Set(num);
					}
					if (ptr2->Length < 7)
					{
						(*ptr2).MC2Set(num);
					}
					if (ptr3->Length < 7)
					{
						(*ptr3).MC2Set(num);
					}
				}
			}
		}

		[BurstCompile]
		private struct Proxy_OrganizeVertexToTrianglsJob : IJobParallelFor
		{
			public NativeArray<FixedList32Bytes<uint>> vertexToTriangles;

			[ReadOnly]
			public NativeArray<float3> triangleNormals;

			[ReadOnly]
			public NativeArray<float3> triangleTangents;

			public NativeArray<VertexAttribute> attributes;

			public void Execute(int vindex)
			{
				FixedList32Bytes<uint> value = vertexToTriangles[vindex];
				int length = value.Length;
				if (length == 0)
				{
					return;
				}
				VertexAttribute value2 = attributes[vindex];
				value2.SetFlag(128, sw: true);
				attributes[vindex] = value2;
				float3 x = 0;
				float3 x2 = 0;
				for (int i = 0; i < length; i++)
				{
					int index = (int)value[i];
					x += triangleNormals[index];
					x2 += triangleTangents[index];
				}
				if (math.length(x) < 0.5f)
				{
					float num = -1f;
					x = 0;
					for (int j = 0; j < length; j++)
					{
						int num2 = (int)value[j];
						float3 x3 = 0;
						float3 float5 = triangleNormals[num2];
						for (int k = 0; k < length; k++)
						{
							int num3 = (int)value[k];
							if (num3 != num2)
							{
								float3 float6 = triangleNormals[num3];
								if (math.dot(float5, float6) >= 0f)
								{
									x3 += float6;
								}
								else
								{
									x3 += -float6;
								}
							}
						}
						float num4 = math.lengthsq(x3);
						if (num4 > num)
						{
							num = num4;
							x = float5;
						}
					}
				}
				else
				{
					x = math.normalize(x);
				}
				if (math.length(x2) < 0.5f)
				{
					float num5 = -1f;
					x2 = 0;
					for (int l = 0; l < length; l++)
					{
						int num6 = (int)value[l];
						float3 x4 = 0;
						float3 float7 = triangleTangents[num6];
						for (int m = 0; m < length; m++)
						{
							int num7 = (int)value[m];
							if (num7 != num6)
							{
								float3 float8 = triangleTangents[num7];
								if (math.dot(float7, float8) >= 0f)
								{
									x4 += float8;
								}
								else
								{
									x4 += -float8;
								}
							}
						}
						float num8 = math.lengthsq(x4);
						if (num8 > num5)
						{
							num5 = num8;
							x2 = float7;
						}
					}
				}
				else
				{
					x2 = math.normalize(x2);
				}
				for (int n = 0; n < length; n++)
				{
					int num9 = (int)value[n];
					float3 y = triangleNormals[num9];
					float3 y2 = triangleTangents[num9];
					int num10 = 0;
					if (math.dot(x, y) < 0f)
					{
						num10 |= 1;
					}
					if (math.dot(x2, y2) < 0f)
					{
						num10 |= 2;
					}
					value[n] = DataUtility.Pack12_20(num10, num9);
				}
				vertexToTriangles[vindex] = value;
			}
		}

		[BurstCompile]
		private struct Proxy_CalcVertexNormalTangentFromTriangleJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<float3> triangleNormals;

			[ReadOnly]
			public NativeArray<float3> triangleTangents;

			[ReadOnly]
			public NativeArray<FixedList32Bytes<uint>> vertexToTriangles;

			public NativeArray<float3> localNormals;

			public NativeArray<float3> localTangents;

			public void Execute(int vindex)
			{
				FixedList32Bytes<uint> fixedList32Bytes = vertexToTriangles[vindex];
				int length = fixedList32Bytes.Length;
				if (length > 0)
				{
					float3 x = 0;
					float3 y = 0;
					for (int i = 0; i < length; i++)
					{
						uint pack = fixedList32Bytes[i];
						int num = DataUtility.Unpack12_20Hi(pack);
						int index = DataUtility.Unpack12_20Low(pack);
						x += triangleNormals[index] * (((num & 1) == 0) ? 1 : (-1));
						y += triangleTangents[index] * (((num & 2) == 0) ? 1 : (-1));
					}
					x = math.normalize(x);
					float3 value = math.normalize(math.cross(x, y));
					localNormals[vindex] = x;
					localTangents[vindex] = value;
				}
			}
		}

		[BurstCompile]
		private struct Proxy_CalcVertexToTransformJob : IJobParallelFor
		{
			public quaternion invRot;

			[ReadOnly]
			public NativeArray<float3> localNormals;

			[ReadOnly]
			public NativeArray<float3> localTangents;

			[WriteOnly]
			public NativeArray<quaternion> vertexToTransformRotations;

			[ReadOnly]
			public NativeArray<quaternion> transformRotations;

			public void Execute(int vindex)
			{
				quaternion b = math.mul(invRot, transformRotations[vindex]);
				quaternion value = math.mul(math.inverse(MathUtility.ToRotation(localNormals[vindex], localTangents[vindex])), b);
				vertexToTransformRotations[vindex] = value;
			}
		}

		[BurstCompile]
		private struct Proxy_CalcEdgeToTriangleJob : IJob
		{
			public int tcnt;

			[ReadOnly]
			public NativeArray<int3> triangles;

			public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

			public void Execute()
			{
				for (int i = 0; i < tcnt; i++)
				{
					int3 int5 = triangles[i];
					int2x3 int2x5 = new int2x3(DataUtility.PackInt2(int5.xy), DataUtility.PackInt2(int5.yz), DataUtility.PackInt2(int5.zx));
					for (int j = 0; j < 3; j++)
					{
						int2 key = int2x5[j];
						edgeToTriangles.MC2UniqueAdd(key, (ushort)i);
					}
				}
			}
		}

		[BurstCompile]
		private struct Proxy_CalcVertexBindPoseJob2 : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<float3> localNormals;

			[ReadOnly]
			public NativeArray<float3> localTangents;

			[WriteOnly]
			public NativeArray<float3> vertexBindPosePositions;

			[WriteOnly]
			public NativeArray<quaternion> vertexBindPoseRotations;

			public void Execute(int vindex)
			{
				float3 float5 = localPositions[vindex];
				quaternion q = MathUtility.ToRotation(localNormals[vindex], localTangents[vindex]);
				vertexBindPosePositions[vindex] = -float5;
				vertexBindPoseRotations[vindex] = math.inverse(q);
			}
		}

		[BurstCompile]
		private struct Proxy_CalcVertexToVertexFromTriangleJob : IJob
		{
			public int triangleCount;

			[ReadOnly]
			public NativeArray<int3> triangles;

			public NativeParallelMultiHashMap<int, ushort> vertexToVertexMap;

			public NativeParallelHashSet<int2> edgeSet;

			public void Execute()
			{
				for (int i = 0; i < triangleCount; i++)
				{
					int3 int5 = triangles[i];
					ushort value = (ushort)int5.x;
					ushort value2 = (ushort)int5.y;
					ushort value3 = (ushort)int5.z;
					vertexToVertexMap.MC2UniqueAdd(int5.x, value2);
					vertexToVertexMap.MC2UniqueAdd(int5.x, value3);
					vertexToVertexMap.MC2UniqueAdd(int5.y, value);
					vertexToVertexMap.MC2UniqueAdd(int5.y, value3);
					vertexToVertexMap.MC2UniqueAdd(int5.z, value);
					vertexToVertexMap.MC2UniqueAdd(int5.z, value2);
					edgeSet.Add(DataUtility.PackInt2(int5.xy));
					edgeSet.Add(DataUtility.PackInt2(int5.yz));
					edgeSet.Add(DataUtility.PackInt2(int5.zx));
				}
			}
		}

		[BurstCompile]
		private struct Proxy_CalcVertexToVertexFromLineJob : IJob
		{
			public int lineCount;

			[ReadOnly]
			public NativeArray<int2> lines;

			public NativeParallelMultiHashMap<int, ushort> vertexToVertexMap;

			public NativeParallelHashSet<int2> edgeSet;

			public void Execute()
			{
				for (int i = 0; i < lineCount; i++)
				{
					int2 d = lines[i];
					vertexToVertexMap.MC2UniqueAdd(d.x, (ushort)d.y);
					vertexToVertexMap.MC2UniqueAdd(d.y, (ushort)d.x);
					edgeSet.Add(DataUtility.PackInt2(in d));
				}
			}
		}

		[BurstCompile]
		private struct Proxy_CreateEdgeFlagJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int2> edges;

			[ReadOnly]
			public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

			[WriteOnly]
			public NativeArray<ExBitFlag8> edgeFlags;

			public void Execute(int eindex)
			{
				ExBitFlag8 value = default(ExBitFlag8);
				int2 key = edges[eindex];
				if (edgeToTriangles.ContainsKey(key) && edgeToTriangles.CountValuesForKey(key) <= 1)
				{
					value.SetFlag(1, sw: true);
				}
				edgeFlags[eindex] = value;
			}
		}

		private struct SkinningBoneInfo
		{
			public int parentTransformIndex;

			public float3 parentPos;

			public int childTransformIndex;

			public float3 childPos;
		}

		[BurstCompile]
		private struct Proxy_CalcCustomSkinningWeightsJobV2 : IJobParallelFor
		{
			public bool isBoneCloth;

			public float angularAttenuation;

			public float distanceReduction;

			public float distancePow;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeList<SkinningBoneInfo> boneInfoList;

			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			public void Execute(int vindex)
			{
				if (attributes[vindex].IsDontMove())
				{
					return;
				}
				float3 float5 = localPositions[vindex];
				ExCostSortedList4 exCostSortedList = new ExCostSortedList4(-1f);
				int length = boneInfoList.Length;
				for (int i = 0; i < length; i++)
				{
					SkinningBoneInfo skinningBoneInfo = boneInfoList[i];
					float3 childPos = skinningBoneInfo.childPos;
					int childTransformIndex = skinningBoneInfo.childTransformIndex;
					float num = math.length(float5 - childPos);
					int num2 = exCostSortedList.indexOf(childTransformIndex);
					if (num2 >= 0)
					{
						if (num < exCostSortedList.costs[num2])
						{
							exCostSortedList.RemoveItem(childTransformIndex);
							exCostSortedList.Add(num, childTransformIndex);
						}
					}
					else
					{
						exCostSortedList.Add(num, childTransformIndex);
					}
				}
				int count = exCostSortedList.Count;
				float num3 = exCostSortedList.MinCost * distanceReduction;
				for (int j = 0; j < count; j++)
				{
					exCostSortedList.costs[j] = exCostSortedList.costs[j] - num3;
				}
				for (int k = 0; k < count; k++)
				{
					exCostSortedList.costs[k] = math.pow(exCostSortedList.costs[k], distancePow);
				}
				if (exCostSortedList.MinCost < 1E-08f)
				{
					exCostSortedList.costs = new float4(1f, 0f, 0f, 0f);
					exCostSortedList.data = new int4(exCostSortedList.data[0], 0, 0, 0);
				}
				else
				{
					count = exCostSortedList.Count;
					float num4 = 0f;
					for (int l = 0; l < count; l++)
					{
						num4 += 1f / exCostSortedList.costs[l];
					}
					for (int m = 0; m < count; m++)
					{
						exCostSortedList.costs[m] = 1f / exCostSortedList.costs[m] / num4;
					}
					num4 = 0f;
					for (int n = 0; n < 4; n++)
					{
						if (exCostSortedList.costs[n] < 0.001f || n >= count)
						{
							exCostSortedList.costs[n] = 0f;
							exCostSortedList.data[n] = 0;
						}
						else
						{
							num4 += exCostSortedList.costs[n];
						}
					}
					exCostSortedList.costs /= num4;
				}
				VirtualMeshBoneWeight value = new VirtualMeshBoneWeight(exCostSortedList.data, exCostSortedList.costs);
				boneWeights[vindex] = value;
			}
		}

		[BurstCompile]
		private struct Proxy_ApplySelectionJob : IJobParallelFor
		{
			public float gridSize;

			public float radius;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			[ReadOnly]
			public NativeArray<float3> selectionPositions;

			[ReadOnly]
			public NativeArray<VertexAttribute> selectionAttributes;

			public void Execute(int vindex)
			{
				float3 float5 = localPositions[vindex];
				VertexAttribute value = attributes[vindex];
				float num = float.MaxValue;
				VertexAttribute attr = VertexAttribute.Invalid;
				foreach (int3 item in GridMap<int>.GetArea(float5, radius, gridMap, gridSize))
				{
					if (!gridMap.ContainsKey(item))
					{
						continue;
					}
					foreach (int item2 in gridMap.GetValuesForKey(item))
					{
						float3 y = selectionPositions[item2];
						float num2 = math.distance(float5, y);
						if (!(num2 > radius) && !(num2 > num))
						{
							num = num2;
							attr = selectionAttributes[item2];
						}
					}
				}
				value.SetFlag(attr, sw: true);
				attributes[vindex] = value;
			}
		}

		[BurstCompile]
		private struct Proxy_BoneClothApplayTransformFlagJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			public NativeArray<ExBitFlag8> transformFlags;

			public void Execute(int vindex)
			{
				VertexAttribute vertexAttribute = attributes[vindex];
				ExBitFlag8 value = transformFlags[vindex];
				if (vertexAttribute.IsMove())
				{
					value.SetFlag(4, sw: true);
				}
				else if (vertexAttribute.IsFixed())
				{
					value.SetFlag(2, sw: true);
				}
				if (!vertexAttribute.IsInvalid())
				{
					value.SetFlag(8, sw: true);
				}
				transformFlags[vindex] = value;
			}
		}

		private struct BaseLineWork : IComparable<BaseLineWork>
		{
			public int vindex;

			public float dist;

			public int CompareTo(BaseLineWork other)
			{
				return (int)math.sign(dist - other.dist);
			}
		}

		[BurstCompile]
		private struct BaseLine_Mesh_CreateParentJob2 : IJob
		{
			public int vcnt;

			public float avgDist;

			[ReadOnly]
			public NativeArray<VertexAttribute> attribues;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<uint> vertexToVertexIndexArray;

			[ReadOnly]
			public NativeArray<ushort> vertexToVertexDataArray;

			public NativeArray<int> vertexParentIndices;

			public NativeParallelMultiHashMap<int, ushort> vertexChildMap;

			[ReadOnly]
			public NativeList<int> fixedList;

			public NativeList<BaseLineWork> nextList;

			public NativeArray<byte> markBuff;

			public NativeParallelHashMap<int, BaseLineWork> vertexMap;

			public void Execute()
			{
				foreach (int @fixed in fixedList)
				{
					nextList.Add(new BaseLineWork
					{
						vindex = @fixed,
						dist = 0f
					});
				}
				int num = 0;
				while (nextList.Length > 0)
				{
					foreach (BaseLineWork next in nextList)
					{
						int vindex = next.vindex;
						if (attribues[vindex].IsDontMove())
						{
							continue;
						}
						float3 float5 = localPositions[vindex];
						ExCostSortedList1 exCostSortedList = new ExCostSortedList1(-1f, -1);
						DataUtility.Unpack12_20(vertexToVertexIndexArray[vindex], out var hi, out var low);
						for (int i = 0; i < hi; i++)
						{
							int num2 = vertexToVertexDataArray[low + i];
							if (markBuff[num2] != 0)
							{
								float3 float6 = localPositions[num2];
								if (attribues[num2].IsDontMove())
								{
									float cost = math.distance(float5, float6);
									exCostSortedList.Add(cost, num2);
								}
								else
								{
									int index = vertexParentIndices[num2];
									float cost2 = MathUtility.Angle(float6 - float5, localPositions[index] - float6);
									exCostSortedList.Add(cost2, num2);
								}
							}
						}
						if (exCostSortedList.IsValid)
						{
							int data = exCostSortedList.data;
							vertexParentIndices[vindex] = data;
							markBuff[vindex] = 1;
						}
					}
					foreach (BaseLineWork next2 in nextList)
					{
						int vindex2 = next2.vindex;
						markBuff[vindex2] = 2;
						int num3 = vertexParentIndices[vindex2];
						if (num3 >= 0)
						{
							vertexChildMap.MC2UniqueAdd(num3, (ushort)vindex2);
						}
					}
					vertexMap.Clear();
					int num4 = 0;
					foreach (BaseLineWork next3 in nextList)
					{
						int vindex3 = next3.vindex;
						DataUtility.Unpack12_20(vertexToVertexIndexArray[vindex3], out var hi2, out var low2);
						if (hi2 == 0)
						{
							continue;
						}
						float3 x = localPositions[vindex3];
						for (int j = 0; j < hi2; j++)
						{
							int num5 = vertexToVertexDataArray[low2 + j];
							if (attribues[num5].IsInvalid() || markBuff[num5] != 0)
							{
								continue;
							}
							float num6 = math.distance(x, localPositions[num5]);
							if (vertexMap.ContainsKey(num5))
							{
								BaseLineWork value = vertexMap[num5];
								if (num6 < value.dist)
								{
									value.dist = num6;
									vertexMap[num5] = value;
								}
							}
							else
							{
								vertexMap.Add(num5, new BaseLineWork
								{
									vindex = num5,
									dist = num6
								});
								num4++;
							}
						}
					}
					nextList.Clear();
					if (num4 > 0)
					{
						foreach (KeyValue<int, BaseLineWork> item in vertexMap)
						{
							nextList.Add(in item.Value);
						}
						nextList.Sort();
					}
					num++;
				}
			}
		}

		[BurstCompile]
		private struct BaseLine_Mesh_CareteFixedListJob : IJob
		{
			public int vcnt;

			[ReadOnly]
			public NativeArray<VertexAttribute> attribues;

			public NativeList<int> fixedList;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					if (attribues[i].IsFixed())
					{
						fixedList.Add(in i);
					}
				}
			}
		}

		[BurstCompile]
		private struct BaseLine_Bone_CreateBoneChildInfoJob : IJob
		{
			public int vcnt;

			[ReadOnly]
			public NativeArray<int> parentIndices;

			public NativeParallelMultiHashMap<int, ushort> childMap;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					int num = parentIndices[i];
					if (num >= 0)
					{
						childMap.Add(num, (ushort)i);
					}
				}
			}
		}

		[BurstCompile]
		private struct BaseLine_CalcLocalPositionRotationJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int> parentIndices;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<float3> localNormals;

			[ReadOnly]
			public NativeArray<float3> localTangents;

			[ReadOnly]
			public NativeArray<ushort> baseLineIndices;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> vertexLocalPositions;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> vertexLocalRotations;

			public void Execute(int index)
			{
				int index2 = baseLineIndices[index];
				int num = parentIndices[index2];
				if (num >= 0)
				{
					float3 float5 = localPositions[num];
					quaternion obj = math.inverse(MathUtility.ToRotation(localNormals[num], localTangents[num]));
					float3 float6 = localPositions[index2];
					quaternion b = MathUtility.ToRotation(localNormals[index2], localTangents[index2]);
					float3 value = math.mul(obj, float6 - float5);
					quaternion value2 = math.mul(obj, b);
					vertexLocalPositions[index2] = value;
					vertexLocalRotations[index2] = value2;
				}
				else
				{
					vertexLocalPositions[index2] = 0;
					vertexLocalRotations[index2] = quaternion.identity;
				}
			}
		}

		[BurstCompile]
		private struct BaseLine_CalcMaxBaseLineLengthJob : IJob
		{
			public int vcnt;

			[ReadOnly]
			public NativeArray<VertexAttribute> attribues;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			[WriteOnly]
			public NativeArray<float> vertexDepths;

			[WriteOnly]
			public NativeArray<int> vertexRootIndices;

			public NativeArray<float> rootLengthArray;

			public void Execute()
			{
				float num = 0f;
				for (int i = 0; i < vcnt; i++)
				{
					int value = -1;
					float num2 = 0f;
					if (attribues[i].IsMove())
					{
						int index = i;
						for (int num3 = vertexParentIndices[index]; num3 >= 0; num3 = vertexParentIndices[index])
						{
							float3 x = localPositions[index];
							float3 y = localPositions[num3];
							float num4 = math.distance(x, y);
							num2 += num4;
							value = num3;
							if (!attribues[num3].IsMove())
							{
								break;
							}
							index = num3;
						}
					}
					vertexRootIndices[i] = value;
					rootLengthArray[i] = num2;
					num = math.max(num, num2);
				}
				if (num > 1E-08f)
				{
					for (int j = 0; j < vcnt; j++)
					{
						float value2 = math.saturate(rootLengthArray[j] / num);
						vertexDepths[j] = value2;
					}
				}
			}
		}

		[BurstCompile]
		private struct Reduction_InitVertexToVertexJob2 : IJob
		{
			public int triangleCount;

			[ReadOnly]
			public NativeArray<int3> triangles;

			public NativeParallelMultiHashMap<ushort, ushort> vertexToVertexMap;

			public void Execute()
			{
				for (int i = 0; i < triangleCount; i++)
				{
					int3 obj = triangles[i];
					ushort num = (ushort)obj.x;
					ushort num2 = (ushort)obj.y;
					ushort num3 = (ushort)obj.z;
					vertexToVertexMap.Add(num, num2);
					vertexToVertexMap.Add(num, num3);
					vertexToVertexMap.Add(num2, num);
					vertexToVertexMap.Add(num2, num3);
					vertexToVertexMap.Add(num3, num);
					vertexToVertexMap.Add(num3, num2);
				}
			}
		}

		[BurstCompile]
		private struct Organize_RemapVertexJob : IJob
		{
			public int oldVertexCount;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			public NativeArray<int> vertexRemapIndices;

			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < oldVertexCount; i++)
				{
					if (joinIndices[i] < 0)
					{
						vertexRemapIndices[i] = num;
						num++;
					}
				}
				for (int j = 0; j < oldVertexCount; j++)
				{
					int num2 = joinIndices[j];
					if (num2 >= 0)
					{
						vertexRemapIndices[j] = vertexRemapIndices[num2];
					}
				}
			}
		}

		[BurstCompile]
		private struct Organize_CollectUseSkinBoneJob : IJob
		{
			public int oldVertexCount;

			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> oldBoneWeights;

			[ReadOnly]
			public NativeArray<float4x4> oldBindPoses;

			public NativeParallelHashMap<int, int> useSkinBoneMap;

			public NativeList<int> newSkinBoneTransformIndices;

			public NativeList<float4x4> newSkinBoneBindPoses;

			public NativeReference<int> newSkinBoneCount;

			public NativeList<int> useSkinBoneMapKeyList;

			public void Execute()
			{
				for (int i = 0; i < oldVertexCount; i++)
				{
					if (joinIndices[i] >= 0)
					{
						continue;
					}
					VirtualMeshBoneWeight virtualMeshBoneWeight = oldBoneWeights[i];
					for (int j = 0; j < 4; j++)
					{
						if (virtualMeshBoneWeight.weights[j] > 0f)
						{
							_ = virtualMeshBoneWeight.boneIndices[j];
							useSkinBoneMap.TryAdd(virtualMeshBoneWeight.boneIndices[j], 0);
						}
					}
				}
				useSkinBoneMapKeyList.Clear();
				foreach (KeyValue<int, int> item in useSkinBoneMap)
				{
					useSkinBoneMapKeyList.Add(item.Key);
				}
				for (int k = 0; k < useSkinBoneMapKeyList.Length; k++)
				{
					int num = useSkinBoneMapKeyList[k];
					useSkinBoneMap[num] = k;
					newSkinBoneTransformIndices.Add(in k);
					newSkinBoneBindPoses.Add(oldBindPoses[num]);
				}
				newSkinBoneCount.Value = useSkinBoneMapKeyList.Length;
			}
		}

		[BurstCompile]
		private struct Organize_CopyVertexJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeArray<int> vertexRemapIndices;

			[ReadOnly]
			public NativeArray<VertexAttribute> oldAttributes;

			[ReadOnly]
			public NativeArray<float3> oldLocalPositions;

			[ReadOnly]
			public NativeArray<float3> oldLocalNormals;

			[ReadOnly]
			public NativeArray<float3> oldLocalTangents;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VertexAttribute> newAttributes;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> newLocalPositions;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> newLocalNormals;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> newLocalTangents;

			public void Execute(int index)
			{
				if (joinIndices[index] < 0)
				{
					int index2 = vertexRemapIndices[index];
					newAttributes[index2] = oldAttributes[index];
					newLocalPositions[index2] = oldLocalPositions[index];
					newLocalNormals[index2] = oldLocalNormals[index];
					newLocalTangents[index2] = oldLocalTangents[index];
				}
			}
		}

		[BurstCompile]
		private struct Organize_RemapBoneWeightJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeArray<int> vertexRemapIndices;

			[ReadOnly]
			public NativeParallelHashMap<int, int> useSkinBoneMap;

			[ReadOnly]
			public NativeArray<int> oldSkinBoneIndices;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> oldBoneWeights;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VirtualMeshBoneWeight> newBoneWeights;

			public void Execute(int vindex)
			{
				if (joinIndices[vindex] >= 0)
				{
					return;
				}
				int index = vertexRemapIndices[vindex];
				VirtualMeshBoneWeight value = oldBoneWeights[vindex];
				for (int i = 0; i < 4; i++)
				{
					if (value.weights[i] > 0f)
					{
						int key = value.boneIndices[i];
						value.boneIndices[i] = useSkinBoneMap[key];
					}
					else
					{
						value.boneIndices[i] = 0;
					}
				}
				newBoneWeights[index] = value;
			}
		}

		[BurstCompile]
		private struct Organize_RemapLinkPointArrayJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<int> joinIndices;

			[ReadOnly]
			public NativeArray<int> vertexRemapIndices;

			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> oldVertexToVertexMap;

			[NativeDisableParallelForRestriction]
			public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

			public void Execute(int vindex)
			{
				if (joinIndices[vindex] >= 0)
				{
					return;
				}
				int num = vertexRemapIndices[vindex];
				foreach (ushort item in oldVertexToVertexMap.GetValuesForKey((ushort)vindex))
				{
					int num2 = vertexRemapIndices[item];
					newVertexToVertexMap.MC2UniqueAdd((ushort)num, (ushort)num2);
				}
			}
		}

		[BurstCompile]
		private struct Organize_CreateLineTriangleJob : IJob
		{
			public int newVertexCount;

			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

			[WriteOnly]
			public NativeParallelHashSet<int2> edgeSet;

			public void Execute()
			{
				for (int i = 0; i < newVertexCount; i++)
				{
					foreach (ushort item2 in newVertexToVertexMap.GetValuesForKey((ushort)i))
					{
						int2 item = DataUtility.PackInt2(i, item2);
						edgeSet.Add(item);
					}
				}
			}
		}

		[BurstCompile]
		private struct Organize_CreateLineTriangleJob2 : IJob
		{
			[ReadOnly]
			public NativeParallelMultiHashMap<ushort, ushort> newVertexToVertexMap;

			[WriteOnly]
			public NativeList<int2> newLineList;

			[ReadOnly]
			public NativeParallelHashSet<int2> edgeSet;

			[WriteOnly]
			public NativeParallelHashSet<int3> triangleSet;

			public void Execute()
			{
				foreach (int2 item2 in edgeSet)
				{
					int2 value = item2;
					int num = 0;
					foreach (ushort item3 in newVertexToVertexMap.GetValuesForKey((ushort)value.x))
					{
						if (item3 != value.x && item3 != value.y && newVertexToVertexMap.MC2Contains((ushort)value.y, item3))
						{
							int3 item = DataUtility.PackInt3(value.x, value.y, item3);
							triangleSet.Add(item);
							num++;
						}
					}
					if (num == 0)
					{
						newLineList.Add(in value);
					}
				}
			}
		}

		[BurstCompile]
		private struct Organize_CreateNewTriangleJob3 : IJob
		{
			[WriteOnly]
			public NativeList<int3> newTriangleList;

			[ReadOnly]
			public NativeParallelHashSet<int3> triangleSet;

			public void Execute()
			{
				foreach (int3 item in triangleSet)
				{
					int3 value = item;
					newTriangleList.Add(in value);
				}
			}
		}

		[Serializable]
		public class ShareSerializationData
		{
			public string name;

			public MeshType meshType;

			public bool isBoneCloth;

			public ExSimpleNativeArray<int>.SerializationData referenceIndices;

			public ExSimpleNativeArray<VertexAttribute>.SerializationData attributes;

			public ExSimpleNativeArray<float3>.SerializationData localPositions;

			public ExSimpleNativeArray<float3>.SerializationData localNormals;

			public ExSimpleNativeArray<float3>.SerializationData localTangents;

			public ExSimpleNativeArray<float2>.SerializationData uv;

			public ExSimpleNativeArray<VirtualMeshBoneWeight>.SerializationData boneWeights;

			public ExSimpleNativeArray<int3>.SerializationData triangles;

			public ExSimpleNativeArray<int2>.SerializationData lines;

			public int centerTransformIndex;

			public float4x4 initLocalToWorld;

			public float4x4 initWorldToLocal;

			public quaternion initRotation;

			public quaternion initInverseRotation;

			public float3 initScale;

			public int skinRootIndex;

			public ExSimpleNativeArray<int>.SerializationData skinBoneTransformIndices;

			public ExSimpleNativeArray<float4x4>.SerializationData skinBoneBindPoses;

			public TransformData.ShareSerializationData transformData;

			public AABB boundingBox;

			public float averageVertexDistance;

			public float maxVertexDistance;

			public byte[] vertexToTriangles;

			public byte[] vertexToVertexIndexArray;

			public byte[] vertexToVertexDataArray;

			public byte[] edges;

			public byte[] edgeFlags;

			public int2[] edgeToTrianglesKeys;

			public ushort[] edgeToTrianglesValues;

			public byte[] vertexBindPosePositions;

			public byte[] vertexBindPoseRotations;

			public byte[] vertexToTransformRotations;

			public byte[] vertexDepths;

			public byte[] vertexRootIndices;

			public byte[] vertexParentIndices;

			public byte[] vertexChildIndexArray;

			public byte[] vertexChildDataArray;

			public byte[] vertexLocalPositions;

			public byte[] vertexLocalRotations;

			public byte[] normalAdjustmentRotations;

			public byte[] baseLineFlags;

			public byte[] baseLineStartDataIndices;

			public byte[] baseLineDataCounts;

			public byte[] baseLineData;

			public int[] customSkinningBoneIndices;

			public ushort[] centerFixedList;

			public float3 localCenterPosition;

			public float3 centerWorldPosition;

			public quaternion centerWorldRotation;

			public float3 centerWorldScale;

			public float4x4 toProxyMatrix;

			public quaternion toProxyRotation;

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				stringBuilder.AppendLine("===== VirtualMesh.SerializeData =====");
				stringBuilder.AppendLine("name:" + name);
				stringBuilder.AppendLine($"meshType:{meshType}");
				stringBuilder.AppendLine($"isBoneCloth:{isBoneCloth}");
				stringBuilder.AppendLine($"VertexCount:{attributes.count}");
				stringBuilder.AppendLine($"TriangleCount:{triangles.count}");
				stringBuilder.AppendLine($"LineCount:{lines.count}");
				return stringBuilder.ToString();
			}
		}

		[Serializable]
		public class UniqueSerializationData : ITransform
		{
			public TransformData.UniqueSerializationData transformData;

			public void GetUsedTransform(HashSet<Transform> transformSet)
			{
				transformData?.GetUsedTransform(transformSet);
			}

			public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
			{
				transformData?.ReplaceTransform(replaceDict);
			}
		}

		[BurstCompile]
		private struct Work_AverageTriangleDistanceJob : IJob
		{
			public int vcnt;

			public int tcnt;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int3> triangles;

			public NativeReference<float> averageVertexDistance;

			public NativeReference<int> averageCount;

			public NativeReference<float> maxVertexDistance;

			public void Execute()
			{
				int num = math.max(tcnt / 100, 1);
				float num2 = 0f;
				float num3 = 0f;
				int num4 = 0;
				for (int i = 0; i < tcnt; i += num)
				{
					int3 int5 = triangles[i];
					float3 float5 = localPositions[int5.x];
					float3 float6 = localPositions[int5.y];
					float3 float7 = localPositions[int5.z];
					float num5 = math.distancesq(float5, float6);
					float num6 = math.distancesq(float6, float7);
					float num7 = math.distancesq(float7, float5);
					num2 += num5;
					num2 += num6;
					num2 += num7;
					num4 += 3;
					num3 = math.max(num3, num5);
					num3 = math.max(num3, num6);
					num3 = math.max(num3, num7);
				}
				averageVertexDistance.Value += num2;
				averageCount.Value += num4;
				maxVertexDistance.Value = math.max(maxVertexDistance.Value, num3);
			}
		}

		[BurstCompile]
		private struct Work_AverageLineDistanceJob : IJob
		{
			public int vcnt;

			public int lcnt;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int2> lines;

			public NativeReference<float> averageVertexDistance;

			public NativeReference<int> averageCount;

			public NativeReference<float> maxVertexDistance;

			public void Execute()
			{
				int num = math.max(lcnt / 100, 1);
				float num2 = 0f;
				int num3 = 0;
				float num4 = 0f;
				for (int i = 0; i < lcnt; i += num)
				{
					int2 int5 = lines[i];
					float3 x = localPositions[int5.x];
					float3 y = localPositions[int5.y];
					float num5 = math.distancesq(x, y);
					num2 += num5;
					num3++;
					num4 = math.max(num4, num5);
				}
				averageVertexDistance.Value += num2;
				averageCount.Value += num3;
				maxVertexDistance.Value = math.max(maxVertexDistance.Value, num4);
			}
		}

		[BurstCompile]
		private struct Work_AddVertexIndexGirdMapJob : IJob
		{
			public float gridSize;

			public int vcnt;

			[ReadOnly]
			public NativeArray<float3> positins;

			[WriteOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			public void Execute()
			{
				for (int i = 0; i < vcnt; i++)
				{
					GridMap<int>.AddGrid(positins[i], i, gridMap, gridSize);
				}
			}
		}

		[BurstCompile]
		private struct Work_IntersectTriangleJob : IJobParallelFor
		{
			public float3 localRayPos;

			public float3 localRayDir;

			public float3 localRayEndPos;

			public bool doubleSide;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int3> triangles;

			[WriteOnly]
			public NativeList<VirtualMeshRaycastHit>.ParallelWriter hitList;

			public void Execute(int tindex)
			{
				int3 int5 = triangles[tindex];
				float3 p = localPositions[int5.x];
				float3 p2 = localPositions[int5.y];
				float3 p3 = localPositions[int5.z];
				MathUtility.GetTriangleSphere(p, p2, p3, out var sc, out var sr);
				float t = 0f;
				float3 q = 0;
				if (MathUtility.IntersectRaySphere(in localRayPos, in localRayDir, in sc, in sr, ref t, ref q) && MathUtility.IntersectSegmentTriangle(in localRayPos, in localRayEndPos, p, p2, p3, doubleSide, out var _, out var _, out var _, out t))
				{
					float3 position = math.lerp(localRayPos, localRayEndPos, t);
					float3 normal = MathUtility.TriangleNormal(in p, in p2, in p3);
					VirtualMeshRaycastHit value = new VirtualMeshRaycastHit
					{
						type = VirtualMeshPrimitive.Triangle,
						index = tindex,
						position = position,
						distance = t,
						normal = normal
					};
					hitList.AddNoResize(value);
				}
			}
		}

		[BurstCompile]
		private struct Work_IntersectEdgeJob : IJobParallelFor
		{
			public float3 localRayPos;

			public float3 localRayDir;

			public float3 localRayEndPos;

			public float3 rayDir;

			public float localEdgeRadius;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<int2> edges;

			[ReadOnly]
			public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

			[WriteOnly]
			public NativeList<VirtualMeshRaycastHit>.ParallelWriter hitList;

			public void Execute(int eindex)
			{
				int2 key = edges[eindex];
				if (!edgeToTriangles.ContainsKey(key) && !(math.sqrt(MathUtility.ClosestPtSegmentSegment(localPositions[key.x], localPositions[key.y], in localRayPos, in localRayEndPos, out var _, out var t, out var _, out var c2)) > localEdgeRadius))
				{
					float3 position = c2;
					VirtualMeshRaycastHit value = new VirtualMeshRaycastHit
					{
						type = VirtualMeshPrimitive.Edge,
						index = eindex,
						position = position,
						distance = t,
						normal = -rayDir
					};
					hitList.AddNoResize(value);
				}
			}
		}

		[BurstCompile]
		private struct Work_IntersectPointJob : IJobParallelFor
		{
			public float3 localRayPos;

			public float3 localRayDir;

			public float3 rayDir;

			public float localPointRadius;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<FixedList32Bytes<int>> vertexToTriangles;

			[WriteOnly]
			public NativeList<VirtualMeshRaycastHit>.ParallelWriter hitList;

			public void Execute(int vindex)
			{
				if (vertexToTriangles[vindex].Length <= 0)
				{
					float3 sc = localPositions[vindex];
					float t = 0f;
					float3 q = 0;
					if (MathUtility.IntersectRaySphere(in localRayPos, in localRayDir, in sc, in localPointRadius, ref t, ref q))
					{
						VirtualMeshRaycastHit value = new VirtualMeshRaycastHit
						{
							type = VirtualMeshPrimitive.Point,
							index = vindex,
							position = sc,
							distance = t,
							normal = -rayDir
						};
						hitList.AddNoResize(value);
					}
				}
			}
		}

		[BurstCompile]
		private struct Work_IntersetcSortJob : IJob
		{
			public NativeList<VirtualMeshRaycastHit> hitList;

			public void Execute()
			{
				if (hitList.Length > 1)
				{
					hitList.Sort();
				}
			}
		}

		public enum MeshType
		{
			NormalMesh = 0,
			NormalBoneMesh = 1,
			ProxyMesh = 2,
			ProxyBoneMesh = 3,
			Mapping = 4
		}

		public string name = string.Empty;

		public ResultCode result;

		public bool isManaged;

		public MeshType meshType;

		public bool isBoneCloth;

		public ExSimpleNativeArray<int> referenceIndices = new ExSimpleNativeArray<int>();

		public ExSimpleNativeArray<VertexAttribute> attributes = new ExSimpleNativeArray<VertexAttribute>();

		public ExSimpleNativeArray<float3> localPositions = new ExSimpleNativeArray<float3>();

		public ExSimpleNativeArray<float3> localNormals = new ExSimpleNativeArray<float3>();

		public ExSimpleNativeArray<float3> localTangents = new ExSimpleNativeArray<float3>();

		public ExSimpleNativeArray<float2> uv = new ExSimpleNativeArray<float2>();

		public ExSimpleNativeArray<VirtualMeshBoneWeight> boneWeights = new ExSimpleNativeArray<VirtualMeshBoneWeight>();

		public ExSimpleNativeArray<int3> triangles = new ExSimpleNativeArray<int3>();

		public ExSimpleNativeArray<int2> lines = new ExSimpleNativeArray<int2>();

		public int centerTransformIndex = -1;

		public float4x4 initLocalToWorld;

		public float4x4 initWorldToLocal;

		public quaternion initRotation;

		public quaternion initInverseRotation;

		public float3 initScale;

		public int skinRootIndex = -1;

		public ExSimpleNativeArray<int> skinBoneTransformIndices = new ExSimpleNativeArray<int>();

		public ExSimpleNativeArray<float4x4> skinBoneBindPoses = new ExSimpleNativeArray<float4x4>();

		public TransformData transformData;

		public NativeReference<AABB> boundingBox;

		public NativeReference<float> averageVertexDistance;

		public NativeReference<float> maxVertexDistance;

		public DataChunk mergeChunk;

		public NativeArray<int> joinIndices;

		public NativeArray<FixedList32Bytes<uint>> vertexToTriangles;

		public NativeArray<uint> vertexToVertexIndexArray;

		public NativeArray<ushort> vertexToVertexDataArray;

		public NativeArray<int2> edges;

		public const byte EdgeFlag_Cut = 1;

		public NativeArray<ExBitFlag8> edgeFlags;

		public NativeParallelMultiHashMap<int2, ushort> edgeToTriangles;

		public NativeArray<float3> vertexBindPosePositions;

		public NativeArray<quaternion> vertexBindPoseRotations;

		public NativeArray<quaternion> vertexToTransformRotations;

		public NativeArray<float> vertexDepths;

		public NativeArray<int> vertexRootIndices;

		public NativeArray<int> vertexParentIndices;

		public NativeArray<uint> vertexChildIndexArray;

		public NativeArray<ushort> vertexChildDataArray;

		public NativeArray<float3> vertexLocalPositions;

		public NativeArray<quaternion> vertexLocalRotations;

		public NativeArray<quaternion> normalAdjustmentRotations;

		public const byte BaseLineFlag_IncludeLine = 1;

		public NativeArray<ExBitFlag8> baseLineFlags;

		public NativeArray<ushort> baseLineStartDataIndices;

		public NativeArray<ushort> baseLineDataCounts;

		public NativeArray<ushort> baseLineData;

		public int[] customSkinningBoneIndices;

		public ushort[] centerFixedList;

		public NativeReference<float3> localCenterPosition;

		public VirtualMesh mappingProxyMesh;

		public float3 centerWorldPosition;

		public quaternion centerWorldRotation;

		public float3 centerWorldScale;

		public float4x4 toProxyMatrix;

		public quaternion toProxyRotation;

		public int mappingId;

		public float InitCalcScale => initScale.x;

		public bool IsSuccess => result.IsSuccess();

		public bool IsError => result.IsError();

		public bool IsProcess => result.IsProcess();

		public int VertexCount => localPositions.Count;

		public int TriangleCount => triangles.Count;

		public int LineCount => lines.Count;

		public int SkinBoneCount => skinBoneTransformIndices.Count;

		public int TransformCount => transformData?.Count ?? 0;

		public bool IsProxy
		{
			get
			{
				if (meshType != MeshType.ProxyMesh)
				{
					return meshType == MeshType.ProxyBoneMesh;
				}
				return true;
			}
		}

		public bool IsMapping => meshType == MeshType.Mapping;

		public int BaseLineCount
		{
			get
			{
				if (!baseLineStartDataIndices.IsCreated)
				{
					return 0;
				}
				return baseLineStartDataIndices.Length;
			}
		}

		public int EdgeCount
		{
			get
			{
				if (!edges.IsCreated)
				{
					return 0;
				}
				return edges.Length;
			}
		}

		public int CustomSkinningBoneCount
		{
			get
			{
				int[] array = customSkinningBoneIndices;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		public int CenterFixedPointCount
		{
			get
			{
				ushort[] array = centerFixedList;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		public int NormalAdjustmentRotationCount
		{
			get
			{
				if (!normalAdjustmentRotations.IsCreated)
				{
					return 0;
				}
				return normalAdjustmentRotations.Length;
			}
		}

		public void ImportFrom(RenderSetupData rsetup)
		{
			try
			{
				if (rsetup == null)
				{
					result.SetError(Define.Result.VirtualMesh_InvalidSetup);
					throw new MagicaClothProcessingException();
				}
				if (rsetup.IsFaild())
				{
					result.SetError(Define.Result.VirtualMesh_InvalidSetup);
					throw new MagicaClothProcessingException();
				}
				if (transformData == null)
				{
					transformData = new TransformData(rsetup.TransformCount);
				}
				int[] array = transformData.AddTransformRange(rsetup.transformList, rsetup.transformIdList, rsetup.transformParentIdList, rsetup.rootTransformIdList, rsetup.transformLocalPositions, rsetup.transformLocalRotations, rsetup.transformPositions, rsetup.transformRotations, rsetup.transformScales, rsetup.transformInverseRotations);
				centerTransformIndex = array[rsetup.renderTransformIndex];
				initLocalToWorld = rsetup.initRenderLocalToWorld;
				initWorldToLocal = rsetup.initRenderWorldtoLocal;
				initRotation = rsetup.initRenderRotation;
				initInverseRotation = math.inverse(initRotation);
				initScale = rsetup.initRenderScale;
				if (rsetup.setupType == RenderSetupData.SetupType.MeshCloth)
				{
					meshType = MeshType.NormalMesh;
					isBoneCloth = false;
					ImportMeshType(rsetup, array);
					if (rsetup.hasBoneWeight)
					{
						ImportMeshSkinning();
					}
				}
				else
				{
					if (rsetup.setupType != RenderSetupData.SetupType.BoneCloth && rsetup.setupType != RenderSetupData.SetupType.BoneSpring)
					{
						result.SetError(Define.Result.RenderSetup_InvalidType);
						throw new IndexOutOfRangeException();
					}
					meshType = MeshType.NormalBoneMesh;
					isBoneCloth = true;
					ImportBoneType(rsetup, array);
				}
				boundingBox = new NativeReference<AABB>(Allocator.Persistent);
				JobUtility.CalcAABBRun(localPositions.GetNativeArray(), VertexCount, boundingBox);
				RenderSetupData.SetupType setupType = rsetup.setupType;
				if ((uint)(setupType - 1) <= 1u && TriangleCount > 0)
				{
					JobUtility.CalcUVWithSphereMappingRun(localPositions.GetNativeArray(), VertexCount, boundingBox, uv.GetNativeArray());
				}
				CalcAverageAndMaxVertexDistanceRun();
			}
			catch (Exception)
			{
				if (result.IsNone())
				{
					result.SetError(Define.Result.VirtualMesh_ImportError);
				}
				throw;
			}
		}

		private void ImportMeshType(RenderSetupData rsetup, int[] transformIndices)
		{
			skinRootIndex = transformIndices[rsetup.skinRootBoneIndex];
			skinBoneTransformIndices.AddRange(transformIndices, rsetup.skinBoneCount);
			skinBoneBindPoses.AddRange(rsetup.bindPoseList.ToArray());
			Mesh.MeshData meshData = rsetup.meshDataArray[0];
			int vertexCount = meshData.vertexCount;
			localPositions.AddRange(vertexCount);
			localNormals.AddRange(vertexCount);
			localTangents.AddRange(vertexCount);
			uv.AddRange(vertexCount);
			boneWeights.AddRange(vertexCount);
			meshData.GetVertices(localPositions.GetNativeArray<Vector3>());
			meshData.GetNormals(localNormals.GetNativeArray<Vector3>());
			if (meshData.HasVertexAttribute(UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				using NativeArray<Vector4> nativeArray = new NativeArray<Vector4>(vertexCount, Allocator.TempJob);
				meshData.GetTangents(nativeArray);
				localTangents.CopyFromWithTypeChangeStride(nativeArray);
			}
			else
			{
				IJobParallelForExtensions.Run(new Import_GenerateTangentJob
				{
					localNormals = localNormals.GetNativeArray(),
					localTangents = localTangents.GetNativeArray()
				}, vertexCount);
			}
			if (meshData.HasVertexAttribute(UnityEngine.Rendering.VertexAttribute.TexCoord0))
			{
				meshData.GetUVs(0, uv.GetNativeArray<Vector2>());
			}
			else
			{
				Debug.LogWarning("[" + name + "] UV not found!");
			}
			attributes.AddRange(vertexCount);
			referenceIndices.AddRange(vertexCount);
			using NativeArray<int> startBoneWeightIndices = new NativeArray<int>(vertexCount, Allocator.TempJob);
			JobUtility.SerialNumberRun(referenceIndices.GetNativeArray(), vertexCount);
			if (rsetup.hasBoneWeight)
			{
				new Import_BoneWeightJob1
				{
					vcnt = vertexCount,
					bonesPerVertexArray = rsetup.bonesPerVertexArray,
					startBoneWeightIndices = startBoneWeightIndices
				}.Run();
				IJobParallelForExtensions.Run(new Import_BoneWeightJob2
				{
					startBoneWeightIndices = startBoneWeightIndices,
					boneWeightArray = rsetup.boneWeightArray,
					bonesPerVertexArray = rsetup.bonesPerVertexArray,
					boneWeights = boneWeights.GetNativeArray()
				}, vertexCount);
			}
			else
			{
				JobUtility.FillRun(boneWeights.GetNativeArray(), vertexCount, new VirtualMeshBoneWeight(0, new float4(1f, 0f, 0f, 0f)));
			}
			for (int i = 0; i < meshData.subMeshCount; i++)
			{
				using NativeArray<int> nativeArray2 = new NativeArray<int>(meshData.GetSubMesh(i).indexCount, Allocator.Persistent);
				meshData.GetIndices(nativeArray2, i);
				triangles.AddRangeTypeChange(nativeArray2);
			}
		}

		private void ImportMeshSkinning()
		{
			IJobParallelForExtensions.Run(new Import_CalcSkinningJob
			{
				localPositions = localPositions.GetNativeArray(),
				localNormals = localNormals.GetNativeArray(),
				localTangents = localTangents.GetNativeArray(),
				boneWeights = boneWeights.GetNativeArray(),
				skinBoneTransformIndices = skinBoneTransformIndices.GetNativeArray(),
				bindPoses = skinBoneBindPoses.GetNativeArray(),
				transformPositionArray = transformData.positionArray.GetNativeArray(),
				transformRotationArray = transformData.rotationArray.GetNativeArray(),
				transformScaleArray = transformData.scaleArray.GetNativeArray(),
				toM = initWorldToLocal
			}, VertexCount);
		}

		private void ImportBoneType(RenderSetupData rsetup, int[] transformIndices)
		{
			int num = rsetup.TransformCount - 1;
			localPositions.AddRange(num);
			localNormals.AddRange(num);
			localTangents.AddRange(num);
			uv.AddRange(num);
			boneWeights.AddRange(num);
			attributes.AddRange(num);
			referenceIndices.AddRange(num);
			skinBoneTransformIndices.AddRange(transformIndices, rsetup.skinBoneCount);
			skinBoneBindPoses.AddRange(num);
			float4x4 initRenderWorldtoLocal = rsetup.initRenderWorldtoLocal;
			float4x4 initRenderLocalToWorld = rsetup.initRenderLocalToWorld;
			IJobParallelForExtensions.Run(new Import_BoneVertexJob
			{
				WtoL = initRenderWorldtoLocal,
				LtoW = initRenderLocalToWorld,
				transformPositions = rsetup.transformPositions,
				transformRotations = rsetup.transformRotations,
				transformScales = rsetup.transformScales,
				localPositions = localPositions.GetNativeArray(),
				localNormals = localNormals.GetNativeArray(),
				localTangents = localTangents.GetNativeArray(),
				boneWeights = boneWeights.GetNativeArray(),
				skinBoneBindPoses = skinBoneBindPoses.GetNativeArray()
			}, num);
			JobUtility.SerialNumberRun(referenceIndices.GetNativeArray(), num);
			if (rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.Line)
			{
				List<int2> list = new List<int2>(num);
				for (int i = 0; i < num; i++)
				{
					int parentTransformIndex = rsetup.GetParentTransformIndex(i, centerExcluded: true);
					if (parentTransformIndex >= 0)
					{
						int2 item = DataUtility.PackInt2(parentTransformIndex, i);
						list.Add(item);
					}
				}
				if (rsetup.setupType == RenderSetupData.SetupType.BoneSpring)
				{
					attributes.Fill(0, num, VertexAttribute.DisableCollision);
					if (rsetup.collisionBoneIndexList != null)
					{
						foreach (int collisionBoneIndex in rsetup.collisionBoneIndexList)
						{
							if (collisionBoneIndex >= 0)
							{
								attributes[collisionBoneIndex] = VertexAttribute.Invalid;
							}
						}
					}
				}
				if (list.Count > 0)
				{
					lines = new ExSimpleNativeArray<int2>(list.ToArray());
				}
				return;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>(num);
			for (int j = 0; j < num; j++)
			{
				if (!dictionary.ContainsKey(rsetup.transformIdList[j]))
				{
					dictionary.Add(rsetup.transformIdList[j], j);
				}
			}
			bool flag = rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialLoopMesh;
			bool flag2 = rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialLoopMesh || rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.SequentialNonLoopMesh;
			List<int> list2 = new List<int>(rsetup.rootTransformIdList);
			int count = list2.Count;
			int b = count - 1;
			if (rsetup.boneConnectionMode == RenderSetupData.BoneConnectionMode.AutomaticMesh)
			{
				List<int> list3 = new List<int>(list2);
				list2.Clear();
				list2.Add(list3[0]);
				float num2 = 0f;
				while (list3.Count > 0)
				{
					int num3 = list2[list2.Count - 1];
					list3.Remove(num3);
					int index = dictionary[num3];
					float3 x = localPositions[index];
					float num4 = float.MaxValue;
					int num5 = 0;
					for (int k = 0; k < list3.Count; k++)
					{
						int num6 = list3[k];
						int index2 = dictionary[num6];
						float3 y = localPositions[index2];
						float num7 = math.distance(x, y);
						if (num7 < num4)
						{
							num4 = num7;
							num5 = num6;
						}
					}
					if (num5 != 0)
					{
						if (num2 == 0f || num4 < num2 * 1.5f)
						{
							list2.Add(num5);
							num2 = ((num2 == 0f) ? num4 : ((num2 + num4) * 0.5f));
						}
						else
						{
							list2.Reverse();
							num2 = 0f;
						}
					}
				}
				if (list2.Count >= 3)
				{
					int key = list2[0];
					int key2 = list2[list2.Count - 1];
					int index3 = dictionary[key];
					int index4 = dictionary[key2];
					float3 x2 = localPositions[index3];
					float3 y2 = localPositions[index4];
					if (math.distance(x2, y2) < num2 * 1.5f)
					{
						flag = true;
					}
				}
			}
			FixedList128Bytes<int>[] array = new FixedList128Bytes<int>[num];
			int[] array2 = new int[num];
			int[] array3 = new int[num];
			List<FixedList512Bytes<int>> list4 = new List<FixedList512Bytes<int>>();
			HashSet<uint> hashSet = new HashSet<uint>();
			Stack<int> stack = new Stack<int>(num);
			Stack<int> stack2 = new Stack<int>(num);
			for (int l = 0; l < count; l++)
			{
				stack.Clear();
				stack.Push(list2[l]);
				stack2.Clear();
				stack2.Push(0);
				while (stack.Count > 0)
				{
					int key3 = stack.Pop();
					int num8 = stack2.Pop();
					int item2 = dictionary[key3];
					_ = localPositions[item2];
					if (list4.Count <= num8)
					{
						list4.Add(default(FixedList512Bytes<int>));
					}
					FixedList512Bytes<int> value = list4[num8];
					value.Add(in item2);
					list4[num8] = value;
					FixedList128Bytes<int> fixedList128Bytes = default(FixedList128Bytes<int>);
					int key4 = rsetup.transformParentIdList[item2];
					if (dictionary.ContainsKey(key4))
					{
						int item3 = dictionary[key4];
						fixedList128Bytes.Add(in item3);
						uint item4 = DataUtility.Pack32Sort(item2, item3);
						hashSet.Add(item4);
					}
					FixedList512Bytes<int> fixedList512Bytes = rsetup.transformChildIdList[item2];
					if (fixedList512Bytes.Length > 0)
					{
						for (int m = 0; m < fixedList512Bytes.Length; m++)
						{
							int num9 = fixedList512Bytes[m];
							stack.Push(num9);
							stack2.Push(num8 + 1);
							int item5 = dictionary[num9];
							fixedList128Bytes.Add(in item5);
							uint item6 = DataUtility.Pack32Sort(item2, item5);
							hashSet.Add(item6);
						}
					}
					array[item2] = fixedList128Bytes;
					array2[item2] = num8;
					array3[item2] = l;
				}
			}
			uint num10 = DataUtility.Pack32Sort(0, b);
			for (int n = 0; n < num; n++)
			{
				int index5 = array2[n];
				FixedList512Bytes<int> fixedList512Bytes2 = list4[index5];
				float3 x3 = localPositions[n];
				FixedList128Bytes<int> fixedList128Bytes2 = array[n];
				int num11 = array3[n];
				float num12 = float.MaxValue;
				int item7 = -1;
				foreach (int item10 in fixedList512Bytes2)
				{
					if (item10 == n)
					{
						continue;
					}
					int num13 = array3[item10];
					bool flag3 = num10 == DataUtility.Pack32Sort(num11, num13) && num10 != 0;
					if (!(!flag && flag3) && (!flag2 || (flag && flag3) || math.abs(num11 - num13) <= 1))
					{
						float3 y3 = localPositions[item10];
						float num14 = math.distance(x3, y3);
						if (num14 < num12)
						{
							num12 = num14;
							item7 = item10;
						}
					}
				}
				if (item7 >= 0)
				{
					fixedList128Bytes2.Add(in item7);
					num12 = (flag2 ? float.MaxValue : (num12 * 1.5f));
					foreach (int item11 in fixedList512Bytes2)
					{
						int item8 = item11;
						if (item8 == n || item8 == item7)
						{
							continue;
						}
						int num15 = array3[item8];
						bool flag4 = num10 == DataUtility.Pack32Sort(num11, num15) && num10 != 0;
						if (!(!flag && flag4) && (!flag2 || (flag && flag4) || math.abs(num11 - num15) <= 1))
						{
							float3 y4 = localPositions[item8];
							if (math.distance(x3, y4) <= num12)
							{
								fixedList128Bytes2.Add(in item8);
							}
						}
					}
				}
				array[n] = fixedList128Bytes2;
			}
			HashSet<int2> hashSet2 = new HashSet<int2>();
			HashSet<int2> hashSet3 = new HashSet<int2>();
			HashSet<int3> hashSet4 = new HashSet<int3>();
			for (int num16 = 0; num16 < num; num16++)
			{
				FixedList128Bytes<int> fixedList128Bytes3 = array[num16];
				if (fixedList128Bytes3.Length == 0)
				{
					Debug.LogError($"Connection 0! [{num16}]");
					continue;
				}
				if (fixedList128Bytes3.Length == 1)
				{
					hashSet2.Add(DataUtility.PackInt2(num16, fixedList128Bytes3[0]));
					continue;
				}
				for (int num17 = 0; num17 < fixedList128Bytes3.Length; num17++)
				{
					int d = fixedList128Bytes3[num17];
					hashSet2.Add(DataUtility.PackInt2(num16, d));
				}
				int num18 = array3[num16];
				float3 float5 = localPositions[num16];
				for (int num19 = 0; num19 < fixedList128Bytes3.Length - 1; num19++)
				{
					int num20 = fixedList128Bytes3[num19];
					float3 v = localPositions[num20] - float5;
					for (int num21 = num19 + 1; num21 < fixedList128Bytes3.Length; num21++)
					{
						int num22 = fixedList128Bytes3[num21];
						float3 v2 = localPositions[num22] - float5;
						if (math.lengthsq(v) < 1E-06f || math.lengthsq(v2) < 1E-06f || math.degrees(MathUtility.Angle(in v, in v2)) >= 120f)
						{
							continue;
						}
						int num23 = array3[num20];
						int num24 = array3[num22];
						if ((num23 == num18 || num24 == num18 || num23 == num24) && 0 + (hashSet.Contains(DataUtility.Pack32Sort(num16, num20)) ? 1 : 0) + (hashSet.Contains(DataUtility.Pack32Sort(num16, num22)) ? 1 : 0) + (hashSet.Contains(DataUtility.Pack32Sort(num20, num22)) ? 1 : 0) != 0)
						{
							int3 item9 = DataUtility.PackInt3(num16, num20, num22);
							if (!hashSet4.Contains(item9))
							{
								hashSet4.Add(item9);
								hashSet3.Add(DataUtility.PackInt2(num16, num20));
								hashSet3.Add(DataUtility.PackInt2(num16, num22));
							}
						}
					}
				}
			}
			if (hashSet4.Count > 0)
			{
				triangles = new ExSimpleNativeArray<int3>(hashSet4.Count);
				int num25 = 0;
				foreach (int3 item12 in hashSet4)
				{
					triangles[num25] = item12;
					num25++;
				}
			}
			foreach (int2 item13 in hashSet3)
			{
				hashSet2.Remove(item13);
			}
			if (hashSet2.Count <= 0)
			{
				return;
			}
			lines = new ExSimpleNativeArray<int2>(hashSet2.Count);
			int num26 = 0;
			foreach (int2 item14 in hashSet2)
			{
				lines[num26] = item14;
				num26++;
			}
		}

		public void ImportFrom(RenderData renderData)
		{
			try
			{
				if (renderData == null)
				{
					result.SetError(Define.Result.VirtualMesh_InvalidRenderData);
					throw new MagicaClothProcessingException();
				}
				ImportFrom(renderData.setupData);
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.VirtualMesh_ImportError);
				}
				throw;
			}
			catch (Exception)
			{
				result.SetError(Define.Result.VirtualMesh_ImportError);
				throw;
			}
		}

		public void SelectionMesh(SelectionData selectionData, float4x4 selectionLocalToWorldMatrix, float mergin)
		{
			try
			{
				if (selectionData == null || !selectionData.IsValid())
				{
					result.SetError(Define.Result.VirtualMesh_InvalidSelection);
					throw new MagicaClothProcessingException();
				}
				int count = selectionData.Count;
				using NativeArray<float3> positions = selectionData.GetPositionNativeArray();
				using NativeArray<VertexAttribute> selectionAttributes = selectionData.GetAttributeNativeArray();
				if (!MathUtility.CompareMatrix(in selectionLocalToWorldMatrix, in initLocalToWorld))
				{
					float4x4 toM = MathUtility.Transform(in selectionLocalToWorldMatrix, in initWorldToLocal);
					JobUtility.TransformPositionRun(positions, count, in toM);
				}
				float gridSize = mergin * 1f;
				using GridMap<int> gridMap = SelectionData.CreateGridMapRun(gridSize, in positions, in selectionAttributes, move: true, fix: true, ignore: true, invalid: false);
				using NativeList<int3> nativeList = new NativeList<int3>(TriangleCount, Allocator.Persistent);
				using NativeArray<int> newVertexRemapIndices = new NativeArray<int>(VertexCount, Allocator.Persistent);
				using NativeReference<int> newVertexCount = new NativeReference<int>(Allocator.Persistent);
				new Select_GridJob
				{
					gridSize = gridSize,
					gridMap = gridMap.GetMultiHashMap(),
					selectionCount = count,
					selectionPositions = positions,
					selectionAttributes = selectionAttributes,
					vertexCount = VertexCount,
					triangleCount = TriangleCount,
					searchRadius = mergin,
					meshPositions = localPositions.GetNativeArray(),
					meshTriangles = triangles.GetNativeArray(),
					newTriangles = nativeList,
					newVertexRemapIndices = newVertexRemapIndices,
					newVertexCount = newVertexCount
				}.Run();
				if (newVertexCount.Value < VertexCount)
				{
					int value = newVertexCount.Value;
					ExSimpleNativeArray<int> exSimpleNativeArray = new ExSimpleNativeArray<int>(value);
					ExSimpleNativeArray<VertexAttribute> exSimpleNativeArray2 = new ExSimpleNativeArray<VertexAttribute>(value);
					ExSimpleNativeArray<float3> exSimpleNativeArray3 = new ExSimpleNativeArray<float3>(value);
					ExSimpleNativeArray<float3> exSimpleNativeArray4 = new ExSimpleNativeArray<float3>(value);
					ExSimpleNativeArray<float3> exSimpleNativeArray5 = new ExSimpleNativeArray<float3>(value);
					ExSimpleNativeArray<float2> exSimpleNativeArray6 = new ExSimpleNativeArray<float2>(value);
					ExSimpleNativeArray<VirtualMeshBoneWeight> exSimpleNativeArray7 = new ExSimpleNativeArray<VirtualMeshBoneWeight>(value);
					new Select_PackVertexJob
					{
						vertexCount = VertexCount,
						newVertexRemapIndices = newVertexRemapIndices,
						attributes = attributes.GetNativeArray(),
						localPositions = localPositions.GetNativeArray(),
						localNormals = localNormals.GetNativeArray(),
						localTangents = localTangents.GetNativeArray(),
						uv = uv.GetNativeArray(),
						boneWeights = boneWeights.GetNativeArray(),
						newReferenceIndices = exSimpleNativeArray.GetNativeArray(),
						newAttributes = exSimpleNativeArray2.GetNativeArray(),
						newLocalPositions = exSimpleNativeArray3.GetNativeArray(),
						newLocalNormals = exSimpleNativeArray4.GetNativeArray(),
						newLocalTangents = exSimpleNativeArray5.GetNativeArray(),
						newUv = exSimpleNativeArray6.GetNativeArray(),
						newBoneWeights = exSimpleNativeArray7.GetNativeArray()
					}.Run();
					referenceIndices.Dispose();
					attributes.Dispose();
					localPositions.Dispose();
					localNormals.Dispose();
					localTangents.Dispose();
					uv.Dispose();
					boneWeights.Dispose();
					referenceIndices = exSimpleNativeArray;
					attributes = exSimpleNativeArray2;
					localPositions = exSimpleNativeArray3;
					localNormals = exSimpleNativeArray4;
					localTangents = exSimpleNativeArray5;
					uv = exSimpleNativeArray6;
					boneWeights = exSimpleNativeArray7;
					ExSimpleNativeArray<int3> exSimpleNativeArray8 = new ExSimpleNativeArray<int3>(nativeList);
					triangles.Dispose();
					triangles = exSimpleNativeArray8;
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.VirtualMesh_SelectionUnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.VirtualMesh_SelectionException);
			}
		}

		public float CalcSelectionMergin(ReductionSettings settings)
		{
			float value = averageVertexDistance.Value;
			float y = 0f;
			if (settings != null && settings.IsEnabled)
			{
				y = boundingBox.Value.MaxSideLength * settings.GetMaxConnectionDistance();
			}
			return math.max(value, y) * 1.5f;
		}

		public void AddMesh(VirtualMesh cmesh)
		{
			try
			{
				if (IsError)
				{
					throw new InvalidOperationException();
				}
				if (cmesh == null || !cmesh.IsSuccess || cmesh.IsError)
				{
					throw new InvalidOperationException();
				}
				int skinBoneCount = SkinBoneCount;
				int vertexCount = VertexCount;
				int triangleCount = TriangleCount;
				int lineCount = LineCount;
				_ = transformData.Count;
				float4x4 float4x5 = cmesh.CenterTransformTo(this);
				int skinBoneCount2 = cmesh.SkinBoneCount;
				skinBoneTransformIndices.AddRange(skinBoneCount2);
				for (int i = 0; i < skinBoneCount2; i++)
				{
					int srcIndex = cmesh.skinBoneTransformIndices[i];
					int value = transformData.AddTransform(cmesh.transformData, srcIndex);
					skinBoneTransformIndices[skinBoneCount + i] = value;
				}
				int vertexCount2 = cmesh.VertexCount;
				cmesh.mergeChunk = new DataChunk(VertexCount, vertexCount2);
				attributes.AddRange(vertexCount2);
				localPositions.AddRange(vertexCount2);
				localNormals.AddRange(vertexCount2);
				localTangents.AddRange(vertexCount2);
				uv.AddRange(vertexCount2);
				boneWeights.AddRange(vertexCount2);
				IJobParallelForExtensions.Run(new Add_CopyVerticesJob
				{
					vertexOffset = vertexCount,
					skinBoneOffset = skinBoneCount,
					toM = float4x5,
					srcAttributes = cmesh.attributes.GetNativeArray(),
					srclocalPositions = cmesh.localPositions.GetNativeArray(),
					srclocalNormals = cmesh.localNormals.GetNativeArray(),
					srclocalTangents = cmesh.localTangents.GetNativeArray(),
					srcUV = cmesh.uv.GetNativeArray(),
					srcBoneWeights = cmesh.boneWeights.GetNativeArray(),
					dstAttributes = attributes.GetNativeArray(),
					dstlocalPositions = localPositions.GetNativeArray(),
					dstlocalNormals = localNormals.GetNativeArray(),
					dstlocalTangents = localTangents.GetNativeArray(),
					dstUV = uv.GetNativeArray(),
					dstBoneWeights = boneWeights.GetNativeArray(),
					dstSkinBoneIndices = skinBoneTransformIndices.GetNativeArray()
				}, cmesh.VertexCount);
				skinBoneBindPoses.AddRange(skinBoneCount2);
				IJobParallelForExtensions.Run(new Add_CalcBindPoseJob
				{
					skinBoneOffset = skinBoneCount,
					srcSkinBoneTransformIndices = cmesh.skinBoneTransformIndices.GetNativeArray(),
					srcTransformPositionArray = cmesh.transformData.positionArray.GetNativeArray(),
					srcTransformRotationArray = cmesh.transformData.rotationArray.GetNativeArray(),
					srcTransformScaleArray = cmesh.transformData.scaleArray.GetNativeArray(),
					dstCenterLocalToWorldMatrix = initLocalToWorld,
					dstSkinBoneBindPoses = skinBoneBindPoses.GetNativeArray()
				}, skinBoneCount2);
				if (cmesh.TriangleCount > 0)
				{
					triangles.AddRange(cmesh.TriangleCount);
					IJobParallelForExtensions.Run(new JobUtility.AddInt3DataCopyJob
					{
						dstOffset = triangleCount,
						addData = vertexCount,
						srcData = cmesh.triangles.GetNativeArray(),
						dstData = triangles.GetNativeArray()
					}, cmesh.TriangleCount);
				}
				if (cmesh.LineCount > 0)
				{
					lines.AddRange(cmesh.LineCount);
					IJobParallelForExtensions.Run(new JobUtility.AddInt2DataCopyJob
					{
						dstOffset = lineCount,
						addData = vertexCount,
						srcData = cmesh.lines.GetNativeArray(),
						dstData = lines.GetNativeArray()
					}, cmesh.LineCount);
				}
				float3 min = cmesh.boundingBox.Value.Min;
				float3 max = cmesh.boundingBox.Value.Max;
				AABB aabb = new AABB(math.transform(float4x5, min), math.transform(float4x5, max));
				if (boundingBox.IsCreated)
				{
					AABB value2 = boundingBox.Value;
					value2.Encapsulate(in aabb);
					boundingBox.Value = value2;
				}
				else
				{
					boundingBox = new NativeReference<AABB>(aabb, Allocator.Persistent);
				}
				float num = math.length(cmesh.initScale) / math.length(initScale);
				averageVertexDistance.Value = math.max(averageVertexDistance.Value, cmesh.averageVertexDistance.Value * num);
				maxVertexDistance.Value = math.max(maxVertexDistance.Value, cmesh.maxVertexDistance.Value * num);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError();
				throw;
			}
		}

		public void SetTransform(Transform center, Transform skinRoot = null, int centerId = 0, int skinRootId = 0)
		{
			SetCenterTransform(center, centerId);
			if (skinRoot != null)
			{
				SetSkinRoot(skinRoot, skinRootId);
			}
			else
			{
				SetSkinRoot(center, centerId);
			}
			initLocalToWorld = center.localToWorldMatrix;
			initWorldToLocal = math.inverse(initLocalToWorld);
			initRotation = center.rotation;
			initInverseRotation = math.inverse(initRotation);
			initScale = center.lossyScale;
		}

		public void SetTransform(TransformRecord centerRecord, TransformRecord skinRootRecord = null)
		{
			centerTransformIndex = transformData.AddTransform(centerRecord, 0, 1);
			if (skinRootRecord != null)
			{
				skinRootIndex = transformData.AddTransform(skinRootRecord, 0, 1);
			}
			else
			{
				skinRootIndex = centerTransformIndex;
			}
			initLocalToWorld = centerRecord.localToWorldMatrix;
			initWorldToLocal = centerRecord.worldToLocalMatrix;
			initRotation = centerRecord.rotation;
			initInverseRotation = math.inverse(initRotation);
			initScale = centerRecord.scale;
		}

		public void SetCenterTransform(Transform t, int tid = 0)
		{
			if ((bool)t)
			{
				if (centerTransformIndex >= 0)
				{
					transformData.ReplaceTransform(centerTransformIndex, t, tid, 0, 1);
				}
				else
				{
					centerTransformIndex = transformData.AddTransform(t, tid, 0, 1);
				}
			}
		}

		public void SetSkinRoot(Transform t, int tid = 0)
		{
			if ((bool)t)
			{
				if (skinRootIndex >= 0)
				{
					transformData.ReplaceTransform(skinRootIndex, t, tid, 0, 1);
				}
				else
				{
					skinRootIndex = transformData.AddTransform(t, tid, 0, 1);
				}
			}
		}

		public Transform GetCenterTransform()
		{
			return transformData.GetTransformFromIndex(centerTransformIndex);
		}

		public void SetCustomSkinningBones(TransformRecord clothTransformRecord, List<TransformRecord> bones)
		{
			if (bones == null || bones.Count == 0)
			{
				return;
			}
			customSkinningBoneIndices = new int[bones.Count];
			for (int i = 0; i < bones.Count; i++)
			{
				TransformRecord transformRecord = bones[i];
				int num = -1;
				if (transformRecord.IsValid())
				{
					transformRecord.localPosition = clothTransformRecord.worldToLocalMatrix.MultiplyPoint(transformRecord.position);
					num = skinBoneTransformIndices.Count;
					int data = transformData.AddTransform(transformRecord, 0, 1, checkDuplicate: false);
					skinBoneTransformIndices.Add(data);
					float4x4 data2 = math.mul(transformRecord.worldToLocalMatrix, initLocalToWorld);
					skinBoneBindPoses.Add(data2);
				}
				customSkinningBoneIndices[i] = num;
			}
		}

		public bool CompareSpace(VirtualMesh target)
		{
			return MathUtility.CompareMatrix(in initLocalToWorld, in target.initLocalToWorld);
		}

		public float4x4 CenterTransformTo(VirtualMesh to)
		{
			if (!CompareSpace(to))
			{
				return MathUtility.Transform(in initLocalToWorld, in to.initWorldToLocal);
			}
			return float4x4.identity;
		}

		public void Mapping(VirtualMesh proxyMesh)
		{
			try
			{
				if (IsError)
				{
					throw new MagicaClothProcessingException();
				}
				if (proxyMesh == null || !proxyMesh.IsSuccess)
				{
					result.SetError(Define.Result.MappingMesh_ProxyError);
					throw new MagicaClothProcessingException();
				}
				result.SetProcess();
				float4x4 matrix = CenterTransformTo(proxyMesh);
				using NativeArray<MappingWorkData> mappingWorkData = new NativeArray<MappingWorkData>(VertexCount, Allocator.Persistent);
				if (mergeChunk.IsValid)
				{
					new Mapping_DirectConnectionVertexDataJob
					{
						toP = matrix,
						vcnt = VertexCount,
						mergeChunk = mergeChunk,
						localPositions = localPositions.GetNativeArray(),
						attributes = attributes.GetNativeArray(),
						joinIndices = proxyMesh.joinIndices,
						proxyAttributes = proxyMesh.attributes.GetNativeArray(),
						proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
						mappingWorkData = mappingWorkData
					}.Run();
					float weightLength = proxyMesh.averageVertexDistance.Value * 1.5f;
					using NativeParallelHashSet<ushort> useSet = new NativeParallelHashSet<ushort>(1024, Allocator.Persistent);
					new Mapping_CalcDirectWeightJob
					{
						vcnt = mappingWorkData.Length,
						weightLength = weightLength,
						mappingWorkData = mappingWorkData,
						attributes = attributes.GetNativeArray(),
						boneWeights = boneWeights.GetNativeArray(),
						proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
						proxyVertexToVertexIndexArray = proxyMesh.vertexToVertexIndexArray,
						proxyVertexToVertexDataArray = proxyMesh.vertexToVertexDataArray,
						useSet = useSet
					}.Run();
				}
				else
				{
					float num = math.max(MathUtility.TransformLength(averageVertexDistance.Value, in matrix), 1E-05f);
					float searchRadius = num * 2.5f;
					float gridSize = num * 1.5f;
					using GridMap<int> gridMap = proxyMesh.CreateVertexIndexGridMapRun(gridSize);
					new Mapping_CalcConnectionVertexDataJob
					{
						gridSize = gridSize,
						searchRadius = searchRadius,
						toP = matrix,
						vcnt = VertexCount,
						localPositions = localPositions.GetNativeArray(),
						boneWeights = boneWeights.GetNativeArray(),
						transformIds = transformData.idArray.GetNativeArray(),
						attributes = attributes.GetNativeArray(),
						gridMap = gridMap.GetMultiHashMap(),
						proxyAttributes = proxyMesh.attributes.GetNativeArray(),
						proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
						proxyBoneWeights = proxyMesh.boneWeights.GetNativeArray(),
						proxyTransformIds = proxyMesh.transformData.idArray.GetNativeArray(),
						mappingWorkData = mappingWorkData
					}.Run();
					if (mappingWorkData.Length > 0)
					{
						IJobParallelForExtensions.Run(new Mapping_CalcWeightJob
						{
							mappingWorkData = mappingWorkData,
							attributes = attributes.GetNativeArray(),
							boneWeights = boneWeights.GetNativeArray(),
							proxyAttributes = proxyMesh.attributes.GetNativeArray(),
							proxyLocalPositions = proxyMesh.localPositions.GetNativeArray(),
							proxyLocalNormals = proxyMesh.localNormals.GetNativeArray(),
							proxyVertexToVertexIndexArray = proxyMesh.vertexToVertexIndexArray,
							proxyVertexToVertexDataArray = proxyMesh.vertexToVertexDataArray
						}, mappingWorkData.Length);
					}
				}
				mappingProxyMesh = proxyMesh;
				toProxyMatrix = matrix;
				toProxyRotation = math.mul(proxyMesh.initInverseRotation, initRotation);
				meshType = MeshType.Mapping;
				result.SetSuccess();
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.MappingMesh_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.MappingMesh_Exception);
			}
		}

		private static float4 CalcVertexWeights(float4 distances)
		{
			distances = math.max(distances, 0);
			distances = math.pow(distances, 4f);
			float num = distances[0];
			for (int i = 0; i < 4; i++)
			{
				distances[i] = ((distances[i] > 0f) ? (num / distances[i]) : 0f);
			}
			float num2 = math.csum(distances);
			if (num2 <= 0f)
			{
				return new float4(1f, 0f, 0f, 0f);
			}
			distances /= num2;
			for (int num3 = 3; num3 >= 1; num3--)
			{
				if (distances[num3] < 0.01f)
				{
					distances[num3] = 0f;
				}
			}
			distances /= math.csum(distances);
			return distances;
		}

		public void Optimization()
		{
			try
			{
				RemoveDuplicateTriangles();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.Optimize_Exception);
			}
		}

		private void RemoveDuplicateTriangles()
		{
			if (TriangleCount < 2)
			{
				return;
			}
			using NativeParallelHashMap<int2, FixedList128Bytes<int>> edgeToTriangleList = new NativeParallelHashMap<int2, FixedList128Bytes<int>>(TriangleCount * 2, Allocator.Persistent);
			using NativeList<int3> nativeList = new NativeList<int3>(TriangleCount, Allocator.Persistent);
			using NativeParallelHashSet<int4> useQuadSet = new NativeParallelHashSet<int4>(TriangleCount / 4, Allocator.Persistent);
			using NativeParallelHashSet<int3> removeTriangleSet = new NativeParallelHashSet<int3>(TriangleCount / 4, Allocator.Persistent);
			new Optimize_EdgeToTrianlgeJob
			{
				tcnt = TriangleCount,
				triangles = triangles.GetNativeArray(),
				localPositions = localPositions.GetNativeArray(),
				edgeToTriangleList = edgeToTriangleList,
				newTriangles = nativeList,
				useQuadSet = useQuadSet,
				removeTriangleSet = removeTriangleSet
			}.Run();
			triangles?.Dispose();
			triangles = new ExSimpleNativeArray<int3>();
			if (nativeList.Length > 0)
			{
				triangles.AddRange(nativeList);
			}
		}

		private bool CheckTwoTriangleOpen(in int3 tri1, in int3 tri2, in int2 edge, in float3 tri1n)
		{
			int index = DataUtility.RemainingData(in tri2, in edge);
			float3 y = math.normalize(localPositions[index] - localPositions[edge.x]);
			return math.dot(tri1n, y) <= 0f;
		}

		private float CalcTwoTriangleAngle(in int3 tri1, in int3 tri2, in int2 edge)
		{
			int index = DataUtility.RemainingData(in tri1, in edge);
			int index2 = DataUtility.RemainingData(in tri2, in edge);
			float3 float5 = localPositions[edge.y] - localPositions[edge.x];
			float3 y = localPositions[index] - localPositions[edge.x];
			return math.degrees(MathUtility.Angle(v2: math.cross(localPositions[index2] - localPositions[edge.x], float5), v1: math.cross(float5, y)));
		}

		public void ConvertProxyMesh(ClothSerializeData sdata, TransformRecord clothTransformRecord, List<TransformRecord> customSkinningBoneRecords, TransformRecord normalAdjustmentTransformRecord)
		{
			try
			{
				if (IsError)
				{
					throw new MagicaClothProcessingException();
				}
				if (sdata.customSkinningSetting.enable && !sdata.IsBoneSpring())
				{
					SetCustomSkinningBones(clothTransformRecord, customSkinningBoneRecords);
				}
				vertexToTriangles = new NativeArray<FixedList32Bytes<uint>>(VertexCount, Allocator.Persistent);
				vertexBindPosePositions = new NativeArray<float3>(VertexCount, Allocator.Persistent);
				vertexBindPoseRotations = new NativeArray<quaternion>(VertexCount, Allocator.Persistent);
				vertexToTransformRotations = new NativeArray<quaternion>(VertexCount, Allocator.Persistent);
				JobUtility.FillRun(vertexToTransformRotations, VertexCount, quaternion.identity);
				using MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(VertexCount, VertexCount * 4);
				using NativeParallelHashSet<int2> edgeSet = new NativeParallelHashSet<int2>(VertexCount * 2, Allocator.Persistent);
				if (TriangleCount > 0)
				{
					new Proxy_CalcVertexToVertexFromTriangleJob
					{
						triangleCount = TriangleCount,
						triangles = triangles.GetNativeArray(),
						vertexToVertexMap = multiDataBuilder.Map,
						edgeSet = edgeSet
					}.Run();
				}
				if (LineCount > 0)
				{
					new Proxy_CalcVertexToVertexFromLineJob
					{
						lineCount = LineCount,
						lines = lines.GetNativeArray(),
						vertexToVertexMap = multiDataBuilder.Map,
						edgeSet = edgeSet
					}.Run();
				}
				edges = edgeSet.ToNativeArray(Allocator.Persistent);
				multiDataBuilder.ToNativeArray(out vertexToVertexIndexArray, out vertexToVertexDataArray);
				if (TriangleCount > 0)
				{
					edgeToTriangles = new NativeParallelMultiHashMap<int2, ushort>(TriangleCount * 2, Allocator.Persistent);
					new Proxy_CalcEdgeToTriangleJob
					{
						tcnt = TriangleCount,
						triangles = triangles.GetNativeArray(),
						edgeToTriangles = edgeToTriangles
					}.Run();
					using NativeArray<float3> triangleNormals = new NativeArray<float3>(TriangleCount, Allocator.Persistent);
					IJobParallelForExtensions.Run(new Proxy_CalcTriangleNormalJob
					{
						triangles = triangles.GetNativeArray(),
						localPositins = localPositions.GetNativeArray(),
						triangleNormals = triangleNormals
					}, TriangleCount);
					OptimizeTriangleDirection(triangleNormals, 80f);
					using NativeArray<float3> triangleTangents = new NativeArray<float3>(TriangleCount, Allocator.Persistent);
					IJobParallelForExtensions.Run(new Proxy_CalcTriangleTangentJob
					{
						triangles = triangles.GetNativeArray(),
						localPositins = localPositions.GetNativeArray(),
						uv = uv.GetNativeArray(),
						triangleTangents = triangleTangents
					}, TriangleCount);
					new Proxy_CreateVertexToTrianglesJob
					{
						triangles = triangles.GetNativeArray(),
						vertexToTriangles = vertexToTriangles
					}.Run();
					IJobParallelForExtensions.Run(new Proxy_OrganizeVertexToTrianglsJob
					{
						vertexToTriangles = vertexToTriangles,
						triangleNormals = triangleNormals,
						triangleTangents = triangleTangents,
						attributes = attributes.GetNativeArray()
					}, VertexCount);
					IJobParallelForExtensions.Run(new Proxy_CalcVertexNormalTangentFromTriangleJob
					{
						triangleNormals = triangleNormals,
						triangleTangents = triangleTangents,
						vertexToTriangles = vertexToTriangles,
						localNormals = localNormals.GetNativeArray(),
						localTangents = localTangents.GetNativeArray()
					}, VertexCount);
				}
				else
				{
					edgeToTriangles = new NativeParallelMultiHashMap<int2, ushort>(1, Allocator.Persistent);
				}
				ProxyCreateFixedListAndAABB();
				if (isBoneCloth)
				{
					CreateTransformBaseLine();
				}
				else
				{
					CreateMeshBaseLine();
				}
				ProxyNormalAdjustment(sdata, normalAdjustmentTransformRecord);
				if (isBoneCloth)
				{
					IJobParallelForExtensions.Run(new Proxy_CalcVertexToTransformJob
					{
						invRot = initInverseRotation,
						localNormals = localNormals.GetNativeArray(),
						localTangents = localTangents.GetNativeArray(),
						vertexToTransformRotations = vertexToTransformRotations,
						transformRotations = transformData.rotationArray.GetNativeArray()
					}, VertexCount);
				}
				IJobParallelForExtensions.Run(new Proxy_CalcVertexBindPoseJob2
				{
					localPositions = localPositions.GetNativeArray(),
					localNormals = localNormals.GetNativeArray(),
					localTangents = localTangents.GetNativeArray(),
					vertexBindPosePositions = vertexBindPosePositions,
					vertexBindPoseRotations = vertexBindPoseRotations
				}, VertexCount);
				edgeFlags = new NativeArray<ExBitFlag8>(EdgeCount, Allocator.Persistent);
				if (EdgeCount > 0)
				{
					IJobParallelForExtensions.Run(new Proxy_CreateEdgeFlagJob
					{
						edges = edges,
						edgeToTriangles = edgeToTriangles,
						edgeFlags = edgeFlags
					}, EdgeCount);
				}
				CreateBaseLinePose();
				CreateVertexRootAndDepth();
				if (sdata.customSkinningSetting.enable && !sdata.IsBoneSpring())
				{
					CreateCustomSkinning(sdata.customSkinningSetting, customSkinningBoneRecords);
				}
				if (isBoneCloth)
				{
					meshType = MeshType.ProxyBoneMesh;
				}
				else
				{
					meshType = MeshType.ProxyMesh;
				}
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.ProxyMesh_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.ProxyMesh_Exception);
			}
		}

		private void ProxyNormalAdjustment(ClothSerializeData sdata, TransformRecord normalAdjustmentTransformRecord)
		{
			int vertexCount = VertexCount;
			if (vertexCount != 0)
			{
				normalAdjustmentRotations = new NativeArray<quaternion>(vertexCount, Allocator.Persistent);
				JobUtility.FillRun(normalAdjustmentRotations, vertexCount, quaternion.identity);
				NormalAlignmentSettings.AlignmentMode alignmentMode = sdata.normalAlignmentSetting.alignmentMode;
				if (alignmentMode != NormalAlignmentSettings.AlignmentMode.None && (alignmentMode == NormalAlignmentSettings.AlignmentMode.BoundingBoxCenter || alignmentMode == NormalAlignmentSettings.AlignmentMode.Transform))
				{
					float3 center = ((alignmentMode != NormalAlignmentSettings.AlignmentMode.BoundingBoxCenter) ? math.transform(initWorldToLocal, normalAdjustmentTransformRecord.position) : boundingBox.Value.Center);
					IJobParallelForExtensions.Run(new ProxyNormalRadiationAdjustmentJob
					{
						center = center,
						localPositions = localPositions.GetNativeArray(),
						localNormals = localNormals.GetNativeArray(),
						localTangents = localTangents.GetNativeArray(),
						normalAdjustmentRotations = normalAdjustmentRotations
					}, vertexCount);
				}
			}
		}

		private void ProxyCreateFixedListAndAABB()
		{
			localCenterPosition = new NativeReference<float3>(0, Allocator.Persistent);
			using NativeList<ushort> fixedList = new NativeList<ushort>(VertexCount / 20 + 1, Allocator.TempJob);
			new ProxyCreateFixedListAndAABBJob
			{
				vcnt = VertexCount,
				attributes = attributes.GetNativeArray(),
				localPositions = localPositions.GetNativeArray(),
				vertexToVertexIndexArray = vertexToVertexIndexArray,
				vertexToVertexDataArray = vertexToVertexDataArray,
				outAABB = boundingBox,
				fixedList = fixedList,
				localCenterPosition = localCenterPosition
			}.Run();
			if (fixedList.Length > 0)
			{
				centerFixedList = fixedList.AsArray().ToArray();
			}
		}

		private void OptimizeTriangleDirection(NativeArray<float3> triangleNormals, float sameSurfaceAngle)
		{
			if (TriangleCount == 0)
			{
				return;
			}
			int num = 0;
			HashSet<int> hashSet = new HashSet<int>();
			Queue<int> queue = new Queue<int>(TriangleCount / 2);
			List<int> list = new List<int>(TriangleCount);
			while (num < TriangleCount)
			{
				if (hashSet.Contains(num))
				{
					num++;
					continue;
				}
				hashSet.Add(num);
				queue.Clear();
				queue.Enqueue(num);
				list.Clear();
				int num2 = 0;
				int num3 = 0;
				while (queue.Count > 0)
				{
					int num4 = queue.Dequeue();
					float3 tri1n = triangleNormals[num4];
					int3 tri = triangles[num4];
					list.Add(num4);
					int2x3 int2x5 = new int2x3(DataUtility.PackInt2(tri.xy), DataUtility.PackInt2(tri.yz), DataUtility.PackInt2(tri.zx));
					for (int i = 0; i < 3; i++)
					{
						int2 edge = int2x5[i];
						if (!edgeToTriangles.ContainsKey(edge))
						{
							continue;
						}
						foreach (ushort item in edgeToTriangles.GetValuesForKey(edge))
						{
							if (hashSet.Contains(item))
							{
								continue;
							}
							int3 tri2 = triangles[item];
							float3 float5 = triangleNormals[item];
							if (!(CalcTwoTriangleAngle(in tri, in tri2, in edge) > sameSurfaceAngle))
							{
								if (math.dot(tri1n, float5) < 0f)
								{
									tri2 = MathUtility.FlipTriangle(in tri2);
									triangles[item] = tri2;
									triangleNormals[item] = -float5;
								}
								if (CheckTwoTriangleOpen(in tri, in tri2, in edge, in tri1n))
								{
									num2++;
								}
								else
								{
									num3++;
								}
								hashSet.Add(item);
								queue.Enqueue(item);
							}
						}
					}
				}
				if (num3 <= num2)
				{
					continue;
				}
				foreach (int item2 in list)
				{
					triangles[item2] = MathUtility.FlipTriangle(triangles[item2]);
					triangleNormals[item2] = -triangleNormals[item2];
				}
			}
		}

		private void CreateCustomSkinning(CustomSkinningSettings setting, List<TransformRecord> bones)
		{
			if (CustomSkinningBoneCount == 0)
			{
				return;
			}
			using NativeList<SkinningBoneInfo> boneInfoList = new NativeList<SkinningBoneInfo>(CustomSkinningBoneCount * 2, Allocator.Persistent);
			for (int i = 0; i < CustomSkinningBoneCount; i++)
			{
				int num = customSkinningBoneIndices[i];
				if (num != -1)
				{
					SkinningBoneInfo value = new SkinningBoneInfo
					{
						childTransformIndex = num,
						childPos = bones[i].localPosition
					};
					boneInfoList.Add(in value);
				}
			}
			if (boneInfoList.Length != 0)
			{
				IJobParallelForExtensions.Run(new Proxy_CalcCustomSkinningWeightsJobV2
				{
					isBoneCloth = isBoneCloth,
					angularAttenuation = 1f,
					distanceReduction = 0.6f,
					distancePow = 2f,
					attributes = attributes.GetNativeArray(),
					localPositions = localPositions.GetNativeArray(),
					boneInfoList = boneInfoList,
					boneWeights = boneWeights.GetNativeArray()
				}, VertexCount);
			}
		}

		public void ApplySelectionAttribute(SelectionData selectionData)
		{
			try
			{
				using NativeArray<float3> positions = selectionData.GetPositionNativeArray();
				using NativeArray<VertexAttribute> selectionAttributes = selectionData.GetAttributeNativeArray();
				float x = math.max(averageVertexDistance.Value, selectionData.maxConnectionDistance);
				x = math.max(x, 1E-05f);
				float gridSize = x * 1.5f;
				using GridMap<int> gridMap = SelectionData.CreateGridMapRun(gridSize, in positions, in selectionAttributes);
				IJobParallelForExtensions.Run(new Proxy_ApplySelectionJob
				{
					gridSize = gridSize,
					radius = x,
					localPositions = localPositions.GetNativeArray(),
					attributes = attributes.GetNativeArray(),
					gridMap = gridMap.GetMultiHashMap(),
					selectionPositions = positions,
					selectionAttributes = selectionAttributes
				}, VertexCount);
				if (isBoneCloth)
				{
					IJobParallelForExtensions.Run(new Proxy_BoneClothApplayTransformFlagJob
					{
						attributes = attributes.GetNativeArray(),
						transformFlags = transformData.flagArray.GetNativeArray()
					}, VertexCount);
				}
			}
			catch (Exception)
			{
				result.SetError(Define.Result.ProxyMesh_ApplySelectionError);
			}
		}

		private void CreateMeshBaseLine()
		{
			int vertexCount = VertexCount;
			vertexParentIndices = new NativeArray<int>(vertexCount, Allocator.Persistent);
			using MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(vertexCount, vertexCount);
			JobUtility.FillRun(vertexParentIndices, vertexCount, -1);
			using NativeList<int> fixedList = new NativeList<int>(vertexCount, Allocator.Persistent);
			new BaseLine_Mesh_CareteFixedListJob
			{
				vcnt = vertexCount,
				attribues = attributes.GetNativeArray(),
				fixedList = fixedList
			}.Run();
			if (fixedList.Length == 0)
			{
				vertexChildIndexArray = new NativeArray<uint>(vertexCount, Allocator.Persistent);
				vertexChildDataArray = new NativeArray<ushort>(0, Allocator.Persistent);
				return;
			}
			using NativeList<BaseLineWork> nextList = new NativeList<BaseLineWork>(vertexCount, Allocator.Persistent);
			using NativeArray<byte> markBuff = new NativeArray<byte>(vertexCount, Allocator.Persistent);
			using NativeParallelHashMap<int, BaseLineWork> vertexMap = new NativeParallelHashMap<int, BaseLineWork>(vertexCount, Allocator.Persistent);
			new BaseLine_Mesh_CreateParentJob2
			{
				vcnt = vertexCount,
				avgDist = averageVertexDistance.Value,
				attribues = attributes.GetNativeArray(),
				localPositions = localPositions.GetNativeArray(),
				vertexToVertexIndexArray = vertexToVertexIndexArray,
				vertexToVertexDataArray = vertexToVertexDataArray,
				vertexParentIndices = vertexParentIndices,
				vertexChildMap = multiDataBuilder.Map,
				fixedList = fixedList,
				nextList = nextList,
				markBuff = markBuff,
				vertexMap = vertexMap
			}.Run();
			Stack<int> stack = new Stack<int>(vertexCount);
			List<ExBitFlag8> list = new List<ExBitFlag8>(fixedList.Length);
			List<ushort> list2 = new List<ushort>(fixedList.Length);
			List<ushort> list3 = new List<ushort>(fixedList.Length);
			List<ushort> list4 = new List<ushort>(vertexCount);
			for (int i = 0; i < fixedList.Length; i++)
			{
				int num = fixedList[i];
				if (multiDataBuilder.GetDataCount(num) == 0)
				{
					continue;
				}
				stack.Clear();
				stack.Push(num);
				ushort item = (ushort)list4.Count;
				ushort num2 = 0;
				ExBitFlag8 item2 = default(ExBitFlag8);
				while (stack.Count > 0)
				{
					int num3 = stack.Pop();
					list4.Add((ushort)num3);
					num2++;
					if (!attributes[num3].IsSet(128))
					{
						item2.SetFlag(1, sw: true);
					}
					if (!multiDataBuilder.Map.ContainsKey(num3))
					{
						continue;
					}
					foreach (ushort item5 in multiDataBuilder.Map.GetValuesForKey(num3))
					{
						stack.Push(item5);
					}
				}
				list.Add(item2);
				list2.Add(item);
				list3.Add(num2);
			}
			baseLineFlags = new NativeArray<ExBitFlag8>(list.ToArray(), Allocator.Persistent);
			baseLineStartDataIndices = new NativeArray<ushort>(list2.ToArray(), Allocator.Persistent);
			baseLineDataCounts = new NativeArray<ushort>(list3.ToArray(), Allocator.Persistent);
			baseLineData = new NativeArray<ushort>(list4.ToArray(), Allocator.Persistent);
			(ushort[], uint[]) tuple = multiDataBuilder.ToArray();
			ushort[] item3 = tuple.Item1;
			uint[] item4 = tuple.Item2;
			vertexChildIndexArray = new NativeArray<uint>(item4, Allocator.Persistent);
			vertexChildDataArray = new NativeArray<ushort>(item3, Allocator.Persistent);
		}

		private void CreateTransformBaseLine()
		{
			int vertexCount = VertexCount;
			vertexParentIndices = new NativeArray<int>(vertexCount, Allocator.Persistent);
			using MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(vertexCount, vertexCount * 2);
			Dictionary<int, int> dictionary = new Dictionary<int, int>(vertexCount);
			NativeArray<int> nativeArray = transformData.idArray.GetNativeArray();
			NativeArray<int> nativeArray2 = transformData.parentIdArray.GetNativeArray();
			for (int i = 0; i < vertexCount; i++)
			{
				dictionary.Add(nativeArray[i], i);
			}
			for (int j = 0; j < vertexCount; j++)
			{
				int key = nativeArray2[j];
				if (dictionary.ContainsKey(key))
				{
					vertexParentIndices[j] = dictionary[key];
				}
				else
				{
					vertexParentIndices[j] = -1;
				}
			}
			new BaseLine_Bone_CreateBoneChildInfoJob
			{
				vcnt = vertexCount,
				parentIndices = vertexParentIndices,
				childMap = multiDataBuilder.Map
			}.Run();
			int rootCount = transformData.RootCount;
			Stack<int> stack = new Stack<int>(vertexCount);
			Stack<int> stack2 = new Stack<int>(vertexCount);
			List<ExBitFlag8> list = new List<ExBitFlag8>(rootCount);
			List<ushort> list2 = new List<ushort>(rootCount);
			List<ushort> list3 = new List<ushort>(rootCount);
			List<ushort> list4 = new List<ushort>(vertexCount);
			foreach (int rootId in transformData.rootIdList)
			{
				stack.Clear();
				int item = dictionary[rootId];
				stack.Push(item);
				while (stack.Count > 0)
				{
					int num = stack.Pop();
					if (!attributes[num].IsDontMove())
					{
						continue;
					}
					bool flag = false;
					foreach (ushort item4 in multiDataBuilder.Map.GetValuesForKey(num))
					{
						if (attributes[item4].IsMove())
						{
							flag = true;
						}
					}
					if (!flag)
					{
						foreach (ushort item5 in multiDataBuilder.Map.GetValuesForKey(num))
						{
							if (attributes[item5].IsDontMove())
							{
								stack.Push(item5);
							}
						}
						continue;
					}
					stack2.Clear();
					stack2.Push(num);
					ushort item2 = (ushort)list4.Count;
					ushort num2 = 0;
					ExBitFlag8 item3 = default(ExBitFlag8);
					while (stack2.Count > 0)
					{
						int num3 = stack2.Pop();
						list4.Add((ushort)num3);
						num2++;
						if (!attributes[num3].IsSet(128))
						{
							item3.SetFlag(1, sw: true);
						}
						if (!multiDataBuilder.Map.ContainsKey(num3))
						{
							continue;
						}
						foreach (ushort item6 in multiDataBuilder.Map.GetValuesForKey(num3))
						{
							if (!attributes[item6].IsDontMove())
							{
								stack2.Push(item6);
							}
						}
					}
					list.Add(item3);
					list2.Add(item2);
					list3.Add(num2);
				}
			}
			baseLineFlags = new NativeArray<ExBitFlag8>(list.ToArray(), Allocator.Persistent);
			baseLineStartDataIndices = new NativeArray<ushort>(list2.ToArray(), Allocator.Persistent);
			baseLineDataCounts = new NativeArray<ushort>(list3.ToArray(), Allocator.Persistent);
			baseLineData = new NativeArray<ushort>(list4.ToArray(), Allocator.Persistent);
			multiDataBuilder.ToNativeArray(out vertexChildIndexArray, out vertexChildDataArray);
		}

		private void CreateBaseLinePose()
		{
			int length = baseLineData.Length;
			vertexLocalPositions = new NativeArray<float3>(VertexCount, Allocator.Persistent);
			vertexLocalRotations = new NativeArray<quaternion>(VertexCount, Allocator.Persistent);
			IJobParallelForExtensions.Run(new BaseLine_CalcLocalPositionRotationJob
			{
				parentIndices = vertexParentIndices,
				localPositions = localPositions.GetNativeArray(),
				localNormals = localNormals.GetNativeArray(),
				localTangents = localTangents.GetNativeArray(),
				baseLineIndices = baseLineData,
				vertexLocalPositions = vertexLocalPositions,
				vertexLocalRotations = vertexLocalRotations
			}, length);
		}

		private void CreateVertexRootAndDepth()
		{
			int vertexCount = VertexCount;
			vertexDepths = new NativeArray<float>(vertexCount, Allocator.Persistent);
			vertexRootIndices = new NativeArray<int>(vertexCount, Allocator.Persistent);
			using NativeArray<float> rootLengthArray = new NativeArray<float>(vertexCount, Allocator.Persistent);
			new BaseLine_CalcMaxBaseLineLengthJob
			{
				vcnt = vertexCount,
				attribues = attributes.GetNativeArray(),
				localPositions = localPositions.GetNativeArray(),
				vertexParentIndices = vertexParentIndices,
				vertexDepths = vertexDepths,
				vertexRootIndices = vertexRootIndices,
				rootLengthArray = rootLengthArray
			}.Run();
		}

		public void Reduction(ReductionSettings settings, CancellationToken ct)
		{
			try
			{
				using ReductionWorkData reductionWorkData = new ReductionWorkData(this);
				InitReductionWorkData(reductionWorkData);
				if (result.IsError())
				{
					throw new MagicaClothProcessingException();
				}
				float maxSideLength = boundingBox.Value.MaxSideLength;
				if (maxSideLength < 1E-08f)
				{
					result.SetError(Define.Result.Reduction_MaxSideLengthZero);
					throw new MagicaClothProcessingException();
				}
				float num = maxSideLength * math.saturate(0.001f);
				float num2 = maxSideLength * math.saturate(settings.simpleDistance);
				float num3 = maxSideLength * math.saturate(settings.shapeDistance);
				ct.ThrowIfCancellationRequested();
				using (SameDistanceReduction sameDistanceReduction = new SameDistanceReduction(name, this, reductionWorkData, num))
				{
					sameDistanceReduction.Reduction();
					if (sameDistanceReduction.Result.IsError())
					{
						result = sameDistanceReduction.Result;
						throw new MagicaClothProcessingException();
					}
				}
				ct.ThrowIfCancellationRequested();
				if (num2 > num)
				{
					float startMergeLength = math.min(num * 2f, num2);
					using SimpleDistanceReduction simpleDistanceReduction = new SimpleDistanceReduction(name, this, reductionWorkData, startMergeLength, num2, 100, dontMakeLine: true, 1f);
					simpleDistanceReduction.Reduction();
					if (simpleDistanceReduction.Result.IsError())
					{
						result = simpleDistanceReduction.Result;
						throw new MagicaClothProcessingException();
					}
				}
				ct.ThrowIfCancellationRequested();
				if (num3 > 0f && num3 > num2)
				{
					float startMergeLength2 = math.min(math.max(num * 2f, num2), num3);
					using ShapeDistanceReduction shapeDistanceReduction = new ShapeDistanceReduction(name, this, reductionWorkData, startMergeLength2, num3, 100, dontMakeLine: true, 1f);
					shapeDistanceReduction.Reduction();
					if (shapeDistanceReduction.Result.IsError())
					{
						result = shapeDistanceReduction.Result;
						throw new MagicaClothProcessingException();
					}
				}
				ct.ThrowIfCancellationRequested();
				Organization(settings, reductionWorkData);
				if (result.IsError())
				{
					throw new MagicaClothProcessingException();
				}
				ct.ThrowIfCancellationRequested();
				OrganizeStoreVirtualMesh(reductionWorkData);
				if (result.IsError())
				{
					throw new MagicaClothProcessingException();
				}
				CalcAverageAndMaxVertexDistanceRun();
				ct.ThrowIfCancellationRequested();
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.Reduction_UnknownError);
				}
			}
			catch (OperationCanceledException)
			{
				result.SetCancel();
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				result.SetError(Define.Result.Reduction_Exception);
			}
		}

		private void InitReductionWorkData(ReductionWorkData workData)
		{
			try
			{
				int vertexCount = VertexCount;
				int triangleCount = TriangleCount;
				workData.vertexJoinIndices = new NativeArray<int>(vertexCount, Allocator.Persistent);
				JobUtility.FillRun(workData.vertexJoinIndices, vertexCount, -1);
				workData.vertexToVertexMap = new NativeParallelMultiHashMap<ushort, ushort>(vertexCount, Allocator.Persistent);
				new Reduction_InitVertexToVertexJob2
				{
					triangleCount = triangleCount,
					triangles = triangles.GetNativeArray(),
					vertexToVertexMap = workData.vertexToVertexMap
				}.Run();
			}
			catch (Exception)
			{
				result.SetError(Define.Result.Reduction_InitError);
			}
		}

		private void Organization(ReductionSettings setting, ReductionWorkData workData)
		{
			try
			{
				OrganizationInit(setting, workData);
				OrganizationCreateRemapData(workData);
				OrganizationCreateBasicData(workData);
				OrganizationCreateLineTriangle(workData);
			}
			catch (Exception)
			{
				result.SetError(Define.Result.Reduction_OrganizationError);
			}
		}

		private void OrganizationInit(ReductionSettings setting, ReductionWorkData workData)
		{
			workData.oldVertexCount = VertexCount;
			workData.newVertexCount = workData.oldVertexCount - workData.removeVertexCount;
			int newVertexCount = workData.newVertexCount;
			workData.vertexRemapIndices = new NativeArray<int>(workData.oldVertexCount, Allocator.Persistent);
			workData.useSkinBoneMap = new NativeParallelHashMap<int, int>(SkinBoneCount, Allocator.Persistent);
			workData.newSkinBoneCount = new NativeReference<int>(Allocator.Persistent);
			workData.newSkinBoneTransformIndices = new NativeList<int>(SkinBoneCount, Allocator.Persistent);
			workData.newSkinBoneBindPoseList = new NativeList<float4x4>(SkinBoneCount, Allocator.Persistent);
			workData.newAttributes = new ExSimpleNativeArray<VertexAttribute>(newVertexCount);
			workData.newLocalPositions = new ExSimpleNativeArray<float3>(newVertexCount);
			workData.newLocalNormals = new ExSimpleNativeArray<float3>(newVertexCount);
			workData.newLocalTangents = new ExSimpleNativeArray<float3>(newVertexCount);
			workData.newUv = new ExSimpleNativeArray<float2>(newVertexCount);
			workData.newBoneWeights = new ExSimpleNativeArray<VirtualMeshBoneWeight>(newVertexCount);
			workData.newVertexToVertexMap = new NativeParallelMultiHashMap<ushort, ushort>(newVertexCount, Allocator.Persistent);
			workData.edgeSet = new NativeParallelHashSet<int2>(newVertexCount * 2, Allocator.Persistent);
			workData.triangleSet = new NativeParallelHashSet<int3>(newVertexCount, Allocator.Persistent);
			workData.newLineList = new NativeList<int2>(newVertexCount, Allocator.Persistent);
			workData.newTriangleList = new NativeList<int3>(newVertexCount, Allocator.Persistent);
		}

		private void OrganizationCreateRemapData(ReductionWorkData workData)
		{
			new Organize_RemapVertexJob
			{
				oldVertexCount = workData.oldVertexCount,
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices
			}.Run();
			using NativeList<int> useSkinBoneMapKeyList = new NativeList<int>(Allocator.Persistent);
			new Organize_CollectUseSkinBoneJob
			{
				oldVertexCount = workData.oldVertexCount,
				joinIndices = workData.vertexJoinIndices,
				oldBoneWeights = boneWeights.GetNativeArray(),
				oldBindPoses = skinBoneBindPoses.GetNativeArray(),
				useSkinBoneMap = workData.useSkinBoneMap,
				newSkinBoneTransformIndices = workData.newSkinBoneTransformIndices,
				newSkinBoneBindPoses = workData.newSkinBoneBindPoseList,
				newSkinBoneCount = workData.newSkinBoneCount,
				useSkinBoneMapKeyList = useSkinBoneMapKeyList
			}.Run();
		}

		private void OrganizationCreateBasicData(ReductionWorkData workData)
		{
			int newVertexCount = workData.newVertexCount;
			int oldVertexCount = workData.oldVertexCount;
			IJobParallelForExtensions.Run(new Organize_CopyVertexJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices,
				oldAttributes = attributes.GetNativeArray(),
				oldLocalPositions = localPositions.GetNativeArray(),
				oldLocalNormals = localNormals.GetNativeArray(),
				oldLocalTangents = localTangents.GetNativeArray(),
				newAttributes = workData.newAttributes.GetNativeArray(),
				newLocalPositions = workData.newLocalPositions.GetNativeArray(),
				newLocalNormals = workData.newLocalNormals.GetNativeArray(),
				newLocalTangents = workData.newLocalTangents.GetNativeArray()
			}, oldVertexCount);
			JobUtility.CalcUVWithSphereMappingRun(workData.newLocalPositions.GetNativeArray(), newVertexCount, workData.vmesh.boundingBox, workData.newUv.GetNativeArray());
			IJobParallelForExtensions.Run(new Organize_RemapBoneWeightJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices,
				useSkinBoneMap = workData.useSkinBoneMap,
				oldSkinBoneIndices = skinBoneTransformIndices.GetNativeArray(),
				oldBoneWeights = boneWeights.GetNativeArray(),
				newBoneWeights = workData.newBoneWeights.GetNativeArray()
			}, oldVertexCount);
			IJobParallelForExtensions.Run(new Organize_RemapLinkPointArrayJob
			{
				joinIndices = workData.vertexJoinIndices,
				vertexRemapIndices = workData.vertexRemapIndices,
				oldVertexToVertexMap = workData.vertexToVertexMap,
				newVertexToVertexMap = workData.newVertexToVertexMap
			}, VertexCount);
		}

		private void OrganizationCreateLineTriangle(ReductionWorkData workData)
		{
			new Organize_CreateLineTriangleJob
			{
				newVertexCount = workData.newVertexCount,
				newVertexToVertexMap = workData.newVertexToVertexMap,
				edgeSet = workData.edgeSet
			}.Run();
			new Organize_CreateLineTriangleJob2
			{
				newVertexToVertexMap = workData.newVertexToVertexMap,
				newLineList = workData.newLineList,
				edgeSet = workData.edgeSet,
				triangleSet = workData.triangleSet
			}.Run();
			new Organize_CreateNewTriangleJob3
			{
				newTriangleList = workData.newTriangleList,
				triangleSet = workData.triangleSet
			}.Run();
		}

		private void OrganizeStoreVirtualMesh(ReductionWorkData workData)
		{
			try
			{
				int newVertexCount = workData.newVertexCount;
				referenceIndices.Dispose();
				referenceIndices = new ExSimpleNativeArray<int>(newVertexCount);
				JobUtility.SerialNumberRun(referenceIndices.GetNativeArray(), newVertexCount);
				attributes.Dispose();
				attributes = workData.newAttributes;
				workData.newAttributes = null;
				localPositions.Dispose();
				localPositions = workData.newLocalPositions;
				workData.newLocalPositions = null;
				localNormals.Dispose();
				localNormals = workData.newLocalNormals;
				workData.newLocalNormals = null;
				localTangents.Dispose();
				localTangents = workData.newLocalTangents;
				workData.newLocalTangents = null;
				uv.Dispose();
				uv = workData.newUv;
				workData.newUv = null;
				boneWeights.Dispose();
				boneWeights = workData.newBoneWeights;
				workData.newBoneWeights = null;
				lines.Dispose();
				lines = new ExSimpleNativeArray<int2>(workData.newLineList);
				triangles.Dispose();
				triangles = new ExSimpleNativeArray<int3>(workData.newTriangleList);
				transformData.OrganizeReductionTransform(this, workData);
				skinBoneTransformIndices.Dispose();
				skinBoneTransformIndices = new ExSimpleNativeArray<int>(workData.newSkinBoneTransformIndices);
				skinBoneBindPoses.Dispose();
				skinBoneBindPoses = new ExSimpleNativeArray<float4x4>(workData.newSkinBoneBindPoseList);
				joinIndices = new NativeArray<int>(workData.vertexRemapIndices, Allocator.Persistent);
			}
			catch (Exception)
			{
				result.SetError(Define.Result.Reduction_StoreVirtualMeshError);
			}
		}

		public ShareSerializationData ShareSerialize()
		{
			ShareSerializationData shareSerializationData = new ShareSerializationData();
			try
			{
				shareSerializationData.name = name;
				shareSerializationData.meshType = meshType;
				shareSerializationData.isBoneCloth = isBoneCloth;
				shareSerializationData.referenceIndices = referenceIndices.Serialize();
				shareSerializationData.attributes = attributes.Serialize();
				shareSerializationData.localPositions = localPositions.Serialize();
				shareSerializationData.localNormals = localNormals.Serialize();
				shareSerializationData.localTangents = localTangents.Serialize();
				shareSerializationData.uv = uv.Serialize();
				shareSerializationData.boneWeights = boneWeights.Serialize();
				shareSerializationData.triangles = triangles.Serialize();
				shareSerializationData.lines = lines.Serialize();
				shareSerializationData.centerTransformIndex = centerTransformIndex;
				shareSerializationData.initLocalToWorld = initLocalToWorld;
				shareSerializationData.initWorldToLocal = initWorldToLocal;
				shareSerializationData.initRotation = initRotation;
				shareSerializationData.initInverseRotation = initInverseRotation;
				shareSerializationData.initScale = initScale;
				shareSerializationData.skinRootIndex = skinRootIndex;
				shareSerializationData.skinBoneTransformIndices = skinBoneTransformIndices.Serialize();
				shareSerializationData.skinBoneBindPoses = skinBoneBindPoses.Serialize();
				shareSerializationData.transformData = transformData?.ShareSerialize();
				if (boundingBox.IsCreated)
				{
					shareSerializationData.boundingBox = boundingBox.Value;
				}
				if (averageVertexDistance.IsCreated)
				{
					shareSerializationData.averageVertexDistance = averageVertexDistance.Value;
				}
				if (maxVertexDistance.IsCreated)
				{
					shareSerializationData.maxVertexDistance = maxVertexDistance.Value;
				}
				shareSerializationData.vertexToTriangles = NativeArrayExtensions.MC2ToRawBytes(ref vertexToTriangles);
				shareSerializationData.vertexToVertexIndexArray = NativeArrayExtensions.MC2ToRawBytes(ref vertexToVertexIndexArray);
				shareSerializationData.vertexToVertexDataArray = NativeArrayExtensions.MC2ToRawBytes(ref vertexToVertexDataArray);
				shareSerializationData.edges = NativeArrayExtensions.MC2ToRawBytes(ref edges);
				shareSerializationData.edgeFlags = NativeArrayExtensions.MC2ToRawBytes(ref edgeFlags);
				ShareSerializationData shareSerializationData2 = shareSerializationData;
				(int2[], ushort[]) tuple = NativeMultiHashMapExtensions.MC2Serialize(ref edgeToTriangles);
				shareSerializationData.edgeToTrianglesKeys = tuple.Item1;
				shareSerializationData2.edgeToTrianglesValues = tuple.Item2;
				shareSerializationData.vertexBindPosePositions = NativeArrayExtensions.MC2ToRawBytes(ref vertexBindPosePositions);
				shareSerializationData.vertexBindPoseRotations = NativeArrayExtensions.MC2ToRawBytes(ref vertexBindPoseRotations);
				shareSerializationData.vertexToTransformRotations = NativeArrayExtensions.MC2ToRawBytes(ref vertexToTransformRotations);
				shareSerializationData.vertexDepths = NativeArrayExtensions.MC2ToRawBytes(ref vertexDepths);
				shareSerializationData.vertexRootIndices = NativeArrayExtensions.MC2ToRawBytes(ref vertexRootIndices);
				shareSerializationData.vertexParentIndices = NativeArrayExtensions.MC2ToRawBytes(ref vertexParentIndices);
				shareSerializationData.vertexChildIndexArray = NativeArrayExtensions.MC2ToRawBytes(ref vertexChildIndexArray);
				shareSerializationData.vertexChildDataArray = NativeArrayExtensions.MC2ToRawBytes(ref vertexChildDataArray);
				shareSerializationData.vertexLocalPositions = NativeArrayExtensions.MC2ToRawBytes(ref vertexLocalPositions);
				shareSerializationData.vertexLocalRotations = NativeArrayExtensions.MC2ToRawBytes(ref vertexLocalRotations);
				shareSerializationData.normalAdjustmentRotations = NativeArrayExtensions.MC2ToRawBytes(ref normalAdjustmentRotations);
				shareSerializationData.baseLineFlags = NativeArrayExtensions.MC2ToRawBytes(ref baseLineFlags);
				shareSerializationData.baseLineStartDataIndices = NativeArrayExtensions.MC2ToRawBytes(ref baseLineStartDataIndices);
				shareSerializationData.baseLineDataCounts = NativeArrayExtensions.MC2ToRawBytes(ref baseLineDataCounts);
				shareSerializationData.baseLineData = NativeArrayExtensions.MC2ToRawBytes(ref baseLineData);
				DataUtility.ArrayCopy(customSkinningBoneIndices, ref shareSerializationData.customSkinningBoneIndices);
				DataUtility.ArrayCopy(centerFixedList, ref shareSerializationData.centerFixedList);
				if (localCenterPosition.IsCreated)
				{
					shareSerializationData.localCenterPosition = localCenterPosition.Value;
				}
				shareSerializationData.centerWorldPosition = centerWorldPosition;
				shareSerializationData.centerWorldRotation = centerWorldRotation;
				shareSerializationData.centerWorldScale = centerWorldScale;
				shareSerializationData.toProxyMatrix = toProxyMatrix;
				shareSerializationData.toProxyRotation = toProxyRotation;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return shareSerializationData;
		}

		public static VirtualMesh ShareDeserialize(ShareSerializationData sdata)
		{
			VirtualMesh virtualMesh = new VirtualMesh();
			virtualMesh.isManaged = true;
			try
			{
				virtualMesh.name = sdata.name;
				virtualMesh.meshType = sdata.meshType;
				virtualMesh.isBoneCloth = sdata.isBoneCloth;
				virtualMesh.referenceIndices.Deserialize(sdata.referenceIndices);
				virtualMesh.attributes.Deserialize(sdata.attributes);
				virtualMesh.localPositions.Deserialize(sdata.localPositions);
				virtualMesh.localNormals.Deserialize(sdata.localNormals);
				virtualMesh.localTangents.Deserialize(sdata.localTangents);
				virtualMesh.uv.Deserialize(sdata.uv);
				virtualMesh.boneWeights.Deserialize(sdata.boneWeights);
				virtualMesh.triangles.Deserialize(sdata.triangles);
				virtualMesh.lines.Deserialize(sdata.lines);
				virtualMesh.centerTransformIndex = sdata.centerTransformIndex;
				virtualMesh.initLocalToWorld = sdata.initLocalToWorld;
				virtualMesh.initWorldToLocal = sdata.initWorldToLocal;
				virtualMesh.initRotation = sdata.initRotation;
				virtualMesh.initInverseRotation = sdata.initInverseRotation;
				virtualMesh.initScale = sdata.initScale;
				virtualMesh.skinRootIndex = sdata.skinRootIndex;
				virtualMesh.skinBoneTransformIndices.Deserialize(sdata.skinBoneTransformIndices);
				virtualMesh.skinBoneBindPoses.Deserialize(sdata.skinBoneBindPoses);
				virtualMesh.transformData = TransformData.ShareDeserialize(sdata.transformData);
				virtualMesh.boundingBox = new NativeReference<AABB>(sdata.boundingBox, Allocator.Persistent);
				virtualMesh.averageVertexDistance = new NativeReference<float>(sdata.averageVertexDistance, Allocator.Persistent);
				virtualMesh.maxVertexDistance = new NativeReference<float>(sdata.maxVertexDistance, Allocator.Persistent);
				virtualMesh.vertexToTriangles = NativeArrayExtensions.MC2FromRawBytes<FixedList32Bytes<uint>>(sdata.vertexToTriangles);
				virtualMesh.vertexToVertexIndexArray = NativeArrayExtensions.MC2FromRawBytes<uint>(sdata.vertexToVertexIndexArray);
				virtualMesh.vertexToVertexDataArray = NativeArrayExtensions.MC2FromRawBytes<ushort>(sdata.vertexToVertexDataArray);
				virtualMesh.edges = NativeArrayExtensions.MC2FromRawBytes<int2>(sdata.edges);
				virtualMesh.edgeFlags = NativeArrayExtensions.MC2FromRawBytes<ExBitFlag8>(sdata.edgeFlags);
				virtualMesh.edgeToTriangles = NativeMultiHashMapExtensions.MC2Deserialize(sdata.edgeToTrianglesKeys, sdata.edgeToTrianglesValues);
				virtualMesh.vertexBindPosePositions = NativeArrayExtensions.MC2FromRawBytes<float3>(sdata.vertexBindPosePositions);
				virtualMesh.vertexBindPoseRotations = NativeArrayExtensions.MC2FromRawBytes<quaternion>(sdata.vertexBindPoseRotations);
				virtualMesh.vertexToTransformRotations = NativeArrayExtensions.MC2FromRawBytes<quaternion>(sdata.vertexToTransformRotations);
				virtualMesh.vertexDepths = NativeArrayExtensions.MC2FromRawBytes<float>(sdata.vertexDepths);
				virtualMesh.vertexRootIndices = NativeArrayExtensions.MC2FromRawBytes<int>(sdata.vertexRootIndices);
				virtualMesh.vertexParentIndices = NativeArrayExtensions.MC2FromRawBytes<int>(sdata.vertexParentIndices);
				virtualMesh.vertexChildIndexArray = NativeArrayExtensions.MC2FromRawBytes<uint>(sdata.vertexChildIndexArray);
				virtualMesh.vertexChildDataArray = NativeArrayExtensions.MC2FromRawBytes<ushort>(sdata.vertexChildDataArray);
				virtualMesh.vertexLocalPositions = NativeArrayExtensions.MC2FromRawBytes<float3>(sdata.vertexLocalPositions);
				virtualMesh.vertexLocalRotations = NativeArrayExtensions.MC2FromRawBytes<quaternion>(sdata.vertexLocalRotations);
				virtualMesh.normalAdjustmentRotations = NativeArrayExtensions.MC2FromRawBytes<quaternion>(sdata.normalAdjustmentRotations);
				virtualMesh.baseLineFlags = NativeArrayExtensions.MC2FromRawBytes<ExBitFlag8>(sdata.baseLineFlags);
				virtualMesh.baseLineStartDataIndices = NativeArrayExtensions.MC2FromRawBytes<ushort>(sdata.baseLineStartDataIndices);
				virtualMesh.baseLineDataCounts = NativeArrayExtensions.MC2FromRawBytes<ushort>(sdata.baseLineDataCounts);
				virtualMesh.baseLineData = NativeArrayExtensions.MC2FromRawBytes<ushort>(sdata.baseLineData);
				DataUtility.ArrayCopy(sdata.customSkinningBoneIndices, ref virtualMesh.customSkinningBoneIndices);
				DataUtility.ArrayCopy(sdata.centerFixedList, ref virtualMesh.centerFixedList);
				virtualMesh.localCenterPosition = new NativeReference<float3>(sdata.localCenterPosition, Allocator.Persistent);
				virtualMesh.centerWorldPosition = sdata.centerWorldPosition;
				virtualMesh.centerWorldRotation = sdata.centerWorldRotation;
				virtualMesh.centerWorldScale = sdata.centerWorldScale;
				virtualMesh.toProxyMatrix = sdata.toProxyMatrix;
				virtualMesh.toProxyRotation = sdata.toProxyRotation;
				virtualMesh.result.SetSuccess();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				virtualMesh.result.SetError(Define.Result.PreBuildData_VirtualMeshDeserializationException);
			}
			return virtualMesh;
		}

		public UniqueSerializationData UniqueSerialize()
		{
			UniqueSerializationData uniqueSerializationData = new UniqueSerializationData();
			try
			{
				uniqueSerializationData.transformData = transformData?.UniqueSerialize();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return uniqueSerializationData;
		}

		internal void CalcAverageAndMaxVertexDistanceRun()
		{
			try
			{
				if (!averageVertexDistance.IsCreated)
				{
					averageVertexDistance = new NativeReference<float>(Allocator.Persistent);
				}
				if (!maxVertexDistance.IsCreated)
				{
					maxVertexDistance = new NativeReference<float>(Allocator.Persistent);
				}
				averageVertexDistance.Value = 0f;
				maxVertexDistance.Value = 0f;
				using NativeReference<int> averageCount = new NativeReference<int>(Allocator.TempJob);
				if (TriangleCount > 0)
				{
					new Work_AverageTriangleDistanceJob
					{
						vcnt = VertexCount,
						tcnt = TriangleCount,
						localPositions = localPositions.GetNativeArray(),
						triangles = triangles.GetNativeArray(),
						averageVertexDistance = averageVertexDistance,
						averageCount = averageCount,
						maxVertexDistance = maxVertexDistance
					}.Run();
				}
				if (LineCount > 0)
				{
					new Work_AverageLineDistanceJob
					{
						vcnt = VertexCount,
						lcnt = LineCount,
						localPositions = localPositions.GetNativeArray(),
						lines = lines.GetNativeArray(),
						averageVertexDistance = averageVertexDistance,
						averageCount = averageCount,
						maxVertexDistance = maxVertexDistance
					}.Run();
				}
				int value = averageCount.Value;
				if (value > 0)
				{
					float value2 = averageVertexDistance.Value;
					averageVertexDistance.Value = math.sqrt(value2 / (float)value);
					maxVertexDistance.Value = math.sqrt(maxVertexDistance.Value);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.Reduction_CalcAverageException);
			}
		}

		internal GridMap<int> CreateVertexIndexGridMapRun(float gridSize)
		{
			int vertexCount = VertexCount;
			GridMap<int> gridMap = new GridMap<int>(vertexCount);
			if (vertexCount > 0)
			{
				new Work_AddVertexIndexGirdMapJob
				{
					gridSize = gridSize,
					vcnt = vertexCount,
					positins = localPositions.GetNativeArray(),
					gridMap = gridMap.GetMultiHashMap()
				}.Run();
			}
			return gridMap;
		}

		public VirtualMeshRaycastHit IntersectRayMesh(float3 rayPos, float3 rayDir, bool doubleSide, float pointRadius)
		{
			Transform centerTransform = GetCenterTransform();
			float3 localRayPos = centerTransform.InverseTransformPoint(rayPos);
			float3 float5 = centerTransform.InverseTransformDirection(rayDir);
			float3 float6 = rayPos + rayDir * 1000f;
			float3 localRayEndPos = centerTransform.InverseTransformPoint(float6);
			float localEdgeRadius = pointRadius / centerTransform.lossyScale.x;
			int initialCapacity = 100;
			using NativeList<VirtualMeshRaycastHit> hitList = new NativeList<VirtualMeshRaycastHit>(initialCapacity, Allocator.TempJob);
			JobHandle dependsOn = IJobParallelForExtensions.Schedule(dependsOn: default(JobHandle), jobData: new Work_IntersectTriangleJob
			{
				localRayPos = localRayPos,
				localRayDir = float5,
				localRayEndPos = localRayEndPos,
				doubleSide = doubleSide,
				localPositions = localPositions.GetNativeArray(),
				triangles = triangles.GetNativeArray(),
				hitList = hitList.AsParallelWriter()
			}, arrayLength: TriangleCount, innerloopBatchCount: 16);
			dependsOn = IJobParallelForExtensions.Schedule(new Work_IntersectEdgeJob
			{
				localRayPos = localRayPos,
				localRayDir = float5,
				localRayEndPos = localRayEndPos,
				rayDir = float5,
				localEdgeRadius = localEdgeRadius,
				localPositions = localPositions.GetNativeArray(),
				edges = edges,
				edgeToTriangles = edgeToTriangles,
				hitList = hitList.AsParallelWriter()
			}, EdgeCount, 16, dependsOn);
			new Work_IntersetcSortJob
			{
				hitList = hitList
			}.Schedule(dependsOn).Complete();
			return (hitList.Length > 0) ? hitList[0] : default(VirtualMeshRaycastHit);
		}

		public VirtualMesh()
		{
		}

		public VirtualMesh(bool initialize)
		{
			if (initialize)
			{
				transformData = new TransformData(100);
				averageVertexDistance = new NativeReference<float>(0f, Allocator.Persistent);
				maxVertexDistance = new NativeReference<float>(0f, Allocator.Persistent);
				result.SetProcess();
			}
		}

		public VirtualMesh(string name)
			: this(initialize: true)
		{
			this.name = name;
		}

		public void Dispose()
		{
			if (!isManaged)
			{
				result.Clear();
				referenceIndices.Dispose();
				attributes.Dispose();
				localPositions.Dispose();
				localNormals.Dispose();
				localTangents.Dispose();
				uv.Dispose();
				boneWeights.Dispose();
				triangles.Dispose();
				lines.Dispose();
				skinBoneTransformIndices.Dispose();
				skinBoneBindPoses.Dispose();
				if (joinIndices.IsCreated)
				{
					joinIndices.Dispose();
				}
				if (vertexToTriangles.IsCreated)
				{
					vertexToTriangles.Dispose();
				}
				if (vertexToVertexIndexArray.IsCreated)
				{
					vertexToVertexIndexArray.Dispose();
				}
				if (vertexToVertexDataArray.IsCreated)
				{
					vertexToVertexDataArray.Dispose();
				}
				if (edges.IsCreated)
				{
					edges.Dispose();
				}
				if (edgeFlags.IsCreated)
				{
					edgeFlags.Dispose();
				}
				if (edgeToTriangles.IsCreated)
				{
					edgeToTriangles.Dispose();
				}
				if (vertexBindPosePositions.IsCreated)
				{
					vertexBindPosePositions.Dispose();
				}
				if (vertexBindPoseRotations.IsCreated)
				{
					vertexBindPoseRotations.Dispose();
				}
				if (vertexToTransformRotations.IsCreated)
				{
					vertexToTransformRotations.Dispose();
				}
				if (vertexRootIndices.IsCreated)
				{
					vertexRootIndices.Dispose();
				}
				if (vertexParentIndices.IsCreated)
				{
					vertexParentIndices.Dispose();
				}
				if (vertexChildIndexArray.IsCreated)
				{
					vertexChildIndexArray.Dispose();
				}
				if (vertexChildDataArray.IsCreated)
				{
					vertexChildDataArray.Dispose();
				}
				if (baseLineFlags.IsCreated)
				{
					baseLineFlags.Dispose();
				}
				if (baseLineStartDataIndices.IsCreated)
				{
					baseLineStartDataIndices.Dispose();
				}
				if (baseLineDataCounts.IsCreated)
				{
					baseLineDataCounts.Dispose();
				}
				if (baseLineData.IsCreated)
				{
					baseLineData.Dispose();
				}
				if (localCenterPosition.IsCreated)
				{
					localCenterPosition.Dispose();
				}
				if (vertexLocalPositions.IsCreated)
				{
					vertexLocalPositions.Dispose();
				}
				if (vertexLocalRotations.IsCreated)
				{
					vertexLocalRotations.Dispose();
				}
				if (normalAdjustmentRotations.IsCreated)
				{
					normalAdjustmentRotations.Dispose();
				}
				if (vertexDepths.IsCreated)
				{
					vertexDepths.Dispose();
				}
				if (boundingBox.IsCreated)
				{
					boundingBox.Dispose();
				}
				if (averageVertexDistance.IsCreated)
				{
					averageVertexDistance.Dispose();
				}
				if (maxVertexDistance.IsCreated)
				{
					maxVertexDistance.Dispose();
				}
				transformData?.Dispose();
			}
		}

		public void SetName(string newName)
		{
			name = newName;
		}

		public bool IsValid()
		{
			if (transformData == null)
			{
				return false;
			}
			if (centerTransformIndex >= 0 && !transformData.IsEmpty && transformData.GetTransformFromIndex(centerTransformIndex) == null)
			{
				return false;
			}
			return true;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("===== " + name + " =====");
			stringBuilder.AppendLine("Result:" + result.GetResultString());
			stringBuilder.AppendLine($"Type:{meshType}");
			stringBuilder.AppendLine($"Vertex:{VertexCount}");
			stringBuilder.AppendLine($"Line:{LineCount}");
			stringBuilder.AppendLine($"Triangle:{TriangleCount}");
			stringBuilder.AppendLine($"Edge:{EdgeCount}");
			stringBuilder.AppendLine($"SkinBone:{SkinBoneCount}");
			stringBuilder.AppendLine($"Transform:{TransformCount}");
			if (averageVertexDistance.IsCreated)
			{
				stringBuilder.AppendLine($"avgDist:{averageVertexDistance.Value}");
			}
			if (maxVertexDistance.IsCreated)
			{
				stringBuilder.AppendLine($"maxDist:{maxVertexDistance.Value}");
			}
			if (boundingBox.IsCreated)
			{
				stringBuilder.AppendLine($"AABB:{boundingBox.Value}");
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("<<< Proxy >>>");
			stringBuilder.AppendLine($"BaseLine:{BaseLineCount}");
			stringBuilder.AppendLine($"EdgeCount:{EdgeCount}");
			int num = (edgeToTriangles.IsCreated ? edgeToTriangles.Count() : 0);
			stringBuilder.AppendLine($"edgeToTriangles:{num}");
			stringBuilder.AppendLine($"CustomSkinningBoneCount:{CustomSkinningBoneCount}");
			stringBuilder.AppendLine($"CenterFixedPointCount:{CenterFixedPointCount}");
			stringBuilder.AppendLine($"NormalAdjustmentRotationCount:{NormalAdjustmentRotationCount}");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("<<< Mapping >>>");
			stringBuilder.AppendLine($"centerWorldPosition:{centerWorldPosition}");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(transformData?.ToString() ?? "(none)");
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}
	}
}
