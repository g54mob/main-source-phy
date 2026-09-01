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

		public RawImage image;

		private Texture current;

		public TextureChangedEvent onTextureChanged;

		private int m_modId;

		private string m_youTubeId = string.Empty;

		public int ModId
		{
			get
			{
				return m_modId;
			}
		}

		public string YouTubeId
		{
			get
			{
				return m_youTubeId;
			}
		}

		public virtual void DisplayThumbnail(int modId, string youTubeId)
		{
			m_modId = modId;
			if (!(m_youTubeId != youTubeId))
			{
				return;
			}
			m_youTubeId = youTubeId;
			OnDestroy();
			image.texture = null;
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
				if (texture.GetRawTextureData().Length > 0)
				{
					OnDestroy();
					current = texture;
					image.texture = texture;
					image.enabled = true;
					if (onTextureChanged != null)
					{
						onTextureChanged.Invoke(texture);
					}
					return;
				}
				Debug.LogError("[YouTubeThumbnailDisplay] Error: Zero length texture");
				if (onTextureChanged != null)
				{
					onTextureChanged.Invoke(texture);
				}
			}
			OnDestroy();
			image.enabled = false;
		}

		public virtual void OpenVideoInBrowser()
		{
			if (!string.IsNullOrEmpty(m_youTubeId))
			{
				UIUtilities.OpenYouTubeVideoURL(m_youTubeId);
			}
		}

		private void OnDestroy()
		{
			if ((bool)current)
			{
				UnityEngine.Object.DestroyImmediate(current);
			}
			current = null;
		}
	}
}
