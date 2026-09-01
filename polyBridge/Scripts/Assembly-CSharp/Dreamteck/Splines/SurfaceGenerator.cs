using UnityEngine;

namespace Dreamteck.Splines
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[AddComponentMenu("Dreamteck/Splines/Surface Generator")]
	public class SurfaceGenerator : MeshGenerator
	{
		[SerializeField]
		[HideInInspector]
		private float _expand;

		[SerializeField]
		[HideInInspector]
		private float _extrude;

		[SerializeField]
		[HideInInspector]
		private Vector2 _sideUvScale = Vector2.one;

		[SerializeField]
		[HideInInspector]
		private Vector2 _sideUvOffset = Vector2.zero;

		[SerializeField]
		[HideInInspector]
		private SplineComputer _extrudeComputer;

		[SerializeField]
		[HideInInspector]
		private SplineResult[] extrudeResults = new SplineResult[0];

		[SerializeField]
		[HideInInspector]
		private Vector3[] identityVertices = new Vector3[0];

		[SerializeField]
		[HideInInspector]
		private Vector3[] identityNormals = new Vector3[0];

		[SerializeField]
		[HideInInspector]
		private Vector2[] projectedVerts = new Vector2[0];

		[SerializeField]
		[HideInInspector]
		private int[] capTris = new int[0];

		[SerializeField]
		[HideInInspector]
		private int[] wallTris = new int[0];

		[SerializeField]
		[HideInInspector]
		private double _extrudeFrom;

		[SerializeField]
		[HideInInspector]
		private double _extrudeTo = 1.0;

		[SerializeField]
		[HideInInspector]
		private bool _uniformUvs;

		public float expand
		{
			get
			{
				return _expand;
			}
			set
			{
				if (value != _expand)
				{
					_expand = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public float extrude
		{
			get
			{
				return _extrude;
			}
			set
			{
				if (value != _extrude)
				{
					_extrude = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public double extrudeClipFrom
		{
			get
			{
				return _extrudeFrom;
			}
			set
			{
				if (value != _extrudeFrom)
				{
					_extrudeFrom = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public double extrudeClipTo
		{
			get
			{
				return _extrudeTo;
			}
			set
			{
				if (value != _extrudeTo)
				{
					_extrudeTo = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public Vector2 sideUvScale
		{
			get
			{
				return _sideUvScale;
			}
			set
			{
				if (value != _sideUvScale)
				{
					_sideUvScale = value;
					Rebuild(sampleComputer: false);
				}
				else
				{
					_sideUvScale = value;
				}
			}
		}

		public Vector2 sideUvOffset
		{
			get
			{
				return _sideUvOffset;
			}
			set
			{
				if (value != _sideUvOffset)
				{
					_sideUvOffset = value;
					Rebuild(sampleComputer: false);
				}
				else
				{
					_sideUvOffset = value;
				}
			}
		}

		public SplineComputer extrudeComputer
		{
			get
			{
				return _extrudeComputer;
			}
			set
			{
				if (value != _extrudeComputer)
				{
					if (_extrudeComputer != null)
					{
						_extrudeComputer.Unsubscribe(this);
					}
					_extrudeComputer = value;
					if (value != null)
					{
						_extrudeComputer.Subscribe(this);
					}
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool uniformUvs
		{
			get
			{
				return _uniformUvs;
			}
			set
			{
				if (value != _uniformUvs)
				{
					_uniformUvs = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			mesh.name = "surface";
		}

		protected override void BuildMesh()
		{
			if (base.computer.pointCount != 0)
			{
				base.BuildMesh();
				Generate();
			}
		}

		public void Generate()
		{
			if (_extrudeComputer != null)
			{
				_extrudeComputer.Evaluate(ref extrudeResults, _extrudeFrom, _extrudeTo);
			}
			int num = base.clippedSamples.Length;
			int num2 = 0;
			int num3 = 0;
			if (base.computer.isClosed)
			{
				num--;
			}
			bool flag = _extrudeComputer != null && extrudeResults.Length != 0;
			bool flag2 = !flag && _extrude != 0f;
			num3 = num;
			if (flag)
			{
				num2 = base.clippedSamples.Length * extrudeResults.Length;
				num3 = num * 2 + num2;
			}
			else if (flag2)
			{
				num2 = base.clippedSamples.Length * 2;
				num3 = num * 2 + num2;
			}
			AllocateMesh(num3, tsMesh.triangles.Length);
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			Vector3 vector = base.transform.right * base.offset.x + base.transform.up * base.offset.y + base.transform.forward * base.offset.z;
			for (int i = 0; i < num; i++)
			{
				tsMesh.vertices[i] = base.clippedSamples[i].position + vector;
				tsMesh.normals[i] = base.clippedSamples[i].normal;
				tsMesh.colors[i] = base.clippedSamples[i].color;
				tsMesh.colors[i] *= base.color;
				zero += tsMesh.vertices[i];
				zero2 += tsMesh.normals[i];
			}
			zero2.Normalize();
			zero /= (float)num;
			GetProjectedVertices(tsMesh.vertices, zero2, zero, num);
			Vector2 vector2 = projectedVerts[0];
			Vector2 vector3 = projectedVerts[0];
			for (int j = 1; j < projectedVerts.Length; j++)
			{
				if (vector2.x < projectedVerts[j].x)
				{
					vector2.x = projectedVerts[j].x;
				}
				if (vector2.y < projectedVerts[j].y)
				{
					vector2.y = projectedVerts[j].y;
				}
				if (vector3.x > projectedVerts[j].x)
				{
					vector3.x = projectedVerts[j].x;
				}
				if (vector3.y > projectedVerts[j].y)
				{
					vector3.y = projectedVerts[j].y;
				}
			}
			for (int k = 0; k < projectedVerts.Length; k++)
			{
				tsMesh.uv[k].x = Mathf.InverseLerp(vector3.x, vector2.x, projectedVerts[k].x) * base.uvScale.x - base.uvScale.x * 0.5f + base.uvOffset.x + 0.5f;
				tsMesh.uv[k].y = Mathf.InverseLerp(vector2.y, vector3.y, projectedVerts[k].y) * base.uvScale.y - base.uvScale.y * 0.5f + base.uvOffset.y + 0.5f;
			}
			bool flag3 = IsClockwise(projectedVerts);
			bool flag4 = false;
			bool flag5 = false;
			if (!flag3)
			{
				flag5 = !flag5;
			}
			if (flag2 && _extrude < 0f)
			{
				flag4 = !flag4;
				flag5 = !flag5;
			}
			GenerateCapTris(flag4);
			if (flag4)
			{
				for (int l = 0; l < num; l++)
				{
					tsMesh.normals[l] *= -1f;
				}
			}
			if (flag)
			{
				GetIdentityVerts(zero, zero2, flag3);
				for (int m = 0; m < num; m++)
				{
					tsMesh.vertices[m + num] = extrudeResults[0].position + extrudeResults[0].rotation * identityVertices[m] + vector;
					tsMesh.normals[m + num] = -extrudeResults[0].direction;
					tsMesh.colors[m + num] = tsMesh.colors[m] * extrudeResults[0].color;
					tsMesh.uv[m + num] = new Vector2(1f - tsMesh.uv[m].x, tsMesh.uv[m].y);
					tsMesh.vertices[m] = extrudeResults[extrudeResults.Length - 1].position + extrudeResults[extrudeResults.Length - 1].rotation * identityVertices[m] + vector;
					tsMesh.normals[m] = extrudeResults[extrudeResults.Length - 1].direction;
					tsMesh.colors[m] *= extrudeResults[extrudeResults.Length - 1].color;
				}
				float num4 = 0f;
				for (int n = 0; n < extrudeResults.Length; n++)
				{
					if (_uniformUvs && n > 0)
					{
						num4 += Vector3.Distance(extrudeResults[n].position, extrudeResults[n - 1].position);
					}
					int num5 = num * 2 + n * base.clippedSamples.Length;
					for (int num6 = 0; num6 < identityVertices.Length; num6++)
					{
						tsMesh.vertices[num5 + num6] = extrudeResults[n].position + extrudeResults[n].rotation * identityVertices[num6] + vector;
						tsMesh.normals[num5 + num6] = extrudeResults[n].rotation * identityNormals[num6];
						if (_uniformUvs)
						{
							tsMesh.uv[num5 + num6] = new Vector2((float)num6 / (float)(identityVertices.Length - 1) * _sideUvScale.x + _sideUvOffset.x, num4 * _sideUvScale.y + _sideUvOffset.y);
						}
						else
						{
							tsMesh.uv[num5 + num6] = new Vector2((float)num6 / (float)(identityVertices.Length - 1) * _sideUvScale.x + _sideUvOffset.x, (float)n / (float)(extrudeResults.Length - 1) * _sideUvScale.y + _sideUvOffset.y);
						}
						if (flag3)
						{
							tsMesh.uv[num5 + num6].x = 1f - tsMesh.uv[num5 + num6].x;
						}
					}
				}
				MeshUtility.GeneratePlaneTriangles(ref wallTris, base.clippedSamples.Length - 1, extrudeResults.Length, flag5, 0, 0, reallocateArray: true);
				tsMesh.triangles = new int[capTris.Length * 2 + wallTris.Length];
				int trisOffset = WriteTris(ref capTris, ref tsMesh.triangles, 0, 0, flip: false);
				trisOffset = WriteTris(ref capTris, ref tsMesh.triangles, num, trisOffset, flip: true);
				trisOffset = WriteTris(ref wallTris, ref tsMesh.triangles, num * 2, trisOffset, flip: false);
			}
			else if (flag2)
			{
				for (int num7 = 0; num7 < num; num7++)
				{
					tsMesh.vertices[num7 + num] = tsMesh.vertices[num7];
					if (_expand != 0f)
					{
						tsMesh.vertices[num7 + num] += (flag3 ? (-base.clippedSamples[num7].right) : base.clippedSamples[num7].right) * _expand;
					}
					tsMesh.normals[num7 + num] = -tsMesh.normals[num7];
					tsMesh.colors[num7 + num] = tsMesh.colors[num7];
					tsMesh.uv[num7 + num] = new Vector2(1f - tsMesh.uv[num7].x, tsMesh.uv[num7].y);
					tsMesh.vertices[num7] += zero2 * _extrude;
					if (_expand != 0f)
					{
						tsMesh.vertices[num7] += (flag3 ? (-base.clippedSamples[num7].right) : base.clippedSamples[num7].right) * _expand;
					}
				}
				for (int num8 = 0; num8 < base.clippedSamples.Length; num8++)
				{
					tsMesh.vertices[num8 + num * 2] = base.clippedSamples[num8].position + vector;
					if (_expand != 0f)
					{
						tsMesh.vertices[num8 + num * 2] += (flag3 ? (-base.clippedSamples[num8].right) : base.clippedSamples[num8].right) * _expand;
					}
					tsMesh.normals[num8 + num * 2] = (flag3 ? (-base.clippedSamples[num8].right) : base.clippedSamples[num8].right);
					tsMesh.colors[num8 + num * 2] = base.clippedSamples[num8].color;
					tsMesh.uv[num8 + num * 2] = new Vector2((float)num8 / (float)(num - 1) * _sideUvScale.x + _sideUvOffset.x, 0f + _sideUvOffset.y);
					if (flag3)
					{
						tsMesh.uv[num8 + num * 2].x = 1f - tsMesh.uv[num8 + num * 2].x;
					}
					tsMesh.vertices[num8 + num * 2 + base.clippedSamples.Length] = tsMesh.vertices[num8 + num * 2] + zero2 * _extrude;
					tsMesh.normals[num8 + num * 2 + base.clippedSamples.Length] = (flag3 ? (-base.clippedSamples[num8].right) : base.clippedSamples[num8].right);
					tsMesh.colors[num8 + num * 2 + base.clippedSamples.Length] = base.clippedSamples[num8].color;
					if (_uniformUvs)
					{
						tsMesh.uv[num8 + num * 2 + base.clippedSamples.Length] = new Vector2((float)num8 / (float)(num - 1) * _sideUvScale.x + _sideUvOffset.x, _extrude * _sideUvScale.y + _sideUvOffset.y);
					}
					else
					{
						tsMesh.uv[num8 + num * 2 + base.clippedSamples.Length] = new Vector2((float)num8 / (float)(num - 1) * _sideUvScale.x + _sideUvOffset.x, 1f * _sideUvScale.y + _sideUvOffset.y);
					}
					if (flag3)
					{
						tsMesh.uv[num8 + num * 2 + base.clippedSamples.Length].x = 1f - tsMesh.uv[num8 + num * 2 + base.clippedSamples.Length].x;
					}
				}
				MeshUtility.GeneratePlaneTriangles(ref wallTris, base.clippedSamples.Length - 1, 2, flag5, 0, 0, reallocateArray: true);
				int num9 = capTris.Length * 2 + wallTris.Length;
				if (base.doubleSided)
				{
					num9 *= 2;
				}
				if (tsMesh.triangles.Length != num9)
				{
					tsMesh.triangles = new int[num9];
				}
				int trisOffset2 = WriteTris(ref capTris, ref tsMesh.triangles, 0, 0, flip: false);
				trisOffset2 = WriteTris(ref capTris, ref tsMesh.triangles, num, trisOffset2, flip: true);
				trisOffset2 = WriteTris(ref wallTris, ref tsMesh.triangles, num * 2, trisOffset2, flip: false);
			}
			else
			{
				int num10 = capTris.Length;
				if (base.doubleSided)
				{
					num10 *= 2;
				}
				if (tsMesh.triangles.Length != num10)
				{
					tsMesh.triangles = new int[num10];
				}
				WriteTris(ref capTris, ref tsMesh.triangles, 0, 0, flip: false);
			}
		}

		private void GenerateCapTris(bool flip)
		{
			MeshUtility.Triangulate(projectedVerts, ref capTris);
			if (flip)
			{
				MeshUtility.FlipTriangles(ref capTris);
			}
		}

		private int WriteTris(ref int[] tris, ref int[] target, int vertexOffset, int trisOffset, bool flip)
		{
			for (int i = trisOffset; i < trisOffset + tris.Length; i += 3)
			{
				if (flip)
				{
					target[i] = tris[i + 2 - trisOffset] + vertexOffset;
					target[i + 1] = tris[i + 1 - trisOffset] + vertexOffset;
					target[i + 2] = tris[i - trisOffset] + vertexOffset;
				}
				else
				{
					target[i] = tris[i - trisOffset] + vertexOffset;
					target[i + 1] = tris[i + 1 - trisOffset] + vertexOffset;
					target[i + 2] = tris[i + 2 - trisOffset] + vertexOffset;
				}
			}
			return trisOffset + tris.Length;
		}

		private bool IsClockwise(Vector2[] points2D)
		{
			float num = 0f;
			for (int i = 1; i < points2D.Length; i++)
			{
				Vector2 vector = points2D[i];
				Vector2 vector2 = points2D[(i + 1) % points2D.Length];
				num += (vector2.x - vector.x) * (vector2.y + vector.y);
			}
			num += (points2D[0].x - points2D[^1].x) * (points2D[0].y + points2D[^1].y);
			return num <= 0f;
		}

		private void GetIdentityVerts(Vector3 center, Vector3 normal, bool clockwise)
		{
			Quaternion quaternion = Quaternion.Inverse(Quaternion.LookRotation(normal));
			if (identityVertices.Length != base.clippedSamples.Length)
			{
				identityVertices = new Vector3[base.clippedSamples.Length];
				identityNormals = new Vector3[base.clippedSamples.Length];
			}
			for (int i = 0; i < base.clippedSamples.Length; i++)
			{
				identityVertices[i] = quaternion * (base.clippedSamples[i].position - center + (clockwise ? (-base.clippedSamples[i].right) : base.clippedSamples[i].right) * _expand);
				identityNormals[i] = quaternion * (clockwise ? (-base.clippedSamples[i].right) : base.clippedSamples[i].right);
			}
		}

		private void GetProjectedVertices(Vector3[] points, Vector3 normal, Vector3 center, int count = 0)
		{
			Quaternion quaternion = Quaternion.LookRotation(normal, Vector3.up);
			Vector3 vector = quaternion * Vector3.up;
			Vector3 vector2 = quaternion * Vector3.right;
			int num = ((count > 0) ? count : points.Length);
			if (projectedVerts.Length != num)
			{
				projectedVerts = new Vector2[num];
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 vector3 = points[i] - center;
				float num2 = Vector3.Project(vector3, vector2).magnitude;
				if (Vector3.Dot(vector3, vector2) < 0f)
				{
					num2 *= -1f;
				}
				float num3 = Vector3.Project(vector3, vector).magnitude;
				if (Vector3.Dot(vector3, vector) < 0f)
				{
					num3 *= -1f;
				}
				projectedVerts[i].x = num2;
				projectedVerts[i].y = num3;
			}
		}
	}
}
