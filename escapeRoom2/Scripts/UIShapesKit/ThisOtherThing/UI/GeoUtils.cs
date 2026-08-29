using System;
using ThisOtherThing.Utils;
using UnityEngine;

namespace ThisOtherThing.UI
{
	public class GeoUtils
	{
		[Serializable]
		public class ShapeProperties
		{
			public Color32 FillColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		[Serializable]
		public class OutlineShapeProperties : ShapeProperties
		{
			public bool DrawFill = true;

			public bool DrawFillShadow = true;

			public bool DrawOutline;

			public Color32 OutlineColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

			public bool DrawOutlineShadow;
		}

		[Serializable]
		public class AntiAliasingProperties
		{
			public float AntiAliasing = 1.25f;

			public float Adjusted { get; private set; }

			public void UpdateAdjusted(Canvas canvas)
			{
				if (canvas != null)
				{
					Adjusted = AntiAliasing * (1f / canvas.scaleFactor);
				}
				else
				{
					Adjusted = AntiAliasing;
				}
			}

			public void OnCheck()
			{
				AntiAliasing = Mathf.Max(AntiAliasing, 0f);
			}
		}

		[Serializable]
		public class RoundingProperties
		{
			public enum ResolutionType
			{
				Calculated = 0,
				Fixed = 1
			}

			public ResolutionType Resolution;

			[Minimum(2)]
			public int FixedResolution = 10;

			[Minimum(0.01f)]
			public float ResolutionMaxDistance = 4f;

			public int AdjustedResolution { get; private set; }

			public bool MakeSharpCorner { get; private set; }

			public void OnCheck(int minFixedResolution = 2)
			{
				FixedResolution = Mathf.Max(FixedResolution, minFixedResolution);
				ResolutionMaxDistance = Mathf.Max(ResolutionMaxDistance, 0.1f);
			}

			public void UpdateAdjusted(float radius, float offset, float numCorners)
			{
				UpdateAdjusted(radius, offset, this, numCorners);
			}

			public void UpdateAdjusted(float radius, float offset, RoundingProperties overrideProperties, float numCorners)
			{
				MakeSharpCorner = radius < 0.001f;
				radius += offset;
				switch (overrideProperties.Resolution)
				{
				case ResolutionType.Calculated:
				{
					float num = TwoPI * radius;
					AdjustedResolution = Mathf.CeilToInt(num / overrideProperties.ResolutionMaxDistance / numCorners);
					AdjustedResolution = Mathf.Max(AdjustedResolution, 2);
					break;
				}
				case ResolutionType.Fixed:
					AdjustedResolution = overrideProperties.FixedResolution;
					break;
				}
			}
		}

		[Serializable]
		public class OutlineProperties
		{
			public enum LineType
			{
				Inner = 0,
				Center = 1,
				Outer = 2
			}

			public LineType Type = LineType.Center;

			public float LineWeight = 2f;

			public float HalfLineWeight { get; private set; }

			public float GetOuterDistace()
			{
				return Type switch
				{
					LineType.Inner => 0f, 
					LineType.Outer => LineWeight, 
					LineType.Center => LineWeight * 0.5f, 
					_ => throw new ArgumentOutOfRangeException(), 
				};
			}

			public float GetCenterDistace()
			{
				return Type switch
				{
					LineType.Inner => LineWeight * -0.5f, 
					LineType.Outer => LineWeight * 0.5f, 
					LineType.Center => 0f, 
					_ => throw new ArgumentOutOfRangeException(), 
				};
			}

			public float GetInnerDistace()
			{
				return Type switch
				{
					LineType.Inner => 0f - LineWeight, 
					LineType.Outer => 0f, 
					LineType.Center => LineWeight * -0.5f, 
					_ => throw new ArgumentOutOfRangeException(), 
				};
			}

			public void OnCheck()
			{
				LineWeight = Mathf.Max(LineWeight, 0f);
			}

			public void UpdateAdjusted()
			{
				HalfLineWeight = LineWeight * 0.5f;
			}
		}

		[Serializable]
		public class ShadowsProperties
		{
			public bool ShowShape = true;

			public bool ShowShadows = true;

			[Range(-1f, 1f)]
			public float Angle;

			[Minimum(0f)]
			public float Distance;

			public ShadowProperties[] Shadows;

			[HideInInspector]
			public Vector2 Offset = Vector2.zero;

			public bool ShadowsEnabled
			{
				get
				{
					if (ShowShadows && Shadows != null)
					{
						return Shadows.Length != 0;
					}
					return false;
				}
			}

			public void UpdateAdjusted()
			{
				Offset.x = Mathf.Sin(Angle * MathF.PI - MathF.PI) * Distance;
				Offset.y = Mathf.Cos(Angle * MathF.PI - MathF.PI) * Distance;
			}

