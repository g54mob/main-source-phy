using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.Utils
{
	public class CursorHelper
	{
		private object m_lock;

		private Texture2D m_texture;

		private readonly Dictionary<KnownCursor, Texture2D> m_knownCursorToTexture = new Dictionary<KnownCursor, Texture2D>();

		private Texture2D m_defaultCursorTexture;

		private Vector2 m_defaultCursorHotspot;

		public Texture2D DefaultCursorTexture => m_defaultCursorTexture;

		public Vector2 DefaultCursorHotspot => m_defaultCursorHotspot;

		[Obsolete("Renamed to SetCursorTexture")]
		public void Map(KnownCursor cursorType, Texture2D texture)
		{
			SetCursorTexture(cursorType, texture);
		}

		[Obsolete("Renamed to ClearCursorTextures")]
		public void Reset()
		{
			ClearCursorTextures();
		}

		public void SetCursorTexture(KnownCursor cursorType, Texture2D texture)
		{
			m_knownCursorToTexture[cursorType] = texture;
		}

		public void ClearCursorTextures()
		{
			m_knownCursorToTexture.Clear();
		}

		public void SetDefaultCursor(Texture2D texture, Vector2 hotspot)
		{
			m_defaultCursorTexture = texture;
			m_defaultCursorHotspot = hotspot;
			ResetCursor(null);
		}

		public bool SetCursor(object locker, KnownCursor cursorType)
		{
			return SetCursor(locker, cursorType, new Vector2(0.5f, 0.5f), CursorMode.Auto);
		}

		public bool SetCursor(object locker, KnownCursor cursorType, Vector2 hotspot, CursorMode mode)
		{
			if (!m_knownCursorToTexture.TryGetValue(cursorType, out var value))
			{
				value = null;
			}
			return SetCursor(locker, value, hotspot, mode);
		}

		public bool SetCursor(object locker, Texture2D texture)
		{
			return SetCursor(locker, texture, new Vector2(0.5f, 0.5f), CursorMode.Auto);
		}

		public bool SetCursor(object locker, Texture2D texture, Vector2 hotspot, CursorMode mode)
		{
			if (m_lock != null && m_lock != locker)
			{
				return false;
			}
			if (texture != null)
			{
				hotspot = new Vector2((float)texture.width * hotspot.x, (float)texture.height * hotspot.y);
			}
			else
			{
				texture = DefaultCursorTexture;
				if (texture != null)
				{
					hotspot = new Vector2((float)texture.width * DefaultCursorHotspot.x, (float)texture.height * DefaultCursorHotspot.y);
				}
			}
			m_lock = locker;
			if (m_texture != texture)
			{
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				Cursor.SetCursor(texture, hotspot, mode);
				m_texture = texture;
				return true;
			}
			return false;
		}

		public void ResetCursor(object locker)
		{
			if (m_lock == locker)
			{
				m_lock = null;
				SetCursor(null, DefaultCursorTexture, DefaultCursorHotspot, CursorMode.Auto);
			}
		}
	}
}
