using System;

namespace Photon.Bolt
{
	public struct ArrayIndices
	{
		private readonly int[] indices;

		public int Length
		{
			get
			{
				if (indices != null)
				{
					return indices.Length;
				}
				return 0;
			}
		}

		[Documentation(Ignore = true)]
		public int this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
				{
					throw new IndexOutOfRangeException();
				}
				return indices[index];
			}
		}

		internal ArrayIndices(int[] indices)
		{
			this.indices = indices;
		}
	}
}
