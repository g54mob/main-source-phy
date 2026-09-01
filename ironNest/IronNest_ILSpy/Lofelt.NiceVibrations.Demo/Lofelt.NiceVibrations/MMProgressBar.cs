using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class MMProgressBar : MonoBehaviour
{
	public enum FillModes
	{
		LocalScale,
		FillAmount,
		Width,
		Height
	}

	public enum BarDirections
	{
		LeftToRight,
		RightToLeft,
		UpToDown,
		DownToUp
	}

	public enum TimeScales
	{
		UnscaledTime,
		Time
	}

	private sealed class _003CBumpCoroutine_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MMProgressBar _003C_003E4__this;

		private float _003Cjourney_003E5__2;

		private float _003CcurrentDeltaTime_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CBumpCoroutine_003Ed__49(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00ab: Expected I4, but got I8
			//IL_0424: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0078: Expected I4, but got I8
			//IL_0140: Expected O, but got I
			//IL_0464: Invalid comparison between I and F4
			//IL_005b: Expected I4, but got I8
			//IL_03c2: Expected O, but got I
			//IL_016d: Expected O, but got I
			//IL_01eb: Invalid comparison between I4 and F4
			//IL_0236: Expected F4, but got I4
			//IL_0250: Expected O, but got I
			//IL_028e: Expected O, but got I
			//IL_02d1: Expected O, but got Ref
			//IL_030d: Expected O, but got I
			//IL_033a: Expected O, but got I
			//IL_0343: Invalid comparison between I4 and F4
			//IL_0396: Expected F4, but got I4
			Component component = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						result = false;
					}
					goto IL_044d;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0416;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				_003Cjourney_003E5__2 = 0f;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0416;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+30]");
				float num = (((nint)0 != 1) ? Time.unscaledDeltaTime : Time.deltaTime);
				_003CcurrentDeltaTime_003E5__3 = num;
				_ = 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
					if ((nint)0 == 0)
					{
						goto IL_0416;
					}
					object obj3 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v492 @ r8_v15+298] (should have been resolved before IL gen)");
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+64]");
			if (!(0f < _003Cjourney_003E5__2))
			{
				float num2 = (_003Cjourney_003E5__2 = _003CcurrentDeltaTime_003E5__3 + _003Cjourney_003E5__2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+64]");
				float num3 = num2 / 0f;
				if (!(0f > num3))
				{
					if (num3 > 1f)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+80]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+80]");
					float num4 = ((AnimationCurve)0).Evaluate(num3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+88]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+88]");
						float num5 = ((AnimationCurve)0).Evaluate(num3);
						Transform transform = _003C_003E4__this.transform;
						if ((object)transform != null)
						{
							object obj4 = default(object);
							transform.localScale = (Vector3)(&obj4);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+68]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
								if ((UnityEngine.Object)0 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
									object obj5 = 0;
									float num6;
									if (!(0f > num5))
									{
										bool flag3 = !(num5 > 1f);
										num6 = num5;
										if (!flag3)
										{
											num6 = 1f;
										}
									}
									else
									{
										num6 = 0f;
									}
									object obj7 = default(object);
									object obj6 = obj7 - obj7;
									float num7 = (float)obj6 * num6;
									float num8 = num7 + (float)obj7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
									if ((nint)0 == 0)
									{
										goto IL_0416;
									}
									object obj8 = obj5;
									Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v647 @ r8_v10+2A8] (should have been resolved before IL gen)");
								}
							}
							_003C_003E2__current = null;
							_003C_003E1__state = 1;
							goto IL_0504;
						}
					}
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (UnityEngine.Component)+E0]");
				if ((nint)0 != 0)
				{
					object obj10 = obj9;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v442 @ r8_v3+2A8] (should have been resolved before IL gen)");
					_ = 0;
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					goto IL_0504;
				}
			}
			goto IL_0416;
			IL_0504:
			result = true;
			goto IL_044d;
			IL_044d:
			return result;
			IL_0416:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
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

	public float StartValue;

	public float EndValue;

	public BarDirections BarDirection;

	public FillModes FillMode;

	public TimeScales TimeScale;

	public bool LerpForegroundBar;

	public float LerpForegroundBarSpeed;

	public float Delay;

	public bool LerpDelayedBar;

	public float LerpDelayedBarSpeed;

	public string PlayerID;

	public Transform DelayedBar;

	public Transform ForegroundBar;

	public bool BumpScaleOnChange;

	public bool BumpOnIncrease;

	public float BumpDuration;

	public bool ChangeColorWhenBumping;

	public Color BumpColor;

	public AnimationCurve BumpAnimationCurve;

	public AnimationCurve BumpColorAnimationCurve;

	private bool _003CBumping_003Ek__BackingField;

	public bool AutoUpdating;

	public float BarProgress;

	protected float _targetFill;

	protected Vector3 _targetLocalScale;

	protected float _newPercent;

	protected float _lastPercent;

	protected float _lastUpdateTimestamp;

	protected bool _bump;

	protected Color _initialColor;

	protected Vector3 _initialScale;

	protected Vector3 _newScale;

	protected Image _foregroundImage;

	protected Image _delayedImage;

	protected bool _initialized;

	protected Vector2 _initialFrontBarSize;

	public bool Bumping
	{
		get
		{
			return _003CBumping_003Ek__BackingField;
		}
		protected set
		{
			_003CBumping_003Ek__BackingField = value;
		}
	}

	protected virtual void Start()
	{
		//IL_0021: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localScale = transform.localScale;
		_initialScale = (Vector3)localScale.x;
		_ = localScale.z;
		Image image = default(Image);
		if (ForegroundBar != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			_foregroundImage = image;
			RectTransform rectTransform = _foregroundImage.rectTransform;
			Vector2 sizeDelta = rectTransform.sizeDelta;
			_initialFrontBarSize = sizeDelta;
		}
		if (DelayedBar != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			_delayedImage = image;
		}
		_initialized = true;
	}

	protected virtual void Update()
	{
		//IL_0011: Expected I, but got O
		//IL_0021: Expected O, but got I
		//IL_0031: Expected O, but got I
		AutoUpdate();
		UpdateFrontBar();
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdx_v5 (Il2CppClass<Lofelt.NiceVibrations.MMProgressBar>)+1B8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdx_v5 (Il2CppClass<Lofelt.NiceVibrations.MMProgressBar>)+1C0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v21 @ rax_v5 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected virtual void AutoUpdate()
	{
		if (AutoUpdating)
		{
			float num = Remap(BarProgress, 0f, 1f, 0f, 0f);
			_newPercent = EndValue;
			_targetFill = EndValue;
			if (TimeScale == TimeScales.Time)
			{
				float time = Time.time;
				_lastUpdateTimestamp = time;
			}
			else
			{
				float unscaledTime = Time.unscaledTime;
				_lastUpdateTimestamp = unscaledTime;
			}
		}
	}

	protected unsafe virtual void UpdateFrontBar()
	{
		//IL_05da: Expected I, but got O
		//IL_007e: Expected O, but got I4
		//IL_04e7: Expected O, but got F4
		//IL_0448: Expected O, but got I4
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_0533: Invalid comparison between I4 and F4
		//IL_04d6: Expected O, but got F4
		//IL_057e: Expected F4, but got I4
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Expected O, but got Unknown
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Expected O, but got Unknown
		//IL_0592: Expected O, but got Ref
		//IL_03a4: Invalid comparison between I4 and F4
		//IL_03ef: Expected F4, but got I4
		//IL_0284: Invalid comparison between I4 and F4
		//IL_02cf: Expected F4, but got I4
		//IL_014f: Invalid comparison between I4 and F4
		//IL_019a: Expected F4, but got I4
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		float num = ((TimeScale != TimeScales.Time) ? Time.unscaledTime : Time.deltaTime);
		if (!(ForegroundBar != null))
		{
			return;
		}
		bool flag = FillMode == FillModes.LocalScale;
		if (!flag)
		{
			object obj = FillMode - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1 || !(_foregroundImage != null))
					{
						return;
					}
					float num2 = Remap(_targetFill, 0f, 1f, 0f, 0f);
					RectTransform rectTransform = _foregroundImage.rectTransform;
					Vector2 sizeDelta = rectTransform.sizeDelta;
					float num3 = num * LerpForegroundBarSpeed;
					if (!(0f > num3))
					{
						if (num3 > 1f)
						{
							num3 = 1f;
						}
					}
					else
					{
						num3 = 0f;
					}
					RectTransform rectTransform2 = _foregroundImage.rectTransform;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.MMProgressBar)+F8]");
					object obj3 = 0 - sizeDelta;
					float num4 = (float)obj3 * num3;
					float size = num4 + (float)sizeDelta;
					rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
				}
				else
				{
					if (!(_foregroundImage != null))
					{
						return;
					}
					float num5 = Remap(_targetFill, 0f, 1f, 0f, 0f);
					RectTransform rectTransform3 = _foregroundImage.rectTransform;
					Vector2 sizeDelta2 = rectTransform3.sizeDelta;
					float num6 = num * LerpForegroundBarSpeed;
					if (!(0f > num6))
					{
						if (num6 > 1f)
						{
							num6 = 1f;
						}
					}
					else
					{
						num6 = 0f;
					}
					RectTransform rectTransform4 = _foregroundImage.rectTransform;
					object obj4 = _initialFrontBarSize - sizeDelta2;
					float num7 = (float)obj4 * num6;
					float size2 = num7 + (float)sizeDelta2;
					rectTransform4.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size2);
				}
				return;
			}
			bool flag2 = _foregroundImage == null;
			if (flag2)
			{
				return;
			}
			Image foregroundImage = _foregroundImage;
			float fillAmount;
			if (LerpForegroundBar == flag2)
			{
				fillAmount = _targetFill;
			}
			else
			{
				float num8 = num * LerpForegroundBarSpeed;
				if (!(0f > num8))
				{
					if (num8 > 1f)
					{
						num8 = 1f;
					}
				}
				else
				{
					num8 = 0f;
				}
				float num9 = _targetFill - foregroundImage.m_FillAmount;
				float num10 = num9 * num8;
				fillAmount = num10 + foregroundImage.m_FillAmount;
			}
			foregroundImage.fillAmount = fillAmount;
			return;
		}
		nint num11 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ rax_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num12 = 0;
		_targetLocalScale = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		bool flag3 = BarDirection == BarDirections.LeftToRight;
		if (!flag3)
		{
			object obj5 = BarDirection - 1;
			if (!flag3)
			{
				object obj6 = obj5 - 1;
				if (!flag3)
				{
					if ((nint)obj6 == 1)
					{
						_ = _targetFill;
					}
				}
				else
				{
					float num13 = 1f - _targetFill;
				}
			}
			else
			{
				float num14 = 1f - _targetFill;
				_targetLocalScale = (Vector3)num14;
			}
		}
		else
		{
			_targetLocalScale = (Vector3)_targetFill;
		}
		float x = default(float);
		if (!LerpForegroundBar)
		{
			_newScale = _targetLocalScale;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.MMProgressBar)+A4]");
			_ = 0;
		}
		else
		{
			Vector3 localScale = ForegroundBar.localScale;
			float num15 = num * LerpForegroundBarSpeed;
			if (!(0f > num15))
			{
				if (num15 > 1f)
				{
					num15 = 1f;
				}
			}
			else
			{
				num15 = 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.MMProgressBar)+A4]");
			object obj7 = 0 - localScale.z;
			float num16 = (float)obj7 * num15;
			float num17 = num16 + localScale.z;
			Vector3 newScale = default(Vector3);
			_newScale = newScale;
			x = localScale.x;
		}
		ForegroundBar.localScale = (Vector3)(&x);
	}

	protected unsafe virtual void UpdateDelayedBar()
	{
		//IL_03ab: Expected I, but got O
		//IL_0195: Expected O, but got F4
		//IL_00f6: Expected O, but got I4
		//IL_01e1: Invalid comparison between I4 and F4
		//IL_0184: Expected O, but got F4
		//IL_02eb: Invalid comparison between I4 and F4
		//IL_022c: Expected F4, but got I4
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0336: Expected F4, but got I4
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Expected O, but got Unknown
		//IL_0240: Expected O, but got Ref
		float num = ((TimeScale != TimeScales.Time) ? Time.unscaledDeltaTime : Time.deltaTime);
		float num2 = ((TimeScale != TimeScales.Time) ? Time.unscaledTime : Time.time);
		if (!(DelayedBar != null))
		{
			return;
		}
		float num3 = num2 - _lastUpdateTimestamp;
		if (!(num3 > Delay))
		{
			return;
		}
		if (FillMode == FillModes.LocalScale)
		{
			nint num4 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rax_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			_targetLocalScale = Vector3.oneVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ rcx_v14 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
			_ = 0;
			bool flag = BarDirection == BarDirections.LeftToRight;
			if (!flag)
			{
				object obj = BarDirection - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 == 1)
						{
							_ = _targetFill;
						}
					}
					else
					{
						float num6 = 1f - _targetFill;
					}
				}
				else
				{
					float num7 = 1f - _targetFill;
					_targetLocalScale = (Vector3)num7;
				}
			}
			else
			{
				_targetLocalScale = (Vector3)_targetFill;
			}
			float x = default(float);
			if (!LerpDelayedBar)
			{
				_newScale = _targetLocalScale;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.MMProgressBar)+A4]");
				_ = 0;
			}
			else
			{
				Vector3 localScale = DelayedBar.localScale;
				float num8 = num * LerpDelayedBarSpeed;
				if (!(0f > num8))
				{
					if (num8 > 1f)
					{
						num8 = 1f;
					}
				}
				else
				{
					num8 = 0f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.MMProgressBar)+A4]");
				object obj3 = 0 - localScale.z;
				float num9 = (float)obj3 * num8;
				float num10 = num9 + localScale.z;
				Vector3 newScale = default(Vector3);
				_newScale = newScale;
				x = localScale.x;
			}
			DelayedBar.localScale = (Vector3)(&x);
		}
		if (FillMode != FillModes.FillAmount || !(_delayedImage != null))
		{
			return;
		}
		Image delayedImage = _delayedImage;
		float fillAmount;
		if (!LerpDelayedBar)
		{
			fillAmount = _targetFill;
		}
		else
		{
			float num11 = num * LerpDelayedBarSpeed;
			if (!(0f > num11))
			{
				if (num11 > 1f)
				{
					num11 = 1f;
				}
			}
			else
			{
				num11 = 0f;
			}
			float num12 = _targetFill - delayedImage.m_FillAmount;
			float num13 = num12 * num11;
			fillAmount = num13 + delayedImage.m_FillAmount;
		}
		delayedImage.fillAmount = fillAmount;
	}

	public virtual void UpdateBar(float currentValue, float minValue, float maxValue)
	{
		float num = Remap(currentValue, minValue, maxValue, 0f, 0f);
		bool flag = EndValue == BarProgress;
		_newPercent = EndValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A8A4DAh\"");
		if (!flag && !_003CBumping_003Ek__BackingField)
		{
			Bump();
		}
		BarProgress = _newPercent;
		_targetFill = _newPercent;
		float lastUpdateTimestamp = ((TimeScale != TimeScales.Time) ? Time.unscaledTime : Time.time);
		_lastUpdateTimestamp = lastUpdateTimestamp;
		_lastPercent = _newPercent;
	}

	public virtual void Bump()
	{
		if (BumpScaleOnChange && _initialized && (BumpOnIncrease || !(_newPercent > _lastPercent)))
		{
			GameObject gameObject = base.gameObject;
			if (gameObject.activeInHierarchy)
			{
				IEnumerator routine = BumpCoroutine();
				Coroutine coroutine = StartCoroutine(routine);
			}
		}
	}

	protected virtual IEnumerator BumpCoroutine()
	{
		_003CBumpCoroutine_003Ed__49 obj = new _003CBumpCoroutine_003Ed__49(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	protected virtual float Remap(float x, float A, float B, float C, float D)
	{
		float num = x - A;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		float num2 = B - A;
		float num3 = num / num2;
		float num4 = num3 * (float)obj;
		return num4 + (float)obj3;
	}

	public unsafe MMProgressBar()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0288: Expected O, but got I
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_0327: Expected native int or pointer, but got O
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0077: Expected native int or pointer, but got O
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_00d6: Expected native int or pointer, but got O
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0165: Expected native int or pointer, but got O
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_01c4: Expected native int or pointer, but got O
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_0223: Expected native int or pointer, but got O
		//IL_033f: Expected I, but got O
		object obj2 = default(object);
		object obj = obj2 - 95;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		BumpColor = (Color)0;
		EndValue = 1f;
		LerpForegroundBar = true;
		LerpForegroundBarSpeed = 15f;
		Delay = 1f;
		LerpDelayedBar = true;
		LerpDelayedBarSpeed = 15f;
		BumpScaleOnChange = true;
		BumpDuration = 0.2f;
		ChangeColorWhenBumping = true;
		Keyframe[] keys = new Keyframe[3];
		Keyframe keyframe = (Keyframe)(obj - 121);
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe = new Keyframe(1f, 1f);
		Keyframe keyframe2 = (Keyframe)(obj - 89);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-79]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-69]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-61]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe2 = new Keyframe(0.3f, 1.05f);
		Keyframe keyframe3 = (Keyframe)(obj - 57);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-41]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe3 = new Keyframe(1f, 1f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
		_ = 0;
		BumpAnimationCurve = new AnimationCurve(keys);
		Keyframe[] keys2 = new Keyframe[3];
		Keyframe keyframe4 = (Keyframe)(obj - 25);
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe4 = new Keyframe(0f, 0f);
		Keyframe keyframe5 = (Keyframe)(obj + 7);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-1]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe5 = new Keyframe(0.3f, 1f);
		Keyframe keyframe6 = (Keyframe)(obj + 39);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1F]");
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe6 = new Keyframe(1f, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+37]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+3F]");
		_ = 0;
		BumpColorAnimationCurve = new AnimationCurve(keys2);
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rax_v34 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		_targetLocalScale = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ rcx_v19 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		base._002Ector();
	}
}
