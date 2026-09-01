using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GunElevationSliderBinding : MonoBehaviour
{
	private enum BackdrivePhase
	{
		None,
		ToReload,
		ToDesired
	}

	private GunController gun;

	private LinearSliderInteractable desiredSlider;

	private LinearSliderInteractable currentSliderGhost;

	private LinearSliderInteractable desiredSliderGhost;

	private bool autoFindTurretController = true;

	private TurretController turretController;

	private bool overrideElevationLimits;

	private float minElevationDegOverride;

	private float maxElevationDegOverride = 45f;

	private bool disableDesiredInteractableDuringOverride = true;

	private bool lockInputUntilRestoreComplete;

	private float backdriveToReloadDelaySeconds;

	private float backdriveToReloadSeconds = 0.2f;

	private float backdriveToDesiredSeconds = 0.2f;

	private AnimationCurve backdriveEaseCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	private bool clampValuesToLimits = true;

	private bool sanitizeGhostSlidersOnAwake;

	public UnityEvent OnReloadOverrideBegan;

	public UnityEvent OnReloadOverrideCompleted;

	private bool logWarnings = true;

	private float minDeg;

	private float maxDeg = 45f;

	private bool prevIsReloading;

	private bool overrideActive;

	private bool suppressDesiredCallback;

	private Interactable desiredInteractable;

	private BackdrivePhase backdrivePhase;

	private bool backdriveActive;

	private float backdriveFromValue;

	private float backdriveToValue;

	private float backdriveDuration;

	private float backdriveElapsed;

	private bool reloadBackdriveDelayPending;

	private float reloadBackdriveDelayElapsed;

	private float desiredSliderVisualValue;

	private bool desiredSliderVisualInitialized;

	public bool IsUserDragging
	{
		get
		{
			//IL_0069: Expected I4, but got O
			bool flag = desiredSlider != null;
			if (!flag)
			{
				return flag;
			}
			LinearSliderInteractable linearSliderInteractable = desiredSlider;
			if ((object)desiredSlider != null)
			{
				return linearSliderInteractable.isDragging;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public void ForceEndInteractiveDrag()
	{
		if (desiredSlider != null)
		{
			LinearSliderInteractable linearSliderInteractable = desiredSlider;
			if (linearSliderInteractable.isDragging)
			{
				linearSliderInteractable.EndSliderDrag();
			}
		}
	}

	private void Awake()
	{
		if (gun == null && logWarnings)
		{
			Debug.LogWarning("[GunElevationSliderBinding] No GunController assigned.", this);
		}
		if (autoFindTurretController && this.turretController == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			TurretController turretController = default(TurretController);
			this.turretController = turretController;
		}
		float num3;
		if (!overrideElevationLimits)
		{
			if (this.turretController == null)
			{
				if (gun != null)
				{
					GunController gunController = gun;
					float num = gunController._003CMinElevationAngle_003Ek__BackingField;
					minDeg = gunController._003CMinElevationAngle_003Ek__BackingField;
					float num2 = gunController._003CMinElevationAngle_003Ek__BackingField + 45f;
					bool flag = gunController._003CMinElevationAngle_003Ek__BackingField == num2;
					if (gunController._003CMinElevationAngle_003Ek__BackingField < num2)
					{
						num = num2;
					}
					maxDeg = num;
					if (!flag)
					{
						Debug.LogWarning("[GunElevationSliderBinding] No TurretController found and overrides disabled. Using Gun.MinElevationAngle and +45°.", this);
					}
				}
			}
			else
			{
				TurretController turretController2 = this.turretController;
				minDeg = turretController2.minBarrelElevation;
				TurretController turretController3 = this.turretController;
				maxDeg = turretController3.maxBarrelElevation;
				if (minDeg > turretController3.maxBarrelElevation)
				{
					if (logWarnings)
					{
						Debug.LogWarning("[GunElevationSliderBinding] TurretController elevation limits are inverted; correcting.", turretController3);
					}
					num3 = minDeg;
					minDeg = maxDeg;
					goto IL_048b;
				}
			}
			goto IL_02d3;
		}
		float num4 = minElevationDegOverride;
		if (minElevationDegOverride > maxElevationDegOverride)
		{
			num4 = maxElevationDegOverride;
		}
		minDeg = num4;
		num3 = minElevationDegOverride;
		if (minElevationDegOverride < maxElevationDegOverride)
		{
			num3 = maxElevationDegOverride;
		}
		goto IL_048b;
		IL_048b:
		maxDeg = num3;
		goto IL_02d3;
		IL_04d2:
		float num5;
		desiredSliderVisualValue = num5;
		desiredSliderVisualInitialized = true;
		return;
		IL_02d3:
		bool flag2 = desiredSlider != null;
		if (!flag2)
		{
			if (logWarnings != flag2)
			{
				Debug.LogWarning("[GunElevationSliderBinding] Desired (interactive) slider is not assigned. The gun will not receive elevation input.", this);
			}
		}
		else
		{
			LinearSliderInteractable linearSliderInteractable = desiredSlider;
			UnityAction<float> call = OnDesiredSliderValueChanged;
			linearSliderInteractable.OnValueChanged.AddListener(call);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
			Interactable interactable = default(Interactable);
			desiredInteractable = interactable;
		}
		if (sanitizeGhostSlidersOnAwake)
		{
			MakeSliderOutputOnly(currentSliderGhost);
			MakeSliderOutputOnly(desiredSliderGhost);
		}
		if (!(gun != null))
		{
			return;
		}
		GunController gunController2 = gun;
		float num6 = minDeg;
		num5 = gunController2._003CDesiredElevationAngle_003Ek__BackingField;
		if (!(minDeg > gunController2._003CDesiredElevationAngle_003Ek__BackingField))
		{
			num6 = maxDeg;
			if (!(gunController2._003CDesiredElevationAngle_003Ek__BackingField > maxDeg))
			{
				goto IL_04d2;
			}
		}
		num5 = num6;
		goto IL_04d2;
	}

	private void OnDestroy()
	{
		if (desiredSlider != null)
		{
			LinearSliderInteractable linearSliderInteractable = desiredSlider;
			UnityAction<float> call = OnDesiredSliderValueChanged;
			linearSliderInteractable.OnValueChanged.RemoveListener(call);
		}
	}

	private void Update()
	{
		//IL_0055: Expected O, but got I4
		//IL_006f: Expected O, but got I4
		//IL_02b0: Invalid comparison between F4 and I4
		//IL_02c1: Invalid comparison between F4 and I4
		//IL_04fb: Invalid comparison between I4 and F4
		//IL_0537: Invalid comparison between I4 and F4
		//IL_0582: Expected F4, but got I4
		//IL_0768: Expected F4, but got I4
		//IL_09e5: Invalid comparison between I4 and F4
		//IL_05a3: Invalid comparison between I4 and F4
		//IL_0637: Expected F4, but got I4
		//IL_05ee: Expected F4, but got I4
		if (!(gun != null))
		{
			return;
		}
		GunController gunController = gun;
		bool flag = !prevIsReloading;
		object obj = gunController.isReloading & flag;
		bool flag2 = obj == null;
		object obj2 = !flag2;
		float seconds = default(float);
		if (obj2 == null)
		{
			if (prevIsReloading && !gunController.isReloading)
			{
				overrideActive = false;
				reloadBackdriveDelayPending = false;
				reloadBackdriveDelayElapsed = 0f;
				if (OnReloadOverrideCompleted != null)
				{
					OnReloadOverrideCompleted.Invoke();
				}
				if (!desiredSliderVisualInitialized)
				{
					float clampedDesired = GetClampedDesired();
					desiredSliderVisualValue = clampedDesired;
				}
				float clampedDesired2 = GetClampedDesired();
				StartBackdrive(BackdrivePhase.ToDesired, desiredSliderVisualValue, clampedDesired2, seconds);
				if (!lockInputUntilRestoreComplete && disableDesiredInteractableDuringOverride && desiredInteractable != null)
				{
					desiredInteractable.enabled = true;
				}
			}
		}
		else
		{
			overrideActive = true;
			if (desiredSlider != null)
			{
				LinearSliderInteractable linearSliderInteractable = desiredSlider;
				if (linearSliderInteractable.isDragging)
				{
					linearSliderInteractable.EndSliderDrag();
				}
			}
			if (disableDesiredInteractableDuringOverride && desiredInteractable != null)
			{
				desiredInteractable.enabled = false;
			}
			if (OnReloadOverrideBegan != null)
			{
				OnReloadOverrideBegan.Invoke();
			}
			bool flag3 = backdriveToReloadDelaySeconds < 0f;
			bool flag4 = backdriveToReloadDelaySeconds == 0f;
			reloadBackdriveDelayElapsed = 0f;
			bool flag5 = !flag3;
			bool flag6 = !flag4;
			if (!(reloadBackdriveDelayPending = flag6 & flag5))
			{
				if (!desiredSliderVisualInitialized)
				{
					float clampedDesired3 = GetClampedDesired();
					desiredSliderVisualValue = clampedDesired3;
				}
				StartBackdrive(BackdrivePhase.ToReload, desiredSliderVisualValue, minDeg, seconds);
			}
		}
		if (overrideActive && reloadBackdriveDelayPending)
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			if (!((reloadBackdriveDelayElapsed = unscaledDeltaTime + reloadBackdriveDelayElapsed) < backdriveToReloadDelaySeconds))
			{
				reloadBackdriveDelayPending = false;
				if (!desiredSliderVisualInitialized)
				{
					float clampedDesired4 = GetClampedDesired();
					desiredSliderVisualValue = clampedDesired4;
				}
				StartBackdrive(BackdrivePhase.ToReload, desiredSliderVisualValue, minDeg, seconds);
			}
		}
		if (~(gunController.isReloading ? 1u : 0u) == 0)
		{
			if (backdriveActive)
			{
				goto IL_04cc;
			}
			if (!reloadBackdriveDelayPending && desiredSlider != null)
			{
				SetDesiredSliderSafely(minDeg);
			}
		}
		if (backdriveActive)
		{
			goto IL_04cc;
		}
		goto IL_070c;
		IL_070c:
		if (currentSliderGhost != null)
		{
			UnityEngine.Object obj3 = currentSliderGhost;
			bool flag7 = gun != null;
			bool flag8 = !flag7;
			float num = 0f;
			if (!flag8)
			{
				GunController gunController2 = gun;
				num = gunController2._003CCurrentElevation_003Ek__BackingField;
			}
			if (clampValuesToLimits)
			{
				if (!(minDeg > num))
				{
					if (num > maxDeg)
					{
						num = maxDeg;
					}
				}
				else
				{
					num = minDeg;
				}
			}
			bool flag9 = currentSliderGhost == null;
			if (!flag9)
			{
				if (clampValuesToLimits != flag9)
				{
					if (!(minDeg > num))
					{
						if (num > maxDeg)
						{
							num = maxDeg;
						}
					}
					else
					{
						num = minDeg;
					}
				}
				currentSliderGhost.SetSliderValue(num);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v187 @ rbx_v12 (UnityEngine.Object)+100]");
				if ((nint)0 != 0)
				{
					currentSliderGhost.EndSliderDrag();
				}
			}
		}
		if (desiredSliderGhost != null)
		{
			float clampedDesired5 = GetClampedDesired();
			SetGhostSliderSafely(desiredSliderGhost, clampedDesired5);
		}
		prevIsReloading = gunController.isReloading;
		return;
		IL_04cc:
		float unscaledDeltaTime2 = Time.unscaledDeltaTime;
		float num2 = (backdriveElapsed = unscaledDeltaTime2 + backdriveElapsed);
		float num3;
		if (!(0f < backdriveDuration))
		{
			num3 = 1f;
		}
		else
		{
			num3 = num2 / backdriveDuration;
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
		}
		float num4;
		if (backdriveEaseCurve != null)
		{
			num4 = backdriveEaseCurve.Evaluate(num3);
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
		else
		{
			num4 = num3;
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
		float num5 = backdriveToValue - backdriveFromValue;
		float num6 = num5 * num4;
		float desiredSliderSafely = num6 + backdriveFromValue;
		SetDesiredSliderSafely(desiredSliderSafely);
		if (!(num3 < 0.9999f))
		{
			GunController gunController3 = gun;
			backdriveActive = false;
			backdrivePhase = BackdrivePhase.None;
			if (!gunController3.isReloading && lockInputUntilRestoreComplete && disableDesiredInteractableDuringOverride && desiredInteractable != null)
			{
				desiredInteractable.enabled = true;
			}
		}
		goto IL_070c;
	}

	private void OnDesiredSliderValueChanged(float valueDeg)
	{
		bool flag = gun == null;
		if (flag || overrideActive != flag || reloadBackdriveDelayPending != flag || backdriveActive != flag || suppressDesiredCallback != flag)
		{
			return;
		}
		bool flag2 = clampValuesToLimits == flag;
		float desiredElevationFromSlider = valueDeg;
		if (!flag2)
		{
			if (!(minDeg > valueDeg))
			{
				bool flag3 = !(valueDeg > maxDeg);
				desiredElevationFromSlider = valueDeg;
				if (!flag3)
				{
					desiredElevationFromSlider = maxDeg;
				}
			}
			else
			{
				desiredElevationFromSlider = minDeg;
			}
		}
		gun.SetDesiredElevationFromSlider(desiredElevationFromSlider);
		desiredSliderVisualValue = desiredElevationFromSlider;
		desiredSliderVisualInitialized = true;
	}

	public void SetInteractiveSliderVisualOnly(float valueDeg)
	{
		bool flag = desiredSlider == null;
		if (flag)
		{
			return;
		}
		bool flag2 = clampValuesToLimits == flag;
		float sliderValue = valueDeg;
		if (!flag2)
		{
			if (!(minDeg > valueDeg))
			{
				bool flag3 = !(valueDeg > maxDeg);
				sliderValue = valueDeg;
				if (!flag3)
				{
					sliderValue = maxDeg;
				}
			}
			else
			{
				sliderValue = minDeg;
			}
		}
		suppressDesiredCallback = true;
		desiredSlider.SetSliderValue(sliderValue);
		desiredSliderVisualValue = sliderValue;
		suppressDesiredCallback = false;
		desiredSliderVisualInitialized = true;
		if (overrideActive || backdriveActive || reloadBackdriveDelayPending)
		{
			LinearSliderInteractable linearSliderInteractable = desiredSlider;
			if (linearSliderInteractable.isDragging)
			{
				linearSliderInteractable.EndSliderDrag();
			}
		}
	}

	private float GetClampedDesired()
	{
		//IL_0058: Expected F4, but got I4
		float num;
		if (gun != null)
		{
			GunController gunController = gun;
			num = gunController._003CDesiredElevationAngle_003Ek__BackingField;
		}
		else
		{
			num = 0f;
		}
		if (clampValuesToLimits)
		{
			if (!(minDeg > num))
			{
				if (num > maxDeg)
				{
					return maxDeg;
				}
			}
			else
			{
				num = minDeg;
			}
		}
		return num;
	}

	private float GetClampedCurrent()
	{
		//IL_0058: Expected F4, but got I4
		float num;
		if (gun != null)
		{
			GunController gunController = gun;
			num = gunController._003CCurrentElevation_003Ek__BackingField;
		}
		else
		{
			num = 0f;
		}
		if (clampValuesToLimits)
		{
			if (!(minDeg > num))
			{
				if (num > maxDeg)
				{
					return maxDeg;
				}
			}
			else
			{
				num = minDeg;
			}
		}
		return num;
	}

	private void SetDesiredSliderSafely(float valueDeg)
	{
		bool flag = desiredSlider == null;
		if (flag)
		{
			return;
		}
		bool flag2 = clampValuesToLimits == flag;
		float sliderValue = valueDeg;
		if (!flag2)
		{
			if (!(minDeg > valueDeg))
			{
				bool flag3 = !(valueDeg > maxDeg);
				sliderValue = valueDeg;
				if (!flag3)
				{
					sliderValue = maxDeg;
				}
			}
			else
			{
				sliderValue = minDeg;
			}
		}
		suppressDesiredCallback = true;
		desiredSlider.SetSliderValue(sliderValue);
		desiredSliderVisualValue = sliderValue;
		suppressDesiredCallback = false;
		desiredSliderVisualInitialized = true;
		if (overrideActive || backdriveActive || reloadBackdriveDelayPending)
		{
			LinearSliderInteractable linearSliderInteractable = desiredSlider;
			if (linearSliderInteractable.isDragging)
			{
				linearSliderInteractable.EndSliderDrag();
			}
		}
	}

	private void SetGhostSliderSafely(LinearSliderInteractable slider, float valueDeg)
	{
		bool flag = slider == null;
		if (flag)
		{
			return;
		}
		bool flag2 = clampValuesToLimits == flag;
		float sliderValue = valueDeg;
		if (!flag2)
		{
			if (!(minDeg > valueDeg))
			{
				bool flag3 = !(valueDeg > maxDeg);
				sliderValue = valueDeg;
				if (!flag3)
				{
					sliderValue = maxDeg;
				}
			}
			else
			{
				sliderValue = minDeg;
			}
		}
		slider.SetSliderValue(sliderValue);
		if (slider.isDragging)
		{
			slider.EndSliderDrag();
		}
	}

	private void MakeSliderOutputOnly(LinearSliderInteractable slider)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_004f: Expected O, but got I4
		//IL_0058: Expected O, but got I4
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00c2: Expected O, but got I4
		//IL_00cb: Expected O, but got I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		if (slider != null)
		{
			Interactable[] componentsInChildren = slider.GetComponentsInChildren<Interactable>(includeInactive: true);
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < componentsInChildren.Length)
			{
				((Behaviour)obj).enabled = false;
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
			Collider[] componentsInChildren2 = slider.GetComponentsInChildren<Collider>(includeInactive: true);
			object obj4 = componentsInChildren2 + 32;
			object obj5 = 0;
			object obj6 = 0;
			while ((nint)obj6 < componentsInChildren2.Length)
			{
				((Collider)obj4).enabled = false;
				obj5++;
				obj4 += 8;
				obj6 = obj5;
			}
		}
	}

	private float GetInteractiveVisualValue()
	{
		if (!desiredSliderVisualInitialized)
		{
			return desiredSliderVisualValue = GetClampedDesired();
		}
		return desiredSliderVisualValue;
	}

	private void StartBackdrive(BackdrivePhase phase, float from, float to, float seconds)
	{
		//IL_0232: Invalid comparison between I4 and F4
		//IL_0244: Expected F4, but got I4
		//IL_0270: Invalid comparison between I4 and F4
		bool flag = !clampValuesToLimits;
		backdrivePhase = phase;
		backdriveActive = true;
		float num = from;
		if (!flag)
		{
			if (!(minDeg > from))
			{
				bool flag2 = !(from > maxDeg);
				num = from;
				if (!flag2)
				{
					num = maxDeg;
				}
			}
			else
			{
				num = minDeg;
			}
		}
		backdriveFromValue = num;
		bool flag3 = !clampValuesToLimits;
		float desiredSliderSafely = to;
		if (!flag3)
		{
			if (!(minDeg > to))
			{
				bool flag4 = !(to > maxDeg);
				desiredSliderSafely = to;
				if (!flag4)
				{
					desiredSliderSafely = maxDeg;
				}
			}
			else
			{
				desiredSliderSafely = minDeg;
			}
		}
		backdriveToValue = desiredSliderSafely;
		float num2 = default(float);
		bool flag5 = !(0f < num2);
		float num3 = 0f;
		if (!flag5)
		{
			num3 = num2;
		}
		backdriveDuration = num3;
		backdriveElapsed = 0f;
		if (!(0f < num3))
		{
			SetDesiredSliderSafely(desiredSliderSafely);
			GunController gunController = gun;
			backdriveActive = false;
			backdrivePhase = BackdrivePhase.None;
			if (!gunController.isReloading && lockInputUntilRestoreComplete && disableDesiredInteractableDuringOverride && desiredInteractable != null)
			{
				desiredInteractable.enabled = true;
			}
		}
	}
}
