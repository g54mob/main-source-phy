using UnityEngine;

public class LookTowardTarget : MonoBehaviour
{
	[Header("Target")]
	[Tooltip("The transform this object will rotate toward.\nCan be changed at runtime; the script reacts on the next frame.")]
	[SerializeField]
	private Transform _target;

	[Header("Weight")]
	[Tooltip("How strongly this object rotates toward the target.\n\n  0 = no offset from base rotation (rest pose).\n  1 = fully facing the target.\n\nValues outside [0, 1] are clamped at runtime.\nDrive this from an Animator, Timeline signal, or code to blend the look.")]
	[Range(0f, 1f)]
	[SerializeField]
	private float _weight;

	[Header("Base Rotation")]
	[Tooltip("Capture the base (zero-weight) LOCAL-space rotation from the object's current local orientation when the component first enables. Because it is stored in local space, a parent dolly can freely move and rotate without affecting the rest pose.\n\nDisable if you want to set the base rotation manually at runtime or reset it yourself via ResetBaseRotation().")]
	[SerializeField]
	private bool _captureBaseOnEnable;

	[Header("Smoothing")]
	[Tooltip("Seconds for the rotation to reach its desired value (SmoothDamp damping on the slerp parameter).  0 = instant, no smoothing.\nUseful for preventing snappy transitions when Weight changes quickly.")]
	[SerializeField]
	[Min(0f)]
	private float _smoothTime;

	private Quaternion _baseLocalRotation;

	private float _smoothedWeight;

	private float _weightVelocity;

	public float Weight
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public Transform Target
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public void ResetBaseRotation()
	{
	}

	private void OnEnable()
	{
	}

	private void LateUpdate()
	{
	}

	private void ApplyRotation()
	{
	}

	private Quaternion ComputeDesiredLocalRotation()
	{
		return default(Quaternion);
	}

	private static Quaternion StripRoll(Quaternion q)
	{
		return default(Quaternion);
	}
}
