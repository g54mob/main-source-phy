using UnityEngine;

namespace Dreamteck.Splines
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[AddComponentMenu("Dreamteck/Splines/Tube Generator")]
	public class TubeGenerator : MeshGenerator
	{
		public enum CapMethod
		{
			None = 0,
			Flat = 1,
			Round = 2
		}

		[SerializeField]
		[HideInInspector]
		private int _sides = 12;

		[SerializeField]
		[HideInInspector]
		private int _roundCapLatitude = 6;

		[SerializeField]
		[HideInInspector]
		private CapMethod _capMode;

		[SerializeField]
		[HideInInspector]
		private float _integrity = 360f;

		[SerializeField]
		[HideInInspector]
		private float _capUVScale = 1f;

		private int bodyVertexCount;

		private int bodyTrisCount;

		private int capVertexCount;

		private int capTrisCount;

		public int sides
		{
			get
			{
				return _sides;
			}
			set
			{
				if (value != _sides)
				{
					if (value < 3)
					{
						value = 3;
					}
					_sides = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public CapMethod capMode
		{
			get
			{
				return _capMode;
			}
			set
			{
				if (value != _capMode)
				{
					_capMode = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public int roundCapLatitude
		{
			get
			{
				return _roundCapLatitude;
			}
			set
			{
				if (value < 1)
				{
					value = 1;
				}
				if (value != _roundCapLatitude)
				{
					_roundCapLatitude = value;
					if (_capMode == CapMethod.Round)
					{
						Rebuild(sampleComputer: false);
					}
				}
			}
		}

		public float integrity
		{
			get
			{
				return _integrity;
			}
			set
			{
				if (value != _integrity)
				{
					_integrity = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public float capUVScale
		{
			get
			{
				return _capUVScale;
			}
			set
			{
				if (value != _capUVScale)
				{
					_capUVScale = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		private bool useCap
		{
			get
			{
				bool flag = _capMode != CapMethod.None;
				if (base.computer != null)
				{
					if (flag)
					{
						if (base.computer.isClosed)
						{
							return base.span < 1.0;
						}
						return true;
					}
					return false;
				}
				if (sampleUser)
				{
					SplineUser splineUser = base.rootUser;
					if (splineUser == null)
					{
						return flag;
					}
					if (splineUser.computer != null)
					{
						if (flag)
						{
							if (splineUser.computer.isClosed)
							{
								return splineUser.span < 1.0;
							}
							return true;
						}
						return false;
					}
				}
				return flag;
			}
		}

		protected override void Reset()
		{
			base.Reset();
		}

		protected override void Awake()
		{
			base.Awake();
			mesh.name = "tube";
		}

		protected override void BuildMesh()
		{
			if (_sides > 2)
			{
				base.BuildMesh();
				bodyVertexCount = (_sides + 1) * base.clippedSamples.Length;
				CapMethod capMethod = _capMode;
				if (!useCap)
				{
					capMethod = CapMethod.None;
				}
				switch (capMethod)
				{
				case CapMethod.Flat:
					capVertexCount = _sides + 1;
					break;
				case CapMethod.Round:
					capVertexCount = _roundCapLatitude * (sides + 1);
					break;
				default:
					capVertexCount = 0;
					break;
				}
				int vertexCount = bodyVertexCount + capVertexCount * 2;
				bodyTrisCount = _sides * (base.clippedSamples.Length - 1) * 2 * 3;
				switch (capMethod)
				{
				case CapMethod.Flat:
					capTrisCount = (_sides - 1) * 3 * 2;
					break;
				case CapMethod.Round:
					capTrisCount = _sides * _roundCapLatitude * 6;
					break;
				default:
					capTrisCount = 0;
					break;
				}
				AllocateMesh(vertexCount, bodyTrisCount + capTrisCount * 2);
				Generate();
				switch (capMethod)
				{
				case CapMethod.Flat:
					GenerateFlatCaps();
					break;
				case CapMethod.Round:
					GenerateRoundCaps();
					break;
				}
			}
		}

		private void Generate()
		{
			int num = 0;
			ResetUVDistance();
			for (int i = 0; i < base.clippedSamples.Length; i++)
			{
				Vector3 position = base.clippedSamples[i].position;
				Vector3 right = base.clippedSamples[i].right;
				if (base.offset != Vector3.zero)
				{
					position += base.offset.x * right + base.offset.y * base.clippedSamples[i].normal + base.offset.z * base.clippedSamples[i].direction;
				}
				if (base.uvMode == UVMode.UniformClamp || base.uvMode == UVMode.UniformClip)
				{
					AddUVDistance(i);
				}
				for (int j = 0; j < _sides + 1; j++)
				{
					float num2 = (float)j / (float)_sides;
					Quaternion quaternion = Quaternion.AngleAxis(_integrity * num2 + base.rotation + 180f, base.clippedSamples[i].direction);
					tsMesh.vertices[num] = position + quaternion * right * base.size * base.clippedSamples[i].size * 0.5f;
					CalculateUVs(base.clippedSamples[i].percent, num2);
					tsMesh.uv[num] = Vector2.one * 0.5f + (Vector2)(Quaternion.AngleAxis(base.uvRotation, Vector3.forward) * (Vector2.one * 0.5f - MeshGenerator.uvs));
					tsMesh.normals[num] = Vector3.Normalize(tsMesh.vertices[num] - position);
					tsMesh.colors[num] = base.clippedSamples[i].color * base.color;
					num++;
				}
			}
			MeshUtility.GeneratePlaneTriangles(ref tsMesh.triangles, _sides, base.clippedSamples.Length, flip: false);
		}

		private void GenerateFlatCaps()
		{
			for (int i = 0; i < _sides + 1; i++)
			{
				int num = bodyVertexCount + i;
				tsMesh.vertices[num] = tsMesh.vertices[i];
				tsMesh.normals[num] = -base.clippedSamples[0].direction;
				tsMesh.colors[num] = tsMesh.colors[i];
				tsMesh.uv[num] = Quaternion.AngleAxis(_integrity * ((float)i / (float)(_sides - 1)), Vector3.forward) * Vector2.right * 0.5f * capUVScale + Vector3.right * 0.5f + Vector3.up * 0.5f;
			}
			for (int j = 0; j < _sides + 1; j++)
			{
				int num2 = bodyVertexCount + (_sides + 1) + j;
				int num3 = bodyVertexCount - (_sides + 1) + j;
				tsMesh.vertices[num2] = tsMesh.vertices[num3];
				tsMesh.normals[num2] = base.clippedSamples[base.clippedSamples.Length - 1].direction;
				tsMesh.colors[num2] = tsMesh.colors[num3];
				tsMesh.uv[num2] = Quaternion.AngleAxis(_integrity * ((float)num3 / (float)(_sides - 1)), Vector3.forward) * Vector2.right * 0.5f * capUVScale + Vector3.right * 0.5f + Vector3.up * 0.5f;
			}
			int num4 = bodyTrisCount;
			int num5 = ((_integrity == 360f) ? (_sides - 1) : _sides);
			for (int k = 0; k < num5 - 1; k++)
			{
				tsMesh.triangles[num4++] = k + bodyVertexCount + 2;
				tsMesh.triangles[num4++] = k + bodyVertexCount + 1;
				tsMesh.triangles[num4++] = bodyVertexCount;
			}
			for (int l = 0; l < num5 - 1; l++)
			{
				tsMesh.triangles[num4++] = bodyVertexCount + (_sides + 1);
				tsMesh.triangles[num4++] = l + 1 + bodyVertexCount + (_sides + 1);
				tsMesh.triangles[num4++] = l + 2 + bodyVertexCount + (_sides + 1);
			}
		}

		private void GenerateRoundCaps()
		{
			Vector3 position = base.clippedSamples[0].position;
			Quaternion quaternion = Quaternion.LookRotation(-base.clippedSamples[0].direction, base.clippedSamples[0].normal);
			float num = 0f;
			float num2 = 0f;
			switch (base.uvMode)
			{
			case UVMode.Clip:
				num = (float)base.clippedSamples[0].percent;
				num2 = base.size * 0.5f / CalculateLength();
				break;
			case UVMode.UniformClip:
				num = CalculateLength(0.0, base.clippedSamples[0].percent);
				num2 = base.size * 0.5f;
				break;
			case UVMode.UniformClamp:
				num = 0f;
				num2 = base.size * 0.5f / (float)base.span;
				break;
			case UVMode.Clamp:
				num2 = base.size * 0.5f / CalculateLength(base.clipFrom, base.clipTo);
				break;
			}
			for (int i = 1; i < _roundCapLatitude + 1; i++)
			{
				float num3 = (float)i / (float)_roundCapLatitude;
				float angle = 90f * num3;
				for (int j = 0; j <= sides; j++)
				{
					float num4 = (float)j / (float)sides;
					int num5 = bodyVertexCount + j + (i - 1) * (sides + 1);
					Quaternion quaternion2 = Quaternion.AngleAxis(_integrity * num4 + base.rotation + 180f, -Vector3.forward) * Quaternion.AngleAxis(angle, Vector3.up);
					tsMesh.vertices[num5] = position + quaternion * quaternion2 * -Vector3.right * base.size * 0.5f * base.clippedSamples[0].size;
					tsMesh.colors[num5] = base.clippedSamples[0].color * base.color;
					tsMesh.normals[num5] = (tsMesh.vertices[num5] - position).normalized;
					tsMesh.uv[num5] = new Vector2(num4 * base.uvScale.x, (num - num2 * num3) * base.uvScale.y) - base.uvOffset;
				}
			}
			int num6 = bodyTrisCount;
			for (int k = -1; k < _roundCapLatitude - 1; k++)
			{
				for (int l = 0; l < sides; l++)
				{
					int num7 = bodyVertexCount + l + k * (sides + 1);
					int num8 = num7 + (sides + 1);
					if (k == -1)
					{
						num7 = l;
						num8 = bodyVertexCount + l;
					}
					tsMesh.triangles[num6++] = num8 + 1;
					tsMesh.triangles[num6++] = num7 + 1;
					tsMesh.triangles[num6++] = num7;
					tsMesh.triangles[num6++] = num8;
					tsMesh.triangles[num6++] = num8 + 1;
					tsMesh.triangles[num6++] = num7;
				}
			}
			position = base.clippedSamples[base.clippedSamples.Length - 1].position;
			quaternion = Quaternion.LookRotation(base.clippedSamples[base.clippedSamples.Length - 1].direction, base.clippedSamples[base.clippedSamples.Length - 1].normal);
			switch (base.uvMode)
			{
			case UVMode.Clip:
				num = (float)base.clippedSamples[base.clippedSamples.Length - 1].percent;
				break;
			case UVMode.UniformClip:
				num = CalculateLength(0.0, base.clippedSamples[base.clippedSamples.Length - 1].percent);
				break;
			case UVMode.Clamp:
				num = 1f;
				break;
			case UVMode.UniformClamp:
				num = CalculateLength();
				break;
			}
			for (int m = 1; m < _roundCapLatitude + 1; m++)
			{
				float num9 = (float)m / (float)_roundCapLatitude;
				float angle2 = 90f * num9;
				for (int n = 0; n <= sides; n++)
				{
					float num10 = (float)n / (float)sides;
					int num11 = bodyVertexCount + capVertexCount + n + (m - 1) * (sides + 1);
					Quaternion quaternion3 = Quaternion.AngleAxis(_integrity * num10 + base.rotation + 180f, Vector3.forward) * Quaternion.AngleAxis(angle2, -Vector3.up);
					tsMesh.vertices[num11] = position + quaternion * quaternion3 * Vector3.right * base.size * 0.5f * base.clippedSamples[base.clippedSamples.Length - 1].size;
					tsMesh.normals[num11] = (tsMesh.vertices[num11] - position).normalized;
					tsMesh.colors[num11] = base.clippedSamples[base.clippedSamples.Length - 1].color * base.color;
					tsMesh.uv[num11] = new Vector2(num10 * base.uvScale.x, (num + num2 * num9) * base.uvScale.y) - base.uvOffset;
				}
			}
			for (int num12 = -1; num12 < _roundCapLatitude - 1; num12++)
			{
				for (int num13 = 0; num13 < sides; num13++)
				{
					int num14 = bodyVertexCount + capVertexCount + num13 + num12 * (sides + 1);
					int num15 = num14 + (sides + 1);
					if (num12 == -1)
					{
						num14 = bodyVertexCount - (_sides + 1) + num13;
						num15 = bodyVertexCount + capVertexCount + num13;
					}
					tsMesh.triangles[num6++] = num14 + 1;
					tsMesh.triangles[num6++] = num15 + 1;
					tsMesh.triangles[num6++] = num15;
					tsMesh.triangles[num6++] = num15;
					tsMesh.triangles[num6++] = num14;
					tsMesh.triangles[num6++] = num14 + 1;
				}
			}
		}
	}
}
