using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
	public static class PTUtils
	{
		public static Vector2 PointOnCircle2(float radius, float angle)
		{
			return new Vector2(radius * Mathf.Sin(angle), radius * Mathf.Cos(angle));
		}

		public static List<Vector2> PointsOnCircle2(float radius, int segments)
		{
			float num = MathF.PI * 2f / (float)segments;
			float num2 = 0f;
			List<Vector2> list = new List<Vector2>(segments);
			for (int i = 0; i < segments; i++)
			{
				list.Add(PointOnCircle2(radius, num2));
				num2 -= num;
			}
			return list;
		}

		public static Vector3 PointOnCircle3(float radius, float angle)
		{
			return new Vector3(radius * Mathf.Sin(angle), 0f, radius * Mathf.Cos(angle));
		}

		public static List<Vector3> PointsOnCircle3(float radius, int segments)
		{
			float num = MathF.PI * 2f / (float)segments;
			float num2 = 0f;
			List<Vector3> list = new List<Vector3>(segments);
			for (int i = 0; i < segments; i++)
			{
				list.Add(PointOnCircle3(radius, num2));
				num2 -= num;
			}
			return list;
		}

		public static Vector3 PointOnSphere(float radius, float longitude, float latitude)
		{
			return new Vector3(radius * Mathf.Sin(longitude) * Mathf.Cos(latitude), radius * Mathf.Sin(latitude), radius * Mathf.Cos(longitude) * Mathf.Cos(latitude));
		}

		public static void DrawLine(int x0, int y0, int x1, int y1, Action<int, int> draw)
		{
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				Swap(ref x0, ref y0);
				Swap(ref x1, ref y1);
			}
			if (x0 > x1)
			{
				Swap(ref x0, ref x1);
				Swap(ref y0, ref y1);
			}
			int num = x1 - x0;
			int num2 = Math.Abs(y1 - y0);
			int num3 = num / 2;
			int num4 = ((y0 < y1) ? 1 : (-1));
			int num5 = y0;
			for (int i = x0; i <= x1; i++)
			{
				draw(flag ? num5 : i, flag ? i : num5);
				num3 -= num2;
				if (num3 < 0)
				{
					num5 += num4;
					num3 += num;
				}
			}
		}

		public static void DrawCircle(int x0, int y0, int radius, Action<int, int> draw)
		{
			int num = -radius;
			int num2 = 0;
			int num3 = 2 - 2 * radius;
			while (num < 0)
			{
				draw(x0 - num, y0 + num2);
				draw(x0 - num2, y0 - num);
				draw(x0 + num, y0 - num2);
				draw(x0 + num2, y0 + num);
				int num4 = num3;
				if (num2 >= num3)
				{
					num2++;
					num3 += 2 * num2 + 1;
				}
				if (num < num4 || num2 < num3)
				{
					num++;
					num3 += 2 * num + 1;
				}
			}
		}

		public static void DrawFilledCircle(int x0, int y0, int radius, Action<int, int> draw)
		{
			int num = -radius;
			int num2 = 0;
			int num3 = 2 - 2 * radius;
			int num4 = int.MaxValue;
			while (num < 0)
			{
				if (num4 != num2)
				{
					DrawHorizontalLine(x0 + num, x0 - num, y0 + num2, draw);
					if (num2 != 0)
					{
						DrawHorizontalLine(x0 + num, x0 - num, y0 - num2, draw);
					}
				}
				num4 = num2;
				int num5 = num3;
				if (num2 >= num3)
				{
					num2++;
					num3 += 2 * num2 + 1;
				}
				if (num < num5 || num2 < num3)
				{
					num++;
					num3 += 2 * num + 1;
				}
			}
		}

		private static void DrawHorizontalLine(int fromX, int toX, int y, Action<int, int> draw)
		{
			for (int i = fromX; i <= toX; i++)
			{
				draw(i, y);
			}
		}

		public static void DrawAALine(int x0, int y0, int x1, int y1, Action<int, int, float> draw)
		{
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				Swap(ref x0, ref y0);
				Swap(ref x1, ref y1);
			}
			if (x0 > x1)
			{
				Swap(ref x0, ref x1);
				Swap(ref y0, ref y1);
			}
			if (flag)
			{
				draw(y0, x0, 1f);
				draw(y1, x1, 1f);
			}
			else
			{
				draw(x0, y0, 1f);
				draw(x1, y1, 1f);
			}
			float num = x1 - x0;
			float num2 = (float)(y1 - y0) / num;
			float num3 = (float)y0 + num2;
			for (int i = x0 + 1; i <= x1 - 1; i++)
			{
				if (flag)
				{
					draw((int)num3, i, 1f - (num3 - (float)(int)num3));
					draw((int)num3 + 1, i, num3 - (float)(int)num3);
				}
				else
				{
					draw(i, (int)num3, 1f - (num3 - (float)(int)num3));
					draw(i, (int)num3 + 1, num3 - (float)(int)num3);
				}
				num3 += num2;
			}
		}

		public static Vector2 Perp(Vector2 vector)
		{
			return new Vector2(0f - vector.y, vector.x);
		}

		public static float PerpDot(Vector2 a, Vector2 b)
		{
			return a.x * b.y - a.y * b.x;
		}

		public static void Swap<T>(ref T left, ref T right)
		{
			T val = left;
			left = right;
			right = val;
		}

		public static Dictionary<T, int> Knapsack<T>(Dictionary<T, float> set, float capacity, Dictionary<T, int> knapsack = null)
		{
			List<T> list = new List<T>(set.Keys);
			list.Sort((T a, T b) => -set[a].CompareTo(set[b]));
			if (knapsack == null)
			{
				knapsack = new Dictionary<T, int>();
				foreach (T item in list)
				{
					knapsack[item] = 0;
				}
			}
			return Knapsack(set, list, capacity, knapsack, 0);
		}

		private static Dictionary<T, int> Knapsack<T>(Dictionary<T, float> set, List<T> keys, float remainder, Dictionary<T, int> knapsack, int startIndex)
		{
			T val = keys[keys.Count - 1];
			if (remainder < set[val])
			{
				knapsack[val] = 1;
				return knapsack;
			}
			for (int i = startIndex; i < keys.Count; i++)
			{
				T key = keys[i];
				float num = set[key];
				knapsack[key] += (int)(remainder / num);
				remainder %= num;
			}
			if (remainder > 0f)
			{
				for (int j = 0; j < keys.Count; j++)
				{
					T key2 = keys[j];
					if (knapsack[key2] != 0)
					{
						if (key2.Equals(val))
						{
							return knapsack;
						}
						knapsack[key2]--;
						remainder += set[key2];
						startIndex = j + 1;
						break;
					}
				}
				knapsack = Knapsack(set, keys, remainder, knapsack, startIndex);
			}
			return knapsack;
		}
	}
}
