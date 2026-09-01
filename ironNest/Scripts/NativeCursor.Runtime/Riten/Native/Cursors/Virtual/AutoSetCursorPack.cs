using UnityEngine;

namespace Riten.Native.Cursors.Virtual
{
	public class AutoSetCursorPack : MonoBehaviour
	{
		[SerializeField]
		private CursorPack _cursorPack;

		[SerializeField]
		private Camera _camera;

		private CursorPack _lastActivated;

		private void OnEnable()
		{
		}

		private void OnValidate()
		{
		}

		private void OnDisable()
		{
		}
	}
}
