using System;
using System.Text;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class AngleConstraint : IDisposable
	{
		[Serializable]
		public class RestorationSerializeData : IDataValidate
		{
			public bool useAngleRestoration;

			public CurveSerializeData stiffness;

			[Range(0f, 1f)]
			public float velocityAttenuation;

			[Range(0f, 1f)]
			public float gravityFalloff;

			public RestorationSerializeData()
			{
				useAngleRestoration = true;
				stiffness = new CurveSerializeData(0.2f, 1f, 0.2f);
				velocityAttenuation = 0.8f;
				gravityFalloff = 0f;
			}

			public void DataValidate()
			{
				stiffness.DataValidate(0f, 1f);
				velocityAttenuation = Mathf.Clamp01(velocityAttenuation);
				gravityFalloff = Mathf.Clamp01(gravityFalloff);
			}

			public RestorationSerializeData Clone()
			{
				return new RestorationSerializeData
				{
					useAngleRestoration = useAngleRestoration,
					stiffness = stiffness.Clone(),
					velocityAttenuation = velocityAttenuation,
					gravityFalloff = gravityFalloff
				};
			}
		}

		[Serializable]
		public class LimitSerializeData : IDataValidate
		{
			public bool useAngleLimit;

			public CurveSerializeData limitAngle;

			[Range(0f, 1f)]
			public float stiffness;

			public LimitSerializeData()
			{
				useAngleLimit = false;
				limitAngle = new CurveSerializeData(60f, 0f, 1f);
				stiffness = 1f;
			}

			public void DataValidate()
			{
				limitAngle.DataValidate(0f, 180f);
				stiffness = Mathf.Clamp01(stiffness);
			}

			public LimitSerializeData Clone()
			{
				return new LimitSerializeData
				{
					useAngleLimit = useAngleLimit,
					limitAngle = limitAngle.Clone(),
					stiffness = stiffness
				};
			}
		}

		public struct AngleConstraintParams
		{
			public bool useAngleRestoration;

			public float4x4 restorationStiffness;

			public float restorationVelocityAttenuation;

			public float restorationGravityFalloff;

			public bool useAngleLimit;

			public float4x4 limitCurveData;

			public float limitstiffness;

			public void Convert(RestorationSerializeData restorationData, LimitSerializeData limitData)
			{
				useAngleRestoration = restorationData.useAngleRestoration;
				restorationStiffness = restorationData.stiffness.ConvertFloatArray() * 0.2f;
				restorationVelocityAttenuation = restorationData.velocityAttenuation;
				restorationGravityFalloff = restorationData.gravityFalloff;
				useAngleLimit = limitData.useAngleLimit;
				limitCurveData = limitData.limitAngle.ConvertFloatArray();
				limitstiffness = limitData.stiffness;
			}
		}

		public void Dispose()
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[AngleConstraint]");
			return stringBuilder.ToString();
		}

		internal static void SolverConstraint(DataChunk chunk, in float4 simulationPower, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> vertexDepths, ref NativeArray<int> vertexParentIndices, ref NativeArray<ushort> baseLineStartDataIndices, ref NativeArray<ushort> baseLineDataCounts, ref NativeArray<ushort> baseLineData, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float> frictionArray, ref NativeArray<float3> stepBasicPositionBuffer, ref NativeArray<quaternion> stepBasicRotationBuffer, ref NativeArray<float> lengthBufferArray, ref NativeArray<float3> localPosBufferArray, ref NativeArray<quaternion> localRotBufferArray, ref NativeArray<quaternion> rotationBufferArray, ref NativeArray<float3> restorationVectorBufferArray)
		{
			AngleConstraintParams angleConstraint = param.angleConstraint;
			if (!angleConstraint.useAngleLimit && !angleConstraint.useAngleRestoration)
			{
				return;
			}
			int startIndex = tdata.baseLineDataChunk.startIndex;
			int startIndex2 = tdata.particleChunk.startIndex;
			int startIndex3 = tdata.proxyCommonChunk.startIndex;
			bool useAngleLimit = angleConstraint.useAngleLimit;
			bool useAngleRestoration = angleConstraint.useAngleRestoration;
			float limitstiffness = angleConstraint.limitstiffness;
			float restorationVelocityAttenuation = angleConstraint.restorationVelocityAttenuation;
			float num = math.lerp(1f - angleConstraint.restorationGravityFalloff, 1f, tdata.gravityDot);
			int num2 = tdata.baseLineChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				int num4 = baseLineStartDataIndices[num2];
				int num5 = baseLineDataCounts[num2];
				int num6 = num4 + startIndex;
				int num7 = 0;
				while (num7 < num5)
				{
					int num8 = baseLineData[num6];
					int index = startIndex2 + num8;
					int index2 = startIndex3 + num8;
					float3 x = nextPosArray[index];
					float3 float5 = stepBasicPositionBuffer[index];
					quaternion b = (rotationBufferArray[index] = stepBasicRotationBuffer[index]);
					if (num7 > 0)
					{
						int index3 = vertexParentIndices[index2] + startIndex2;
						float3 y = nextPosArray[index3];
						float3 float6 = stepBasicPositionBuffer[index3];
						quaternion q = stepBasicRotationBuffer[index3];
						if (useAngleLimit)
						{
							float num9 = math.distance(x, y);
							float3 float7 = float5 - float6;
							float num10 = math.length(float7);
							if (num9 < 1E-08f || num10 < 1E-08f)
							{
								lengthBufferArray[index] = 0f;
								localPosBufferArray[index] = 0;
								localRotBufferArray[index] = quaternion.identity;
							}
							else
							{
								float3 v = float7 / num10;
								quaternion obj = math.inverse(q);
								float3 value = math.mul(obj, v);
								quaternion value2 = math.mul(obj, b);
								lengthBufferArray[index] = num9;
								localPosBufferArray[index] = value;
								localRotBufferArray[index] = value2;
							}
						}
						if (useAngleRestoration)
						{
							float3 value3 = float5 - float6;
							restorationVectorBufferArray[index] = value3;
						}
					}
					num7++;
					num6++;
				}
				for (int i = 0; i < 3; i++)
				{
					float t = (float)i / 2f;
					float num11 = 0.4f;
					float num12 = math.lerp(0.1f, 0.5f, t);
					num6 = num4 + startIndex;
					int num13 = 0;
					while (num13 < num5)
					{
						int num14 = baseLineData[num6];
						int index4 = startIndex2 + num14;
						int index5 = startIndex3 + num14;
						float3 float8 = nextPosArray[index4];
						float time = vertexDepths[index5];
						VertexAttribute vertexAttribute = attributes[index5];
						float num15 = MathUtility.CalcInverseMass(frictionArray[index4]);
						if (vertexAttribute.IsMove())
						{
							int index6 = vertexParentIndices[index5] + startIndex2;
							int index7 = vertexParentIndices[index5] + startIndex3;
							float3 float9 = nextPosArray[index6];
							VertexAttribute vertexAttribute2 = attributes[index7];
							float num16 = MathUtility.CalcInverseMass(frictionArray[index6]);
							if (useAngleLimit)
							{
								quaternion quaternion3 = rotationBufferArray[index6];
								float3 v2 = localPosBufferArray[index4];
								quaternion b2 = localRotBufferArray[index4];
								float3 v3 = float8 - float9;
								float num17 = math.length(v3);
								if (!(num17 < 1E-08f))
								{
									float3 v4 = math.mul(quaternion3, v2);
									float num18 = math.length(v4);
									if (num18 < 1E-08f)
									{
										float3 float10 = float9 - float8;
										nextPosArray[index4] = float9;
										velocityPosArray[index4] += float10;
										rotationBufferArray[index4] = math.mul(quaternion3, b2);
									}
									else
									{
										v3 /= num17;
										v4 /= num18;
										float num19 = lengthBufferArray[index4];
										num17 = math.lerp(num17, num19, 0.5f);
										if (!(num19 < 1E-08f) && !(num17 < 1E-08f))
										{
											v3 *= num17;
											float num20 = math.radians(angleConstraint.limitCurveData.MC2EvaluateCurve(time));
											float num21 = MathUtility.Angle(in v3, in v4);
											float3 outdir = v3;
											if (num21 > num20)
											{
												float maxAngle = math.lerp(num21, num20, limitstiffness);
												MathUtility.ClampAngle(in v3, in v4, maxAngle, out outdir);
											}
											float3 obj2 = float9 + v3 * num11;
											float3 float11 = obj2 - outdir * num11;
											float3 obj3 = obj2 + outdir * (1f - num11);
											float3 float12 = float11 - float9;
											float3 float13 = obj3 - float8;
											float13 *= num15;
											float12 *= num16;
											if (vertexAttribute.IsMove())
											{
												float8 += float13;
												nextPosArray[index4] = float8;
												velocityPosArray[index4] += float13 * 0.9f;
											}
											if (vertexAttribute2.IsMove())
											{
												float9 += float12;
												nextPosArray[index6] = float9;
												velocityPosArray[index6] += float12 * 0.9f;
											}
											v3 = float8 - float9;
											num17 = math.length(v3);
											if (!(num17 < 1E-08f))
											{
												v3 /= num17;
												quaternion b3 = math.mul(quaternion3, b2);
												b3 = math.mul(MathUtility.FromToRotationWithoutNormalize(in v4, in v3), b3);
												rotationBufferArray[index4] = b3;
											}
										}
									}
								}
							}
							if (useAngleRestoration)
							{
								float3 float14 = restorationVectorBufferArray[index4];
								float num22 = math.length(float14);
								if (num22 < 1E-08f)
								{
									float3 float15 = float9 - float8;
									nextPosArray[index4] = float9;
									velocityPosArray[index4] += float15;
								}
								else
								{
									float3 float16 = float8 - float9;
									float num23 = math.length(float16);
									if (!(num23 < 1E-08f))
									{
										float num24 = angleConstraint.restorationStiffness.MC2EvaluateCurveClamp01(time);
										num24 = math.saturate(num24 * simulationPower.w);
										num24 *= num;
										float3 float17 = math.mul(MathUtility.FromToRotationWithoutNormalize(float16 / num23, float14 / num22, num24), float16);
										float3 obj4 = float9 + float16 * num12;
										float3 float18 = obj4 - float17 * num12;
										float3 obj5 = obj4 + float17 * (1f - num12);
										float3 float19 = float18 - float9;
										float3 float20 = obj5 - float8;
										float19 *= num15;
										float20 *= num16;
										if (vertexAttribute.IsMove())
										{
											float8 += float20;
											nextPosArray[index4] = float8;
											velocityPosArray[index4] += float20 * restorationVelocityAttenuation;
										}
										if (vertexAttribute2.IsMove())
										{
											float9 += float19;
											nextPosArray[index6] = float9;
											velocityPosArray[index6] += float19 * restorationVelocityAttenuation;
										}
									}
								}
							}
						}
						num13++;
						num6++;
					}
				}
				num3++;
				num2++;
			}
			num2 = tdata.baseLineChunk.startIndex + chunk.startIndex;
			int num25 = 0;
			while (num25 < chunk.dataLength)
			{
				ushort num26 = baseLineStartDataIndices[num2];
				int num27 = baseLineDataCounts[num2];
				int num28 = num26 + startIndex;
				int num29 = 0;
				while (num29 < num27)
				{
					int num30 = baseLineData[num28];
					int index8 = startIndex2 + num30;
					lengthBufferArray[index8] = 0f;
					localPosBufferArray[index8] = 0;
					restorationVectorBufferArray[index8] = 0;
					num29++;
					num28++;
				}
				num25++;
				num2++;
			}
		}
	}
}
