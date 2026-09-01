using System;
using System.IO;
using DM;
using TFBGames;
using UnityEngine;
using UnityEngine.U2D;

namespace Landfall.TABS.Workshop
{
	public class CustomFactionHandler
	{
		public static void SaveFaction(Faction customFaction, DatabaseID id = default(DatabaseID), Action doneRefreshCallback = null)
		{
			try
			{
				if (id == default(DatabaseID))
				{
					customFaction.Entity.GenerateNewID();
				}
				else
				{
					customFaction.Entity.GUID = id;
				}
				string guid = customFaction.Entity.GUID.ToString();
				Faction.GetCustomFactionPathAsync(customFaction, delegate(string path)
				{
					if (path != null)
					{
						Path.Combine(CustomContentFilePaths.FilePathFaction, guid, "icon.png");
						string contents = JsonUtility.ToJson(customFaction.Serialize(), prettyPrint: true);
						ServiceLocator.GetService<FileIOWrapper>().WriteAllText(path, contents, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
						{
							ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Faction, doneRefreshCallback);
						});
					}
					else
					{
						Debug.LogError("Null factionpath was returned. Cannot process SaveFaction.");
						doneRefreshCallback?.Invoke();
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Saving faction process failed with error " + ex.Message);
				doneRefreshCallback?.Invoke();
			}
		}

		public static Texture2D GetFactionIcon(string factionName, SpriteAtlas factionAtlas)
		{
			Faction factionByName = ContentDatabase.Instance().GetFactionByName(factionName);
			Texture2D texture = factionByName.FactionIcon.Entity.SpriteIcon.texture;
			if (texture == null)
			{
				return null;
			}
			Texture2D texture2D = null;
			Color[] array = null;
			Sprite sprite = factionAtlas?.GetSprite(factionByName.FactionIcon.Entity.SpriteIcon.name);
			if (sprite != null)
			{
				Rect textureRect = sprite.textureRect;
				int num = (int)textureRect.width;
				int num2 = (int)textureRect.height;
				int x = (int)textureRect.x;
				int y = (int)textureRect.y;
				texture2D = new Texture2D(num, num2);
				array = texture.GetPixels(x, y, num, num2);
			}
			else
			{
				texture2D = new Texture2D(texture.width, texture.height);
				array = texture.GetPixels();
			}
			if (array == null || texture2D == null)
			{
				return null;
			}
			texture2D.SetPixels(array);
			texture2D.Apply();
			return texture2D;
		}

		public static Texture2D GetColoredFactionIcon(string factionName, SpriteAtlas factionAtlas)
		{
			Texture2D factionIcon = GetFactionIcon(factionName, factionAtlas);
			Color factionColor = GetFactionColor(factionName);
			Color[] pixels = factionIcon.GetPixels();
			for (int i = 0; i < pixels.Length; i++)
			{
				Color color = pixels[i];
				if (color == Color.black || color.a <= 0.25f || color.grayscale <= 0.25f)
				{
					pixels[i] = factionColor;
				}
			}
			factionIcon.SetPixels(pixels);
			factionIcon.Apply();
			return factionIcon;
		}

		public static Color GetFactionColor(string factionName)
		{
			Faction factionByName = ContentDatabase.Instance().GetFactionByName(factionName);
			if (factionByName == null)
			{
				return Color.white;
			}
			return factionByName.CustomFactionColor.m_Color;
		}

		public static void GetLoadedFactionFromDisk(string fileName, Action<Faction> doneCallback)
		{
			FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
			try
			{
				fileIO.FileExists(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
				{
					if (!exists)
					{
						Debug.LogErrorFormat("Could not find file: {0}", fileName);
						doneCallback?.Invoke(null);
					}
					else if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.InvariantCultureIgnoreCase))
					{
						doneCallback?.Invoke(null);
					}
					else
					{
						fileIO.DeserializeWithBinaryFormatter<UnitBlueprint>(fileName, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(object deserializedObject, Exception exception)
						{
							if (deserializedObject != null)
							{
								doneCallback?.Invoke((Faction)deserializedObject);
							}
							else
							{
								Debug.LogErrorFormat("Failed to load: {0}\n{1}", fileName, exception);
								doneCallback?.Invoke(null);
							}
						});
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to Load Faction from disk with exception " + ex.Message);
				doneCallback?.Invoke(null);
			}
		}
	}
}
