using UnityEngine;

namespace Riten.Native.Cursors.Virtual
{
	public class VirtualCursorService : MonoBehaviour, ICursorService
	{
		private CursorPack _cursorPack;

		private VirtualCursorBase _activeCursor;

		private Camera _camera;

		private int _lastFrame;

		private int _frame;

		private float _fps;

		private Texture2D _screenTexture;

		private Texture2D _maskTexture;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnPostRenderCb(Camera cmr)
		{
		}

		public void UpdatePack(CursorPack pack, Camera cmr)
		{
		}

		public bool SetCursor(NTCursors ntCursor)
		{
			return false;
		}

		public void ResetCursor()
		{
		}

		private void Update()
		{
		}

		private void DoCursorUpdate()
		{
		}

		private void CaptureScreen()
		{
		}

		private void DoMaskedPostProcess()
		{
		}

		public void SetCamera(Camera cmr)
		{
		}
	}
}
