using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
	public enum CommandSource
	{
		Unknown,
		Slider,
		Dial,
		API
	}

	private sealed class _003CFireShellDelayed_003Ed__108 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GunController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFireShellDelayed_003Ed__108(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_00b7: Expected I4, but got I8
			//IL_021e: Expected I4, but got O
			//IL_00a3: Expected I4, but got I8
			//IL_016b: Invalid comparison between F4 and I4
			//IL_0052: Expected I4, but got I8
			GunController gunController = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				bool flag2 = (byte)(_003C_003E1__state - 1) != 0;
				if (!flag)
				{
					if (flag2)
					{
						_003C_003E1__state = -1;
						if ((object)_003C_003E4__this == null)
						{
							goto IL_0210;
						}
						gunController.pendingReload = flag2;
						_003C_003E4__this.UpdateFireButtonActiveState();
					}
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0210;
				}
				gunController.hasFired = true;
				gunController.isReloading = true;
				if (gunController.gunAnimator != null)
				{
					if ((object)gunController.gunAnimator == null)
					{
						goto IL_0210;
					}
					gunController.gunAnimator.SetTrigger("fire");
				}
				if (gunController.fireDelay > 0f)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(gunController.fireDelay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.FireShell();
				WaitForSeconds waitForSeconds2 = new WaitForSeconds(1f);
				_003C_003E2__current = waitForSeconds2;
				_003C_003E1__state = 2;
				return true;
			}
			goto IL_0210;
			IL_0210:
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

	public string gunName;

	public int barrelIndex3D;

	public RectTransform firePoint;

	public ArtilleryReloadController artilleryReloadController;

	public Animator gunAnimator;

	public LookAtTarget fireButton;

	public LookAtTarget buttonToActivate;

	public float fireDelay;

	public float elevationChangeSpeed;

	public float elevationAccelerationTime;

	public float gunHorizontalDispersion;

	public float gunVerticalDispersion;

	private bool attenuateSliderBySystemPressure;

	private HighPressureSystemManager highPressureSystemManager;

	private bool autoFindSystemManagerById;

	private string systemIdForAutoFind;

	private AnimationCurve healthToSpeedScale;

	private bool logSliderAttenuation;

	private bool externalReloadLoweringLocked;

	private bool externalHoldAtReloadAfterComplete;

	private Action<float> m_OnPredictedImpactTimeChanged;

	private Action m_OnGunFired;

	private Action m_OnShellLaunched;

	private Action<int> m_OnPowderChargeChanged;

	private float _003CCurrentRange_003Ek__BackingField;

	private float _003CCurrentElevation_003Ek__BackingField;

	private float _003CDesiredElevationAngle_003Ek__BackingField;

	private float _003CMinElevationAngle_003Ek__BackingField;

	private float _003CCurrentElevationSpeed_003Ek__BackingField;

	private float _003CPredictedImpactTime_003Ek__BackingField;

	private float elevationChangeVelocity;

	private float minRange;

	private float maxRange;

	private bool isReloading;

	private bool pendingReload;

	private bool hasFired;

	private Turret3DMimic turret3DMimic;

	private TurretController parentTurret;

	private float reloadElevation;

	private bool isTargetingReloadElevation;

	private float internalDesiredElevation;

	private CommandSource lastCommandSource;

	private bool? cachedFireButtonActive;

	private bool? cachedButtonToActivateActive;

	public ShellBlueprint ChamberedShellBlueprint
	{
		get
		{
			if ((bool)this.artilleryReloadController)
			{
				ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
				if ((object)this.artilleryReloadController != null)
				{
					if (!artilleryReloadController.chamberedShell)
					{
						goto IL_00e0;
					}
					ArtilleryReloadController artilleryReloadController2 = this.artilleryReloadController;
					if ((object)this.artilleryReloadController != null && (object)artilleryReloadController2.chamberedShell != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
						ShellBlueprint result = default(ShellBlueprint);
						return result;
					}
				}
				return (ShellBlueprint)(object)new NullReferenceException();
			}
			goto IL_00e0;
			IL_00e0:
			return null;
		}
	}

	public float CurrentRange
	{
		get
		{
			return _003CCurrentRange_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentRange_003Ek__BackingField = value;
		}
	}

	public float CurrentElevation
	{
		get
		{
			return _003CCurrentElevation_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentElevation_003Ek__BackingField = value;
		}
	}

	public float DesiredElevationAngle
	{
		get
		{
			return _003CDesiredElevationAngle_003Ek__BackingField;
		}
		private set
		{
			_003CDesiredElevationAngle_003Ek__BackingField = value;
		}
	}

	public float MinElevationAngle
	{
		get
		{
			return _003CMinElevationAngle_003Ek__BackingField;
		}
		private set
		{
			_003CMinElevationAngle_003Ek__BackingField = value;
		}
	}

	public int PowderCharges
	{
		get
		{
			//IL_000a: Expected I4, but got O
			int num = (int)ChamberedShellBlueprint;
			if (num != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v1 (System.Int32)+28]");
				return 0;
			}
			return num;
		}
	}

	public float CurrentElevationSpeed
	{
		get
		{
			return _003CCurrentElevationSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentElevationSpeed_003Ek__BackingField = value;
		}
	}

	public bool IsReloading => isReloading;

	public bool CanFire
	{
		get
		{
			//IL_008d: Expected I4, but got O
			if (!isReloading && this.artilleryReloadController != null)
			{
				ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
				if ((object)this.artilleryReloadController != null)
				{
					return artilleryReloadController.chamberedShell != null;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return false;
		}
	}

	public float PredictedImpactTime
	{
		get
		{
			return _003CPredictedImpactTime_003Ek__BackingField;
		}
		private set
		{
			_003CPredictedImpactTime_003Ek__BackingField = value;
		}
	}

	public float ElevationErrorDeg => _003CDesiredElevationAngle_003Ek__BackingField - _003CCurrentElevation_003Ek__BackingField;

	public CommandSource LastCommandSource => lastCommandSource;

	public bool ExternalReloadLoweringLocked => externalReloadLoweringLocked;

	public bool ExternalHoldAtReloadAfterComplete => externalHoldAtReloadAfterComplete;

	public event Action<float> OnPredictedImpactTimeChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 152;
			Delegate obj2 = this.m_OnPredictedImpactTimeChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 152;
			Delegate obj2 = this.m_OnPredictedImpactTimeChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action OnGunFired
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 160;
			Delegate obj2 = this.m_OnGunFired;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 160;
			Delegate obj2 = this.m_OnGunFired;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action OnShellLaunched
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 168;
			Delegate obj2 = this.m_OnShellLaunched;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 168;
			Delegate obj2 = this.m_OnShellLaunched;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<int> OnPowderChargeChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 176;
			Delegate obj2 = this.m_OnPowderChargeChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 176;
			Delegate obj2 = this.m_OnPowderChargeChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (this.artilleryReloadController != null)
		{
			ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
			if (artilleryReloadController.gunController != this)
			{
				ArtilleryReloadController artilleryReloadController2 = this.artilleryReloadController;
				artilleryReloadController2.gunController = this;
			}
		}
	}

	private void OnEnable()
	{
		if (attenuateSliderBySystemPressure && this.highPressureSystemManager == null && autoFindSystemManagerById)
		{
			HighPressureSystemManager highPressureSystemManager = HighPressureSystemManager.FindBySystemId(systemIdForAutoFind);
			this.highPressureSystemManager = highPressureSystemManager;
		}
		UpdateFireButtonActiveState();
	}

	private void Start()
	{
		if (fireButton != null)
		{
			UnityAction action = RequestFire;
			fireButton.RegisterOnClickDown(action);
			UpdateFireButtonActiveState();
		}
	}

	private void Update()
	{
		if (pendingReload || (isReloading && !hasFired))
		{
			isReloading = true;
			hasFired = false;
			isTargetingReloadElevation = true;
			if (!externalReloadLoweringLocked)
			{
				internalDesiredElevation = reloadElevation;
			}
		}
		UpdateElevationPhysics();
		NotifyPredictedImpactTime();
	}

	public void Initialize(TurretController controller, float minElevation, float maxElevation)
	{
		parentTurret = controller;
		_003CMinElevationAngle_003Ek__BackingField = minElevation;
		reloadElevation = minElevation;
		_003CCurrentElevation_003Ek__BackingField = minElevation;
		internalDesiredElevation = minElevation;
		_003CCurrentElevationSpeed_003Ek__BackingField = 0f;
		elevationChangeVelocity = 0f;
		lastCommandSource = CommandSource.API;
		if (!isReloading)
		{
			internalDesiredElevation = _003CDesiredElevationAngle_003Ek__BackingField;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
		minRange = 0f;
		maxRange = 1000f;
		_003CCurrentRange_003Ek__BackingField = 0f;
		pendingReload = true;
		Turret3DMimic turret3DMimic = UnityEngine.Object.FindObjectOfType<Turret3DMimic>();
		this.turret3DMimic = turret3DMimic;
		if (this.artilleryReloadController != null)
		{
			ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
			artilleryReloadController.gunController = this;
		}
		UpdateFireButtonActiveState();
	}

	public void SetDesiredElevationFromDial(float elevationAngle)
	{
		_003CDesiredElevationAngle_003Ek__BackingField = elevationAngle;
		lastCommandSource = CommandSource.Dial;
		if (!isReloading)
		{
			internalDesiredElevation = elevationAngle;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
	}

	public void SetDesiredElevationFromSlider(float elevationAngle)
	{
		_003CDesiredElevationAngle_003Ek__BackingField = elevationAngle;
		lastCommandSource = CommandSource.Slider;
		if (!isReloading)
		{
			internalDesiredElevation = elevationAngle;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
	}

	public void SetDesiredElevation(float elevationAngle)
	{
		_003CDesiredElevationAngle_003Ek__BackingField = elevationAngle;
		lastCommandSource = CommandSource.API;
		if (!isReloading)
		{
			internalDesiredElevation = elevationAngle;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
	}

	public void SetExternalReloadLoweringLocked(bool locked)
	{
		externalReloadLoweringLocked = locked;
	}

	public void SetExternalHoldAtReloadAfterComplete(bool hold)
	{
		externalHoldAtReloadAfterComplete = hold;
	}

	public void ReleaseReloadHoldAndRestore()
	{
		internalDesiredElevation = _003CDesiredElevationAngle_003Ek__BackingField;
		externalHoldAtReloadAfterComplete = false;
		isTargetingReloadElevation = false;
		NotifyPredictedImpactTime();
		UpdateFireButtonActiveState();
	}

	private void SetDesiredElevationInternal(float elevationAngle, CommandSource source)
	{
		_003CDesiredElevationAngle_003Ek__BackingField = elevationAngle;
		lastCommandSource = source;
		if (!isReloading)
		{
			internalDesiredElevation = elevationAngle;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
	}

	public void ResetElevation()
	{
		_003CCurrentElevation_003Ek__BackingField = _003CMinElevationAngle_003Ek__BackingField;
		internalDesiredElevation = _003CMinElevationAngle_003Ek__BackingField;
		_003CCurrentElevationSpeed_003Ek__BackingField = 0f;
		elevationChangeVelocity = 0f;
		lastCommandSource = CommandSource.API;
		if (!isReloading)
		{
			internalDesiredElevation = _003CDesiredElevationAngle_003Ek__BackingField;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
	}

	public void SetDesiredRange(float targetRange)
	{
		//IL_00c1: Invalid comparison between I4 and F4
		//IL_00d0: Expected O, but got I4
		//IL_01bf: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_00f9: Expected O, but got I4
		//IL_0043: Invalid comparison between O and F4
		//IL_0127: Expected F4, but got I4
		//IL_0119: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018054C2C5h\"");
		object obj;
		float num3;
		if (minRange == maxRange)
		{
			obj = 0;
		}
		else
		{
			float num = targetRange - minRange;
			float num2 = maxRange - minRange;
			num3 = num / num2;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					num3 = 1f;
					obj = 0;
				}
				goto IL_01c4;
			}
		}
		num3 = 0f;
		goto IL_01c4;
		IL_01c4:
		TurretController turretController = parentTurret;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
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
		float num4 = turretController.maxBarrelElevation - _003CMinElevationAngle_003Ek__BackingField;
		lastCommandSource = CommandSource.API;
		float num5 = num4 * num3;
		float num6 = (_003CDesiredElevationAngle_003Ek__BackingField = num5 + _003CMinElevationAngle_003Ek__BackingField);
		if (!isReloading)
		{
			internalDesiredElevation = num6;
			isTargetingReloadElevation = false;
			NotifyPredictedImpactTime();
		}
	}

	public bool SetPowderCharge(int chargeLevel)
	{
		//IL_00c4: Expected I4, but got O
		ShellBlueprint chamberedShellBlueprint = ChamberedShellBlueprint;
		if (chamberedShellBlueprint != null)
		{
			ShellBlueprint chamberedShellBlueprint2 = ChamberedShellBlueprint;
			if ((object)chamberedShellBlueprint2 == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			if (chamberedShellBlueprint2.SetPowderCharge(chargeLevel))
			{
				UpdateRangeLimitsFromCharge();
				Action<int> onPowderChargeChanged = this.m_OnPowderChargeChanged;
				if (this.m_OnPowderChargeChanged != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v150 @ rcx_v9 (System.Action`1<System.Int32>)+18] (should have been resolved before IL gen)");
				}
				return true;
			}
		}
		return false;
	}

	public void UpdateRangeLimitsFromCharge()
	{
		ShellBlueprint chamberedShellBlueprint = ChamberedShellBlueprint;
		if (chamberedShellBlueprint != null)
		{
			ShellBlueprint chamberedShellBlueprint2 = ChamberedShellBlueprint;
			ShellBlueprint chamberedShellBlueprint3 = ChamberedShellBlueprint;
			chamberedShellBlueprint2.GetRangeForCharge(chamberedShellBlueprint3.currentPowderCharge, out var num, out var num2);
			minRange = num;
			maxRange = num2;
			NotifyPredictedImpactTime();
		}
	}

	public float MapElevationToRange(float elevation)
	{
		//IL_0177: Expected F4, but got I4
		//IL_0106: Invalid comparison between I4 and F4
		//IL_0115: Expected O, but got I4
		//IL_01c7: Expected F4, but got I4
		//IL_0069: Expected O, but got I4
		//IL_01d4: Invalid comparison between O and F4
		//IL_013e: Expected O, but got I4
		//IL_016c: Expected F4, but got I4
		//IL_0155: Expected O, but got I4
		ShellBlueprint chamberedShellBlueprint = ChamberedShellBlueprint;
		object obj;
		float num3;
		if (chamberedShellBlueprint != null)
		{
			TurretController turretController = parentTurret;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018054BCC3h\"");
			if (_003CMinElevationAngle_003Ek__BackingField == turretController.maxBarrelElevation)
			{
				obj = 0;
			}
			else
			{
				float num = elevation - _003CMinElevationAngle_003Ek__BackingField;
				float num2 = turretController.maxBarrelElevation - _003CMinElevationAngle_003Ek__BackingField;
				num3 = num / num2;
				bool flag = 0f > num3;
				obj = 0;
				if (!flag)
				{
					bool flag2 = !(num3 > 1f);
					obj = 0;
					if (!flag2)
					{
						obj = 0;
						num3 = 1f;
					}
					goto IL_01cc;
				}
			}
			num3 = 0f;
			goto IL_01cc;
		}
		return 0f;
		IL_01cc:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				float num4 = maxRange - minRange;
				float num5 = num4 * 1f;
				return num5 + minRange;
			}
		}
		else
		{
			num3 = 0f;
		}
		float num6 = maxRange - minRange;
		float num7 = num6 * num3;
		return num7 + minRange;
	}

	private void UpdateElevationPhysics()
	{
		//IL_0817: Expected O, but got I
		//IL_01f9: Expected O, but got I4
		//IL_0858: Expected O, but got I4
		//IL_005b: Expected O, but got I4
		//IL_02cc: Invalid comparison between F4 and I4
		//IL_0888: Invalid comparison between F4 and I4
		//IL_0342: Expected O, but got I4
		//IL_016e: Invalid comparison between I4 and F4
		//IL_00f8: Invalid comparison between I4 and F4
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Expected O, but got Unknown
		//IL_08ba: Invalid comparison between F4 and O
		//IL_02f7: Expected F4, but got I4
		//IL_030d: Expected O, but got I4
		//IL_0143: Expected F4, but got I4
		//IL_070a: Expected F4, but got I4
		//IL_0714: Expected F4, but got I4
		//IL_01b7: Expected O, but got I4
		//IL_0393: Invalid comparison between F4 and I
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Expected O, but got Unknown
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Expected O, but got Unknown
		//IL_03c2: Expected F4, but got I
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Expected O, but got Unknown
		//IL_04c7: Invalid comparison between O and F4
		//IL_04e5: Invalid comparison between F4 and I4
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Expected O, but got Unknown
		//IL_03f6: Invalid comparison between O and F4
		//IL_0a14: Invalid comparison between F4 and O
		//IL_0421: Invalid comparison between F4 and I4
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Expected O, but got Unknown
		//IL_0607: Invalid comparison between F4 and O
		//IL_0678: Expected F4, but got I4
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Expected O, but got Unknown
		//IL_09b0: Invalid comparison between F4 and O
		//IL_0641: Invalid comparison between F4 and I4
		//IL_0a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Expected O, but got Unknown
		//IL_0a7a: Invalid comparison between F4 and O
		//IL_046b: Invalid comparison between F4 and I4
		//IL_0585: Invalid comparison between F4 and I4
		bool flag = !(elevationAccelerationTime < 0.0001f);
		float num = elevationAccelerationTime;
		if (!flag)
		{
			num = 0.0001f;
		}
		float num2 = elevationChangeSpeed / num;
		bool flag3;
		bool flag4;
		if (elevationAccelerationTime == 0.0001f && attenuateSliderBySystemPressure)
		{
			object obj = lastCommandSource - 1;
			bool flag2 = obj == null;
			flag3 = flag2;
			flag4 = false;
		}
		else
		{
			flag3 = false;
			flag4 = false;
		}
		bool flag5 = !flag3;
		IntPtr intPtr = default(IntPtr);
		UnityEngine.Object obj2 = (UnityEngine.Object)(nint)intPtr;
		AnimationCurve animationCurve = (AnimationCurve)(object)this;
		if (flag5)
		{
			goto IL_01d7;
		}
		bool flag6 = this.highPressureSystemManager != null;
		bool flag7 = !flag6;
		float num3 = 1f;
		if (!flag7)
		{
			HighPressureSystemManager highPressureSystemManager = this.highPressureSystemManager;
			num3 = highPressureSystemManager.currentHealth01;
			if (!(0f > highPressureSystemManager.currentHealth01))
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
		AnimationCurve animationCurve2;
		float num5;
		object obj4;
		float num6;
		UnityEngine.Object obj3;
		if (healthToSpeedScale != null)
		{
			animationCurve = healthToSpeedScale;
			num = healthToSpeedScale.Evaluate(num3);
			if (!(0f > num))
			{
				bool flag8 = !(num > 1f);
				obj2 = null;
				num4 = num;
				num5 = num3;
				obj3 = null;
				obj4 = 0;
				num6 = num;
				animationCurve2 = healthToSpeedScale;
				if (!flag8)
				{
					goto IL_01d7;
				}
			}
			else
			{
				num4 = 0f;
				num5 = num3;
				obj3 = null;
				obj4 = 0;
				num6 = num;
				animationCurve2 = healthToSpeedScale;
			}
		}
		else
		{
			num4 = num3;
			float num7 = default(float);
			num5 = num7;
			obj3 = null;
			obj4 = 0;
			num6 = num;
			animationCurve2 = (AnimationCurve)(object)this.highPressureSystemManager;
		}
		goto IL_0849;
		IL_0b2f:
		_003CCurrentElevationSpeed_003Ek__BackingField = elevationChangeVelocity;
		float num8;
		_003CCurrentElevation_003Ek__BackingField = num8;
		float elevation = num8;
		goto IL_0bbc;
		IL_01d7:
		num4 = 1f;
		num5 = num3;
		obj3 = obj2;
		obj4 = 0;
		num6 = num;
		animationCurve2 = animationCurve;
		goto IL_0849;
		IL_0bbc:
		float num9 = MapElevationToRange(elevation);
		_003CCurrentRange_003Ek__BackingField = num9;
		return;
		IL_0849:
		object obj5 = logSliderAttenuation & flag3;
		bool flag9 = obj5 == null;
		object obj6 = animationCurve2;
		if (!flag9)
		{
			bool isPlaying = Application.isPlaying;
			bool flag10 = !isPlaying;
			obj6 = null;
			if (!flag10)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				string text = $"[{gunName}] Pressure atten: Health={arg:0.###}, SpeedScale={arg2:0.###}";
				Debug.Log(text, this);
				obj3 = this;
				obj6 = text;
			}
		}
		float num10 = internalDesiredElevation - _003CCurrentElevation_003Ek__BackingField;
		float num11 = num4 * elevationChangeSpeed;
		float num12 = num4 * num2;
		float num13 = ((num10 < 0f) ? (-1f) : 1f);
		float num14 = ((elevationChangeVelocity < 0f) ? (-1f) : 1f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj7 = num10 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BBC]");
			bool flag11 = !(num2 < 0f);
			float num15 = num2;
			if (!flag11)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BBC]");
				num15 = 0f;
			}
			float num16 = num15 + num15;
			float num17 = elevationChangeVelocity * elevationChangeVelocity;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj8 = num10 & 0;
			float num18 = num17 / num16;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj9 = num18 & 0;
			float num19 = (float)obj9 + 0.01f;
			bool flag12 = !flag3;
			float num20 = 0.01f;
			if (!flag12)
			{
				float num21 = num11 + 0.0001f;
				float num22 = elevationChangeVelocity;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj10 = num22 & 0;
				bool flag13 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num21);
				num20 = 0.01f;
				if (!flag13)
				{
					float num23 = ((elevationChangeVelocity < 0f) ? (-1f) : 1f);
					float num24 = num23 * num11;
					float deltaTime = Time.deltaTime;
					float num25 = num24 - elevationChangeVelocity;
					float num26 = deltaTime * num2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj11 = num25 & 0;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num26) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
					{
						float num27 = num24 - elevationChangeVelocity;
						float num28 = ((num27 < 0f) ? (-1f) : 1f);
						float num29 = num28 * num26;
						num24 = num29 + elevationChangeVelocity;
					}
					elevationChangeVelocity = num24;
					num20 = 0.01f;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018054C8E1h\"");
			bool flag14;
			if (num14 == num13)
			{
				flag14 = flag4;
			}
			else
			{
				float num30 = elevationChangeVelocity;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj12 = num30 & 0;
				bool flag15 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num20);
				float num31 = (float)obj12 - num20;
				bool flag16 = num31 == 0f;
				bool flag17 = !flag15;
				bool flag18 = !flag16;
				flag14 = flag18 & flag17;
			}
			bool flag19 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num19) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8);
			bool flag20 = true;
			if (!flag19)
			{
				flag20 = flag14;
			}
			float num38;
			if (!flag20)
			{
				float num32;
				if (flag3)
				{
					num32 = num13 * num11;
				}
				else
				{
					num32 = num13 * elevationChangeSpeed;
					num12 = num2;
				}
				float deltaTime2 = Time.deltaTime;
				float num33 = num32 - elevationChangeVelocity;
				float num34 = deltaTime2 * num12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj13 = num33 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num34) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13))
				{
					float num35 = num32 - elevationChangeVelocity;
					bool flag21 = !(num35 < 0f);
					float num36 = 1f;
					if (!flag21)
					{
						num36 = -1f;
					}
					float num37 = num36 * num34;
					num38 = num37 + elevationChangeVelocity;
				}
				else
				{
					num38 = num32;
				}
			}
			else
			{
				float deltaTime3 = Time.deltaTime;
				float num39 = 0f - elevationChangeVelocity;
				float num40 = deltaTime3 * num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj14 = num39 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num40) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14))
				{
					float num41 = 0f - elevationChangeVelocity;
					bool flag22 = !(num41 < 0f);
					float num42 = 1f;
					if (!flag22)
					{
						num42 = -1f;
					}
					float num43 = num42 * num40;
					num38 = num43 + elevationChangeVelocity;
				}
				else
				{
					num38 = 0f;
				}
			}
			elevationChangeVelocity = num38;
			float deltaTime4 = Time.deltaTime;
			TurretController turretController = parentTurret;
			float num44 = deltaTime4 * num38;
			num8 = (_003CCurrentElevation_003Ek__BackingField = num44 + _003CCurrentElevation_003Ek__BackingField);
			float num45 = _003CMinElevationAngle_003Ek__BackingField;
			if (!(_003CMinElevationAngle_003Ek__BackingField > num8))
			{
				num45 = turretController.maxBarrelElevation;
				if (!(num8 > turretController.maxBarrelElevation))
				{
					goto IL_0b2f;
				}
			}
			num8 = num45;
			goto IL_0b2f;
		}
		bool flag23 = !isTargetingReloadElevation;
		_003CCurrentElevation_003Ek__BackingField = internalDesiredElevation;
		elevationChangeVelocity = (flag4 ? 1 : 0);
		_003CCurrentElevationSpeed_003Ek__BackingField = (flag4 ? 1 : 0);
		if (!flag23)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj15 = default(object);
			if (obj15 != null && this.artilleryReloadController != null)
			{
				ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
				if (artilleryReloadController.currentStateIndex == 0)
				{
					artilleryReloadController.AdvanceState();
				}
				this.artilleryReloadController.TryLoadShell();
			}
		}
		elevation = _003CCurrentElevation_003Ek__BackingField;
		goto IL_0bbc;
	}

	private void HandleReloading()
	{
		if (pendingReload || (isReloading && !hasFired))
		{
			isReloading = true;
			hasFired = false;
			isTargetingReloadElevation = true;
			if (!externalReloadLoweringLocked)
			{
				internalDesiredElevation = reloadElevation;
			}
		}
	}

	public void OnReloadingComplete()
	{
		//IL_0037: Expected O, but got I4
		//IL_0065: Expected F4, but got I
		//IL_004e: Expected O, but got I4
		isTargetingReloadElevation = externalHoldAtReloadAfterComplete;
		isReloading = false;
		UpdateRangeLimitsFromCharge();
		bool flag = !externalHoldAtReloadAfterComplete;
		object obj = 192;
		if (!flag)
		{
			obj = 240;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v4+this @ rcx (GunController)]");
		internalDesiredElevation = 0f;
		NotifyPredictedImpactTime();
		UpdateFireButtonActiveState();
	}

	public void RequestFire()
	{
		if (isReloading || !(this.artilleryReloadController != null))
		{
			return;
		}
		ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
		if (artilleryReloadController.chamberedShell != null)
		{
			Action onGunFired = this.m_OnGunFired;
			if (this.m_OnGunFired != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v200.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
			_003CFireShellDelayed_003Ed__108 obj = new _003CFireShellDelayed_003Ed__108(0);
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
			UpdateFireButtonActiveState();
		}
	}

	private IEnumerator FireShellDelayed()
	{
		_003CFireShellDelayed_003Ed__108 obj = new _003CFireShellDelayed_003Ed__108(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private unsafe void FireShell()
	{
		//IL_045f: Expected O, but got Ref
		//IL_045f: Expected O, but got Ref
		//IL_0502: Expected O, but got I4
		//IL_02d9: Expected O, but got I4
		//IL_01fa: Expected O, but got F4
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Expected O, but got Unknown
		//IL_0234: Expected O, but got F4
		//IL_0234: Expected O, but got F4
		//IL_023e: Expected O, but got I4
		//IL_0247: Expected O, but got I4
		//IL_0291: Expected O, but got I4
		ShellBlueprint chamberedShellBlueprint = ChamberedShellBlueprint;
		ShellDefinition shellDefinition = chamberedShellBlueprint?.shellDefinition;
		if (chamberedShellBlueprint != null && shellDefinition != null && chamberedShellBlueprint.shellVisualPrefab != null && firePoint != null)
		{
			ImpactMarkerManager impactMarkerManager = UnityEngine.Object.FindFirstObjectByType<ImpactMarkerManager>();
			Transform transform = impactMarkerManager.transform;
			Vector3 localPosition = firePoint.localPosition;
			TurretController turretController = parentTurret;
			Vector3 localPosition2 = turretController.turretBase.localPosition;
			Vector3 euler = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			object obj = default(object);
			ShellDefinition shellDefinition2 = default(ShellDefinition);
			float num = ((Quaternion)(&obj) * (Vector3)(&shellDefinition2)).x * _003CCurrentRange_003Ek__BackingField;
			float num2 = num + localPosition2.x;
			float adjustedHorizontalDispersion = chamberedShellBlueprint.GetAdjustedHorizontalDispersion();
			float num3 = adjustedHorizontalDispersion + gunHorizontalDispersion;
			float adjustedVerticalDispersion = chamberedShellBlueprint.GetAdjustedVerticalDispersion();
			float num4 = adjustedVerticalDispersion + gunVerticalDispersion;
			int num5 = shellDefinition.projectilesPerShell;
			if (shellDefinition.projectilesPerShell < 1)
			{
				num5 = 1;
			}
			float minInclusive = num3 ^ -0f;
			float minInclusive2 = num4 ^ -0f;
			float minInclusive3 = shellDefinition.shellSpeedVariationPercent ^ -0f;
			float adjustedShellSpeed = chamberedShellBlueprint.GetAdjustedShellSpeed();
			object obj2 = 0;
			ShellDefinition shellDefinition3 = null;
			float x = localPosition.x;
			Transform parent = transform;
			Vector2 vector = default(Vector2);
			ShellDefinition shell = default(ShellDefinition);
			object obj3;
			bool flag;
			do
			{
				float num6 = UnityEngine.Random.Range(minInclusive, num3);
				float num7 = UnityEngine.Random.Range(minInclusive2, num4);
				float num8 = UnityEngine.Random.Range(minInclusive3, shellDefinition.shellSpeedVariationPercent);
				float num9 = num8 + 1f;
				float num10 = num9 * adjustedShellSpeed;
				if (Mathf.Epsilon < num10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
					float travelDuration = num8 / num10;
					GameObject gameObject = UnityEngine.Object.Instantiate(chamberedShellBlueprint.shellVisualPrefab, parent);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					Vector2 vector2;
					if (!((UnityEngine.Object)localPosition.x == null))
					{
						((ShellVisual)localPosition.x).Initialize((Vector2)localPosition.x, vector, travelDuration, shell);
						obj2 = 1;
						obj3 = 1;
						vector2 = vector;
						goto IL_0521;
					}
					string message = gunName + ": Firing failed. Shell visual prefab has no ShellVisual component.";
					Debug.LogError(message, gameObject);
					UnityEngine.Object.Destroy(gameObject);
					vector2 = (Vector2)0;
				}
				else
				{
					string message2 = gunName + ": Firing failed. Projectile speed must be greater than zero.";
					ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
					Debug.LogError(message2, artilleryReloadController.chamberedShell);
					Vector2 vector2 = (Vector2)0;
				}
				obj3 = obj2;
				goto IL_0521;
				IL_0521:
				shellDefinition3 = (ShellDefinition)(shellDefinition3 + 1);
				flag = (nint)shellDefinition3 < num5;
				parent = transform;
			}
			while (flag);
			if (obj3 != null)
			{
				Action onShellLaunched = this.m_OnShellLaunched;
				if (this.m_OnShellLaunched != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v1042.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				}
			}
			this.artilleryReloadController.EjectChamberedShell();
			if (turret3DMimic != null)
			{
				turret3DMimic.OnFireBarrel(barrelIndex3D);
			}
		}
		else
		{
			string message3 = gunName + ": Firing failed. Missing blueprint, visual prefab, or firepoint on chambered shell.";
			bool flag2 = this.artilleryReloadController != null;
			bool flag3 = !flag2;
			UnityEngine.Object context = null;
			if (!flag3)
			{
				ArtilleryReloadController artilleryReloadController2 = this.artilleryReloadController;
				context = artilleryReloadController2.chamberedShell;
			}
			Debug.LogError(message3, context);
			isReloading = false;
			hasFired = false;
			UpdateFireButtonActiveState();
		}
	}

	private void NotifyPredictedImpactTime()
	{
		//IL_00a8: Invalid comparison between I4 and F4
		float num = MapElevationToRange(_003CCurrentElevation_003Ek__BackingField);
		ShellBlueprint chamberedShellBlueprint = ChamberedShellBlueprint;
		float num2;
		if (chamberedShellBlueprint != null)
		{
			ShellBlueprint chamberedShellBlueprint2 = ChamberedShellBlueprint;
			float adjustedShellSpeed = chamberedShellBlueprint2.GetAdjustedShellSpeed();
			num2 = adjustedShellSpeed;
		}
		else
		{
			num2 = 1f;
		}
		if (!(0f < num2))
		{
			num2 = 1f;
		}
		Action<float> onPredictedImpactTimeChanged = this.m_OnPredictedImpactTimeChanged;
		float num3 = num / num2;
		_003CPredictedImpactTime_003Ek__BackingField = num3;
		if (this.m_OnPredictedImpactTimeChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v117 @ rcx_v7 (System.Action`1<System.Single>)+18] (should have been resolved before IL gen)");
		}
	}

	private unsafe void UpdateFireButtonActiveState()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0148: Expected O, but got I4
		//IL_0151: Expected O, but got I4
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0221: Expected O, but got I4
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		bool flag2;
		if (!isReloading && this.artilleryReloadController != null)
		{
			ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
			bool flag = artilleryReloadController.chamberedShell != null;
			flag2 = flag;
		}
		else
		{
			flag2 = false;
		}
		bool flag3 = fireButton != null;
		bool flag4 = !flag3;
		bool? flag6 = default(bool?);
		bool? flag5 = flag6;
		bool flag8 = default(bool);
		if (!flag4)
		{
			object obj = this + 256;
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			if (obj2 != null)
			{
				object obj3 = this + 256;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
				bool flag7 = flag8 == flag2;
				flag5 = flag6;
				if (flag7)
				{
					goto IL_015e;
				}
			}
			fireButton.SetActive(flag2);
			flag6 = (byte)(&flag8) != 0;
			cachedFireButtonActive = (bool?)(object)0;
			flag5 = (bool?)(object)0;
			flag8 = flag2;
		}
		goto IL_015e;
		IL_015e:
		if (!(buttonToActivate != null))
		{
			return;
		}
		object obj4 = this + 258;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			object obj6 = this + 258;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			if (flag2 == flag2)
			{
				return;
			}
		}
		buttonToActivate.SetActive(flag2);
		flag5 = (byte)(&flag8) != 0;
		cachedButtonToActivateActive = (bool?)(object)0;
	}

	public void OnShellLoaded()
	{
		ShellBlueprint chamberedShellBlueprint = ChamberedShellBlueprint;
		if (chamberedShellBlueprint != null)
		{
			UpdateRangeLimitsFromCharge();
		}
		UpdateFireButtonActiveState();
	}

	public void ForcePendingReload()
	{
		pendingReload = true;
	}

	public GunController()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AAFD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		gunName = "Gun";
		fireDelay = 0.1f;
		elevationChangeSpeed = 15f;
		elevationAccelerationTime = 0.5f;
		attenuateSliderBySystemPressure = true;
		autoFindSystemManagerById = true;
		systemIdForAutoFind = "Default";
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		healthToSpeedScale = animationCurve;
		base._002Ector();
	}
}
