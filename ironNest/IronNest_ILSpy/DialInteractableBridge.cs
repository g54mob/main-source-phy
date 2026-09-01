using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DialInteractableBridge : MonoBehaviour
{
	public enum DriverMode
	{
		LastChangedWins,
		DialAIsMaster,
		DialBIsMaster
	}

	private enum DialId
	{
		A,
		B
	}

	private DialInteractable dialA;

	private DialInteractable dialB;

	private float dialAToBridgeScale = 1f;

	private float dialAOffset;

	private float dialBToBridgeScale = 1f;

	private float dialBOffset;

	private DriverMode driverMode;

	private bool syncOnEnable = true;

	private bool ignoreNonDraggingDialWhileDragging;

	private bool allowDrivingOtherWhileItIsDragging;

	private float valueEpsilon = 0.0001f;

	private bool _suppressCallbacks;

	private bool _subscribed;

	private DialId _lastChanged;

	private void OnEnable()
	{
		if (!_subscribed && dialA != null && dialB != null)
		{
			DialInteractable dialInteractable = dialA;
			UnityAction<float> call = HandleDialAValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			DialInteractable dialInteractable2 = dialB;
			UnityAction<float> call2 = HandleDialBValueChanged;
			dialInteractable2.OnValueChanged.AddListener(call2);
			_subscribed = true;
		}
		if (syncOnEnable && dialA != null && dialB != null)
		{
			DialInteractable dialInteractable3 = dialA;
			DialInteractable dialInteractable4 = dialB;
			if (allowDrivingOtherWhileItIsDragging || !dialInteractable4.isDragging)
			{
				float num = dialInteractable3.accumulatedValue - dialAOffset;
				float bridgeValue = num * dialAToBridgeScale;
				ApplyBridgeValueToDial(DialId.B, dialB, bridgeValue);
			}
		}
	}

	private void OnDisable()
	{
		if (_subscribed)
		{
			if (dialA != null)
			{
				DialInteractable dialInteractable = dialA;
				UnityAction<float> call = HandleDialAValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
			if (dialB != null)
			{
				DialInteractable dialInteractable2 = dialB;
				UnityAction<float> call2 = HandleDialBValueChanged;
				dialInteractable2.OnValueChanged.RemoveListener(call2);
			}
			_subscribed = false;
		}
	}

	private void OnValidate()
	{
		//IL_0088: Invalid comparison between I4 and F4
		//IL_009a: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		if (obj != null)
		{
			dialAToBridgeScale = 1f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj2 = default(object);
		if (obj2 != null)
		{
			dialBToBridgeScale = 1f;
		}
		bool flag = !(0f < valueEpsilon);
		float num = 0f;
		if (!flag)
		{
			num = valueEpsilon;
		}
		valueEpsilon = num;
	}

	private void Subscribe()
	{
		if (!_subscribed && dialA != null && dialB != null)
		{
			DialInteractable dialInteractable = dialA;
			UnityAction<float> call = HandleDialAValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			DialInteractable dialInteractable2 = dialB;
			UnityAction<float> call2 = HandleDialBValueChanged;
			dialInteractable2.OnValueChanged.AddListener(call2);
			_subscribed = true;
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed)
		{
			if (dialA != null)
			{
				DialInteractable dialInteractable = dialA;
				UnityAction<float> call = HandleDialAValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
			if (dialB != null)
			{
				DialInteractable dialInteractable2 = dialB;
				UnityAction<float> call2 = HandleDialBValueChanged;
				dialInteractable2.OnValueChanged.RemoveListener(call2);
			}
			_subscribed = false;
		}
	}

	private void HandleDialAValueChanged(float _)
	{
		if (_suppressCallbacks)
		{
			return;
		}
		DialInteractable dialInteractable2;
		float bridgeValue;
		DialId targetId;
		if (driverMode != DriverMode.DialBIsMaster)
		{
			if (ShouldIgnoreChangeFrom(DialId.A))
			{
				return;
			}
			_lastChanged = DialId.A;
			if ((driverMode != DriverMode.LastChangedWins && driverMode != DriverMode.DialAIsMaster) || !(dialA != null) || !(dialB != null))
			{
				return;
			}
			DialInteractable dialInteractable = dialA;
			dialInteractable2 = dialB;
			if (!allowDrivingOtherWhileItIsDragging && dialInteractable2.isDragging)
			{
				return;
			}
			float num = dialInteractable.accumulatedValue - dialAOffset;
			bridgeValue = num * dialAToBridgeScale;
			targetId = DialId.B;
		}
		else
		{
			if (!(dialA != null) || !(dialB != null))
			{
				return;
			}
			DialInteractable dialInteractable3 = dialB;
			dialInteractable2 = dialA;
			if (!allowDrivingOtherWhileItIsDragging && dialInteractable2.isDragging)
			{
				return;
			}
			float num2 = dialInteractable3.accumulatedValue - dialBOffset;
			bridgeValue = num2 * dialBToBridgeScale;
			targetId = DialId.A;
		}
		ApplyBridgeValueToDial(targetId, dialInteractable2, bridgeValue);
	}

	private void HandleDialBValueChanged(float _)
	{
		if (_suppressCallbacks)
		{
			return;
		}
		if (driverMode != DriverMode.DialAIsMaster)
		{
			if (ShouldIgnoreChangeFrom(DialId.B))
			{
				return;
			}
			bool flag = driverMode == DriverMode.LastChangedWins;
			_lastChanged = DialId.B;
			if ((flag || driverMode == DriverMode.DialBIsMaster) && dialA != null && dialB != null)
			{
				DialInteractable dialInteractable = dialB;
				DialInteractable dialInteractable2 = dialA;
				if (allowDrivingOtherWhileItIsDragging || !dialInteractable2.isDragging)
				{
					float num = dialInteractable.accumulatedValue - dialBOffset;
					float bridgeValue = num * dialBToBridgeScale;
					ApplyBridgeValueToDial(DialId.A, dialA, bridgeValue);
				}
			}
		}
		else if (dialA != null && dialB != null)
		{
			DialInteractable dialInteractable3 = dialA;
			DialInteractable dialInteractable4 = dialB;
			if (allowDrivingOtherWhileItIsDragging || !dialInteractable4.isDragging)
			{
				float num2 = dialInteractable3.accumulatedValue - dialAOffset;
				float bridgeValue2 = num2 * dialAToBridgeScale;
				ApplyBridgeValueToDial(DialId.B, dialB, bridgeValue2);
			}
		}
	}

	private bool ShouldIgnoreChangeFrom(DialId source)
	{
		//IL_0188: Expected I4, but got O
		if (ignoreNonDraggingDialWhileDragging)
		{
			bool flag;
			if (!(dialA != null))
			{
				flag = false;
			}
			else
			{
				DialInteractable dialInteractable = dialA;
				if ((object)dialA == null)
				{
					goto IL_017a;
				}
				flag = dialInteractable.isDragging;
			}
			bool flag2 = dialB != null;
			bool flag3 = !flag2;
			bool flag4 = false;
			if (!flag3)
			{
				DialInteractable dialInteractable2 = dialB;
				if ((object)dialB == null)
				{
					goto IL_017a;
				}
				flag4 = dialInteractable2.isDragging;
			}
			if (flag || flag4)
			{
				if (source == DialId.A)
				{
					if (flag)
					{
						goto IL_0130;
					}
				}
				else if (source != DialId.B || flag4)
				{
					goto IL_0130;
				}
				return true;
			}
		}
		goto IL_0130;
		IL_017a:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0130:
		return false;
	}

	private void DriveOtherFrom(DialId source)
	{
		//IL_005e: Expected O, but got I4
		//IL_013a: Expected O, but got I
		//IL_0143: Expected O, but got I4
		//IL_018f: Expected O, but got I
		//IL_0075: Expected O, but got I4
		//IL_0083: Expected O, but got I4
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_017a: Expected O, but got I
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		if (!(dialA != null) || !(dialB != null))
		{
			return;
		}
		bool flag = source == DialId.A;
		object obj = 32;
		if (!flag)
		{
			obj = 40;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rax_v8+this @ rcx (DialInteractableBridge)]");
		object obj2 = 0;
		object obj3 = 40;
		if (!flag)
		{
			obj3 = 32;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rcx_v8+this @ rcx (DialInteractableBridge)]");
		DialInteractable dialInteractable = (DialInteractable)0;
		if (allowDrivingOtherWhileItIsDragging || !dialInteractable.isDragging)
		{
			float bridgeValue;
			if (source != DialId.A)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rdx_v4+9C]");
				object obj4 = 0 - dialBOffset;
				bridgeValue = (float)obj4 * dialBToBridgeScale;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rdx_v4+9C]");
				object obj5 = 0 - dialAOffset;
				bridgeValue = (float)obj5 * dialAToBridgeScale;
			}
			bool targetId = source == DialId.A;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rcx_v8+this @ rcx (DialInteractableBridge)]");
			ApplyBridgeValueToDial(targetId ? DialId.B : DialId.A, (DialInteractable)0, bridgeValue);
		}
	}

	private float DialValueToBridgeValue(DialId dial, float dialValue)
	{
		if (dial != DialId.A)
		{
			float num = dialValue - dialBOffset;
			return num * dialBToBridgeScale;
		}
		float num2 = dialValue - dialAOffset;
		return num2 * dialAToBridgeScale;
	}

	private float BridgeValueToDialValue(DialId dial, float bridgeValue)
	{
		if (dial != DialId.A)
		{
			float num = bridgeValue / dialBToBridgeScale;
			return num + dialBOffset;
		}
		float num2 = bridgeValue / dialAToBridgeScale;
		return num2 + dialAOffset;
	}

	private unsafe void ApplyBridgeValueToDial(DialId targetId, DialInteractable target, float bridgeValue)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_009e: Invalid comparison between F4 and O
		//IL_019c: Expected O, but got Ref
		//IL_0124: Invalid comparison between F4 and I4
		float num2;
		if (targetId != DialId.A)
		{
			float num = bridgeValue / dialBToBridgeScale;
			num2 = num + dialBOffset;
		}
		else
		{
			float num3 = bridgeValue / dialAToBridgeScale;
			num2 = num3 + dialAOffset;
		}
		float num4 = target.accumulatedValue - num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num4 & 0;
		float num5 = valueEpsilon;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			return;
		}
		_suppressCallbacks = true;
		if (target.dialMode == DialInteractable.DialMode.Limited)
		{
			target.SetDialValue(num2);
			_ = 0;
			return;
		}
		if (target.dialMode == DialInteractable.DialMode.Unlimited)
		{
			target.accumulatedValue = num2;
			if (target.useDetents && target.detentStepSize > 0f)
			{
				target.detentTargetAngle = num2;
			}
			else
			{
				Transform transform = target.transform;
				Vector3 axis = default(Vector3);
				Quaternion quaternion = Quaternion.Internal_AngleAxis(target.accumulatedValue, ref axis);
				float num6 = default(float);
				transform.localRotation = (Quaternion)(&num6);
			}
		}
		_ = 0;
	}

	public void ForceResync()
	{
		if (!(dialA != null) || !(dialB != null))
		{
			return;
		}
		if (driverMode == DriverMode.DialAIsMaster)
		{
			if (dialA != null && dialB != null)
			{
				DialInteractable dialInteractable = dialA;
				DialInteractable dialInteractable2 = dialB;
				if (allowDrivingOtherWhileItIsDragging || !dialInteractable2.isDragging)
				{
					float num = dialInteractable.accumulatedValue - dialAOffset;
					float bridgeValue = num * dialAToBridgeScale;
					ApplyBridgeValueToDial(DialId.B, dialB, bridgeValue);
				}
			}
		}
		else if (driverMode == DriverMode.DialBIsMaster)
		{
			if (dialA != null && dialB != null)
			{
				DialInteractable dialInteractable3 = dialB;
				DialInteractable dialInteractable4 = dialA;
				if (allowDrivingOtherWhileItIsDragging || !dialInteractable4.isDragging)
				{
					float num2 = dialInteractable3.accumulatedValue - dialBOffset;
					float bridgeValue2 = num2 * dialBToBridgeScale;
					ApplyBridgeValueToDial(DialId.A, dialA, bridgeValue2);
				}
			}
		}
		else
		{
			DriveOtherFrom(_lastChanged);
		}
	}

	public void SetBridgeValue(float bridgeValue)
	{
		if (dialA != null && dialB != null)
		{
			_suppressCallbacks = true;
			DialInteractableBridge dialInteractableBridge = default(DialInteractableBridge);
			dialInteractableBridge.ApplyBridgeValueToDial(DialId.A, dialInteractableBridge.dialA, bridgeValue);
			dialInteractableBridge.ApplyBridgeValueToDial(DialId.B, dialInteractableBridge.dialB, bridgeValue);
			dialInteractableBridge._suppressCallbacks = false;
		}
	}
}
