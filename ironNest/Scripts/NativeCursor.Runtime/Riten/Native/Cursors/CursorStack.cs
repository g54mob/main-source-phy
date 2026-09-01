using System.Collections.Generic;
using UnityEngine;

namespace Riten.Native.Cursors
{
	public static class CursorStack
	{
		private static int _nextUid;

		private static bool _paused;

		private static readonly List<CursorStackItem> _stack;

		public static bool IsEmpty => false;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Setup()
		{
		}

		private static void OnStackChanged(bool force = false)
		{
		}

		public static void OnDebugGUI()
		{
		}

		public static void PauseRendering(bool isPaused)
		{
		}

		public static void ReApply()
		{
		}

		public static int Push(NTCursors cursor, int priority = 0, int secondaryPriority = 0)
		{
			return 0;
		}

		public static bool Pop()
		{
			return false;
		}

		public static bool Pop(int id)
		{
			return false;
		}

		public static void Clear()
		{
		}

		public static CursorStackItem Peek()
		{
			return default(CursorStackItem);
		}

		public static bool Replace(int id, NTCursors cursor)
		{
			return false;
		}
	}
}
