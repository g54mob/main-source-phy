using System;
using System.Collections.Generic;
using ThisOtherThing.Utils;
using UnityEngine;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class PointsList
	{
		[Serializable]
		public class PointListsProperties
		{
			public PointListProperties[] PointListProperties;

			public PointListsProperties()
			{
				PointListProperties = new PointListProperties[1]
				{
					new PointListProperties()
				};
			}
		}

		[Serializable]
		public class PointListProperties
		{
			public PointListGeneratorData GeneratorData = new PointListGeneratorData();

			public Vector2[] Positions = new Vector2[3]
			{
				new Vector2(-20f, 0f),
				new Vector2(20f, 0f),
				new Vector2(20f, -20f)
			};

			[Range(0f, MathF.PI)]
			public float MaxAngle = 0.2f;

			[Minimum(0f)]
			public float RoundingDistance;

			public GeoUtils.RoundingProperties CornerRounding = new GeoUtils.RoundingProperties();

			public bool ShowHandles = true;

			public void SetPoints()
			{
				if (GeneratorData.NeedsUpdate && GeneratorData.Generator != PointListGeneratorData.Generators.Custom)
				{
					PointsGenerator.SetPoints(ref Positions, GeneratorData);
				}
				GeneratorData.NeedsUpdate = false;
			}
		}

		[Serializable]
		public class PointListGeneratorData
		{
			public enum Generators
			{
				Custom = 0,
				Rect = 1,
				Round = 2,
				RadialGraph = 3,
				LineGraph = 4,
				AngleLine = 5,
				Star = 6,
				Gear = 7
			}

			public Generators Generator;

			public bool NeedsUpdate = true;

			public Vector2 Center = Vector2.zero;

			[Min(1f)]
			public float Width = 10f;

			[Min(1f)]
			public float Height = 10f;

			[Min(1f)]
			public float Radius = 10f;

			[Range(-1f, 1f)]
			public float Direction = 1f;

			public float[] FloatValues;

			public float MinFloatValue;

			public float MaxFloatValue = 1f;

			public int IntStartOffset;

			public float FloatStartOffset;

			public float Length = 1f;

			public float EndRadius;

			[Min(2f)]
			public int Resolution = 10;

			public bool CenterPoint;

			public bool SkipLastPosition;

			public float Angle;

			public float InnerScaler = 0.8f;

			public float OuterScaler = 0.5f;
		}

		public struct PointsData
		{
			public bool NeedsUpdate;

			public bool IsClosed;

			public List<Vector2> Positions;

			public int NumPositions;

			public Vector2[] PositionTangents;

			public Vector2[] PositionNormals;

			public float TotalLength;

			public float[] PositionDistances;

			public float[] NormalizedPositionDistances;

			public Vector2 StartCapOffset;

			public Vector2 EndCapOffset;

			public bool GenerateRoundedCaps;

			public int RoundedCapResolution;

			public Vector2[] StartCapOffsets;

			public Vector2[] StartCapUVs;

			public Vector2[] EndCapOffsets;

			public Vector2[] EndCapUVs;

			public float LineWeight;
		}

		private static Vector2 tmpPos;

		private static Vector2 tmpBackV;

		private static Vector2 tmpBackNormV;

		private static Vector2 tmpForwV;

		private static Vector2 tmpForwNormV;

		private static Vector2 tmpBackPos;

		private static Vector2 tmpForwPos;

		private static List<Vector2> tmpCachedPositions = new List<Vector2>();

		public static void SetPositions(PointListProperties pointListProperties, ref PointsData lineData)
		{
			if (lineData.Positions == null)
			{
				lineData.Positions = new List<Vector2>(pointListProperties.Positions.Length);
			}
			CheckMinPointDistances(ref pointListProperties.Positions, ref tmpCachedPositions, lineData.LineWeight * 0.5f, lineData.IsClosed);
			lineData.Positions.Clear();
			int count = tmpCachedPositions.Count;
			if (lineData.Positions.Capacity < count)
			{
				lineData.Positions.Capacity = lineData.Positions.Capacity + count + 1;
			}
			if (lineData.IsClosed)
			{
				InterpolatePoints(ref lineData, tmpCachedPositions[count - 1], tmpCachedPositions[0], tmpCachedPositions[1], pointListProperties, 0);
			}
			else
			{
				lineData.Positions.Add(tmpCachedPositions[0]);
			}
			for (int i = 1; i < count - 1; i++)
			{
				InterpolatePoints(ref lineData, tmpCachedPositions[i - 1], tmpCachedPositions[i], tmpCachedPositions[i + 1], pointListProperties, i);
			}
			if (lineData.IsClosed)
			{
				InterpolatePoints(ref lineData, tmpCachedPositions[count - 2], tmpCachedPositions[count - 1], tmpCachedPositions[0], pointListProperties, count - 1);
			}
			else
			{
				lineData.Positions.Add(tmpCachedPositions[count - 1]);
			}
			lineData.NumPositions = lineData.Positions.Count;
		}

		private static void CheckMinPointDistances(ref Vector2[] inPositions, ref List<Vector2> outPositions, float minDistance, bool isClosed)
		{
			outPositions.Clear();
			if (outPositions.Capacity < inPositions.Length)
			{
				outPositions.Capacity = inPositions.Length;
			}
			float num = minDistance * minDistance;
			outPositions.Add(inPositions[0]);
			for (int i = 2; i < inPositions.Length; i++)
			{
				tmpPos.x = inPositions[i].x - inPositions[i - 1].x;
				tmpPos.y = inPositions[i].y - inPositions[i - 1].y;
				if (tmpPos.x * tmpPos.x + tmpPos.y * tmpPos.y < num)
				{
					tmpPos.x *= 0.5f;
					tmpPos.x += inPositions[i - 1].x;
					tmpPos.y *= 0.5f;
					tmpPos.y += inPositions[i - 1].y;
					outPositions.Add(tmpPos);
					i++;
				}
				else
				{
					outPositions.Add(inPositions[i - 1]);
				}
			}
			if (!isClosed)
			{
				outPositions.Add(inPositions[inPositions.Length - 1]);
				return;
			}
			tmpPos.x = inPositions[inPositions.Length - 1].x - inPositions[0].x;
			tmpPos.y = inPositions[inPositions.Length - 1].y - inPositions[0].y;
			if (tmpPos.x * tmpPos.x + tmpPos.y * tmpPos.y < num)
			{
				tmpPos.x *= 0.5f;
				tmpPos.x += inPositions[0].x;
				tmpPos.y *= 0.5f;
				tmpPos.y += inPositions[0].y;
				outPositions[0] = tmpPos;
			}
			else
			{
				outPositions.Add(inPositions[inPositions.Length - 1]);
			}
		}

		private static void InterpolatePoints(ref PointsData lineData, Vector2 prevPosition, Vector2 position, Vector2 nextPosition, PointListProperties pointListProperties, int index)
		{
			tmpBackV.x = prevPosition.x - position.x;
			tmpBackV.y = prevPosition.y - position.y;
			float num = Mathf.Sqrt(tmpBackV.x * tmpBackV.x + tmpBackV.y * tmpBackV.y);
			tmpBackNormV.x = tmpBackV.x / num;
			tmpBackNormV.y = tmpBackV.y / num;
			tmpForwV.x = nextPosition.x - position.x;
			tmpForwV.y = nextPosition.y - position.y;
			float num2 = Mathf.Sqrt(tmpForwV.x * tmpForwV.x + tmpForwV.y * tmpForwV.y);
			tmpForwNormV.x = tmpForwV.x / num2;
			tmpForwNormV.y = tmpForwV.y / num2;
			float num3 = tmpBackNormV.x * tmpForwNormV.x + tmpBackNormV.y * tmpForwNormV.y;
			float num4 = Mathf.Acos(num3);
			if (!(num3 <= -0.9999f))
			{
				if (pointListProperties.RoundingDistance > 0f)
				{
					AddRoundedPoints(ref lineData, tmpBackNormV, position, tmpForwNormV, pointListProperties, num4, Mathf.Min(num, num2) * 0.49f);
				}
				else if (num4 < pointListProperties.MaxAngle)
				{
					lineData.Positions.Add(position + tmpBackNormV * 0.5f);
					lineData.Positions.Add(position + tmpForwNormV * 0.5f);
				}
				else
				{
					lineData.Positions.Add(position);
				}
			}
		}

		private static void AddRoundedPoints(ref PointsData lineData, Vector2 backNormV, Vector2 position, Vector2 forwNormV, PointListProperties pointListProperties, float angle, float maxDistance)
		{
			float num = Mathf.Min(maxDistance, pointListProperties.RoundingDistance);
			tmpBackPos.x = position.x + backNormV.x * num;
			tmpBackPos.y = position.y + backNormV.y * num;
			tmpForwPos.x = position.x + forwNormV.x * num;
			tmpForwPos.y = position.y + forwNormV.y * num;
			pointListProperties.CornerRounding.UpdateAdjusted(num / 4f, 0f, (GeoUtils.TwoPI - angle) / MathF.PI);
			int adjustedResolution = pointListProperties.CornerRounding.AdjustedResolution;
			float num2 = (float)pointListProperties.CornerRounding.AdjustedResolution - 1f;
			if (lineData.Positions.Capacity < lineData.Positions.Count + adjustedResolution)
			{
				lineData.Positions.Capacity = lineData.Positions.Count + adjustedResolution;
			}
			for (int i = 0; i < adjustedResolution; i++)
			{
				float t = (float)i / num2;
				tmpPos.x = Mathf.LerpUnclamped(Mathf.LerpUnclamped(tmpBackPos.x, position.x, t), Mathf.LerpUnclamped(position.x, tmpForwPos.x, t), t);
				tmpPos.y = Mathf.LerpUnclamped(Mathf.LerpUnclamped(tmpBackPos.y, position.y, t), Mathf.LerpUnclamped(position.y, tmpForwPos.y, t), t);
				lineData.Positions.Add(tmpPos);
			}
		}

		public static bool SetLineData(PointListProperties pointListProperties, ref PointsData lineData)
		{
			if (pointListProperties.Positions == null || pointListProperties.Positions.Length <= 1)
			{
				return false;
			}
			bool flag = lineData.NeedsUpdate || lineData.Positions == null;
			if (flag)
			{
				SetPositions(pointListProperties, ref lineData);
			}
			int numPositions = lineData.NumPositions;
			if (lineData.PositionNormals == null || lineData.PositionNormals.Length != numPositions)
			{
				lineData.PositionTangents = new Vector2[numPositions];
				lineData.PositionNormals = new Vector2[numPositions];
				lineData.PositionDistances = new float[numPositions];
				lineData.NormalizedPositionDistances = new float[numPositions];
				for (int i = 0; i < numPositions; i++)
				{
					lineData.PositionNormals[i] = GeoUtils.ZeroV2;
					lineData.PositionTangents[i] = GeoUtils.ZeroV2;
				}
				flag = true;
			}
			if (flag)
			{
				int num = numPositions - 1;
				lineData.TotalLength = 0f;
				Vector2 lastUnitTangent = GeoUtils.ZeroV2;
				Vector2 currentUnitTangent = GeoUtils.ZeroV2;
				if (!lineData.IsClosed)
				{
					lineData.PositionTangents[0].x = lineData.Positions[0].x - lineData.Positions[1].x;
					lineData.PositionTangents[0].y = lineData.Positions[0].y - lineData.Positions[1].y;
					float num2 = Mathf.Sqrt(lineData.PositionTangents[0].x * lineData.PositionTangents[0].x + lineData.PositionTangents[0].y * lineData.PositionTangents[0].y);
					lineData.PositionDistances[0] = num2;
					lineData.TotalLength += num2;
					lineData.PositionNormals[0].x = lineData.PositionTangents[0].y / num2;
					lineData.PositionNormals[0].y = (0f - lineData.PositionTangents[0].x) / num2;
					lastUnitTangent.x = (0f - lineData.PositionTangents[0].x) / num2;
					lastUnitTangent.y = (0f - lineData.PositionTangents[0].y) / num2;
					lineData.StartCapOffset.x = 0f - lastUnitTangent.x;
					lineData.StartCapOffset.y = 0f - lastUnitTangent.y;
				}
				else
				{
					lastUnitTangent.x = lineData.Positions[0].x - lineData.Positions[num].x;
					lastUnitTangent.y = lineData.Positions[0].y - lineData.Positions[num].y;
					float num2 = Mathf.Sqrt(lastUnitTangent.x * lastUnitTangent.x + lastUnitTangent.y * lastUnitTangent.y);
					lastUnitTangent.x /= num2;
					lastUnitTangent.y /= num2;
					SetPointData(lineData.Positions[0], lineData.Positions[1], ref currentUnitTangent, ref lineData.PositionTangents[0], ref lineData.PositionNormals[0], ref lastUnitTangent, ref lineData.PositionDistances[0]);
					lineData.TotalLength += lineData.PositionDistances[0];
				}
				for (int j = 1; j < num; j++)
				{
					SetPointData(lineData.Positions[j], lineData.Positions[j + 1], ref currentUnitTangent, ref lineData.PositionTangents[j], ref lineData.PositionNormals[j], ref lastUnitTangent, ref lineData.PositionDistances[j]);
					lineData.TotalLength += lineData.PositionDistances[j];
				}
				if (!lineData.IsClosed)
				{
					lineData.PositionTangents[num].x = lineData.Positions[num].x - lineData.Positions[num - 1].x;
					lineData.PositionTangents[num].y = lineData.Positions[num].y - lineData.Positions[num - 1].y;
					float num2 = Mathf.Sqrt(lineData.PositionTangents[num].x * lineData.PositionTangents[num].x + lineData.PositionTangents[num].y * lineData.PositionTangents[num].y);
					lineData.EndCapOffset.x = lineData.PositionTangents[num].x / num2;
					lineData.EndCapOffset.y = lineData.PositionTangents[num].y / num2;
					lineData.PositionNormals[num].x = (0f - lineData.PositionTangents[num].y) / num2;
					lineData.PositionNormals[num].y = lineData.PositionTangents[num].x / num2;
				}
				else
				{
					SetPointData(lineData.Positions[num], lineData.Positions[0], ref currentUnitTangent, ref lineData.PositionTangents[num], ref lineData.PositionNormals[num], ref lastUnitTangent, ref lineData.PositionDistances[num]);
					lineData.TotalLength += lineData.PositionDistances[num];
				}
				if (lineData.GenerateRoundedCaps)
				{
					SetRoundedCapPointData(Mathf.Atan2(0f - lineData.PositionNormals[0].x, 0f - lineData.PositionNormals[0].y), ref lineData.StartCapOffsets, ref lineData.StartCapUVs, lineData.RoundedCapResolution, isStart: true);
					SetRoundedCapPointData(Mathf.Atan2(lineData.PositionNormals[num].x, lineData.PositionNormals[num].y), ref lineData.EndCapOffsets, ref lineData.EndCapUVs, lineData.RoundedCapResolution, isStart: false);
				}
				float num3 = 0f;
				for (int k = 0; k < lineData.PositionDistances.Length; k++)
				{
					lineData.NormalizedPositionDistances[k] = num3 / lineData.TotalLength;
					num3 += lineData.PositionDistances[k];
				}
			}
			lineData.NeedsUpdate = false;
			return true;
		}

		private static void SetRoundedCapPointData(float centerAngle, ref Vector2[] offsets, ref Vector2[] uvs, int resolution, bool isStart)
		{
			float num = MathF.PI / (float)(resolution + 1);
			float num2 = centerAngle;
			if (offsets == null || offsets.Length != resolution)
			{
				offsets = new Vector2[resolution];
				uvs = new Vector2[resolution];
			}
			num2 += num;
			for (int i = 0; i < resolution; i++)
			{
				float f = num2 + num * (float)i;
				offsets[i].x = Mathf.Sin(f);
				offsets[i].y = Mathf.Cos(f);
				f = num * (float)i + 0.43982297f;
				if (isStart)
				{
					f += MathF.PI;
				}
				uvs[i].x = Mathf.Abs(Mathf.Sin(f));
				uvs[i].y = Mathf.Cos(f) * 0.5f + 0.5f;
			}
		}

		private static void SetPointData(Vector2 currentPoint, Vector2 nextPoint, ref Vector2 currentUnitTangent, ref Vector2 positionTangent, ref Vector2 positionNormal, ref Vector2 lastUnitTangent, ref float distance)
		{
			positionTangent.x = currentPoint.x - nextPoint.x;
			positionTangent.y = currentPoint.y - nextPoint.y;
			distance = Mathf.Sqrt(positionTangent.x * positionTangent.x + positionTangent.y * positionTangent.y);
			currentUnitTangent.x = positionTangent.x / distance;
			currentUnitTangent.y = positionTangent.y / distance;
			positionNormal.x = 0f - (lastUnitTangent.x + currentUnitTangent.x);
			positionNormal.y = 0f - (lastUnitTangent.y + currentUnitTangent.y);
			if (positionNormal.x == 0f && positionNormal.y == 0f)
			{
				positionNormal.x = 0f - lastUnitTangent.y;
				positionNormal.y = lastUnitTangent.x;
			}
			float num = Mathf.Sqrt(positionNormal.x * positionNormal.x + positionNormal.y * positionNormal.y);
			positionNormal.x /= num;
			positionNormal.y /= num;
			float f = Mathf.Acos(Vector2.Dot(lastUnitTangent, currentUnitTangent)) * 0.5f;
			float num2 = 1f / Mathf.Sin(f);
			if (currentUnitTangent.x * positionNormal.y - currentUnitTangent.y * positionNormal.x > 0f)
			{
				num2 *= -1f;
			}
			positionNormal.x *= num2;
			positionNormal.y *= num2;
			lastUnitTangent.x = 0f - currentUnitTangent.x;
			lastUnitTangent.y = 0f - currentUnitTangent.y;
		}
	}
}
