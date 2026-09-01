using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class FollowTarget : MonoBehaviour
	{
		public enum FollowMode
		{
			Position = 0,
			Rotation = 1,
			PositionAndRotation = 2
		}

		public FollowMode followMode;

		public bool unParentOnStart;

		private Transform parent;

		public Transform target;

		[ShowIf("ValidateTarget")]
		public Vector3 rotationOffset;

		[ShowIf("ValidateTarget")]
		public Vector3 positionOffset;

		[HideIf("ValidateTarget")]
		public Vector3 targetRotation;

		[HideIf("ValidateTarget")]
		public Vector3 targetPosition;

		public float positionSpeed = 0.5f;

		public float rotationSpeed = 0.5f;

		private bool ValidateTarget()
		{
			return target != null;
		}

		private void Start()
		{
			if (unParentOnStart)
			{
				parent = base.transform.parent;
				base.transform.SetParent(null);
			}
		}

		private void Update()
		{
			if (unParentOnStart && parent == null)
			{
				Object.Destroy(base.gameObject);
			}
			Vector3 b = (ValidateTarget() ? (target.position + positionOffset) : targetPosition);
			Quaternion b2 = (ValidateTarget() ? Quaternion.Euler(target.rotation.eulerAngles + rotationOffset) : Quaternion.Euler(targetRotation));
			switch (followMode)
			{
			case FollowMode.Position:
				base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * positionSpeed);
				break;
			case FollowMode.Rotation:
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b2, Time.deltaTime * rotationSpeed);
				break;
			case FollowMode.PositionAndRotation:
				base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * positionSpeed);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b2, Time.deltaTime * rotationSpeed);
				break;
			}
		}
	}
}
