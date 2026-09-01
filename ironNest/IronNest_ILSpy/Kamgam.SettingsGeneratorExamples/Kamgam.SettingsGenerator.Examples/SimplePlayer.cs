using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Kamgam.SettingsGenerator.Examples;

public class SimplePlayer : MonoBehaviour
{
	public Button OpenMenuButton;

	public float JumpForce;

	public float MoveForce;

	public InputActionAsset InputActionAsset;

	protected Rigidbody _rigidbody;

	protected InputAction _moveAction;

	protected PlayerInput _playerInput;

	protected bool _isNearGround;

	protected bool _jumpRequested;

	protected bool _movementRequested;

	protected Vector2 _movementDirection;

	public Rigidbody Rigidbody
	{
		get
		{
			if (_rigidbody == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Rigidbody rigidbody = default(Rigidbody);
				_rigidbody = rigidbody;
			}
			return _rigidbody;
		}
	}

	public InputAction MoveAction
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F166]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if (_moveAction == null)
			{
				if ((object)InputActionAsset == null)
				{
					return (InputAction)(object)new NullReferenceException();
				}
				InputAction moveAction = InputActionAsset.FindAction("Move");
				_moveAction = moveAction;
			}
			return _moveAction;
		}
	}

	public PlayerInput PlayerInput
	{
		get
		{
			if (_playerInput == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				PlayerInput playerInput = default(PlayerInput);
				_playerInput = playerInput;
			}
			return _playerInput;
		}
	}

	public void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F16B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		PlayerInput playerInput = PlayerInput;
		playerInput.SwitchCurrentActionMap("gameplay");
	}

	public void Update()
	{
		//IL_0090: Invalid comparison between F4 and I4
		//IL_016c: Invalid comparison between O and F4
		//IL_018b: Invalid comparison between F4 and I4
		Transform transform = base.transform;
		bool isNearGround;
		if (!(transform.position.y > -0.01f))
		{
			isNearGround = false;
		}
		else
		{
			Transform transform2 = base.transform;
			Vector3 position = transform2.position;
			bool flag = 0.05f < position.y;
			float num = 0.05f - position.y;
			bool flag2 = num == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			isNearGround = flag4 & flag3;
		}
		_isNearGround = isNearGround;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F166]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_moveAction == null)
		{
			InputAction moveAction = InputActionAsset.FindAction("Move");
			_moveAction = moveAction;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
		Vector2 vector = default(Vector2);
		_movementDirection = vector;
		object obj = vector * vector;
		object obj3 = default(object);
		object obj2 = obj3 * obj3;
		object obj4 = obj + obj2;
		bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f);
		float num2 = (float)obj4 - 0.0001f;
		bool flag6 = num2 == 0f;
		bool flag7 = !flag5;
		bool flag8 = !flag6;
		bool movementRequested = flag8 & flag7;
		_movementRequested = movementRequested;
	}

	public void OnJump()
	{
		_jumpRequested = true;
	}

	public void OnSpecialMove()
	{
		Debug.Log("Special Move: Though it does nothing in this demo.");
	}

	public void OnOpenMenu()
	{
		//IL_002e: Expected I, but got O
		//IL_003e: Expected O, but got I
		//IL_004e: Expected O, but got I
		while (true)
		{
			Button openMenuButton = OpenMenuButton;
			EventSystem current = EventSystem.current;
			PointerEventData pointerEventData = new PointerEventData(current);
			nint num = (nint)openMenuButton;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ r8_v2 (Il2CppClass<UnityEngine.UI.Button>)+3C8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ r8_v2 (Il2CppClass<UnityEngine.UI.Button>)+3D0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v70 @ r9_v1 (should have been resolved before IL gen)");
		}
	}

	public unsafe void FixedUpdate()
	{
		//IL_005f: Expected O, but got Ref
		//IL_0167: Expected O, but got Ref
		//IL_018d: Expected O, but got Ref
		Vector3 vector = default(Vector3);
		if (_isNearGround && _jumpRequested)
		{
			Rigidbody rigidbody = Rigidbody;
			rigidbody.AddForce((Vector3)(&vector), ForceMode.Impulse);
		}
		_jumpRequested = false;
		Rigidbody rigidbody2 = Rigidbody;
		Vector3 linearVelocity = rigidbody2.linearVelocity;
		if (_isNearGround && _movementRequested)
		{
			object obj2 = default(object);
			object obj = obj2 * obj2;
			float num = linearVelocity.x * linearVelocity.x;
			float num2 = linearVelocity.z * linearVelocity.z;
			float num3 = (float)obj + num;
			float num4 = num3 + num2;
			if (49f > num4)
			{
				Rigidbody rigidbody3 = Rigidbody;
				float num5 = default(float);
				rigidbody3.AddForce((Vector3)(&num5), ForceMode.Force);
				Rigidbody rigidbody4 = Rigidbody;
				rigidbody4.AddForce((Vector3)(&vector), ForceMode.Force);
			}
		}
	}

	public void SetControlsIdle(bool idle)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F16B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		PlayerInput playerInput = PlayerInput;
		if (!idle)
		{
			playerInput.SwitchCurrentActionMap("gameplay");
		}
		else
		{
			playerInput.SwitchCurrentActionMap("idle");
		}
	}

	public SimplePlayer()
	{
		//IL_0029: Expected I, but got O
		JumpForce = 30f;
		MoveForce = 150f;
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v3 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		_movementDirection = Vector2.zeroVector;
		base._002Ector();
	}
}
