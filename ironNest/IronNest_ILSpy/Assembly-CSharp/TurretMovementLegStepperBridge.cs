using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class TurretMovementLegStepperBridge : MonoBehaviour
{
	private TurretController turret;

	private bool useTurretIsMoving = true;

	private bool usePositionDelta;

	private float positionDeltaThreshold = 0.0005f;

	private bool useRotationDelta;

	private float rotationDeltaThresholdDeg = 0.01f;

	private float stopDelaySeconds = 0.05f;

	private bool bindToTurretMoveEvents = true;

	public UnityEvent OnMovementStarted;

	public UnityEvent OnMovementStopped;

	private bool _003CIsMoving_003Ek__BackingField;

	private bool enableLegStepping = true;

	private List<Animator> legAnimators;

	private string stepTriggerName;

	private bool triggerFirstStepImmediatelyOnMoveStart;

	private bool resetLegSequenceOnMoveStart;

	private bool controlAnimatorSpeed;

	private float idleAnimatorSpeed;

	private float startAnimatorSpeed;

	private float fullAnimatorSpeed;

	private float startStepsPerMinute;

	private float fullStepsPerMinute;

	private float rampUpSeconds;

	private float rampDownSeconds;

	private float startStepDelaySeconds;

	private float minStepIntervalSeconds;

	private bool requireAtLeastOneLegAnimator;

	public UnityEvent OnStepTriggered;

	private Vector3 _lastLocalPos;

	private float _lastAngle;

	private bool _hasSamples;

	private float _noMoveTimer;

	private int _nextLegIndex;

	private float _stepTimer;

	private float _movePhase01;

	private bool _wasMovingLastFrame;

	private bool _cadenceDelaySatisfiedThisMove;

	private int _stepTriggerHash;

	public bool IsMoving
	{
		get
		{
			return _003CIsMoving_003Ek__BackingField;
		}
		private set
		{
			_003CIsMoving_003Ek__BackingField = value;
		}
	}

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4B7]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		useTurretIsMoving = true;
		useRotationDelta = false;
		positionDeltaThreshold = 0.0005f;
		rotationDeltaThresholdDeg = 0.01f;
		stopDelaySeconds = 0.05f;
		bindToTurretMoveEvents = true;
		enableLegStepping = true;
		stepTriggerName = "Step";
		triggerFirstStepImmediatelyOnMoveStart = true;
		controlAnimatorSpeed = true;
		idleAnimatorSpeed = 1f;
		startAnimatorSpeed = 0.85f;
		fullAnimatorSpeed = 1.25f;
		startStepsPerMinute = 60f;
		fullStepsPerMinute = 140f;
		rampUpSeconds = 0.75f;
		rampDownSeconds = 0.6f;
		minStepIntervalSeconds = 0.05f;
		requireAtLeastOneLegAnimator = true;
	}

	private void OnEnable()
	{
		int stepTriggerHash = Animator.StringToHash(stepTriggerName);
		_stepTriggerHash = stepTriggerHash;
		ResolveTurretReference();
		CacheInitialSamples();
		BindIfRequested();
		_nextLegIndex = 0;
		_movePhase01 = 0f;
		_wasMovingLastFrame = false;
		ApplyAnimatorSpeed(0f);
	}

	private void OnDisable()
	{
		UnbindIfNeeded();
		_hasSamples = false;
		_noMoveTimer = 0f;
		_003CIsMoving_003Ek__BackingField = false;
		_stepTimer = 0f;
		_wasMovingLastFrame = false;
		ApplyAnimatorSpeed(0f);
	}

	private void Update()
	{
		//IL_0066: Invalid comparison between I4 and F4
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Expected O, but got Unknown
		//IL_0304: Invalid comparison between I4 and F4
		//IL_0255: Invalid comparison between I4 and F4
		//IL_0704: Expected O, but got F4
		//IL_034d: Invalid comparison between I4 and F4
		//IL_02f4: Expected F4, but got I4
		//IL_03ce: Expected F4, but got I4
		//IL_03a3: Expected F4, but got I4
		//IL_029e: Invalid comparison between I4 and F4
		//IL_06a7: Invalid comparison between I4 and F4
		//IL_0738: Invalid comparison between I4 and F4
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Expected O, but got Unknown
		//IL_062f: Expected O, but got F4
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		//IL_0764: Invalid comparison between I4 and F4
		//IL_04cb: Expected F4, but got I4
		//IL_04eb: Invalid comparison between F4 and I4
		//IL_0806: Expected F4, but got I4
		//IL_0538: Invalid comparison between F4 and I4
		if (turret != null)
		{
			UnityEvent unityEvent;
			if (!ComputeMovingNow())
			{
				if (!_003CIsMoving_003Ek__BackingField)
				{
					_noMoveTimer = 0f;
					goto IL_0141;
				}
				if (0f < stopDelaySeconds)
				{
					float deltaTime = Time.deltaTime;
					if ((_noMoveTimer = deltaTime + _noMoveTimer) < stopDelaySeconds)
					{
						goto IL_0141;
					}
					unityEvent = OnMovementStopped;
					_003CIsMoving_003Ek__BackingField = false;
					_noMoveTimer = 0f;
				}
				else
				{
					unityEvent = OnMovementStopped;
					_003CIsMoving_003Ek__BackingField = false;
				}
			}
			else
			{
				_noMoveTimer = 0f;
				if (_003CIsMoving_003Ek__BackingField)
				{
					goto IL_0141;
				}
				unityEvent = OnMovementStarted;
				_003CIsMoving_003Ek__BackingField = true;
			}
			unityEvent?.Invoke();
			goto IL_0141;
		}
		ResolveTurretReference();
		CacheInitialSamples();
		BindIfRequested();
		return;
		IL_06ec:
		object obj = this + 192;
		goto IL_06fc;
		IL_039a:
		float num = 0f;
		goto IL_06ec;
		IL_06fc:
		obj = num;
		float phase = ((!controlAnimatorSpeed) ? 0f : _movePhase01);
		ApplyAnimatorSpeed(phase);
		if (0f < _movePhase01 && (!requireAtLeastOneLegAnimator || HasAnyValidLegAnimator()))
		{
			if (!_cadenceDelaySatisfiedThisMove)
			{
				float deltaTime2 = Time.deltaTime;
				float num2 = (_stepTimer = deltaTime2 + _stepTimer);
				if (startStepDelaySeconds > num2)
				{
					goto IL_05a1;
				}
				_cadenceDelaySatisfiedThisMove = true;
				_stepTimer = 0f;
			}
			float num3 = _movePhase01;
			if (!(0f > _movePhase01))
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
			float num4 = fullStepsPerMinute - startStepsPerMinute;
			float num5 = num4 * num3;
			float num6 = num5 + startStepsPerMinute;
			if (0.0001f < num6)
			{
				float num7 = 60f / num6;
				if (minStepIntervalSeconds > 0f && !(num7 > minStepIntervalSeconds))
				{
					num7 = minStepIntervalSeconds;
				}
				float deltaTime3 = Time.deltaTime;
				bool flag = (_stepTimer = deltaTime3 + _stepTimer) < num7;
				float num8 = 0f;
				if (!flag)
				{
					while (num8 < 4f)
					{
						float stepTimer = _stepTimer - num7;
						num8++;
						_stepTimer = stepTimer;
						TriggerNextLegStep();
						if (_stepTimer < num7)
						{
							break;
						}
					}
				}
			}
		}
		goto IL_05a1;
		IL_05a1:
		if (turret != null)
		{
			TurretController turretController = turret;
			if (turretController.turretBase != null)
			{
				TurretController turretController2 = turret;
				Vector3 localPosition = turretController2.turretBase.localPosition;
				_lastLocalPos = (Vector3)localPosition.x;
				_ = localPosition.z;
			}
			TurretController turretController3 = turret;
			_lastAngle = turretController3._003CCurrentAngle_003Ek__BackingField;
			_hasSamples = true;
		}
		_wasMovingLastFrame = _003CIsMoving_003Ek__BackingField;
		return;
		IL_0141:
		if (enableLegStepping)
		{
			if (_003CIsMoving_003Ek__BackingField && !_wasMovingLastFrame)
			{
				if (resetLegSequenceOnMoveStart)
				{
					_nextLegIndex = 0;
				}
				bool flag2 = 0f < startStepDelaySeconds;
				_stepTimer = 0f;
				bool cadenceDelaySatisfiedThisMove = !flag2;
				_cadenceDelaySatisfiedThisMove = cadenceDelaySatisfiedThisMove;
				if (triggerFirstStepImmediatelyOnMoveStart && (!requireAtLeastOneLegAnimator || HasAnyValidLegAnimator()))
				{
					TriggerNextLegStep();
				}
			}
			obj = this + 192;
			if (!_003CIsMoving_003Ek__BackingField)
			{
				if (0f < rampDownSeconds)
				{
					float deltaTime4 = Time.deltaTime;
					float num9 = deltaTime4 / rampDownSeconds;
					num = (float)obj - num9;
					if (0f > num)
					{
						goto IL_039a;
					}
					if (!(num > 1f))
					{
						goto IL_06ec;
					}
					obj = this + 192;
					num = 1f;
				}
				else
				{
					num = 0f;
				}
			}
			else if (0f < rampUpSeconds)
			{
				float deltaTime5 = Time.deltaTime;
				float num10 = deltaTime5 / rampUpSeconds;
				num = num10 + (float)obj;
				if (0f > num)
				{
					goto IL_039a;
				}
				if (!(num > 1f))
				{
					goto IL_06ec;
				}
				obj = this + 192;
				num = 1f;
			}
			else
			{
				num = 1f;
			}
			goto IL_06fc;
		}
		goto IL_05a1;
	}

	private void HandleMoveStartEdgeAndImmediateStepIfNeeded()
	{
		//IL_00d0: Invalid comparison between I4 and F4
		if (_003CIsMoving_003Ek__BackingField && !_wasMovingLastFrame)
		{
			if (resetLegSequenceOnMoveStart)
			{
				_nextLegIndex = 0;
			}
			_stepTimer = 0f;
			bool flag = 0f < startStepDelaySeconds;
			bool cadenceDelaySatisfiedThisMove = !flag;
			bool flag2 = !triggerFirstStepImmediatelyOnMoveStart;
			_cadenceDelaySatisfiedThisMove = cadenceDelaySatisfiedThisMove;
			if (!flag2 && (!requireAtLeastOneLegAnimator || HasAnyValidLegAnimator()))
			{
				TriggerNextLegStep();
			}
		}
	}

	private void ResolveTurretReference()
	{
		if (turret == null)
		{
			turret = TurretController.Instance;
		}
	}

	private void CacheInitialSamples()
	{
		//IL_0093: Expected O, but got F4
		if (turret != null)
		{
			TurretController turretController = turret;
			if (turretController.turretBase != null)
			{
				TurretController turretController2 = turret;
				Vector3 localPosition = turretController2.turretBase.localPosition;
				_lastLocalPos = (Vector3)localPosition.x;
				_ = localPosition.z;
			}
			TurretController turretController3 = turret;
			_lastAngle = turretController3._003CCurrentAngle_003Ek__BackingField;
			_hasSamples = true;
		}
	}

	private void UpdateSamples()
	{
		//IL_0093: Expected O, but got F4
		if (turret != null)
		{
			TurretController turretController = turret;
			if (turretController.turretBase != null)
			{
				TurretController turretController2 = turret;
				Vector3 localPosition = turretController2.turretBase.localPosition;
				_lastLocalPos = (Vector3)localPosition.x;
				_ = localPosition.z;
			}
			TurretController turretController3 = turret;
			_lastAngle = turretController3._003CCurrentAngle_003Ek__BackingField;
			_hasSamples = true;
		}
	}

	private bool ComputeMovingNow()
	{
		//IL_02e2: Expected I4, but got O
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02a9: Invalid comparison between O and F4
		//IL_0333: Expected I, but got O
		//IL_01f9: Expected F8, but got I4
		bool flag = turret == null;
		if (!flag)
		{
			if (_hasSamples == flag)
			{
				CacheInitialSamples();
			}
			bool flag2 = !useTurretIsMoving;
			bool result = false;
			if (!flag2)
			{
				if ((object)turret == null)
				{
					goto IL_02d4;
				}
				bool isMoving = turret.IsMoving;
				result = isMoving;
			}
			if (usePositionDelta)
			{
				TurretController turretController = turret;
				if ((object)turret == null)
				{
					goto IL_02d4;
				}
				if (turretController.turretBase != null)
				{
					TurretController turretController2 = turret;
					if ((object)turret == null || (object)turretController2.turretBase == null)
					{
						goto IL_02d4;
					}
					Vector3 localPosition = turretController2.turretBase.localPosition;
					nint num = (nint)typeof(Math);
					float num2 = (float)_lastLocalPos - localPosition.x;
					object obj2 = default(object);
					object obj3 = default(object);
					object obj = obj2 - obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TurretMovementLegStepperBridge)+A8]");
					float num3 = 0f - localPosition.z;
					object obj4 = obj * obj;
					float num4 = num2 * num2;
					float num5 = num3 * num3;
					float num6 = (float)obj4 + num4;
					float num7 = num6 + num5;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ rcx_v11 (Il2CppClass<System.Math>)+E4]");
					double num8;
					if ((nint)0 <= (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
						num8 = 0.0;
					}
					else
					{
						num8 = Math.Sqrt(num7);
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
					if (num8 > (double)positionDeltaThreshold)
					{
						result = true;
					}
				}
			}
			if (useRotationDelta)
			{
				TurretController turretController3 = turret;
				if ((object)turret == null)
				{
					goto IL_02d4;
				}
				float num9 = Mathf.DeltaAngle(_lastAngle, turretController3._003CCurrentAngle_003Ek__BackingField);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj5 = num9 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)rotationDeltaThresholdDeg))
				{
					result = true;
				}
			}
			return result;
		}
		return false;
		IL_02d4:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void BindIfRequested()
	{
		if (bindToTurretMoveEvents && turret != null)
		{
			UnbindIfNeeded();
			TurretController turretController = turret;
			if (turretController.OnTurretStartMove != null)
			{
				UnityAction call = HandleTurretStartMoveEvent;
				turretController.OnTurretStartMove.AddListener(call);
			}
			TurretController turretController2 = turret;
			if (turretController2.OnTurretFinishMove != null)
			{
				UnityAction call2 = HandleTurretFinishMoveEvent;
				turretController2.OnTurretFinishMove.AddListener(call2);
			}
		}
	}

	private void UnbindIfNeeded()
	{
		if (turret != null)
		{
			TurretController turretController = turret;
			if (turretController.OnTurretStartMove != null)
			{
				UnityAction call = HandleTurretStartMoveEvent;
				turretController.OnTurretStartMove.RemoveListener(call);
			}
			TurretController turretController2 = turret;
			if (turretController2.OnTurretFinishMove != null)
			{
				UnityAction call2 = HandleTurretFinishMoveEvent;
				turretController2.OnTurretFinishMove.RemoveListener(call2);
			}
		}
	}

	private void HandleTurretStartMoveEvent()
	{
		_noMoveTimer = 0f;
		if (!_003CIsMoving_003Ek__BackingField)
		{
			_003CIsMoving_003Ek__BackingField = true;
			if (OnMovementStarted != null)
			{
				OnMovementStarted.Invoke();
			}
		}
	}

	private void HandleTurretFinishMoveEvent()
	{
		_noMoveTimer = 0f;
	}

	private void UpdateMovePhase01(bool isMoving)
	{
		//IL_00c9: Invalid comparison between I4 and F4
		//IL_0019: Invalid comparison between I4 and F4
		//IL_0114: Invalid comparison between I4 and F4
		//IL_015d: Expected F4, but got I4
		//IL_0064: Invalid comparison between I4 and F4
		//IL_00ad: Expected F4, but got I4
		if (!isMoving)
		{
			if (0f < rampDownSeconds)
			{
				float deltaTime = Time.deltaTime;
				float num = deltaTime / rampDownSeconds;
				float num2 = _movePhase01 - num;
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						_movePhase01 = 1f;
						return;
					}
				}
				else
				{
					num2 = 0f;
				}
				_movePhase01 = num2;
			}
			else
			{
				_movePhase01 = 0f;
			}
		}
		else if (0f < rampUpSeconds)
		{
			float deltaTime2 = Time.deltaTime;
			float num3 = deltaTime2 / rampUpSeconds;
			float num4 = num3 + _movePhase01;
			if (!(0f > num4))
			{
				if (num4 > 1f)
				{
					_movePhase01 = 1f;
					return;
				}
			}
			else
			{
				num4 = 0f;
			}
			_movePhase01 = num4;
		}
		else
		{
			_movePhase01 = 1f;
		}
	}

	private void UpdateAnimatorSpeeds()
	{
		if (controlAnimatorSpeed)
		{
			ApplyAnimatorSpeed(_movePhase01);
		}
		else
		{
			ApplyAnimatorSpeed(0f);
		}
	}

	private void ApplyAnimatorSpeed(float phase01)
	{
		//IL_010c: Invalid comparison between I4 and F4
		//IL_001d: Invalid comparison between I4 and F4
		//IL_0072: Expected F4, but got I4
		//IL_0133: Expected O, but got I4
		//IL_013c: Expected O, but got I4
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		float num = default(float);
		float speed;
		if (!(0f < num))
		{
			speed = idleAnimatorSpeed;
		}
		else
		{
			float num2;
			if (!(0f > num))
			{
				bool flag = !(num > 1f);
				num2 = num;
				if (!flag)
				{
					num2 = 1f;
				}
			}
			else
			{
				num2 = 0f;
			}
			float num3 = fullAnimatorSpeed - startAnimatorSpeed;
			float num4 = num3 * num2;
			speed = num4 + startAnimatorSpeed;
		}
		List<Animator> list = legAnimators;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				((Animator)obj3).speed = speed;
			}
			list = legAnimators;
			obj++;
			obj2 = obj;
		}
	}

	private void UpdateStepping(bool isMoving)
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_0206: Invalid comparison between I4 and F4
		//IL_011a: Expected F4, but got I4
		//IL_013a: Invalid comparison between F4 and I4
		//IL_02a8: Expected F4, but got I4
		//IL_0187: Invalid comparison between F4 and I4
		if (!(0f < _movePhase01) || (requireAtLeastOneLegAnimator && !HasAnyValidLegAnimator()))
		{
			return;
		}
		if (!_cadenceDelaySatisfiedThisMove)
		{
			float deltaTime = Time.deltaTime;
			float num = (_stepTimer = deltaTime + _stepTimer);
			if (startStepDelaySeconds > num)
			{
				return;
			}
			_cadenceDelaySatisfiedThisMove = true;
			_stepTimer = 0f;
		}
		float num2 = _movePhase01;
		if (!(0f > _movePhase01))
		{
			if (num2 > 1f)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		float num3 = fullStepsPerMinute - startStepsPerMinute;
		float num4 = num3 * num2;
		float num5 = num4 + startStepsPerMinute;
		if (!(0.0001f < num5))
		{
			return;
		}
		float num6 = 60f / num5;
		if (minStepIntervalSeconds > 0f && !(num6 > minStepIntervalSeconds))
		{
			num6 = minStepIntervalSeconds;
		}
		float deltaTime2 = Time.deltaTime;
		bool flag = (_stepTimer = deltaTime2 + _stepTimer) < num6;
		float num7 = 0f;
		if (flag)
		{
			return;
		}
		while (num7 < 4f)
		{
			float stepTimer = _stepTimer - num6;
			num7++;
			_stepTimer = stepTimer;
			TriggerNextLegStep();
			if (_stepTimer < num6)
			{
				break;
			}
		}
	}

	private bool HasAnyValidLegAnimator()
	{
		//IL_00d1: Expected O, but got I4
		//IL_00da: Expected O, but got I4
		//IL_00ad: Expected I4, but got O
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		List<Animator> list = legAnimators;
		bool flag = legAnimators == null;
		object obj = 0;
		object obj2 = 0;
		if (!flag)
		{
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			while (true)
			{
				if ((nint)obj2 < list._size)
				{
					if (legAnimators == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj3 == null)
					{
						list = legAnimators;
						obj++;
						if (legAnimators == null)
						{
							break;
						}
						obj2 = obj;
						continue;
					}
					return true;
				}
				return false;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void TriggerNextLegStep()
	{
		//IL_006c: Expected O, but got I4
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected I4, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		List<Animator> list = legAnimators;
		if (list._size == 0)
		{
			return;
		}
		UnityEngine.Object obj = null;
		UnityEngine.Object obj2 = null;
		UnityEngine.Object obj3 = null;
		UnityEngine.Object obj5 = default(UnityEngine.Object);
		while ((nint)obj3 < list._size)
		{
			List<Animator> list2 = legAnimators;
			int num = _nextLegIndex % list2._size;
			object obj4 = num + 1;
			int nextLegIndex = obj4 % list2._size;
			_nextLegIndex = nextLegIndex;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag = obj5 == null;
			obj2 = obj5;
			if (!flag)
			{
				break;
			}
			list = legAnimators;
			obj = (UnityEngine.Object)(obj + 1);
			obj2 = obj5;
			obj3 = obj;
		}
		if (obj2 != null)
		{
			((Animator)obj2).ResetTrigger(_stepTriggerHash = Animator.StringToHash(stepTriggerName));
			((Animator)obj2).SetTrigger(_stepTriggerHash);
			if (OnStepTriggered != null)
			{
				OnStepTriggered.Invoke();
			}
		}
	}

	public TurretMovementLegStepperBridge()
	{
		List<Animator> list = new List<Animator>();
		legAnimators = list;
		stepTriggerName = "Step";
		triggerFirstStepImmediatelyOnMoveStart = true;
		controlAnimatorSpeed = true;
		idleAnimatorSpeed = 1f;
		startAnimatorSpeed = 0.85f;
		fullAnimatorSpeed = 1.25f;
		startStepsPerMinute = 60f;
		fullStepsPerMinute = 140f;
		rampUpSeconds = 0.75f;
		rampDownSeconds = 0.6f;
		minStepIntervalSeconds = 0.05f;
		requireAtLeastOneLegAnimator = true;
		base._002Ector();
	}
}
