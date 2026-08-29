using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public static class InterlockUtility
	{
		internal const int ToFixed = 1000000;

		internal const float ToFloat = 1E-06f;

		internal unsafe static void AddFloat3(int index, float3 add, int* cntPt, int* sumPt)
		{
			Interlocked.Increment(ref cntPt[index]);
			int3 int5 = (int3)(add * 1000000f);
			index *= 3;
			int num = 0;
			while (num < 3)
			{
				if (int5[num] != 0)
				{
					Interlocked.Add(ref sumPt[index], int5[num]);
				}
				num++;
				index++;
			}
		}

		internal unsafe static void AddFloat3(int index, float3 add, int* sumPt)
		{
			int3 int5 = (int3)(add * 1000000f);
			index *= 3;
			int num = 0;
			while (num < 3)
			{
				if (int5[num] != 0)
				{
					Interlocked.Add(ref sumPt[index], int5[num]);
				}
				num++;
				index++;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void Max(int index, float value, int* pt)
		{
			int num = (int)value * 1000000;
			int num2 = pt[index];
			int num3 = num2 + 1;
			while (num > num2 && num2 != num3)
			{
				num3 = num2;
				num2 = Interlocked.CompareExchange(ref pt[index], num, num2);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static float3 ReadAverageFloat3(int index, int* cntPt, int* sumPt)
		{
			int num = cntPt[index];
			if (num == 0)
			{
				return 0;
			}
			int num2 = index * 3;
			return new float3(sumPt[num2], sumPt[num2 + 1], sumPt[num2 + 2]) / num * 1E-06f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static float3 ReadFloat3(int index, int* vecPt)
		{
			int num = index * 3;
			return new float3(vecPt[num], vecPt[num + 1], vecPt[num + 2]) * 1E-06f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static float ReadFloat(int index, int* floatPt)
		{
			return (float)floatPt[index] * 1E-06f;
		}
	}
}
