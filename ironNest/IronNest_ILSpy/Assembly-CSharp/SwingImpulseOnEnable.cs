using Cpp2ILInjected;
using UnityEngine;

public sealed class SwingImpulseOnEnable : MonoBehaviour
{
	private SwingController swingController;

	private Vector2 worldDirectionXZ;

	private float strength;

	private float worldTwistImpulseY;

	private bool triggerOnAwake;

	private bool triggerOnEnable;

	private bool onlyOncePerLifetime;

	private bool logWhenFired;

	private bool _hasFired;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD37]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (triggerOnAwake)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 36 Invalid \"Jump target not found in method: 0x18058C6A0\"");
		}
	}

	private void OnEnable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD38]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (triggerOnEnable)
		{
			FireIfAllowed("OnEnable");
		}
	}

	private void FireIfAllowed(string source)
	{
		//IL_016f: Expected I, but got O
		//IL_01db: Expected I, but got O
		//IL_01eb: Expected O, but got I
		//IL_02c6: Expected O, but got F4
		//IL_0263: Expected I, but got O
		//IL_0273: Expected O, but got I
		//IL_02fa: Expected I, but got O
		//IL_030a: Expected O, but got I
		if (!Application.isPlaying || (onlyOncePerLifetime && _hasFired))
		{
			return;
		}
		if (this.swingController != null)
		{
			SwingController swingController = this.swingController;
			Vector2 vector = default(Vector2);
			Vector2 vector2;
			if (Application.isPlaying && swingController.allowExternalOneShot)
			{
				swingController.ApplyImpulseToAll(vector, worldTwistImpulseY);
				vector2 = vector;
			}
			bool flag = !logWhenFired;
			_hasFired = true;
			if (flag)
			{
				return;
			}
			object[] array = new object[4];
			bool flag2 = "SwingImpulseOnEnable" == null;
			object obj = "SwingImpulseOnEnable";
			if (!flag2)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj2 = default(object);
				if (obj2 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text = default(string);
					throw text;
				}
				obj = "SwingImpulseOnEnable";
			}
			array[0] = obj;
			if (source != null)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v675 @ rdx_v34 (Il2CppClass<System.Object[]>)+40]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj4 = default(object);
				if (obj4 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj5 = default(object);
					throw obj5;
				}
			}
			array[1] = source;
			object obj7 = default(object);
			object obj6 = (Vector2)obj7;
			if (obj6 != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v736 @ rdx_v32 (Il2CppClass<System.Object[]>)+40]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj9 = default(object);
				bool flag3 = obj9 == null;
				vector2 = vector;
				object obj10 = obj6;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj11 = default(object);
					throw obj11;
				}
			}
			array[2] = obj6;
			vector2 = (Vector2)worldTwistImpulseY;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj12 = default(object);
			if (obj12 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v819 @ rdx_v30 (Il2CppClass<System.Object[]>)+40]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj14 = default(object);
				bool flag4 = obj14 == null;
				object obj15 = obj12;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj16 = default(object);
					throw obj16;
				}
			}
			array[3] = obj12;
			string message = string.Format("[{0}] Fired impulse from {1}: XZ={2}, TwistY={3}", array);
			Debug.Log(message, this);
		}
		else if (logWhenFired)
		{
			string message2 = "[SwingImpulseOnEnable] No SwingController assigned; impulse not fired (" + source + ").";
			Debug.LogWarning(message2, this);
		}
	}

	public SwingImpulseOnEnable()
	{
		//IL_000b: Expected O, but got I4
		worldDirectionXZ = (Vector2)1065353216;
		strength = 1f;
		triggerOnEnable = true;
		base._002Ector();
	}
}
