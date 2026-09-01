using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class FpsController : ImmediateModeShapeDrawer
{
	private sealed class _003CFixedSteps_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FpsController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFixedSteps_003Ed__23(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0039: Expected I4, but got I8
			//IL_00a5: Expected I4, but got O
			if (_003C_003E1__state != 0 && _003C_003E1__state != 1)
			{
				return false;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.FixedUpdateManual();
				WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
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

	public Transform head;

	public Camera cam;

	public Crosshair crosshair;

	public ChargeBar chargeBar;

	public AmmoBar ammoBar;

	public Compass compass;

	public Transform crosshairTransform;

	public float smoof;

	public float moveSpeed;

	public float lookSensitivity;

	private float yaw;

	private float pitch;

	private Vector2 moveInput;

	private Vector3 moveVel;

	public float ammoBarAngularSpanRad;

	public float ammoBarOutlineThickness;

	public float ammoBarThickness;

	public float ammoBarRadius;

	public float fireSidebarRadiusPunchAmount;

	public AnimationCurve shakeAnimX;

	public AnimationCurve shakeAnimY;

	private bool InputFocus
	{
		get
		{
			bool visible = Cursor.visible;
			return (byte)((visible ? 1u : 0u) ^ 1u) != 0;
		}
		set
		{
			Cursor.lockState = (value ? CursorLockMode.Locked : CursorLockMode.None);
			bool visible = (byte)((value ? 1u : 0u) ^ 1u) != 0;
			Cursor.visible = visible;
		}
	}

	private void Awake()
	{
		if (Application.isPlaying)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			_003CFixedSteps_003Ed__23 obj = new _003CFixedSteps_003Ed__23(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	public unsafe override void DrawShapes(Camera cam)
	{
		//IL_0073: Expected I, but got O
		//IL_02b3: Expected I, but got O
		//IL_0307: Expected O, but got F4
		//IL_02c6: Expected I, but got O
		//IL_02d9: Expected I, but got O
		//IL_0376: Expected I, but got O
		//IL_00ec: Expected I, but got O
		//IL_0107: Expected I, but got O
		//IL_0216: Expected O, but got Ref
		if (cam == this.cam)
		{
			DrawCommand drawCommand = Draw.Command(cam);
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v12 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			_ = 8;
			if ((object)crosshairTransform == null)
			{
				throw new NullReferenceException();
			}
			Matrix4x4 localToWorldMatrix = crosshairTransform.localToWorldMatrix;
			nint num3 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ rax_v19 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num4 = 0;
			Draw.matrix = (Matrix4x4)localToWorldMatrix.m00;
			_ = localToWorldMatrix.m01;
			_ = localToWorldMatrix.m02;
			_ = localToWorldMatrix.m03;
			nint num5 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rax_v23 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num6 = 0;
			_ = 1;
			nint num7 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ rax_v27 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num8 = 0;
			_ = 0;
			bool flag = (object)this.crosshair == null;
			num2 = (nint)this.crosshair;
			if (flag)
			{
				throw new NullReferenceException();
			}
			this.crosshair.DrawCrosshair();
			Crosshair crosshair = this.crosshair;
			bool flag2 = (object)this.crosshair == null;
			num2 = (nint)this.crosshair;
			if (flag2)
			{
				throw new NullReferenceException();
			}
			num2 = (nint)crosshair.fireDecayer;
			if (crosshair.fireDecayer == null)
			{
				throw new NullReferenceException();
			}
			float num9 = fireSidebarRadiusPunchAmount * (float)Draw.mpbDisc;
			float barRadius = num9 + ammoBarRadius;
			if ((object)ammoBar == null)
			{
				throw new NullReferenceException();
			}
			ammoBar.DrawBar(this, barRadius);
			if ((object)chargeBar == null)
			{
				throw new NullReferenceException();
			}
			chargeBar.DrawBar(this, barRadius);
			if ((object)head == null)
			{
				throw new NullReferenceException();
			}
			Transform transform = head.transform;
			if ((object)transform == null)
			{
				throw new NullReferenceException();
			}
			Vector3 forward = transform.forward;
			float num10 = default(float);
			compass.DrawCompass((Vector3)(&num10));
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
	}

	private IEnumerator FixedSteps()
	{
		_003CFixedSteps_003Ed__23 obj = new _003CFixedSteps_003Ed__23(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public static void DrawRoundedArcOutline(Vector2 origin, float radius, float thickness, float outlineThickness, float angStart, float angEnd)
	{
		float num = thickness * 0.5f;
		float num2 = thickness * 0.5f;
		float num3 = num + radius;
		float num4 = radius - num2;
		object obj = default(object);
		float num5 = (float)obj - 0.01f;
		object obj2 = default(object);
		float num6 = (float)obj2 + 0.01f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CE60");
		float num7 = (float)obj - 0.01f;
		float num8 = (float)obj2 + 0.01f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CE60");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		float num9 = thickness * 0.5f;
		float num10 = (float)obj - (float)Math.PI;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CE60");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		float num11 = thickness * 0.5f;
		float num12 = (float)obj2 + (float)Math.PI;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CE60");
	}

	public Vector2 GetShake(float speed, float amp)
	{
		float time = Time.time;
		float num = time * speed;
		float num2 = MathF.Floor(num);
		float time2 = num - num2;
		if (shakeAnimX != null)
		{
			float num3 = shakeAnimX.Evaluate(time2);
			if (shakeAnimY != null)
			{
				float num4 = shakeAnimY.Evaluate(time2);
				Vector2 result = default(Vector2);
				return result;
			}
		}
		return (Vector2)new NullReferenceException();
	}

	private unsafe void FixedUpdateManual()
	{
		//IL_0130: Expected O, but got Ref
		if (Application.isPlaying)
		{
			Vector3 vector = default(Vector3);
			float x = default(float);
			if (!Cursor.visible)
			{
				Vector3 right = head.right;
				Vector3 forward = head.forward;
				float fixedDeltaTime = Time.fixedDeltaTime;
				float num = fixedDeltaTime * moveSpeed;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+78]");
				float num2 = 0f * forward.z;
				float num3 = (float)moveInput * right.z;
				float num4 = num3 + num2;
				moveVel = vector;
				float num5 = num4 * num;
				float num6 = num5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+84]");
				float num7 = num6 + 0f;
				x = right.x;
			}
			Transform transform = base.transform;
			Vector3 position = transform.position;
			float deltaTime = Time.deltaTime;
			transform.position = (Vector3)(&x);
			float num8 = smoof;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+84]");
			float num9 = num8 * 0f;
			moveVel = vector;
		}
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0680: Invalid comparison between I4 and F4
		//IL_0146: Expected F4, but got I4
		//IL_0264: Expected O, but got Ref
		//IL_07e2: Expected I, but got O
		//IL_0759: Expected I, but got O
		//IL_081d: Expected I, but got O
		//IL_055c: Expected O, but got I
		//IL_0376: Expected O, but got I4
		//IL_0862: Expected I, but got O
		//IL_059d: Expected O, but got I
		//IL_08a7: Expected I, but got O
		//IL_05de: Expected O, but got I
		//IL_061f: Expected O, but got I
		//IL_03fb: Expected O, but got Ref
		//IL_0448: Expected O, but got Ref
		//IL_049a: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (!Application.isPlaying)
		{
			return;
		}
		Crosshair crosshair = this.crosshair;
		crosshair.fireDecayer.Update();
		crosshair.hitDecayer.Update();
		ChargeBar chargeBar = this.chargeBar;
		float num2;
		if (!chargeBar.isCharging)
		{
			float deltaTime = Time.deltaTime;
			float num = deltaTime * chargeBar.chargeDecaySpeed;
			num2 = chargeBar.charge - num;
		}
		else
		{
			float deltaTime2 = Time.deltaTime;
			float num3 = deltaTime2 * chargeBar.chargeSpeed;
			float num4 = num3 + chargeBar.charge;
			num2 = num4;
		}
		chargeBar.charge = num2;
		float charge;
		if (!(0f > num2))
		{
			bool flag = !(num2 > 1f);
			charge = num2;
			if (!flag)
			{
				charge = 1f;
			}
		}
		else
		{
			charge = 0f;
		}
		chargeBar.charge = charge;
		bool visible;
		if (Cursor.visible)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Cursor.lockState = CursorLockMode.Locked;
				visible = false;
				goto IL_07c6;
			}
			return;
		}
		float axis = Input.GetAxis("Mouse X");
		float num5 = axis * lookSensitivity;
		float num6 = num5 + yaw;
		yaw = num6;
		float axis2 = Input.GetAxis("Mouse Y");
		float num7 = axis2 * lookSensitivity;
		float num8 = pitch - num7;
		bool flag2 = -90f > num8;
		float num9 = -90f;
		if (!flag2)
		{
			bool flag3 = !(num8 > 90f);
			num9 = 90f;
			if (flag3)
			{
				goto IL_06c3;
			}
		}
		num8 = num9;
		goto IL_06c3;
		IL_06c3:
		ref Vector3 euler = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		float num10 = yaw * ((float)Math.PI / 180f);
		pitch = num8;
		float num11 = num8 * ((float)Math.PI / 180f);
		_ = 0;
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		_ = quaternion.x;
		head.localRotation = localRotation;
		ChargeBar chargeBar2 = this.chargeBar;
		bool mouseButton = Input.GetMouseButton(1);
		chargeBar2.isCharging = mouseButton;
		if (Input.GetKeyInt(KeyCode.R))
		{
			AmmoBar ammoBar = this.ammoBar;
			ammoBar.bullets = ammoBar.totalBullets;
		}
		if (Input.GetMouseButtonDown(0))
		{
			AmmoBar ammoBar2 = this.ammoBar;
			if (ammoBar2.bullets > 0)
			{
				float[] bulletFireTimes = ammoBar2.bulletFireTimes;
				int bullets = ammoBar2.bullets - 1;
				ammoBar2.bullets = bullets;
				float time = Time.time;
				object obj3 = ammoBar2.bullets - 1;
				bulletFireTimes[obj3] = time;
				Crosshair crosshair2 = this.crosshair;
				Decayer fireDecayer = crosshair2.fireDecayer;
				fireDecayer.t = 1f;
				Vector3 position = head.position;
				Vector3 forward = head.forward;
				_ = position.x;
				Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 13));
				_ = position.z;
				_ = forward.x;
				_ = forward.z;
				((Vector3*)vector)->Normalize();
				ref RaycastHit hitInfo = ref System.Runtime.CompilerServices.Unsafe.As<object, RaycastHit>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
				Ray ray = (Ray)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-9]");
				_ = 0;
				if (Physics.Raycast(ray, out hitInfo))
				{
					RaycastHit raycastHit = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
					Collider collider = ((RaycastHit*)raycastHit)->collider;
					GameObject gameObject = collider.gameObject;
					string text = gameObject.name;
					if (text == "Enemy")
					{
						Crosshair crosshair3 = this.crosshair;
						Decayer hitDecayer = crosshair3.hitDecayer;
						hitDecayer.t = 1f;
					}
				}
			}
		}
		nint num12 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v767 @ rax_v23 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v768 @ rax_v24 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		moveInput = Vector2.zeroVector;
		nint num14 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v785 @ rax_v27 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num15 = 0;
		if (Input.GetKeyInt(KeyCode.W))
		{
			Vector2 vector2 = Vector2.upVector + moveInput;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v787 @ rcx_v23 (Il2CppStaticFields<UnityEngine.Vector2>)+14]");
			nint num16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+78]");
			object obj4 = num16 + 0;
			moveInput = vector2;
		}
		nint num17 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v821 @ rax_v30 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num18 = 0;
		if (Input.GetKeyInt(KeyCode.S))
		{
			Vector2 vector3 = Vector2.downVector + moveInput;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v823 @ rcx_v26 (Il2CppStaticFields<UnityEngine.Vector2>)+1C]");
			nint num19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+78]");
			object obj5 = num19 + 0;
			moveInput = vector3;
		}
		nint num20 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v857 @ rax_v33 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num21 = 0;
		if (Input.GetKeyInt(KeyCode.D))
		{
			Vector2 vector4 = Vector2.rightVector + moveInput;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v859 @ rcx_v29 (Il2CppStaticFields<UnityEngine.Vector2>)+2C]");
			nint num22 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+78]");
			object obj6 = num22 + 0;
			moveInput = vector4;
		}
		nint num23 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v895 @ rax_v36 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num24 = 0;
		if (Input.GetKeyInt(KeyCode.A))
		{
			Vector2 vector5 = Vector2.leftVector + moveInput;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v897 @ rcx_v32 (Il2CppStaticFields<UnityEngine.Vector2>)+24]");
			nint num25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+78]");
			object obj7 = num25 + 0;
			moveInput = vector5;
		}
		if (Input.GetKeyDownInt(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			visible = true;
			goto IL_07c6;
		}
		return;
		IL_07c6:
		Cursor.visible = visible;
	}

	public FpsController()
	{
		//IL_004a: Expected I, but got O
		//IL_0085: Expected I, but got O
		smoof = 0.99f;
		moveSpeed = 1f;
		lookSensitivity = 1f;
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		moveInput = Vector2.zeroVector;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		moveVel = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		ammoBarOutlineThickness = 0.1f;
		fireSidebarRadiusPunchAmount = 0.1f;
		AnimationCurve animationCurve = AnimationCurve.Constant(0f, 1f, 0f);
		shakeAnimX = animationCurve;
		AnimationCurve animationCurve2 = AnimationCurve.Constant(0f, 1f, 0f);
		shakeAnimY = animationCurve2;
		base._002Ector();
	}

	private void _003CUpdate_003Eg__DoInput_007C30_0(KeyCode key, Vector2 dir)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		if (Input.GetKeyInt(key))
		{
			Vector2 vector = dir + moveInput;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.FpsController)+78]");
			object obj2 = default(object);
			object obj = obj2 + 0;
			moveInput = vector;
		}
	}
}
