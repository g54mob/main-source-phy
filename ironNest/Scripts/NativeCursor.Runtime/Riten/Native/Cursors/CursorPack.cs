using UnityEngine;

namespace Riten.Native.Cursors
{
	[CreateAssetMenu(fileName = "CursorPack", menuName = "Native Cursor/Cursor Pack")]
	public class CursorPack : ScriptableObject
	{
		public VirtualCursorBase @default;

		public VirtualCursorBase pointer;

		public VirtualCursorBase ibeam;

		public VirtualCursorBase wait;

		public VirtualCursorBase cross;

		[Space]
		public VirtualCursorBase grab;

		public VirtualCursorBase grabbing;

		public VirtualCursorBase denied;

		[Space]
		public VirtualCursorBase move;

		public VirtualCursorBase resizeHorizontal;

		public VirtualCursorBase resizeVertical;

		public VirtualCursorBase resizeDiagonal1;

		public VirtualCursorBase resizeDiagonal2;

		public VirtualCursorBase GetCursor(NTCursors ntCursor)
		{
			return null;
		}
	}
}
