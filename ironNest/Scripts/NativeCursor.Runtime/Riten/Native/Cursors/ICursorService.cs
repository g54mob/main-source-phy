namespace Riten.Native.Cursors
{
	public interface ICursorService
	{
		bool SetCursor(NTCursors ntCursor);

		void ResetCursor();
	}
}
