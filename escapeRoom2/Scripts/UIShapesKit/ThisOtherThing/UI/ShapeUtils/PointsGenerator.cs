using UnityEngine;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class PointsGenerator
	{
		public static void SetPoints(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			switch (data.Generator)
			{
			case PointsList.PointListGeneratorData.Generators.Rect:
				SetPointsRect(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.Round:
				SetPointsRound(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.RadialGraph:
				SetPointsRadialGraph(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.LineGraph:
				SetPointsLineGraph(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.AngleLine:
				SetPointsAngleLine(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.Star:
				SetPointsStar(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.Gear:
				SetPointsGear(ref positions, data);
				break;
			case PointsList.PointListGeneratorData.Generators.Custom:
				break;
			}
		}

		public static void SetPointsRect(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			if (positions == null || positions.Length != 4)
			{
				positions = new Vector2[4];
			}
			float num = data.Width * 0.5f;
			float num2 = data.Height * 0.5f;
			int num3 = data.IntStartOffset % 4;
			num3 = 4 + num3;
			num3 %= 4;
			for (int i = 0; i < 4; i++)
			{
				int num4 = i + num3;
				switch (num4 % 4)
				{
				case 0:
					positions[i].x = data.Center.x - num;
					positions[i].y = data.Center.y + num2;
					break;
				case 1:
					positions[i].x = data.Center.x + num;
					positions[i].y = data.Center.y + num2;
					break;
				case 2:
					positions[i].x = data.Center.x + num;
					positions[i].y = data.Center.y - num2;
					break;
				case 3:
					positions[i].x = data.Center.x - num;
					positions[i].y = data.Center.y - num2;
					break;
				}
			}
		}

		public static void SetPointsRound(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			float num = Mathf.Abs(data.Length);
			int num2 = Mathf.CeilToInt((float)data.Resolution * num);
			float num3 = 1f + ((float)data.Resolution * num - (float)num2);
			bool flag = num3 >= 0.0001f;
			int num4 = num2;
			if (flag)
			{
				num4++;
			}
			if (data.CenterPoint)
			{
				num4++;
			}
			if (positions == null || positions.Length != num4)
			{
				positions = new Vector2[num4];
			}
			if (data.CenterPoint)
			{
				positions[num4 - 1].x = data.Center.x;
				positions[num4 - 1].y = data.Center.y;
			}
			float num5 = Mathf.Max(0.001f, data.Width * 0.5f);
			float num6 = Mathf.Max(0.001f, data.Height * 0.5f);
			float num7 = data.FloatStartOffset * GeoUtils.TwoPI;
			float num8 = GeoUtils.TwoPI / (float)data.Resolution;
			if (data.SkipLastPosition)
			{
				num8 = GeoUtils.TwoPI / ((float)data.Resolution + 1f);
			}
			num8 *= Mathf.Sign(data.Direction);
			for (int i = 0; i < num2; i++)
			{
				float num9 = (float)i / (float)num4;
				positions[i].x = data.Center.x + Mathf.Sin(num7) * (num5 + num5 * data.EndRadius * num9);
				positions[i].y = data.Center.y + Mathf.Cos(num7) * (num6 + num6 * data.EndRadius * num9);
				num7 += num8;
			}
			if (flag)
			{
				float num9 = ((float)num2 + num3) / (float)num4;
				positions[num2].x = data.Center.x + Mathf.Sin(num7) * (num5 + num5 * data.EndRadius * num9);
				positions[num2].y = data.Center.y + Mathf.Cos(num7) * (num6 + num6 * data.EndRadius * num9);
				int num10 = Mathf.Max(num2 - 1, 0);
				positions[num2].x = Mathf.LerpUnclamped(positions[num10].x, positions[num2].x, num3);
				positions[num2].y = Mathf.LerpUnclamped(positions[num10].y, positions[num2].y, num3);
			}
		}

		public static void SetPointsRadialGraph(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			int num = data.FloatValues.Length;
			if (data.FloatValues.Length >= 3)
			{
				if (positions == null || positions.Length != num)
				{
					positions = new Vector2[num];
				}
				float num2 = data.FloatStartOffset * GeoUtils.TwoPI;
				float num3 = GeoUtils.TwoPI / (float)num;
				for (int i = 0; i < num; i++)
				{
					float num4 = Mathf.InverseLerp(data.MinFloatValue, data.MaxFloatValue, data.FloatValues[i]);
					num4 *= data.Radius;
					positions[i].x = data.Center.x + Mathf.Sin(num2) * num4;
					positions[i].y = data.Center.y + Mathf.Cos(num2) * num4;
					num2 += num3;
				}
			}
		}

		public static void SetPointsLineGraph(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			int num = data.FloatValues.Length;
			if (data.FloatValues.Length >= 2)
			{
				if (data.CenterPoint)
				{
					num += 2;
				}
				if (positions == null || positions.Length != num)
				{
					positions = new Vector2[num];
				}
				float num2 = data.Center.x + data.Width * -0.5f;
				float num3 = data.Width / ((float)data.FloatValues.Length - 1f);
				for (int i = 0; i < data.FloatValues.Length; i++)
				{
					float num4 = Mathf.InverseLerp(data.MinFloatValue, data.MaxFloatValue, data.FloatValues[i]);
					num4 -= 0.5f;
					num4 *= data.Height;
					positions[i].x = num2;
					positions[i].y = data.Center.y + num4;
					num2 += num3;
				}
				if (data.CenterPoint)
				{
					positions[data.FloatValues.Length].x = data.Center.x + data.Width * 0.5f;
					positions[data.FloatValues.Length].y = data.Center.y - data.Height * 0.5f;
					positions[data.FloatValues.Length + 1].x = data.Center.x + data.Width * -0.5f;
					positions[data.FloatValues.Length + 1].y = positions[data.FloatValues.Length].y;
				}
			}
		}

		public static void SetPointsAngleLine(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			if (positions == null || positions.Length != 2)
			{
				positions = new Vector2[2];
			}
			float num = Mathf.Sin(data.Angle * GeoUtils.TwoPI);
			float num2 = Mathf.Cos(data.Angle * GeoUtils.TwoPI);
			float num3 = data.Length * data.FloatStartOffset;
			positions[0].x = data.Center.x + num * num3;
			positions[0].y = data.Center.y + num2 * num3;
			positions[1].x = data.Center.x + num * (data.Length + num3);
			positions[1].y = data.Center.y + num2 * (data.Length + num3);
		}

		public static void SetPointsStar(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			int num = data.Resolution * 2;
			if (positions == null || positions.Length != num)
			{
				positions = new Vector2[num];
			}
			float num2 = data.FloatStartOffset * GeoUtils.TwoPI;
			float num3 = GeoUtils.TwoPI * data.Length / (float)num;
			float width = data.Width;
			float height = data.Height;
			float num4 = data.EndRadius * width;
			float num5 = data.EndRadius * width;
			for (int i = 0; i < num; i += 2)
			{
				positions[i].x = data.Center.x + Mathf.Sin(num2) * width;
				positions[i].y = data.Center.y + Mathf.Cos(num2) * height;
				num2 += num3;
				positions[i + 1].x = data.Center.x + Mathf.Sin(num2) * num4;
				positions[i + 1].y = data.Center.y + Mathf.Cos(num2) * num5;
				num2 += num3;
			}
		}

		public static void SetPointsGear(ref Vector2[] positions, PointsList.PointListGeneratorData data)
		{
			int num = data.Resolution * 4;
			if (positions == null || positions.Length != num)
			{
				positions = new Vector2[num];
			}
			float num2 = data.FloatStartOffset * GeoUtils.TwoPI;
			float num3 = GeoUtils.TwoPI / (float)data.Resolution;
			float width = data.Width;
			float height = data.Height;
			float num4 = data.EndRadius * width;
			float num5 = data.EndRadius * height;
			float num6 = num3 * 0.49f * data.InnerScaler;
			float num7 = num3 * 0.49f * data.OuterScaler;
			for (int i = 0; i < data.Resolution; i++)
			{
				int num8 = i * 4;
				positions[num8].x = data.Center.x + Mathf.Sin(num2 - num6) * num4;
				positions[num8].y = data.Center.y + Mathf.Cos(num2 - num6) * num5;
				positions[num8 + 1].x = data.Center.x + Mathf.Sin(num2 - num7) * width;
				positions[num8 + 1].y = data.Center.y + Mathf.Cos(num2 - num7) * height;
				positions[num8 + 2].x = data.Center.x + Mathf.Sin(num2 + num7) * width;
				positions[num8 + 2].y = data.Center.y + Mathf.Cos(num2 + num7) * height;
				positions[num8 + 3].x = data.Center.x + Mathf.Sin(num2 + num6) * num4;
				positions[num8 + 3].y = data.Center.y + Mathf.Cos(num2 + num6) * num5;
				num2 += num3;
			}
		}
	}
}
