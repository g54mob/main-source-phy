using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class FirstPersonFootstepsBridge : MonoBehaviour
{
	[Serializable]
	public class FootstepSurfaceBoolEvent : UnityEvent<bool>
	{
	}

	public FirstPersonController controller;

	public CharacterController characterController;

	public bool enableFootsteps = true;

	public bool suppressWhenPlayerCannotMove;

	public bool requireGrounded = true;

	public float metersPerStep = 1.8f;

	public float minHorizontalSpeedForSteps = 0.15f;

	public int maxStepsPerFrame = 2;

	public bool useCrouchStrideMultiplier;

	public float crouchStrideMultiplier = 1.25f;

	public MonoBehaviour crouchStateProvider;

	public LayerMask groundMask;

	public float groundRayDistance;

	public float groundRayOriginUpOffset;

	public bool enableSpecialSurfaceDetection;

	public UnityEvent OnFootstepDefault;

	public UnityEvent OnFootstepSpecial;

	public FootstepSurfaceBoolEvent OnFootstep;

	public bool debugDrawRay;

	public bool debugLogSteps;

	private float _accumulatedDistance;

	private bool _wasGrounded;

	private IFootstepCrouchStateProvider _crouchProvider;

	private void Awake()
	{
		if (controller == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			FirstPersonController firstPersonController = default(FirstPersonController);
			controller = firstPersonController;
		}
		if (this.characterController == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			CharacterController characterController = default(CharacterController);
			this.characterController = characterController;
		}
		if (crouchStateProvider != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IFootstepCrouchStateProvider crouchProvider = default(IFootstepCrouchStateProvider);
			_crouchProvider = crouchProvider;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		}
		if (useCrouchStrideMultiplier && crouchStateProvider != null && _crouchProvider == null)
		{
			Debug.LogWarning("[FirstPersonFootstepsBridge] 'crouchStateProvider' was assigned but does not implement IFootstepCrouchStateProvider. Crouch stride scaling will not be applied.", this);
		}
	}

	private void OnEnable()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_0110: Expected I, but got O
		FirstPersonController firstPersonController = controller;
		_accumulatedDistance = 0f;
		Action action = TriggerStep;
		if ((object)controller == null)
		{
			NullReferenceException ex = new NullReferenceException();
			nint num = 0;
			Action action2 = action;
		}
		else
		{
			Delegate obj = firstPersonController.OnJump;
			object obj2 = controller + 600;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, action);
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
					bool flag3 = (object)obj4 == null;
					nint num = unchecked((nint)null);
					NullReferenceException ex = (NullReferenceException)(object)obj3;
					Action action2 = (Action)(object)typeof(Action);
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj5 != obj;
				obj = obj5;
				if (!flag4)
				{
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void OnDisable()
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_0105: Expected I, but got O
		FirstPersonController firstPersonController = controller;
		Action action = TriggerStep;
		if ((object)controller == null)
		{
			NullReferenceException ex = new NullReferenceException();
			nint num = 0;
			Action action2 = action;
		}
		else
		{
			Delegate obj = firstPersonController.OnJump;
			object obj2 = controller + 600;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj, action);
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
					bool flag3 = (object)obj4 == null;
					nint num = unchecked((nint)null);
					NullReferenceException ex = (NullReferenceException)(object)obj3;
					Action action2 = (Action)(object)typeof(Action);
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj5 != obj;
				obj = obj5;
				if (!flag4)
				{
					return;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void Update()
	{
		//IL_0157: Invalid comparison between F4 and O
		//IL_0362: Expected F4, but got I4
		//IL_026c: Invalid comparison between F4 and I4
		float num3;
		float num4;
		if (enableFootsteps && characterController != null)
		{
			if (suppressWhenPlayerCannotMove && controller != null)
			{
				FirstPersonController firstPersonController = controller;
				if (!firstPersonController.playerCanMove)
				{
					goto IL_02e7;
				}
			}
			bool isGrounded = characterController.isGrounded;
			bool flag = _wasGrounded;
			bool flag2 = false;
			if (!flag)
			{
				flag2 = isGrounded;
			}
			if (flag2)
			{
				_wasGrounded = true;
				TriggerStep();
				return;
			}
			_wasGrounded = isGrounded;
			if (!requireGrounded || isGrounded)
			{
				Vector3 velocity = characterController.velocity;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				float num = minHorizontalSpeedForSteps;
				object obj = default(object);
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					return;
				}
				float deltaTime = Time.deltaTime;
				float num2 = deltaTime * (float)obj;
				float accumulatedDistance = num2 + _accumulatedDistance;
				_accumulatedDistance = accumulatedDistance;
				bool flag3 = 0.1f > metersPerStep;
				num3 = 0.1f;
				if (!flag3)
				{
					num3 = metersPerStep;
				}
				if (useCrouchStrideMultiplier && _crouchProvider != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj2 = default(object);
					if (obj2 != null)
					{
						num4 = crouchStrideMultiplier;
						bool flag4 = 0.25f > crouchStrideMultiplier;
						float num5 = 0.25f;
						if (!flag4)
						{
							bool flag5 = !(crouchStrideMultiplier > 3f);
							num5 = 3f;
							if (flag5)
							{
								goto IL_0370;
							}
						}
						num4 = num5;
						goto IL_0370;
					}
				}
				goto IL_0349;
			}
		}
		goto IL_02e7;
		IL_02e7:
		_accumulatedDistance = 0f;
		return;
		IL_0349:
		bool flag6 = _accumulatedDistance < num3;
		float num6 = 0f;
		if (flag6)
		{
			return;
		}
		while (num6 < (float)maxStepsPerFrame)
		{
			float accumulatedDistance2 = _accumulatedDistance - num3;
			num6++;
			_accumulatedDistance = accumulatedDistance2;
			TriggerStep();
			if (_accumulatedDistance < num3)
			{
				break;
			}
		}
		return;
		IL_0370:
		float num7 = num4 * num3;
		num3 = num7;
		goto IL_0349;
	}

	private unsafe void TriggerStep()
	{
		bool flag = ResolveIsSpecialSurface();
		if (debugLogSteps)
		{
			string text = "SPECIAL";
			if (!flag)
			{
				text = "DEFAULT";
			}
			string message = "[FirstPersonFootstepsBridge] Footstep: " + text;
			Debug.Log(message, this);
		}
		if (OnFootstep != null)
		{
			object obj = default(object);
			OnFootstep.Invoke((byte)(&obj) != 0);
		}
		(flag ? OnFootstepSpecial : OnFootstepDefault)?.Invoke();
	}

	private float GetCurrentStepDistance()
	{
		bool flag = !(0.1f < metersPerStep);
		float num = 0.1f;
		if (!flag)
		{
			num = metersPerStep;
		}
		float num2;
		if (useCrouchStrideMultiplier && _crouchProvider != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if (obj != null)
			{
				num2 = crouchStrideMultiplier;
				bool flag2 = 0.25f > crouchStrideMultiplier;
				float num3 = 0.25f;
				if (!flag2)
				{
					bool flag3 = !(crouchStrideMultiplier > 3f);
					num3 = 3f;
					if (flag3)
					{
						goto IL_00f4;
					}
				}
				num2 = num3;
				goto IL_00f4;
			}
		}
		return num;
		IL_00f4:
		return num2 * num;
	}

	private unsafe bool ResolveIsSpecialSurface()
	{
		//IL_0008: Expected O, but got Ref
		//IL_01c7: Expected I4, but got O
		//IL_0066: Expected O, but got Ref
		//IL_0066: Expected O, but got Ref
		//IL_00c8: Expected O, but got Ref
		//IL_00c8: Expected O, but got Ref
		//IL_00c8: Expected O, but got Ref
		//IL_0133: Expected O, but got Ref
		//IL_014d: Expected O, but got I
		//IL_0164: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		if (enableSpecialSurfaceDetection)
		{
			Transform transform = base.transform;
			if ((object)transform == null)
			{
				goto IL_01b9;
			}
			Vector3 position = transform.position;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			float num = default(float);
			float num2 = default(float);
			int num3 = default(int);
			QueryTriggerInteraction queryTriggerInteraction = default(QueryTriggerInteraction);
			bool flag = Physics.Raycast((Vector3)(&num), (Vector3)(&num2), out var hitInfo, groundRayDistance, num3, queryTriggerInteraction);
			if (debugDrawRay)
			{
				if (flag)
				{
				}
				Vector3 vector = default(Vector3);
				object obj3 = default(object);
				Debug.DrawRay((Vector3)(&vector), (Vector3)(&num), (Color)(&obj3), 0f, (byte)num3 != 0);
			}
			if (flag)
			{
				Collider collider = hitInfo.collider;
				if (collider != null)
				{
					Collider collider2 = hitInfo.collider;
					if ((object)collider2 != null)
					{
						object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+40]");
						UnityEngine.Object obj5 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+40]");
						if (!((UnityEngine.Object)0 != null))
						{
							goto IL_01b3;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+40]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rbx_v6 (UnityEngine.Object)+20]");
							return false;
						}
					}
					goto IL_01b9;
				}
			}
		}
		goto IL_01b3;
		IL_01b9:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_01b3:
		return false;
	}

	public FirstPersonFootstepsBridge()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		groundMask = layerMask;
		groundRayDistance = 1.25f;
		groundRayOriginUpOffset = 0.1f;
		enableSpecialSurfaceDetection = true;
		base._002Ector();
	}
}
