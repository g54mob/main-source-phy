using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class CameraZoneRTSTransitionHelper : MonoBehaviour
{
	public CameraZoneTrigger transitionScript;

	public RTSMapCameraController rtsCameraController;

	public Canvas mapCanvas;

	private void Start()
	{
		if ((bool)transitionScript)
		{
			CameraZoneTrigger cameraZoneTrigger = transitionScript;
			UnityAction call = OnRTSCameraActivated;
			cameraZoneTrigger.onConsoleActivated.AddListener(call);
		}
	}

	public unsafe void OnRTSCameraActivated()
	{
		//IL_008c: Expected O, but got Ref
		//IL_007d: Expected O, but got Ref
		object obj = default(object);
		Vector3? canvasLookAtLocalPoint = ((CameraZoneRTSTransitionHelper)(&obj)).GetCanvasLookAtLocalPoint();
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null && (bool)rtsCameraController)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			rtsCameraController.CenterOnFocusPointLocal((Vector3)(&obj));
		}
	}

	private unsafe Vector3? GetCanvasLookAtLocalPoint()
	{
		//IL_0008: Expected O, but got Ref
		//IL_001e: Expected O, but got I
		//IL_03a6: Expected O, but got I4
		//IL_0051: Expected O, but got I
		//IL_00f3: Expected O, but got Ref
		//IL_012a: Expected O, but got Ref
		//IL_014a: Expected O, but got I
		//IL_019d: Expected O, but got I
		//IL_01b2: Expected O, but got I
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Expected O, but got Unknown
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Expected O, but got Unknown
		//IL_01f0: Expected F4, but got I4
		//IL_01f9: Expected F4, but got I4
		//IL_0202: Expected F4, but got I4
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Expected O, but got Unknown
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Expected O, but got Unknown
		//IL_05b9: Invalid comparison between F4 and O
		//IL_025b: Expected O, but got I4
		//IL_0285: Invalid comparison between F4 and I4
		//IL_0294: Invalid comparison between F4 and I4
		//IL_02bd: Expected O, but got I4
		//IL_0310: Expected O, but got I
		//IL_0554: Expected O, but got Ref
		//IL_055d: Expected O, but got I4
		//IL_0393: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+30]");
		object obj13;
		if ((bool)(UnityEngine.Object)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+28]");
			if ((bool)(UnityEngine.Object)0)
			{
				Camera main = Camera.main;
				if ((bool)main)
				{
					int width = Screen.width;
					int height = Screen.height;
					float num = (float)height * 0.5f;
					if ((object)main != null)
					{
						float num2 = default(float);
						Ray ray = main.ScreenPointToRay((Vector3)(&num2));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+30]");
						if ((nint)0 != 0)
						{
							object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
							if ((bool)(UnityEngine.Object)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
								if ((nint)0 == 0)
								{
									goto IL_03ab;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
								Vector3 forward = ((Transform)0).forward;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
								Vector3 position = ((Transform)0).position;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
								float num3;
								float num4;
								float num5;
								if (!(position.x > 1E-05f))
								{
									num3 = 0f;
									num4 = 0f;
									num5 = 0f;
								}
								else
								{
									num5 = forward.x / position.x;
									object obj4 = default(object);
									num3 = (float)obj4 / position.x;
									num4 = forward.z / position.x;
								}
								float num6 = position.x * num5;
								object obj5 = default(object);
								float num7 = (float)obj5 * num3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v502 @ rax_v16 (UnityEngine.Ray)+10]");
								float num8 = 0f * num3;
								float num9 = num7 + num6;
								Vector3? vector = default(Vector3?);
								float num10 = (float)vector * num3;
								float num11 = position.z * num4;
								float num12 = (float)vector * num5;
								float num13 = num9 + num11;
								object obj6 = default(object);
								float num14 = (float)obj6 * num4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
								object obj7 = num13 ^ 0;
								float num15 = num8 + num12;
								float num16 = (float)ray.m_Origin * num5;
								float num17 = num15 + num14;
								float num18 = num10 + num16;
								float num19 = (float)vector * num4;
								float num20 = num18 + num19;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
								object obj8 = num20 ^ 0;
								object obj9 = obj8 - obj7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
								object obj10 = num17 & 0;
								float num21 = 0f - num17;
								if ((nint)obj10 < 0)
								{
									obj10 = 0;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
								object obj11 = num21 & 0;
								float num22 = Mathf.Epsilon * 8f;
								float num23 = (float)obj10 * 1E-06f;
								if (num23 < num22)
								{
									num23 = num22;
								}
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num23) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
								{
									float num24 = (float)obj9 / num17;
									bool flag = num24 < 0f;
									bool flag2 = num24 == 0f;
									bool flag3 = !flag;
									bool flag4 = !flag2;
									object obj12 = flag4 & flag3;
									if (obj12 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+28]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+28]");
											Transform transform = ((Component)0).transform;
											if ((object)transform != null)
											{
												Transform parent = transform.parent;
												if ((bool)parent)
												{
													if ((object)parent == null)
													{
														goto IL_03ab;
													}
													Vector3 vector2 = parent.InverseTransformPoint((Vector3)(&num2));
												}
												Vector3? vector3 = (Vector3)(&num2);
												obj13 = 0;
												goto IL_0562;
											}
										}
										goto IL_03ab;
									}
								}
							}
							goto IL_039d;
						}
					}
					goto IL_03ab;
				}
			}
		}
		goto IL_039d;
		IL_0562:
		CameraZoneRTSTransitionHelper cameraZoneRTSTransitionHelper = (CameraZoneRTSTransitionHelper)obj13;
		return (Vector3?)this;
		IL_03ab:
		return (Vector3?)new NullReferenceException();
		IL_039d:
		obj13 = 0;
		goto IL_0562;
	}
}
