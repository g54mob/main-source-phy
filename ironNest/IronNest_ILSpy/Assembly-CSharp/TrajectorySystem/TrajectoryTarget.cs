using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace TrajectorySystem;

public sealed class TrajectoryTarget : MonoBehaviour
{
	private Vector3 defaultLocalPosition;

	private float maxResetSpeed;

	private Vector3 followLocalOffset;

	private bool drawGizmos;

	private Color gizmoDefaultColor;

	private Color gizmoOffsetColor;

	private float gizmoSphereRadius;

	public UnityEvent OnTargetLocked;

	public UnityEvent OnTargetLost;

	public UnityEvent OnResetRequested;

	private bool _003CIsClaimed_003Ek__BackingField;

	private UnityEngine.Object _003CCurrentOwner_003Ek__BackingField;

	private bool isResetting;

	public bool IsClaimed
	{
		get
		{
			return _003CIsClaimed_003Ek__BackingField;
		}
		private set
		{
			_003CIsClaimed_003Ek__BackingField = value;
		}
	}

	public UnityEngine.Object CurrentOwner
	{
		get
		{
			return _003CCurrentOwner_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentOwner_003Ek__BackingField = value;
		}
	}

	public unsafe Vector3 FollowLocalOffset
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)followLocalOffset;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TrajectorySystem.TrajectoryTarget)+38]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
	}

	private void Reset()
	{
		//IL_002b: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		defaultLocalPosition = (Vector3)localPosition.x;
		_ = localPosition.z;
	}

	private unsafe void Update()
	{
		//IL_009e: Invalid comparison between I4 and F4
		//IL_00b0: Expected F4, but got I4
		//IL_03cc: Invalid comparison between I4 and F4
		//IL_0389: Expected O, but got Ref
		//IL_0474: Invalid comparison between F4 and I4
		//IL_0164: Expected F4, but got O
		//IL_0174: Expected F4, but got I
		//IL_017e: Expected F4, but got O
		//IL_0105: Invalid comparison between F4 and I4
		//IL_02b1: Expected O, but got Ref
		//IL_0199: Expected I, but got O
		//IL_036c: Expected O, but got Ref
		//IL_01e0: Expected F8, but got I4
		if (_003CIsClaimed_003Ek__BackingField && _003CCurrentOwner_003Ek__BackingField == null)
		{
			_003CIsClaimed_003Ek__BackingField = false;
			_003CCurrentOwner_003Ek__BackingField = null;
			if (OnTargetLost != null)
			{
				OnTargetLost.Invoke();
			}
		}
		if (!isResetting)
		{
			return;
		}
		bool flag = !(0f < maxResetSpeed);
		float num = 0f;
		if (!flag)
		{
			num = maxResetSpeed;
		}
		Transform transform = base.transform;
		object obj2 = default(object);
		float num17;
		double num19;
		float num20;
		float num21;
		if (0f < num)
		{
			Vector3 localPosition = transform.localPosition;
			float deltaTime = Time.deltaTime;
			float num2 = deltaTime * num;
			float num3 = (float)defaultLocalPosition - localPosition.x;
			object obj3 = default(object);
			object obj = obj2 - obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TrajectorySystem.TrajectoryTarget)+28]");
			float num4 = 0f - localPosition.z;
			float num5 = num3 * num3;
			object obj4 = obj * obj;
			float num6 = num4 * num4;
			float num7 = (float)obj4 + num5;
			float num8 = num7 + num6;
			bool flag2 = num8 == 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804BF732h\"");
			if (flag2)
			{
				goto IL_015a;
			}
			if (!(num2 < 0f))
			{
				float num9 = num2 * num2;
				if (!(num9 < num8))
				{
					goto IL_015a;
				}
			}
			nint num10 = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v583 @ rcx_v17 (Il2CppClass<System.Math>)+E4]");
			double num11;
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				num11 = 0.0;
			}
			else
			{
				num11 = Math.Sqrt(num8);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
			float num12 = num3 / (float)num11;
			double num13 = (double)obj / num11;
			float num14 = num4 / (float)num11;
			float num15 = num12 * num2;
			double num16 = num13 * (double)num2;
			num17 = num15 + localPosition.x;
			float num18 = num14 * num2;
			num19 = num16 + (double)obj3;
			num20 = num18 + localPosition.z;
			num21 = localPosition.x;
			goto IL_0492;
		}
		Vector3 vector = default(Vector3);
		transform.localPosition = (Vector3)(&vector);
		isResetting = false;
		return;
		IL_0492:
		Transform transform2 = base.transform;
		transform2.localPosition = (Vector3)(&num21);
		double num22 = num19 - (double)obj2;
		float num23 = num17 - (float)defaultLocalPosition;
		float num24 = num20;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TrajectorySystem.TrajectoryTarget)+28]");
		float num25 = num24 - 0f;
		double num26 = num22 * num22;
		float num27 = num23 * num23;
		float num28 = num25 * num25;
		double num29 = num26 + (double)num27;
		double num30 = num29 + (double)num28;
		if (!(1.0000000116860974E-07 < num30))
		{
			Transform transform3 = base.transform;
			transform3.localPosition = (Vector3)(&vector);
			isResetting = false;
		}
		return;
		IL_015a:
		num17 = (float)defaultLocalPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TrajectorySystem.TrajectoryTarget)+28]");
		num20 = 0f;
		num21 = (float)defaultLocalPosition;
		double num31 = default(double);
		num19 = num31;
		goto IL_0492;
	}

	public bool TryClaim(UnityEngine.Object owner)
	{
		if (!_003CIsClaimed_003Ek__BackingField)
		{
			_003CIsClaimed_003Ek__BackingField = true;
			_003CCurrentOwner_003Ek__BackingField = owner;
			isResetting = false;
			if (OnTargetLocked != null)
			{
				OnTargetLocked.Invoke();
			}
			return true;
		}
		return false;
	}

	public void Release(UnityEngine.Object owner)
	{
		if (!_003CIsClaimed_003Ek__BackingField)
		{
			return;
		}
		bool flag = _003CCurrentOwner_003Ek__BackingField != owner;
		if (!flag)
		{
			_003CIsClaimed_003Ek__BackingField = flag;
			_003CCurrentOwner_003Ek__BackingField = null;
			if (OnTargetLost != null)
			{
				OnTargetLost.Invoke();
			}
		}
	}

	public void RequestResetToDefault()
	{
		if (!_003CIsClaimed_003Ek__BackingField)
		{
			isResetting = true;
			if (OnResetRequested != null)
			{
				OnResetRequested.Invoke();
			}
		}
	}

	public unsafe void SnapResetToDefault()
	{
		//IL_0049: Expected O, but got Ref
		if (!_003CIsClaimed_003Ek__BackingField)
		{
			isResetting = false;
			Transform transform = base.transform;
			object obj = default(object);
			transform.localPosition = (Vector3)(&obj);
			if (OnResetRequested != null)
			{
				OnResetRequested.Invoke();
			}
		}
	}

	public void SetDefaultLocalPositionToCurrent()
	{
		//IL_002b: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		defaultLocalPosition = (Vector3)localPosition.x;
		_ = localPosition.z;
	}

	public TrajectoryTarget()
	{
		//IL_0013: Expected I, but got O
		//IL_0059: Expected I, but got O
		//IL_0086: Expected O, but got I
		//IL_00b0: Expected O, but got I
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		defaultLocalPosition = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		maxResetSpeed = 6f;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		followLocalOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182207140]");
		gizmoDefaultColor = (Color)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		drawGizmos = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182207130]");
		gizmoOffsetColor = (Color)0;
		gizmoSphereRadius = 0.06f;
		base._002Ector();
	}
}
