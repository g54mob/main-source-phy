using System;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast
{
	internal struct SamplerKey : IEquatable<SamplerKey>
	{
		private FilterMode m_FilterMode;

		private TextureWrapMode m_WrapModeU;

		private TextureWrapMode m_WrapModeV;

		public SamplerKey(Sampler sampler)
		{
			m_FilterMode = sampler.FilterMode;
			m_WrapModeU = sampler.WrapU;
			m_WrapModeV = sampler.WrapV;
		}

		public SamplerKey(FilterMode filterMode, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV)
		{
			m_FilterMode = filterMode;
			m_WrapModeU = wrapModeU;
			m_WrapModeV = wrapModeV;
		}

		public override int GetHashCode()
		{
			return (m_FilterMode, m_WrapModeU, m_WrapModeV).GetHashCode();
		}

		public bool Equals(SamplerKey other)
		{
			if (m_FilterMode == other.m_FilterMode && m_WrapModeU == other.m_WrapModeU)
			{
				return m_WrapModeV == other.m_WrapModeV;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is SamplerKey other)
			{
				return Equals(other);
			}
			return false;
		}
	}
}
