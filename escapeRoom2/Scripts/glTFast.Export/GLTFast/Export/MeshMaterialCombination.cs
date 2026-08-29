namespace GLTFast.Export
{
	internal readonly struct MeshMaterialCombination
	{
		private readonly int m_MeshId;

		private readonly int[] m_MaterialIds;

		public MeshMaterialCombination(int meshId, int[] materialIds)
		{
			m_MeshId = meshId;
			m_MaterialIds = materialIds;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			return Equals((MeshMaterialCombination)obj);
		}

		private bool Equals(MeshMaterialCombination other)
		{
			if (m_MeshId == other.m_MeshId)
			{
				return Equals(m_MaterialIds, other.m_MaterialIds);
			}
			return false;
		}

		private static bool Equals(int[] a, int[] b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if ((a == null) ^ (b == null))
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = 17;
			int num2 = num * 31;
			int meshId = m_MeshId;
			num = num2 + meshId.GetHashCode();
			if (m_MaterialIds != null)
			{
				num = num * 31 + m_MaterialIds.Length;
				int[] materialIds = m_MaterialIds;
				foreach (int num3 in materialIds)
				{
					num = num * 31 + num3;
				}
			}
			return num;
		}
	}
}
