using Cpp2ILInjected;
using UnityEngine;

public class SimpleCharacterMotor : MonoBehaviour
{
	public CursorLockMode cursorLockMode = CursorLockMode.Locked;

	public bool cursorVisible;

	public float walkSpeed = 2f;

	public float runSpeed = 4f;

	public float gravity = 9.8f;

	public Transform cameraPivot;

	public float lookSpeed = 45f;

	public bool invertY = true;

	public float movementAcceleration = 1f;

	private CharacterController controller;

	private Vector3 movement;

	private Vector3 finalMovement;

	private float speed;

	private Quaternion targetRotation;

	private Quaternion targetPivotRotation;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		CharacterController characterController = default(CharacterController);
		controller = characterController;
		Cursor.lockState = cursorLockMode;
		Cursor.visible = cursorVisible;
		targetPivotRotation = Quaternion.identityQuaternion;
		targetRotation = Quaternion.identityQuaternion;
	}

	private void Update()
	{
		UpdateTranslation();
		UpdateLookRotation();
	}

	private unsafe void UpdateLookRotation()
	{
		//IL_00e3: Expected O, but got I8
		//IL_003c: Expected O, but got I4
		//IL_0091: Expected O, but got Ref
		//IL_00a5: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C4D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		float axis = Input.GetAxis("Mouse Y");
		float axis2 = Input.GetAxis("Mouse X");
		bool flag = invertY;
		object obj = 4294967295L;
		if (!flag)
		{
			obj = 1;
		}
		float num = axis * (float)obj;
		Transform transform = base.transform;
		Quaternion localRotation = transform.localRotation;
		float deltaTime = Time.deltaTime;
		float num2 = lookSpeed * axis2;
		float angle = num2 * deltaTime;
		Vector3 axis3 = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_AngleAxis(angle, ref axis3);
		Vector3 vector = default(Vector3);
		targetRotation = (Quaternion)vector;
		Quaternion localRotation2 = cameraPivot.localRotation;
		float deltaTime2 = Time.deltaTime;
		float num3 = lookSpeed * num;
		float angle2 = num3 * deltaTime2;
		Quaternion quaternion2 = Quaternion.Internal_AngleAxis(angle2, ref axis3);
		targetPivotRotation = (Quaternion)vector;
		Transform transform2 = base.transform;
		transform2.localRotation = (Quaternion)(&axis3);
		Quaternion quaternion3 = default(Quaternion);
		cameraPivot.localRotation = (Quaternion)(&quaternion3);
	}

	private unsafe void UpdateTranslation()
	{
		//IL_00cc: Expected F4, but got I
		//IL_00e8: Expected O, but got Ref
		//IL_00fb: Expected O, but got F4
		//IL_017e: Invalid comparison between I4 and F4
		//IL_0141: Expected F4, but got I4
		//IL_01af: Expected O, but got I
		//IL_0155: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C4E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!controller.isGrounded)
		{
			float deltaTime = Time.deltaTime;
			float num = deltaTime * gravity;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SimpleCharacterMotor)+5C]");
			float num2 = 0f - num;
		}
		else
		{
			float axis = Input.GetAxis("Horizontal");
			float axis2 = Input.GetAxis("Vertical");
			bool keyInt = Input.GetKeyInt(KeyCode.LeftShift);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SimpleCharacterMotor)+28+v260 @ rax_v7 (System.Boolean)*4]");
			speed = 0f;
			Transform transform = base.transform;
			object obj = default(object);
			Vector3 vector = transform.TransformDirection((Vector3)(&obj));
			movement = (Vector3)vector.x;
			_ = vector.z;
		}
		float deltaTime2 = Time.deltaTime;
		float num3 = deltaTime2 * movementAcceleration;
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SimpleCharacterMotor)+60]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SimpleCharacterMotor)+6C]");
		object obj2 = num4 - 0;
		float num5 = (float)obj2 * num3;
		float num6 = num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (SimpleCharacterMotor)+6C]");
		float num7 = num6 + 0f;
		Vector3 vector2 = default(Vector3);
		finalMovement = vector2;
		float deltaTime3 = Time.deltaTime;
		Vector3 vector3 = default(Vector3);
		CollisionFlags collisionFlags = controller.Move((Vector3)(&vector3));
	}
}
