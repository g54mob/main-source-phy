using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Riten.Native.Cursors
{
	internal class WindowsCursorPatch : MonoBehaviour, ICursorService
	{
		private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		private const uint IDC_ARROW = 32512u;

		private const uint IDC_IBEAM = 32513u;

		private const uint IDC_WAIT = 32650u;

		private const uint IDC_CROSS = 32515u;

		private const uint IDC_SIZENWSE = 32642u;

		private const uint IDC_SIZENESW = 32643u;

		private const uint IDC_SIZEWE = 32644u;

		private const uint IDC_SIZENS = 32645u;

		private const uint IDC_SIZEALL = 32646u;

		private const uint IDC_NO = 32648u;

		private const uint IDC_HAND = 32649u;

		private static HandleRef hMainWindow;

		private static IntPtr unityWndProcHandler;

		private static IntPtr customWndProcHandler;

		private static WndProcDelegate procDelegate;

		private const int GWLP_WNDPROC = -4;

		private const uint WM_SETCURSOR = 32u;

		private const uint WM_MOUSEMOVE = 512u;

		private static IntPtr cursorHandle;

		private static readonly Dictionary<NTCursors, IntPtr> _cursors;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Setup()
		{
		}

		[PreserveSig]
		private static extern IntPtr SetCursor(IntPtr hCursor);

		[PreserveSig]
		private static extern IntPtr LoadCursor(IntPtr hInstance, uint lpCursorName);

		[PreserveSig]
		private static extern IntPtr GetActiveWindow();

		[PreserveSig]
		private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		[PreserveSig]
		private static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		[PreserveSig]
		private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

		[PreserveSig]
		private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		[MonoPInvokeCallback(typeof(WndProcDelegate))]
		private static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
		{
			return (IntPtr)0;
		}

		private static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
		{
			return (IntPtr)0;
		}

		private static IntPtr GetCursor(NTCursors nativeCursorName)
		{
			return (IntPtr)0;
		}

		public bool SetCursor(NTCursors nativeCursorName)
		{
			return false;
		}

		public void ResetCursor()
		{
		}
	}
}
