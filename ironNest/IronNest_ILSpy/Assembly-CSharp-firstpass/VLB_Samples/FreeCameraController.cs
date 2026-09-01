using Cpp2ILInjected;
using UnityEngine;

namespace VLB_Samples;

public class FreeCameraController : MonoBehaviour
{
	public float cameraSensitivity = 90f;

	public float speedNormal = 10f;

	public float speedFactorSlow = 0.25f;

	public float speedFactorFast = 3f;

	public float speedClimb = 4f;

	private float rotationH;

	private float rotationV;

	private bool m_UseMouseView = true;

	private bool useMouseView
	{
		get
		{
			return m_UseMouseView;
		}
		set
		{
			m_UseMouseView = value;
			Cursor.lockState = (value ? CursorLockMode.Locked : CursorLockMode.None);
			bool visible = (byte)((value ? 1u : 0u) ^ 1u) != 0;
			Cursor.visible = visible;
		}
	}

	private unsafe void Start()
	{
		//IL_005b: Expected O, but got Ref
		m_UseMouseView = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		Quaternion rotation2 = default(Quaternion);
		Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation2);
		object obj = default(object);
		Vector3 vector2 = Quaternion.Internal_MakePositive((Vector3)(&obj));
		rotationV = vector2.x;
		float num = default(float);
		rotationH = num;
		if (vector2.x > 180f)
		{
			float num2 = vector2.x - 360f;
			rotationV = num2;
		}
	}

	private unsafe void Update()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_05c1: Expected I, but got O
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Expected Ref, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0618: Expected I, but got O
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Expected Ref, but got Unknown
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Expected O, but got Unknown
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected O, but got Unknown
		//IL_06bb: Expected I, but got O
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Expected O, but got Unknown
		//IL_0745: Expected I, but got O
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 95;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39D8E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (m_UseMouseView)
		{
			float axis = Input.GetAxis("Mouse X");
			float deltaTime = Time.deltaTime;
			float num = cameraSensitivity * axis;
			float num2 = deltaTime * num;
			float num3 = num2 + rotationH;
			rotationH = num3;
			float axis2 = Input.GetAxis("Mouse Y");
			float deltaTime2 = Time.deltaTime;
			float num4 = cameraSensitivity * axis2;
			float num5 = deltaTime2 * num4;
			float num6 = rotationV - num5;
			rotationV = num6;
		}
		float num7 = rotationV;
		bool flag = -90f > rotationV;
		float num8 = -90f;
		if (!flag)
		{
			bool flag2 = !(rotationV > 90f);
			num8 = 90f;
			if (flag2)
			{
				goto IL_059a;
			}
		}
		num7 = num8;
		goto IL_059a;
		IL_059a:
		rotationV = num7;
		Transform transform = base.transform;
		nint num9 = (nint)typeof(Vector3);
		ref Vector3 axis3 = ref *(Vector3*)(obj - 73);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num10 = 0;
		_ = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v5 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		Quaternion quaternion = Quaternion.Internal_AngleAxis(rotationH, ref axis3);
		Quaternion rotation = (Quaternion)(obj - 41);
		_ = quaternion.x;
		transform.rotation = rotation;
		Transform transform2 = base.transform;
		Quaternion rotation2 = transform2.rotation;
		nint num11 = (nint)typeof(Vector3);
		ref Vector3 axis4 = ref *(Vector3*)(obj - 73);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v496 @ rax_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num12 = 0;
		_ = Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v500 @ rax_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		_ = 0;
		Quaternion quaternion2 = Quaternion.Internal_AngleAxis(rotationV, ref axis4);
		Quaternion rotation3 = (Quaternion)(obj - 41);
		transform2.rotation = rotation3;
		float num13 = speedNormal;
		if (!Input.GetKeyInt(KeyCode.LeftShift) && !Input.GetKeyInt(KeyCode.RightShift))
		{
			if (Input.GetKeyInt(KeyCode.LeftControl) || Input.GetKeyInt(KeyCode.RightControl))
			{
				num13 *= speedFactorSlow;
			}
		}
		else
		{
			num13 *= speedFactorFast;
		}
		Transform transform3 = base.transform;
		Vector3 position = transform3.position;
		_ = position.x;
		float axis5 = Input.GetAxis("Vertical");
		float deltaTime3 = Time.deltaTime;
		Transform transform4 = base.transform;
		Vector3 forward = transform4.forward;
		float num14 = axis5 * num13;
		Vector3 position2 = (Vector3)(obj - 73);
		float num15 = num14 * deltaTime3;
		_ = forward.x;
		float num16 = forward.z * num15;
		float num17 = num16 + position.z;
		transform3.position = position2;
		Transform transform5 = base.transform;
		Vector3 position3 = transform5.position;
		_ = position3.x;
		float axis6 = Input.GetAxis("Horizontal");
		float deltaTime4 = Time.deltaTime;
		Transform transform6 = base.transform;
		Vector3 right = transform6.right;
		float num18 = axis6 * num13;
		Vector3 position4 = (Vector3)(obj - 73);
		float num19 = num18 * deltaTime4;
		float num20 = right.z * num19;
		float num21 = num20 + position3.z;
		transform5.position = position4;
		if (Input.GetKeyInt(KeyCode.Q))
		{
			Transform transform7 = base.transform;
			Vector3 position5 = transform7.position;
			_ = position5.x;
			float deltaTime5 = Time.deltaTime;
			nint num22 = (nint)typeof(Vector3);
			Vector3 position6 = (Vector3)(obj - 73);
			float num23 = deltaTime5 * speedClimb;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v968 @ rax_v56 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v969 @ rcx_v57 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			float num25 = 0f * num23;
			_ = Vector3.upVector;
			float num26 = num25 + position5.z;
			transform7.position = position6;
		}
		if (Input.GetKeyInt(KeyCode.E))
		{
			Transform transform8 = base.transform;
			Vector3 position7 = transform8.position;
			_ = position7.x;
			float deltaTime6 = Time.deltaTime;
			nint num27 = (nint)typeof(Vector3);
			Vector3 position8 = (Vector3)(obj - 73);
			float num28 = deltaTime6 * speedClimb;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1020 @ rax_v50 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num29 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1021 @ rcx_v50 (Il2CppStaticFields<UnityEngine.Vector3>)+2C]");
			float num30 = 0f * num28;
			float num31 = num30 + position7.z;
			transform8.position = position8;
		}
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			bool flag3 = !m_UseMouseView;
			m_UseMouseView = flag3;
			bool lockState = !m_UseMouseView;
			Cursor.lockState = (lockState ? CursorLockMode.Locked : CursorLockMode.None);
			bool flag4 = !m_UseMouseView;
			bool visible = !flag4;
			Cursor.visible = visible;
		}
		if (Input.GetKeyDownInt(KeyCode.Escape))
		{
			m_UseMouseView = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
