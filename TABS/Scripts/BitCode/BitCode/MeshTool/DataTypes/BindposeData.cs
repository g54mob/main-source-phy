using UnityEngine;

namespace BitCode.MeshTool.DataTypes
{
	public struct BindposeData
	{
		public readonly string BoneName;

		public readonly int NameHash;

		public readonly Matrix4x4 BindPose;

		public readonly bool IsRoot;

		public BindposeData(string boneName, Matrix4x4 bindPose, bool isRoot)
		{
			IsRoot = isRoot;
			BoneName = boneName;
			BindPose = bindPose;
			NameHash = boneName.GetHashCode();
		}
	}
}
