using UnityEngine;

[ExecuteAlways]
public class FakeParentConstraint : MonoBehaviour
{
	[Tooltip("The transform to use as the fake parent.")]
	public Transform fakeParent;

	[Tooltip("Enable to constrain position and rotation to the fake parent (with offset).")]
	public bool constraintActive;

	private Vector3 positionOffset;

	private Quaternion rotationOffset;

	private bool previousConstraintActive;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void CacheOffset()
	{
	}

	private void ApplyConstraint()
	{
	}

	private void ResetToLocalZero()
	{
	}
}
