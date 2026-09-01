using System;
using System.Collections.Generic;
using UnityEngine;

namespace Poly.Physics.ThirdParty
{
	public static class ConvexDecomposer
	{
		private const int MAX_CONVEX_PARTITION_CALL_DEPTH = 128;

		public static int callDepth;

		private static Vec2 At(int i, List<Vec2> vertices)
		{
			int count = vertices.Count;
			return vertices[(i + 10 * count) % count];
		}

		private static List<Vec2> Copy(int i, int j, List<Vec2> vertices)
		{
			List<Vec2> list = new List<Vec2>();
			while (j < i)
			{
				j += vertices.Count;
			}
			while (i <= j)
			{
				list.Add(At(i, vertices));
				i++;
			}
			return list;
		}

		public static List<List<Vec2>> ConvexPartition(List<Vec2> vertices)
		{
			callDepth = 0;
			return _ConvexPartition(vertices);
		}

		private static List<List<Vec2>> _ConvexPartition(List<Vec2> vertices)
		{
			callDepth++;
			if (128 < callDepth)
			{
				return new List<List<Vec2>>();
			}
			ForceCounterClockWise(vertices);
			List<List<Vec2>> list = new List<List<Vec2>>();
			Vec2 vec = default(Vec2);
			Vec2 vec2 = default(Vec2);
			int num = 0;
			int i = 0;
			for (int j = 0; j < vertices.Count; j++)
			{
				if (!Reflex(j, vertices))
				{
					continue;
				}
				float num3;
				float num2 = (num3 = float.MaxValue);
				for (int k = 0; k < vertices.Count; k++)
				{
					Vec2 vec3;
					if (Left(At(j - 1, vertices), At(j, vertices), At(k, vertices)) && RightOn(At(j - 1, vertices), At(j, vertices), At(k - 1, vertices)))
					{
						vec3 = LineIntersect(At(j - 1, vertices), At(j, vertices), At(k, vertices), At(k - 1, vertices));
						if (Right(At(j + 1, vertices), At(j, vertices), vec3))
						{
							float num4 = SquareDist(At(j, vertices), vec3);
							if (num4 < num2)
							{
								num2 = num4;
								vec = vec3;
								num = k;
							}
						}
					}
					if (!Left(At(j + 1, vertices), At(j, vertices), At(k + 1, vertices)) || !RightOn(At(j + 1, vertices), At(j, vertices), At(k, vertices)))
					{
						continue;
					}
					vec3 = LineIntersect(At(j + 1, vertices), At(j, vertices), At(k, vertices), At(k + 1, vertices));
					if (Left(At(j - 1, vertices), At(j, vertices), vec3))
					{
						float num4 = SquareDist(At(j, vertices), vec3);
						if (num4 < num3)
						{
							num3 = num4;
							i = k;
							vec2 = vec3;
						}
					}
				}
				List<Vec2> list2;
				List<Vec2> list3;
				if (num == (i + 1) % vertices.Count)
				{
					Vec2 item = (vec + vec2) / 2f;
					list2 = Copy(j, i, vertices);
					list2.Add(item);
					list3 = Copy(num, j, vertices);
					list3.Add(item);
				}
				else
				{
					double num5 = 0.0;
					double num6 = num;
					for (; i < num; i += vertices.Count)
					{
					}
					for (int l = num; l <= i; l++)
					{
						if (CanSee(j, l, vertices))
						{
							double num7 = 1f / (SquareDist(At(j, vertices), At(l, vertices)) + 1f);
							num7 = ((!Reflex(l, vertices)) ? (num7 + 1.0) : ((!RightOn(At(l - 1, vertices), At(l, vertices), At(j, vertices)) || !LeftOn(At(l + 1, vertices), At(l, vertices), At(j, vertices))) ? (num7 + 2.0) : (num7 + 3.0)));
							if (num7 > num5)
							{
								num6 = l;
								num5 = num7;
							}
						}
					}
					list2 = Copy(j, (int)num6, vertices);
					list3 = Copy((int)num6, j, vertices);
				}
				list.AddRange(_ConvexPartition(list2));
				list.AddRange(_ConvexPartition(list3));
				return list;
			}
			list.Add(vertices);
			for (int m = 0; m < list.Count; m++)
			{
			}
			for (int num8 = list.Count - 1; num8 >= 0; num8--)
			{
				if (list[num8].Count == 0)
				{
					list.RemoveAt(num8);
				}
			}
			return list;
		}

