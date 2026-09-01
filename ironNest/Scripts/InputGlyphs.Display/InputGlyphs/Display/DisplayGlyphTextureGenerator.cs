using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs.Display
{
	public static class DisplayGlyphTextureGenerator
	{
		private static List<Texture2D> _textureBuffer;

		private static List<string> _usedPathBuffer;

		public static bool GenerateGlyphTexture(Texture2D texture, IReadOnlyList<InputDevice> activeDevices, IReadOnlyList<string> inputLayoutPaths, GlyphsLayoutData layoutData)
		{
			return false;
		}

		private static bool GenerateSingleGlyphTexture(Texture2D texture, IReadOnlyList<InputDevice> activeDevices, IReadOnlyList<string> inputLayoutPaths, int index)
		{
			return false;
		}

		private static bool GenerateMultipleGlyphsTexture(Texture2D texture, IReadOnlyList<InputDevice> activeDevices, IReadOnlyList<string> inputLayoutPaths, int maxCount)
		{
			return false;
		}
	}
}
