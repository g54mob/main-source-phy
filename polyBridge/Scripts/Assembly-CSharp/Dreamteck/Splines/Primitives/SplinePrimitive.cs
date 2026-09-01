using UnityEngine;

namespace Dreamteck.Splines.Primitives
{
	public class SplinePrimitive
	{
		public enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2,
			nX = 3,
			nY = 4,
			nZ = 5
		}

		protected bool closed;

		protected SplinePoint[] points = new SplinePoint[0];

		protected Spline.Type type = Spline.Type.Linear;

		public Axis axis = Axis.Y;

		public Vector3 offset = Vector3.zero;

		public Vector3 rotation = Vector3.zero;

		protected virtual void Generate()
		{
		}

		public Spline GetSpline()
		{
			Generate();
			ApplyOffset();
			Spline spline = new Spline(type);
			spline.points = points;
			if (closed)
			{
				spline.Close();
			}
			return spline;
		}

		public void UpdateSpline(Spline spline)
		{
			Generate();
			ApplyOffset();
			spline.type = type;
			spline.points = points;
			if (closed)
			{
				spline.Close();
			}
			else if (spline.isClosed)
			{
				spline.Break();
			}
		}

		public SplineComputer CreateSplineComputer(string name, Vector3 position, Quaternion rotation)
		{
			Generate();
			ApplyOffset();
			SplineComputer splineComputer = new GameObject(name).AddComponent<SplineComputer>();
			splineComputer.type = type;
			splineComputer.SetPoints(points, SplineComputer.Space.Local);
			if (closed)
			{
				splineComputer.Close();
			}
			splineComputer.transform.position = position;
			splineComputer.transform.rotation = rotation;
			return splineComputer;
		}

		public void UpdateSplineComputer(SplineComputer comp)
		{
			Generate();
			ApplyOffset();
			comp.type = type;
			comp.SetPoints(points, SplineComputer.Space.Local);
			if (closed)
			{
				comp.Close();
			}
			else if (comp.isClosed)
			{
				comp.Break();
			}
		}

		private void ApplyOffset()
		{
			Quaternion quaternion = Quaternion.LookRotation(GetNormal());
			Quaternion quaternion2 = Quaternion.Euler(rotation);
			for (int i = 0; i < points.Length; i++)
			{
				points[i].position = quaternion * quaternion2 * points[i].position;
				points[i].tangent = quaternion * quaternion2 * points[i].tangent;
				points[i].tangent2 = quaternion * quaternion2 * points[i].tangent2;
				points[i].normal = quaternion * quaternion2 * Vector3.forward;
			}
			for (int j = 0; j < points.Length; j++)
			{
				points[j].SetPosition(points[j].position + offset);
			}
		}

		protected void CreatePoints(int count, SplinePoint.Type type)
		{
			if (points.Length != count)
			{
				points = new SplinePoint[count];
			}
			Vector3 normal = GetNormal();
			for (int i = 0; i < points.Length; i++)
			{
				points[i].type = type;
				points[i].normal = normal;
				points[i].color = Color.white;
				points[i].size = 1f;
			}
		}

		protected Vector3 GetNormal()
		{
			return axis switch
			{
				Axis.X => Vector3.right, 
				Axis.Y => Vector3.up, 
				Axis.Z => Vector3.forward, 
				Axis.nX => Vector3.left, 
				Axis.nY => Vector3.down, 
				Axis.nZ => Vector3.back, 
				_ => Vector3.up, 
			};
		}
	}
}
