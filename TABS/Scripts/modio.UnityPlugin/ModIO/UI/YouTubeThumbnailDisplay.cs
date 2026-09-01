using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class YouTubeThumbnailDisplay : MonoBehaviour
	{
		[Serializable]
		public class TextureChangedEvent : UnityEvent<Texture2D>
		{
		}

		public Image image;

		public TextureChangedEvent onTextureChanged;

		private int m_modId;

		private string m_youTubeId = string.Empty;

		public int ModId => m_modId;

		public string YouTubeId => m_youTubeId;

		public virtual void DisplayThumbnail(int modId, string youTubeId)
		{
			m_modId = modId;
			if (!(m_youTubeId != youTubeId))
			{
				return;
			}
			m_youTubeId = youTubeId;
			image.sprite = null;
			image.enabled = false;
			if (onTextureChanged != null)
			{
				onTextureChanged.Invoke(null);
			}
			if (!string.IsNullOrEmpty(youTubeId))
			{
				Action<Texture2D> onThumbnailReceived = delegate(Texture2D t)
				{
					ApplyTexture(youTubeId, t);
				};
				ImageRequestManager.instance.RequestYouTubeThumbnail(modId, youTubeId, onThumbnailReceived, null);
			}
		}

		protected virtual void ApplyTexture(string youTubeId, Texture2D texture)
		{
			if (this != null && texture != null && m_youTubeId == youTubeId)
			{
				image.sprite = UIUtilities.CreateSpriteFromTexture(texture);
				image.enabled = true;
				if (onTextureChanged != null)
				{
					onTextureChanged.Invoke(texture);
				}
			}
		}

		public virtual void OpenVideoInBrowser()
		{
			if (!string.IsNullOrEmpty(m_youTubeId))
			{
				UIUtilities.OpenYouTubeVideoURL(m_youTubeId);
			}
		}
	}
}
