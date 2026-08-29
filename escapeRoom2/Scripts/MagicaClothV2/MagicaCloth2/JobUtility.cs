using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public static class JobUtility
	{
		[BurstCompile]
		private struct FillJob<T> : IJobParallelFor where T : unmanaged
		{
			public T value;

			[WriteOnly]
			public NativeArray<T> array;

			public void Execute(int index)
			{
				array[index] = value;
			}
		}

		[BurstCompile]
		private struct FillJob2<T> : IJobParallelFor where T : unmanaged
		{
			public T value;

			public int startIndex;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<T> array;

			public void Execute(int index)
			{
				array[startIndex + index] = value;
			}
		}

		[BurstCompile]
		private struct FillRefJob<T> : IJob where T : unmanaged
		{
			public T value;

			[WriteOnly]
			public NativeReference<T> reference;

			public void Execute()
			{
				reference.Value = value;
			}
		}

		[BurstCompile]
		private struct SerialNumberJob : IJobParallelFor
		{
			[WriteOnly]
			public NativeArray<int> array;

			public void Execute(int index)
			{
				array[index] = index;
			}
		}

		[BurstCompile]
		private struct ConvertHashSetToListJob<T> : IJob where T : unmanaged, IEquatable<T>
		{
			[ReadOnly]
			public NativeParallelHashSet<T> hashSet;

			[WriteOnly]
			public NativeList<T> list;

			public void Execute()
			{
				foreach (T item in hashSet)
				{
					list.AddNoResize(item);
				}
			}
		}

		[BurstCompile]
		private struct ConvertHashSetKeyToListJob<T> : IJob where T : unmanaged, IEquatable<T>
		{
			[ReadOnly]
			public NativeParallelHashSet<T> hashSet;

			[WriteOnly]
			public NativeList<T> list;

			public void Execute()
			{
				foreach (T item in hashSet)
				{
					T value = item;
					list.Add(in value);
				}
			}
		}

		[BurstCompile]
		private struct CalcAABBJob : IJob
		{
			public int length;

			[ReadOnly]
			public NativeArray<float3> positions;

			public NativeReference<AABB> outAABB;

			public void Execute()
			{
				outAABB.Value = CalcAABBInternal(in positions, length);
			}
		}

		[BurstCompile]
		private struct CalcAABBDeferJob : IJob
		{
			[ReadOnly]
			public NativeList<float3> positions;

			public NativeReference<AABB> outAABB;

			public void Execute()
			{
				outAABB.Value = CalcAABBInternal(positions.AsArray(), positions.Length);
			}
		}

		[BurstCompile]
		private struct CalcUVJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<float3> positions;

			[ReadOnly]
			public NativeReference<AABB> aabb;

			[WriteOnly]
			public NativeArray<float2> uvs;

			public void Execute(int index)
			{
				float3 x = positions[index] - aabb.Value.Center;
				x = math.normalize(x);
				float x2 = math.atan2(x.x, x.z);
				x2 = math.clamp(math.unlerp(-MathF.PI, MathF.PI, x2), 0f, 1f);
				float x3 = math.dot(math.up(), x);
				x3 = math.clamp(math.unlerp(1f, -1f, x3), 0f, 1f);
				float2 float5 = new float2(x3, x2);
				float num = (float)index * 0.0001234f;
				float5 = float5 * 10f + num;
				uvs[index] = float5;
			}
		}

		[BurstCompile]
		public struct AddIntDataCopyJob : IJobParallelFor
		{
			public int dstOffset;

			public int addData;

			[ReadOnly]
			public NativeArray<int> srcData;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int> dstData;

			public void Execute(int index)
			{
				int index2 = dstOffset + index;
				int num = srcData[index];
				num += addData;
				dstData[index2] = num;
			}
		}

		[BurstCompile]
		public struct AddInt2DataCopyJob : IJobParallelFor
		{
			public int dstOffset;

			public int2 addData;

			[ReadOnly]
			public NativeArray<int2> srcData;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int2> dstData;

			public void Execute(int index)
			{
				int index2 = dstOffset + index;
				int2 value = srcData[index];
				value += addData;
				dstData[index2] = value;
			}
		}

		[BurstCompile]
		public struct AddInt3DataCopyJob : IJobParallelFor
		{
			public int dstOffset;

			public int3 addData;

			[ReadOnly]
			public NativeArray<int3> srcData;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<int3> dstData;

			public void Execute(int index)
			{
				int index2 = dstOffset + index;
				int3 value = srcData[index];
				value += addData;
				dstData[index2] = value;
			}
		}

		[BurstCompile]
		public struct TransformPositionJob : IJobParallelFor
		{
			public float4x4 toM;

			public NativeArray<float3> positions;

			public void Execute(int vindex)
			{
				positions[vindex] = MathUtility.TransformPoint(positions[vindex], in toM);
			}
		}

		[BurstCompile]
		public struct TransformPositionJob2 : IJobParallelFor
		{
			public float4x4 toM;

			[ReadOnly]
			public NativeArray<float3> srcPositions;

			[WriteOnly]
			public NativeArray<float3> dstPositions;

			public void Execute(int vindex)
			{
				dstPositions[vindex] = MathUtility.TransformPoint(srcPositions[vindex], in toM);
			}
		}

		[BurstCompile]
		private struct ConvertArrayToMapJob<TData> : IJob where TData : unmanaged
		{
			[ReadOnly]
			public NativeArray<uint> indexArray;

			[ReadOnly]
			public NativeArray<TData> dataArray;

			[WriteOnly]
			public NativeParallelMultiHashMap<int, TData> map;

			public void Execute()
			{
				int length = indexArray.Length;
				for (int i = 0; i < length; i++)
				{
					DataUtility.Unpack12_20(indexArray[i], out var hi, out var low);
					for (int j = 0; j < hi; j++)
					{
						TData item = dataArray[low + j];
						map.Add(i, item);
					}
				}
			}
		}

		[BurstCompile]
		private struct ClearReferenceJob : IJob
		{
			public NativeReference<int> reference;

			public void Execute()
			{
				reference.Value = 0;
			}
		}

		public static JobHandle Fill(NativeArray<int> array, int length, int value, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new FillJob<int>
			{
				value = value,
				array = array
			}, length, 32, dependsOn);
		}

		public static JobHandle Fill(NativeArray<Vector4> array, int length, Vector4 value, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new FillJob<Vector4>
			{
				value = value,
				array = array
			}, length, 32, dependsOn);
		}

		public static JobHandle Fill(NativeArray<VirtualMeshBoneWeight> array, int length, VirtualMeshBoneWeight value, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new FillJob<VirtualMeshBoneWeight>
			{
				value = value,
				array = array
			}, length, 32, dependsOn);
		}

		public static JobHandle Fill(NativeArray<byte> array, int length, byte value, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new FillJob<byte>
			{
				value = value,
				array = array
			}, length, 32, dependsOn);
		}

		public static void FillRun(NativeArray<int> array, int length, int value)
		{
			IJobParallelForExtensions.Run(new FillJob<int>
			{
				value = value,
				array = array
			}, length);
		}

		public static void FillRun(NativeArray<Vector4> array, int length, Vector4 value)
		{
			IJobParallelForExtensions.Run(new FillJob<Vector4>
			{
				value = value,
				array = array
			}, length);
		}

		public static void FillRun(NativeArray<quaternion> array, int length, quaternion value)
		{
			IJobParallelForExtensions.Run(new FillJob<quaternion>
			{
				value = value,
				array = array
			}, length);
		}

		public static void FillRun(NativeArray<VirtualMeshBoneWeight> array, int length, VirtualMeshBoneWeight value)
		{
			IJobParallelForExtensions.Run(new FillJob<VirtualMeshBoneWeight>
			{
				value = value,
				array = array
			}, length);
		}

		public static JobHandle Fill(NativeArray<int> array, int startIndex, int length, int value, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new FillJob2<int>
			{
				value = value,
				startIndex = startIndex,
				array = array
			}, length, 32, dependsOn);
		}

		public static JobHandle Fill(NativeReference<int> reference, int value, JobHandle dependsOn = default(JobHandle))
		{
			return new FillRefJob<int>
			{
				value = value,
				reference = reference
			}.Schedule(dependsOn);
		}

		public static JobHandle SerialNumber(NativeArray<int> array, int length, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new SerialNumberJob
			{
				array = array
			}, length, 32, dependsOn);
		}

		public static void SerialNumberRun(NativeArray<int> array, int length)
		{
			IJobParallelForExtensions.Run(new SerialNumberJob
			{
				array = array
			}, length);
		}

		public static JobHandle ConvertHashSetToNativeList(NativeParallelHashSet<int> hashSet, NativeList<int> list, JobHandle dependsOn = default(JobHandle))
		{
			return new ConvertHashSetToListJob<int>
			{
				hashSet = hashSet,
				list = list
			}.Schedule(dependsOn);
		}

		public static JobHandle ConvertHashSetKeyToNativeList(NativeParallelHashSet<int2> hashSet, NativeList<int2> keyList, JobHandle dependsOn = default(JobHandle))
		{
			return new ConvertHashSetKeyToListJob<int2>
			{
				hashSet = hashSet,
				list = keyList
			}.Schedule(dependsOn);
		}

		public static JobHandle ConvertHashSetKeyToNativeList(NativeParallelHashSet<int4> hashSet, NativeList<int4> keyList, JobHandle dependsOn = default(JobHandle))
		{
			return new ConvertHashSetKeyToListJob<int4>
			{
				hashSet = hashSet,
				list = keyList
			}.Schedule(dependsOn);
		}

		public static JobHandle CalcAABB(NativeArray<float3> positions, int length, NativeReference<AABB> outAABB, JobHandle dependsOn = default(JobHandle))
		{
			return new CalcAABBJob
			{
				length = length,
				positions = positions,
				outAABB = outAABB
			}.Schedule(dependsOn);
		}

		public static void CalcAABBRun(NativeArray<float3> positions, int length, NativeReference<AABB> outAABB)
		{
			new CalcAABBJob
			{
				length = length,
				positions = positions,
				outAABB = outAABB
			}.Run();
		}

		public static JobHandle CalcAABB(NativeList<float3> positions, NativeReference<AABB> outAABB, JobHandle dependsOn = default(JobHandle))
		{
			return new CalcAABBDeferJob
			{
				positions = positions,
				outAABB = outAABB
			}.Schedule(dependsOn);
		}

		public static void CalcAABBRun(NativeList<float3> positions, NativeReference<AABB> outAABB)
		{
			new CalcAABBDeferJob
			{
				positions = positions,
				outAABB = outAABB
			}.Run();
		}

		private static AABB CalcAABBInternal(in NativeArray<float3> positions, int length)
		{
			if (positions.Length == 0)
			{
				return default(AABB);
			}
			float3 min = float.MaxValue;
			float3 max = float.MinValue;
			for (int i = 0; i < length; i++)
			{
				float3 y = positions[i];
				min = math.min(min, y);
				max = math.max(max, y);
			}
			return new AABB(in min, in max);
		}

		public static JobHandle CalcUVWithSphereMapping(NativeArray<float3> positions, int length, NativeReference<AABB> aabb, NativeArray<float2> outUVs, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new CalcUVJob
			{
				positions = positions,
				aabb = aabb,
				uvs = outUVs
			}, length, 32, dependsOn);
		}

		public static void CalcUVWithSphereMappingRun(NativeArray<float3> positions, int length, NativeReference<AABB> aabb, NativeArray<float2> outUVs)
		{
			IJobParallelForExtensions.Run(new CalcUVJob
			{
				positions = positions,
				aabb = aabb,
				uvs = outUVs
			}, length);
		}

		public static JobHandle TransformPosition(NativeArray<float3> positions, int length, in float4x4 toM, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new TransformPositionJob
			{
				toM = toM,
				positions = positions
			}, length, 32, dependsOn);
		}

		public static void TransformPositionRun(NativeArray<float3> positions, int length, in float4x4 toM)
		{
			IJobParallelForExtensions.Run(new TransformPositionJob
			{
				toM = toM,
				positions = positions
			}, length);
		}

		public static JobHandle TransformPosition(NativeArray<float3> srcPositions, NativeArray<float3> dstPositions, int length, in float4x4 toM, JobHandle dependsOn = default(JobHandle))
		{
			return IJobParallelForExtensions.Schedule(new TransformPositionJob2
			{
				toM = toM,
				srcPositions = srcPositions,
				dstPositions = dstPositions
			}, length, 32, dependsOn);
		}

		public static void TransformPositionRun(NativeArray<float3> srcPositions, NativeArray<float3> dstPositions, int length, in float4x4 toM)
		{
			IJobParallelForExtensions.Run(new TransformPositionJob2
			{
				toM = toM,
				srcPositions = srcPositions,
				dstPositions = dstPositions
			}, length);
		}

		public static NativeParallelMultiHashMap<int, ushort> ToNativeMultiHashMap(in NativeArray<uint> indexArray, in NativeArray<ushort> dataArray)
		{
			NativeParallelMultiHashMap<int, ushort> nativeParallelMultiHashMap = new NativeParallelMultiHashMap<int, ushort>(dataArray.Length, Allocator.Persistent);
			new ConvertArrayToMapJob<ushort>
			{
				indexArray = indexArray,
				dataArray = dataArray,
				map = nativeParallelMultiHashMap
			}.Run();
			return nativeParallelMultiHashMap;
		}

		public static JobHandle ClearReference(NativeReference<int> reference, JobHandle jobHandle)
		{
			return new ClearReferenceJob
			{
				reference = reference
			}.Schedule(jobHandle);
		}
	}
}
