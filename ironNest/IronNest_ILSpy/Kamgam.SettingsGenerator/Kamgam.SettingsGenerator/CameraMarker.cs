using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CameraMarker<T> : MonoBehaviour
{
	public static List<CameraMarker<T>> Markers;

	protected Camera _camera;

	public Camera Camera
	{
		get
		{
			//IL_0017: Expected O, but got I
			//IL_0055: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.CameraMarker`1<T>)+20]");
			if ((UnityEngine.Object)0 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.CameraMarker`1<T>)+20]");
			return (Camera)0;
		}
	}

	public unsafe static bool HasValidMarkers()
	{
		//IL_002a: Expected O, but got I
		//IL_003f: Expected O, but got I
		//IL_005f: Expected O, but got I
		//IL_0071: Expected O, but got Ref
		//IL_01d4: Expected O, but got I
		//IL_0086: Expected O, but got I
		//IL_0185: Expected O, but got I
		//IL_019a: Expected O, but got I
		//IL_00bd: Expected O, but got I
		//IL_00d2: Expected O, but got I
		//IL_00e2: Expected O, but got I
		//IL_0121: Expected O, but got I
		//IL_0136: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v11 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rax_v13+B8]");
		object obj2 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r8_v2 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+18]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj5 = default(object);
		object obj4 = (object)(&obj5);
		object obj8 = default(object);
		object obj11 = default(object);
		object obj12 = default(object);
		object obj15 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ stack_8_v3+20]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rax_v26+C0]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj8 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ stack_8_v3+20]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rax_v36+C0]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r8_v5+28]");
				obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj11 == null;
				nint num3 = (nint)(&obj12);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ stack_8_v3+20]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ rax_v40+C0]");
				object obj14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004C900");
				if (obj15 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807A6C00");
					return true;
				}
				continue;
			}
			object obj16 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ rax_v28+20]");
			object obj17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ rax_v30+C0]");
			object obj18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			return false;
		}
		throw new NullReferenceException();
	}

	public unsafe static CameraMarker<T> GetFirstValidMarker()
	{
		//IL_002a: Expected O, but got I
		//IL_003f: Expected O, but got I
		//IL_005f: Expected O, but got I
		//IL_0071: Expected O, but got Ref
		//IL_01cf: Expected O, but got I
		//IL_0086: Expected O, but got I
		//IL_0184: Expected O, but got I
		//IL_0199: Expected O, but got I
		//IL_00bd: Expected O, but got I
		//IL_00d2: Expected O, but got I
		//IL_00e2: Expected O, but got I
		//IL_0121: Expected O, but got I
		//IL_0136: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v11 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rax_v13+B8]");
		object obj2 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r8_v2 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+18]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj5 = default(object);
		object obj4 = (object)(&obj5);
		object obj8 = default(object);
		CameraMarker<T> cameraMarker = default(CameraMarker<T>);
		object obj11 = default(object);
		object obj14 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ stack_8_v3+20]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rax_v26+C0]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj8 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ stack_8_v3+20]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rax_v36+C0]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r8_v5+28]");
				obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = (object)cameraMarker == null;
				nint num3 = (nint)(&obj11);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ stack_8_v3+20]");
				object obj12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ rax_v40+C0]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004C900");
				if (obj14 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807A6C00");
					return cameraMarker;
				}
				continue;
			}
			object obj15 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ rax_v28+20]");
			object obj16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ rax_v30+C0]");
			object obj17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			return null;
		}
		throw new NullReferenceException();
	}

	public void OnEnable()
	{
	}

	public void Awake()
	{
		//IL_0020: Expected O, but got I
		//IL_0035: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v3 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v7+B8]");
		object obj2 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
	}

	public void OnDestroy()
	{
		//IL_0020: Expected O, but got I
		//IL_0035: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v3 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v7+B8]");
		object obj2 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5880");
	}

	public bool IsValid()
	{
		//IL_0127: Expected I4, but got O
		//IL_00c2: Expected O, but got I
		//IL_010a: Expected O, but got I
		if ((object)this != null)
		{
			if (base.isActiveAndEnabled)
			{
				GameObject gameObject = base.gameObject;
				if (gameObject != null)
				{
					GameObject gameObject2 = base.gameObject;
					if ((object)gameObject2 == null)
					{
						goto IL_0119;
					}
					if (gameObject2.activeInHierarchy)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.CameraMarker`1<T>)+20]");
						if ((UnityEngine.Object)0 == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.CameraMarker`1<T>)+20]");
						return (UnityEngine.Object)0 != null;
					}
				}
			}
			return false;
		}
		goto IL_0119;
		IL_0119:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	static CameraMarker()
	{
		//IL_0045: Expected O, but got I
		//IL_005a: Expected O, but got I
		nint num = 0;
		object obj = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12 (Il2CppRgctx<Kamgam.SettingsGenerator.CameraMarker`1>)+10]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v14+B8]");
		object obj3 = 0;
		obj3 = obj;
	}
}
