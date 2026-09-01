using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Collider))]
	public class MMCinemachineZone3D : MMCinemachineZone
	{
		protected Collider _collider;

		protected Collider _confinerCollider;

		protected Rigidbody _confinerRigidbody;

		protected BoxCollider _boxCollider;

		protected SphereCollider _sphereCollider;

		protected CinemachineConfiner3D _cinemachineConfiner;

		protected override void InitializeCollider()
		{
		}

		protected override void SetupConfiner()
		{
		}

		protected virtual void CopyCollider()
		{
		}

		protected virtual void OnTriggerEnter(Collider collider)
		{
		}

		protected virtual void OnTriggerExit(Collider collider)
		{
		}
	}
}
