using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GunElevationDialBinding : MonoBehaviour
{
	public enum BackdriveSource
	{
		CurrentElevation,
		DesiredElevation
	}

	private GunController gun;

	private DialInteractable elevationDial;

	private GunElevationSliderBinding sliderBindingForVisualSync;

	private bool detectAndSignalSliderOverride = true;

	public UnityEvent OnDialOverrideSliderBegan;

	public UnityEvent OnDialOverrideSliderEnded;

	private bool requireMovementOrDeltaForSliderOverride = true;

	private float sliderOverrideSpeedThresholdDegPerSec = 0.5f;

	private float sliderOverrideDeltaThresholdDeg = 0.5f;

	private LinearSliderInteractable desiredSliderGhost;

	private bool autoFindTurretController = true;

	private TurretController turretController;

	private bool clampValuesToLimits = true;

	private float dialDegreesPerElevationDegree = 4f;

	private float elevationOffset;

	private bool backdriveDial = true;

	private BackdriveSource backdriveSource;

	private bool backdriveUseDialSmoothing = true;

	private bool dragOverridesElevationSpeedDial;

	private DialInteractable elevationSpeedDialToOverride;

	private bool ignoreDialWhileReloading = true;

	public UnityEvent OnElevationDragOverrideSpeedDial;

	public UnityEvent OnElevationOverrideBegan;

	public UnityEvent OnElevationOverrideEnded;

	private float minDeg;

	private float maxDeg = 45f;

	private bool dialDragActive;

	private float dialBaseElevationDeg;

	private bool overrideActiveThisDrag_SpeedDial;

	private bool overrideActiveThisDrag_Slider;

	private bool logWarnings = true;

	private void Awake()
	{
		if (autoFindTurretController && this.turretController == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			TurretController turretController = default(TurretController);
			this.turretController = turretController;
		}
		ResolveLimits();
		bool flag = elevationDial != null;
		if (!flag)
		{
			if (logWarnings != flag)
			{
				Debug.LogWarning("[GunElevationDialBinding] Elevation Dial is not assigned. This binding will do nothing.", this);
			}
		}
		else
		{
			Action value = OnBeginDialDrag;
			elevationDial.OnBeginDialDrag += value;
			Action value2 = OnEndDialDrag;
			elevationDial.OnEndDialDrag += value2;
		}
		if (gun == null && logWarnings)
		{
			Debug.LogWarning("[GunElevationDialBinding] No GunController assigned.", this);
		}
	}

	private void OnDestroy()
	{
		if (elevationDial != null)
		{
			Action value = OnBeginDialDrag;
			elevationDial.OnBeginDialDrag -= value;
			Action value2 = OnEndDialDrag;
			elevationDial.OnEndDialDrag -= value2;
		}
	}

	private void Update()
	{
		//IL_01bc: Expected O, but got I4
		//IL_01d6: Expected O, but got I4
		//IL_0241: Invalid comparison between F4 and I
		//IL_0268: Expected F4, but got I
		if (!(gun != null) || !(elevationDial != null))
		{
			return;
		}
		GunController gunController = gun;
		if (!dialDragActive)
		{
			if (!backdriveDial)
			{
				return;
			}
			float num = ((backdriveSource != BackdriveSource.CurrentElevation) ? gunController._003CDesiredElevationAngle_003Ek__BackingField : gunController._003CCurrentElevation_003Ek__BackingField);
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
			DialInteractable dialInteractable = elevationDial;
			if (dialInteractable.dialMode != DialInteractable.DialMode.Unlimited)
			{
				dialInteractable.SetDialValue(num);
				return;
			}
			float num2 = num - elevationOffset;
			float angleDegrees = num2 * dialDegreesPerElevationDegree;
			dialInteractable.SetAccumulatedValueUnlimited(angleDegrees, fireValueChangedEvent: false, backdriveUseDialSmoothing);
			return;
		}
		object obj = ignoreDialWhileReloading & gunController.isReloading;
		bool flag = obj == null;
		object obj2 = !flag;
		if (obj2 != null)
		{
			return;
		}
		DialInteractable dialInteractable2 = elevationDial;
		float num3 = dialInteractable2.accumulatedValue;
		if (dialInteractable2.dialMode == DialInteractable.DialMode.Unlimited)
		{
			float num4 = dialDegreesPerElevationDegree;
			float num5 = dialDegreesPerElevationDegree;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			if (num5 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
				num4 = 0f;
			}
			float num6 = num3 / num4;
			num3 = num6 + elevationOffset;
		}
		bool flag2 = !clampValuesToLimits;
		float num7 = num3 + dialBaseElevationDeg;
		if (!flag2)
		{
			if (!(minDeg > num7))
			{
				if (num7 > maxDeg)
				{
					num7 = maxDeg;
				}
			}
			else
			{
				num7 = minDeg;
			}
		}
		gun.SetDesiredElevationFromDial(num7);
		if (sliderBindingForVisualSync != null)
		{
			sliderBindingForVisualSync.SetInteractiveSliderVisualOnly(num7);
		}
		if (desiredSliderGhost != null)
		{
			desiredSliderGhost.SetSliderValue(num7);
		}
	}

	private void OnBeginDialDrag()
	{
		//IL_0146: Invalid comparison between F4 and I
		//IL_016d: Expected F4, but got I
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Expected O, but got Unknown
		//IL_0624: Invalid comparison between O and F4
		//IL_047c: Expected O, but got I4
		//IL_03b3: Expected O, but got I4
		//IL_0383: Expected O, but got I4
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Expected O, but got Unknown
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Expected O, but got Unknown
		//IL_0435: Invalid comparison between O and F4
		//IL_0456: Invalid comparison between O and F4
		//IL_0465: Expected O, but got I4
		if (!(gun != null) || !(elevationDial != null))
		{
			return;
		}
		GunController gunController = gun;
		float num = ((backdriveSource != BackdriveSource.CurrentElevation) ? gunController._003CDesiredElevationAngle_003Ek__BackingField : gunController._003CCurrentElevation_003Ek__BackingField);
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
		DialInteractable dialInteractable = elevationDial;
		float num2 = dialInteractable.accumulatedValue;
		if (dialInteractable.dialMode == DialInteractable.DialMode.Unlimited)
		{
			float num3 = dialDegreesPerElevationDegree;
			float num4 = dialDegreesPerElevationDegree;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			if (num4 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
				num3 = 0f;
			}
			float num5 = num2 / num3;
			num2 = num5 + elevationOffset;
		}
		float num6 = num - num2;
		overrideActiveThisDrag_SpeedDial = false;
		dialBaseElevationDeg = num6;
		if (!dragOverridesElevationSpeedDial || !(elevationSpeedDialToOverride != null))
		{
			goto IL_05dc;
		}
		DialInteractable dialInteractable2 = elevationSpeedDialToOverride;
		float num7 = dialInteractable2.accumulatedValue;
		bool flag = -1f > dialInteractable2.accumulatedValue;
		float num8 = -1f;
		if (!flag)
		{
			bool flag2 = !(dialInteractable2.accumulatedValue > 1f);
			num8 = 1f;
			if (flag2)
			{
				goto IL_0606;
			}
		}
		num7 = num8;
		goto IL_0606;
		IL_064e:
		if (ignoreDialWhileReloading)
		{
			GunController gunController2 = gun;
			if (gunController2.isReloading)
			{
				goto IL_066d;
			}
		}
		object obj;
		if (obj != null)
		{
			if ((object)sliderBindingForVisualSync != null)
			{
				sliderBindingForVisualSync.ForceEndInteractiveDrag();
			}
			if (OnDialOverrideSliderBegan != null)
			{
				OnDialOverrideSliderBegan.Invoke();
			}
			overrideActiveThisDrag_Slider = true;
		}
		goto IL_066d;
		IL_05dc:
		overrideActiveThisDrag_Slider = false;
		if (detectAndSignalSliderOverride)
		{
			bool flag3;
			if (!(sliderBindingForVisualSync != null))
			{
				flag3 = false;
			}
			else
			{
				bool isUserDragging = sliderBindingForVisualSync.IsUserDragging;
				flag3 = isUserDragging;
			}
			bool flag4 = gun != null;
			bool flag5 = !flag4;
			bool flag6 = false;
			if (!flag5)
			{
				GunController gunController3 = gun;
				object obj2 = gunController3.lastCommandSource - 1;
				bool flag7 = obj2 == null;
				flag6 = flag7;
			}
			if (!flag3)
			{
				bool flag8 = !flag6;
				obj = 0;
				if (flag8)
				{
					goto IL_064e;
				}
				if (requireMovementOrDeltaForSliderOverride)
				{
					GunController gunController4 = gun;
					float num9 = gunController4._003CCurrentElevationSpeed_003Ek__BackingField;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj3 = num9 & 0;
					float elevationErrorDeg = gunController4.ElevationErrorDeg;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj4 = elevationErrorDeg & 0;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)sliderOverrideSpeedThresholdDegPerSec))
					{
						bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)sliderOverrideDeltaThresholdDeg);
						obj = 0;
						if (flag9)
						{
							goto IL_064e;
						}
					}
				}
			}
			obj = 1;
			goto IL_064e;
		}
		goto IL_066d;
		IL_0606:
		float num10 = num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj5 = num10 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f))
		{
			if (dialInteractable2.dialMode != DialInteractable.DialMode.Limited)
			{
				dialInteractable2.SetAccumulatedValueUnlimited(0f, fireValueChangedEvent: true);
			}
			else
			{
				dialInteractable2.SetDialValue(0f);
			}
			if (OnElevationDragOverrideSpeedDial != null)
			{
				OnElevationDragOverrideSpeedDial.Invoke();
			}
			if (OnElevationOverrideBegan != null)
			{
				OnElevationOverrideBegan.Invoke();
			}
			overrideActiveThisDrag_SpeedDial = true;
		}
		goto IL_05dc;
		IL_066d:
		dialDragActive = true;
	}

	private void OnEndDialDrag()
	{
		if (overrideActiveThisDrag_SpeedDial)
		{
			if (OnElevationOverrideEnded != null)
			{
				OnElevationOverrideEnded.Invoke();
			}
			overrideActiveThisDrag_SpeedDial = false;
		}
		if (overrideActiveThisDrag_Slider)
		{
			if (OnDialOverrideSliderEnded != null)
			{
				OnDialOverrideSliderEnded.Invoke();
			}
			overrideActiveThisDrag_Slider = false;
		}
		dialDragActive = false;
	}

	private void ResolveLimits()
	{
		if (this.turretController == null)
		{
			bool flag = gun != null;
			if (!flag)
			{
				minDeg = 0f;
				maxDeg = 45f;
				if (logWarnings != flag)
				{
					Debug.LogWarning("[GunElevationDialBinding] No GunController or TurretController assigned. Using default [0..45] limits.", this);
				}
				return;
			}
			GunController gunController = gun;
			minDeg = gunController._003CMinElevationAngle_003Ek__BackingField;
			GunController gunController2 = gun;
			bool flag2 = !logWarnings;
			float num = gunController2._003CMinElevationAngle_003Ek__BackingField + 45f;
			maxDeg = num;
			if (!flag2)
			{
				Debug.LogWarning("[GunElevationDialBinding] No TurretController found. Using Gun.MinElevationAngle and +45° for limits.", this);
			}
			return;
		}
		TurretController turretController = this.turretController;
		bool flag3 = !(turretController.minBarrelElevation > turretController.maxBarrelElevation);
		float num2 = turretController.maxBarrelElevation;
		float num3 = turretController.minBarrelElevation;
		if (!flag3)
		{
			bool flag4 = !logWarnings;
			num2 = turretController.minBarrelElevation;
			num3 = turretController.maxBarrelElevation;
			if (!flag4)
			{
				Debug.LogWarning("[GunElevationDialBinding] TurretController elevation limits are inverted; correcting.", turretController);
				num2 = turretController.minBarrelElevation;
				num3 = turretController.maxBarrelElevation;
			}
		}
		minDeg = num3;
		maxDeg = num2;
	}

	private float MapDialToElevation(float dialDegrees)
	{
		//IL_001c: Invalid comparison between F4 and I
		//IL_0043: Expected F4, but got I
		float num = dialDegreesPerElevationDegree;
		float num2 = dialDegreesPerElevationDegree;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
		if (num2 < 0f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			num = 0f;
		}
		float num3 = dialDegrees / num;
		return num3 + elevationOffset;
	}

	private float MapElevationToDialDegrees(float elevationDeg)
	{
		float num = elevationDeg - elevationOffset;
		return num * dialDegreesPerElevationDegree;
	}
}
