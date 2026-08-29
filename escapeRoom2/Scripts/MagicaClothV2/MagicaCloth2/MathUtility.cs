using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public static class MathUtility
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp1(float a)
		{
			return math.clamp(a, -1f, 1f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 Project(in float3 v, in float3 n)
		{
			return math.dot(v, n) * n;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ProjectOnPlane(in float3 v, in float3 n)
		{
			return v - math.dot(v, n) * n;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(in float3 v1, in float3 v2)
		{
			float num = math.length(v1);
			float num2 = math.length(v2);
			return math.acos(Clamp1(math.dot(v1, v2) / (num * num2)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClampVector(float3 v, float minlength, float maxlength)
		{
			float num = math.length(v);
			if (num > 1E-09f)
			{
				if (num > maxlength)
				{
					v *= maxlength / num;
				}
				else if (num < minlength)
				{
					v *= minlength / num;
				}
			}
			return v;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClampVector(float3 v, float maxlength)
		{
			float num = math.length(v);
			if (num > 1E-09f && num > maxlength)
			{
				v *= maxlength / num;
			}
			return v;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClampDistance(float3 from, float3 to, float maxlength)
		{
			float num = math.distance(from, to);
			if (num <= maxlength)
			{
				return to;
			}
			float t = maxlength / num;
			return math.lerp(from, to, t);
		}

		public static bool ClampAngle(in float3 dir, in float3 basedir, float maxAngle, out float3 outdir)
		{
			float3 x = math.normalize(dir);
			float3 y = math.normalize(basedir);
			float num = Clamp1(math.dot(x, y));
			float num2 = math.acos(num);
			if (num2 <= maxAngle)
			{
				outdir = dir;
				return false;
			}
			float num3 = (num2 - maxAngle) / num2;
			float3 x2 = math.cross(x, y);
			if (math.abs(1f + num) < 1E-06f)
			{
				num2 = MathF.PI;
				x2 = ((!(x.x > x.y) || !(x.x > x.z)) ? math.cross(x, new float3(1f, 0f, 0f)) : math.cross(x, new float3(0f, 1f, 0f)));
			}
			else if (math.abs(1f - num) < 1E-06f)
			{
				outdir = dir;
				return false;
			}
			quaternion q = quaternion.AxisAngle(math.normalize(x2), num2 * num3);
			outdir = math.mul(q, dir);
			return true;
		}

		public static quaternion FromToRotation(in float3 from, in float3 to, float t = 1f)
		{
			float3 x = math.normalize(from);
			float3 y = math.normalize(to);
			float num = Clamp1(math.dot(x, y));
			float num2 = math.acos(num);
			float3 x2 = math.cross(x, y);
			if (math.abs(1f + num) < 1E-06f)
			{
				num2 = MathF.PI;
				x2 = ((!(x.x > x.y) || !(x.x > x.z)) ? math.cross(x, new float3(1f, 0f, 0f)) : math.cross(x, new float3(0f, 1f, 0f)));
			}
			else if (math.abs(1f - num) < 1E-06f)
			{
				return quaternion.identity;
			}
			return quaternion.AxisAngle(math.normalize(x2), num2 * t);
		}

		public static quaternion FromToRotationWithoutNormalize(in float3 v1, in float3 v2, float t = 1f)
		{
			float num = Clamp1(math.dot(v1, v2));
			float num2 = math.acos(num);
			float3 x = math.cross(v1, v2);
			if (math.abs(1f + num) < 1E-06f)
			{
				num2 = MathF.PI;
				x = ((!(v1.x > v1.y) || !(v1.x > v1.z)) ? math.cross(v1, new float3(1f, 0f, 0f)) : math.cross(v1, new float3(0f, 1f, 0f)));
			}
			else if (math.abs(1f - num) < 1E-06f)
			{
				return quaternion.identity;
			}
			return quaternion.AxisAngle(math.normalize(x), num2 * t);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion FromToRotation(in quaternion from, in quaternion to)
		{
			return math.mul(to, math.inverse(from));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(in quaternion a, in quaternion b)
		{
			float num = math.dot(a, b);
			if (math.abs(num) < 0.9999f)
			{
				float num2 = math.acos(Clamp1(num)) * 2f;
				if (!(num2 > MathF.PI))
				{
					return num2;
				}
				return MathF.PI * 2f - num2;
			}
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion ClampAngle(quaternion from, quaternion to, float maxAngle)
		{
			float num = Angle(in from, in to);
			if (num <= maxAngle)
			{
				return to;
			}
			float t = maxAngle / num;
			return math.slerp(from, to, t);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion ToRotation(in float3 nor, in float3 tan)
		{
			return quaternion.LookRotation(tan, nor);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToNormalTangent(in quaternion rot, out float3 nor, out float3 tan)
		{
			nor = math.mul(rot, math.up());
			tan = math.mul(rot, math.forward());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ToNormal(in quaternion rot)
		{
			return math.mul(rot, math.up());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ToTangent(in quaternion rot)
		{
			return math.mul(rot, math.forward());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ToBinormal(in quaternion rot)
		{
			return math.mul(rot, math.right());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 Binormal(in float3 nor, in float3 tan)
		{
			return math.cross(nor, tan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 AxisToEuler(in float3 axis)
		{
			float y = math.atan2(axis.x, axis.z);
			return new float3(math.atan2(0f - axis.y, math.length(axis - new float3(0f, axis.y, 0f))), y, 0f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion AxisQuaternion(float3 dir)
		{
			return quaternion.Euler(AxisToEuler(in dir));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToAngleAxis(in quaternion q, out float angle, out float3 axis)
		{
			float num = ((math.abs(q.value.w) < 0.9999f) ? math.acos(q.value.w) : 0f);
			angle = 2f * num;
			float num2 = math.sin(num);
			if (math.abs(num2) < 1E-06f)
			{
				axis = 0;
			}
			else
			{
				axis = q.value.xyz / num2;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ClosestPtPointSegmentRatio(in float3 c, in float3 a, in float3 b)
		{
			float3 float5 = b - a;
			float num = math.dot(float5, float5);
			if (num == 0f)
			{
				return 0f;
			}
			return math.saturate(math.dot(c - a, float5) / num);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ClosestPtPointSegmentRatioNoClamp(float3 c, float3 a, float3 b)
		{
			float3 float5 = b - a;
			float num = math.dot(float5, float5);
			return math.dot(c - a, float5) / num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClosestPtPointSegment(float3 c, float3 a, float3 b)
		{
			float3 float5 = b - a;
			float num = math.dot(float5, float5);
			float x = math.dot(c - a, float5) / num;
			x = math.saturate(x);
			return a + x * float5;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClosestPtPointSegmentNoClamp(float3 c, float3 a, float3 b)
		{
			float3 float5 = b - a;
			float num = math.dot(float5, float5);
			float num2 = math.dot(c - a, float5) / num;
			return a + num2 * float5;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ClosestPtSegmentSegment(in float3 p1, in float3 q1, in float3 p2, in float3 q2, out float s, out float t, out float3 c1, out float3 c2)
		{
			float3 float5 = q1 - p1;
			float3 float6 = q2 - p2;
			float3 y = p1 - p2;
			float num = math.dot(float5, float5);
			float num2 = math.dot(float6, float6);
			float num3 = math.dot(float6, y);
			if (num <= 1E-08f && num2 <= 1E-08f)
			{
				s = (t = 0f);
				c1 = p1;
				c2 = p2;
				return math.dot(c1 - c2, c1 - c2);
			}
			if (num <= 1E-08f)
			{
				s = 0f;
				t = math.saturate(num3 / num2);
			}
			else
			{
				float num4 = math.dot(float5, y);
				if (num2 <= 1E-08f)
				{
					t = 0f;
					s = math.saturate((0f - num4) / num);
				}
				else
				{
					float num5 = math.dot(float5, float6);
					float num6 = num * num2 - num5 * num5;
					if (num6 != 0f)
					{
						s = math.saturate((num5 * num3 - num4 * num2) / num6);
					}
					else
					{
						s = 0f;
					}
					t = (num5 * s + num3) / num2;
					if (t < 0f)
					{
						t = 0f;
						s = math.saturate((0f - num4) / num);
					}
					else if (t > 1f)
					{
						t = 1f;
						s = math.saturate((num5 - num4) / num);
					}
				}
			}
			c1 = p1 + float5 * s;
			c2 = p2 + float6 * t;
			return math.dot(c1 - c2, c1 - c2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ClosestPtSegmentSegment2(in float3 p1, in float3 q1, in float3 p2, in float3 q2, out float s, out float t)
		{
			float3 float5 = q1 - p1;
			float3 float6 = q2 - p2;
			float3 y = p1 - p2;
			float num = math.dot(float5, float5);
			float num2 = math.dot(float6, float6);
			float num3 = math.dot(float6, y);
			if (num <= 1E-08f && num2 <= 1E-08f)
			{
				s = (t = 0f);
				return;
			}
			if (num <= 1E-08f)
			{
				s = 0f;
				t = math.saturate(num3 / num2);
				return;
			}
			float num4 = math.dot(float5, y);
			if (num2 <= 1E-08f)
			{
				t = 0f;
				s = math.saturate((0f - num4) / num);
				return;
			}
			float num5 = math.dot(float5, float6);
			float num6 = num * num2 - num5 * num5;
			if (num6 != 0f)
			{
				s = math.saturate((num5 * num3 - num4 * num2) / num6);
			}
			else
			{
				s = 0f;
			}
			t = (num5 * s + num3) / num2;
			if (t < 0f)
			{
				t = 0f;
				s = math.saturate((0f - num4) / num);
			}
			else if (t > 1f)
			{
				t = 1f;
				s = math.saturate((num5 - num4) / num);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ClosestPtPointTriangle(in float3 p, in float3 a, in float3 b, in float3 c, out float3 uvw)
		{
			uvw = 0;
			float num = 0f;
			float num2 = 0f;
			float3 float5 = b - a;
			float3 float6 = c - a;
			float3 y = p - a;
			float num3 = math.dot(float5, y);
			float num4 = math.dot(float6, y);
			if (num3 <= 0f && num4 <= 0f)
			{
				uvw.x = 1f;
				return a;
			}
			float3 y2 = p - b;
			float num5 = math.dot(float5, y2);
			float num6 = math.dot(float6, y2);
			if (num5 >= 0f && num6 <= num5)
			{
				uvw.y = 1f;
				return b;
			}
			float num7 = num3 * num6 - num5 * num4;
			if (num7 <= 0f && num3 >= 0f && num5 <= 0f)
			{
				num = num3 / (num3 - num5);
				uvw = new float3(1f - num, num, 0f);
				return a + num * float5;
			}
			float3 y3 = p - c;
			float num8 = math.dot(float5, y3);
			float num9 = math.dot(float6, y3);
			if (num9 >= 0f && num8 <= num9)
			{
				uvw.z = 1f;
				return c;
			}
			float num10 = num8 * num4 - num3 * num9;
			if (num10 <= 0f && num4 >= 0f && num9 <= 0f)
			{
				num2 = num4 / (num4 - num9);
				uvw = new float3(1f - num2, 0f, num2);
				return a + num2 * float6;
			}
			float num11 = num5 * num9 - num8 * num6;
			if (num11 <= 0f && num6 - num5 >= 0f && num8 - num9 >= 0f)
			{
				float num12 = num6 - num5 + (num8 - num9);
				num2 = (num6 - num5) / num12;
				uvw = new float3(0f, 1f - num2, num2);
				return b + num2 * (c - b);
			}
			float num13 = num11 + num10 + num7;
			float num14 = 1f / num13;
			num = num10 * num14;
			num2 = num7 * num14;
			uvw = new float3(1f - num - num2, num, num2);
			return a + float5 * num + float6 * num2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool PointInTriangleUVW(float3 uvw)
		{
			return math.all(uvw);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TriangleCenter(in float3 p0, in float3 p1, in float3 p2)
		{
			return (p0 + p1 + p2) / 3f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TriangleNormal(in float3 p0, in float3 p1, in float3 p2)
		{
			return math.normalize(math.cross(p1 - p0, p2 - p0));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TriangleArea(in float3 p0, in float3 p1, in float3 p2)
		{
			return math.length(math.cross(p1 - p0, p2 - p0));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSafeTriangle(in float3 p0, in float3 p1, in float3 p2)
		{
			return TriangleArea(in p0, in p1, in p2) > 1E-06f;
		}

		public static float3 TriangleTangent(in float3 p0, in float3 p1, in float3 p2, in float2 uv0, in float2 uv1, in float2 uv2)
		{
			float3 float5 = p1 - p0;
			float3 float6 = p2 - p0;
			float2 float7 = uv1 - uv0;
			float2 float8 = uv2 - uv0;
			float num = float7.x * float8.y - float7.y * float8.x;
			_ = (float3)0;
			if (num == 0f)
			{
				num = 1f;
			}
			float num2 = 1f / num;
			return math.normalizesafe(-(new float3(float5.x * float8.y + float6.x * (0f - float7.y), float5.y * float8.y + float6.y * (0f - float7.y), float5.z * float8.y + float6.z * (0f - float7.y)) * num2), 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TriangleAngle(in float3 v0, in float3 v1, in float3 v2, in float3 v3)
		{
			float3 float5 = v1 - v0;
			float3 x = v2 - v0;
			return Angle(v2: math.cross(float5, v3 - v0), v1: math.cross(x, float5));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceTriangleCenter(float3 p, float3 p0, float3 p1, float3 p2)
		{
			float3 y = (p0 + p1 + p2) / 3f;
			return math.distance(p, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DirectionPointTriangle(float3 p, float3 a, float3 b, float3 c)
		{
			float3 x = b - a;
			float3 y = c - a;
			float3 x2 = p - a;
			float3 y2 = math.cross(x, y);
			return math.sign(math.dot(x2, y2));
		}

		public static int2 GetRestTriangleVertex(int3 tri1, int3 tri2, int2 edge)
		{
			int2 result = -1;
			for (int i = 0; i < 3; i++)
			{
				int num = tri1[i];
				if (num != edge.x && num != edge.x && num != edge.y && num != edge.y)
				{
					result[0] = num;
					break;
				}
			}
			for (int j = 0; j < 3; j++)
			{
				int num2 = tri2[j];
				if (num2 != edge.x && num2 != edge.x && num2 != edge.y && num2 != edge.y)
				{
					result[1] = num2;
					break;
				}
			}
			return result;
		}

		public static int2 GetCommonEdgeFromTrianglePair(int3 tri1, int3 tri2)
		{
			int2 result = 0;
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				if (tri1[i] == tri2.x || tri1[i] == tri2.y || tri1[i] == tri2.z)
				{
					result[num] = tri1[i];
					num++;
				}
			}
			if (num != 2)
			{
				Debug.LogError("Common edge nothing!");
				return 0;
			}
			if (result.x > result.y)
			{
				int x = result.x;
				result.x = result.y;
				result.y = x;
			}
			return result;
		}

		public static int4 GetTrianglePairIndices(int3 tri1, int3 tri2)
		{
			int2 commonEdgeFromTrianglePair = GetCommonEdgeFromTrianglePair(tri1, tri2);
			int4 result = new int4(0, 0, commonEdgeFromTrianglePair.x, commonEdgeFromTrianglePair.y);
			for (int i = 0; i < 3; i++)
			{
				if (tri1[i] != commonEdgeFromTrianglePair.x && tri1[i] != commonEdgeFromTrianglePair.y)
				{
					result[0] = tri1[i];
				}
				if (tri2[i] != commonEdgeFromTrianglePair.x && tri2[i] != commonEdgeFromTrianglePair.y)
				{
					result[1] = tri2[i];
				}
			}
			return result;
		}

		public static int GetUnuseTriangleIndex(int3 tri, int2 edge)
		{
			if (tri.x != edge.x && tri.x != edge.y)
			{
				return tri.x;
			}
			if (tri.y != edge.x && tri.y != edge.y)
			{
				return tri.y;
			}
			if (tri.z != edge.x && tri.z != edge.y)
			{
				return tri.z;
			}
			return -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetTrianglePairAngle(float3 pos0, float3 pos1, float3 pos2, float3 pos3)
		{
			float3 float5 = pos3 - pos2;
			float3 y = pos0 - pos2;
			return Angle(v2: math.cross(pos1 - pos2, float5), v1: math.cross(float5, y));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 FlipTriangle(in int3 tri)
		{
			return tri.xzy;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GetTriangleSphere(float3 pos0, float3 pos1, float3 pos2, out float3 sc, out float sr)
		{
			float3 float5 = math.max(math.max(pos0, pos1), pos2);
			float3 float6 = math.min(math.min(pos0, pos1), pos2);
			sc = (float6 + float5) * 0.5f;
			sr = math.distance(float6, float5) * 0.5f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 LocalToWorldMatrix(in float3 wpos, in quaternion wrot, in float3 wscl)
		{
			return Matrix4x4.TRS(wpos, wrot, wscl);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 WorldToLocalMatrix(in float3 wpos, in quaternion wrot, in float3 wscl)
		{
			return math.inverse(Matrix4x4.TRS(wpos, wrot, wscl));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformPoint(in float3 pos, in float3 wpos, in quaternion wrot, in float3 wscl)
		{
			return math.transform(Matrix4x4.TRS(wpos, wrot, wscl), pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformPoint(in float3 pos, in float4x4 m)
		{
			return math.transform(m, pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformVector(in float3 vec, in float4x4 m)
		{
			return math.mul(m, new float4(vec, 0f)).xyz;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 TransformDirection(in float3 dir, in float4x4 m)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(TransformVector(in dir, in m)) * num;
			}
			return dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion TransformRotation(in quaternion rot, in float4x4 m, in float3 normalTangentFlip)
		{
			ToNormalTangent(in rot, out var nor, out var tan);
			nor = math.mul(m, new float4(nor, 0f)).xyz * normalTangentFlip.y;
			tan = math.mul(m, new float4(tan, 0f)).xyz * normalTangentFlip.z;
			return quaternion.LookRotation(tan, nor);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TransformDistance(in float dist, in float4x4 localToWorldMatrix)
		{
			return math.csum(math.mul(localToWorldMatrix, new float4(dist, dist, dist, 0f)).xyz) / 3f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TransformPositionNormalTangent(in float3 tpos, in quaternion trot, in float3 tscl, ref float3 pos, ref float3 nor, ref float3 tan)
		{
			pos *= tscl;
			pos = math.mul(trot, pos);
			pos += tpos;
			nor = math.mul(trot, nor);
			tan = math.mul(trot, tan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TransformLength(float length, in float4x4 matrix)
		{
			return math.length(math.mul(matrix, new float4(length, length, length, 0f)).xyz) / 1.73205f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformPoint(in float3 pos, in float4x4 worldToLocalMatrix)
		{
			return math.transform(worldToLocalMatrix, pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformPoint(in float3 pos, in float3 wpos, in quaternion wrot, in float3 wscl)
		{
			return math.transform(math.inverse(Matrix4x4.TRS(wpos, wrot, wscl)), pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformVector(in float3 vec, in float4x4 worldToLocalMatrix)
		{
			return math.mul(worldToLocalMatrix, new float4(vec, 0f)).xyz;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformVector(in float3 vec, in quaternion rot)
		{
			return math.mul(math.inverse(rot), vec);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 InverseTransformDirection(in float3 dir, in float4x4 worldToLocalMatrix)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(InverseTransformVector(in dir, in worldToLocalMatrix)) * num;
			}
			return dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Transform(in float4x4 fromLocalToWorldMatrix, in float4x4 toWorldToLocalMatrix)
		{
			return math.mul(toWorldToLocalMatrix, fromLocalToWorldMatrix);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CompareMatrix(in float4x4 m1, in float4x4 m2)
		{
			bool4x4 bool4x5 = m1 == m2;
			if (math.all(bool4x5.c0) && math.all(bool4x5.c1) && math.all(bool4x5.c2))
			{
				return math.all(bool4x5.c3);
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CompareTransform(in float3 pos1, in quaternion rot1, in float3 scl1, in float3 pos2, in quaternion rot2, in float3 scl2)
		{
			if (pos1.Equals(pos2) && rot1.Equals(rot2))
			{
				return scl1.Equals(scl2);
			}
			return false;
		}

		public static bool IntersectSegmentTriangle(in float3 p, in float3 q, float3 a, float3 b, float3 c, bool doubleSide, out float u, out float v, out float w, out float t)
		{
			t = 0f;
			u = 0f;
			v = 0f;
			w = 0f;
			float3 x = b - a;
			float3 float5 = c - a;
			float3 x2 = p - q;
			float3 float6 = math.cross(x, float5);
			float num = math.dot(x2, float6);
			if (math.abs(num) < 1E-09f)
			{
				return false;
			}
			if (!doubleSide)
			{
				if (num <= 0f)
				{
					return false;
				}
			}
			else if (num < 0f)
			{
				float6 = -float6;
				float3 obj = b;
				b = c;
				c = obj;
				x = b - a;
				float5 = c - a;
				num = math.dot(x2, float6);
			}
			float3 float7 = p - a;
			t = math.dot(float7, float6);
			if (t < 0f)
			{
				return false;
			}
			if (t > num)
			{
				return false;
			}
			float3 y = math.cross(x2, float7);
			v = math.dot(float5, y);
			if (v < 0f || v > num)
			{
				return false;
			}
			w = 0f - math.dot(x, y);
			if (w < 0f || v + w > num)
			{
				return false;
			}
			float num2 = 1f / num;
			t *= num2;
			v *= num2;
			w *= num2;
			u = 1f - v - w;
			return true;
		}

		public static bool IntersectSegmentTriangle(in float3 p, in float3 q, float3 a, float3 b, float3 c)
		{
			float3 x = b - a;
			float3 float5 = c - a;
			float3 x2 = p - q;
			float3 float6 = math.cross(x, float5);
			float num = math.dot(x2, float6);
			if (math.abs(num) < 1E-09f)
			{
				return false;
			}
			if (num < 0f)
			{
				float6 = -float6;
				float3 obj = b;
				b = c;
				c = obj;
				x = b - a;
				float5 = c - a;
				num = math.dot(x2, float6);
			}
			float3 float7 = p - a;
			float num2 = math.dot(float7, float6);
			if (num2 < 0f)
			{
				return false;
			}
			if (num2 > num)
			{
				return false;
			}
			float3 y = math.cross(x2, float7);
			float num3 = math.dot(float5, y);
			if (num3 < 0f || num3 > num)
			{
				return false;
			}
			float num4 = 0f - math.dot(x, y);
			if (num4 < 0f || num3 + num4 > num)
			{
				return false;
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IntersectPointPlaneDist(in float3 planePos, in float3 planeDir, in float3 pos, out float3 outPos)
		{
			float3 v = pos - planePos;
			float3 float5 = Project(in v, in planeDir);
			float num = math.length(float5);
			if (math.dot(planeDir, v) < 0f)
			{
				outPos = pos - float5;
				return 0f - num;
			}
			outPos = pos;
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IntersectRaySphere(in float3 p, in float3 d, in float3 sc, in float sr, ref float t, ref float3 q)
		{
			float3 obj = p - sc;
			float num = math.dot(obj, d);
			float num2 = math.dot(obj, obj) - sr * sr;
			if (num2 > 0f && num > 0f)
			{
				return false;
			}
			float num3 = num * num - num2;
			if (num3 < 0f)
			{
				return false;
			}
			t = 0f - num - math.sqrt(num3);
			if (t < 0f)
			{
				t = 0f;
			}
			q = p + t * d;
			return true;
		}

		public static float SqDistPointSegment(Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 vector3 = c - b;
			float num = Vector3.Dot(vector2, vector);
			if (num <= 0f)
			{
				return Vector3.Dot(vector2, vector2);
			}
			float num2 = Vector3.Dot(vector, vector);
			if (num >= num2)
			{
				return Vector3.Dot(vector3, vector3);
			}
			return Vector3.Dot(vector2, vector2) - num * num / num2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(float3 v)
		{
			return math.any(math.isnan(v));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(float4 v)
		{
			return math.any(math.isnan(v));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(quaternion q)
		{
			return math.any(math.isnan(q.value));
		}

		public static float3 ShiftPosition(in float3 oldPos, in float3 oldPivotPosition, in float3 shiftVector, in quaternion shiftRotation)
		{
			float3 v = oldPos - oldPivotPosition;
			v = math.mul(shiftRotation, v);
			v += shiftVector;
			return oldPivotPosition + v;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcMass(float depth)
		{
			float num = 1f - depth;
			return 1f + num * num * 5f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcInverseMass(float friction)
		{
			float num = 1f + friction * 3f;
			return 1f / num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcInverseMass(float friction, float depth)
		{
			float num = 1f;
			num += friction * 3f;
			float num2 = 1f - depth;
			num += num2 * num2 * 5f;
			return 1f / num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcInverseMass(float friction, float depth, bool fix, float fixMass)
		{
			if (!fix)
			{
				return CalcInverseMass(friction, depth);
			}
			return 1f / fixMass;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CalcSelfCollisionInverseMass(float friction, bool fix, float clothMass)
		{
			float num = (fix ? 100f : (1f + friction * 10f));
			num += clothMass * 50f;
			return 1f / num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 CalcSplitRange(int dataLength, int divCount, int divIndex)
		{
			if (dataLength <= 0)
			{
				return -1;
			}
			if (dataLength < divCount)
			{
				if (divIndex < dataLength)
				{
					return new int2(divIndex, divIndex + 1);
				}
				return -1;
			}
			float num = (float)dataLength / (float)divCount;
			int x = (int)(num * (float)divIndex);
			int y = ((divIndex == divCount - 1) ? dataLength : ((int)(num * (float)(divIndex + 1))));
			return new int2(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DataChunk GetWorkerChunk(int dataLenght, int workerCount, int workerIndex)
		{
			int2 int5 = CalcSplitRange(dataLenght, workerCount, workerIndex);
			if (int5.x < 0)
			{
				return DataChunk.Empty;
			}
			return new DataChunk(int5.x, int5.y - int5.x);
		}
	}
}
