using System;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast
{
	public static class Mathematics
	{
		public static void Decompose(this Matrix4x4 m, out Vector3 translation, out Quaternion rotation, out Vector3 scale)
		{
			translation = new Vector3(m.m03, m.m13, m.m23);
			new float3x3(m.m00, m.m01, m.m02, m.m10, m.m11, m.m12, m.m20, m.m21, m.m22).Decompose(out var rotation2, out var scale2);
			rotation = rotation2;
			scale = new Vector3(scale2.x, scale2.y, scale2.z);
		}

		public static void Decompose(this float4x4 m, out float3 translation, out quaternion rotation, out float3 scale)
		{
			new float3x3(m.c0.xyz, m.c1.xyz, m.c2.xyz).Decompose(out rotation, out scale);
			translation = m.c3.xyz;
		}

		private static void Decompose(this float3x3 m, out quaternion rotation, out float3 scale)
		{
			float num = math.length(m.c0);
			float num2 = math.length(m.c1);
			float num3 = math.length(m.c2);
			float3x3 m2 = default(float3x3);
			m2.c0 = m.c0 / num;
			m2.c1 = m.c1 / num2;
			m2.c2 = m.c2 / num3;
			scale.x = num;
			scale.y = num2;
			scale.z = num3;
			if (m2.IsNegative())
			{
				m2 *= -1f;
				scale *= -1f;
			}
			m2.c0 = math.normalize(m2.c0);
			m2.c1 = math.normalize(m2.c1);
			m2.c2 = math.normalize(m2.c2);
			rotation = new quaternion(m2);
		}

		private static bool IsNegative(this float3x3 m)
		{
			return math.dot(math.cross(m.c0, m.c1), m.c2) < 0f;
		}

		public static float Normalize(float2 input, out float2 output)
		{
			float num = math.length(input);
			output = input / num;
			return num;
		}

		[Obsolete("Use Decompose overload with rotation parameter of type quaternion.")]
		public static void Decompose(this float4x4 m, out float3 translation, out float4 rotation, out float3 scale)
		{
			m.Decompose(out translation, out quaternion rotation2, out scale);
			rotation = rotation2.value;
		}
	}
}
