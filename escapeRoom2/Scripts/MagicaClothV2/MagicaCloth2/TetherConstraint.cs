using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class TetherConstraint : IDisposable
	{
		[Serializable]
		public class SerializeData : IDataValidate
		{
			[Range(0f, 1f)]
			public float distanceCompression;

			public SerializeData()
			{
				distanceCompression = 0.4f;
			}

			public void DataValidate()
			{
				distanceCompression = Mathf.Clamp(distanceCompression, 0f, 1f);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					distanceCompression = distanceCompression
				};
			}
		}

		public struct TetherConstraintParams
		{
			public float compressionLimit;

			public float stretchLimit;

			public void Convert(SerializeData sdata, ClothProcess.ClothType clothType)
			{
				switch (clothType)
				{
				case ClothProcess.ClothType.MeshCloth:
				case ClothProcess.ClothType.BoneCloth:
					compressionLimit = sdata.distanceCompression;
					break;
				case ClothProcess.ClothType.BoneSpring:
					compressionLimit = 0.8f;
					break;
				}
				stretchLimit = 0.03f;
			}
		}

		public void Dispose()
		{
		}

		internal static void SolverConstraint(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref InertiaConstraint.CenterData cdata, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> vertexDepths, ref NativeArray<int> vertexRootIndices, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float> frictionArray, ref NativeArray<float3> stepBasicPositionBuffer)
		{
			int startIndex = tdata.particleChunk.startIndex;
			int i = startIndex + chunk.startIndex;
			int j = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			for (int k = 0; k < chunk.dataLength; k++, i++, j++)
			{
				if (!attributes[j].IsMove())
				{
					continue;
				}
				int num = vertexRootIndices[j];
				if (num < 0)
				{
					continue;
				}
				float3 float5 = nextPosArray[i];
				float3 obj = nextPosArray[num + startIndex];
				_ = vertexDepths[j];
				_ = frictionArray[i];
				float3 float6 = obj - float5;
				float num2 = math.length(float6);
				if (num2 < 1E-08f)
				{
					continue;
				}
				float3 x = stepBasicPositionBuffer[i];
				float3 y = stepBasicPositionBuffer[num + startIndex];
				float num3 = math.distance(x, y);
				if (num3 == 0f)
				{
					continue;
				}
				float num4 = num2 / num3;
				float num5 = 0f;
				float num6 = 1f - param.tetherConstraint.compressionLimit;
				float num7 = 1f + param.tetherConstraint.stretchLimit;
				float num9;
				float num10;
				if (num4 < num6)
				{
					num5 = num2 - num6 * num3;
					float num8 = math.saturate((num6 - num4) / 0.3f);
					num9 = 1f * num8;
					num10 = 0.7f;
				}
				else
				{
					if (!(num4 > num7))
					{
						continue;
					}
					num5 = num2 - num7 * num3;
					float num11 = math.saturate((num4 - num7) / 0.3f);
					num9 = 1f * num11;
					num10 = 0.7f;
				}
				float3 float7 = float6 / num2 * (num5 * num9);
				float5 += float7;
				nextPosArray[i] = float5;
				velocityPosArray[i] += float7 * num10;
			}
		}
	}
}
