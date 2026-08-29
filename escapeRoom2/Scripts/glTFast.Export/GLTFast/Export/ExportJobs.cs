using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Export
{
	[BurstCompile]
	internal static class ExportJobs
	{
		[BurstCompile]
		public struct ConvertIndicesFlippedJobUInt16 : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<ushort> input;

			[WriteOnly]
			[NativeDisableParallelForRestriction]
			public NativeArray<ushort> result;

			public int indexStart;

			public ushort baseVertexOffset;

			public void Execute(int i)
			{
				result[i * 3] = (ushort)(input[i * 3] + baseVertexOffset);
				result[i * 3 + 1] = (ushort)(input[i * 3 + 2] + baseVertexOffset);
				result[i * 3 + 2] = (ushort)(input[i * 3 + 1] + baseVertexOffset);
			}
		}

		[BurstCompile]
		public struct ConvertIndicesFlippedJobUInt32 : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<uint> input;

			[WriteOnly]
			[NativeDisableParallelForRestriction]
			public NativeArray<uint> result;

			public uint baseVertexOffset;

			public void Execute(int index)
			{
				result[index * 3] = input[index * 3] + baseVertexOffset;
				result[index * 3 + 1] = input[index * 3 + 2] + baseVertexOffset;
				result[index * 3 + 2] = input[index * 3 + 1] + baseVertexOffset;
			}
		}

		[BurstCompile]
		public struct ConvertIndicesQuadFlippedJobUInt16 : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<ushort> input;

			[WriteOnly]
			[NativeDisableParallelForRestriction]
			public NativeArray<ushort> result;

			public ushort baseVertexOffset;

			public void Execute(int i)
			{
				result[i * 6] = (ushort)(input[i * 4] + baseVertexOffset);
				result[i * 6 + 1] = (ushort)(input[i * 4 + 2] + baseVertexOffset);
				result[i * 6 + 2] = (ushort)(input[i * 4 + 1] + baseVertexOffset);
				result[i * 6 + 3] = (ushort)(input[i * 4 + 2] + baseVertexOffset);
				result[i * 6 + 4] = (ushort)(input[i * 4] + baseVertexOffset);
				result[i * 6 + 5] = (ushort)(input[i * 4 + 3] + baseVertexOffset);
			}
		}

		[BurstCompile]
		public struct ConvertIndicesQuadFlippedJobUInt32 : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<uint> input;

			[WriteOnly]
			[NativeDisableParallelForRestriction]
			public NativeArray<uint> result;

			public uint baseVertexOffset;

			public void Execute(int index)
			{
				result[index * 6] = input[index * 4] + baseVertexOffset;
				result[index * 6 + 1] = input[index * 4 + 2] + baseVertexOffset;
				result[index * 6 + 2] = input[index * 4 + 1] + baseVertexOffset;
				result[index * 6 + 3] = input[index * 4 + 2] + baseVertexOffset;
				result[index * 6 + 4] = input[index * 4] + baseVertexOffset;
				result[index * 6 + 5] = input[index * 4 + 3] + baseVertexOffset;
			}
		}

		[BurstCompile]
		public struct ConvertPositionFloatJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				float3* ptr = (float3*)(input + i * inputByteStride);
				float3* ptr2 = (float3*)(output + i * outputByteStride);
				float3 float5 = *ptr;
				float5.x *= -1f;
				*ptr2 = float5;
			}
		}

		[BurstCompile]
		public struct ConvertPositionHalfJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				half3* ptr = (half3*)(input + i * inputByteStride);
				float3* ptr2 = (float3*)(output + i * outputByteStride);
				float3 float5 = *ptr;
				float5.x *= -1f;
				*ptr2 = float5;
			}
		}

		[BurstCompile]
		public struct ConvertTangentFloatJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				float4* ptr = (float4*)(input + i * inputByteStride);
				float4* ptr2 = (float4*)(output + i * outputByteStride);
				float4 float5 = *ptr;
				float5.z *= -1f;
				*ptr2 = float5;
			}
		}

		[BurstCompile]
		public struct ConvertTangentHalfJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				half4* ptr = (half4*)(input + i * inputByteStride);
				float4* ptr2 = (float4*)(output + i * outputByteStride);
				float4 float5 = *ptr;
				float5.z *= -1f;
				*ptr2 = float5;
			}
		}

		[BurstCompile]
		public struct ConvertTexCoordFloatJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				float2* ptr = (float2*)(input + i * inputByteStride);
				float2* ptr2 = (float2*)(output + i * outputByteStride);
				float2 float5 = *ptr;
				float5.y = 1f - float5.y;
				*ptr2 = float5;
			}
		}

		[BurstCompile]
		public struct ConvertTexCoordHalfJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				half2* ptr = (half2*)(input + i * inputByteStride);
				float2* ptr2 = (float2*)(output + i * outputByteStride);
				float2 float5 = *ptr;
				float5.y = 1f - float5.y;
				*ptr2 = float5;
			}
		}

		[BurstCompile]
		public struct ConvertSkinWeightsJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				float4* ptr = (float4*)(input + i * inputByteStride);
				float4* ptr2 = (float4*)(output + i * outputByteStride);
				*ptr2 = *ptr;
			}
		}

		[BurstCompile]
		public struct ConvertMatrixJob : IJobParallelFor
		{
			public NativeArray<float4x4> matrices;

			public void Execute(int i)
			{
				float4x4 value = matrices[i];
				value.c0.y *= -1f;
				value.c0.z *= -1f;
				value.c1.x *= -1f;
				value.c2.x *= -1f;
				value.c3.x *= -1f;
				matrices[i] = value;
			}
		}

		[BurstCompile]
		public struct ConvertSkinIndicesJob : IJobParallelFor
		{
			private struct ushort4
			{
				private ushort m_X;

				private ushort m_Y;

				private ushort m_Z;

				private ushort m_W;

				public ushort4(uint x, uint y, uint z, uint w)
				{
					m_X = (ushort)x;
					m_Y = (ushort)y;
					m_Z = (ushort)z;
					m_W = (ushort)w;
				}
			}

			public uint inputByteStride;

			public int indicesOffset;

			public uint outputByteStride;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				uint4* ptr = (uint4*)(indicesOffset + input + i * inputByteStride);
				ushort4* ptr2 = (ushort4*)(indicesOffset + output + i * outputByteStride);
				uint4 uint5 = *ptr;
				ushort4 ushort5 = new ushort4(uint5[0], uint5[1], uint5[2], uint5[3]);
				*ptr2 = ushort5;
			}
		}

		[BurstCompile]
		public struct ConvertGenericJob : IJobParallelFor
		{
			public uint inputByteStride;

			public uint outputByteStride;

			public uint byteLength;

			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* input;

			[WriteOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* output;

			public unsafe void Execute(int i)
			{
				byte* source = input + i * inputByteStride;
				UnsafeUtility.MemCpy(output + i * outputByteStride, source, byteLength);
			}
		}
	}
}
