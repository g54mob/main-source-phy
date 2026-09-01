using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGlyphs.Loaders.Utils
{
	public class DeviceGlyphLoader<T> : IInputGlyphLoader
	{
		public readonly List<InputGlyphTextureMap> TextureMaps;

		public bool LoadGlyph(Texture2D texture, IReadOnlyList<InputDevice> activeDevices, string inputLayoutPath)
		{
			return false;
		}
	}
}
