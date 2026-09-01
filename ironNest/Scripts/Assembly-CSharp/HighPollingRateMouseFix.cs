using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[DefaultExecutionOrder(-32000)]
[DisallowMultipleComponent]
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

	[Header("Activation")]
	[Tooltip("Only suppress legacy input in fullscreen/borderless modes. Recommended: with suppression active in a decorated window, the title bar (drag/resize/close) stops responding.")]
	[SerializeField]
	private bool onlyActivateInFullscreen;

	[Tooltip("Master switch. Expose this as a user-facing graphics/input option if desired.")]
	[SerializeField]
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

	private static double s_lastLeftPressTime;

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

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void OnDestroy()
	{
	}

	private static void SetSuppression(bool active)
	{
	}

	[MonoPInvokeCallback(typeof(WndProc))]
	private static IntPtr WndProcHook(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		return (IntPtr)0;
	}

	private static void ProcessRawMouse(in RAWMOUSE m)
	{
	}

	private static void ApplyButton(ushort flags, ushort downFlag, ushort upFlag, MouseButton button)
	{
	}

	private static void InjectPerFrameState()
	{
	}

	private static void QueueMouseState(int deltaX, int deltaY, float scrollX, float scrollY)
	{
	}

	private static Vector2 ReadCursorClientPosition()
	{
		return default(Vector2);
	}

	private static void ReleaseAllButtons()
	{
	}

	private static void ResetAccumulators()
	{
	}
}
