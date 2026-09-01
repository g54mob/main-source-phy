using UnityEngine;

namespace Dreamteck.Splines
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[AddComponentMenu("Dreamteck/Splines/Waveform Generator")]
	public class WaveformGenerator : MeshGenerator
	{
		public enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		public enum Space
		{
			World = 0,
			Local = 1
		}

		public enum UVWrapMode
		{
			Clamp = 0,
			UniformX = 1,
			UniformY = 2,
			Uniform = 3
		}

		[SerializeField]
		[HideInInspector]
		private Axis _axis = Axis.Y;

		[SerializeField]
		[HideInInspector]
		private bool _symmetry;

		[SerializeField]
		[HideInInspector]
		private UVWrapMode _uvWrapMode;

		[SerializeField]
		[HideInInspector]
		private int _slices = 1;

		public Axis axis
		{
			get
			{
				return _axis;
			}
			set
			{
				if (value != _axis)
				{
					_axis = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public bool symmetry
		{
			get
			{
				return _symmetry;
			}
			set
			{
				if (value != _symmetry)
				{
					_symmetry = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public UVWrapMode uvWrapMode
		{
			get
			{
				return _uvWrapMode;
			}
			set
			{
				if (value != _uvWrapMode)
				{
					_uvWrapMode = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		public int slices
		{
			get
			{
				return _slices;
			}
			set
			{
				if (value != _slices)
				{
					if (value < 1)
					{
						value = 1;
					}
					_slices = value;
					Rebuild(sampleComputer: false);
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			mesh.name = "waveform";
		}

		protected override void BuildMesh()
		{
			base.BuildMesh();
			Generate();
		}

		protected override void Build()
		{
			base.Build();
		}

		protected override void LateRun()
		{
			base.LateRun();
		}

		private void Generate()
		{
			int vertexCount = base.clippedSamples.Length * (_slices + 1);
			AllocateMesh(vertexCount, _slices * (base.clippedSamples.Length - 1) * 6);
			int num = 0;
			float num2 = 0f;
			float num3 = 0f;
			SplineComputer splineComputer = base.rootUser.computer;
			_ = splineComputer.position;
			Vector3 vector = splineComputer.TransformDirection(Vector3.right);
			switch (_axis)
			{
			case Axis.Y:
				vector = splineComputer.TransformDirection(Vector3.up);
				break;
			case Axis.Z:
				vector = splineComputer.TransformDirection(Vector3.forward);
				break;
			}
			for (int i = 0; i < base.clippedSamples.Length; i++)
			{
				Vector3 position = base.clippedSamples[i].position;
				Vector3 vector2 = splineComputer.InverseTransformPoint(position);
				Vector3 point = vector2;
				Vector3 direction = base.clippedSamples[i].direction;
				Vector3 normal = base.clippedSamples[i].normal;
				float num4 = 1f;
				if ((_uvWrapMode == UVWrapMode.UniformX || _uvWrapMode == UVWrapMode.Uniform) && i > 0)
				{
					num3 += Vector3.Distance(base.clippedSamples[i].position, base.clippedSamples[i - 1].position);
				}
				switch (_axis)
				{
				case Axis.X:
					point.x = (_symmetry ? (0f - vector2.x) : 0f);
					num4 = base.uvScale.y * Mathf.Abs(vector2.x);
					num2 += vector2.x;
					break;
				case Axis.Y:
					point.y = (_symmetry ? (0f - vector2.y) : 0f);
					num4 = base.uvScale.y * Mathf.Abs(vector2.y);
					num2 += vector2.y;
					break;
				case Axis.Z:
					point.z = (_symmetry ? (0f - vector2.z) : 0f);
					num4 = base.uvScale.y * Mathf.Abs(vector2.z);
					num2 += vector2.z;
					break;
				}
				point = splineComputer.TransformPoint(point);
				Vector3 normalized = Vector3.Cross(vector, direction).normalized;
				Vector3 vector3 = Vector3.Cross(normal, direction);
				for (int j = 0; j < _slices + 1; j++)
				{
					float num5 = (float)j / (float)_slices;
					tsMesh.vertices[num] = Vector3.Lerp(point, position, num5) + vector * base.offset.y + vector3 * base.offset.x;
					tsMesh.normals[num] = normalized;
					switch (_uvWrapMode)
					{
					case UVWrapMode.Clamp:
						tsMesh.uv[num] = new Vector2((float)base.clippedSamples[i].percent * base.uvScale.x + base.uvOffset.x, num5 * base.uvScale.y + base.uvOffset.y);
						break;
					case UVWrapMode.UniformX:
						tsMesh.uv[num] = new Vector2(num3 * base.uvScale.x + base.uvOffset.x, num5 * base.uvScale.y + base.uvOffset.y);
						break;
					case UVWrapMode.UniformY:
						tsMesh.uv[num] = new Vector2((float)base.clippedSamples[i].percent * base.uvScale.x + base.uvOffset.x, num4 * num5 * base.uvScale.y + base.uvOffset.y);
						break;
					case UVWrapMode.Uniform:
						tsMesh.uv[num] = new Vector2(num3 * base.uvScale.x + base.uvOffset.x, num4 * num5 * base.uvScale.y + base.uvOffset.y);
						break;
					}
					tsMesh.colors[num] = base.clippedSamples[i].color * base.color;
					num++;
				}
			}
			if (base.clippedSamples.Length != 0)
			{
				num2 /= (float)base.clippedSamples.Length;
			}
			MeshUtility.GeneratePlaneTriangles(ref tsMesh.triangles, _slices, base.clippedSamples.Length, num2 < 0f);
		}
	}
}