			public Vector2 GetCenterOffset(Vector2 center, int index)
			{
				center.x += Offset.x + Shadows[index].Offset.x;
				center.y += Offset.y + Shadows[index].Offset.y;
				return center;
			}
		}

		[Serializable]
		public class ShadowProperties
		{
			public Color32 Color = new Color32(0, 0, 0, 120);

			public Vector2 Offset = Vector2.zero;

			[Minimum(0f)]
			public float Size = 5f;

			[Range(0f, 1f)]
			public float Softness = 0.5f;
		}

		public struct EdgeGradientData
		{
			public bool IsActive;

			public float InnerScale;

			public float ShadowOffset;

			public float SizeAdd;

			public void SetActiveData(float innerScale, float shadowOffset, float sizeAdd)
			{
				IsActive = true;
				InnerScale = innerScale;
				ShadowOffset = shadowOffset;
				SizeAdd = sizeAdd;
			}

			public void Reset()
			{
				IsActive = false;
				InnerScale = 1f;
				ShadowOffset = 0f;
				SizeAdd = 0f;
			}
		}

		[Serializable]
		public class SnappedPositionAndOrientationProperties
		{
			public enum OrientationTypes
			{
				Horizontal = 0,
				Vertical = 1
			}

			public enum PositionTypes
			{
				Center = 0,
				Top = 1,
				Bottom = 2,
				Left = 3,
				Right = 4
			}

			public OrientationTypes Orientation;

			public PositionTypes Position;
		}

		public struct UnitPositionData
		{
			public Vector3[] UnitPositions;

			public float LastBaseAngle;

			public float LastDirection;
		}

		public static readonly Vector3 UpV3 = Vector3.up;

		public static readonly Vector3 DownV3 = Vector3.down;

		public static readonly Vector3 LeftV3 = Vector3.left;

		public static readonly Vector3 RightV3 = Vector3.right;

		public static readonly Vector3 ZeroV3 = Vector3.zero;

		public static readonly Vector2 ZeroV2 = Vector2.zero;

		public static readonly Vector3 UINormal = Vector3.back;

		public static readonly Vector4 UITangent = new Vector4(1f, 0f, 0f, -1f);

		public static readonly float HalfPI = MathF.PI / 2f;

		public static readonly float TwoPI = MathF.PI * 2f;

		public static float GetAdjustedAntiAliasing(Canvas canvas, float antiAliasing)
		{
			return antiAliasing * (1f / canvas.scaleFactor);
		}

		public static void AddOffset(ref float width, ref float height, float offset)
		{
			width += offset * 2f;
			height += offset * 2f;
		}

		public static void SetUnitPositionData(ref UnitPositionData unitPositionData, int resolution, float baseAngle = 0f, float direction = 1f)
		{
			bool flag = false;
			if (unitPositionData.UnitPositions == null || unitPositionData.UnitPositions.Length != resolution)
			{
				unitPositionData.UnitPositions = new Vector3[resolution];
				for (int i = 0; i < unitPositionData.UnitPositions.Length; i++)
				{
					unitPositionData.UnitPositions[i] = ZeroV3;
				}
				flag = true;
			}
			if (flag | (baseAngle != unitPositionData.LastBaseAngle || direction != unitPositionData.LastDirection))
			{
				float num = TwoPI / (float)resolution;
				num *= direction;
				for (int j = 0; j < resolution; j++)
				{
					float f = baseAngle + num * (float)j;
					unitPositionData.UnitPositions[j].x = Mathf.Sin(f);
					unitPositionData.UnitPositions[j].y = Mathf.Cos(f);
				}
				unitPositionData.LastBaseAngle = baseAngle;
				unitPositionData.LastDirection = direction;
			}
		}

		public static void SetUnitPositions(ref Vector2[] positions, int resolution, float angleOffset = 0f, float radius = 1f)
		{
			float num = angleOffset;
			float num2 = TwoPI / (float)resolution;
			bool flag = false;
			if (positions == null || positions.Length != resolution)
			{
				positions = new Vector2[resolution];
				flag = true;
			}
			if (!flag)
			{
				flag |= positions[0].x * positions[0].x + positions[0].y * positions[0].y != radius * radius;
			}
			if (flag)
			{
				for (int i = 0; i < resolution; i++)
				{
					positions[i].x = Mathf.Sin(num) * radius;
					positions[i].y = Mathf.Cos(num) * radius;
					num += num2;
				}
			}
		}

		public static float RadianAngleDifference(float angle1, float angle2)
		{
			float num = (angle2 - angle1 + MathF.PI) % TwoPI - MathF.PI;
			if (!(num < -MathF.PI))
			{
				return num;
			}
			return num + TwoPI;
		}

		public static int SimpleMap(int x, int in_max, int out_max)
		{
			return x * out_max / in_max;
		}

		public static float SimpleMap(float x, float in_max, float out_max)
		{
			return x * out_max / in_max;
		}

		public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
		{
			return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
		}
	}
}
