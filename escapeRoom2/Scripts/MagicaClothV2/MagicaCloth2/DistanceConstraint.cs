using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class DistanceConstraint : IDisposable
	{
		[Serializable]
		public class SerializeData : IDataValidate
		{
			public CurveSerializeData stiffness;

			public SerializeData()
			{
				stiffness = new CurveSerializeData(1f, 1f, 0.5f, useCurve: false);
			}

			public void DataValidate()
			{
				stiffness.DataValidate(0f, 1f);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					stiffness = stiffness.Clone()
				};
			}
		}

		public struct DistanceConstraintParams
		{
			public float4x4 restorationStiffness;

			public float velocityAttenuation;

			public void Convert(SerializeData sdata, ClothProcess.ClothType clothType)
			{
				switch (clothType)
				{
				case ClothProcess.ClothType.MeshCloth:
				case ClothProcess.ClothType.BoneCloth:
					restorationStiffness = sdata.stiffness.ConvertFloatArray();
					break;
				case ClothProcess.ClothType.BoneSpring:
					restorationStiffness = 0.5f;
					break;
				}
				velocityAttenuation = 0.3f;
			}
		}

		[Serializable]
		public class ConstraintData : IValid
		{
			public ResultCode result;

			public uint[] indexArray;

			public ushort[] dataArray;

			public float[] distanceArray;

			public bool IsValid()
			{
				if (indexArray != null)
				{
					return indexArray.Length != 0;
				}
				return false;
			}
		}

		public const int TypeCount = 2;

		public ExNativeArray<uint> indexArray;

		public ExNativeArray<ushort> dataArray;

		public ExNativeArray<float> distanceArray;

		public int DataCount => indexArray?.Count ?? 0;

		public DistanceConstraint()
		{
			indexArray = new ExNativeArray<uint>(0, create: true);
			dataArray = new ExNativeArray<ushort>(0, create: true);
			distanceArray = new ExNativeArray<float>(0, create: true);
		}

		public void Dispose()
		{
			indexArray?.Dispose();
			dataArray?.Dispose();
			distanceArray?.Dispose();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[DistanceConstraint]");
			stringBuilder.AppendLine("  -indexArray:" + indexArray.ToSummary());
			stringBuilder.AppendLine("  -dataArray:" + dataArray.ToSummary());
			stringBuilder.AppendLine("  -distanceArray:" + distanceArray.ToSummary());
			return stringBuilder.ToString();
		}

		public static ConstraintData CreateData(VirtualMesh proxyMesh, in ClothParameters parameters)
		{
			ConstraintData constraintData = new ConstraintData();
			NativeParallelMultiHashMap<int, ushort> nativeParallelMultiHashMap = default(NativeParallelMultiHashMap<int, ushort>);
			try
			{
				int vertexCount = proxyMesh.VertexCount;
				nativeParallelMultiHashMap = JobUtility.ToNativeMultiHashMap(in proxyMesh.vertexToVertexIndexArray, in proxyMesh.vertexToVertexDataArray);
				HashSet<uint> hashSet = new HashSet<uint>();
				using MultiDataBuilder<ushort> multiDataBuilder = new MultiDataBuilder<ushort>(vertexCount, vertexCount * 2);
				using MultiDataBuilder<ushort> multiDataBuilder2 = new MultiDataBuilder<ushort>(vertexCount, vertexCount * 2);
				for (int i = 0; i < vertexCount; i++)
				{
					if (!nativeParallelMultiHashMap.ContainsKey(i))
					{
						continue;
					}
					VertexAttribute vertexAttribute = proxyMesh.attributes[i];
					int num = proxyMesh.vertexParentIndices[i];
					foreach (ushort item8 in nativeParallelMultiHashMap.GetValuesForKey(i))
					{
						int num2 = item8;
						VertexAttribute vertexAttribute2 = proxyMesh.attributes[num2];
						int num3 = proxyMesh.vertexParentIndices[num2];
						if ((vertexAttribute.IsMove() || vertexAttribute2.IsMove()) && !vertexAttribute.IsInvalid() && !vertexAttribute2.IsInvalid())
						{
							if (num2 == num || i == num3)
							{
								multiDataBuilder.Add(i, item8);
							}
							else
							{
								multiDataBuilder2.Add(i, item8);
							}
							uint item = DataUtility.Pack32Sort(i, num2);
							hashSet.Add(item);
						}
					}
				}
				if (proxyMesh.edgeToTriangles.IsCreated)
				{
					int edgeCount = proxyMesh.EdgeCount;
					for (int j = 0; j < edgeCount; j++)
					{
						int2 int5 = proxyMesh.edges[j];
						FixedList128Bytes<ushort> fixedList128Bytes = NativeMultiHashMapExtensions.MC2ToFixedList128Bytes(ref proxyMesh.edgeToTriangles, int5);
						int length = fixedList128Bytes.Length;
						if (length < 2)
						{
							continue;
						}
						float3 p = proxyMesh.localPositions[int5.x];
						float3 p2 = proxyMesh.localPositions[int5.y];
						float num4 = math.length(p - p2);
						if (num4 < 1E-08f)
						{
							continue;
						}
						math.normalize(p - p2);
						_ = (p + p2) * 0.5f;
						for (int k = 0; k < length - 1; k++)
						{
							int unuseTriangleIndex = MathUtility.GetUnuseTriangleIndex(proxyMesh.triangles[fixedList128Bytes[k]], int5);
							float3 p3 = proxyMesh.localPositions[unuseTriangleIndex];
							VertexAttribute vertexAttribute3 = proxyMesh.attributes[unuseTriangleIndex];
							float3 x = MathUtility.TriangleNormal(in p, in p2, in p3);
							for (int l = k + 1; l < length; l++)
							{
								int unuseTriangleIndex2 = MathUtility.GetUnuseTriangleIndex(proxyMesh.triangles[fixedList128Bytes[l]], int5);
								float3 p4 = proxyMesh.localPositions[unuseTriangleIndex2];
								VertexAttribute vertexAttribute4 = proxyMesh.attributes[unuseTriangleIndex2];
								float3 y = MathUtility.TriangleNormal(in p, in p2, in p4);
								if ((vertexAttribute3.IsMove() || vertexAttribute4.IsMove()) && !(math.abs(math.dot(x, y)) < 0.9396926f) && math.abs(math.length(p3 - p4) / num4 - 1f) <= 0.3f)
								{
									uint item2 = DataUtility.Pack32Sort(unuseTriangleIndex, unuseTriangleIndex2);
									if (!hashSet.Contains(item2))
									{
										hashSet.Add(item2);
										multiDataBuilder2.Add(unuseTriangleIndex, (ushort)unuseTriangleIndex2);
										multiDataBuilder2.Add(unuseTriangleIndex2, (ushort)unuseTriangleIndex);
									}
								}
							}
						}
					}
				}
				(ushort[], uint[]) tuple = multiDataBuilder.ToArray();
				ushort[] item3 = tuple.Item1;
				uint[] item4 = tuple.Item2;
				(ushort[], uint[]) tuple2 = multiDataBuilder2.ToArray();
				ushort[] item5 = tuple2.Item1;
				uint[] item6 = tuple2.Item2;
				int num5 = ((item3 != null) ? item3.Length : 0) + ((item5 != null) ? item5.Length : 0);
				if (num5 > 0)
				{
					List<uint> list = new List<uint>(vertexCount);
					List<ushort> list2 = new List<ushort>(num5);
					List<float> list3 = new List<float>(num5);
					for (int m = 0; m < vertexCount; m++)
					{
						int count = list2.Count;
						int num6 = 0;
						float3 x2 = proxyMesh.localPositions[m];
						for (int n = 0; n < 2; n++)
						{
							DataUtility.Unpack12_20((n == 0) ? item4[m] : item6[m], out var hi, out var low);
							for (int num7 = 0; num7 < hi; num7++)
							{
								ushort num8 = ((n == 0) ? item3[low + num7] : item5[low + num7]);
								float3 y2 = proxyMesh.localPositions[num8];
								float num9 = math.distance(x2, y2);
								if (!(num9 < 1E-06f))
								{
									list2.Add(num8);
									list3.Add((n == 0) ? num9 : (0f - num9));
									num6++;
								}
							}
						}
						uint item7 = DataUtility.Pack12_20(num6, count);
						list.Add(item7);
					}
					constraintData.indexArray = list.ToArray();
					constraintData.dataArray = list2.ToArray();
					constraintData.distanceArray = list3.ToArray();
				}
				constraintData.result.SetSuccess();
				return constraintData;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				constraintData.result.SetError(Define.Result.Constraint_CreateDistanceException);
				throw;
			}
			finally
			{
				if (nativeParallelMultiHashMap.IsCreated)
				{
					nativeParallelMultiHashMap.Dispose();
				}
			}
		}

		internal void Register(ClothProcess cprocess)
		{
			if (cprocess?.distanceConstraintData?.IsValid() == true)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
				teamDataRef.distanceStartChunk = indexArray.AddRange(cprocess.distanceConstraintData.indexArray);
				teamDataRef.distanceDataChunk = dataArray.AddRange(cprocess.distanceConstraintData.dataArray);
				distanceArray.AddRange(cprocess.distanceConstraintData.distanceArray);
			}
		}

		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
				indexArray.Remove(teamDataRef.distanceStartChunk);
				dataArray.Remove(teamDataRef.distanceDataChunk);
				distanceArray.Remove(teamDataRef.distanceDataChunk);
				teamDataRef.distanceStartChunk.Clear();
				teamDataRef.distanceDataChunk.Clear();
			}
		}

		internal static void SolverConstraint(DataChunk chunk, float4 simulationPower, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> depthArray, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> basePosArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float> frictionArray, ref NativeArray<uint> indexArray, ref NativeArray<ushort> dataArray, ref NativeArray<float> distanceArray)
		{
			DataChunk distanceStartChunk = tdata.distanceStartChunk;
			DataChunk distanceDataChunk = tdata.distanceDataChunk;
			if (distanceStartChunk.dataLength == 0)
			{
				return;
			}
			int startIndex = distanceStartChunk.startIndex;
			int startIndex2 = distanceDataChunk.startIndex;
			float animationPoseRatio = tdata.animationPoseRatio;
			float num = tdata.InitScale * tdata.scaleRatio;
			bool isSpring = tdata.IsSpring;
			int startIndex3 = tdata.particleChunk.startIndex;
			int num2 = startIndex3 + chunk.startIndex;
			int startIndex4 = tdata.proxyCommonChunk.startIndex;
			int num3 = startIndex4 + chunk.startIndex;
			int num4 = chunk.startIndex;
			int num5 = 0;
			while (num5 < chunk.dataLength)
			{
				float3 float5 = nextPosArray[num2];
				VertexAttribute vertexAttribute = attributes[num3];
				float num6 = depthArray[num3];
				float friction = frictionArray[num2];
				if (!vertexAttribute.IsInvalid() && (!vertexAttribute.IsDontMove() || isSpring))
				{
					float fixMass = (isSpring ? 10f : 50f);
					float num7 = MathUtility.CalcInverseMass(friction, num6, vertexAttribute.IsDontMove(), fixMass);
					float num8 = param.distanceConstraint.restorationStiffness.MC2EvaluateCurveClamp01(num6);
					num8 *= simulationPower.y;
					DataUtility.Unpack12_20(indexArray[startIndex + num4], out var hi, out var low);
					if (hi > 0)
					{
						float3 x = basePosArray[num2];
						float3 float6 = 0;
						int num9 = 0;
						int num10 = startIndex2 + low;
						for (int i = 0; i < hi; i++)
						{
							int num11 = dataArray[num10 + i];
							float num12 = distanceArray[num10 + i];
							float num13 = math.saturate((num12 >= 0f) ? num8 : (num8 * 0.5f));
							int index = startIndex3 + num11;
							int index2 = startIndex4 + num11;
							float3 obj = nextPosArray[index];
							float3 y = basePosArray[index];
							float num14 = MathUtility.CalcInverseMass(depth: depthArray[index2], friction: frictionArray[index], fix: attributes[index2].IsDontMove(), fixMass: fixMass);
							float num15 = math.lerp(math.abs(num12) * num, math.distance(x, y), animationPoseRatio);
							float3 x2 = obj - float5;
							float num16 = math.length(x2);
							if (!(num16 < 1E-08f))
							{
								float3 float7 = math.normalize(x2);
								float3 float8 = num13 * float7 * (num16 - num15) / (num7 + num14);
								float3 float9 = num7 * float8;
								float6 += float9;
								num9++;
							}
						}
						if (num9 > 0)
						{
							float6 /= (float)num9;
							float5 += float6;
							nextPosArray[num2] = float5;
							float velocityAttenuation = param.distanceConstraint.velocityAttenuation;
							velocityPosArray[num2] += float6 * velocityAttenuation;
						}
					}
				}
				num5++;
				num2++;
				num3++;
				num4++;
			}
		}
	}
}
