using UnityEngine;

namespace BitCode.MeshTool.DataTypes
{
	public struct TransformData
	{
		public readonly string BoneName;

		public readonly Vector3 LocalPosition;

		public readonly Quaternion LocalRotation;

		public readonly string ParentName;

		public TransformData(string boneName, Vector3 localPosition, Quaternion localRotation, string parentName = "")
		{
			BoneName = boneName;
			LocalPosition = localPosition;
			LocalRotation = localRotation;
			ParentName = parentName;
		}
	}
}
