using System;
using System.IO;
using TFBGames;
using UnityEngine;

namespace DM
{
	public class CacheUtil
	{
		private static Vector2Int? m_unitLargeIconSize = new Vector2Int(512, 512);

		private static Vector2Int? m_unitSmallIconSize = new Vector2Int(64, 64);

		private static Vector2Int? m_levelPreviewIconSize = new Vector2Int(512, 256);

		public static Vector2Int ToPowersOfTwo(string path, Vector2Int size)
		{
			return TextureUtil.ToPowersOfTwo(size);
		}

		private static Sprite TryLoadSpriteResource(string resourcePath, Vector2Int? size)
		{
			Texture2D texture2D = Resources.Load<Texture2D>(resourcePath);
			if (texture2D == null)
			{
				return null;
			}
			return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
		}

		private static void TryLoadSpriteAsync(string path, Vector2Int? size, Action<Sprite> doneCallBack)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			try
			{
				fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
				{
					if (exists)
					{
						fileIO.ReadAllBytes(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] result, Exception exception)
						{
							if (exception != null)
							{
								doneCallBack?.Invoke(null);
							}
							else
							{
								Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: false, linear: false);
								tex.LoadImage(result);
								doneCallBack?.Invoke(CreateSpriteFromTextureData(path, size, tex));
							}
						});
					}
					else
					{
						doneCallBack?.Invoke(null);
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Reading sprite data at path " + path + " failed with error: " + ex.Message);
				doneCallBack?.Invoke(null);
			}
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		private static Sprite TryLoadSprite(string path, Vector2Int? size)
		{
			if (File.Exists(path))
			{
				Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: false, linear: false);
				tex.LoadImage(File.ReadAllBytes(path));
				return CreateSpriteFromTextureData(path, size, tex);
			}
			return null;
		}

		private static Sprite CreateSpriteFromTextureData(string path, Vector2Int? size, Texture2D tex)
		{
			Vector2Int vector2Int = new Vector2Int(tex.width, tex.height);
			Vector2Int vector2Int2 = (size.HasValue ? size.Value : ToPowersOfTwo(path, vector2Int));
			if (vector2Int != vector2Int2)
			{
				TextureScale.Point(tex, vector2Int2.x, vector2Int2.y);
			}
			tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
			if (tex.width > 512 || tex.height > 512)
			{
				Debug.LogWarningFormat("Loaded (probably) excessively large texture \"{0}\" -> width: {1}, height: {2}, format: {3}, dim: {4}, readable: {5}, mipcount: {6}, name: \"{7}\"", path, tex.width, tex.height, tex.graphicsFormat, tex.dimension, tex.isReadable, tex.mipmapCount, string.IsNullOrEmpty(tex.name) ? "N/A" : tex.name);
			}
			tex.name = "CACHE: " + path;
			return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
		}

		private static Sprite LoadFallbackSprite(string path, Vector2Int? size)
		{
			Debug.LogWarningFormat("Creating a fallback texture for {0}, {1}", path, size);
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: false, linear: false);
			return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
		}

		private static string BuildSpriteURI(string path, Vector2Int? size)
		{
			if (size.HasValue)
			{
				return string.Concat(path, "?size={", size.Value, "}");
			}
			return path;
		}

		public static void GetSpriteAsync(string path, Vector2Int? size, Action<Sprite> doneCallBack)
		{
			Vector2Int? adjustedSize = (size.HasValue ? new Vector2Int?(ToPowersOfTwo(path, size.Value)) : size);
			BuildSpriteURI(path, adjustedSize);
			Sprite sprite = TryLoadSpriteResource(path, adjustedSize);
			if (sprite == null)
			{
				TryLoadSpriteAsync(path, adjustedSize, delegate(Sprite loadedSprite)
				{
					sprite = loadedSprite;
					if (sprite == null)
					{
						sprite = LoadFallbackSprite(path, adjustedSize);
					}
					doneCallBack?.Invoke(sprite);
				});
			}
			else
			{
				doneCallBack?.Invoke(sprite);
			}
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		public static Sprite GetSprite(string path, Vector2Int? size)
		{
			Vector2Int? adjustedSize = (size.HasValue ? new Vector2Int?(ToPowersOfTwo(path, size.Value)) : size);
			string uri = BuildSpriteURI(path, adjustedSize);
			return (Sprite)Cache.Instance().GetObject(uri, delegate
			{
				Sprite sprite = TryLoadSpriteResource(path, adjustedSize);
				if (sprite == null)
				{
					sprite = TryLoadSprite(path, adjustedSize);
				}
				if (sprite == null)
				{
					sprite = LoadFallbackSprite(path, adjustedSize);
				}
				if (sprite == null)
				{
					throw new Exception("Failed to load sprite " + path);
				}
				int estimatedSize = sprite.texture.width * sprite.texture.height * 2;
				return new AssetObject
				{
					obj = sprite,
					assetInfo = new AssetInfo
					{
						name = uri,
						estimatedSize = estimatedSize
					}
				};
			});
		}

		public static void GetSpriteIconAsync(Sprite primarySprite, string path, Action<Sprite> doneCallBack, Vector2Int? size = null)
		{
			if (primarySprite != null)
			{
				doneCallBack?.Invoke(primarySprite);
				return;
			}
			if (!string.IsNullOrEmpty(path))
			{
				GetSpriteAsync(path, size, doneCallBack);
				return;
			}
			Debug.LogError("Sprite path was null or empty.");
			doneCallBack?.Invoke(null);
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		public static Sprite GetSpriteIcon(Sprite primarySprite, string path, Vector2Int? size = null)
		{
			if ((bool)primarySprite)
			{
				return primarySprite;
			}
			if (path != "")
			{
				return GetSprite(path, size);
			}
			return null;
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		public static Sprite GetUnitSpriteLargeIcon(Sprite primarySprite, string path)
		{
			return GetSpriteIcon(primarySprite, path, m_unitLargeIconSize);
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		public static Sprite GetUnitSpriteSmallIcon(Sprite primarySprite, string path)
		{
			return GetSpriteIcon(primarySprite, path, m_unitSmallIconSize);
		}

		[Obsolete("This method is obsolete, please use the async version.", false)]
		public static Sprite GetLevelCellIcon(Sprite primarySprite, string path)
		{
			return GetSpriteIcon(primarySprite, path, m_levelPreviewIconSize);
		}

		public static void GetUnitSpriteLargeIconAsync(Sprite primarySprite, string path, Action<Sprite> doneCallBack)
		{
			GetSpriteIconAsync(primarySprite, path, doneCallBack, m_unitLargeIconSize);
		}

		public static void GetUnitSpriteSmallIconAsync(Sprite primarySprite, string path, Action<Sprite> doneCallBack)
		{
			GetSpriteIconAsync(primarySprite, path, doneCallBack, m_unitSmallIconSize);
		}

		public static void GetLevelCellIconAsync(Sprite primarySprite, string path, Action<Sprite> doneCallBack)
		{
			GetSpriteIconAsync(primarySprite, path, doneCallBack, m_levelPreviewIconSize);
		}

		public static void InvalidateSprite(string path)
		{
			Cache.Instance().InvalidateEntry(path);
		}
	}
}
