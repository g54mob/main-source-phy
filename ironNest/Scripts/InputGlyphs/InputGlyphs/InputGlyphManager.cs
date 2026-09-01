using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs
{
	public static class InputGlyphManager
	{
		private static List<IInputGlyphLoader> _loaders;

		public static void RegisterLoader(IInputGlyphLoader loader)
		{
		}

		public static void UnregisterLoader(IInputGlyphLoader loader)
		{
		}

		public static bool LoadGlyph(Texture2D texture, IReadOnlyList<InputDevice> activeDevices, string inputLayoutPath, out string usedPath)
		{
			usedPath = null;
			return false;
		}
	}
}
