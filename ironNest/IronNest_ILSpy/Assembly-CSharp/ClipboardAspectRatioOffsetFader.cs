using Cpp2ILInjected;
using UnityEngine;

public class ClipboardAspectRatioOffsetFader : MonoBehaviour
{
	public enum LocalAxis
	{
		X,
		Y,
		Z
	}

	private ClipboardStateController clipboardController;

	private Transform target;

	private LocalAxis axis;

	private float aspectRatioOffsetAmount;

	private float smoothTime = 0.03f;

	private bool recaptureBaselineOnResolutionChange = true;

	private bool forceZeroWithoutController;

	private bool logWarnings;

	private Vector3 _baselineLocalPos;

	private float _currentAppliedOffset;

	private float _offsetVelocity;

	private int _lastScreenW;

	private int _lastScreenH;

	public float AspectRatioOffsetAmount => aspectRatioOffsetAmount;

	public void SetAspectRatioOffsetAmount(float amount)
	{
		aspectRatioOffsetAmount = amount;
	}

	private void Awake()
	{
		//IL_0146: Expected F4, but got I4
		if (target == null)
		{
			Transform transform = base.transform;
			target = transform;
		}
		if (clipboardController == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
			ClipboardStateController clipboardStateController = default(ClipboardStateController);
			clipboardController = clipboardStateController;
		}
		CaptureBaseline();
		int width = Screen.width;
		_lastScreenW = width;
		int height = Screen.height;
		_lastScreenH = height;
		ApplyOffset(_currentAppliedOffset = ((!(clipboardController != null) || !clipboardController.isActiveAndEnabled || !clipboardController.IsFocused) ? aspectRatioOffsetAmount : 0f));
	}

	private void OnEnable()
	{
		CaptureBaseline();
		ApplyOffset(_currentAppliedOffset);
	}

	private unsafe void Update()
	{
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected Ref, but got Unknown
		//IL_0188: Expected F4, but got I4
		bool flag = target == null;
		if (flag)
		{
			return;
		}
		if (recaptureBaselineOnResolutionChange != flag)
		{
			int width = Screen.width;
			if (width == _lastScreenW)
			{
				int height = Screen.height;
				if (height == _lastScreenH)
				{
					goto IL_00cd;
				}
			}
			int width2 = Screen.width;
			_lastScreenW = width2;
			int height2 = Screen.height;
			_lastScreenH = height2;
			CaptureBaseline();
		}
		goto IL_00cd;
		IL_00cd:
		bool flag4;
		if (clipboardController != null)
		{
			bool flag2 = clipboardController.isActiveAndEnabled;
			if (flag2)
			{
				bool isFocused = clipboardController.IsFocused;
				bool flag3 = !isFocused;
				flag4 = flag2;
				if (!flag3)
				{
					goto IL_017f;
				}
				goto IL_01a2;
			}
		}
		flag4 = false;
		goto IL_01a2;
		IL_01ba:
		bool flag5 = !(0.0001f < smoothTime);
		float num = 0.0001f;
		if (!flag5)
		{
			num = smoothTime;
		}
		float deltaTime = Time.deltaTime;
		float num2;
		float maxSpeed = default(float);
		float deltaTime2 = default(float);
		ApplyOffset(_currentAppliedOffset = Mathf.SmoothDamp(_currentAppliedOffset, num2, ref *(float*)(this + 80), num, maxSpeed, deltaTime2));
		return;
		IL_017f:
		num2 = 0f;
		goto IL_01ba;
		IL_01a2:
		num2 = aspectRatioOffsetAmount;
		if (!flag4 && forceZeroWithoutController != flag4)
		{
			goto IL_017f;
		}
		goto IL_01ba;
	}

	private void CaptureBaseline()
	{
		//IL_0095: Expected O, but got F4
		bool flag = target != null;
		if (!flag)
		{
			if (logWarnings != flag)
			{
				string text = base.name;
				string message = text + ": ClipboardAspectRatioOffsetFader has no target Transform.";
				Debug.LogWarning(message, this);
			}
		}
		else
		{
			Vector3 localPosition = target.localPosition;
			_baselineLocalPos = (Vector3)localPosition.x;
			_ = localPosition.z;
		}
	}

	private unsafe void ApplyOffset(float offset)
	{
		//IL_0062: Expected O, but got Ref
		if (axis == LocalAxis.X || axis == LocalAxis.Y)
		{
		}
		Vector3 vector = default(Vector3);
		target.localPosition = (Vector3)(&vector);
	}
}