		private static bool CanSee(int i, int j, List<Vec2> vertices)
		{
			if (Reflex(i, vertices))
			{
				if (LeftOn(At(i, vertices), At(i - 1, vertices), At(j, vertices)) && RightOn(At(i, vertices), At(i + 1, vertices), At(j, vertices)))
				{
					return false;
				}
			}
			else if (RightOn(At(i, vertices), At(i + 1, vertices), At(j, vertices)) || LeftOn(At(i, vertices), At(i - 1, vertices), At(j, vertices)))
			{
				return false;
			}
			if (Reflex(j, vertices))
			{
				if (LeftOn(At(j, vertices), At(j - 1, vertices), At(i, vertices)) && RightOn(At(j, vertices), At(j + 1, vertices), At(i, vertices)))
				{
					return false;
				}
			}
			else if (RightOn(At(j, vertices), At(j + 1, vertices), At(i, vertices)) || LeftOn(At(j, vertices), At(j - 1, vertices), At(i, vertices)))
			{
				return false;
			}
			for (int k = 0; k < vertices.Count; k++)
			{
				if ((k + 1) % vertices.Count != i && k != i && (k + 1) % vertices.Count != j && k != j && LineIntersect2(At(i, vertices), At(j, vertices), At(k, vertices), At(k + 1, vertices), out var _))
				{
					return false;
				}
			}
			return true;
		}

		private static bool Reflex(int i, List<Vec2> vertices)
		{
			return Right(i, vertices);
		}

		private static bool Right(int i, List<Vec2> vertices)
		{
			return Right(At(i - 1, vertices), At(i, vertices), At(i + 1, vertices));
		}

		private static bool Left(Vec2 a, Vec2 b, Vec2 c)
		{
			return Area(ref a, ref b, ref c) > 0f;
		}

		private static bool LeftOn(Vec2 a, Vec2 b, Vec2 c)
		{
			return Area(ref a, ref b, ref c) >= 0f;
		}

		private static bool Right(Vec2 a, Vec2 b, Vec2 c)
		{
			return Area(ref a, ref b, ref c) < 0f;
		}

		private static bool RightOn(Vec2 a, Vec2 b, Vec2 c)
		{
			return Area(ref a, ref b, ref c) <= 0f;
		}

		private static float SquareDist(Vec2 a, Vec2 b)
		{
			float num = b.x - a.x;
			float num2 = b.y - a.y;
			return num * num + num2 * num2;
		}

		private static void ForceCounterClockWise(List<Vec2> vertices)
		{
			if (!IsCounterClockWise(vertices))
			{
				vertices.Reverse();
			}
		}

		private static bool IsCounterClockWise(List<Vec2> vertices)
		{
			if (vertices.Count < 3)
			{
				return true;
			}
			return GetSignedArea(vertices) > 0f;
		}

		private static float GetSignedArea(List<Vec2> vertices)
		{
			float num = 0f;
			for (int i = 0; i < vertices.Count; i++)
			{
				int index = (i + 1) % vertices.Count;
				num += vertices[i].x * vertices[index].y;
				num -= vertices[i].y * vertices[index].x;
			}
			return num / 2f;
		}

