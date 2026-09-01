using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace InputGlyphs.Utils
{
	public static class InputLayoutPathUtility
	{
		private static StringBuilder _stringBuilder;

		private static List<int> _bindingIndexBuffer;

		public static string RemoveRoot(string inputControlPath)
		{
			return null;
		}

		public static string GetParent(string inputLayoutPath)
		{
			return null;
		}

		public static bool TryGetActionBindingPath(InputAction action, string controlScheme, List<string> results)
		{
			return false;
		}

		public static bool HasPathComponent(string path)
		{
			return false;
		}
	}
}
