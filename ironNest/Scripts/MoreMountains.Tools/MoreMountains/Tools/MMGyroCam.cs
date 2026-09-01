using System;
using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	[AddComponentMenu("More Mountains/Tools/Cinemachine/MMGyroCam")]
	public class MMGyroCam
	{
		public CinemachineCamera Cam;

		public Transform LookAt;

		public Transform RotationCenter;

		public Vector2 MinRotation;

		public Vector2 MaxRotation;

		public Transform AnimatedPosition;

		[MMReadOnly]
		public Vector3 InitialAngles;

		[MMReadOnly]
		public Vector3 InitialPosition;
	}
}