		private static Vec2 LineIntersect(Vec2 p1, Vec2 p2, Vec2 q1, Vec2 q2)
		{
			Vec2 zero = Vec2.zero;
			float num = p2.y - p1.y;
			float num2 = p1.x - p2.x;
			float num3 = num * p1.x + num2 * p1.y;
			float num4 = q2.y - q1.y;
			float num5 = q1.x - q2.x;
			float num6 = num4 * q1.x + num5 * q1.y;
			float num7 = num * num5 - num4 * num2;
			if (!FloatEquals(num7, 0f))
			{
				zero.x = (num5 * num3 - num2 * num6) / num7;
				zero.y = (num * num6 - num4 * num3) / num7;
			}
			return zero;
		}

		private static bool LineIntersect2(Vec2 a0, Vec2 a1, Vec2 b0, Vec2 b1, out Vec2 intersectionPoint)
		{
			intersectionPoint = Vec2.zero;
			if (a0 == b0 || a0 == b1 || a1 == b0 || a1 == b1)
			{
				return false;
			}
			float x = a0.x;
			float y = a0.y;
			float x2 = a1.x;
			float y2 = a1.y;
			float x3 = b0.x;
			float y3 = b0.y;
			float x4 = b1.x;
			float y4 = b1.y;
			if (System.Math.Max(x, x2) < System.Math.Min(x3, x4) || System.Math.Max(x3, x4) < System.Math.Min(x, x2))
			{
				return false;
			}
			if (System.Math.Max(y, y2) < System.Math.Min(y3, y4) || System.Math.Max(y3, y4) < System.Math.Min(y, y2))
			{
				return false;
			}
			float num = (x4 - x3) * (y - y3) - (y4 - y3) * (x - x3);
			float num2 = (x2 - x) * (y - y3) - (y2 - y) * (x - x3);
			float num3 = (y4 - y3) * (x2 - x) - (x4 - x3) * (y2 - y);
			if (System.Math.Abs(num3) < Mathf.Epsilon)
			{
				return false;
			}
			num /= num3;
			num2 /= num3;
			if (0f < num && num < 1f && 0f < num2 && num2 < 1f)
			{
				intersectionPoint.x = x + num * (x2 - x);
				intersectionPoint.y = y + num * (y2 - y);
				return true;
			}
			return false;
		}

		private static bool FloatEquals(float value1, float value2)
		{
			return System.Math.Abs(value1 - value2) <= Mathf.Epsilon;
		}

		private static float Area(Vec2 a, Vec2 b, Vec2 c)
		{
			return Area(ref a, ref b, ref c);
		}

		private static float Area(ref Vec2 a, ref Vec2 b, ref Vec2 c)
		{
			return a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y);
		}

		private static List<Vec2> CollinearSimplify(List<Vec2> vertices, float collinearityTolerance)
		{
			if (vertices.Count < 3)
			{
				return vertices;
			}
			List<Vec2> list = new List<Vec2>();
			for (int i = 0; i < vertices.Count; i++)
			{
				int index = PreviousIndex(vertices, i);
				int index2 = NextIndex(vertices, i);
				Vec2 a = vertices[index];
				Vec2 b = vertices[i];
				Vec2 c = vertices[index2];
				if (!Collinear(ref a, ref b, ref c, collinearityTolerance))
				{
					list.Add(b);
				}
			}
			return list;
		}

		private static int PreviousIndex(List<Vec2> vertices, int index)
		{
			if (index == 0)
			{
				return vertices.Count - 1;
			}
			return index - 1;
		}

		private static int NextIndex(List<Vec2> vertices, int index)
		{
			if (index == vertices.Count - 1)
			{
				return 0;
			}
			return index + 1;
		}

		private static bool Collinear(ref Vec2 a, ref Vec2 b, ref Vec2 c, float tolerance)
		{
			return FloatInRange(Area(ref a, ref b, ref c), 0f - tolerance, tolerance);
		}

		private static bool FloatInRange(float value, float min, float max)
		{
			if (value >= min)
			{
				return value <= max;
			}
			return false;
		}
	}
}
