namespace GLTFast
{
	internal readonly struct GlbBinChunk
	{
		public int Start { get; }

		public uint Length { get; }

		public GlbBinChunk(int start, uint length)
		{
			Start = start;
			Length = length;
		}
	}
}
