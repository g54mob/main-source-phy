namespace GLTFast.Vertex
{
	internal struct VBones
	{
		public unsafe fixed float weights[4];

		public unsafe fixed uint joints[4];
	}
}
