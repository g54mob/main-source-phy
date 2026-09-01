using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GunStopwatch : MonoBehaviour
{
	public enum RotationAxis
	{
		Z,
		X,
		Y,
		NegativeZ,
		NegativeX,
		NegativeY
	}

	public enum LocalComputationMode
	{
		MirrorGunRangeModel,
		UISpacePathLength
	}

	private enum WatchState
	{
		Predicting,
		AwaitFireDelay,
		CountingDown,
		ImpactHold
	}

	public GunController watchedGun;

	public Transform handTransform;

	public float secondsPerFullRotation;

	public float zeroAngle;

	public RotationAxis rotationAxis;

	public AnimationCurve tickCurve;

	public bool useGunPredictions;

	public bool zeroWhenNoShellLoaded;

	public LocalComputationMode localComputationMode;

	public bool fallbackToRangeDivSpeedIfNoUIContext;

	public bool autoFindImpactMarkerManager;

	public ImpactMarkerManager impactMarkerManagerOverride;

	public UnityEvent onCountdownStarted;

	public UnityEvent onCountdownFinished;

	public UnityEvent onFiveSecondsRemaining;

	public UnityEvent onTick;

	public bool tickOnStateEnter;

	private float lastPredictedTravelTime;

	private string state;

	private WatchState currentState;

	private Quaternion initialHandRotation;

	private double countdownStartTime;

	private float latchedTravelTime;

	private int lastTickWholeSecond;

	private bool hasFiredFiveSecondsEventThisShot;

	private float previousCountingDownRemainingSeconds;

	private ImpactMarkerManager cachedImpactManager;

	private RectTransform cachedParentRect;

	private const float FiveSecondsThreshold = 5f;

	private void Awake()
	{
		//IL_004c: Expected O, but got F4
		if (handTransform != null)
		{
			initialHandRotation = (Quaternion)handTransform.localRotation.x;
		}
	}

	private void OnEnable()
	{
		//IL_02b4: Invalid comparison between I4 and F4
		//IL_02c6: Expected F4, but got I4
		//IL_036f: Invalid comparison between I4 and F4
		//IL_0381: Expected F4, but got I4
		//IL_031b: Expected F4, but got I4
		//IL_027c: Invalid comparison between I4 and F4
		//IL_028e: Expected F4, but got I4
		//IL_0215: Invalid comparison between I4 and F4
		//IL_0227: Expected F4, but got I4
		//IL_049c: Expected F4, but got I4
		//IL_03ae: Expected O, but got I4
		//IL_035f: Expected F4, but got I4
		//IL_04f7: Invalid comparison between I4 and F4
		//IL_0509: Expected F4, but got I4
		//IL_03d7: Expected F4, but got I4
		//IL_04de: Expected F4, but got I4
		//IL_0443: Invalid comparison between I4 and F8
		//IL_0455: Expected F8, but got I4
		if (watchedGun != null)
		{
			Action value = HandleGunFired;
			watchedGun.OnGunFired += value;
			Action value2 = HandleShellLaunched;
			watchedGun.OnShellLaunched += value2;
			Action<float> value3 = HandlePredictedImpactTimeChanged;
			watchedGun.OnPredictedImpactTimeChanged += value3;
		}
		if (cachedImpactManager == null && autoFindImpactMarkerManager)
		{
			ImpactMarkerManager impactMarkerManager = UnityEngine.Object.FindObjectOfType<ImpactMarkerManager>();
			cachedImpactManager = impactMarkerManager;
			Transform transform2;
			if (cachedImpactManager != null)
			{
				Transform transform = cachedImpactManager.transform;
				bool flag = (object)transform == null;
				transform2 = null;
				if (!flag)
				{
					bool flag2 = (object)transform.GetType() != typeof(RectTransform);
					transform2 = null;
					if (!flag2)
					{
						transform2 = transform;
					}
				}
			}
			else
			{
				transform2 = null;
			}
			cachedParentRect = (RectTransform)transform2;
		}
		if (useGunPredictions && watchedGun != null)
		{
			GunController gunController = watchedGun;
			bool flag3 = !(0f < gunController._003CPredictedImpactTime_003Ek__BackingField);
			float num = 0f;
			if (!flag3)
			{
				num = gunController._003CPredictedImpactTime_003Ek__BackingField;
			}
			lastPredictedTravelTime = num;
		}
		if (HasLoadedShell())
		{
			if (!useGunPredictions)
			{
				float num2 = ComputeTravelTimeLocally();
				bool flag4 = !(0f < num2);
				float num3 = 0f;
				if (!flag4)
				{
					num3 = num2;
				}
				lastPredictedTravelTime = num3;
			}
			else
			{
				bool flag5 = !(0f < lastPredictedTravelTime);
				float num4 = 0f;
				if (!flag5)
				{
					num4 = lastPredictedTravelTime;
				}
				lastPredictedTravelTime = num4;
			}
		}
		else if (zeroWhenNoShellLoaded)
		{
			lastPredictedTravelTime = 0f;
		}
		float seconds;
		if (currentState != WatchState.Predicting)
		{
			seconds = 0f;
		}
		else
		{
			bool flag6 = HasLoadedShell();
			if (!flag6 && zeroWhenNoShellLoaded != flag6)
			{
				seconds = 0f;
			}
			else
			{
				bool flag7 = !(0f < lastPredictedTravelTime);
				seconds = 0f;
				if (!flag7)
				{
					seconds = lastPredictedTravelTime;
				}
			}
		}
		ApplyHandFromSeconds(seconds);
		bool flag8 = currentState == WatchState.Predicting;
		float displayedSeconds;
		if (!flag8)
		{
			object obj = currentState - 1;
			if (flag8)
			{
				goto IL_0470;
			}
			bool flag9 = (nint)obj != 1;
			displayedSeconds = 0f;
			if (!flag9)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [rbx+0A8h]\"");
				if ((nint)obj > 1)
				{
					goto IL_0470;
				}
				double timeAsDouble = Time.timeAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rbx+0A8h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
				double num5 = (double)latchedTravelTime - timeAsDouble;
				bool flag10 = !(0.0 < num5);
				double num6 = 0.0;
				if (!flag10)
				{
					num6 = num5;
				}
				displayedSeconds = (float)num6;
			}
		}
		else
		{
			bool flag11 = currentState != WatchState.Predicting;
			displayedSeconds = 0f;
			if (!flag11)
			{
				bool flag12 = HasLoadedShell();
				if (!flag12)
				{
					bool flag13 = zeroWhenNoShellLoaded != flag12;
					displayedSeconds = 0f;
					if (flag13)
					{
						goto IL_05dc;
					}
				}
				bool flag14 = !(0f < lastPredictedTravelTime);
				displayedSeconds = 0f;
				if (!flag14)
				{
					displayedSeconds = lastPredictedTravelTime;
				}
			}
		}
		goto IL_05dc;
		IL_0470:
		displayedSeconds = latchedTravelTime;
		goto IL_05dc;
		IL_05dc:
		ResetTickTracking(displayedSeconds);
		hasFiredFiveSecondsEventThisShot = false;
		previousCountingDownRemainingSeconds = -1f;
	}

	private void OnDisable()
	{
		if (watchedGun != null)
		{
			Action value = HandleGunFired;
			watchedGun.OnGunFired -= value;
			Action value2 = HandleShellLaunched;
			watchedGun.OnShellLaunched -= value2;
			Action<float> value3 = HandlePredictedImpactTimeChanged;
			watchedGun.OnPredictedImpactTimeChanged -= value3;
		}
	}

	private void Update()
	{
		//IL_002f: Expected O, but got I4
		//IL_03a9: Invalid comparison between I4 and F4
		//IL_03bb: Expected F4, but got I4
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_0111: Invalid comparison between I4 and F8
		//IL_0123: Expected F8, but got I4
		//IL_025d: Invalid comparison between I4 and F4
		bool flag = currentState == WatchState.Predicting;
		if (!flag)
		{
			object obj = currentState - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 == 1)
					{
						ApplyHandFromSeconds(0f);
						if (HasLoadedShell() && lastPredictedTravelTime > 0.0001f)
						{
							SetState(WatchState.Predicting);
						}
					}
					return;
				}
				double timeAsDouble = Time.timeAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rbx+0A8h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
				double num = (double)latchedTravelTime - timeAsDouble;
				bool flag2 = !(0.0 < num);
				double num2 = 0.0;
				if (!flag2)
				{
					num2 = num;
				}
				ApplyHandFromSeconds((float)num2);
				if (Application.isPlaying && currentState == WatchState.CountingDown)
				{
					int num3 = FloorToWholeSecond((float)num2);
					if (num3 != lastTickWholeSecond)
					{
						bool flag3 = onTick == null;
						lastTickWholeSecond = num3;
						if (!flag3)
						{
							onTick.Invoke();
						}
					}
				}
				if (Application.isPlaying && currentState == WatchState.CountingDown)
				{
					if (!hasFiredFiveSecondsEventThisShot)
					{
						if (0f > previousCountingDownRemainingSeconds)
						{
							previousCountingDownRemainingSeconds = (float)num2;
						}
						if (!(previousCountingDownRemainingSeconds < 5f) && 5.0 > num2)
						{
							bool flag4 = onFiveSecondsRemaining == null;
							hasFiredFiveSecondsEventThisShot = true;
							if (!flag4)
							{
								onFiveSecondsRemaining.Invoke();
							}
						}
					}
					previousCountingDownRemainingSeconds = (float)num2;
				}
				if (!(9.999999747378752E-05 < num2))
				{
					if (onCountdownFinished != null)
					{
						onCountdownFinished.Invoke();
					}
					SetState(WatchState.ImpactHold);
				}
			}
			else
			{
				ApplyHandFromSeconds(latchedTravelTime);
			}
			return;
		}
		RefreshLivePrediction();
		if (currentState == WatchState.Predicting)
		{
			bool flag5 = HasLoadedShell();
			if (flag5 || zeroWhenNoShellLoaded == flag5)
			{
				bool flag6 = !(0f < lastPredictedTravelTime);
				float seconds = 0f;
				if (!flag6)
				{
					seconds = lastPredictedTravelTime;
				}
				ApplyHandFromSeconds(seconds);
				return;
			}
		}
		ApplyHandFromSeconds(0f);
	}

	private void SubscribeToGun()
	{
		if (watchedGun != null)
		{
			Action value = HandleGunFired;
			watchedGun.OnGunFired += value;
			Action value2 = HandleShellLaunched;
			watchedGun.OnShellLaunched += value2;
			Action<float> value3 = HandlePredictedImpactTimeChanged;
			watchedGun.OnPredictedImpactTimeChanged += value3;
		}
	}

	private void UnsubscribeFromGun()
	{
		if (watchedGun != null)
		{
			Action value = HandleGunFired;
			watchedGun.OnGunFired -= value;
			Action value2 = HandleShellLaunched;
			watchedGun.OnShellLaunched -= value2;
			Action<float> value3 = HandlePredictedImpactTimeChanged;
			watchedGun.OnPredictedImpactTimeChanged -= value3;
		}
	}

	private void HandlePredictedImpactTimeChanged(float predictedSeconds)
	{
		//IL_0089: Invalid comparison between I4 and F4
		//IL_009b: Expected F4, but got I4
		if (currentState == WatchState.CountingDown || currentState == WatchState.AwaitFireDelay)
		{
			return;
		}
		if (useGunPredictions && HasLoadedShell())
		{
			bool flag = !(0f < predictedSeconds);
			float num = 0f;
			if (!flag)
			{
				num = predictedSeconds;
			}
			lastPredictedTravelTime = num;
		}
		if (currentState == WatchState.ImpactHold && lastPredictedTravelTime > 0.0001f && HasLoadedShell())
		{
			SetState(WatchState.Predicting);
		}
	}

	private void HandleGunFired()
	{
		//IL_008c: Expected F4, but got I4
		//IL_00ad: Invalid comparison between I4 and F4
		//IL_00bf: Expected F4, but got I4
		if (watchedGun != null)
		{
			float num = ((!HasLoadedShell()) ? 0f : ((!useGunPredictions) ? ComputeTravelTimeLocally() : lastPredictedTravelTime));
			bool flag = !(0f < num);
			float num2 = 0f;
			if (!flag)
			{
				num2 = num;
			}
			hasFiredFiveSecondsEventThisShot = false;
			previousCountingDownRemainingSeconds = -1f;
			latchedTravelTime = num2;
			SetState(WatchState.AwaitFireDelay);
		}
	}

	private void HandleShellLaunched()
	{
		//IL_00ab: Expected F4, but got I4
		//IL_0115: Invalid comparison between I4 and F4
		//IL_0127: Expected F4, but got I4
		if (!(watchedGun != null))
		{
			return;
		}
		if (currentState != WatchState.AwaitFireDelay)
		{
			float num = ((!HasLoadedShell()) ? 0f : ((!useGunPredictions) ? ComputeTravelTimeLocally() : lastPredictedTravelTime));
			bool flag = !(0f < num);
			float num2 = 0f;
			if (!flag)
			{
				num2 = num;
			}
			hasFiredFiveSecondsEventThisShot = false;
			previousCountingDownRemainingSeconds = -1f;
			latchedTravelTime = num2;
		}
		double timeAsDouble = Time.timeAsDouble;
		countdownStartTime = timeAsDouble;
		SetState(WatchState.CountingDown);
		if (onCountdownStarted != null)
		{
			onCountdownStarted.Invoke();
		}
	}

	private void RefreshLivePrediction()
	{
		//IL_0090: Invalid comparison between I4 and F4
		//IL_00a2: Expected F4, but got I4
		//IL_0058: Invalid comparison between I4 and F4
		//IL_006a: Expected F4, but got I4
		if (HasLoadedShell())
		{
			if (!useGunPredictions)
			{
				float num = ComputeTravelTimeLocally();
				bool flag = !(0f < num);
				float num2 = 0f;
				if (!flag)
				{
					num2 = num;
				}
				lastPredictedTravelTime = num2;
			}
			else
			{
				bool flag2 = !(0f < lastPredictedTravelTime);
				float num3 = 0f;
				if (!flag2)
				{
					num3 = lastPredictedTravelTime;
				}
				lastPredictedTravelTime = num3;
			}
		}
		else if (zeroWhenNoShellLoaded)
		{
			lastPredictedTravelTime = 0f;
		}
	}

	private float GetCurrentPredictedTravelTime()
	{
		//IL_005e: Expected F4, but got I4
		if (HasLoadedShell())
		{
			if (useGunPredictions)
			{
				return lastPredictedTravelTime;
			}
			return ComputeTravelTimeLocally();
		}
		return 0f;
	}

	private unsafe float ComputeTravelTimeLocally()
	{
		//IL_0417: Expected F4, but got I4
		//IL_00a5: Invalid comparison between I4 and F4
		//IL_0458: Expected O, but got Ref
		//IL_0458: Expected O, but got Ref
		//IL_0499: Expected I, but got O
		if (!(watchedGun != null))
		{
			goto IL_040e;
		}
		float num;
		if ((object)watchedGun != null)
		{
			ShellBlueprint chamberedShellBlueprint = watchedGun.ChamberedShellBlueprint;
			if (!(chamberedShellBlueprint != null))
			{
				goto IL_040e;
			}
			if ((object)chamberedShellBlueprint != null)
			{
				float adjustedShellSpeed = chamberedShellBlueprint.GetAdjustedShellSpeed();
				bool flag = 0f < adjustedShellSpeed;
				num = adjustedShellSpeed;
				if (!flag)
				{
					num = 1f;
				}
				if (localComputationMode == LocalComputationMode.MirrorGunRangeModel || localComputationMode != LocalComputationMode.UISpacePathLength)
				{
					goto IL_03b9;
				}
				if (!ResolveImpactContext())
				{
					goto IL_039a;
				}
				GunController gunController = watchedGun;
				if ((object)watchedGun != null)
				{
					if (!(gunController.firePoint != null))
					{
						goto IL_039a;
					}
					GunController gunController2 = watchedGun;
					if ((object)watchedGun != null && (object)gunController2.firePoint != null)
					{
						Vector3 localPosition = gunController2.firePoint.localPosition;
						ImpactMarkerManager impactMarkerManager = cachedImpactManager;
						bool flag2 = (object)cachedImpactManager == null;
						adjustedShellSpeed = localPosition.x;
						if (!flag2)
						{
							GunController gunController3 = watchedGun;
							TurretController turretController = impactMarkerManager.turretController;
							bool flag3 = (object)watchedGun == null;
							adjustedShellSpeed = localPosition.x;
							if (!flag3)
							{
								float num2 = watchedGun.MapElevationToRange(gunController3._003CCurrentElevation_003Ek__BackingField);
								bool flag4 = (object)impactMarkerManager.turretController == null;
								adjustedShellSpeed = num2;
								if (!flag4)
								{
									Vector3 euler = default(Vector3);
									Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
									object obj = default(object);
									Vector3 vector = (Quaternion)(&obj) * (Vector3)(&euler);
									adjustedShellSpeed = vector.x;
									if ((object)turretController.turretBase != null)
									{
										Vector3 localPosition2 = turretController.turretBase.localPosition;
										float num3 = vector.x * num2;
										object obj2 = default(object);
										float num4 = (float)obj2 * num2;
										float num5 = num3 + localPosition2.x;
										object obj3 = default(object);
										float num6 = num4 + (float)obj3;
										nint num7 = (nint)typeof(Math);
										float num8 = localPosition.x - num5;
										object obj4 = default(object);
										float num9 = (float)obj4 - num6;
										float num10 = num9 * num9;
										float num11 = num8 * num8;
										float num12 = num10 + num11;
										Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rcx_v25 (Il2CppClass<System.Math>)+E4]");
										if ((nint)0 <= (nint)0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
											return 0f / num;
										}
										double num13 = Math.Sqrt(num12);
										Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
										return (float)num13 / num;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_041c;
		IL_040e:
		return 0f;
		IL_03b9:
		GunController gunController4 = watchedGun;
		if ((object)watchedGun != null)
		{
			float num14 = watchedGun.MapElevationToRange(gunController4._003CCurrentElevation_003Ek__BackingField);
			return num14 / num;
		}
		goto IL_041c;
		IL_041c:
		throw new NullReferenceException();
		IL_039a:
		if (fallbackToRangeDivSpeedIfNoUIContext)
		{
			goto IL_03b9;
		}
		goto IL_040e;
	}

	private bool ResolveImpactContext()
	{
		//IL_023d: Expected I4, but got O
		if (cachedImpactManager != null && cachedParentRect != null)
		{
			return true;
		}
		if (autoFindImpactMarkerManager || impactMarkerManagerOverride != null)
		{
			ImpactMarkerManager impactMarkerManager = ((!(impactMarkerManagerOverride != null)) ? UnityEngine.Object.FindObjectOfType<ImpactMarkerManager>() : impactMarkerManagerOverride);
			cachedImpactManager = impactMarkerManager;
			if (cachedImpactManager != null)
			{
				if ((object)cachedImpactManager != null)
				{
					Transform transform = cachedImpactManager.transform;
					if ((object)transform == null)
					{
						cachedParentRect = null;
					}
					else
					{
						bool flag = (object)transform.GetType() != typeof(RectTransform);
						Transform transform2 = null;
						if (!flag)
						{
							transform2 = transform;
						}
						cachedParentRect = (RectTransform)transform2;
						if ((object)transform.GetType() == typeof(RectTransform))
						{
							/*Error: End of method reached without returning.*/;
						}
					}
					if (!(cachedParentRect != null))
					{
						goto IL_0229;
					}
					ImpactMarkerManager impactMarkerManager2 = cachedImpactManager;
					if ((object)cachedImpactManager != null)
					{
						return impactMarkerManager2.turretController != null;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		goto IL_0229;
		IL_0229:
		return false;
	}

	private bool HasLoadedShell()
	{
		//IL_0111: Expected I4, but got O
		if (watchedGun != null)
		{
			GunController gunController = watchedGun;
			if ((object)watchedGun != null)
			{
				if (!(gunController.artilleryReloadController != null))
				{
					goto IL_00fd;
				}
				GunController gunController2 = watchedGun;
				if ((object)watchedGun != null)
				{
					ArtilleryReloadController artilleryReloadController = gunController2.artilleryReloadController;
					if ((object)gunController2.artilleryReloadController != null)
					{
						return artilleryReloadController.chamberedShell != null;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_00fd;
		IL_00fd:
		return false;
	}

	private bool HasMeaningfulPrediction()
	{
		//IL_002c: Invalid comparison between F4 and I4
		bool flag = lastPredictedTravelTime < 0.0001f;
		float num = lastPredictedTravelTime - 0.0001f;
		bool flag2 = num == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	private float GetDisplaySeconds()
	{
		//IL_0069: Invalid comparison between I4 and F4
		//IL_007b: Expected F4, but got I4
		//IL_0025: Expected F4, but got I4
		if (currentState == WatchState.Predicting)
		{
			bool flag = HasLoadedShell();
			if (flag || zeroWhenNoShellLoaded == flag)
			{
				bool flag2 = !(0f < lastPredictedTravelTime);
				float result = 0f;
				if (!flag2)
				{
					result = lastPredictedTravelTime;
				}
				return result;
			}
		}
		return 0f;
	}

	private unsafe void ApplyHandFromSeconds(float seconds)
	{
		//IL_0038: Invalid comparison between I4 and F4
		//IL_0074: Invalid comparison between I4 and F4
		//IL_00d1: Expected F4, but got I4
		//IL_01fe: Expected O, but got I4
		//IL_0304: Invalid comparison between I4 and F4
		//IL_0138: Expected F4, but got I4
		//IL_00f2: Expected O, but got I4
		//IL_0194: Expected O, but got I8
		//IL_01d9: Expected O, but got Ref
		//IL_01a6: Expected O, but got I8
		//IL_01c0: Expected O, but got I8
		//IL_0179: Expected O, but got I4
		if (!(handTransform != null) || !(0f < secondsPerFullRotation))
		{
			return;
		}
		float num2 = default(float);
		float num = MathF.Floor(num2);
		float num3 = num2 - num;
		if (!(0f > num3))
		{
			bool flag = !(num3 > 1f);
			num2 = 1f;
			if (!flag)
			{
				num2 = 1f;
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		bool flag2 = tickCurve == null;
		object obj = 0;
		if (!flag2)
		{
			float num4 = tickCurve.Evaluate(num3);
			obj = 0;
			num3 = num4;
		}
		float num5 = num3 + num;
		float x = num5 / secondsPerFullRotation;
		float num6 = MathF.Floor(x);
		float num7 = num6 * secondsPerFullRotation;
		float num8 = num5 - num7;
		if (!(0f > num8))
		{
			if (num8 > secondsPerFullRotation)
			{
				num8 = secondsPerFullRotation;
			}
		}
		else
		{
			num8 = 0f;
		}
		object obj2;
		if (this.rotationAxis != RotationAxis.NegativeZ && this.rotationAxis != RotationAxis.NegativeX)
		{
			bool flag3 = this.rotationAxis != RotationAxis.NegativeY;
			obj2 = 1;
			if (flag3)
			{
				goto IL_022b;
			}
		}
		obj2 = 4294967295L;
		goto IL_022b;
		IL_022b:
		float num9 = num8 / secondsPerFullRotation;
		RotationAxis rotationAxis = this.rotationAxis;
		float num10 = num9 * (float)obj2;
		float num11 = num10 * 360f;
		float num12 = zeroAngle - num11;
		if (this.rotationAxis <= RotationAxis.NegativeY)
		{
			object obj3 = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rdx_v6+4424C4+v283 @ rax_v8 (GunStopwatch+RotationAxis)*4]");
			object obj4 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v257 @ rcx_v10 (should have been resolved before IL gen)");
		}
		else
		{
			Vector3 euler = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		}
		float num13 = default(float);
		handTransform.localRotation = (Quaternion)(&num13);
	}

	private unsafe void SetState(WatchState newState)
	{
		//IL_0018: Expected O, but got Ref
		if (currentState == newState)
		{
			return;
		}
		currentState = newState;
		object obj = default(object);
		string text = ((Enum)(&obj)).ToString();
		state = text;
		if (currentState == WatchState.CountingDown)
		{
			float currentlyDisplayedSeconds = GetCurrentlyDisplayedSeconds();
			ResetTickTracking(currentlyDisplayedSeconds);
			float currentlyDisplayedSeconds2 = GetCurrentlyDisplayedSeconds();
			bool flag = !tickOnStateEnter;
			previousCountingDownRemainingSeconds = currentlyDisplayedSeconds2;
			if (!flag && Application.isPlaying && onTick != null)
			{
				onTick.Invoke();
			}
		}
	}

	private float GetCurrentlyDisplayedSeconds()
	{
		//IL_002f: Expected O, but got I4
		//IL_0161: Invalid comparison between I4 and F4
		//IL_0173: Expected F4, but got I4
		//IL_011d: Expected F4, but got I4
		//IL_0058: Expected F4, but got I4
		//IL_00c4: Invalid comparison between I4 and F8
		//IL_00d6: Expected F4, but got I4
		bool flag = currentState == WatchState.Predicting;
		float result;
		if (!flag)
		{
			object obj = currentState - 1;
			if (!flag)
			{
				bool flag2 = (nint)obj != 1;
				result = 0f;
				if (flag2)
				{
					goto IL_0190;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [rbx+0A8h]\"");
				if ((nint)obj <= 1)
				{
					double timeAsDouble = Time.timeAsDouble;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rbx+0A8h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
					double num = (double)latchedTravelTime - timeAsDouble;
					bool flag3 = !(0.0 < num);
					float result2 = 0f;
					if (!flag3)
					{
						result2 = (float)num;
					}
					return result2;
				}
			}
			return latchedTravelTime;
		}
		if (currentState == WatchState.Predicting)
		{
			bool flag4 = HasLoadedShell();
			if (flag4 || zeroWhenNoShellLoaded == flag4)
			{
				bool flag5 = !(0f < lastPredictedTravelTime);
				result = 0f;
				if (!flag5)
				{
					result = lastPredictedTravelTime;
				}
				goto IL_0190;
			}
		}
		return 0f;
		IL_0190:
		return result;
	}

	private void ResetTickTracking(float displayedSeconds)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_004b: Expected I4, but got F8
		if (0f < displayedSeconds)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num = Math.Floor(0.0);
			lastTickWholeSecond = (int)num;
		}
		else
		{
			lastTickWholeSecond = 0;
		}
	}

	private int FloorToWholeSecond(float seconds)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0046: Expected I4, but got F8
		if (0f < seconds)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num = Math.Floor(0.0);
			return (int)num;
		}
		return 0;
	}

	private void CheckAndEmitTick(float displayedSeconds)
	{
		if (!Application.isPlaying || currentState != WatchState.CountingDown)
		{
			return;
		}
		int num = FloorToWholeSecond(displayedSeconds);
		if (num != lastTickWholeSecond)
		{
			bool flag = onTick == null;
			lastTickWholeSecond = num;
			if (!flag)
			{
				onTick.Invoke();
			}
		}
	}

	private void EmitTickNow()
	{
		if (Application.isPlaying && onTick != null)
		{
			onTick.Invoke();
		}
	}

	private void ResetFiveSecondsTracking()
	{
		hasFiredFiveSecondsEventThisShot = false;
		previousCountingDownRemainingSeconds = -1f;
	}

	private void CheckAndEmitFiveSecondsRemaining(float displayedSeconds)
	{
		//IL_0076: Invalid comparison between I4 and F4
		if (!Application.isPlaying || currentState != WatchState.CountingDown)
		{
			return;
		}
		if (!hasFiredFiveSecondsEventThisShot)
		{
			if (0f > previousCountingDownRemainingSeconds)
			{
				previousCountingDownRemainingSeconds = displayedSeconds;
			}
			if (!(previousCountingDownRemainingSeconds < 5f) && 5f > displayedSeconds)
			{
				bool flag = onFiveSecondsRemaining == null;
				hasFiredFiveSecondsEventThisShot = true;
				if (!flag)
				{
					onFiveSecondsRemaining.Invoke();
				}
			}
		}
		previousCountingDownRemainingSeconds = displayedSeconds;
	}

	public unsafe void ResetStopwatch()
	{
		//IL_0355: Expected O, but got Ref
		//IL_0072: Invalid comparison between I4 and F4
		//IL_0084: Expected F4, but got I4
		//IL_012e: Invalid comparison between I4 and F4
		//IL_0140: Expected F4, but got I4
		//IL_00d9: Expected F4, but got I4
		//IL_003a: Invalid comparison between I4 and F4
		//IL_004c: Expected F4, but got I4
		//IL_025b: Expected F4, but got I4
		//IL_016d: Expected O, but got I4
		//IL_011e: Expected F4, but got I4
		//IL_02b7: Invalid comparison between I4 and F4
		//IL_02c9: Expected F4, but got I4
		//IL_0196: Expected F4, but got I4
		//IL_029e: Expected F4, but got I4
		//IL_0202: Invalid comparison between I4 and F8
		//IL_0214: Expected F8, but got I4
		countdownStartTime = -1.0;
		latchedTravelTime = 0f;
		if (HasLoadedShell())
		{
			if (!useGunPredictions)
			{
				float num = ComputeTravelTimeLocally();
				bool flag = !(0f < num);
				float num2 = 0f;
				if (!flag)
				{
					num2 = num;
				}
				lastPredictedTravelTime = num2;
			}
			else
			{
				bool flag2 = !(0f < lastPredictedTravelTime);
				float num3 = 0f;
				if (!flag2)
				{
					num3 = lastPredictedTravelTime;
				}
				lastPredictedTravelTime = num3;
			}
		}
		else if (zeroWhenNoShellLoaded)
		{
			lastPredictedTravelTime = 0f;
		}
		currentState = WatchState.Predicting;
		object obj = default(object);
		string text = ((Enum)(&obj)).ToString();
		state = text;
		float seconds;
		if (currentState != WatchState.Predicting)
		{
			seconds = 0f;
		}
		else if (!HasLoadedShell() && zeroWhenNoShellLoaded)
		{
			seconds = 0f;
		}
		else
		{
			bool flag3 = !(0f < lastPredictedTravelTime);
			seconds = 0f;
			if (!flag3)
			{
				seconds = lastPredictedTravelTime;
			}
		}
		ApplyHandFromSeconds(seconds);
		bool flag4 = currentState == WatchState.Predicting;
		float displayedSeconds;
		if (!flag4)
		{
			object obj2 = currentState - 1;
			if (flag4)
			{
				goto IL_022f;
			}
			bool flag5 = (nint)obj2 != 1;
			displayedSeconds = 0f;
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [rbx+0A8h]\"");
				if ((nint)obj2 > 1)
				{
					goto IL_022f;
				}
				double timeAsDouble = Time.timeAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rbx+0A8h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
				double num4 = (double)latchedTravelTime - timeAsDouble;
				bool flag6 = !(0.0 < num4);
				double num5 = 0.0;
				if (!flag6)
				{
					num5 = num4;
				}
				displayedSeconds = (float)num5;
			}
		}
		else
		{
			bool flag7 = currentState != WatchState.Predicting;
			displayedSeconds = 0f;
			if (!flag7)
			{
				if (!HasLoadedShell())
				{
					bool flag8 = zeroWhenNoShellLoaded;
					displayedSeconds = 0f;
					if (flag8)
					{
						goto IL_03ab;
					}
				}
				bool flag9 = !(0f < lastPredictedTravelTime);
				displayedSeconds = 0f;
				if (!flag9)
				{
					displayedSeconds = lastPredictedTravelTime;
				}
			}
		}
		goto IL_03ab;
		IL_022f:
		displayedSeconds = latchedTravelTime;
		goto IL_03ab;
		IL_03ab:
		ResetTickTracking(displayedSeconds);
		hasFiredFiveSecondsEventThisShot = false;
		previousCountingDownRemainingSeconds = -1f;
	}

	private void CacheImpactContextIfNeeded()
	{
		if (!(cachedImpactManager == null) || !autoFindImpactMarkerManager)
		{
			return;
		}
		ImpactMarkerManager impactMarkerManager = UnityEngine.Object.FindObjectOfType<ImpactMarkerManager>();
		cachedImpactManager = impactMarkerManager;
		Transform transform2;
		if (cachedImpactManager != null)
		{
			Transform transform = cachedImpactManager.transform;
			bool flag = (object)transform == null;
			transform2 = null;
			if (!flag)
			{
				bool flag2 = (object)transform.GetType() != typeof(RectTransform);
				transform2 = null;
				if (!flag2)
				{
					transform2 = transform;
				}
			}
		}
		else
		{
			transform2 = null;
		}
		cachedParentRect = (RectTransform)transform2;
	}

	public GunStopwatch()
	{
		//IL_008c: Expected I4, but got I8
		secondsPerFullRotation = 60f;
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		tickCurve = animationCurve;
		useGunPredictions = true;
		fallbackToRangeDivSpeedIfNoUIContext = true;
		UnityEvent unityEvent = new UnityEvent();
		unityEvent._002Ector();
		onCountdownStarted = unityEvent;
		UnityEvent unityEvent2 = new UnityEvent();
		onCountdownFinished = unityEvent2;
		UnityEvent unityEvent3 = new UnityEvent();
		onFiveSecondsRemaining = unityEvent3;
		UnityEvent unityEvent4 = new UnityEvent();
		onTick = unityEvent4;
		state = "Predicting";
		lastTickWholeSecond = -2147483648;
		countdownStartTime = -1.0;
		previousCountingDownRemainingSeconds = -1f;
		base._002Ector();
	}
}
