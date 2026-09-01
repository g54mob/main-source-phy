using UnityEngine;

namespace InputGlyphs.Loaders.Utils
{
	public class DeviceGlyphLoaderInitializer<T> : MonoBehaviour
	{
		[SerializeField]
		public InputGlyphTextureMap[] TextureMaps;

		private static bool _initialized;

		private void Awake()
		{
		}
	}
}
