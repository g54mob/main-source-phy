using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GunElevationLinkCoordinator : MonoBehaviour
{
	public enum InitialSyncOnLink
	{
		None,
		UseGunA,
		UseGunB,
		Average
	}

	private enum DragLeader
	{
		None,
		SliderA,
		SliderB,
		DialA,
		DialB
	}

	private sealed class _003CInitialSyncCoroutine_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GunElevationLinkCoordinator _003C_003E4__this;

		public GunElevationSliderBinding followerSliderBinding;

		public GunController followerGun;

		public float duration;

		public float targetDeg;

		private float _003Cstart_003E5__2;

		private float _003Ct_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CInitialSyncCoroutine_003Ed__46(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_008f: Expected I4, but got I8
			//IL_05ca: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0052: Expected I4, but got I8
			//IL_02c9: Invalid comparison between I4 and F4
			//IL_0372: Invalid comparison between I4 and F4
			//IL_03b4: Invalid comparison between I4 and F4
			//IL_03ff: Expected F4, but got I4
			//IL_0648: Invalid comparison between I4 and F4
			//IL_04aa: Expected F4, but got I4
			//IL_0423: Invalid comparison between I4 and F4
			//IL_046e: Expected F4, but got I4
			GunElevationLinkCoordinator gunElevationLinkCoordinator = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			float num;
			GunController gunController;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag && (nint)obj != 1)
				{
					goto IL_05ae;
				}
				_003C_003E1__state = -1;
				num = _003Ct_003E5__3;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_05f3;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (gunElevationLinkCoordinator.endFollowerDragsWhenLinked)
					{
						if ((object)followerSliderBinding != null)
						{
							followerSliderBinding.ForceEndInteractiveDrag();
						}
						if (followerGun == gunElevationLinkCoordinator.gunA && gunElevationLinkCoordinator.dialA != null)
						{
							DialInteractable dialA = gunElevationLinkCoordinator.dialA;
							if ((object)gunElevationLinkCoordinator.dialA == null)
							{
								goto IL_05bc;
							}
							if (dialA.isDragging)
							{
								gunElevationLinkCoordinator.dialA.EndDialDrag();
							}
						}
						if (followerGun == gunElevationLinkCoordinator.gunB && gunElevationLinkCoordinator.dialB != null)
						{
							DialInteractable dialB = gunElevationLinkCoordinator.dialB;
							if ((object)gunElevationLinkCoordinator.dialB == null)
							{
								goto IL_05bc;
							}
							if (dialB.isDragging)
							{
								gunElevationLinkCoordinator.dialB.EndDialDrag();
							}
						}
					}
					gunController = followerGun;
					if ((object)followerGun != null)
					{
						if (!(0f < duration))
						{
							goto IL_056a;
						}
						_003Ct_003E5__3 = 0f;
						num = _003Ct_003E5__3;
						_003Cstart_003E5__2 = gunController._003CDesiredElevationAngle_003Ek__BackingField;
						goto IL_05f3;
					}
				}
			}
			goto IL_05bc;
			IL_05ae:
			return false;
			IL_05bc:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_056a:
			gunController.SetDesiredElevationFromDial(targetDeg);
			if ((object)followerSliderBinding != null)
			{
				followerSliderBinding.SetInteractiveSliderVisualOnly(targetDeg);
			}
			goto IL_05ae;
			IL_05f3:
			if (1f > num)
			{
				if (gunElevationLinkCoordinator.isLinked)
				{
					float num2 = ((!gunElevationLinkCoordinator.useUnscaledTimeForSmoothing) ? Time.deltaTime : Time.unscaledDeltaTime);
					if (0f < num2)
					{
						float num3 = num2 / duration;
						float num4 = num3 + _003Ct_003E5__3;
						if (!(0f > num4))
						{
							if (num4 > 1f)
							{
								num4 = 1f;
							}
						}
						else
						{
							num4 = 0f;
						}
						_003Ct_003E5__3 = num4;
						if (gunElevationLinkCoordinator.initialSyncEaseCurve != null)
						{
							num4 = gunElevationLinkCoordinator.initialSyncEaseCurve.Evaluate(num4);
							if (!(0f > num4))
							{
								if (num4 > 1f)
								{
									num4 = 1f;
								}
							}
							else
							{
								num4 = 0f;
							}
						}
						if (!(0f > num4))
						{
							if (num4 > 1f)
							{
								num4 = 1f;
							}
						}
						else
						{
							num4 = 0f;
						}
						float num5 = targetDeg - _003Cstart_003E5__2;
						float num6 = num5 * num4;
						float num7 = num6 + _003Cstart_003E5__2;
						if ((object)followerGun == null)
						{
							goto IL_05bc;
						}
						followerGun.SetDesiredElevationFromDial(num7);
						if ((object)followerSliderBinding != null)
						{
							followerSliderBinding.SetInteractiveSliderVisualOnly(num7);
						}
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
					}
					else
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
					}
					return true;
				}
			}
			else if (gunElevationLinkCoordinator.isLinked)
			{
				gunController = followerGun;
				if ((object)followerGun != null)
				{
					goto IL_056a;
				}
				goto IL_05bc;
			}
			goto IL_05ae;
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

	private GunController gunA;

	private GunController gunB;

	private GunElevationSliderBinding sliderBindingA;

	private GunElevationSliderBinding sliderBindingB;

	private DialInteractable dialA;

	private DialInteractable dialB;

	private LookAtTarget linkToggleButton;

	private InputActionReference toggleLinkAction;

	private Animator linkedStateAnimator;

	private string linkedStateBoolParam;

	private bool requireBothGunsLoadedToLink;

	private bool unlinkOnAnyGunFired;

	private bool endFollowerDragsWhenLinked;

	private bool startLinked;

	private InitialSyncOnLink initialSyncOnLink;

	private float liveFollowSmoothTimeSeconds;

	private float initialSyncSmoothTimeSeconds;

	private AnimationCurve initialSyncEaseCurve;

	private bool useUnscaledTimeForSmoothing;

	private bool isLinked;

	private bool prevSliderADrag;

	private bool prevSliderBDrag;

	private bool prevDialADrag;

	private bool prevDialBDrag;

	private float tSliderAStart;

	private float tSliderBStart;

	private float tDialAStart;

	private float tDialBStart;

	private float followerVelAtoB;

	private float followerVelBtoA;

	private Coroutine initialSyncRoutineAtoB;

	private Coroutine initialSyncRoutineBtoA;

	public bool IsLinked => isLinked;

	private void OnEnable()
	{
		if (linkToggleButton != null)
		{
			UnityAction action = ToggleLinked;
			linkToggleButton.RegisterOnClickDown(action);
		}
		if (toggleLinkAction != null)
		{
			InputAction action2 = toggleLinkAction.action;
			if (action2 != null)
			{
				InputAction action3 = toggleLinkAction.action;
				Action<InputAction.CallbackContext> value = OnToggleActionPerformed;
				action3.performed += value;
			}
		}
		if (unlinkOnAnyGunFired)
		{
			if (gunA != null)
			{
				Action value2 = HandleAnyGunFired;
				gunA.OnGunFired += value2;
			}
			if (gunB != null)
			{
				Action value3 = HandleAnyGunFired;
				gunB.OnGunFired += value3;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 308 Invalid \"Jump target not found in method: 0x18055D9C0\"");
		throw new NullReferenceException();
	}

	private void OnDisable()
	{
		if (toggleLinkAction != null)
		{
			InputAction action = toggleLinkAction.action;
			if (action != null)
			{
				InputAction action2 = toggleLinkAction.action;
				Action<InputAction.CallbackContext> value = OnToggleActionPerformed;
				action2.performed -= value;
			}
		}
		if (gunA != null)
		{
			Action value2 = HandleAnyGunFired;
			gunA.OnGunFired -= value2;
		}
		if (gunB != null)
		{
			Action value3 = HandleAnyGunFired;
			gunB.OnGunFired -= value3;
		}
		StopInitialSyncRoutines();
	}

	private unsafe void Update()
	{
		//IL_0692: Expected O, but got I4
		//IL_0261: Expected O, but got I4
		//IL_0282: Expected O, but got I4
		//IL_02b8: Expected O, but got I4
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_03ae: Expected O, but got I4
		//IL_02ee: Expected O, but got I4
		//IL_03d8: Expected O, but got I4
		//IL_071c: Expected O, but got I4
		//IL_0752: Expected O, but got I4
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Expected O, but got Unknown
		//IL_03ca: Expected O, but got I4
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Expected O, but got Unknown
		//IL_03bc: Expected O, but got I4
		//IL_04d9: Invalid comparison between I4 and F4
		//IL_04f9: Invalid comparison between I4 and F4
		if (!isLinked || !(gunA != null) || !(gunB != null))
		{
			return;
		}
		bool flag;
		if (!(sliderBindingA != null))
		{
			flag = false;
		}
		else
		{
			bool isUserDragging = sliderBindingA.IsUserDragging;
			flag = isUserDragging;
		}
		bool flag2;
		if (!(sliderBindingB != null))
		{
			flag2 = false;
		}
		else
		{
			bool isUserDragging2 = sliderBindingB.IsUserDragging;
			flag2 = isUserDragging2;
		}
		bool flag3;
		if (!(dialA != null))
		{
			flag3 = false;
		}
		else
		{
			DialInteractable dialInteractable = dialA;
			flag3 = dialInteractable.isDragging;
		}
		bool flag4 = !flag3;
		bool flag5 = !flag4;
		bool flag6;
		if (!(dialB != null))
		{
			flag6 = false;
		}
		else
		{
			DialInteractable dialInteractable2 = dialB;
			flag6 = dialInteractable2.isDragging;
		}
		bool flag7 = !flag6;
		bool flag8 = !flag7;
		float unscaledTime = Time.unscaledTime;
		if (flag && !prevSliderADrag)
		{
			tSliderAStart = unscaledTime;
		}
		if (flag2 && !prevSliderBDrag)
		{
			tSliderBStart = unscaledTime;
		}
		if (flag3 && !prevDialADrag)
		{
			tDialAStart = unscaledTime;
		}
		if (flag6 && !prevDialBDrag)
		{
			tDialBStart = unscaledTime;
		}
		prevSliderADrag = flag;
		prevSliderBDrag = flag2;
		prevDialADrag = flag5;
		prevDialBDrag = flag8;
		bool flag9 = !flag;
		float num = -1f / 0f;
		object obj = 0;
		if (!flag9)
		{
			bool flag10 = tSliderAStart < -1f / 0f;
			num = -1f / 0f;
			obj = 0;
			if (!flag10)
			{
				num = tSliderAStart;
				obj = 1;
			}
		}
		if (flag2 && !(tSliderBStart < num))
		{
			num = tSliderBStart;
			obj = 2;
		}
		if (flag3 && !(tDialAStart < num))
		{
			num = tDialAStart;
			obj = 3;
		}
		if (flag6 && !(tDialBStart < num))
		{
			goto IL_03a5;
		}
		bool flag11 = obj == null;
		if (flag11)
		{
			return;
		}
		object obj2 = obj - 1;
		object obj4;
		if (!flag11)
		{
			object obj3 = obj2 - 1;
			if (flag11)
			{
				obj4 = 1;
				goto IL_06eb;
			}
			object obj5 = obj3 - 1;
			if (!flag11)
			{
				if ((nint)obj5 == 1)
				{
					goto IL_03a5;
				}
				return;
			}
			obj4 = 0;
		}
		else
		{
			obj4 = 1;
		}
		UnityEngine.Object obj6 = dialB;
		GunElevationSliderBinding gunElevationSliderBinding = sliderBindingB;
		GunController gunController = gunB;
		GunController gunController2 = gunA;
		object obj7 = 160;
		goto IL_078d;
		IL_06eb:
		gunController = gunA;
		gunController2 = gunB;
		gunElevationSliderBinding = sliderBindingA;
		obj6 = dialA;
		obj7 = 164;
		goto IL_078d;
		IL_03a5:
		obj4 = 0;
		goto IL_06eb;
		IL_078d:
		ref float currentVelocity = ref *(float*)(obj7 + (object)this);
		bool flag12 = !endFollowerDragsWhenLinked;
		float num2 = gunController2._003CDesiredElevationAngle_003Ek__BackingField;
		if (!flag12)
		{
			gunElevationSliderBinding?.ForceEndInteractiveDrag();
			if (obj6 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v362 @ rbp_v4 (UnityEngine.Object)+160]");
				if ((nint)0 != 0)
				{
					((DialInteractable)obj6).EndDialDrag();
				}
			}
		}
		float num3 = ((!useUnscaledTimeForSmoothing) ? Time.deltaTime : Time.unscaledDeltaTime);
		if (0f < liveFollowSmoothTimeSeconds && !(0f < num3))
		{
			float maxSpeed = default(float);
			float deltaTime = default(float);
			float num4 = Mathf.SmoothDamp(gunController._003CDesiredElevationAngle_003Ek__BackingField, num2, ref currentVelocity, liveFollowSmoothTimeSeconds, maxSpeed, deltaTime);
			num2 = num4;
		}
		if (obj4 == null)
		{
			gunController.SetDesiredElevationFromDial(num2);
		}
		else
		{
			gunController.SetDesiredElevationFromSlider(num2);
		}
		gunElevationSliderBinding?.SetInteractiveSliderVisualOnly(num2);
	}

	private DragLeader ChooseLeader(bool sliderADrag, bool sliderBDrag, bool dialADrag, bool dialBDrag)
	{
		bool flag = !sliderADrag;
		DragLeader result = DragLeader.None;
		float num = -1f / 0f;
		if (!flag)
		{
			bool flag2 = tSliderAStart < -1f / 0f;
			result = DragLeader.None;
			num = -1f / 0f;
			if (!flag2)
			{
				result = DragLeader.SliderA;
				num = tSliderAStart;
			}
		}
		if (sliderBDrag && !(tSliderBStart < num))
		{
			result = DragLeader.SliderB;
			num = tSliderBStart;
		}
		if (dialADrag && !(tDialAStart < num))
		{
			result = DragLeader.DialA;
			num = tDialAStart;
		}
		object obj = default(object);
		if (obj != null && !(tDialBStart < num))
		{
			result = DragLeader.DialB;
		}
		return result;
	}

	private void OnToggleActionPerformed(InputAction.CallbackContext ctx)
	{
		bool flag = !isLinked;
		if (isLinked || requireBothGunsLoadedToLink == isLinked || AreBothGunsEligibleToLink())
		{
			SetLinked(flag, flag);
		}
	}

	private void HandleAnyGunFired()
	{
		if (unlinkOnAnyGunFired)
		{
			isLinked = false;
			if (linkedStateAnimator != null && !string.IsNullOrWhiteSpace(linkedStateBoolParam))
			{
				linkedStateAnimator.SetBool(linkedStateBoolParam, value: false);
			}
			StopInitialSyncRoutines();
		}
	}

	public void ToggleLinked()
	{
		bool flag = !isLinked;
		if (isLinked || requireBothGunsLoadedToLink == isLinked || AreBothGunsEligibleToLink())
		{
			SetLinked(flag, flag);
		}
	}

	public void SetLinked(bool linked, bool doInitialSync)
	{
		//IL_0183: Expected O, but got I4
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		if (linked && requireBothGunsLoadedToLink && !AreBothGunsEligibleToLink())
		{
			return;
		}
		isLinked = linked;
		if (linkedStateAnimator != null && !string.IsNullOrWhiteSpace(linkedStateBoolParam))
		{
			linkedStateAnimator.SetBool(linkedStateBoolParam, linked);
		}
		StopInitialSyncRoutines();
		if (!linked || !doInitialSync || !(gunA != null) || !(gunB != null))
		{
			return;
		}
		GunController gunController = gunA;
		float num = gunController._003CDesiredElevationAngle_003Ek__BackingField;
		GunController gunController2 = gunB;
		bool flag = initialSyncOnLink == InitialSyncOnLink.None;
		if (flag)
		{
			return;
		}
		object obj = initialSyncOnLink - 1;
		GunElevationSliderBinding followerSliderBinding;
		GunController followerGun;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				if ((nint)obj2 != 1)
				{
					return;
				}
				float num2 = gunController2._003CDesiredElevationAngle_003Ek__BackingField + num;
				float num3 = num2 * 0.5f;
				StartInitialSyncRoutine(gunA, sliderBindingA, num3);
				followerSliderBinding = sliderBindingB;
				followerGun = gunB;
				num = num3;
			}
			else
			{
				followerSliderBinding = sliderBindingA;
				followerGun = gunA;
				num = gunController2._003CDesiredElevationAngle_003Ek__BackingField;
			}
		}
		else
		{
			followerSliderBinding = sliderBindingB;
			followerGun = gunB;
		}
		StartInitialSyncRoutine(followerGun, followerSliderBinding, num);
	}

	private bool AreBothGunsEligibleToLink()
	{
		//IL_00d4: Expected I4, but got O
		if (!(gunA != null) || !(gunB != null))
		{
			goto IL_00c0;
		}
		if ((object)gunA != null)
		{
			if (!gunA.CanFire)
			{
				goto IL_00c0;
			}
			if ((object)gunB != null)
			{
				return gunB.CanFire;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00c0:
		return false;
	}

	private void StartInitialSyncRoutine(GunController followerGun, GunElevationSliderBinding followerSliderBinding, float targetDeg)
	{
		//IL_002c: Invalid comparison between I4 and F4
		if (!(followerGun != null))
		{
			return;
		}
		if (0f < initialSyncSmoothTimeSeconds)
		{
		}
		float duration = default(float);
		if (followerGun != gunB)
		{
			if (!(followerGun == gunA))
			{
				IEnumerator routine = InitialSyncCoroutine(followerGun, followerSliderBinding, targetDeg, duration);
				Coroutine coroutine = StartCoroutine(routine);
				return;
			}
			if (initialSyncRoutineBtoA != null)
			{
				StopCoroutine(initialSyncRoutineBtoA);
			}
			IEnumerator routine2 = InitialSyncCoroutine(followerGun, followerSliderBinding, targetDeg, duration);
			Coroutine coroutine2 = StartCoroutine(routine2);
			initialSyncRoutineBtoA = coroutine2;
		}
		else
		{
			if (initialSyncRoutineAtoB != null)
			{
				StopCoroutine(initialSyncRoutineAtoB);
			}
			IEnumerator routine3 = InitialSyncCoroutine(followerGun, followerSliderBinding, targetDeg, duration);
			Coroutine coroutine3 = StartCoroutine(routine3);
			initialSyncRoutineAtoB = coroutine3;
		}
	}

	private IEnumerator InitialSyncCoroutine(GunController followerGun, GunElevationSliderBinding followerSliderBinding, float targetDeg, float duration)
	{
		_003CInitialSyncCoroutine_003Ed__46 obj = new _003CInitialSyncCoroutine_003Ed__46(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.followerGun = followerGun;
		obj.followerSliderBinding = followerSliderBinding;
		obj.targetDeg = targetDeg;
		float duration2 = default(float);
		obj.duration = duration2;
		return obj;
	}

	private void StopInitialSyncRoutines()
	{
		if (initialSyncRoutineAtoB != null)
		{
			StopCoroutine(initialSyncRoutineAtoB);
			initialSyncRoutineAtoB = null;
		}
		if (initialSyncRoutineBtoA != null)
		{
			StopCoroutine(initialSyncRoutineBtoA);
			initialSyncRoutineBtoA = null;
		}
	}

	private void UpdateAnimator(bool linked)
	{
		if (linkedStateAnimator != null && !string.IsNullOrWhiteSpace(linkedStateBoolParam))
		{
			linkedStateAnimator.SetBool(linkedStateBoolParam, linked);
		}
	}

	public GunElevationLinkCoordinator()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AB0D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		linkedStateBoolParam = "IsLinked";
		requireBothGunsLoadedToLink = true;
		endFollowerDragsWhenLinked = true;
		initialSyncOnLink = InitialSyncOnLink.UseGunA;
		initialSyncSmoothTimeSeconds = 0.5f;
		AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		initialSyncEaseCurve = animationCurve;
		useUnscaledTimeForSmoothing = true;
		base._002Ector();
	}
}
