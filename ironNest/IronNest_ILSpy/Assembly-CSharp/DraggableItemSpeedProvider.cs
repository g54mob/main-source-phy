using System;
using Cpp2ILInjected;
using UnityEngine;

public class DraggableItemSpeedProvider : MonoBehaviour, IFloatValueProvider
{
	private DraggableItem _draggable;

	private bool _useUnscaledTime;

	private float _speedNormalizationRange = 1f;

	private bool _hasPreviousPosition;

	private Vector3 _previousPosition;

	private float _003CSpeed_003Ek__BackingField;

	private float _003CNormalizedSpeed_003Ek__BackingField;

	public float Speed
	{
		get
		{
			return _003CSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CSpeed_003Ek__BackingField = value;
		}
	}

	public float NormalizedSpeed
	{
		get
		{
			return _003CNormalizedSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CNormalizedSpeed_003Ek__BackingField = value;
		}
	}

	float IFloatValueProvider.GetFloatValue()
	{
		return _003CNormalizedSpeed_003Ek__BackingField;
	}

	private void Start()
	{
		if (_draggable == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DraggableItem draggable = default(DraggableItem);
			_draggable = draggable;
		}
		if (_draggable == null)
		{
			Debug.LogError("DraggableItemSpeedProvider has no DraggableItem", this);
		}
	}

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		DraggableItem draggable = default(DraggableItem);
		_draggable = draggable;
	}

	private void Update()
	{
		//IL_00f5: Invalid comparison between I4 and F4
		//IL_00c1: Expected O, but got F4
		//IL_02e9: Expected I, but got O
		//IL_017b: Expected O, but got F4
		//IL_022a: Invalid comparison between I4 and F8
		//IL_01d6: Expected F8, but got I4
		//IL_0277: Expected F8, but got I4
		if ((bool)_draggable)
		{
			Component draggable = _draggable;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v7 (UnityEngine.Component)+34]");
			if ((nint)0 != 0)
			{
				if (!_hasPreviousPosition)
				{
					_hasPreviousPosition = true;
					Transform transform = draggable.transform;
					Vector3 position = transform.position;
					_previousPosition = (Vector3)position.x;
					_ = position.z;
				}
				float num = ((!_useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
				if (!(0f < num))
				{
					return;
				}
				Transform transform2 = _draggable.transform;
				Vector3 position2 = transform2.position;
				nint num2 = (nint)typeof(Math);
				float num3 = position2.x - (float)_previousPosition;
				object obj2 = default(object);
				object obj3 = default(object);
				object obj = obj2 - obj3;
				float num4 = position2.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (DraggableItemSpeedProvider)+3C]");
				float num5 = num4 - 0f;
				object obj4 = obj * obj;
				float num6 = num3 * num3;
				float num7 = num5 * num5;
				float num8 = (float)obj4 + num6;
				_previousPosition = (Vector3)position2.x;
				_ = position2.z;
				float num9 = num8 + num7;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rcx_v11 (Il2CppClass<System.Math>)+E4]");
				double num10;
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
					num10 = 0.0;
				}
				else
				{
					num10 = Math.Sqrt(num9);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
				double num11 = num10 / (double)num;
				_003CSpeed_003Ek__BackingField = (float)num11;
				double num12 = num11 / (double)_speedNormalizationRange;
				if (!(0.0 > num12))
				{
					if (num12 > 1.0)
					{
						_003CNormalizedSpeed_003Ek__BackingField = 1f;
						return;
					}
				}
				else
				{
					num12 = 0.0;
				}
				_003CNormalizedSpeed_003Ek__BackingField = (float)num12;
				return;
			}
		}
		if (_hasPreviousPosition)
		{
			_hasPreviousPosition = false;
			_003CSpeed_003Ek__BackingField = 0f;
		}
	}
}
