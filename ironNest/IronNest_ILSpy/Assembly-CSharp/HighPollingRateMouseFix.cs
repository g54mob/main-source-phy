using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public sealed class HighPollingRateMouseFix : MonoBehaviour
{
	private struct RAWINPUTDEVICE
	{
		public ushort usUsagePage;

		public ushort usUsage;

		public uint dwFlags;

		public IntPtr hwndTarget;
	}

	private struct POINT
	{
		public int x;

		public int y;
	}

	private struct RECT
	{
		public int left;

		public int top;

		public int right;

		public int bottom;
	}

	private struct RAWINPUTHEADER
	{
		public uint dwType;

		public uint dwSize;

		public IntPtr hDevice;

		public IntPtr wParam;
	}

	[StructLayout((LayoutKind)2)]
	private struct RAWMOUSE
	{
		[FieldOffset(0)]
		public ushort usFlags;

		[FieldOffset(4)]
		public ushort usButtonFlags;

		[FieldOffset(6)]
		public ushort usButtonData;

		[FieldOffset(8)]
		public uint ulRawButtons;

		[FieldOffset(12)]
		public int lLastX;

		[FieldOffset(16)]
		public int lLastY;

		[FieldOffset(20)]
		public uint ulExtraInformation;
	}

	private struct RAWINPUTMOUSE
	{
		public RAWINPUTHEADER header;

		public RAWMOUSE mouse;
	}

	private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	private bool onlyActivateInFullscreen = true;

	private bool fixEnabled;

	private const uint RIDEV_NOLEGACY = 48u;

	private const uint RID_INPUT = 268435459u;

	private const uint RIM_TYPEMOUSE = 0u;

	private const uint WM_INPUT = 255u;

	private const uint WM_KILLFOCUS = 8u;

	private const int GWLP_WNDPROC = -4;

	private const ushort MOUSE_MOVE_ABSOLUTE = 1;

	private const ushort RI_MOUSE_LEFT_DOWN = 1;

	private const ushort RI_MOUSE_LEFT_UP = 2;

	private const ushort RI_MOUSE_RIGHT_DOWN = 4;

	private const ushort RI_MOUSE_RIGHT_UP = 8;

	private const ushort RI_MOUSE_MIDDLE_DOWN = 16;

	private const ushort RI_MOUSE_MIDDLE_UP = 32;

	private const ushort RI_MOUSE_BUTTON4_DOWN = 64;

	private const ushort RI_MOUSE_BUTTON4_UP = 128;

	private const ushort RI_MOUSE_BUTTON5_DOWN = 256;

	private const ushort RI_MOUSE_BUTTON5_UP = 512;

	private const ushort RI_MOUSE_WHEEL = 1024;

	private const ushort RI_MOUSE_HWHEEL = 2048;

	private const float WHEEL_DELTA = 120f;

	private static HighPollingRateMouseFix s_instance;

	private static IntPtr s_hwnd;

	private static IntPtr s_originalWndProc;

	private static WndProc s_wndProcHook;

	private static bool s_suppressionActive;

	private static int s_pendingDeltaX;

	private static int s_pendingDeltaY;

	private static float s_pendingScrollX;

	private static float s_pendingScrollY;

	private static ushort s_buttons;

	private static ushort s_clickCount;

	private static double s_lastLeftPressTime = -1.0 / 0.0;

	private static POINT s_lastAbsolute;

	private static bool s_hasLastAbsolute;

	[PreserveSig]
	private static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] devices, uint count, uint structSize);

	[PreserveSig]
	private static extern uint GetRawInputData(IntPtr hRawInput, uint command, out RAWINPUTMOUSE data, ref uint size, uint headerSize);

	[PreserveSig]
	private static extern bool GetCursorPos(out POINT point);

	[PreserveSig]
	private static extern bool ScreenToClient(IntPtr hWnd, ref POINT point);

	[PreserveSig]
	private static extern bool GetClientRect(IntPtr hWnd, out RECT rect);

	[PreserveSig]
	private static extern IntPtr GetActiveWindow();

	[PreserveSig]
	private static extern uint GetDoubleClickTime();

	[PreserveSig]
	private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int index, IntPtr newLong);

	[PreserveSig]
	private static extern IntPtr CallWindowProc(IntPtr prevProc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	[PreserveSig]
	private static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	private unsafe void Awake()
	{
		//IL_0046: Expected O, but got I4
		//IL_0106: Expected O, but got I4
		//IL_0095: Expected O, but got I4
		//IL_0181: Expected O, but got I
		//IL_013c: Expected O, but got I4
		//IL_01c8: Expected O, but got Ref
		//IL_0218: Expected I, but got O
		//IL_028b: Expected I, but got O
		//IL_0458: Expected I, but got I8
		//IL_0274: Expected I, but got I8
		//IL_02c2: Expected I4, but got I8
		fixEnabled = false;
		string graphicsDeviceName = SystemInfo.graphicsDeviceName;
		if (graphicsDeviceName == null)
		{
			goto IL_037d;
		}
		object obj;
		if (!graphicsDeviceName.Contains("RTX"))
		{
			string graphicsDeviceName2 = SystemInfo.graphicsDeviceName;
			bool flag = graphicsDeviceName2 == null;
			obj = 0;
			if (flag)
			{
				goto IL_037d;
			}
			if (!graphicsDeviceName2.Contains("GTX"))
			{
				string graphicsDeviceName3 = SystemInfo.graphicsDeviceName;
				bool flag2 = graphicsDeviceName3 == null;
				obj = 0;
				if (flag2)
				{
					goto IL_037d;
				}
				if (!graphicsDeviceName3.Contains("RX"))
				{
					goto IL_03ba;
				}
			}
		}
		fixEnabled = true;
		goto IL_03ba;
		IL_03ba:
		if (!fixEnabled)
		{
			return;
		}
		bool flag3 = s_instance != null;
		bool flag4 = !flag3;
		obj = 0;
		if (!flag4)
		{
			bool flag5 = s_instance != this;
			bool flag6 = !flag5;
			obj = 0;
			if (!flag6)
			{
				GameObject obj2 = base.gameObject;
				UnityEngine.Object.Destroy(obj2);
				return;
			}
		}
		s_instance = this;
		GameObject gameObject = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A508]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A508]");
		bool flag7 = (nint)0 != 0;
		UnityEngine.Object obj4 = gameObject;
		if (!flag7)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
			object obj5 = default(object);
			obj4 = (UnityEngine.Object)(&obj5);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v573 @ rax_v25 (should have been resolved before IL gen)");
		IntPtr intPtr = default(IntPtr);
		s_hwnd = intPtr;
		if (s_hwnd != (IntPtr)0)
		{
			WndProc wndProc = null;
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ rbx_v7 (Il2CppMethodInfo)+8]");
			((Delegate)wndProc).method_ptr = (IntPtr)0;
			((Delegate)wndProc).method = (nint)(delegate*<IntPtr, uint, IntPtr, IntPtr, IntPtr>)(&WndProcHook);
			((Delegate)wndProc).m_target = null;
			((Delegate)wndProc).method_code = (IntPtr)wndProc;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
			object obj6 = default(object);
			if (obj6 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6F80");
				object obj7 = default(object);
				throw obj7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ rbx_v7 (Il2CppMethodInfo)+52]");
			nint invoke_impl;
			if ((nint)0 == 4)
			{
				invoke_impl = unchecked((nint)6442483008L);
			}
			else
			{
				((Delegate)wndProc).method_code = (IntPtr)((Delegate)wndProc).m_target;
				invoke_impl = ((Delegate)wndProc).method_ptr;
			}
			((Delegate)wndProc).invoke_impl = invoke_impl;
			((Delegate)wndProc).extra_arg = unchecked((nint)6442482848L);
			s_wndProcHook = wndProc;
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(s_wndProcHook);
			IntPtr intPtr2 = SetWindowLongPtr(s_hwnd, -4, functionPointerForDelegate);
			s_originalWndProc = intPtr2;
			if (s_originalWndProc != (IntPtr)0)
			{
				Action value = InjectPerFrameState;
				InputSystem.onBeforeUpdate += value;
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[HighPollingRateMouseFix] SetWindowLongPtr failed (Win32 error {arg}); fix disabled.";
			Debug.LogError(message);
			s_wndProcHook = null;
			base.enabled = false;
		}
		else
		{
			Debug.LogError("[HighPollingRateMouseFix] Could not resolve the player window handle; fix disabled.");
			base.enabled = false;
		}
		return;
		IL_037d:
		throw new NullReferenceException();
	}

	private void Update()
	{
		//IL_004c: Expected O, but got I4
		if (fixEnabled)
		{
			bool flag;
			if (!onlyActivateInFullscreen)
			{
				flag = true;
			}
			else
			{
				FullScreenMode fullScreenMode = Screen.fullScreenMode;
				object obj = fullScreenMode - 3;
				bool flag2 = obj == null;
				flag = !flag2;
			}
			if (flag != s_suppressionActive)
			{
				SetSuppression(flag);
			}
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus && fixEnabled)
		{
			if (s_suppressionActive)
			{
				SetSuppression(active: true);
			}
		}
		else
		{
			ReleaseAllButtons();
		}
	}

	private void OnDestroy()
	{
		//IL_00e1: Expected I4, but got I8
		if (s_instance == this)
		{
			Action value = InjectPerFrameState;
			InputSystem.onBeforeUpdate -= value;
			if (s_suppressionActive)
			{
				SetSuppression(active: false);
			}
			if (s_hwnd != (IntPtr)0 && s_originalWndProc != (IntPtr)0)
			{
				IntPtr intPtr = SetWindowLongPtr(s_hwnd, -4, s_originalWndProc);
				s_originalWndProc = (IntPtr)0;
			}
			s_wndProcHook = null;
			s_instance = null;
		}
	}

	private static void SetSuppression(bool active)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_005d: Expected O, but got I
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		RAWINPUTDEVICE[] array = new RAWINPUTDEVICE[1];
		_ = 1;
		_ = 2;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
		object obj = typeof(RAWINPUTDEVICE[]) & 0x30;
		_ = s_hwnd;
		int num = Marshal.SizeOf<RAWINPUTDEVICE>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4E0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4E0]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
		}
		object obj3 = array + 32;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v257 @ rax_v14 (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A74C0");
		object obj4 = default(object);
		if (obj4 != null)
		{
			s_suppressionActive = active;
			if (!active)
			{
				ReleaseAllButtons();
			}
			ResetAccumulators();
			s_hasLastAbsolute = false;
		}
		else
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[HighPollingRateMouseFix] RegisterRawInputDevices failed (Win32 error {arg}).";
			Debug.LogError(message);
		}
	}

	private unsafe static IntPtr WndProcHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0253: Expected O, but got I
		//IL_0293: Expected O, but got Ref
		//IL_007e: Expected O, but got I
		//IL_00be: Expected O, but got Ref
		//IL_015d: Expected O, but got I
		//IL_019d: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (msg == 255)
		{
			bool flag = !s_suppressionActive;
			nint num = wParam;
			nint num2 = lParam;
			uint num3 = msg;
			if (!flag)
			{
				int num4 = Marshal.SizeOf<RAWINPUTMOUSE>();
				int num5 = Marshal.SizeOf<RAWINPUTHEADER>();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4E8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4E8]");
				if ((nint)0 == 0)
				{
					_ = 6478131240L;
					object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 49));
					_ = 6478131264L;
					_ = 10;
					_ = 15;
					_ = 0;
					_ = 2;
					_ = 32;
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
				}
				num2 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
				num = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 1));
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v301 @ rax_v22 (should have been resolved before IL gen)");
				object obj5 = default(object);
				bool flag2 = (nint)obj5 == -1;
				num3 = 268435459u;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-1]");
					bool flag3 = (nint)0 != 0;
					num3 = 268435459u;
					if (!flag3)
					{
						ProcessRawMouse(ref System.Runtime.CompilerServices.Unsafe.As<object, RAWMOUSE>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23)));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A528]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A528]");
						if ((nint)0 == 0)
						{
							_ = 6478131240L;
							object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 49));
							_ = 6478131408L;
							_ = 10;
							_ = 14;
							_ = 0;
							_ = 2;
							_ = 28;
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v323 @ rax_v27 (should have been resolved before IL gen)");
						goto IL_0377;
					}
				}
			}
		}
		else
		{
			bool flag4 = msg != 8;
			nint num = wParam;
			nint num2 = lParam;
			uint num3 = msg;
			if (!flag4)
			{
				ReleaseAllButtons();
				num = wParam;
				num2 = lParam;
				num3 = msg;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A520]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A520]");
		if ((nint)0 == 0)
		{
			_ = 6478131240L;
			object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 49));
			_ = 6478131392L;
			_ = 10;
			_ = 15;
			_ = 0;
			_ = 2;
			_ = 36;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v215 @ rax_v7 (should have been resolved before IL gen)");
		goto IL_0377;
		IL_0377:
		IntPtr result = default(IntPtr);
		return result;
	}

	private unsafe static void ProcessRawMouse([In] ref RAWMOUSE m)
	{
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_01af: Expected O, but got I4
		//IL_022c: Expected I, but got O
		//IL_0209: Expected I, but got O
		//IL_0551: Expected O, but got I
		//IL_055f: Expected I, but got O
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Expected I4, but got Unknown
		//IL_027d: Expected I, but got O
		//IL_02a3: Expected O, but got I
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Expected I, but got Unknown
		//IL_02cf: Expected I, but got O
		//IL_0054: Expected I, but got O
		//IL_0071: Expected O, but got I
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_00c8: Expected O, but got I4
		//IL_0391: Expected O, but got I
		//IL_00e4: Expected O, but got Ref
		object obj = m & 1;
		bool flag = obj == null;
		object obj2 = !flag;
		if (obj2 == null)
		{
			int num = s_pendingDeltaX;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+C]");
			int num2 = (int)((nint)num + (nint)0);
			s_pendingDeltaX = num2;
			int num3 = s_pendingDeltaY;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+10]");
			int num4 = (int)((nint)num3 + (nint)0);
			s_pendingDeltaY = num4;
			nint num5 = (nint)typeof(HighPollingRateMouseFix);
		}
		else
		{
			bool flag2 = !s_hasLastAbsolute;
			nint num6 = (nint)typeof(HighPollingRateMouseFix);
			IntPtr intPtr = default(IntPtr);
			nint num5 = intPtr;
			if (!flag2)
			{
				object obj3 = s_pendingDeltaX - s_lastAbsolute;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+C]");
				int num7 = obj3 + 0;
				s_pendingDeltaX = num7;
				nint num8 = (nint)typeof(HighPollingRateMouseFix);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ r8_v14 (Il2CppClass<HighPollingRateMouseFix>)+B8]");
				nint num9 = 0;
				int num10 = s_pendingDeltaY;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v61 (Il2CppStaticFields<HighPollingRateMouseFix>)+44]");
				object obj4 = (nint)num10 - (nint)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+10]");
				num5 = (nint)(obj4 + 0);
				s_pendingDeltaY = (int)num5;
				num6 = (nint)typeof(HighPollingRateMouseFix);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+C]");
			s_lastAbsolute = (POINT)0;
			nint num11 = (nint)typeof(HighPollingRateMouseFix);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rax_v55 (Il2CppClass<HighPollingRateMouseFix>)+B8]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+10]");
			_ = 0;
			s_hasLastAbsolute = true;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		nint num13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		int num14 = (int)(num13 ^ 0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		int num15 = (int)((nint)0 & (nint)num14);
		bool flag3 = num15 < 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		bool flag4 = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		if ((nint)0 == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt di,0Ah\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		if ((nint)0 < (nint)0)
		{
			nint num16 = (nint)typeof(HighPollingRateMouseFix);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rcx_v32 (Il2CppClass<HighPollingRateMouseFix>)+E4]");
			nint num17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rcx_v32 (Il2CppClass<HighPollingRateMouseFix>)+E4]");
			object obj5 = num17 ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rcx_v32 (Il2CppClass<HighPollingRateMouseFix>)+E4]");
			object obj6 = 0 & obj5;
			flag3 = (nint)obj6 < 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rcx_v32 (Il2CppClass<HighPollingRateMouseFix>)+E4]");
			flag4 = (nint)0 < (nint)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+6]");
			float num18 = 0f / 120f;
			float num19 = num18 + s_pendingScrollY;
			s_pendingScrollY = num19;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt di,0Bh\"");
		if (flag4 != flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+6]");
			float num20 = 0f / 120f;
			float num21 = num20 + s_pendingScrollX;
			s_pendingScrollX = num21;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		if ((uint)((nuint)0u & (nuint)1u) != 0)
		{
			double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A510]");
			bool flag5 = (nint)0 != 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A510]");
			object obj8 = 0;
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
				object obj9 = default(object);
				obj7 = (object)(&obj9);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [1822070E8h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v474 @ rax_v26 (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A510]");
			ushort num22 = (ushort)(((nint)0 < (nint)0) ? 1 : (s_clickCount + 1));
			s_clickCount = num22;
			s_lastLeftPressTime = realtimeSinceStartupAsDouble;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		ApplyButton(0, 1, 2, MouseButton.Left);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		ApplyButton(0, 4, 8, MouseButton.Right);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		ApplyButton(0, 16, 32, MouseButton.Middle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		ApplyButton(0, 64, 128, MouseButton.Back);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [m @ rcx (RAWMOUSE&)+4]");
		ApplyButton(0, 256, 512, MouseButton.Forward);
		if (s_buttons != s_buttons)
		{
			QueueMouseState(0, 0, 0f, 0f);
		}
	}

	private static void ApplyButton(ushort flags, ushort downFlag, ushort upFlag, MouseButton button)
	{
		//IL_001d: Expected O, but got I4
		//IL_002b: Expected O, but got I4
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected I4, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected I4, but got Unknown
		object obj = button & (MouseButton)0x1F;
		object obj2 = 1 << (int)obj;
		if ((downFlag & flags) != 0)
		{
			int num = obj2 | s_buttons;
			s_buttons = (ushort)num;
		}
		if ((upFlag & flags) != 0)
		{
			object obj3 = ~obj2;
			int num2 = s_buttons & obj3;
			s_buttons = (ushort)num2;
		}
	}

	private static void InjectPerFrameState()
	{
		if (s_suppressionActive)
		{
			QueueMouseState(s_pendingDeltaX, s_pendingDeltaY, s_pendingScrollX, s_pendingScrollY);
			s_pendingDeltaX = 0;
			s_pendingDeltaY = 0;
			s_pendingScrollX = 0f;
			s_pendingScrollY = 0f;
		}
	}

	private unsafe static void QueueMouseState(int deltaX, int deltaY, float scrollX, float scrollY)
	{
		//IL_0043: Expected O, but got Ref
		if (Mouse._003Ccurrent_003Ek__BackingField != null)
		{
			Vector2 vector = ReadCursorClientPosition();
			object obj = default(object);
			InputSystem.QueueStateEvent(Mouse._003Ccurrent_003Ek__BackingField, (MouseState)(&obj));
		}
	}

	private static Vector2 ReadCursorClientPosition()
	{
		//IL_0015: Expected O, but got I
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_02d7: Expected O, but got I
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_030f: Expected O, but got I
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_026e: Expected O, but got I
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0165: Expected O, but got I
		//IL_0347: Expected O, but got I
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Expected O, but got Unknown
		//IL_0196: Expected O, but got I4
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Expected O, but got Unknown
		//IL_01a4: Expected O, but got I4
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4F0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4F0]");
		object obj3 = default(object);
		if ((nint)0 == 0)
		{
			_ = 6478131240L;
			object obj2 = obj3 - 48;
			_ = 6478131280L;
			_ = 10;
			_ = 12;
			_ = 0;
			_ = 2;
			_ = 8;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
		}
		object obj4 = obj3 + 40;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v66 @ rax_v4 (should have been resolved before IL gen)");
		object obj5 = default(object);
		Vector2 result = default(Vector2);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4F8]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A4F8]");
			if ((nint)0 == 0)
			{
				_ = 6478131240L;
				object obj7 = obj3 - 48;
				_ = 6478131296L;
				_ = 10;
				_ = 14;
				_ = 0;
				_ = 2;
				_ = 16;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
			}
			object obj8 = obj3 + 40;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v188 @ rax_v24 (should have been resolved before IL gen)");
			object obj9 = default(object);
			if (obj9 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A500]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A500]");
				if ((nint)0 == 0)
				{
					_ = 6478131240L;
					object obj11 = obj3 - 48;
					_ = 6478131312L;
					_ = 10;
					_ = 13;
					_ = 0;
					_ = 2;
					_ = 16;
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7670");
				}
				object obj12 = obj3 - 64;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v368 @ rax_v30 (should have been resolved before IL gen)");
				object obj13 = default(object);
				if (obj13 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-38]");
					object obj14 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-38]");
					if ((nint)0 < (nint)1)
					{
						obj14 = 1;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-34]");
					object obj15 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-34]");
					if ((nint)0 < (nint)1)
					{
						obj15 = 1;
					}
					object obj16 = obj14 - 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
					bool flag = (nint)0 < (nint)0;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
						object obj17 = 0 - obj16;
						flag = (nint)obj17 < 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
						if (0 <= (nint)obj16)
						{
						}
					}
					object obj18 = obj15 - 1;
					object obj19 = obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+2C]");
					object obj20 = obj19 - 0;
					object obj21 = obj20 - 1;
					if (flag || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj21) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18))
					{
					}
					return result;
				}
			}
		}
		if (Mouse._003Ccurrent_003Ek__BackingField != null)
		{
			Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
			if (Mouse._003Ccurrent_003Ek__BackingField != null && ((Pointer)mouse)._003Cposition_003Ek__BackingField != null)
			{
				object obj22 = obj3 + 48;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18088D950");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+30]");
				return (Vector2)0;
			}
			return (Vector2)new NullReferenceException();
		}
		return result;
	}

	private static void ReleaseAllButtons()
	{
		s_hasLastAbsolute = false;
		if (s_buttons != 0)
		{
			s_buttons = 0;
			QueueMouseState(0, 0, 0f, 0f);
		}
	}

	private static void ResetAccumulators()
	{
		s_pendingDeltaX = 0;
		s_pendingDeltaY = 0;
		s_pendingScrollX = 0f;
		s_pendingScrollY = 0f;
	}
}
