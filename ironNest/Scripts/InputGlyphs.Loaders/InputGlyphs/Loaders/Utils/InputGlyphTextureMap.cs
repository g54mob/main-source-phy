using System;
using UnityEngine;

namespace InputGlyphs.Loaders.Utils
{
	[CreateAssetMenu(fileName = "InputGlyphTextureMap", menuName = "InputGlyphs/InputGlyphTextureMap")]
	public class InputGlyphTextureMap : ScriptableObject
	{
		[Serializable]
		public class TextureDetail
		{
			[SerializeField]
			public string InputLayoutLocalPath;

			[SerializeField]
			public Texture2D GlyphTexture;
		}

		[SerializeField]
		public TextureDetail[] TextureDetails;

		public bool TryGetTexture(string inputLayoutLocalPath, out Texture2D texture)
		{
			texture = null;
			return false;
		}
	}
}
