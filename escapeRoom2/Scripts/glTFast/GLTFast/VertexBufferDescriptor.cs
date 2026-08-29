using System;
using GLTFast.Schema;

namespace GLTFast
{
	internal readonly struct VertexBufferDescriptor : IEquatable<VertexBufferDescriptor>
	{
		private readonly bool m_HasNormals;

		private readonly bool m_HasTangents;

		private readonly int m_TexCoordCount;

		private readonly bool m_HasColors;

		private readonly bool m_HasBones;

		private readonly int m_MorphTargetCount;

		private VertexBufferDescriptor(bool hasNormals, bool hasTangents, int texCoordCount, bool hasColors, bool hasBones, int morphTargetCount)
		{
			m_HasNormals = hasNormals;
			m_HasTangents = hasTangents;
			m_TexCoordCount = texCoordCount;
			m_HasColors = hasColors;
			m_HasBones = hasBones;
			m_MorphTargetCount = morphTargetCount;
		}

		public static VertexBufferDescriptor FromPrimitive(MeshPrimitiveBase primitive)
		{
			bool hasNormals = primitive.attributes.NORMAL >= 0;
			bool hasTangents = primitive.attributes.TANGENT >= 0;
			int texCoordsCount = primitive.attributes.GetTexCoordsCount();
			bool hasColors = primitive.attributes.COLOR_0 >= 0;
			bool hasBones = primitive.attributes.WEIGHTS_0 >= 0 && primitive.attributes.JOINTS_0 >= 0;
			MorphTarget[] targets = primitive.targets;
			return new VertexBufferDescriptor(hasNormals, hasTangents, texCoordsCount, hasColors, hasBones, (targets != null) ? targets.Length : 0);
		}

		public override int GetHashCode()
		{
			int num = 13;
			if (m_HasNormals)
			{
				num = num * 31 + 13;
			}
			if (m_HasTangents)
			{
				num = num * 31 + 14;
			}
			num = num * 31 + m_TexCoordCount;
			if (m_HasColors)
			{
				num = num * 31 + 15;
			}
			if (m_HasBones)
			{
				num = num * 31 + 16;
			}
			return num * 31 + m_MorphTargetCount;
		}

		public override bool Equals(object? obj)
		{
			if (obj is VertexBufferDescriptor other)
			{
				return Equals(other);
			}
			return false;
		}

		public bool Equals(VertexBufferDescriptor other)
		{
			if (m_HasNormals == other.m_HasNormals && m_HasTangents == other.m_HasTangents && m_TexCoordCount == other.m_TexCoordCount && m_HasColors == other.m_HasColors && m_HasBones == other.m_HasBones)
			{
				return m_MorphTargetCount == other.m_MorphTargetCount;
			}
			return false;
		}

		public static bool operator ==(VertexBufferDescriptor lhs, VertexBufferDescriptor rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(VertexBufferDescriptor lhs, VertexBufferDescriptor rhs)
		{
			return !(lhs == rhs);
		}
	}
}
