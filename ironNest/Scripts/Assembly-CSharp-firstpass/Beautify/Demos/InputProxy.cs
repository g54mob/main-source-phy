using UnityEngine;

namespace Beautify.Demos
{
	public static class InputProxy
	{
		public static bool GetKeyDown(KeyCode keyCode)
		{
			return false;
		}

		public static bool GetMouseButtonDown(int button)
		{
			return false;
		}
	}
}
