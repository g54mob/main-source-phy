using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.Utils
{
	public static class MathHelper
	{
		private static Transform tempChild;

		private static Transform tempParent;

		private static Vector3[] positionRegister;

		private static float[] posTimeRegister;

		private static int positionSamplesTaken;

		private static Quaternion[] rotationRegister;

		private static float[] rotTimeRegister;

		private static int rotationSamplesTaken;

		public static float CountOfDigits(float number)
		{
			if (number != 0f)
			{
				return Mathf.Ceil(Mathf.Log10(Mathf.Abs(number) + 0.5f));
			}
			return 1f;
		}

		public static bool Approximately(Vector3 a, Vector3 b, float epsilonSq = 0.0001f)
		{
			return Vector3.SqrMagnitude(a - b) <= epsilonSq;
		}

		public static bool Approximately(Quaternion a, Quaternion b, float range = 0f)
		{
			return Quaternion.Dot(a, b) >= 1f - range;
		}

		public static bool RayIntersectsTriangle(Ray inRay, Vector3 inTriA, Vector3 inTriB, Vector3 inTriC, out float outDistance, out Vector3 outPoint)
		{
			outDistance = 0f;
			outPoint = Vector3.zero;
			Vector3 vector = inTriB - inTriA;
			Vector3 vector2 = inTriC - inTriA;
			Vector3 rhs = Vector3.Cross(inRay.direction, vector2);
			float num = Vector3.Dot(vector, rhs);
			if (num > 0f - Mathf.Epsilon && num < Mathf.Epsilon)
			{
				return false;
			}
			float num2 = 1f / num;
			Vector3 lhs = inRay.origin - inTriA;
			float num3 = Vector3.Dot(lhs, rhs) * num2;
			if (num3 < 0f || num3 > 1f)
			{
				return false;
			}
			Vector3 rhs2 = Vector3.Cross(lhs, vector);
			float num4 = Vector3.Dot(inRay.direction, rhs2) * num2;
			if (num4 < 0f || num3 + num4 > 1f)
			{
				return false;
			}
			float num5 = Vector3.Dot(vector2, rhs2) * num2;
			if (num5 > Mathf.Epsilon)
			{
				outDistance = num5;
				outPoint.x = num3 * inTriB.x + num4 * inTriC.x + (1f - (num3 + num4)) * inTriA.x;
				outPoint.y = num3 * inTriB.y + num4 * inTriC.y + (1f - (num3 + num4)) * inTriA.y;
				outPoint.z = num3 * inTriB.z + num4 * inTriC.z + (1f - (num3 + num4)) * inTriA.z;
				return true;
			}
			return false;
		}

		public static float RaySphereIntertsect(Vector3 r0, Vector3 rd, Vector3 s0, float sr)
		{
			float num = Vector3.Dot(rd, rd);
			Vector3 vector = r0 - s0;
			float num2 = 2f * Vector3.Dot(rd, vector);
			float num3 = Vector3.Dot(vector, vector) - sr * sr;
			if ((double)(num2 * num2) - 4.0 * (double)num * (double)num3 < 0.0)
			{
				return -1f;
			}
			return (0f - num2 - Mathf.Sqrt(num2 * num2 - 4f * num * num3)) / (2f * num);
		}

		public static void Init()
		{
			tempChild = new GameObject("Math3d_TempChild").transform;
			tempParent = new GameObject("Math3d_TempParent").transform;
			tempChild.gameObject.hideFlags = HideFlags.HideAndDontSave;
			UnityEngine.Object.DontDestroyOnLoad(tempChild.gameObject);
			tempParent.gameObject.hideFlags = HideFlags.HideAndDontSave;
			UnityEngine.Object.DontDestroyOnLoad(tempParent.gameObject);
			tempChild.parent = tempParent;
		}

		public static Vector2 GetPointOnSpline(float percentage, Vector2[] cPoints)
		{
			if (cPoints.Length >= 4)
			{
				int num = cPoints.Length - 3;
				int num2 = Mathf.Min(Mathf.FloorToInt(percentage * (float)num), num - 1);
				float num3 = percentage * (float)num - (float)num2;
				Vector2 vector = cPoints[num2];
				Vector2 vector2 = cPoints[num2 + 1];
				Vector2 vector3 = cPoints[num2 + 2];
				Vector2 vector4 = cPoints[num2 + 3];
				Vector2 vector5 = 0.5f * (2f * vector2 + (-vector + vector3) * num3 + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3));
				return new Vector2(vector5.x, vector5.y);
			}
			return new Vector2(0f, 0f);
		}

		public static float[] GetLineSplineIntersections(Vector2[] linePoints, Vector2[] cPoints)
		{
			List<float> list = new List<float>();
			int num = cPoints.Length - 3;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = cPoints[i];
				Vector2 vector2 = cPoints[i + 1];
				Vector2 vector3 = cPoints[i + 2];
				Vector2 vector4 = cPoints[i + 3];
				float num2 = 0.5f * (0f - vector.x + 3f * vector2.x - 3f * vector3.x + vector4.x);
				float num3 = 0.5f * (0f - vector.y + 3f * vector2.y - 3f * vector3.y + vector4.y);
				float num4 = 0.5f * (2f * vector.x - 5f * vector2.x + 4f * vector3.x - vector4.x);
				float num5 = 0.5f * (2f * vector.y - 5f * vector2.y + 4f * vector3.y - vector4.y);
				float num6 = 0.5f * (0f - vector.x + vector3.x);
				float num7 = 0.5f * (0f - vector.y + vector3.y);
				float num8 = 0.5f * (2f * vector2.x);
				float num9 = 0.5f * (2f * vector2.y);
				float num10 = linePoints[0].y - linePoints[1].y;
				float num11 = linePoints[1].x - linePoints[0].x;
				float num12 = (linePoints[0].x - linePoints[1].x) * linePoints[0].y + (linePoints[1].y - linePoints[0].y) * linePoints[0].x;
				float a = num10 * num2 + num11 * num3;
				float b = num10 * num4 + num11 * num5;
				float c = num10 * num6 + num11 * num7;
				float d = num10 * num8 + num11 * num9 + num12;
				SolveCubic(out var _, out var x, out var x2, out var x3, a, b, c, d);
				float num13 = (float)i / (float)num;
				float num14 = ((float)i + 1f) / (float)num - num13;
				if (x >= 0f && x <= 1f)
				{
					float item = x * num14 + num13;
					list.Add(item);
				}
				if (x2 >= 0f && x2 <= 1f)
				{
					float item = x2 * num14 + num13;
					list.Add(item);
				}
				if (x3 >= 0f && x3 <= 1f)
				{
					float item = x3 * num14 + num13;
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		private static void SolveCubic(out int nRoots, out float x1, out float x2, out float x3, float a, float b, float c, float d)
		{
			float num = MathF.PI * 2f;
			float num2 = MathF.PI * 4f;
			float num3 = a;
			a = b / num3;
			b = c / num3;
			c = d / num3;
			float num4 = a / 3f;
			float num5 = (3f * b - a * a) / 9f;
			float num6 = num5 * num5 * num5;
			float num7 = (9f * a * b - 27f * c - 2f * a * a * a) / 54f;
			float num8 = num7 * num7;
			float num9 = num6 + num8;
			if (num9 < 0f)
			{
				nRoots = 3;
				float num10 = Mathf.Acos(num7 / Mathf.Sqrt(0f - num6));
				float num11 = Mathf.Sqrt(0f - num5);
				x1 = 2f * num11 * Mathf.Cos(num10 / 3f) - num4;
				x2 = 2f * num11 * Mathf.Cos((num10 + num) / 3f) - num4;
				x3 = 2f * num11 * Mathf.Cos((num10 + num2) / 3f) - num4;
			}
			else if (num9 > 0f)
			{
				nRoots = 1;
				float num12 = Mathf.Sqrt(num9);
				float num13 = CubeRoot(num7 + num12);
				float num14 = CubeRoot(num7 - num12);
				x1 = num13 + num14 - num4;
				x2 = float.NaN;
				x3 = float.NaN;
			}
			else
			{
				nRoots = 3;
				float num15 = CubeRoot(num7);
				x1 = 2f * num15 - num4;
				x2 = num15 - num4;
				x3 = x2;
			}
		}

		private static float CubeRoot(float d)
		{
			if (d < 0f)
			{
				return 0f - Mathf.Pow(0f - d, 1f / 3f);
			}
			return Mathf.Pow(d, 1f / 3f);
		}

		public static Vector3 AddVectorLength(Vector3 vector, float size)
		{
			float num = Vector3.Magnitude(vector);
			float num2 = (num + size) / num;
			return vector * num2;
		}

		public static Vector3 SetVectorLength(Vector3 vector, float size)
		{
			return Vector3.Normalize(vector) * size;
		}

		public static Quaternion SubtractRotation(Quaternion B, Quaternion A)
		{
			return Quaternion.Inverse(A) * B;
		}

		public static Quaternion AddRotation(Quaternion A, Quaternion B)
		{
			return A * B;
		}

		public static Vector3 TransformDirectionMath(Quaternion rotation, Vector3 vector)
		{
			return rotation * vector;
		}

		public static Vector3 InverseTransformDirectionMath(Quaternion rotation, Vector3 vector)
		{
			return Quaternion.Inverse(rotation) * vector;
		}

		public static Vector3 RotateVectorFromTo(Quaternion from, Quaternion to, Vector3 vector)
		{
			Quaternion quaternion = SubtractRotation(to, from);
			Vector3 vector2 = InverseTransformDirectionMath(from, vector);
			Vector3 vector3 = quaternion * vector2;
			return TransformDirectionMath(from, vector3);
		}

		public static bool PlanePlaneIntersection(out Vector3 linePoint, out Vector3 lineVec, Vector3 plane1Normal, Vector3 plane1Position, Vector3 plane2Normal, Vector3 plane2Position)
		{
			linePoint = Vector3.zero;
			lineVec = Vector3.zero;
			lineVec = Vector3.Cross(plane1Normal, plane2Normal);
			Vector3 vector = Vector3.Cross(plane2Normal, lineVec);
			float num = Vector3.Dot(plane1Normal, vector);
			if (Mathf.Abs(num) > 0.006f)
			{
				Vector3 rhs = plane1Position - plane2Position;
				float num2 = Vector3.Dot(plane1Normal, rhs) / num;
				linePoint = plane2Position + num2 * vector;
				return true;
			}
			return false;
		}

		public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint)
		{
			intersection = Vector3.zero;
			float num = Vector3.Dot(planePoint - linePoint, planeNormal);
			float num2 = Vector3.Dot(lineVec, planeNormal);
			if (num2 != 0f)
			{
				float size = num / num2;
				Vector3 vector = SetVectorLength(lineVec, size);
				intersection = linePoint + vector;
				return true;
			}
			return false;
		}

		public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
		{
			Vector3 lhs = linePoint2 - linePoint1;
			Vector3 rhs = Vector3.Cross(lineVec1, lineVec2);
			Vector3 lhs2 = Vector3.Cross(lhs, lineVec2);
			if (Mathf.Abs(Vector3.Dot(lhs, rhs)) < 0.0001f && rhs.sqrMagnitude > 0.0001f)
			{
				float num = Vector3.Dot(lhs2, rhs) / rhs.sqrMagnitude;
				intersection = linePoint1 + lineVec1 * num;
				return true;
			}
			intersection = Vector3.zero;
			return false;
		}

		public static bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
		{
			closestPointLine1 = Vector3.zero;
			closestPointLine2 = Vector3.zero;
			float num = Vector3.Dot(lineVec1, lineVec1);
			float num2 = Vector3.Dot(lineVec1, lineVec2);
			float num3 = Vector3.Dot(lineVec2, lineVec2);
			float num4 = num * num3 - num2 * num2;
			if (num4 != 0f)
			{
				Vector3 rhs = linePoint1 - linePoint2;
				float num5 = Vector3.Dot(lineVec1, rhs);
				float num6 = Vector3.Dot(lineVec2, rhs);
				float num7 = (num2 * num6 - num5 * num3) / num4;
				float num8 = (num * num6 - num5 * num2) / num4;
				closestPointLine1 = linePoint1 + lineVec1 * num7;
				closestPointLine2 = linePoint2 + lineVec2 * num8;
				return true;
			}
			return false;
		}

		public static float DistanceToLine(Vector3 position, Vector3 linePoint1, Vector3 linePoint2)
		{
			return (ProjectPointOnLineSegment(linePoint1, linePoint2, position) - position).magnitude;
		}

		public static float DistanceToLine(Camera camera, Vector3 mousePosition, Vector3 linePoint1, Vector3 linePoint2)
		{
			Vector3 projectedPoint;
			return DistanceToLine(camera, mousePosition, linePoint1, linePoint2, out projectedPoint);
		}

		public static float DistanceToLine(Camera camera, Vector3 mousePosition, Vector3 linePoint1, Vector3 linePoint2, out Vector3 projectedPoint)
		{
			Vector3 linePoint3 = camera.WorldToScreenPoint(linePoint1);
			Vector3 linePoint4 = camera.WorldToScreenPoint(linePoint2);
			projectedPoint = ProjectPointOnLineSegment(linePoint3, linePoint4, mousePosition);
			projectedPoint = new Vector3(projectedPoint.x, projectedPoint.y, 0f);
			return (projectedPoint - mousePosition).magnitude;
		}

		public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
		{
			float num = Vector3.Dot(point - linePoint, lineVec);
			return linePoint + lineVec * num;
		}

		public static Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
		{
			Vector3 vector = ProjectPointOnLine(linePoint1, (linePoint2 - linePoint1).normalized, point);
			return PointOnWhichSideOfLineSegment(linePoint1, linePoint2, vector) switch
			{
				0 => vector, 
				1 => linePoint1, 
				2 => linePoint2, 
				_ => Vector3.zero, 
			};
		}

		public static Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
		{
			float num = SignedDistancePlanePoint(planeNormal, planePoint, point);
			num *= -1f;
			Vector3 vector = SetVectorLength(planeNormal, num);
			return point + vector;
		}

		public static Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector)
		{
			return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
		}

		public static float SignedDistancePlanePoint(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
		{
			return Vector3.Dot(planeNormal, point - planePoint);
		}

		public static float SignedDotProduct(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
		{
			return Vector3.Dot(Vector3.Cross(normal, vectorA), vectorB);
		}

		public static float SignedVectorAngle(Vector3 referenceVector, Vector3 otherVector, Vector3 normal)
		{
			Vector3 lhs = Vector3.Cross(normal, referenceVector);
			return Vector3.Angle(referenceVector, otherVector) * Mathf.Sign(Vector3.Dot(lhs, otherVector));
		}

		public static float AngleVectorPlane(Vector3 vector, Vector3 normal)
		{
			float num = (float)Math.Acos(Vector3.Dot(vector, normal));
			return MathF.PI / 2f - num;
		}

		public static float DotProductAngle(Vector3 vec1, Vector3 vec2)
		{
			double num = Vector3.Dot(vec1, vec2);
			if (num < -1.0)
			{
				num = -1.0;
			}
			if (num > 1.0)
			{
				num = 1.0;
			}
			return (float)Math.Acos(num);
		}

		public static void PlaneFrom3Points(out Vector3 planeNormal, out Vector3 planePoint, Vector3 pointA, Vector3 pointB, Vector3 pointC)
		{
			planeNormal = Vector3.zero;
			planePoint = Vector3.zero;
			Vector3 vector = pointB - pointA;
			Vector3 vector2 = pointC - pointA;
			planeNormal = Vector3.Normalize(Vector3.Cross(vector, vector2));
			Vector3 vector3 = pointA + vector / 2f;
			Vector3 vector4 = pointA + vector2 / 2f;
			Vector3 lineVec = pointC - vector3;
			Vector3 lineVec2 = pointB - vector4;
			ClosestPointsOnTwoLines(out planePoint, out var _, vector3, lineVec, vector4, lineVec2);
		}

		public static Vector3 GetForwardVector(Quaternion q)
		{
			return q * Vector3.forward;
		}

		public static Vector3 GetUpVector(Quaternion q)
		{
			return q * Vector3.up;
		}

		public static Vector3 GetRightVector(Quaternion q)
		{
			return q * Vector3.right;
		}

		public static Quaternion QuaternionFromMatrix(Matrix4x4 m)
		{
			return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
		}

		public static Vector3 PositionFromMatrix(Matrix4x4 m)
		{
			Vector4 column = m.GetColumn(3);
			return new Vector3(column.x, column.y, column.z);
		}

		public static void LookRotationExtended(ref GameObject gameObjectInOut, Vector3 alignWithVector, Vector3 alignWithNormal, Vector3 customForward, Vector3 customUp)
		{
			Quaternion quaternion = Quaternion.LookRotation(alignWithVector, alignWithNormal);
			Quaternion rotation = Quaternion.LookRotation(customForward, customUp);
			gameObjectInOut.transform.rotation = quaternion * Quaternion.Inverse(rotation);
		}

		public static void TransformWithParent(out Quaternion childRotation, out Vector3 childPosition, Quaternion parentRotation, Vector3 parentPosition, Quaternion startParentRotation, Vector3 startParentPosition, Quaternion startChildRotation, Vector3 startChildPosition)
		{
			childRotation = Quaternion.identity;
			childPosition = Vector3.zero;
			tempParent.rotation = startParentRotation;
			tempParent.position = startParentPosition;
			tempParent.localScale = Vector3.one;
			tempChild.rotation = startChildRotation;
			tempChild.position = startChildPosition;
			tempChild.localScale = Vector3.one;
			tempParent.rotation = parentRotation;
			tempParent.position = parentPosition;
			childRotation = tempChild.rotation;
			childPosition = tempChild.position;
		}

		public static void PreciseAlign(ref GameObject gameObjectInOut, Vector3 alignWithVector, Vector3 alignWithNormal, Vector3 alignWithPosition, Vector3 triangleForward, Vector3 triangleNormal, Vector3 trianglePosition)
		{
			LookRotationExtended(ref gameObjectInOut, alignWithVector, alignWithNormal, triangleForward, triangleNormal);
			Vector3 vector = gameObjectInOut.transform.TransformPoint(trianglePosition);
			Vector3 translation = alignWithPosition - vector;
			gameObjectInOut.transform.Translate(translation, Space.World);
		}

		public static void VectorsToTransform(ref GameObject gameObjectInOut, Vector3 positionVector, Vector3 directionVector, Vector3 normalVector)
		{
			gameObjectInOut.transform.position = positionVector;
			gameObjectInOut.transform.rotation = Quaternion.LookRotation(directionVector, normalVector);
		}

		public static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
		{
			Vector3 rhs = linePoint2 - linePoint1;
			Vector3 lhs = point - linePoint1;
			if (Vector3.Dot(lhs, rhs) > 0f)
			{
				if (lhs.magnitude <= rhs.magnitude)
				{
					return 0;
				}
				return 2;
			}
			return 1;
		}

		public static bool IsLineInRectangle(Vector3 linePoint1, Vector3 linePoint2, Vector3 rectA, Vector3 rectB, Vector3 rectC, Vector3 rectD)
		{
			bool flag = false;
			bool num = IsPointInRectangle(linePoint1, rectA, rectC, rectB, rectD);
			if (!num)
			{
				flag = IsPointInRectangle(linePoint2, rectA, rectC, rectB, rectD);
			}
			if (!num && !flag)
			{
				bool num2 = AreLineSegmentsCrossing(linePoint1, linePoint2, rectA, rectB);
				bool flag2 = AreLineSegmentsCrossing(linePoint1, linePoint2, rectB, rectC);
				bool flag3 = AreLineSegmentsCrossing(linePoint1, linePoint2, rectC, rectD);
				bool flag4 = AreLineSegmentsCrossing(linePoint1, linePoint2, rectD, rectA);
				if (num2 || flag2 || flag3 || flag4)
				{
					return true;
				}
				return false;
			}
			return true;
		}

		public static bool IsPointInRectangle(Vector3 point, Vector3 rectA, Vector3 rectC, Vector3 rectB, Vector3 rectD)
		{
			Vector3 vector = rectC - rectA;
			float size = 0f - vector.magnitude / 2f;
			vector = AddVectorLength(vector, size);
			Vector3 linePoint = rectA + vector;
			Vector3 vector2 = rectB - rectA;
			float num = vector2.magnitude / 2f;
			Vector3 vector3 = rectD - rectA;
			float num2 = vector3.magnitude / 2f;
			float magnitude = (ProjectPointOnLine(linePoint, vector2.normalized, point) - point).magnitude;
			if ((ProjectPointOnLine(linePoint, vector3.normalized, point) - point).magnitude <= num && magnitude <= num2)
			{
				return true;
			}
			return false;
		}

		public static bool AreLineSegmentsCrossing(Vector3 pointA1, Vector3 pointA2, Vector3 pointB1, Vector3 pointB2)
		{
			Vector3 vector = pointA2 - pointA1;
			Vector3 vector2 = pointB2 - pointB1;
			if (ClosestPointsOnTwoLines(out var closestPointLine, out var closestPointLine2, pointA1, vector.normalized, pointB1, vector2.normalized))
			{
				int num = PointOnWhichSideOfLineSegment(pointA1, pointA2, closestPointLine);
				int num2 = PointOnWhichSideOfLineSegment(pointB1, pointB2, closestPointLine2);
				if (num == 0 && num2 == 0)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public static bool LinearAcceleration(out Vector3 vector, Vector3 position, int samples)
		{
			Vector3 zero = Vector3.zero;
			vector = Vector3.zero;
			if (samples < 3)
			{
				samples = 3;
			}
			if (positionRegister == null)
			{
				positionRegister = new Vector3[samples];
				posTimeRegister = new float[samples];
			}
			for (int i = 0; i < positionRegister.Length - 1; i++)
			{
				positionRegister[i] = positionRegister[i + 1];
				posTimeRegister[i] = posTimeRegister[i + 1];
			}
			positionRegister[positionRegister.Length - 1] = position;
			posTimeRegister[posTimeRegister.Length - 1] = Time.unscaledTime;
			positionSamplesTaken++;
			if (positionSamplesTaken >= samples)
			{
				for (int j = 0; j < positionRegister.Length - 2; j++)
				{
					Vector3 vector2 = positionRegister[j + 1] - positionRegister[j];
					float num = posTimeRegister[j + 1] - posTimeRegister[j];
					if (num == 0f)
					{
						return false;
					}
					Vector3 vector3 = vector2 / num;
					vector2 = positionRegister[j + 2] - positionRegister[j + 1];
					num = posTimeRegister[j + 2] - posTimeRegister[j + 1];
					if (num == 0f)
					{
						return false;
					}
					Vector3 vector4 = vector2 / num;
					zero += vector4 - vector3;
				}
				zero /= (float)(positionRegister.Length - 2);
				float num2 = posTimeRegister[posTimeRegister.Length - 1] - posTimeRegister[0];
				vector = zero / num2;
				return true;
			}
			return false;
		}

		public static bool AngularAcceleration(out Vector3 vector, Quaternion rotation, int samples)
		{
			Vector3 zero = Vector3.zero;
			vector = Vector3.zero;
			if (samples < 3)
			{
				samples = 3;
			}
			if (rotationRegister == null)
			{
				rotationRegister = new Quaternion[samples];
				rotTimeRegister = new float[samples];
			}
			for (int i = 0; i < rotationRegister.Length - 1; i++)
			{
				rotationRegister[i] = rotationRegister[i + 1];
				rotTimeRegister[i] = rotTimeRegister[i + 1];
			}
			rotationRegister[rotationRegister.Length - 1] = rotation;
			rotTimeRegister[rotTimeRegister.Length - 1] = Time.unscaledTime;
			rotationSamplesTaken++;
			if (rotationSamplesTaken >= samples)
			{
				for (int j = 0; j < rotationRegister.Length - 2; j++)
				{
					Quaternion rotation2 = SubtractRotation(rotationRegister[j + 1], rotationRegister[j]);
					float num = rotTimeRegister[j + 1] - rotTimeRegister[j];
					if (num == 0f)
					{
						return false;
					}
					Vector3 vector2 = RotDiffToSpeedVec(rotation2, num);
					rotation2 = SubtractRotation(rotationRegister[j + 2], rotationRegister[j + 1]);
					num = rotTimeRegister[j + 2] - rotTimeRegister[j + 1];
					if (num == 0f)
					{
						return false;
					}
					Vector3 vector3 = RotDiffToSpeedVec(rotation2, num);
					zero += vector3 - vector2;
				}
				zero /= (float)(rotationRegister.Length - 2);
				float num2 = rotTimeRegister[rotTimeRegister.Length - 1] - rotTimeRegister[0];
				vector = zero / num2;
				return true;
			}
			return false;
		}

		public static float LinearFunction2DBasic(float x, float Qx, float Qy)
		{
			return x * (Qy / Qx);
		}

		public static float LinearFunction2DFull(float x, float Px, float Py, float Qx, float Qy)
		{
			float num = Qy - Py;
			float num2 = Qx - Px;
			float num3 = num / num2;
			return Py + num3 * (x - Px);
		}

		private static Vector3 RotDiffToSpeedVec(Quaternion rotation, float deltaTime)
		{
			float num = ((!(rotation.eulerAngles.x <= 180f)) ? (rotation.eulerAngles.x - 360f) : rotation.eulerAngles.x);
			float num2 = ((!(rotation.eulerAngles.y <= 180f)) ? (rotation.eulerAngles.y - 360f) : rotation.eulerAngles.y);
			return new Vector3(z: ((!(rotation.eulerAngles.z <= 180f)) ? (rotation.eulerAngles.z - 360f) : rotation.eulerAngles.z) / deltaTime, x: num / deltaTime, y: num2 / deltaTime);
		}
	}
}
