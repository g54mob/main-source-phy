using System;
using System.Collections.Generic;

namespace GLTFast
{
	internal readonly struct MeshOrder : IDisposable
	{
		public readonly MeshGeneratorBase generator;

		private readonly List<MeshSubset> m_Recipients;

		public IReadOnlyList<MeshSubset> Recipients => m_Recipients;

		public MeshOrder(MeshGeneratorBase generator)
		{
			this.generator = generator;
			m_Recipients = new List<MeshSubset>();
		}

		public void AddRecipient(MeshSubset subset)
		{
			m_Recipients.Add(subset);
		}

		public void Dispose()
		{
			generator?.Dispose();
		}
	}
}
