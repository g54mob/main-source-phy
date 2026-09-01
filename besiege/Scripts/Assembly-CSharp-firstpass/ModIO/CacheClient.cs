using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModIO
{
	public static class CacheClient
	{
		public static string gameProfileFilePath
		{
			get
			{
				return IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "game_profile.data");
			}
		}

		[Obsolete("Use DataStorage.CACHE_DIRECTORY instead")]
		public static string cacheDirectory
		{
			get
			{
				return DataStorage.CACHE_DIRECTORY;
			}
		}

		public static void SaveGameProfile(GameProfile profile, Action<bool> onComplete)
		{
			DataStorage.WriteJSONFile(gameProfileFilePath, profile, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadGameProfile(Action<GameProfile> onComplete)
		{
			DataStorage.ReadJSONFile(gameProfileFilePath, delegate(string p, bool success, GameProfile data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static string GenerateModDirectoryPath(int modId)
		{
			return IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "mods", modId.ToString());
		}

		public static string GenerateModProfileFilePath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModDirectoryPath(modId), "profile.data");
		}

		public static void SaveModProfile(ModProfile profile, Action<bool> onComplete)
		{
			string path = GenerateModProfileFilePath(profile.id);
			DataStorage.WriteJSONFile(path, profile, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModProfile(int modId, Action<ModProfile> onComplete)
		{
			string path = GenerateModProfileFilePath(modId);
			DataStorage.ReadJSONFile(path, delegate(string p, bool success, ModProfile data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static void SaveModProfiles(IEnumerable<ModProfile> modProfiles, Action<bool> onComplete)
		{
			bool success = true;
			List<ModProfile> profiles = new List<ModProfile>(modProfiles);
			Action writeNextProfile = null;
			writeNextProfile = delegate
			{
				if (profiles.Count > 0)
				{
					int index = profiles.Count - 1;
					ModProfile modProfile = profiles[index];
					string path = GenerateModProfileFilePath(modProfile.id);
					profiles.RemoveAt(index);
					if (modProfile != null)
					{
						DataStorage.WriteJSONFile(path, modProfile, delegate(string p, bool s)
						{
							success &= s;
							writeNextProfile();
						});
					}
					else
					{
						writeNextProfile();
					}
				}
				else if (onComplete != null)
				{
					onComplete(success);
				}
			};
			writeNextProfile();
		}

		public static void RequestAllModProfiles(Action<IList<ModProfile>> onComplete)
		{
			RequestAllModProfilesFromOffset(0, onComplete);
		}

		public static void RequestAllModProfilesFromOffset(int offset, Action<IList<ModProfile>> onComplete)
		{
			List<string> profilePaths = new List<string>();
			List<ModProfile> modProfiles = new List<ModProfile>();
			string profileDirectory = IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "mods");
			DataStorage.GetDirectories(profileDirectory, delegate(string gd_path, bool gd_success, IList<string> modDirectories)
			{
				if (gd_success)
				{
					if (modDirectories == null)
					{
						modDirectories = new string[0];
					}
					else if (modDirectories.Count - offset > 0)
					{
						for (int i = offset; i < modDirectories.Count; i++)
						{
							string item = IOUtilities.CombinePath(modDirectories[i], "profile.data");
							profilePaths.Add(item);
						}
					}
				}
				else
				{
					string message = "[mod.io] Failed to read mod profile directory.\nDirectory: " + profileDirectory;
					Debug.LogWarning(message);
					modDirectories = new string[0];
				}
				Action loadNextProfile = null;
				loadNextProfile = delegate
				{
					if (profilePaths.Count > 0)
					{
						int index = profilePaths.Count - 1;
						string path = profilePaths[index];
						profilePaths.RemoveAt(index);
						DataStorage.ReadJSONFile(path, delegate(string p, bool success, ModProfile data)
						{
							if (success)
							{
								modProfiles.Add(data);
								loadNextProfile();
							}
							else
							{
								DataStorage.DeleteFile(path, delegate
								{
									loadNextProfile();
								});
							}
						});
					}
					else if (onComplete != null)
					{
						onComplete(modProfiles);
					}
				};
				loadNextProfile();
			});
		}

		public static void RequestFilteredModProfiles(IList<int> idFilter, Action<IList<ModProfile>> onComplete)
		{
			RequestAllModProfilesFromOffset(0, delegate(IList<ModProfile> modProfiles)
			{
				List<ModProfile> list = new List<ModProfile>();
				foreach (ModProfile modProfile in modProfiles)
				{
					if (modProfile != null && idFilter.Contains(modProfile.id))
					{
						list.Add(modProfile);
					}
				}
				if (onComplete != null)
				{
					onComplete(list);
				}
			});
		}

		public static void DeleteMod(int modId, Action<bool> onComplete)
		{
			string path = GenerateModDirectoryPath(modId);
			DataStorage.DeleteDirectory(path, delegate(string text, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static string GenerateModStatisticsFilePath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModDirectoryPath(modId), "stats.data");
		}

		public static void SaveModStatistics(ModStatistics stats, Action<bool> onComplete)
		{
			string path = GenerateModStatisticsFilePath(stats.modId);
			DataStorage.WriteJSONFile(path, stats, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModStatistics(int modId, Action<ModStatistics> onComplete)
		{
			string path = GenerateModStatisticsFilePath(modId);
			DataStorage.ReadJSONFile(path, delegate(string p, bool success, ModStatistics data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static void RequestFilteredModStatistics(IList<int> idFilter, Action<IList<ModStatistics>> onComplete)
		{
			List<ModStatistics> modStatistics = new List<ModStatistics>();
			List<string> statsPaths = new List<string>();
			if (idFilter == null || idFilter.Count == 0)
			{
				onComplete(modStatistics);
				return;
			}
			string statisticsDirectory = IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "mods");
			DataStorage.GetDirectories(statisticsDirectory, delegate(string gd_path, bool gd_success, IList<string> modDirectories)
			{
				if (gd_success)
				{
					if (modDirectories == null)
					{
						modDirectories = new string[0];
					}
					else
					{
						foreach (string modDirectory in modDirectories)
						{
							string s = modDirectory.Substring(statisticsDirectory.Length + 1);
							int result = 0;
							if (!int.TryParse(s, out result))
							{
								result = 0;
							}
							if (idFilter.Contains(result))
							{
								string item = IOUtilities.CombinePath(modDirectory, "stats.data");
								statsPaths.Add(item);
							}
						}
					}
				}
				else
				{
					string message = "[mod.io] Failed to read mod statistics directory.\nDirectory: " + statisticsDirectory;
					Debug.LogWarning(message);
					modDirectories = new string[0];
				}
				Action loadNextStatistics = null;
				loadNextStatistics = delegate
				{
					if (statsPaths.Count > 0)
					{
						int index = statsPaths.Count - 1;
						string path = statsPaths[index];
						statsPaths.RemoveAt(index);
						DataStorage.ReadJSONFile(path, delegate(string p, bool success, ModStatistics data)
						{
							if (success)
							{
								modStatistics.Add(data);
								loadNextStatistics();
							}
							else
							{
								DataStorage.DeleteFile(path, delegate
								{
									loadNextStatistics();
								});
							}
						});
					}
					else if (onComplete != null)
					{
						onComplete(modStatistics);
					}
				};
				loadNextStatistics();
			});
		}

		public static string GenerateModBinariesDirectoryPath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModDirectoryPath(modId), "binaries");
		}

		public static string GenerateModfileFilePath(int modId, int modfileId)
		{
			return IOUtilities.CombinePath(GenerateModBinariesDirectoryPath(modId), modfileId + ".data");
		}

		public static string GenerateModBinaryZipFilePath(int modId, int modfileId)
		{
			return IOUtilities.CombinePath(GenerateModBinariesDirectoryPath(modId), modfileId + ".zip");
		}

		public static void SaveModfile(Modfile modfile, Action<bool> onComplete)
		{
			string path = GenerateModfileFilePath(modfile.modId, modfile.id);
			DataStorage.WriteJSONFile(path, modfile, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModfile(int modId, int modfileId, Action<Modfile> onComplete)
		{
			string path = GenerateModfileFilePath(modId, modfileId);
			DataStorage.ReadJSONFile(path, delegate(string p, bool success, Modfile data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static void SaveModBinaryZip(int modId, int modfileId, byte[] modBinary, Action<bool> onComplete)
		{
			string path = GenerateModBinaryZipFilePath(modId, modfileId);
			DataStorage.WriteFile(path, modBinary, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModBinaryZip(int modId, int modfileId, Action<byte[]> onComplete)
		{
			string path = GenerateModBinaryZipFilePath(modId, modfileId);
			DataStorage.ReadFile(path, delegate(string p, bool s, byte[] data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static void DeleteModfileAndBinaryZip(int modId, int modfileId, Action<bool> onComplete)
		{
			string path = GenerateModfileFilePath(modId, modfileId);
			string zipPath = GenerateModBinaryZipFilePath(modId, modfileId);
			DataStorage.DeleteFile(path, delegate(string mfP, bool mfS)
			{
				DataStorage.DeleteFile(zipPath, delegate(string zP, bool zS)
				{
					if (onComplete != null)
					{
						onComplete(mfS && zS);
					}
				});
			});
		}

		public static void DeleteAllModfileAndBinaryData(int modId, Action<bool> onComplete)
		{
			string path = GenerateModBinariesDirectoryPath(modId);
			DataStorage.DeleteDirectory(path, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static string GenerateModLogoCollectionDirectoryPath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModDirectoryPath(modId), "logo");
		}

		public static string GenerateModLogoFilePath(int modId, LogoSize size)
		{
			return IOUtilities.CombinePath(GenerateModLogoCollectionDirectoryPath(modId), size.ToString() + ".png");
		}

		public static string GenerateModLogoVersionInfoFilePath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModLogoCollectionDirectoryPath(modId), "versionInfo.data");
		}

		public static string GenerateModMediaDirectoryPath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModDirectoryPath(modId), "mod_media");
		}

		public static string GenerateModGalleryImageFilePath(int modId, string imageFileName, ModGalleryImageSize size)
		{
			return IOUtilities.CombinePath(GenerateModMediaDirectoryPath(modId), "images_" + size, Path.GetFileNameWithoutExtension(imageFileName) + ".png");
		}

		public static string GenerateModYouTubeThumbnailFilePath(int modId, string youTubeId)
		{
			return IOUtilities.CombinePath(GenerateModMediaDirectoryPath(modId), "youTube", youTubeId + ".png");
		}

		public static void GetModLogoVersionFileNames(int modId, Action<Dictionary<LogoSize, string>> onComplete)
		{
			string path = GenerateModLogoVersionInfoFilePath(modId);
			DataStorage.ReadJSONFile(path, delegate(string p, bool success, Dictionary<LogoSize, string> data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static void SaveModLogo(int modId, string fileName, LogoSize size, Texture2D logoTexture, Action<bool> onComplete)
		{
			string path = GenerateModLogoFilePath(modId, size);
			byte[] data = logoTexture.EncodeToPNG();
			DataStorage.WriteFile(path, data, delegate(string p, bool success)
			{
				GetModLogoVersionFileNames(modId, delegate(Dictionary<LogoSize, string> versionInfo)
				{
					if (versionInfo == null)
					{
						versionInfo = new Dictionary<LogoSize, string>();
					}
					versionInfo[size] = fileName;
					string path2 = GenerateModLogoVersionInfoFilePath(modId);
					DataStorage.WriteJSONFile(path2, versionInfo, null);
				});
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModLogo(int modId, LogoSize size, Action<Texture2D> onComplete)
		{
			string path = GenerateModLogoFilePath(modId, size);
			DataStorage.ReadFile(path, delegate(string p, bool success, byte[] data)
			{
				Texture2D obj = null;
				if (success && data != null)
				{
					obj = IOUtilities.ParseImageData(data);
				}
				if (onComplete != null)
				{
					onComplete(obj);
				}
			});
		}

		public static void LoadModLogo(int modId, string fileName, LogoSize size, Action<Texture2D> onComplete)
		{
			GetModLogoFileName(modId, size, delegate(string logoFileName)
			{
				if (logoFileName == fileName)
				{
					LoadModLogo(modId, size, onComplete);
				}
				else if (onComplete != null)
				{
					onComplete(null);
				}
			});
		}

		public static void GetModLogoFileName(int modId, LogoSize size, Action<string> onComplete)
		{
			GetModLogoVersionFileNames(modId, delegate(Dictionary<LogoSize, string> versionInfo)
			{
				string value = null;
				if (versionInfo != null)
				{
					versionInfo.TryGetValue(size, out value);
				}
				if (onComplete != null)
				{
					onComplete(value);
				}
			});
		}

		public static void SaveModGalleryImage(int modId, string imageFileName, ModGalleryImageSize size, Texture2D imageTexture, Action<bool> onComplete)
		{
			string path = GenerateModGalleryImageFilePath(modId, imageFileName, size);
			byte[] data = imageTexture.EncodeToPNG();
			DataStorage.WriteFile(path, data, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModGalleryImage(int modId, string imageFileName, ModGalleryImageSize size, Action<Texture2D> onComplete)
		{
			string path = GenerateModGalleryImageFilePath(modId, imageFileName, size);
			DataStorage.ReadFile(path, delegate(string p, bool success, byte[] data)
			{
				Texture2D obj = null;
				if (success && data != null)
				{
					obj = IOUtilities.ParseImageData(data);
				}
				if (onComplete != null)
				{
					onComplete(obj);
				}
			});
		}

		public static void SaveModYouTubeThumbnail(int modId, string youTubeId, Texture2D thumbnail, Action<bool> onComplete)
		{
			string path = GenerateModYouTubeThumbnailFilePath(modId, youTubeId);
			byte[] data = thumbnail.EncodeToPNG();
			DataStorage.WriteFile(path, data, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModYouTubeThumbnail(int modId, string youTubeId, Action<Texture2D> onComplete)
		{
			string path = GenerateModYouTubeThumbnailFilePath(modId, youTubeId);
			DataStorage.ReadFile(path, delegate(string p, bool success, byte[] data)
			{
				Texture2D obj = null;
				if (success && data != null)
				{
					obj = IOUtilities.ParseImageData(data);
				}
				if (onComplete != null)
				{
					onComplete(obj);
				}
			});
		}

		public static string GenerateModTeamFilePath(int modId)
		{
			return IOUtilities.CombinePath(GenerateModDirectoryPath(modId), "team.data");
		}

		public static void SaveModTeam(int modId, List<ModTeamMember> modTeam, Action<bool> onComplete)
		{
			string path = GenerateModTeamFilePath(modId);
			DataStorage.WriteJSONFile(path, modTeam, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadModTeam(int modId, Action<List<ModTeamMember>> onComplete)
		{
			string path = GenerateModTeamFilePath(modId);
			DataStorage.ReadJSONFile(path, delegate(string p, bool success, List<ModTeamMember> data)
			{
				if (onComplete != null)
				{
					onComplete(data);
				}
			});
		}

		public static void DeleteModTeam(int modId, Action<bool> onComplete)
		{
			string path = GenerateModTeamFilePath(modId);
			DataStorage.DeleteFile(path, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static string GenerateUserAvatarDirectoryPath(int userId)
		{
			return IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "users", userId + "_avatar");
		}

		public static string GenerateUserAvatarFilePath(int userId, UserAvatarSize size)
		{
			return IOUtilities.CombinePath(GenerateUserAvatarDirectoryPath(userId), size.ToString() + ".png");
		}

		public static void SaveUserAvatar(int userId, UserAvatarSize size, Texture2D avatarTexture, Action<bool> onComplete)
		{
			string path = GenerateUserAvatarFilePath(userId, size);
			byte[] data = avatarTexture.EncodeToPNG();
			DataStorage.WriteFile(path, data, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		public static void LoadUserAvatar(int userId, UserAvatarSize size, Action<Texture2D> onComplete)
		{
			string path = GenerateUserAvatarFilePath(userId, size);
			DataStorage.ReadFile(path, delegate(string p, bool success, byte[] data)
			{
				Texture2D obj = null;
				if (success && data != null)
				{
					obj = IOUtilities.ParseImageData(data);
				}
				if (onComplete != null)
				{
					onComplete(obj);
				}
			});
		}

		public static void DeleteUserAvatar(int userId, Action<bool> onComplete)
		{
			string path = GenerateUserAvatarDirectoryPath(userId);
			DataStorage.DeleteDirectory(path, delegate(string p, bool success)
			{
				if (onComplete != null)
				{
					onComplete(success);
				}
			});
		}

		[Obsolete("Use CacheClient.GetModLogoVersionFileNames() instead")]
		public static Dictionary<LogoSize, string> LoadModLogoFilePaths(int modId)
		{
			return GetModLogoVersionFileNames(modId);
		}

		[Obsolete("User Profiles are no longer accessible via the mod.io API.")]
		public static string GenerateUserProfileFilePath(int userId)
		{
			return IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "users", userId.ToString(), "profile.data");
		}

		[Obsolete("User Profiles are no longer accessible via the mod.io API.")]
		public static bool SaveUserProfile(UserProfile userProfile)
		{
			bool result = false;
			string path = GenerateUserProfileFilePath(userProfile.id);
			DataStorage.WriteJSONFile(path, userProfile, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("User Profiles are no longer accessible via the mod.io API.")]
		public static UserProfile LoadUserProfile(int userId)
		{
			string path = GenerateUserProfileFilePath(userId);
			UserProfile result = null;
			DataStorage.ReadJSONFile(path, delegate(string p, bool s, UserProfile r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("User Profiles are no longer accessible via the mod.io API.")]
		public static bool DeleteUserProfile(int userId)
		{
			bool result = false;
			string path = GenerateUserProfileFilePath(userId);
			DataStorage.DeleteFile(path, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("User Profiles are no longer accessible via the mod.io API.")]
		public static IEnumerable<UserProfile> IterateAllUserProfiles()
		{
			return null;
		}

		[Obsolete("Use SaveGameProfile(GameProfile, Action<bool>) instead.")]
		public static bool SaveGameProfile(GameProfile profile)
		{
			bool result = false;
			SaveGameProfile(profile, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadGameProfile(Action<GameProfile>) instead.")]
		public static GameProfile LoadGameProfile()
		{
			GameProfile result = null;
			LoadGameProfile(delegate(GameProfile r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModProfile(ModProfile, Action<bool>) instead.")]
		public static bool SaveModProfile(ModProfile profile)
		{
			bool result = false;
			SaveModProfile(profile, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModProfile(int, Action<ModProfile>) instead.")]
		public static ModProfile LoadModProfile(int modId)
		{
			ModProfile result = null;
			LoadModProfile(modId, delegate(ModProfile r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModProfile(IEnumerable<ModProfile>, Action<bool>) instead.")]
		public static bool SaveModProfiles(IEnumerable<ModProfile> modProfiles)
		{
			bool result = false;
			SaveModProfiles(modProfiles, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use RequestAllModProfiles(Action<IList<ModProfile>>) instead.")]
		public static IEnumerable<ModProfile> IterateAllModProfiles()
		{
			IList<ModProfile> result = null;
			RequestAllModProfiles(delegate(IList<ModProfile> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use RequestAllModProfilesFromOffset(int, Action<IList<ModProfile>>) instead.")]
		public static IEnumerable<ModProfile> IterateAllModProfilesFromOffset(int offset)
		{
			IList<ModProfile> result = null;
			RequestAllModProfilesFromOffset(offset, delegate(IList<ModProfile> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use RequestFilteredModProfiles(IList<int>, Action<IList<ModProfile>>) instead.")]
		public static IEnumerable<ModProfile> IterateFilteredModProfiles(IList<int> idFilter)
		{
			IList<ModProfile> result = null;
			RequestFilteredModProfiles(idFilter, delegate(IList<ModProfile> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use DeleteMod(int modId, Action<bool> onComplete)")]
		public static bool DeleteMod(int modId)
		{
			bool result = false;
			DeleteMod(modId, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("No longer supported.", true)]
		public static int CountModProfiles()
		{
			return -1;
		}

		[Obsolete("Use SaveModStatistics(ModStatistics, Action<bool>) instead.")]
		public static bool SaveModStatistics(ModStatistics stats)
		{
			bool result = false;
			SaveModStatistics(stats, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModStatistics(int, Action<ModStatistics>) instead.")]
		public static ModStatistics LoadModStatistics(int modId)
		{
			ModStatistics result = null;
			LoadModStatistics(modId, delegate(ModStatistics r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModfile(Modfile, Action<bool>) instead.")]
		public static bool SaveModfile(Modfile modfile)
		{
			bool result = false;
			SaveModfile(modfile, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModfile(int, int, Action<Modfile>) instead.")]
		public static Modfile LoadModfile(int modId, int modfileId)
		{
			Modfile result = null;
			LoadModfile(modId, modfileId, delegate(Modfile r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModBinaryZip(int, int, byte[], Action<bool>) instead.")]
		public static bool SaveModBinaryZip(int modId, int modfileId, byte[] modBinary)
		{
			bool result = false;
			SaveModBinaryZip(modId, modfileId, modBinary, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModBinaryZip(int, int, Action<byte[]>) instead.")]
		public static byte[] LoadModBinaryZip(int modId, int modfileId)
		{
			byte[] result = null;
			LoadModBinaryZip(modId, modfileId, delegate(byte[] r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use DeleteModfileAndBinaryZip(int, int, Action<bool>) instead.")]
		public static bool DeleteModfileAndBinaryZip(int modId, int modfileId)
		{
			bool result = false;
			DeleteModfileAndBinaryZip(modId, modfileId, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use DeleteAllModfileAndBinaryData(int, Action<bool>) instead.")]
		public static bool DeleteAllModfileAndBinaryData(int modId)
		{
			bool result = false;
			DeleteAllModfileAndBinaryData(modId, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use GetModLogoVersionFileNames(int, Action<IDictionary<LogoSize, string>>) instead.")]
		public static Dictionary<LogoSize, string> GetModLogoVersionFileNames(int modId)
		{
			Dictionary<LogoSize, string> result = null;
			GetModLogoVersionFileNames(modId, delegate(Dictionary<LogoSize, string> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModLogo(int, string, LogoSize, Texture2D, Action<bool>) instead.")]
		public static bool SaveModLogo(int modId, string fileName, LogoSize size, Texture2D logoTexture)
		{
			bool result = false;
			SaveModLogo(modId, fileName, size, logoTexture, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModLogo(int, LogoSize, Action<bool>) instead.")]
		public static Texture2D LoadModLogo(int modId, LogoSize size)
		{
			Texture2D result = null;
			LoadModLogo(modId, size, delegate(Texture2D r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModLogo(int, string, LogoSize, Action<bool>) instead.")]
		public static Texture2D LoadModLogo(int modId, string fileName, LogoSize size)
		{
			Texture2D result = null;
			LoadModLogo(modId, fileName, size, delegate(Texture2D r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use GetModLogoFileName(int, LogoSize, Action<string>) instead.")]
		public static string GetModLogoFileName(int modId, LogoSize size)
		{
			string result = null;
			GetModLogoFileName(modId, size, delegate(string r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModGalleryImage(int, string, ModGalleryImageSize, Texture2D, Action<bool>) instead.")]
		public static bool SaveModGalleryImage(int modId, string imageFileName, ModGalleryImageSize size, Texture2D imageTexture)
		{
			bool result = false;
			SaveModGalleryImage(modId, imageFileName, size, imageTexture, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModGalleryImage(int, string, ModGalleryImageSize, Action<Texture2D>) instead.")]
		public static Texture2D LoadModGalleryImage(int modId, string imageFileName, ModGalleryImageSize size)
		{
			Texture2D result = null;
			LoadModGalleryImage(modId, imageFileName, size, delegate(Texture2D r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModYouTubeThumbnail(int, string, Texture2D, Action<bool>) instead.")]
		public static bool SaveModYouTubeThumbnail(int modId, string youTubeId, Texture2D thumbnail)
		{
			bool result = false;
			SaveModYouTubeThumbnail(modId, youTubeId, thumbnail, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModYouTubeThumbnail(int, string, Action<Texture2D>) instead.")]
		public static Texture2D LoadModYouTubeThumbnail(int modId, string youTubeId)
		{
			Texture2D result = null;
			LoadModYouTubeThumbnail(modId, youTubeId, delegate(Texture2D r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveModTeam(int, List<ModTeamMember>, Action<bool>) instead.")]
		public static bool SaveModTeam(int modId, List<ModTeamMember> modTeam)
		{
			bool result = false;
			SaveModTeam(modId, modTeam, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadModTeam(int, Action<List<ModTeamMember>>) instead.")]
		public static List<ModTeamMember> LoadModTeam(int modId)
		{
			List<ModTeamMember> result = null;
			LoadModTeam(modId, delegate(List<ModTeamMember> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use DeleteModTeam(int, Action<bool>) instead.")]
		public static bool DeleteModTeam(int modId)
		{
			bool result = false;
			DeleteModTeam(modId, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use SaveUserAvatar(int, UserAvatarSize, Texture2D, Action<bool>) instead.")]
		public static bool SaveUserAvatar(int userId, UserAvatarSize size, Texture2D avatarTexture)
		{
			bool result = false;
			SaveUserAvatar(userId, size, avatarTexture, delegate(bool r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use LoadUserAvatar(int, UserAvatarSize, Action<Texture2D>) instead.")]
		public static Texture2D LoadUserAvatar(int userId, UserAvatarSize size)
		{
			Texture2D result = null;
			LoadUserAvatar(userId, size, delegate(Texture2D r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use DeleteUserAvatar(int, Action<bool>) instead.")]
		public static bool DeleteUserAvatar(int userId)
		{
			bool result = false;
			DeleteUserAvatar(userId, delegate(bool r)
			{
				result = r;
			});
			return result;
		}
	}
}
