using InputGlyphs.Loaders.Utils;
using UnityEngine;

namespace InputGlyphs.Loaders
{
	public class GamepadGlyphInitializer : MonoBehaviour
	{
		[SerializeField]
		private InputGlyphTextureMap _fallbackTextureMap;

		[SerializeField]
		private InputGlyphTextureMap _xboxTextureMap;

		[SerializeField]
		private InputGlyphTextureMap _playstationTextureMap;

		[SerializeField]
		private InputGlyphTextureMap _switchProControllerTextureMap;

		private static bool _initialized;

		private void Awake()
		{
		}
	}
}
