using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.ThirdParty.LibTessDotNet;
using UnityEngine;

namespace FluffyUnderware.Curvy.Utils
{
	[Serializable]
	public class SplinePolyLine
	{
		public enum VertexCalculation
		{
			ByApproximation = 0,
			ByAngle = 1
		}

		public ContourOrientation Orientation;

		public CurvySpline Spline;

		public VertexCalculation VertexMode;

		public float Angle;

		public float Distance;

		public Space Space;

		public bool IsClosed
		{
			get
			{
				if ((bool)Spline)
				{
					return Spline.Closed;
				}
				return false;
			}
		}

		public SplinePolyLine(CurvySpline spline)
			: this(spline, VertexCalculation.ByApproximation, 0f, 0f)
		{
		}

		public SplinePolyLine(CurvySpline spline, float angle, float distance)
			: this(spline, VertexCalculation.ByAngle, angle, distance)
		{
		}

		private SplinePolyLine(CurvySpline spline, VertexCalculation vertexMode, float angle, float distance, Space space = Space.World)
		{
			Spline = spline;
			VertexMode = vertexMode;
			Angle = angle;
			Distance = distance;
			Space = space;
		}

		public Vector3[] GetVertices()
		{
			Vector3[] array = new Vector3[0];
			array = ((VertexMode != VertexCalculation.ByAngle) ? Spline.GetApproximation() : GetPolygon(Spline, 0f, 1f, Angle, Distance, -1f, out var _, out var _, includeEndPoint: false));
			if (Space == Space.World)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Spline.transform.TransformPoint(array[i]);
				}
			}
			return array;
		}

		private static Vector3[] GetPolygon(CurvySpline spline, float fromTF, float toTF, float maxAngle, float minDistance, float maxDistance, out List<float> vertexTF, out List<Vector3> vertexTangents, bool includeEndPoint = true, float stepSize = 0.01f)
		{
			stepSize = Mathf.Clamp(stepSize, 0.002f, 1f);
			maxDistance = ((maxDistance == -1f) ? spline.Length : Mathf.Clamp(maxDistance, 0f, spline.Length));
			minDistance = Mathf.Clamp(minDistance, 0f, maxDistance);
			if (!spline.Closed)
			{
				toTF = Mathf.Clamp01(toTF);
				fromTF = Mathf.Clamp(fromTF, 0f, toTF);
			}
			List<Vector3> vPos = new List<Vector3>();
			List<Vector3> vTan = new List<Vector3>();
			List<float> vTF = new List<float>();
			int linearSteps = 0;
			float angleFromLast = 0f;
			float distAccu = 0f;
			Vector3 curPos = spline.Interpolate(fromTF);
			Vector3 curTangent = spline.GetTangent(fromTF);
			Vector3 vector = curPos;
			Vector3 vector2 = curTangent;
			Action<float> action = delegate(float f)
			{
				vPos.Add(curPos);
				vTan.Add(curTangent);
				vTF.Add(f);
				angleFromLast = 0f;
				distAccu = 0f;
				linearSteps = 0;
			};
			action(fromTF);
			float num = fromTF + stepSize;
			while (num < toTF)
			{
				float num2 = num % 1f;
				spline.InterpolateAndGetTangent(num2, out curPos, out curTangent);
				if (curTangent == Vector3.zero)
				{
					Debug.Log("zero Tangent! Oh no!");
				}
				distAccu += (curPos - vector).magnitude;
				if (curTangent == vector2)
				{
					linearSteps++;
				}
				if (distAccu >= minDistance)
				{
					if (distAccu >= maxDistance)
					{
						action(num2);
					}
					else
					{
						angleFromLast += Vector3.Angle(vector2, curTangent);
						if (angleFromLast >= maxAngle || (linearSteps > 0 && angleFromLast > 0f))
						{
							action(num2);
						}
					}
				}
				num += stepSize;
				vector = curPos;
				vector2 = curTangent;
			}
			if (includeEndPoint)
			{
				vTF.Add(toTF % 1f);
				spline.InterpolateAndGetTangent(toTF % 1f, out curPos, out var localTangent);
				vPos.Add(curPos);
				vTan.Add(localTangent);
			}
			vertexTF = vTF;
			vertexTangents = vTan;
			return vPos.ToArray();
		}
	}
}
