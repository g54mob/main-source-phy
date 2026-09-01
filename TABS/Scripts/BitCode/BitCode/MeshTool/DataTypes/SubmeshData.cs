namespace BitCode.MeshTool.DataTypes
{
	public struct SubmeshData
	{
		public readonly int Identifier;

		public readonly int[] TriangleList;

		public SubmeshData(int[] triangleList, int identifier)
		{
			Identifier = identifier;
			TriangleList = triangleList;
		}
	}
}
