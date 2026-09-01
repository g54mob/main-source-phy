using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
	public enum BackdriveSource
	{
		CurrentAngle,
		DesiredRotation
	}

	[Serializable]
	public class FloatValueProvider_CurrentAngle : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_CurrentAngle(TurretController c)
		{
			controller = c;
		}

		public float GetFloatValue()
		{
			TurretController turretController = controller;
			return turretController._003CCurrentAngle_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_DesiredRotation : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_DesiredRotation(TurretController c)
		{
			controller = c;
		}

		public float GetFloatValue()
		{
			TurretController turretController = controller;
			return turretController._003CDesiredRotation_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_CurrentRotationSpeed : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_CurrentRotationSpeed(TurretController c)
		{
			controller = c;
		}

		public float GetFloatValue()
		{
			TurretController turretController = controller;
			return turretController.observedRotationSpeed;
		}
	}

	[Serializable]
	public class FloatValueProvider_DesiredElevation : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_DesiredElevation(TurretController c)
		{
			controller = c;
		}

		public float GetFloatValue()
		{
			TurretController turretController = controller;
			return turretController._003CDesiredElevation_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_CurrentElevation : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_CurrentElevation(TurretController c)
		{
			controller = c;
		}

		public float GetFloatValue()
		{
			return controller.CurrentElevation;
		}
	}

	[Serializable]
	public class FloatValueProvider_PowderCharge : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_PowderCharge(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			//IL_0014: Expected F4, but got I4
			int powderCharges = gun.PowderCharges;
			return powderCharges;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunCurrentElevation : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunCurrentElevation(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			GunController gunController = gun;
			return gunController._003CCurrentElevation_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunElevationSpeed : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunElevationSpeed(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			GunController gunController = gun;
			return gunController._003CCurrentElevationSpeed_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunElevationErrorDeg : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunElevationErrorDeg(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			return gun.ElevationErrorDeg;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunCurrentRange : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunCurrentRange(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			GunController gunController = gun;
			return gunController._003CCurrentRange_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunPredictedImpactTime : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunPredictedImpactTime(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			GunController gunController = gun;
			return gunController._003CPredictedImpactTime_003Ek__BackingField;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunCanFire : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunCanFire(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			//IL_0034: Expected F4, but got I4
			if (gun.CanFire)
			{
				return 1f;
			}
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_GunIsReloading : IFloatValueProvider
	{
		private GunController gun;

		public FloatValueProvider_GunIsReloading(GunController g)
		{
			gun = g;
		}

		public float GetFloatValue()
		{
			//IL_003d: Expected F4, but got I4
			GunController gunController = gun;
			if (gunController.isReloading)
			{
				return 1f;
			}
			return 0f;
		}
	}

	[Serializable]
	public class FloatValueProvider_RotationErrorDeg : IFloatValueProvider
	{
		private TurretController controller;

		public FloatValueProvider_RotationErrorDeg(TurretController c)
		{
			controller = c;
		}

		public float GetFloatValue()
		{
			//IL_006d: Invalid comparison between I4 and F4
			//IL_00b8: Expected F4, but got I4
			TurretController turretController = controller;
			float num = turretController._003CDesiredRotation_003Ek__BackingField - turretController._003CCurrentAngle_003Ek__BackingField;
			float x = num / 360f;
			float num2 = MathF.Floor(x);
			float num3 = num2 * 360f;
			float num4 = num - num3;
			if (!(0f > num4))
			{
				if (num4 > 360f)
				{
					num4 = 360f;
				}
			}
			else
			{
				num4 = 0f;
			}
			if (num4 > 180f)
			{
				num4 -= 360f;
			}
			return num4;
		}
	}

	private sealed class _003CInternal_MoveTurret_003Ed__121 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Vector3 worldPos;

		public TurretController _003C_003E4__this;

		private Vector3 _003CdesiredLocation_003E5__2;

		private Vector3 _003CstartingLocation_003E5__3;

		private double _003CstartedAt_003E5__4;

		private double _003CendsAt_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CInternal_MoveTurret_003Ed__121(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_00e7: Expected I4, but got I8
			//IL_00fa: Expected O, but got Ref
			//IL_001d: Expected O, but got I4
			//IL_014b: Expected O, but got I
			//IL_015e: Expected O, but got F4
			//IL_00b0: Expected I4, but got I8
			//IL_076d: Expected I, but got O
			//IL_078a: Expected O, but got I
			//IL_07a7: Expected O, but got I
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_009c: Expected I4, but got I8
			//IL_0744: Expected O, but got I4
			//IL_074c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0751: Expected O, but got Unknown
			//IL_0227: Invalid comparison between F4 and I4
			//IL_05a8: Expected O, but got Ref
			//IL_05d1: Expected O, but got I
			//IL_007f: Expected I4, but got I8
			//IL_024f: Invalid comparison between F4 and I
			//IL_04f6: Expected F4, but got I4
			//IL_0526: Expected O, but got I
			//IL_04bf: Invalid comparison between I4 and F4
			//IL_04d1: Expected F4, but got I4
			//IL_055f: Expected O, but got Ref
			//IL_057e: Expected O, but got I
			//IL_07d6: Expected I4, but got O
			//IL_0650: Expected O, but got I
			//IL_02e2: Expected O, but got Ref
			//IL_02f0: Expected O, but got Ref
			//IL_0310: Expected native int or pointer, but got O
			//IL_0328: Expected O, but got Ref
			//IL_0336: Expected O, but got Ref
			//IL_0370: Expected native int or pointer, but got O
			//IL_03ac: Expected O, but got I
			//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c1: Expected O, but got Unknown
			//IL_041d: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			UnityEngine.Object context = _003C_003E4__this;
			int num = _003C_003E1__state ^ _003C_003E1__state;
			int num2 = _003C_003E1__state & num;
			bool flag = num2 < 0;
			bool flag2 = _003C_003E1__state < 0;
			bool flag3 = _003C_003E1__state == 0;
			bool result;
			if (!flag3)
			{
				object obj3 = _003C_003E1__state - 1;
				if (!flag3)
				{
					object obj4 = obj3 - 1;
					if (flag3)
					{
						_003C_003E1__state = -1;
						goto IL_0718;
					}
					bool flag4 = (nint)obj4 != 1;
					result = false;
					if (!flag4)
					{
						_003C_003E1__state = -1;
						result = false;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					_ = 0;
					Debug.Log("[DEBUG] Early exit");
					result = false;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				_ = worldPos;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretController+<Internal_MoveTurret>d__121)+28]");
				_ = 0;
				Vector2 vector2 = FireMission._003CInstance_003Ek__BackingField.ToLocalSpace(vector);
				Vector3 vector3 = default(Vector3);
				_003CdesiredLocation_003E5__2 = vector3;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+20]");
				Vector3 localPosition = ((Transform)0).localPosition;
				_003CstartingLocation_003E5__3 = (Vector3)localPosition.x;
				_ = localPosition.x;
				_ = localPosition.z;
				_ = _003CdesiredLocation_003E5__2;
				nint num3 = (nint)typeof(Math);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-19]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-29]");
				object obj5 = num4 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-15]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-25]");
				object obj6 = num5 - 0;
				float num6 = localPosition.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretController+<Internal_MoveTurret>d__121)+40]");
				float num7 = num6 - 0f;
				object obj7 = obj6 * obj6;
				object obj8 = obj5 * obj5;
				float num8 = num7 * num7;
				object obj9 = obj7 + obj8;
				float num9 = (float)obj9 + num8;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v596 @ rcx_v27 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				}
				else
				{
					double num10 = Math.Sqrt(num9);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm0\"");
				if (!(Mathf.Epsilon < 0f))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					goto IL_07e5;
				}
				float epsilon = Mathf.Epsilon;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+E0]");
				if (epsilon < 0f)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
					EventData_TurretMovement eventData_TurretMovement = new EventData_TurretMovement();
					if (eventData_TurretMovement != null)
					{
						eventData_TurretMovement.MovementType = EventData_TurretMovement.MovementTypes.Started;
						FireMission fireMission = default(FireMission);
						if ((object)fireMission != null)
						{
							fireMission.ProcessEvent(eventData_TurretMovement);
							Vector2 value = (Vector2)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
							Vector2? vector4 = (Vector2?)(object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
							_ = _003CstartingLocation_003E5__3;
							_ = 0;
							_ = 0;
							*(Vector2?*)(nint)vector4 = value;
							Vector2 value2 = (Vector2)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
							Vector2? vector5 = (Vector2?)(object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-29]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-21]");
							_ = 0;
							_ = _003CdesiredLocation_003E5__2;
							_ = 0;
							_ = 0;
							*(Vector2?*)(nint)vector5 = value2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-19]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-11]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+118]");
							nint num11 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+118]");
							object obj10 = num11 ^ 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+118]");
							object obj11 = 0 & obj10;
							flag = (nint)obj11 < 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+118]");
							flag2 = (nint)0 < (nint)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+118]");
							flag3 = (nint)0 == 0;
							if (!flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+118]");
								((UnityEvent)0).Invoke();
							}
							double timeAsDouble = Time.timeAsDouble;
							_003CstartedAt_003E5__4 = timeAsDouble;
							double num12 = 0.0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+E0]");
							double num13 = num12 / 0.0;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
							_003CendsAt_003E5__5 = num13;
							goto IL_0718;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				_ = 0;
				Debug.LogError("[TurretController] Movement speed must be greater than zero.", context);
				result = false;
			}
			goto IL_0713;
			IL_0713:
			return result;
			IL_0718:
			double timeAsDouble2 = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm0\"");
			bool flag5 = flag2 == flag;
			object obj12 = !flag5;
			object obj13 = obj12 | flag3;
			if (obj13 == null)
			{
				double timeAsDouble3 = Time.timeAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rbx+50h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rbx+50h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm0\"");
				float num14;
				if (0 <= 0)
				{
					bool flag6 = !(0f > 1f);
					num14 = 0f;
					if (!flag6)
					{
						num14 = 1f;
					}
				}
				else
				{
					num14 = 0f;
				}
				_ = _003CdesiredLocation_003E5__2;
				_ = _003CstartingLocation_003E5__3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretController+<Internal_MoveTurret>d__121)+40]");
				nint num15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretController+<Internal_MoveTurret>d__121)+4C]");
				object obj14 = num15 - 0;
				float num16 = (float)obj14 * num14;
				float num17 = num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretController+<Internal_MoveTurret>d__121)+4C]");
				float num18 = num17 + 0f;
				Vector3 localPosition2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+20]");
				((Transform)0).localPosition = localPosition2;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
			}
			else
			{
				Vector3 localPosition3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				_ = _003CdesiredLocation_003E5__2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretController+<Internal_MoveTurret>d__121)+40]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+20]");
				((Transform)0).localPosition = localPosition3;
				EventData_TurretMovement eventData_TurretMovement2 = new EventData_TurretMovement();
				eventData_TurretMovement2.MovementType = EventData_TurretMovement.MovementTypes.Finished;
				FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_TurretMovement2);
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+120]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdi_v1 (UnityEngine.Object)+120]");
					((UnityEvent)0).Invoke();
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
			}
			goto IL_07e5;
			IL_07e5:
			result = true;
			goto IL_0713;
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

	public static TurretController Instance;

	public RectTransform turretBase;

	public Turret3DMimic turret3DMimic;

	public List<GunController> guns;

	public float rotationSpeed;

	public float rotationAccelerationTime;

	public float startingRotation;

	public float minBarrelElevation;

	public float maxBarrelElevation;

	public float startingElevation;

	public KeyCode rotateLeftKey;

	public KeyCode rotateRightKey;

	public KeyCode increaseElevationKey;

	public KeyCode decreaseElevationKey;

	public float desiredRotationSpeed;

	public float desiredElevationChangeSpeed;

	public DialInteractable rotationDial;

	public float dialDegreesPerTurretDegree;

	public float turretRotationOffset;

	public DialInteractable elevationDial;

	public float dialDegreesPerElevationDegree;

	public float elevationOffset;

	public float compassBearingOffset;

	public bool invertCompassBearing;

	public DialInteractable rotationSpeedDial;

	public float maxManualRotationSpeed;

	private InputActionReference forceManualRotateLeftAction;

	private InputActionReference forceManualRotateRightAction;

	private bool debugForceActionsCancelOut;

	public DialInteractable elevationSpeedDial;

	public float maxManualElevationSpeed;

	public float desiredRotationAccelerationTime;

	public float rotationSpeedSmoothing;

	public bool backdriveRotationDial;

	public BackdriveSource backdriveSource;

	public bool backdriveUseDialSmoothing;

	public bool wrapBackdriveAngle;

	public float backdriveWrapDegrees;

	public bool dragOverridesSpeedDial;

	public bool dragOverridesElevationSpeedDial;

	public bool driveGunElevationsFromController;

	public float MovementSpeed;

	public Vector2? MovementStartLoc;

	public Vector2? MovementTargetLoc;

	private Coroutine CR_Movement;

	public UnityEvent OnRotationDragOverrideSpeedDial;

	public UnityEvent OnElevationDragOverrideSpeedDial;

	public UnityEvent OnTurretStartMove;

	public UnityEvent OnTurretFinishMove;

	private float _003CDesiredRotation_003Ek__BackingField;

	private float _003CCurrentAngle_003Ek__BackingField;

	private float _003CDesiredElevation_003Ek__BackingField;

	private float rotationVelocity;

	private int controlledGunIndex;

	private float desiredRotationVelocity;

	private float desiredRotationVelocityTarget;

	private bool isUsingSpeedDial;

	private float rotationDialBaseAngle;

	private bool rotationDialDragActive;

	private bool isUsingElevationSpeedDial;

	private float elevationDialBaseAngle;

	private bool elevationDialDragActive;

	private float lastAngleForSpeed;

	private float observedRotationSpeed;

	private bool firstSpeedSample;

	private bool debugForceLeftHeld;

	private bool debugForceRightHeld;

	public bool IsMoving
	{
		get
		{
			bool flag = (nint)CR_Movement < 0;
			bool flag2 = CR_Movement == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public float DesiredRotation
	{
		get
		{
			return _003CDesiredRotation_003Ek__BackingField;
		}
		private set
		{
			_003CDesiredRotation_003Ek__BackingField = value;
		}
	}

	public float CurrentAngle
	{
		get
		{
			return _003CCurrentAngle_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentAngle_003Ek__BackingField = value;
		}
	}

	public float DesiredElevation
	{
		get
		{
			return _003CDesiredElevation_003Ek__BackingField;
		}
		private set
		{
			_003CDesiredElevation_003Ek__BackingField = value;
		}
	}

	public float CurrentElevation => GetAverageCurrentElevation();

	public float CurrentRotationSpeed => observedRotationSpeed;

	public float CommandedRotationSpeed => rotationVelocity;

	public float DesiredRotationCompass
	{
		get
		{
			//IL_007c: Invalid comparison between I4 and F4
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected F4, but got Unknown
			bool flag = !invertCompassBearing;
			float num = _003CDesiredRotation_003Ek__BackingField + compassBearingOffset;
			if (!flag)
			{
				float num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				num = num2 ^ 0;
			}
			float num3 = MathF.FMod(num, 360f);
			if (0f > num3)
			{
				num3 += 360f;
			}
			return num3;
		}
	}

	public float CurrentAngleCompass
	{
		get
		{
			//IL_007c: Invalid comparison between I4 and F4
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected F4, but got Unknown
			bool flag = !invertCompassBearing;
			float num = _003CCurrentAngle_003Ek__BackingField + compassBearingOffset;
			if (!flag)
			{
				float num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				num = num2 ^ 0;
			}
			float num3 = MathF.FMod(num, 360f);
			if (0f > num3)
			{
				num3 += 360f;
			}
			return num3;
		}
	}

	private void Start()
	{
		Instance = this;
		float num = startingRotation;
		_003CCurrentAngle_003Ek__BackingField = startingRotation;
		_003CDesiredRotation_003Ek__BackingField = startingRotation;
		float num2 = startingElevation;
		if (!(minBarrelElevation > startingElevation))
		{
			if (num2 > maxBarrelElevation)
			{
				num2 = maxBarrelElevation;
			}
		}
		else
		{
			num2 = minBarrelElevation;
		}
		_003CDesiredElevation_003Ek__BackingField = num2;
		lastAngleForSpeed = num;
		firstSpeedSample = true;
		observedRotationSpeed = 0f;
		desiredRotationVelocity = 0f;
		ApplyRotationToTransforms();
		if (guns != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GunController>.Enumerator enumerator = default(List<GunController>.Enumerator);
			GunController gunController = default(GunController);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)gunController != null)
				{
					num = maxBarrelElevation;
					gunController.Initialize(this, minBarrelElevation, maxBarrelElevation);
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (!(turret3DMimic != null) || (object)turret3DMimic != null)
			{
				if (!(rotationDial != null))
				{
					goto IL_01b7;
				}
				Action value = OnBeginRotationDialDrag;
				if ((object)rotationDial != null)
				{
					rotationDial.OnBeginDialDrag += value;
					Action value2 = OnEndRotationDialDrag;
					if ((object)rotationDial != null)
					{
						rotationDial.OnEndDialDrag += value2;
						goto IL_01b7;
					}
				}
			}
		}
		goto IL_0274;
		IL_0274:
		throw new NullReferenceException();
		IL_026d:
		BindAndEnableDebugRotationActions();
		return;
		IL_01b7:
		if (!(elevationDial != null))
		{
			goto IL_026d;
		}
		Action value3 = OnBeginElevationDialDrag;
		if ((object)elevationDial != null)
		{
			elevationDial.OnBeginDialDrag += value3;
			Action value4 = OnEndElevationDialDrag;
			if ((object)elevationDial != null)
			{
				elevationDial.OnEndDialDrag += value4;
				goto IL_026d;
			}
		}
		goto IL_0274;
	}

	private void OnDestroy()
	{
		Instance = null;
		if (rotationDial != null)
		{
			Action value = OnBeginRotationDialDrag;
			rotationDial.OnBeginDialDrag -= value;
			Action value2 = OnEndRotationDialDrag;
			rotationDial.OnEndDialDrag -= value2;
		}
		if (elevationDial != null)
		{
			Action value3 = OnBeginElevationDialDrag;
			elevationDial.OnBeginDialDrag -= value3;
			Action value4 = OnEndElevationDialDrag;
			elevationDial.OnEndDialDrag -= value4;
		}
		UnbindAndDisableDebugRotationActions();
	}

	private void Update()
	{
		//IL_01d7: Invalid comparison between I4 and F4
		//IL_0222: Expected F4, but got I4
		//IL_026e: Invalid comparison between F4 and I
		//IL_04bd: Invalid comparison between F4 and I4
		//IL_0250: Expected F4, but got I4
		//IL_02b2: Invalid comparison between I4 and F4
		//IL_0295: Expected F4, but got I
		//IL_00d3: Expected O, but got I4
		//IL_02fd: Expected F4, but got I4
		//IL_051d: Invalid comparison between I4 and F4
		//IL_017a: Expected F4, but got I
		//IL_00ea: Expected O, but got I4
		//IL_0339: Expected F4, but got I4
		//IL_03eb: Invalid comparison between I4 and F4
		//IL_0165: Expected F4, but got I4
		HandleInput();
		UpdateRotationPhysics();
		if (driveGunElevationsFromController)
		{
			UpdateElevationForAllGuns();
		}
		ApplyRotationToTransforms();
		bool flag = !backdriveRotationDial;
		UnityEngine.Object obj = null;
		float num8;
		bool flag3;
		if (!flag)
		{
			bool flag2 = rotationDial != null;
			flag3 = false;
			obj = null;
			if (flag2)
			{
				DialInteractable dialInteractable = rotationDial;
				bool flag4 = dialInteractable.isDragging;
				flag3 = false;
				obj = null;
				if (!flag4)
				{
					bool flag5 = backdriveSource == BackdriveSource.CurrentAngle;
					object obj2 = 300;
					if (!flag5)
					{
						obj2 = 296;
					}
					if (wrapBackdriveAngle)
					{
						bool flag6 = !(1f < backdriveWrapDegrees);
						float num = 1f;
						if (!flag6)
						{
							num = backdriveWrapDegrees;
						}
						float num2 = num * 0.5f;
						float num3 = num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rax_v15+this @ rcx (TurretController)]");
						float num4 = num3 + 0f;
						float x = num4 / num;
						float num5 = MathF.Floor(x);
						float num6 = num5 * num;
						float num7 = num4 - num6;
						if (!(0f > num7))
						{
							if (num7 > num)
							{
								num8 = num - num2;
								goto IL_0413;
							}
						}
						else
						{
							num7 = 0f;
						}
						num8 = num7 - num2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rax_v15+this @ rcx (TurretController)]");
						num8 = 0f;
					}
					goto IL_0413;
				}
			}
		}
		goto IL_017f;
		IL_017f:
		float num9 = _003CCurrentAngle_003Ek__BackingField - lastAngleForSpeed;
		float x2 = num9 / 360f;
		float num10 = MathF.Floor(x2);
		float num11 = num10 * 360f;
		float num12 = num9 - num11;
		if (!(0f > num12))
		{
			if (num12 > 360f)
			{
				num12 = 360f;
			}
		}
		else
		{
			num12 = 0f;
		}
		if (num12 > 180f)
		{
			num12 -= 360f;
		}
		float num13;
		if (firstSpeedSample)
		{
			firstSpeedSample = false;
			num13 = 0f;
		}
		else
		{
			float num14 = Time.deltaTime;
			float num15 = num14;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
			if (num15 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
				num14 = 0f;
			}
			float num16 = num12 / num14;
			float num17 = rotationSpeedSmoothing;
			if (!(rotationSpeedSmoothing > 0f))
			{
				num13 = num16;
			}
			else
			{
				if (!(0f > rotationSpeedSmoothing))
				{
					if (num17 > 1f)
					{
						num17 = 1f;
					}
				}
				else
				{
					num17 = 0f;
				}
				float num18 = 1f - num17;
				float deltaTime = Time.deltaTime;
				float num19 = deltaTime * 60f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num20 = 1f - num18;
				if (!(0f > num20))
				{
					if (num20 > 1f)
					{
						num20 = 1f;
					}
				}
				else
				{
					num20 = 0f;
				}
				float num21 = num16 - observedRotationSpeed;
				float num22 = num21 * num20;
				num13 = num22 + observedRotationSpeed;
			}
		}
		observedRotationSpeed = num13;
		lastAngleForSpeed = _003CCurrentAngle_003Ek__BackingField;
		return;
		IL_0413:
		float num23 = num8 - turretRotationOffset;
		float angleDegrees = num23 * dialDegreesPerTurretDegree;
		rotationDial.SetAccumulatedValueUnlimited(angleDegrees, fireValueChangedEvent: false, backdriveUseDialSmoothing);
		flag3 = false;
		obj = null;
		goto IL_017f;
	}

	private void BindAndEnableDebugRotationActions()
	{
		if (forceManualRotateLeftAction != null)
		{
			InputAction action = forceManualRotateLeftAction.action;
			if (action != null)
			{
				InputAction action2 = forceManualRotateLeftAction.action;
				Action<InputAction.CallbackContext> value = OnForceManualRotateLeftPerformed;
				action2.performed += value;
				InputAction action3 = forceManualRotateLeftAction.action;
				Action<InputAction.CallbackContext> value2 = OnForceManualRotateLeftCanceled;
				action3.canceled += value2;
				InputAction action4 = forceManualRotateLeftAction.action;
				action4.Enable();
			}
		}
		if (forceManualRotateRightAction != null)
		{
			InputAction action5 = forceManualRotateRightAction.action;
			if (action5 != null)
			{
				InputAction action6 = forceManualRotateRightAction.action;
				Action<InputAction.CallbackContext> value3 = OnForceManualRotateRightPerformed;
				action6.performed += value3;
				InputAction action7 = forceManualRotateRightAction.action;
				Action<InputAction.CallbackContext> value4 = OnForceManualRotateRightCanceled;
				action7.canceled += value4;
				InputAction action8 = forceManualRotateRightAction.action;
				action8.Enable();
			}
		}
	}

	private void UnbindAndDisableDebugRotationActions()
	{
		if (forceManualRotateLeftAction != null)
		{
			InputAction action = forceManualRotateLeftAction.action;
			if (action != null)
			{
				InputAction action2 = forceManualRotateLeftAction.action;
				Action<InputAction.CallbackContext> value = OnForceManualRotateLeftPerformed;
				action2.performed -= value;
				InputAction action3 = forceManualRotateLeftAction.action;
				Action<InputAction.CallbackContext> value2 = OnForceManualRotateLeftCanceled;
				action3.canceled -= value2;
				InputAction action4 = forceManualRotateLeftAction.action;
				action4.Disable();
			}
		}
		if (forceManualRotateRightAction != null)
		{
			InputAction action5 = forceManualRotateRightAction.action;
			if (action5 != null)
			{
				InputAction action6 = forceManualRotateRightAction.action;
				Action<InputAction.CallbackContext> value3 = OnForceManualRotateRightPerformed;
				action6.performed -= value3;
				InputAction action7 = forceManualRotateRightAction.action;
				Action<InputAction.CallbackContext> value4 = OnForceManualRotateRightCanceled;
				action7.canceled -= value4;
				InputAction action8 = forceManualRotateRightAction.action;
				action8.Disable();
			}
		}
	}

	private void OnForceManualRotateLeftPerformed(InputAction.CallbackContext ctx)
	{
		debugForceLeftHeld = true;
		ApplyDebugForcedManualRotationSpeedToDial();
	}

	private void OnForceManualRotateLeftCanceled(InputAction.CallbackContext ctx)
	{
		//IL_00e3: Expected F4, but got I4
		//IL_0091: Expected F4, but got I4
		debugForceLeftHeld = false;
		bool flag = rotationSpeedDial == null;
		if (flag)
		{
			return;
		}
		if (debugForceLeftHeld == flag)
		{
			goto IL_00b8;
		}
		float num;
		if (debugForceRightHeld != flag)
		{
			if (debugForceActionsCancelOut == flag)
			{
				goto IL_00e8;
			}
			num = 0f;
		}
		else
		{
			if (!debugForceLeftHeld)
			{
				goto IL_00b8;
			}
			num = -1f;
		}
		goto IL_0156;
		IL_00e8:
		num = 1f;
		goto IL_0156;
		IL_00b8:
		if (debugForceRightHeld)
		{
			goto IL_00e8;
		}
		num = 0f;
		goto IL_0156;
		IL_0156:
		DialInteractable dialInteractable = rotationSpeedDial;
		if (dialInteractable.dialMode != DialInteractable.DialMode.Limited)
		{
			dialInteractable.SetAccumulatedValueUnlimited(num, fireValueChangedEvent: true);
		}
		else
		{
			dialInteractable.SetDialValue(num);
		}
	}

	private void OnForceManualRotateRightPerformed(InputAction.CallbackContext ctx)
	{
		debugForceRightHeld = true;
		ApplyDebugForcedManualRotationSpeedToDial();
	}

	private void OnForceManualRotateRightCanceled(InputAction.CallbackContext ctx)
	{
		//IL_00e3: Expected F4, but got I4
		//IL_0091: Expected F4, but got I4
		debugForceRightHeld = false;
		bool flag = rotationSpeedDial == null;
		if (flag)
		{
			return;
		}
		if (debugForceLeftHeld == flag)
		{
			goto IL_00b8;
		}
		float num;
		if (debugForceRightHeld != flag)
		{
			if (debugForceActionsCancelOut == flag)
			{
				goto IL_00e8;
			}
			num = 0f;
		}
		else
		{
			if (!debugForceLeftHeld)
			{
				goto IL_00b8;
			}
			num = -1f;
		}
		goto IL_0156;
		IL_00e8:
		num = 1f;
		goto IL_0156;
		IL_00b8:
		if (debugForceRightHeld)
		{
			goto IL_00e8;
		}
		num = 0f;
		goto IL_0156;
		IL_0156:
		DialInteractable dialInteractable = rotationSpeedDial;
		if (dialInteractable.dialMode != DialInteractable.DialMode.Limited)
		{
			dialInteractable.SetAccumulatedValueUnlimited(num, fireValueChangedEvent: true);
		}
		else
		{
			dialInteractable.SetDialValue(num);
		}
	}

	private void ApplyDebugForcedManualRotationSpeedToDial()
	{
		//IL_00d8: Expected F4, but got I4
		//IL_0086: Expected F4, but got I4
		bool flag = rotationSpeedDial == null;
		if (flag)
		{
			return;
		}
		if (debugForceLeftHeld == flag)
		{
			goto IL_00ad;
		}
		float num;
		if (debugForceRightHeld != flag)
		{
			if (debugForceActionsCancelOut == flag)
			{
				goto IL_00dd;
			}
			num = 0f;
		}
		else
		{
			if (!debugForceLeftHeld)
			{
				goto IL_00ad;
			}
			num = -1f;
		}
		goto IL_014b;
		IL_00dd:
		num = 1f;
		goto IL_014b;
		IL_00ad:
		if (debugForceRightHeld)
		{
			goto IL_00dd;
		}
		num = 0f;
		goto IL_014b;
		IL_014b:
		DialInteractable dialInteractable = rotationSpeedDial;
		if (dialInteractable.dialMode != DialInteractable.DialMode.Limited)
		{
			dialInteractable.SetAccumulatedValueUnlimited(num, fireValueChangedEvent: true);
		}
		else
		{
			dialInteractable.SetDialValue(num);
		}
	}

	private unsafe void ApplyRotationToTransforms()
	{
		//IL_003d: Expected O, but got Ref
		if (turretBase != null)
		{
			object obj = default(object);
			turretBase.localEulerAngles = (Vector3)(&obj);
		}
		if (turret3DMimic != null)
		{
			turret3DMimic.SyncTurret(_003CCurrentAngle_003Ek__BackingField);
		}
	}

	private void UpdateMeasuredRotationSpeed()
	{
		//IL_0058: Invalid comparison between I4 and F4
		//IL_00a3: Expected F4, but got I4
		//IL_00ef: Invalid comparison between F4 and I
		//IL_021a: Invalid comparison between F4 and I4
		//IL_00d1: Expected F4, but got I4
		//IL_0133: Invalid comparison between I4 and F4
		//IL_0116: Expected F4, but got I
		//IL_017e: Expected F4, but got I4
		//IL_027a: Invalid comparison between I4 and F4
		//IL_01ba: Expected F4, but got I4
		float num = _003CCurrentAngle_003Ek__BackingField - lastAngleForSpeed;
		float x = num / 360f;
		float num2 = MathF.Floor(x);
		float num3 = num2 * 360f;
		float num4 = num - num3;
		if (!(0f > num4))
		{
			if (num4 > 360f)
			{
				num4 = 360f;
			}
		}
		else
		{
			num4 = 0f;
		}
		if (num4 > 180f)
		{
			num4 -= 360f;
		}
		float num5;
		if (firstSpeedSample)
		{
			firstSpeedSample = false;
			num5 = 0f;
		}
		else
		{
			float num6 = Time.deltaTime;
			float num7 = num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
			if (num7 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206CEC]");
				num6 = 0f;
			}
			float num8 = num4 / num6;
			float num9 = rotationSpeedSmoothing;
			if (!(rotationSpeedSmoothing > 0f))
			{
				num5 = num8;
			}
			else
			{
				if (!(0f > rotationSpeedSmoothing))
				{
					if (num9 > 1f)
					{
						num9 = 1f;
					}
				}
				else
				{
					num9 = 0f;
				}
				float num10 = 1f - num9;
				float deltaTime = Time.deltaTime;
				float num11 = deltaTime * 60f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num12 = 1f - num10;
				if (!(0f > num12))
				{
					if (num12 > 1f)
					{
						num12 = 1f;
					}
				}
				else
				{
					num12 = 0f;
				}
				float num13 = num8 - observedRotationSpeed;
				float num14 = num13 * num12;
				num5 = num14 + observedRotationSpeed;
			}
		}
		observedRotationSpeed = num5;
		lastAngleForSpeed = _003CCurrentAngle_003Ek__BackingField;
	}

	private float GetAverageCurrentElevation()
	{
		//IL_003b: Expected O, but got I4
		//IL_0044: Expected O, but got I4
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		List<GunController> list = guns;
		if (list._size != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			object obj = 0;
			object obj2 = 0;
			List<GunController>.Enumerator enumerator = default(List<GunController>.Enumerator);
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj3 != null)
				{
					if ((object)obj3 == null)
					{
						throw new NullReferenceException();
					}
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ stack_-68 (UnityEngine.Object)+BC]");
					obj2 = obj4 + 0;
					obj++;
				}
			}
			enumerator.Dispose();
			if ((nint)obj > 0)
			{
				return (float)obj2 / (float)obj;
			}
		}
		return minBarrelElevation;
	}

	private void HandleInput()
	{
		//IL_00c5: Expected F4, but got I4
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_04b5: Invalid comparison between O and F4
		//IL_04d4: Invalid comparison between F4 and I4
		//IL_01a5: Expected F4, but got I4
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Expected O, but got Unknown
		//IL_0549: Invalid comparison between O and F4
		//IL_0568: Invalid comparison between F4 and I4
		//IL_01ce: Expected F4, but got I4
		//IL_0295: Invalid comparison between I4 and F4
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Expected O, but got Unknown
		//IL_0691: Invalid comparison between F4 and O
		//IL_0302: Invalid comparison between F4 and I4
		float num;
		if (rotationSpeedDial != null)
		{
			DialInteractable dialInteractable = rotationSpeedDial;
			num = dialInteractable.accumulatedValue;
			if (!(-1f > dialInteractable.accumulatedValue))
			{
				if (num > 1f)
				{
					num = 1f;
				}
			}
			else
			{
				num = -1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num2 = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num2 & 0;
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f);
		float num3 = (float)obj - 0.001f;
		bool flag2 = num3 == 0f;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		bool flag5 = flag4 & flag3;
		bool flag6 = !dragOverridesSpeedDial;
		isUsingSpeedDial = flag5;
		if (!flag6 && rotationDialDragActive)
		{
			isUsingSpeedDial = false;
		}
		float num4;
		if (elevationSpeedDial != null)
		{
			DialInteractable dialInteractable2 = elevationSpeedDial;
			num4 = dialInteractable2.accumulatedValue;
			if (!(-1f > dialInteractable2.accumulatedValue))
			{
				if (num4 > 1f)
				{
					num4 = 1f;
				}
			}
			else
			{
				num4 = -1f;
			}
		}
		else
		{
			num4 = 0f;
		}
		float num5 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num5 & 0;
		bool flag7 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f);
		float num6 = (float)obj2 - 0.001f;
		bool flag8 = num6 == 0f;
		bool flag9 = !flag7;
		bool flag10 = !flag8;
		bool flag11 = flag10 & flag9;
		isUsingElevationSpeedDial = flag11;
		if (!driveGunElevationsFromController)
		{
			isUsingElevationSpeedDial = false;
			elevationDialDragActive = false;
		}
		float num7;
		if (!isUsingSpeedDial)
		{
			num7 = 0f;
		}
		else
		{
			num7 = num * maxManualRotationSpeed;
			rotationDialDragActive = false;
		}
		desiredRotationVelocityTarget = num7;
		float num11;
		float num12;
		if (!isUsingSpeedDial && rotationDial != null && rotationDialDragActive)
		{
			DialInteractable dialInteractable3 = rotationDial;
			float num8 = dialDegreesPerTurretDegree;
			if (dialDegreesPerTurretDegree < 0.0001f)
			{
				num8 = 0.0001f;
			}
			desiredRotationVelocity = 0f;
			float num9 = dialInteractable3.accumulatedValue / num8;
			float num10 = num9 + turretRotationOffset;
			num11 = num10 + rotationDialBaseAngle;
			num12 = 0.0001f;
		}
		else
		{
			float num20;
			if (0f < desiredRotationAccelerationTime)
			{
				float num13 = desiredRotationAccelerationTime;
				if (desiredRotationAccelerationTime < 0.0001f)
				{
					num13 = 0.0001f;
				}
				float deltaTime = Time.deltaTime;
				float num14 = maxManualRotationSpeed / num13;
				float num15 = desiredRotationVelocityTarget - desiredRotationVelocity;
				float num16 = deltaTime * num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj3 = num15 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num16) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
				{
					float num17 = desiredRotationVelocityTarget - desiredRotationVelocity;
					bool flag12 = !(num17 < 0f);
					float num18 = 1f;
					if (!flag12)
					{
						num18 = -1f;
					}
					float num19 = num18 * num16;
					num20 = num19 + desiredRotationVelocity;
				}
				else
				{
					num20 = desiredRotationVelocityTarget;
				}
			}
			else
			{
				num20 = desiredRotationVelocityTarget;
			}
			desiredRotationVelocity = num20;
			float deltaTime2 = Time.deltaTime;
			float num21 = deltaTime2 * num20;
			num11 = num21 + _003CDesiredRotation_003Ek__BackingField;
			num12 = 0.0001f;
		}
		_003CDesiredRotation_003Ek__BackingField = num11;
		if (!driveGunElevationsFromController)
		{
			return;
		}
		if (!isUsingElevationSpeedDial)
		{
			if (elevationDial != null && elevationDialDragActive)
			{
				DialInteractable dialInteractable4 = elevationDial;
				float num22 = dialDegreesPerElevationDegree;
				if (dialDegreesPerElevationDegree < num12)
				{
					num22 = num12;
				}
				float num23 = dialInteractable4.accumulatedValue / num22;
				float num24 = num23 + elevationOffset;
				float num25 = num24 + elevationDialBaseAngle;
				_003CDesiredElevation_003Ek__BackingField = num25;
			}
		}
		else
		{
			float deltaTime3 = Time.deltaTime;
			float num26 = maxManualElevationSpeed * num4;
			elevationDialDragActive = false;
			float num27 = deltaTime3 * num26;
			float num28 = num27 + _003CDesiredElevation_003Ek__BackingField;
			_003CDesiredElevation_003Ek__BackingField = num28;
		}
		float num29 = minBarrelElevation;
		float num30 = _003CDesiredElevation_003Ek__BackingField;
		if (!(minBarrelElevation > _003CDesiredElevation_003Ek__BackingField))
		{
			num29 = maxBarrelElevation;
			if (!(_003CDesiredElevation_003Ek__BackingField > maxBarrelElevation))
			{
				goto IL_0790;
			}
		}
		num30 = num29;
		goto IL_0790;
		IL_0790:
		_003CDesiredElevation_003Ek__BackingField = num30;
	}

	private void UpdateDesiredRotationTargetDynamics()
	{
		//IL_0015: Invalid comparison between I4 and F4
		//IL_0048: Invalid comparison between F4 and I
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0153: Invalid comparison between F4 and O
		//IL_006f: Expected F4, but got I
		//IL_008e: Invalid comparison between F4 and I4
		float num = desiredRotationVelocityTarget;
		if (0f < desiredRotationAccelerationTime)
		{
			float num2 = desiredRotationAccelerationTime;
			float num3 = desiredRotationAccelerationTime;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			if (num3 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
				num2 = 0f;
			}
			float deltaTime = Time.deltaTime;
			float num4 = maxManualRotationSpeed / num2;
			float num5 = deltaTime * num4;
			float num6 = num - desiredRotationVelocity;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj = num6 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				float num7 = num - desiredRotationVelocity;
				float num8 = ((num7 < 0f) ? (-1f) : 1f);
				float num9 = num8 * num5;
				num = num9 + desiredRotationVelocity;
			}
		}
		desiredRotationVelocity = num;
		float deltaTime2 = Time.deltaTime;
		float num10 = deltaTime2 * num;
		float num11 = num10 + _003CDesiredRotation_003Ek__BackingField;
		_003CDesiredRotation_003Ek__BackingField = num11;
	}

	private void OnBeginRotationDialDrag()
	{
		//IL_0233: Expected O, but got I4
		//IL_000e: Expected O, but got I4
		//IL_009c: Expected F4, but got I4
		//IL_0067: Invalid comparison between F4 and I
		//IL_00c8: Expected O, but got I4
		//IL_008e: Expected F4, but got I
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_02b5: Invalid comparison between O and F4
		//IL_02d4: Invalid comparison between F4 and I4
		//IL_02fd: Expected O, but got I4
		bool flag = backdriveSource == BackdriveSource.CurrentAngle;
		object obj = 300;
		if (!flag)
		{
			obj = 296;
		}
		float num4;
		if (rotationDial != null)
		{
			DialInteractable dialInteractable = rotationDial;
			float num = dialDegreesPerTurretDegree;
			float num2 = dialDegreesPerTurretDegree;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			if (num2 < 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
				num = 0f;
			}
			float num3 = dialInteractable.accumulatedValue / num;
			num4 = num3 + turretRotationOffset;
		}
		else
		{
			num4 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v3+this @ rcx (TurretController)]");
		float num5 = 0f - num4;
		rotationDialBaseAngle = num5;
		object obj2;
		if (!(rotationSpeedDial != null))
		{
			obj2 = 0;
			goto IL_026b;
		}
		DialInteractable dialInteractable2 = rotationSpeedDial;
		float num6 = dialInteractable2.accumulatedValue;
		bool flag2 = -1f > dialInteractable2.accumulatedValue;
		float num7 = -1f;
		if (!flag2)
		{
			bool flag3 = !(dialInteractable2.accumulatedValue > 1f);
			num7 = 1f;
			if (flag3)
			{
				goto IL_0297;
			}
		}
		num6 = num7;
		goto IL_0297;
		IL_0297:
		float num8 = num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = num8 & 0;
		bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f);
		float num9 = (float)obj3 - 0.001f;
		bool flag5 = num9 == 0f;
		bool flag6 = !flag4;
		bool flag7 = !flag5;
		obj2 = flag7 & flag6;
		goto IL_026b;
		IL_026b:
		object obj4 = dragOverridesSpeedDial & obj2;
		if (obj4 != null)
		{
			if (rotationSpeedDial != null)
			{
				DialInteractable dialInteractable3 = rotationSpeedDial;
				if (dialInteractable3.dialMode != DialInteractable.DialMode.Limited)
				{
					dialInteractable3.SetAccumulatedValueUnlimited(0f, fireValueChangedEvent: true);
				}
				else
				{
					dialInteractable3.SetDialValue(0f);
				}
			}
			if (OnRotationDragOverrideSpeedDial != null)
			{
				OnRotationDragOverrideSpeedDial.Invoke();
			}
		}
		rotationDialDragActive = true;
	}

	private void OnEndRotationDialDrag()
	{
		rotationDialDragActive = false;
	}

	private void OnBeginElevationDialDrag()
	{
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0154: Invalid comparison between O and F4
		//IL_0173: Invalid comparison between F4 and I4
		//IL_01b7: Expected O, but got I4
		elevationDialBaseAngle = _003CDesiredElevation_003Ek__BackingField;
		bool flag;
		bool flag2;
		if (!(elevationSpeedDial != null))
		{
			flag = false;
			flag2 = false;
			goto IL_0117;
		}
		DialInteractable dialInteractable = elevationSpeedDial;
		float num = dialInteractable.accumulatedValue;
		bool flag3 = -1f > dialInteractable.accumulatedValue;
		float num2 = -1f;
		if (!flag3)
		{
			bool flag4 = !(dialInteractable.accumulatedValue > 1f);
			num2 = 1f;
			if (flag4)
			{
				goto IL_0136;
			}
		}
		num = num2;
		goto IL_0136;
		IL_0136:
		float num3 = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num3 & 0;
		bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f);
		float num4 = (float)obj - 0.001f;
		bool flag6 = num4 == 0f;
		bool flag7 = !flag5;
		bool flag8 = !flag6;
		flag2 = flag8 & flag7;
		flag = false;
		goto IL_0117;
		IL_0117:
		if (driveGunElevationsFromController)
		{
			flag = dragOverridesElevationSpeedDial;
		}
		object obj2 = flag2 & flag;
		if (obj2 != null && OnElevationDragOverrideSpeedDial != null)
		{
			OnElevationDragOverrideSpeedDial.Invoke();
		}
		elevationDialDragActive = true;
	}

	private void OnEndElevationDialDrag()
	{
		elevationDialDragActive = false;
	}

	private void UpdateRotationPhysics()
	{
		//IL_0071: Invalid comparison between I4 and F4
		//IL_00bc: Expected F4, but got I4
		//IL_077b: Invalid comparison between F4 and I4
		//IL_066e: Invalid comparison between F4 and I4
		//IL_069e: Invalid comparison between F4 and I
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Expected O, but got Unknown
		//IL_07cd: Invalid comparison between F4 and O
		//IL_011e: Expected F4, but got I
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_0298: Invalid comparison between O and F4
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01fb: Invalid comparison between F4 and O
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_03fc: Expected F4, but got I4
		//IL_0405: Expected F4, but got I4
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		//IL_0303: Invalid comparison between F4 and O
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Expected O, but got Unknown
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Expected O, but got Unknown
		//IL_0709: Invalid comparison between F4 and O
		//IL_0235: Invalid comparison between F4 and I4
		//IL_03bf: Expected F4, but got I4
		//IL_03ee: Expected F4, but got I4
		//IL_033c: Invalid comparison between F4 and I4
		//IL_0273: Expected F4, but got I4
		//IL_03a9: Expected F4, but got I4
		//IL_0490: Invalid comparison between I4 and F4
		//IL_037a: Expected F4, but got I4
		//IL_04db: Expected F4, but got I4
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Expected O, but got Unknown
		//IL_07fe: Invalid comparison between F4 and O
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Expected O, but got Unknown
		//IL_05a3: Invalid comparison between F4 and O
		//IL_051b: Invalid comparison between F4 and I4
		//IL_0544: Expected O, but got I4
		//IL_05dc: Invalid comparison between F4 and I4
		float num = _003CDesiredRotation_003Ek__BackingField - _003CCurrentAngle_003Ek__BackingField;
		float x = num / 360f;
		float num2 = MathF.Floor(x);
		float num3 = num2 * 360f;
		float num4 = num - num3;
		if (!(0f > num4))
		{
			if (num4 > 360f)
			{
				num4 = 360f;
			}
		}
		else
		{
			num4 = 0f;
		}
		if (num4 > 180f)
		{
			num4 -= 360f;
		}
		float num5 = ((num4 < 0f) ? (-1f) : 1f);
		float num6 = ((rotationVelocity < 0f) ? (-1f) : 1f);
		float num7 = rotationAccelerationTime;
		float num8 = rotationAccelerationTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
		if (num8 < 0f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			num7 = 0f;
		}
		float num9 = rotationSpeed / num7;
		float num10 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num10 & 0;
		float num11 = num9 + num9;
		float num21;
		float num22;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001805693B5h\"");
			if (num6 == num5)
			{
				float num12 = rotationVelocity * rotationVelocity;
				float num13 = num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj2 = num13 & 0;
				float num14 = num12 / num11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj3 = num14 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
				{
					goto IL_01b4;
				}
			}
			else
			{
				float num15 = rotationVelocity;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj4 = num15 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f))
				{
					goto IL_01b4;
				}
			}
			float deltaTime = Time.deltaTime;
			float num16 = rotationSpeed * num5;
			float num17 = deltaTime * num9;
			float num18 = num16 - rotationVelocity;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num18 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num17) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
			{
				float num19 = num16 - rotationVelocity;
				if (!(num19 < 0f))
				{
					float num20 = num17 * 1f;
					num21 = num20 + rotationVelocity;
					num22 = 0f;
				}
				else
				{
					float num23 = num17 * -1f;
					num21 = num23 + rotationVelocity;
					num22 = 0f;
				}
			}
			else
			{
				num21 = num16;
				num22 = 0f;
			}
			goto IL_06b5;
		}
		rotationVelocity = 0f;
		_003CCurrentAngle_003Ek__BackingField = _003CDesiredRotation_003Ek__BackingField;
		return;
		IL_06b5:
		rotationVelocity = num21;
		float num24 = num21;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj6 = num24 & 0;
		float deltaTime2 = Time.deltaTime;
		float num25 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj7 = num25 & 0;
		float num26 = deltaTime2 * (float)obj6;
		float num28;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num26) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
		{
			float deltaTime3 = Time.deltaTime;
			float num27 = deltaTime3 * rotationVelocity;
			num28 = num27 + _003CCurrentAngle_003Ek__BackingField;
			float num29 = num28 - _003CCurrentAngle_003Ek__BackingField;
			float x2 = num29 / 360f;
			float num30 = MathF.Floor(x2);
			float num31 = num30 * 360f;
			float num32 = num29 - num31;
			if (!(0f > num32))
			{
				if (num32 > 360f)
				{
					num32 = 360f;
				}
			}
			else
			{
				num32 = 0f;
			}
			if (num32 > 180f)
			{
				num32 -= 360f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj8 = num26 ^ 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num32) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8))
			{
				bool flag = num26 < num32;
				float num33 = num26 - num32;
				bool flag2 = num33 == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				object obj9 = flag4 & flag3;
				if (obj9 != null)
				{
					goto IL_073d;
				}
			}
			num28 = num32 + _003CCurrentAngle_003Ek__BackingField;
			float num34 = num28 - _003CCurrentAngle_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj10 = num34 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num26) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
			{
				float num35 = num28 - _003CCurrentAngle_003Ek__BackingField;
				float num36 = ((num35 < 0f) ? (-1f) : 1f);
				float num37 = num36 * num26;
				num28 = num37 + _003CCurrentAngle_003Ek__BackingField;
			}
			goto IL_073d;
		}
		rotationVelocity = num22;
		_003CCurrentAngle_003Ek__BackingField = _003CDesiredRotation_003Ek__BackingField;
		return;
		IL_01b4:
		float deltaTime4 = Time.deltaTime;
		float num38 = 0f - rotationVelocity;
		float num39 = deltaTime4 * num9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj11 = num38 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num39) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
		{
			float num40 = 0f - rotationVelocity;
			if (!(num40 < 0f))
			{
				float num41 = 1f * num39;
				num21 = num41 + rotationVelocity;
				num22 = 0f;
			}
			else
			{
				float num42 = -1f * num39;
				num21 = num42 + rotationVelocity;
				num22 = 0f;
			}
		}
		else
		{
			num21 = 0f;
			num22 = 0f;
		}
		goto IL_06b5;
		IL_073d:
		_003CCurrentAngle_003Ek__BackingField = num28;
	}

	private void UpdateElevationForAllGuns()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GunController>.Enumerator enumerator = default(List<GunController>.Enumerator);
		GunController gunController = default(GunController);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)gunController == null)
				{
					break;
				}
				gunController.SetDesiredElevation(_003CDesiredElevation_003Ek__BackingField);
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private float MapDialToTurretRotation(float dialDegrees)
	{
		//IL_001c: Invalid comparison between F4 and I
		//IL_0043: Expected F4, but got I
		float num = dialDegreesPerTurretDegree;
		float num2 = dialDegreesPerTurretDegree;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
		if (num2 < 0f)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			num = 0f;
		}
		float num3 = dialDegrees / num;
		return num3 + turretRotationOffset;
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

	private float MapTurretToDialDegrees(float turretDegrees)
	{
		float num = turretDegrees - turretRotationOffset;
		return num * dialDegreesPerTurretDegree;
	}

	private float NormalizeCompassBearing(float angle)
	{
		//IL_007a: Invalid comparison between I4 and F4
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected F4, but got Unknown
		bool flag = !invertCompassBearing;
		float num = angle + compassBearingOffset;
		if (!flag)
		{
			float num2 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			num = num2 ^ 0;
		}
		float num3 = MathF.FMod(num, 360f);
		if (0f > num3)
		{
			num3 += 360f;
		}
		return num3;
	}

	private void BackdriveRotationDial()
	{
		//IL_0076: Expected O, but got I4
		//IL_0116: Expected F4, but got I
		//IL_008d: Expected O, but got I4
		//IL_01c4: Invalid comparison between I4 and F4
		//IL_0101: Expected F4, but got I4
		if (!backdriveRotationDial || !(rotationDial != null))
		{
			return;
		}
		DialInteractable dialInteractable = rotationDial;
		if (dialInteractable.isDragging)
		{
			return;
		}
		bool flag = backdriveSource == BackdriveSource.CurrentAngle;
		object obj = 300;
		if (!flag)
		{
			obj = 296;
		}
		float num8;
		if (wrapBackdriveAngle)
		{
			bool flag2 = !(1f < backdriveWrapDegrees);
			float num = 1f;
			if (!flag2)
			{
				num = backdriveWrapDegrees;
			}
			float num2 = num * 0.5f;
			float num3 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v8+this @ rcx (TurretController)]");
			float num4 = num3 + 0f;
			float x = num4 / num;
			float num5 = MathF.Floor(x);
			float num6 = num5 * num;
			float num7 = num4 - num6;
			if (!(0f > num7))
			{
				if (num7 > num)
				{
					num7 = num;
				}
			}
			else
			{
				num7 = 0f;
			}
			num8 = num7 - num2;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v8+this @ rcx (TurretController)]");
			num8 = 0f;
		}
		float num9 = num8 - turretRotationOffset;
		float angleDegrees = num9 * dialDegreesPerTurretDegree;
		rotationDial.SetAccumulatedValueUnlimited(angleDegrees, fireValueChangedEvent: false, backdriveUseDialSmoothing);
	}

	private static float WrapAngle(float angleDeg, float modulo)
	{
		//IL_0062: Invalid comparison between I4 and F4
		//IL_00ab: Expected F4, but got I4
		float num = modulo * 0.5f;
		float num2 = num + angleDeg;
		float x = num2 / modulo;
		float num3 = MathF.Floor(x);
		float num4 = num3 * modulo;
		float num5 = num2 - num4;
		if (!(0f > num5))
		{
			if (num5 > modulo)
			{
				num5 = modulo;
			}
		}
		else
		{
			num5 = 0f;
		}
		return num5 - num;
	}

	public void FireControlledGun()
	{
		List<GunController> list = guns;
		if (list._size > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				GunController gunController = default(GunController);
				gunController.RequestFire();
			}
		}
	}

	public void FireGunByIndex(int gunIndex)
	{
		if (gunIndex < 0)
		{
			return;
		}
		List<GunController> list = guns;
		if (gunIndex < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				GunController gunController = default(GunController);
				gunController.RequestFire();
			}
		}
	}

	public void SetPowderChargeForAllGuns(int chargeLevel)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GunController>.Enumerator enumerator = default(List<GunController>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					ShellBlueprint chamberedShellBlueprint = ((GunController)obj).ChamberedShellBlueprint;
					if (chamberedShellBlueprint != null)
					{
						bool flag = ((GunController)obj).SetPowderCharge(chargeLevel);
					}
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public unsafe void SetTurretLocation(Vector3 worldPos)
	{
		//IL_0014: Expected O, but got Ref
		//IL_002c: Expected O, but got Ref
		float num = default(float);
		Vector2 vector = FireMission._003CInstance_003Ek__BackingField.ToLocalSpace((Vector3)(&num));
		turretBase.localPosition = (Vector3)(&num);
	}

	public void MoveTurret(Vector3 worldPos)
	{
		//IL_0043: Expected O, but got F4
		if (CR_Movement == null)
		{
			_003CInternal_MoveTurret_003Ed__121 obj = new _003CInternal_MoveTurret_003Ed__121(0);
			obj._003C_003E4__this = this;
			obj.worldPos = (Vector3)worldPos.x;
			_ = worldPos.z;
			Coroutine cR_Movement = StartCoroutine(obj);
			CR_Movement = cR_Movement;
		}
	}

	public IEnumerator Internal_MoveTurret(Vector3 worldPos)
	{
		//IL_0021: Expected O, but got F4
		_003CInternal_MoveTurret_003Ed__121 obj = new _003CInternal_MoveTurret_003Ed__121(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.worldPos = (Vector3)worldPos.x;
			_ = worldPos.z;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	public void ResetTurretRotation()
	{
		float num = minBarrelElevation;
		float num2 = startingElevation;
		_003CCurrentAngle_003Ek__BackingField = startingRotation;
		_003CDesiredRotation_003Ek__BackingField = startingRotation;
		if (!(minBarrelElevation > startingElevation))
		{
			num = maxBarrelElevation;
			if (!(startingElevation > maxBarrelElevation))
			{
				goto IL_0086;
			}
		}
		num2 = num;
		goto IL_0086;
		IL_0086:
		lastAngleForSpeed = startingRotation;
		_003CDesiredElevation_003Ek__BackingField = num2;
		observedRotationSpeed = 0f;
		desiredRotationVelocity = 0f;
		firstSpeedSample = true;
		ApplyRotationToTransforms();
	}

	public TurretController()
	{
		List<GunController> list = new List<GunController>();
		guns = list;
		rotationSpeed = 45f;
		rotationAccelerationTime = 0.5f;
		maxBarrelElevation = 45f;
		rotateLeftKey = KeyCode.A;
		rotateRightKey = KeyCode.D;
		increaseElevationKey = KeyCode.W;
		decreaseElevationKey = KeyCode.S;
		desiredRotationSpeed = 180f;
		desiredElevationChangeSpeed = 15f;
		dialDegreesPerTurretDegree = 4f;
		dialDegreesPerElevationDegree = 4f;
		maxManualRotationSpeed = 180f;
		debugForceActionsCancelOut = true;
		maxManualElevationSpeed = 15f;
		backdriveRotationDial = true;
		backdriveUseDialSmoothing = true;
		backdriveWrapDegrees = 360f;
		MovementSpeed = 5f;
		firstSpeedSample = true;
		base._002Ector();
	}
}
