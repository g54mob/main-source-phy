using System.Text;
using GLTFast.Schema;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast
{
	internal static class AnimationUtils
	{
		private const float k_TimeEpsilon = 1E-05f;

		public static void AddTranslationCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<Vector3> translations, InterpolationType interpolationType)
		{
			NativeArray<float3> values = translations.Reinterpret<float3>();
			AddVec3Curves(clip, animationPath, "localPosition.", times, values, interpolationType);
		}

		public static void AddScaleCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<Vector3> translations, InterpolationType interpolationType)
		{
			NativeArray<float3> values = translations.Reinterpret<float3>();
			AddVec3Curves(clip, animationPath, "localScale.", times, values, interpolationType);
		}

		public static void AddRotationCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<Quaternion> quaternions, InterpolationType interpolationType)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			AnimationCurve animationCurve2 = new AnimationCurve();
			AnimationCurve animationCurve3 = new AnimationCurve();
			AnimationCurve animationCurve4 = new AnimationCurve();
			NativeArray<quaternion> nativeArray = quaternions.Reinterpret<quaternion>();
			switch (interpolationType)
			{
			case InterpolationType.Step:
			{
				for (int k = 0; k < times.Length; k++)
				{
					float time2 = times[k];
					quaternion quaternion8 = nativeArray[k];
					animationCurve.AddKey(new Keyframe(time2, quaternion8.value.x, float.PositiveInfinity, 0f));
					animationCurve2.AddKey(new Keyframe(time2, quaternion8.value.y, float.PositiveInfinity, 0f));
					animationCurve3.AddKey(new Keyframe(time2, quaternion8.value.z, float.PositiveInfinity, 0f));
					animationCurve4.AddKey(new Keyframe(time2, quaternion8.value.w, float.PositiveInfinity, 0f));
				}
				break;
			}
			case InterpolationType.CubicSpline:
			{
				for (int j = 0; j < times.Length; j++)
				{
					float time = times[j];
					quaternion quaternion5 = nativeArray[j * 3];
					quaternion quaternion6 = nativeArray[j * 3 + 1];
					quaternion quaternion7 = nativeArray[j * 3 + 2];
					animationCurve.AddKey(new Keyframe(time, quaternion6.value.x, quaternion5.value.x, quaternion7.value.x, 0.5f, 0.5f));
					animationCurve2.AddKey(new Keyframe(time, quaternion6.value.y, quaternion5.value.y, quaternion7.value.y, 0.5f, 0.5f));
					animationCurve3.AddKey(new Keyframe(time, quaternion6.value.z, quaternion5.value.z, quaternion7.value.z, 0.5f, 0.5f));
					animationCurve4.AddKey(new Keyframe(time, quaternion6.value.w, quaternion5.value.w, quaternion7.value.w, 0.5f, 0.5f));
				}
				break;
			}
			default:
			{
				float num = times[0];
				quaternion a = nativeArray[0];
				quaternion quaternion2 = new quaternion(new float4(0f));
				quaternion quaternion4 = default(quaternion);
				for (int i = 1; i < times.Length; i++)
				{
					float num2 = times[i];
					quaternion quaternion3 = nativeArray[i];
					if (!(num >= num2))
					{
						if (math.dot(a, quaternion3) < 0f)
						{
							quaternion3.value = -quaternion3.value;
						}
						float num3 = num2 - num;
						float4 float5 = quaternion3.value - a.value;
						if (num3 < 1E-05f)
						{
							quaternion4.value.x = (((float5.x < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
							quaternion4.value.y = (((float5.y < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
							quaternion4.value.z = (((float5.z < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
							quaternion4.value.w = (((float5.w < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
						}
						else
						{
							quaternion4 = float5 / num3;
						}
						animationCurve.AddKey(new Keyframe(num, a.value.x, quaternion2.value.x, quaternion4.value.x));
						animationCurve2.AddKey(new Keyframe(num, a.value.y, quaternion2.value.y, quaternion4.value.y));
						animationCurve3.AddKey(new Keyframe(num, a.value.z, quaternion2.value.z, quaternion4.value.z));
						animationCurve4.AddKey(new Keyframe(num, a.value.w, quaternion2.value.w, quaternion4.value.w));
						quaternion2 = quaternion4;
						num = num2;
						a = quaternion3;
					}
				}
				animationCurve.AddKey(new Keyframe(num, a.value.x, quaternion2.value.x, 0f));
				animationCurve2.AddKey(new Keyframe(num, a.value.y, quaternion2.value.y, 0f));
				animationCurve3.AddKey(new Keyframe(num, a.value.z, quaternion2.value.z, 0f));
				animationCurve4.AddKey(new Keyframe(num, a.value.w, quaternion2.value.w, 0f));
				break;
			}
			}
			clip.SetCurve(animationPath, typeof(Transform), "localRotation.x", animationCurve);
			clip.SetCurve(animationPath, typeof(Transform), "localRotation.y", animationCurve2);
			clip.SetCurve(animationPath, typeof(Transform), "localRotation.z", animationCurve3);
			clip.SetCurve(animationPath, typeof(Transform), "localRotation.w", animationCurve4);
		}

		public static string CreateAnimationPath(int nodeIndex, string[] nodeNames, int[] parentIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			do
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Insert(0, '/');
				}
				stringBuilder.Insert(0, nodeNames[nodeIndex]);
				nodeIndex = parentIndex[nodeIndex];
			}
			while (nodeIndex >= 0);
			return stringBuilder.ToString();
		}

		public static void AddMorphTargetWeightCurves(AnimationClip clip, string animationPath, NativeArray<float> times, NativeArray<float> values, InterpolationType interpolationType, string[] morphTargetNames = null)
		{
			int num;
			if (morphTargetNames == null)
			{
				num = values.Length / times.Length;
				if (interpolationType == InterpolationType.CubicSpline)
				{
					num /= 3;
				}
			}
			else
			{
				num = morphTargetNames.Length;
			}
			for (int i = 0; i < num; i++)
			{
				string propertyPrefix = ((morphTargetNames == null) ? i.ToString() : morphTargetNames[i]);
				AddScalarCurve(clip, animationPath, propertyPrefix, i, num, times, values, interpolationType);
			}
		}

		private static void AddVec3Curves(AnimationClip clip, string animationPath, string propertyPrefix, NativeArray<float> times, NativeArray<float3> values, InterpolationType interpolationType)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			AnimationCurve animationCurve2 = new AnimationCurve();
			AnimationCurve animationCurve3 = new AnimationCurve();
			switch (interpolationType)
			{
			case InterpolationType.Step:
			{
				for (int k = 0; k < times.Length; k++)
				{
					float time2 = times[k];
					float3 float13 = values[k];
					animationCurve.AddKey(new Keyframe(time2, float13.x, float.PositiveInfinity, 0f));
					animationCurve2.AddKey(new Keyframe(time2, float13.y, float.PositiveInfinity, 0f));
					animationCurve3.AddKey(new Keyframe(time2, float13.z, float.PositiveInfinity, 0f));
				}
				break;
			}
			case InterpolationType.CubicSpline:
			{
				for (int j = 0; j < times.Length; j++)
				{
					float time = times[j];
					float3 float10 = values[j * 3];
					float3 float11 = values[j * 3 + 1];
					float3 float12 = values[j * 3 + 2];
					animationCurve.AddKey(new Keyframe(time, float11.x, float10.x, float12.x, 0.5f, 0.5f));
					animationCurve2.AddKey(new Keyframe(time, float11.y, float10.y, float12.y, 0.5f, 0.5f));
					animationCurve3.AddKey(new Keyframe(time, float11.z, float10.z, float12.z, 0.5f, 0.5f));
				}
				break;
			}
			default:
			{
				float num = times[0];
				float3 float5 = values[0];
				float3 float6 = new float3(0f);
				float3 float9 = default(float3);
				for (int i = 1; i < times.Length; i++)
				{
					float num2 = times[i];
					float3 float7 = values[i];
					if (!(num >= num2))
					{
						float num3 = num2 - num;
						float3 float8 = float7 - float5;
						if (num3 < 1E-05f)
						{
							float9.x = (((float8.x < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
							float9.y = (((float8.y < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
							float9.z = (((float8.z < 0f) ^ (num3 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity);
						}
						else
						{
							float9 = float8 / num3;
						}
						animationCurve.AddKey(new Keyframe(num, float5.x, float6.x, float9.x));
						animationCurve2.AddKey(new Keyframe(num, float5.y, float6.y, float9.y));
						animationCurve3.AddKey(new Keyframe(num, float5.z, float6.z, float9.z));
						float6 = float9;
						num = num2;
						float5 = float7;
					}
				}
				animationCurve.AddKey(new Keyframe(num, float5.x, float6.x, 0f));
				animationCurve2.AddKey(new Keyframe(num, float5.y, float6.y, 0f));
				animationCurve3.AddKey(new Keyframe(num, float5.z, float6.z, 0f));
				break;
			}
			}
			clip.SetCurve(animationPath, typeof(Transform), propertyPrefix + "x", animationCurve);
			clip.SetCurve(animationPath, typeof(Transform), propertyPrefix + "y", animationCurve2);
			clip.SetCurve(animationPath, typeof(Transform), propertyPrefix + "z", animationCurve3);
		}

		private static void AddScalarCurve(AnimationClip clip, string animationPath, string propertyPrefix, int curveIndex, int valueStride, NativeArray<float> times, NativeArray<float> values, InterpolationType interpolationType)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			switch (interpolationType)
			{
			case InterpolationType.Step:
			{
				for (int k = 0; k < times.Length; k++)
				{
					float time2 = times[k];
					int index2 = k * valueStride + curveIndex;
					float value2 = values[index2];
					animationCurve.AddKey(new Keyframe(time2, value2, float.PositiveInfinity, 0f));
				}
				break;
			}
			case InterpolationType.CubicSpline:
			{
				for (int j = 0; j < times.Length; j++)
				{
					float time = times[j];
					int num8 = j * valueStride + curveIndex;
					float inTangent2 = values[num8 * 3];
					float value = values[num8 * 3 + 1];
					float outTangent = values[num8 * 3 + 2];
					animationCurve.AddKey(new Keyframe(time, value, inTangent2, outTangent, 0.5f, 0.5f));
				}
				break;
			}
			default:
			{
				float num = times[0];
				float num2 = values[curveIndex];
				float inTangent = 0f;
				for (int i = 1; i < times.Length; i++)
				{
					float num3 = times[i];
					int index = i * valueStride + curveIndex;
					float num4 = values[index];
					if (!(num >= num3))
					{
						float num5 = num3 - num;
						float num6 = num4 - num2;
						float num7 = ((!(num5 < 1E-05f)) ? (num6 / num5) : (((num6 < 0f) ^ (num5 < 0f)) ? float.NegativeInfinity : float.PositiveInfinity));
						animationCurve.AddKey(new Keyframe(num, num2, inTangent, num7));
						inTangent = num7;
						num = num3;
						num2 = num4;
					}
				}
				animationCurve.AddKey(new Keyframe(num, num2, inTangent, 0f));
				break;
			}
			}
			clip.SetCurve(animationPath, typeof(SkinnedMeshRenderer), "blendShape." + propertyPrefix, animationCurve);
		}
	}
}
