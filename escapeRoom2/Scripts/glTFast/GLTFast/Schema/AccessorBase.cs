using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class AccessorBase<TSparse> : AccessorBase where TSparse : AccessorSparseBase
	{
		public TSparse sparse;

		public override AccessorSparseBase Sparse => sparse;

		internal override void UnsetSparse()
		{
			sparse = null;
		}
	}
	[Serializable]
	public abstract class AccessorBase : NamedObject
	{
		public int bufferView = -1;

		public int byteOffset;

		public GltfComponentType componentType;

		public bool normalized;

		public int count;

		[Obsolete("Use GetAttributeType and SetAttributeType for access.")]
		public string type;

		[NonSerialized]
		private GltfAccessorAttributeType m_TypeEnum;

		public float[] max;

		public float[] min;

		public abstract AccessorSparseBase Sparse { get; }

		public bool IsSparse => Sparse != null;

		public int ElementByteSize => GetAccessorAttributeTypeLength(GetAttributeType()) * GetComponentTypeSize(componentType);

		public int ByteSize => ElementByteSize * count;

		public GltfAccessorAttributeType GetAttributeType()
		{
			if (m_TypeEnum != GltfAccessorAttributeType.Undefined)
			{
				return m_TypeEnum;
			}
			if (Enum.TryParse<GltfAccessorAttributeType>(type, ignoreCase: true, out m_TypeEnum))
			{
				type = null;
				return m_TypeEnum;
			}
			type = null;
			return GltfAccessorAttributeType.Undefined;
		}

		public void SetAttributeType(GltfAccessorAttributeType attributeType)
		{
			m_TypeEnum = attributeType;
			type = null;
		}

		internal abstract void UnsetSparse();

		public static int GetComponentTypeSize(GltfComponentType componentType)
		{
			switch (componentType)
			{
			case GltfComponentType.Byte:
			case GltfComponentType.UnsignedByte:
				return 1;
			case GltfComponentType.Short:
			case GltfComponentType.UnsignedShort:
				return 2;
			case GltfComponentType.UnsignedInt:
			case GltfComponentType.Float:
				return 4;
			default:
				throw new ArgumentOutOfRangeException("componentType", componentType, null);
			}
		}

		public static GltfComponentType GetComponentType(VertexAttributeFormat format)
		{
			switch (format)
			{
			case VertexAttributeFormat.Float32:
			case VertexAttributeFormat.Float16:
				return GltfComponentType.Float;
			case VertexAttributeFormat.UNorm8:
			case VertexAttributeFormat.UInt8:
				return GltfComponentType.UnsignedByte;
			case VertexAttributeFormat.SNorm8:
			case VertexAttributeFormat.SInt8:
				return GltfComponentType.Byte;
			case VertexAttributeFormat.UNorm16:
			case VertexAttributeFormat.UInt16:
				return GltfComponentType.UnsignedShort;
			case VertexAttributeFormat.SNorm16:
			case VertexAttributeFormat.SInt16:
				return GltfComponentType.Short;
			case VertexAttributeFormat.UInt32:
			case VertexAttributeFormat.SInt32:
				return GltfComponentType.UnsignedInt;
			default:
				throw new ArgumentOutOfRangeException("format", format, null);
			}
		}

		public static GltfAccessorAttributeType GetAccessorAttributeType(int dimension)
		{
			if (dimension < 1 || dimension > 4)
			{
				throw new ArgumentOutOfRangeException("dimension", dimension, null);
			}
			return (GltfAccessorAttributeType)dimension;
		}

		public static int GetAccessorAttributeTypeLength(GltfAccessorAttributeType type)
		{
			switch (type)
			{
			case GltfAccessorAttributeType.SCALAR:
				return 1;
			case GltfAccessorAttributeType.VEC2:
				return 2;
			case GltfAccessorAttributeType.VEC3:
				return 3;
			case GltfAccessorAttributeType.VEC4:
			case GltfAccessorAttributeType.MAT2:
				return 4;
			case GltfAccessorAttributeType.MAT3:
				return 9;
			case GltfAccessorAttributeType.MAT4:
				return 16;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		public Bounds? TryGetBounds()
		{
			if (min != null && min.Length > 2 && max != null && max.Length > 2)
			{
				float3 float5 = new float3(0f - min[0], max[1], max[2]);
				float3 float6 = new float3(0f - max[0], min[1], min[2]);
				if (normalized)
				{
					switch (componentType)
					{
					case GltfComponentType.Byte:
						float5 = math.max(float5 / 127f, -1);
						float6 = math.max(float6 / 127f, -1);
						break;
					case GltfComponentType.UnsignedByte:
						float5 /= 255f;
						float6 /= 255f;
						break;
					case GltfComponentType.Short:
						float5 = math.max(float5 / 32767f, -1);
						float6 = math.max(float6 / 32767f, -1);
						break;
					case GltfComponentType.UnsignedShort:
						float5 /= 65535f;
						float6 /= 65535f;
						break;
					case GltfComponentType.UnsignedInt:
						float5 /= 4.2949673E+09f;
						float6 /= 4.2949673E+09f;
						break;
					}
				}
				return new Bounds
				{
					max = float5,
					min = float6
				};
			}
			return null;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (bufferView >= 0)
			{
				writer.AddProperty("bufferView", bufferView);
			}
			writer.AddProperty("componentType", (int)componentType);
			writer.AddProperty("count", count);
			writer.AddProperty("type", m_TypeEnum.ToString());
			if (byteOffset > 0)
			{
				writer.AddProperty("byteOffset", byteOffset);
			}
			if (normalized)
			{
				writer.AddProperty("normalized", normalized);
			}
			if (max != null)
			{
				writer.AddArrayProperty("max", max);
			}
			if (min != null)
			{
				writer.AddArrayProperty("min", min);
			}
			if (Sparse != null)
			{
				writer.AddProperty("sparse");
				Sparse.GltfSerialize(writer);
				writer.Close();
			}
			writer.Close();
		}
	}
}
