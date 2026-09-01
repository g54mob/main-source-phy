using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class LinearSliderAutoRetractor : MonoBehaviour
{
	public enum RetractMode
	{
		SmoothDamp,
		AccelLimited
	}

	private sealed class _003CRetractCoroutine_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LinearSliderAutoRetractor _003C_003E4__this;

		public float restValue;

		private float _003Ct_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRetractCoroutine_003Ed__30(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_01b7: Expected I4, but got I8
			//IL_04e0: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_01e4: Invalid comparison between F4 and I4
			//IL_0184: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Expected O, but got Unknown
			//IL_012e: Invalid comparison between F4 and O
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Expected Ref, but got Unknown
			LinearSliderAutoRetractor linearSliderAutoRetractor = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						goto IL_04c4;
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						goto IL_0076;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						goto IL_0518;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!(linearSliderAutoRetractor.startDelaySeconds > 0f))
					{
						goto IL_0076;
					}
					_003Ct_003E5__2 = 0f;
					goto IL_0518;
				}
			}
			goto IL_04d2;
			IL_04d2:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_04c4:
			return false;
			IL_0509:
			linearSliderAutoRetractor._retractRoutine = null;
			goto IL_04c4;
			IL_0076:
			if (linearSliderAutoRetractor.slider != null)
			{
				LinearSliderInteractable slider = linearSliderAutoRetractor.slider;
				if ((object)linearSliderAutoRetractor.slider != null)
				{
					if (slider.isDragging)
					{
						goto IL_0509;
					}
					float num = restValue - slider.accumulatedValue;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj2 = num & 0;
					float snapEpsilonValue = linearSliderAutoRetractor.snapEpsilonValue;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)snapEpsilonValue) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
					{
						float num2 = ((!linearSliderAutoRetractor.useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
						bool flag2 = !(1E-06f < num2);
						float num3 = 1E-06f;
						if (!flag2)
						{
							num3 = num2;
						}
						float sliderValue;
						if (linearSliderAutoRetractor.retractMode == RetractMode.SmoothDamp)
						{
							ref float currentVelocity = ref *(float*)(_003C_003E4__this + 104);
							bool flag3 = !(0.01f < linearSliderAutoRetractor.smoothTimeSeconds);
							float smoothTime = 0.01f;
							if (!flag3)
							{
								smoothTime = linearSliderAutoRetractor.smoothTimeSeconds;
							}
							float maxSpeed = default(float);
							float deltaTime = default(float);
							float num4 = Mathf.SmoothDamp(slider.accumulatedValue, restValue, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
							sliderValue = num4;
						}
						else
						{
							bool flag4 = !(0.0001f < linearSliderAutoRetractor.maxAccelerationValuePerSecondSq);
							float num5 = 0.0001f;
							if (!flag4)
							{
								num5 = linearSliderAutoRetractor.maxAccelerationValuePerSecondSq;
							}
							float num6 = linearSliderAutoRetractor._velocityValuePerSecond * linearSliderAutoRetractor.damping;
							float num7 = num * linearSliderAutoRetractor.springStrength;
							float num8 = num5 ^ -0f;
							float num9 = num7 - num6;
							if (!(num8 > num9))
							{
								if (num9 > num5)
								{
									num9 = num5;
								}
							}
							else
							{
								num9 = num8;
							}
							bool flag5 = !(0.0001f < linearSliderAutoRetractor.maxSpeedValuePerSecond);
							float num10 = 0.0001f;
							if (!flag5)
							{
								num10 = linearSliderAutoRetractor.maxSpeedValuePerSecond;
							}
							float num11 = num9 * num3;
							float num12 = num11 + linearSliderAutoRetractor._velocityValuePerSecond;
							float num13 = num10 ^ -0f;
							if (!(num13 > num12))
							{
								if (num12 > num10)
								{
									num12 = num10;
								}
							}
							else
							{
								num12 = num13;
							}
							linearSliderAutoRetractor._velocityValuePerSecond = num12;
							float num14 = num12 * num3;
							sliderValue = num14 + slider.accumulatedValue;
						}
						if ((object)linearSliderAutoRetractor.slider != null)
						{
							linearSliderAutoRetractor.slider.SetSliderValue(sliderValue);
							_003C_003E2__current = null;
							_003C_003E1__state = 2;
							goto IL_0683;
						}
					}
					else if ((object)linearSliderAutoRetractor.slider != null)
					{
						linearSliderAutoRetractor.slider.SetSliderValue(restValue);
						linearSliderAutoRetractor._smoothVelocity = 0f;
						goto IL_0509;
					}
				}
				goto IL_04d2;
			}
			goto IL_0509;
			IL_0683:
			return true;
			IL_0518:
			if (!(linearSliderAutoRetractor.startDelaySeconds > _003Ct_003E5__2))
			{
				goto IL_0076;
			}
			if (linearSliderAutoRetractor.slider != null)
			{
				LinearSliderInteractable slider2 = linearSliderAutoRetractor.slider;
				if ((object)linearSliderAutoRetractor.slider == null)
				{
					goto IL_04d2;
				}
				if (!slider2.isDragging)
				{
					float num15 = ((!linearSliderAutoRetractor.useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
					float num16 = num15 + _003Ct_003E5__2;
					_003C_003E2__current = null;
					_003Ct_003E5__2 = num16;
					_003C_003E1__state = 1;
					goto IL_0683;
				}
			}
			goto IL_0509;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private LinearSliderInteractable slider;

	private bool retractToSliderMinimum = true;

	private float customRestValue;

	private float startDelaySeconds;

	private RetractMode retractMode = RetractMode.AccelLimited;

	private bool useUnscaledTime = true;

	private float smoothTimeSeconds = 0.18f;

	private float maxSpeedValuePerSecond = 120f;

	private float maxAccelerationValuePerSecondSq = 450f;

	private float springStrength = 35f;

	private float damping = 14f;

	private bool zeroVelocityWhenGrabbed = true;

	private float snapEpsilonValue = 0.02f;

	private bool requireMinimumPull;

	private float minimumPullAmountValue = 1f;

	private Coroutine _retractRoutine;

	private float _smoothVelocity;

	private float _velocityValuePerSecond;

	private float Dt
	{
		get
		{
			if (useUnscaledTime)
			{
				return Time.unscaledDeltaTime;
			}
			return Time.deltaTime;
		}
	}

	private void Reset()
	{
		if (slider == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			LinearSliderInteractable linearSliderInteractable = default(LinearSliderInteractable);
			slider = linearSliderInteractable;
			if (slider == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
				slider = linearSliderInteractable;
			}
		}
	}

	private void Awake()
	{
		if (slider == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			LinearSliderInteractable linearSliderInteractable = default(LinearSliderInteractable);
			slider = linearSliderInteractable;
			if (slider == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
				slider = linearSliderInteractable;
			}
		}
		if (slider == null)
		{
			Debug.LogError("[LinearSliderAutoRetractor] No LinearSliderInteractable reference found. Please assign one.", this);
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		Action value = HandleDragStarted;
		slider.OnBeginSliderDrag += value;
		Action value2 = HandleDragEnded;
		slider.OnEndSliderDrag += value2;
	}

	private void OnDisable()
	{
		if (slider != null)
		{
			Action value = HandleDragStarted;
			slider.OnBeginSliderDrag -= value;
			Action value2 = HandleDragEnded;
			slider.OnEndSliderDrag -= value2;
		}
		if (_retractRoutine != null)
		{
			StopCoroutine(_retractRoutine);
			_retractRoutine = null;
		}
		_smoothVelocity = 0f;
	}

	private void HandleDragStarted()
	{
		if (_retractRoutine != null)
		{
			StopCoroutine(_retractRoutine);
			_retractRoutine = null;
		}
		bool flag = !zeroVelocityWhenGrabbed;
		_smoothVelocity = 0f;
		if (!flag)
		{
			_velocityValuePerSecond = 0f;
		}
	}

	private void HandleDragEnded()
	{
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_0115: Invalid comparison between F4 and O
		if (!(slider != null))
		{
			return;
		}
		LinearSliderInteractable linearSliderInteractable = slider;
		if (linearSliderInteractable.isDragging)
		{
			return;
		}
		float accumulatedValue;
		if (retractToSliderMinimum)
		{
			linearSliderInteractable.ResetToMinimum();
			LinearSliderInteractable linearSliderInteractable2 = slider;
			accumulatedValue = linearSliderInteractable2.accumulatedValue;
			linearSliderInteractable2.SetSliderValue(linearSliderInteractable.accumulatedValue);
		}
		else
		{
			accumulatedValue = customRestValue;
		}
		LinearSliderInteractable linearSliderInteractable3 = slider;
		if (requireMinimumPull)
		{
			float num = linearSliderInteractable3.accumulatedValue - accumulatedValue;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num & 0;
			float num2 = minimumPullAmountValue;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				return;
			}
		}
		if (_retractRoutine != null)
		{
			StopCoroutine(_retractRoutine);
			_retractRoutine = null;
		}
		_smoothVelocity = 0f;
		_003CRetractCoroutine_003Ed__30 obj2 = new _003CRetractCoroutine_003Ed__30(0);
		obj2._003C_003E1__state = 0;
		obj2._003C_003E4__this = this;
		obj2.restValue = accumulatedValue;
		Coroutine retractRoutine = StartCoroutine(obj2);
		_retractRoutine = retractRoutine;
	}

	private float GetRestValue()
	{
		if (retractToSliderMinimum)
		{
			LinearSliderInteractable linearSliderInteractable = slider;
			slider.ResetToMinimum();
			LinearSliderInteractable linearSliderInteractable2 = slider;
			slider.SetSliderValue(linearSliderInteractable.accumulatedValue);
			return linearSliderInteractable2.accumulatedValue;
		}
		return customRestValue;
	}

	private void StartRetract(float restValue)
	{
		if (_retractRoutine != null)
		{
			StopCoroutine(_retractRoutine);
			_retractRoutine = null;
		}
		_smoothVelocity = 0f;
		_003CRetractCoroutine_003Ed__30 obj = new _003CRetractCoroutine_003Ed__30(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.restValue = restValue;
		Coroutine retractRoutine = StartCoroutine(obj);
		_retractRoutine = retractRoutine;
	}

	private void StopRetract()
	{
		if (_retractRoutine != null)
		{
			StopCoroutine(_retractRoutine);
			_retractRoutine = null;
			_smoothVelocity = 0f;
		}
		else
		{
			_smoothVelocity = 0f;
		}
	}

	private IEnumerator RetractCoroutine(float restValue)
	{
		_003CRetractCoroutine_003Ed__30 obj = new _003CRetractCoroutine_003Ed__30(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.restValue = restValue;
		return obj;
	}
}
