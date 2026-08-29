using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class DataUtility
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 PackInt2(int d0, int d1)
		{
			if (d0 >= d1)
			{
				return new int2(d1, d0);
			}
			return new int2(d0, d1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 PackInt2(in int2 d)
		{
			return PackInt2(d.x, d.y);
		}

		public static int3 PackInt3(int d0, int d1, int d2)
		{
			if (d0 < d1 && d0 < d2)
			{
				if (d1 < d2)
				{
					return new int3(d0, d1, d2);
				}
				return new int3(d0, d2, d1);
			}
			if (d1 < d2)
			{
				if (d0 < d2)
				{
					return new int3(d1, d0, d2);
				}
				return new int3(d1, d2, d0);
			}
			if (d0 < d1)
			{
				return new int3(d2, d0, d1);
			}
			return new int3(d2, d1, d0);
		}

		public static int3 PackInt3(in int3 d)
		{
			return PackInt3(d.x, d.y, d.z);
		}

		public static int4 PackInt4(int d0, int d1, int d2, int d3)
		{
			if (d0 > d3)
			{
				int num = d0;
				d0 = d3;
				d3 = num;
			}
			if (d1 > d2)
			{
				int num2 = d1;
				d1 = d2;
				d2 = num2;
			}
			if (d0 > d1)
			{
				int num3 = d0;
				d0 = d1;
				d1 = num3;
			}
			if (d2 > d3)
			{
				int num4 = d2;
				d2 = d3;
				d3 = num4;
			}
			if (d1 > d2)
			{
				int num5 = d1;
				d1 = d2;
				d2 = num5;
			}
			return new int4(d0, d1, d2, d3);
		}

		public static int4 PackInt4(int4 d)
		{
			return PackInt4(d.x, d.y, d.z, d.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack32(int hi, int low)
		{
			return (uint)((hi << 16) | (low & 0xFFFF));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack32Sort(int a, int b)
		{
			if (a > b)
			{
				return (uint)((b << 16) | (a & 0xFFFF));
			}
			return (uint)((a << 16) | (b & 0xFFFF));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack32Hi(uint pack)
		{
			return (int)((pack >> 16) & 0xFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack32Low(uint pack)
		{
			return (int)(pack & 0xFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack12_20(int hi, int low)
		{
			return (uint)((hi << 20) | (low & 0xFFFFF));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack12_20Hi(uint pack)
		{
			return (int)((pack >> 20) & 0xFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack12_20Low(uint pack)
		{
			return (int)(pack & 0xFFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Unpack12_20(uint pack, out int hi, out int low)
		{
			hi = (int)((pack >> 20) & 0xFFF);
			low = (int)(pack & 0xFFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Pack64(int x, int y, int z, int w)
		{
			return (((ulong)x & 0xFFFFuL) << 48) | (((ulong)y & 0xFFFFuL) << 32) | (((ulong)z & 0xFFFFuL) << 16) | ((ulong)w & 0xFFFFuL);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Pack64(in int4 a)
		{
			return Pack64(a.x, a.y, a.z, a.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 Unpack64(in ulong pack)
		{
			return new int4((int)((pack >> 48) & 0xFFFF), (int)((pack >> 32) & 0xFFFF), (int)((pack >> 16) & 0xFFFF), (int)(pack & 0xFFFF));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64X(in ulong pack)
		{
			return (int)((pack >> 48) & 0xFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64Y(in ulong pack)
		{
			return (int)((pack >> 32) & 0xFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64Z(in ulong pack)
		{
			return (int)((pack >> 16) & 0xFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Unpack64W(in ulong pack)
		{
			return (int)(pack & 0xFFFF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Pack32(int x, int y, int z, int w)
		{
			return (uint)(((x & 0xFF) << 24) | ((y & 0xFF) << 16) | ((z & 0xFF) << 8) | (w & 0xFF));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Pack32(in int4 a)
		{
			return Pack64(a.x, a.y, a.z, a.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 Unpack32(in uint pack)
		{
			return new int4((int)((pack >> 24) & 0xFF), (int)((pack >> 16) & 0xFF), (int)((pack >> 8) & 0xFF), (int)(pack & 0xFF));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RemainingData(in int3 data, in int2 use)
		{
			if (data.x != use.x && data.x != use.y)
			{
				return data.x;
			}
			if (data.y != use.x && data.y != use.y)
			{
				return data.y;
			}
			return data.z;
		}

		public static float4x4 ConvertAnimationCurve(AnimationCurve curve)
		{
			float4x4 m = 0;
			for (int i = 0; i < 16; i++)
			{
				float time = (float)i / 15f;
				float value = curve.Evaluate(time);
				m.MC2SetValue(i, value);
			}
			return m;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EvaluateCurve(in float4x4 curve, float time)
		{
			int num = (int)(math.saturate(time) * 15f);
			time -= (float)num * (1f / 15f);
			float t = time / (1f / 15f);
			return math.lerp(curve.MC2GetValue(num), curve.MC2GetValue(num + 1), t);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ColliderManager.ColliderType GetColliderType(in ExBitFlag8 flag)
		{
			return (ColliderManager.ColliderType)(flag.Value & 0xF);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ExBitFlag8 SetColliderType(ExBitFlag8 flag, ColliderManager.ColliderType ctype)
		{
			flag.Value = (byte)((uint)(flag.Value & 0xF0) | (uint)ctype);
			return flag;
		}

		public static void ArrayCopy<T>(T[] src, ref T[] dst)
		{
			if (src == null)
			{
				dst = null;
				return;
			}
			if (src.Length == 0)
			{
				dst = new T[0];
				return;
			}
			dst = new T[src.Length];
			Array.Copy(src, dst, src.Length);
		}
	}
}
