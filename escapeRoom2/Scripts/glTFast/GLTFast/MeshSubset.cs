namespace GLTFast
{
	internal readonly struct MeshSubset
	{
		public readonly int meshIndex;

		public readonly int meshNumeration;

		public readonly int[] primitives;

		public MeshSubset(int meshIndex, int meshNumeration, int[] primitives)
		{
			this.meshIndex = meshIndex;
			this.meshNumeration = meshNumeration;
			this.primitives = primitives;
		}
	}
}
