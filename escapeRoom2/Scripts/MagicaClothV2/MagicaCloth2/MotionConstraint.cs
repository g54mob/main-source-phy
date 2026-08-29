using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class MotionConstraint : IDisposable
	{
		[Serializable]
		public class SerializeData : IDataValidate
		{
			public bool useMaxDistance;

			public CurveSerializeData maxDistance;

			public bool useBackstop;

			[Range(0.1f, 10f)]
			public float backstopRadius;

			public CurveSerializeData backstopDistance;

			[Range(0f, 1f)]
			public float stiffness;

			public SerializeData()
			{
				useMaxDistance = false;
				maxDistance = new CurveSerializeData(0.3f);
				useBackstop = false;
				backstopRadius = 10f;
				backstopDistance = new CurveSerializeData(0f);
				stiffness = 1f;
			}

			public void DataValidate()
			{
				maxDistance.DataValidate(0f, 5f);
				backstopRadius = Mathf.Clamp(backstopRadius, 0f, 10f);
				backstopDistance.DataValidate(0f, 1f);
				stiffness = Mathf.Clamp01(stiffness);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					useMaxDistance = useMaxDistance,
					maxDistance = maxDistance.Clone(),
					useBackstop = useBackstop,
					backstopRadius = backstopRadius,
					backstopDistance = backstopDistance.Clone(),
					stiffness = stiffness
				};
			}
		}

		public struct MotionConstraintParams
		{
			public bool useMaxDistance;

			public float4x4 maxDistanceCurveData;

			public bool useBackstop;

			public float backstopRadius;

			public float4x4 backstopDistanceCurveData;

			public float stiffness;

			public void Convert(SerializeData sdata, ClothProcess.ClothType clothType)
			{
				useMaxDistance = clothType != ClothProcess.ClothType.BoneSpring && sdata.useMaxDistance;
				maxDistanceCurveData = sdata.maxDistance.ConvertFloatArray();
				useBackstop = clothType != ClothProcess.ClothType.BoneSpring && sdata.useBackstop;
				backstopRadius = sdata.backstopRadius;
				backstopDistanceCurveData = sdata.backstopDistance.ConvertFloatArray();
				stiffness = sdata.stiffness;
			}
		}

		public void Dispose()
		{
		}

		internal static void SolverConstraint(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> vertexDepths, ref NativeArray<float3> basePosArray, ref NativeArray<quaternion> baseRotArray, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float> frictionArray, ref NativeArray<float3> collisionNormalArray)
		{
			if (!param.motionConstraint.useMaxDistance && !param.motionConstraint.useBackstop)
			{
				return;
			}
			float stiffness = param.motionConstraint.stiffness;
			float backstopRadius = param.motionConstraint.backstopRadius;
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				VertexAttribute vertexAttribute = attributes[num2];
				if (vertexAttribute.IsMove())
				{
					float3 float5 = nextPosArray[num];
					float3 float6 = basePosArray[num];
					float num4 = vertexDepths[num2];
					if (vertexAttribute.IsMotion())
					{
						float3 float7 = float5;
						float num5 = math.max(param.radiusCurveData.MC2EvaluateCurve(num4), 0.0001f) * 1f;
						num4 *= num4;
						quaternion q = baseRotArray[num];
						float3 v = math.up();
						switch (param.normalAxis)
						{
						case ClothNormalAxis.Right:
							v = math.right();
							break;
						case ClothNormalAxis.Up:
							v = math.up();
							break;
						case ClothNormalAxis.Forward:
							v = math.forward();
							break;
						case ClothNormalAxis.InverseRight:
							v = -math.right();
							break;
						case ClothNormalAxis.InverseUp:
							v = -math.up();
							break;
						case ClothNormalAxis.InverseForward:
							v = -math.forward();
							break;
						}
						v = math.mul(q, v);
						if (param.motionConstraint.useMaxDistance)
						{
							float maxlength = param.motionConstraint.maxDistanceCurveData.MC2EvaluateCurve(num4);
							float3 float8 = float6;
							float3 float9 = MathUtility.ClampVector(float5 - float8, maxlength);
							float5 = float8 + float9;
						}
						if (param.motionConstraint.useBackstop)
						{
							float num6 = param.motionConstraint.backstopDistanceCurveData.MC2EvaluateCurve(num4);
							if (backstopRadius > 0f)
							{
								float3 float10 = float6 + -v * (num6 + backstopRadius);
								float3 float11 = float5 - float10;
								float num7 = math.length(float11);
								if (num7 > 1E-08f && num7 < backstopRadius + num5)
								{
									float3 float12 = float11 / num7;
									if (num7 < backstopRadius)
									{
										float5 = float10 + float12 * backstopRadius;
									}
								}
							}
						}
						float5 = (nextPosArray[num] = math.lerp(float7, float5, stiffness));
						float3 float14 = float5 - float7;
						velocityPosArray[num] += float14 * 0.95f;
					}
				}
				num3++;
				num++;
				num2++;
			}
		}
	}
}
