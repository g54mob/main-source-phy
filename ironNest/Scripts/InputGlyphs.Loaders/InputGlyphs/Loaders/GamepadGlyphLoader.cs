using System.Collections.Generic;
using InputGlyphs.Loaders.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs.Loaders
{
	public class GamepadGlyphLoader : IInputGlyphLoader
	{
		private readonly InputGlyphTextureMap _fallbackTextureMap;

		private readonly InputGlyphTextureMap _xboxControllerTextureMap;

		private readonly InputGlyphTextureMap _playstationControllerTextureMap;

		private readonly InputGlyphTextureMap _switchProControllerTextureMap;

		public GamepadGlyphLoader(InputGlyphTextureMap fallbackTextureMap, InputGlyphTextureMap xboxControllerTextureMap, InputGlyphTextureMap playstationControllerTextureMap, InputGlyphTextureMap switchProControllerTextureMap)
		{
		}

		public bool LoadGlyph(Texture2D texture, IReadOnlyList<InputDevice> activeDevices, string inputLayoutPath)
		{
			return false;
		}

		private InputGlyphTextureMap GetTextureMap(InputDevice device)
		{
			return null;
		}
	}
}
