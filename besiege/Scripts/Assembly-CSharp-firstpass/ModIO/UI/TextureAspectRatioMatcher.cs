using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class TextureAspectRatioMatcher : AspectRatioFitter
	{
		protected override void OnEnable()
		{
			base.OnEnable();
			SetDirty();
		}

		public void MatchTexture(Texture2D texture)
		{
			if (texture != null)
			{
				base.aspectRatio = (float)texture.width / (float)texture.height;
			}
		}
	}
}
