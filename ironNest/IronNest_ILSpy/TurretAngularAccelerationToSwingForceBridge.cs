using System;
using Cpp2ILInjected;
using UnityEngine;

public sealed class TurretAngularAccelerationToSwingForceBridge : MonoBehaviour
{
	private SwingController swingController;

	private TurretController turretController;

	private bool useAccelerationSignForXDirection = true;

	private bool invertOutput;

	private float accelerationSmoothing = 0.12f;

	private float maxAbsAngularAcceleration = 1500f;

	private float accelerationDeadzone;

	private AnimationCurve absAngularAccelerationToAbsWorldXForce = AnimationCurve.Linear(0f, 0f, 1000f, 1f);

	private float outputMultiplier = 1f;

	private bool logDebug;

	private float debugLogInterval = 0.5f;

	private float _lastSpeedDegPerSec;

	private bool _hasLastSpeed;

	private float _smoothedAccel;

	private float _nextLogTime;

	private void Reset()
	{
		if (this.swingController == null)
		{
			SwingController swingController = UnityEngine.Object.FindFirstObjectByType<SwingController>();
			this.swingController = swingController;
		}
		if (this.turretController == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			TurretController turretController = default(TurretController);
			this.turretController = turretController;
		}
	}

	private void Update()
	{
		//IL_007f: Invalid comparison between I4 and F4
		//IL_00c2: Expected O, but got I4
		//IL_00ef: Expected F4, but got I4
		//IL_0614: Invalid comparison between F4 and I4
		//IL_0636: Invalid comparison between F4 and I4
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Expected F4, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected F4, but got Unknown
		//IL_01ac: Invalid comparison between I4 and F4
		//IL_025e: Expected F4, but got I4
		//IL_0267: Expected O, but got I4
		//IL_0203: Expected F4, but got I4
		//IL_0698: Invalid comparison between I4 and F4
		//IL_0299: Expected O, but got I4
		//IL_023f: Expected F4, but got I4
		//IL_02d7: Invalid comparison between F4 and I4
		//IL_03a9: Expected I, but got O
		//IL_03b9: Expected O, but got I
		//IL_03db: Expected O, but got I4
		//IL_0437: Expected I, but got O
		//IL_0447: Expected O, but got I
		//IL_0469: Expected O, but got I4
		//IL_04c5: Expected I, but got O
		//IL_04d5: Expected O, but got I
		//IL_04f7: Expected O, but got I4
		//IL_0553: Expected I, but got O
		//IL_0563: Expected O, but got I
		//IL_0585: Expected O, but got I4
		if (!Application.isPlaying || !(this.swingController != null) || !(this.turretController != null))
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (!(0f < deltaTime))
		{
			return;
		}
		TurretController turretController = this.turretController;
		bool flag = (object)this.turretController == null;
		float num = deltaTime;
		object obj = 0;
		SwingController swingController = null;
		if (!flag)
		{
			bool flag2 = !_hasLastSpeed;
			float num2 = 0f;
			if (!flag2)
			{
				float num3 = turretController.observedRotationSpeed - _lastSpeedDegPerSec;
				num2 = num3 / deltaTime;
			}
			_hasLastSpeed = true;
			_lastSpeedDegPerSec = turretController.observedRotationSpeed;
			if (maxAbsAngularAcceleration > 0f)
			{
				float num4 = maxAbsAngularAcceleration;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				float num5 = num4 ^ 0;
				if (!(num5 > num2))
				{
					float num6 = maxAbsAngularAcceleration;
					if (num2 > maxAbsAngularAcceleration)
					{
						num2 = maxAbsAngularAcceleration;
					}
				}
				else
				{
					num2 = num5;
				}
			}
			bool flag3 = !(accelerationSmoothing > 0f);
			num = deltaTime;
			if (!flag3)
			{
				float num7;
				if (!(0f > accelerationSmoothing))
				{
					bool flag4 = !(accelerationSmoothing > 1f);
					num7 = accelerationSmoothing;
					if (!flag4)
					{
						num7 = 1f;
					}
				}
				else
				{
					num7 = 0f;
				}
				num = deltaTime * 60f;
				float num8 = 1f - num7;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				float num6 = 1f - num8;
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
				float num9 = num2 - _smoothedAccel;
				float num10 = num9 * num6;
				num2 = (_smoothedAccel = num10 + _smoothedAccel);
			}
			float num11 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num12 = num11 & 0;
			if (!(accelerationDeadzone < num12))
			{
				return;
			}
			bool flag5 = absAngularAccelerationToAbsWorldXForce == null;
			float num13 = 0f;
			obj = 0;
			if (!flag5)
			{
				num13 = absAngularAccelerationToAbsWorldXForce.Evaluate(num12);
				float num6 = num12;
				obj = 0;
			}
			bool flag6 = !useAccelerationSignForXDirection;
			deltaTime = num13 * outputMultiplier;
			if (flag6 || !(num2 < 0f))
			{
				if (invertOutput)
				{
					throw new NullReferenceException();
				}
				swingController = this.swingController;
				if ((object)this.swingController == null)
				{
					goto IL_05d8;
				}
			}
			Vector2 worldXZImpulse = default(Vector2);
			this.swingController.AddExternalContinuousWorldXZ(worldXZImpulse);
			if (!logDebug)
			{
				return;
			}
			float time = Time.time;
			if (time < _nextLogTime)
			{
				return;
			}
			float time2 = Time.time;
			deltaTime = time2 + debugLogInterval;
			_nextLogTime = deltaTime;
			object[] array = new object[4];
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			SwingController swingController2 = default(SwingController);
			if ((object)swingController2 != null)
			{
				nint num14 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1092 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
				UnityEngine.Object obj2 = (UnityEngine.Object)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag7 = obj3 == null;
				obj = 0;
				swingController = swingController2;
				if (flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj4 = default(object);
					throw obj4;
				}
			}
			array[0] = swingController2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj5 = default(object);
			if (obj5 != null)
			{
				nint num15 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1120 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj7 = default(object);
				bool flag8 = obj7 == null;
				obj = 0;
				object obj8 = obj5;
				if (flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj9 = default(object);
					throw obj9;
				}
			}
			array[1] = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj10 = default(object);
			if (obj10 != null)
			{
				nint num16 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1148 @ rdx_v35 (Il2CppClass<System.Object[]>)+40]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj12 = default(object);
				bool flag9 = obj12 == null;
				obj = 0;
				object obj13 = obj10;
				if (flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj14 = default(object);
					throw obj14;
				}
			}
			array[2] = obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj15 = default(object);
			if (obj15 != null)
			{
				nint num17 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1176 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
				object obj16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj17 = default(object);
				bool flag10 = obj17 == null;
				obj = 0;
				object obj18 = obj15;
				if (flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj19 = default(object);
					throw obj19;
				}
			}
			array[3] = obj15;
			string message = string.Format("[TurretAccel->Swing] speed={0:0.###} deg/s, accel={1:0.###} deg/s^2, absAccel={2:0.###}, forceX={3:0.###}", array);
			Debug.Log(message, this);
			return;
		}
		goto IL_05d8;
		IL_05d8:
		throw new NullReferenceException();
	}
}
