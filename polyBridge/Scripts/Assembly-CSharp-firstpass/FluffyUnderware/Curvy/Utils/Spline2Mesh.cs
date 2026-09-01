using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.ThirdParty.LibTessDotNet;
using UnityEngine;

namespace FluffyUnderware.Curvy.Utils
{
	public class Spline2Mesh
	{
		public List<SplinePolyLine> Lines = new List<SplinePolyLine>();

		public WindingRule Winding;

		public Vector2 UVTiling = Vector2.one;

		public Vector2 UVOffset = Vector2.zero;

		public bool SuppressUVMapping;

		public bool UV2;

		public string MeshName = string.Empty;

		public bool VertexLineOnly;

		private Tess mTess;

		private Mesh mMesh;

		public string Error { get; private set; }

		public bool Apply(out Mesh result)
		{
			mTess = null;
			mMesh = null;
			Error = string.Empty;
			bool flag = triangulate();
			if (flag)
			{
				mMesh = new Mesh();
				mMesh.name = MeshName;
				if (VertexLineOnly && Lines.Count > 0 && Lines[0] != null)
				{
					mMesh.vertices = Lines[0].GetVertices();
				}
				else
				{
					mMesh.vertices = UnityLibTessUtility.FromContourVertex(mTess.Vertices);
					mMesh.triangles = mTess.Elements;
				}
				mMesh.RecalculateBounds();
				mMesh.RecalculateNormals();
				if (!SuppressUVMapping && !VertexLineOnly)
				{
					Vector3 size = mMesh.bounds.size;
					Vector3 min = mMesh.bounds.min;
					float num = Mathf.Min(size.x, Mathf.Min(size.y, size.z));
					bool flag2 = num == size.x;
					bool flag3 = num == size.y;
					bool flag4 = num == size.z;
					Vector3[] vertices = mMesh.vertices;
					Vector2[] array = new Vector2[vertices.Length];
					float num2 = 0f;
					float num3 = 0f;
					for (int i = 0; i < vertices.Length; i++)
					{
						float num4;
						float num5;
						if (flag2)
						{
							num4 = UVOffset.x + (vertices[i].y - min.y) / size.y;
							num5 = UVOffset.y + (vertices[i].z - min.z) / size.z;
						}
						else if (flag3)
						{
							num4 = UVOffset.x + (vertices[i].z - min.z) / size.z;
							num5 = UVOffset.y + (vertices[i].x - min.x) / size.x;
						}
						else
						{
							if (!flag4)
							{
								throw new InvalidOperationException("Couldn't find the minimal bound dimension");
							}
							num4 = UVOffset.x + (vertices[i].x - min.x) / size.x;
							num5 = UVOffset.y + (vertices[i].y - min.y) / size.y;
						}
						num4 *= UVTiling.x;
						num5 *= UVTiling.y;
						num2 = ((num4 < num2) ? num2 : num4);
						num3 = ((num5 < num3) ? num3 : num5);
						array[i].x = num4;
						array[i].y = num5;
					}
					mMesh.uv = array;
					Vector2[] array2 = new Vector2[0];
					if (UV2)
					{
						array2 = new Vector2[array.Length];
						float num6 = 1f / num2;
						float num7 = 1f / num3;
						for (int j = 0; j < vertices.Length; j++)
						{
							array2[j].x = array[j].x * num6;
							array2[j].y = array[j].y * num7;
						}
					}
					mMesh.uv2 = array2;
				}
			}
			result = mMesh;
			return flag;
		}

		private bool triangulate()
		{
			if (Lines.Count == 0)
			{
				Error = "Missing splines to triangulate";
				return false;
			}
			if (VertexLineOnly)
			{
				return true;
			}
			mTess = new Tess();
			for (int i = 0; i < Lines.Count; i++)
			{
				if (Lines[i].Spline == null)
				{
					Error = "Missing Spline";
					return false;
				}
				if (!polyLineIsValid(Lines[i]))
				{
					Error = Lines[i].Spline.name + ": Angle must be >0";
					return false;
				}
				Vector3[] vertices = Lines[i].GetVertices();
				if (vertices.Length < 3)
				{
					Error = Lines[i].Spline.name + ": At least 3 Vertices needed!";
					return false;
				}
				mTess.AddContour(UnityLibTessUtility.ToContourVertex(vertices), Lines[i].Orientation);
			}
			try
			{
				mTess.Tessellate(Winding, ElementType.Polygons, 3);
				return true;
			}
			catch (Exception ex)
			{
				Error = ex.Message;
			}
			return false;
		}

		private static bool polyLineIsValid(SplinePolyLine pl)
		{
			if (pl == null || pl.VertexMode != SplinePolyLine.VertexCalculation.ByApproximation)
			{
				return !Mathf.Approximately(0f, pl.Angle);
			}
			return true;
		}
	}
}
