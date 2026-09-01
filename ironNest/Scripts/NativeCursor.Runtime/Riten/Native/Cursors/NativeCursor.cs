using Riten.Native.Cursors.Virtual;
using UnityEngine;

namespace Riten.Native.Cursors
{
	public static class NativeCursor
	{
		private static ICursorService _instance;

		private static ICursorService _defaultService;

		private static VirtualCursorService _vcs;

		public static string ServiceName => null;

		public static void SetFallbackService(ICursorService service)
		{
		}

		public static void SetService(ICursorService service)
		{
		}

		public static bool SetCursor(NTCursors ntCursor)
		{
			return false;
		}

		public static void SetCursorPack(CursorPack cursorPack, Camera cmr)
		{
		}

		public static void SetCursorPackCamera(Camera cmr)
		{
		}

		public static void ClearCursorPack()
		{
		}

		public static void ResetCursor()
		{
		}
	}
}
