using UnityEngine;

namespace Meta.XR.EnvironmentDepth
{
	internal struct DepthFrameDesc
	{
		internal Vector3 createPoseLocation;

		internal Vector4 createPoseRotation;

		internal float fovLeftAngle;

		internal float fovRightAngle;

		internal float fovTopAngle;

		internal float fovDownAngle;

		internal float nearZ;

		internal float farZ;
	}
}
