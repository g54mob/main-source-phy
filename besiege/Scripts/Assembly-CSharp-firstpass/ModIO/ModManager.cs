using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModIO.API;
using ModIO.PlatformIOCallbacks;
using UnityEngine;

namespace ModIO
{
	public static class ModManager
	{
		private struct PersistentData
		{
			public ModIOVersion lastRunVersion;
		}

		private delegate void GetAllObjectsQuery<T>(APIPaginationParameters pagination, Action<RequestPage<T>> onSuccess, Action<WebRequestError> onError);

		public const string PERSISTENTDATA_FILENAME = "mod_manager.data";

		public static readonly string PERSISTENTDATA_FILEPATH;

		private static PersistentData m_data;

		public static int minimumFetchSize;

		private static HashSet<int> assertTargets;

		[Obsolete("Use DataStorage.INSTALLATION_DIRECTORY instead")]
		public static string installationDirectory
		{
			get
			{
				return DataStorage.INSTALLATION_DIRECTORY;
			}
		}

		public static event Action<ModfileIdPair> onModBinaryInstalled;

		public static event Action<ModfileIdPair[]> onModBinariesUninstalled;

		[Obsolete("Use ModManager.onModBinariesUninstalled instead.", false)]
		public static event Action<ModfileIdPair> onModBinaryUninstalled;

		static ModManager()
		{
			minimumFetchSize = 100;
			assertTargets = new HashSet<int>();
			PERSISTENTDATA_FILEPATH = IOUtilities.CombinePath(DataStorage.CACHE_DIRECTORY, "mod_manager.data");
			DataStorage.ReadJSONFile(PERSISTENTDATA_FILEPATH, delegate(string p, bool success, PersistentData data)
			{
				if (!success)
				{
					data = default(PersistentData);
				}
				else if (data.lastRunVersion < ModIOVersion.Current)
				{
					DataUpdater.UpdateFromVersion(data.lastRunVersion);
				}
				data.lastRunVersion = ModIOVersion.Current;
				m_data = data;
				DataStorage.WriteJSONFile(PERSISTENTDATA_FILEPATH, m_data, null);
			});
		}

		public static string GetModInstallDirectory(int modId, int modfileId)
		{
			return IOUtilities.CombinePath(DataStorage.INSTALLATION_DIRECTORY, modId + "_" + modfileId);
		}

		public static void TryInstallMod(int modId, int modfileId, Action<bool> onComplete)
		{
			string installDirectory = GetModInstallDirectory(modId, modfileId);
			string tempLocation = IOUtilities.CombinePath(CacheClient.GenerateModBinariesDirectoryPath(modId), modfileId.ToString());
			string archivePath = CacheClient.GenerateModBinaryZipFilePath(modId, modfileId);
			GetFileExistsCallback getFileExistsCallback = null;
			Action<bool> onOldVersionsUninstalled = null;
			DeleteFileCallback onArchiveDeleted = null;
			getFileExistsCallback = delegate(string path, bool success)
			{
				if (!success)
				{
					Debug.LogWarning("[mod.io] Unable to extract binary to the mod install folder.\nMod Binary ZipFile [" + archivePath + "] does not exist.");
					if (onComplete != null)
					{
						onComplete(false);
					}
				}
				else
				{
					DataStorage.DeleteDirectory(tempLocation, delegate(string dd_path, bool dd_success)
					{
						DataStorage.CreateDirectory(tempLocation, delegate(string cd_path, bool cd_success)
						{
							if (dd_success && cd_success)
							{
								if (CompressionModule.ExtractAll(archivePath, tempLocation))
								{
									UninstallMod(modId, onOldVersionsUninstalled);
								}
								else
								{
									DataStorage.DeleteDirectory(tempLocation, null);
									if (onComplete != null)
									{
										onComplete(false);
									}
								}
							}
							else if (onComplete != null)
							{
								onComplete(false);
							}
						});
					});
				}
			};
			onOldVersionsUninstalled = delegate(bool success)
			{
				if (!success)
				{
					Debug.LogWarning("[mod.io] Unable to extract binary to the mod install folder.\nFailed to uninstall existing versions of this mod.");
					DataStorage.DeleteDirectory(tempLocation, null);
					DataStorage.DeleteFile(archivePath, null);
					if (onComplete != null)
					{
						onComplete(false);
					}
				}
				else
				{
					DataStorage.DeleteDirectory(installDirectory, delegate(string dd_path, bool dd_success)
					{
						DataStorage.CreateDirectory(DataStorage.INSTALLATION_DIRECTORY, delegate(string cd_path, bool cd_success)
						{
							DataStorage.MoveDirectory(tempLocation, installDirectory, delegate(string md_src, string md_dst, bool md_success)
							{
								if (dd_success && cd_success && md_success)
								{
									DataStorage.DeleteFile(archivePath, onArchiveDeleted);
								}
								else
								{
									Debug.LogWarning("[mod.io] Unable to relocate the mod data from a temp folder to the installations folder.\nSrc: " + tempLocation + "\nDest: " + installDirectory);
									if (onComplete != null)
									{
										onComplete(false);
									}
								}
							});
						});
					});
				}
			};
			onArchiveDeleted = delegate
			{
				if (ModManager.onModBinaryInstalled != null)
				{
					ModfileIdPair obj = new ModfileIdPair
					{
						modId = modId,
						modfileId = modfileId
					};
					ModManager.onModBinaryInstalled(obj);
				}
				if (onComplete != null)
				{
					onComplete(true);
				}
			};
			DataStorage.GetFileExists(archivePath, getFileExistsCallback);
		}

		public static void UninstallMod(int modId, Action<bool> onComplete)
		{
			QueryInstalledMods(new int[1] { modId }, delegate(IList<KeyValuePair<ModfileIdPair, string>> installedMods)
			{
				List<ModfileIdPair> successfulUninstalls = new List<ModfileIdPair>();
				int index = 0;
				Action uninstallAction = null;
				uninstallAction = delegate
				{
					if (index < installedMods.Count)
					{
						KeyValuePair<ModfileIdPair, string> installInfo = installedMods.ElementAt(index++);
						DataStorage.DeleteDirectory(installInfo.Value, delegate(string p, bool success)
						{
							if (success)
							{
								successfulUninstalls.Add(installInfo.Key);
							}
							uninstallAction();
						});
					}
					else
					{
						if (ModManager.onModBinariesUninstalled != null)
						{
							ModManager.onModBinariesUninstalled(successfulUninstalls.ToArray());
						}
						if (onComplete != null)
						{
							onComplete(successfulUninstalls.Count == installedMods.Count);
						}
					}
				};
				uninstallAction();
			});
		}

		public static void TryUninstallModVersion(int modId, int modfileId, Action<bool> onComplete)
		{
			QueryInstalledMods(new int[1] { modId }, delegate(IList<KeyValuePair<ModfileIdPair, string>> installedMods)
			{
				foreach (KeyValuePair<ModfileIdPair, string> installedMod in installedMods)
				{
					if (installedMod.Key.modfileId == modfileId)
					{
						DataStorage.DeleteDirectory(installedMod.Value, delegate(string path, bool success)
						{
							if (success && ModManager.onModBinariesUninstalled != null)
							{
								ModfileIdPair modfileIdPair = new ModfileIdPair
								{
									modId = modId,
									modfileId = modfileId
								};
								ModManager.onModBinariesUninstalled(new ModfileIdPair[1] { modfileIdPair });
							}
							if (onComplete != null)
							{
								onComplete(success);
							}
						});
						return;
					}
				}
				if (onComplete != null)
				{
					onComplete(true);
				}
			});
		}

		public static void QueryInstalledModDirectories(bool excludeDisabledMods, Action<List<string>> onComplete)
		{
			List<int> list = null;
			if (excludeDisabledMods)
			{
				list = new List<int>(LocalUser.EnabledModIds);
				list.Add(0);
			}
			QueryInstalledMods(list, delegate(IList<KeyValuePair<ModfileIdPair, string>> installedMods)
			{
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<ModfileIdPair, string> installedMod in installedMods)
				{
					list2.Add(installedMod.Value);
				}
				onComplete(list2);
			});
		}

		public static void QueryInstalledModVersions(bool excludeDisabledMods, Action<List<ModfileIdPair>> onComplete)
		{
			List<int> modIdFilter = null;
			if (excludeDisabledMods)
			{
				modIdFilter = new List<int>(LocalUser.EnabledModIds);
			}
			QueryInstalledMods(modIdFilter, delegate(IList<KeyValuePair<ModfileIdPair, string>> installedMods)
			{
				List<ModfileIdPair> list = new List<ModfileIdPair>();
				foreach (KeyValuePair<ModfileIdPair, string> installedMod in installedMods)
				{
					if (installedMod.Key.modId != 0)
					{
						list.Add(installedMod.Key);
					}
				}
				onComplete(list);
			});
		}

		public static void QueryInstalledMods(IList<int> modIdFilter, Action<IList<KeyValuePair<ModfileIdPair, string>>> onComplete)
		{
			DataStorage.GetDirectories(DataStorage.INSTALLATION_DIRECTORY, delegate(string path, bool exists, IList<string> modDirectories)
			{
				List<KeyValuePair<ModfileIdPair, string>> list = new List<KeyValuePair<ModfileIdPair, string>>();
				if (exists && modDirectories != null)
				{
					foreach (string modDirectory in modDirectories)
					{
						string pathItemName = IOUtilities.GetPathItemName(modDirectory);
						string[] array = pathItemName.Split('_');
						int result;
						if (array.Length <= 0 || !int.TryParse(array[0], out result))
						{
							result = 0;
						}
						if (modIdFilter == null || modIdFilter.Count == 0 || modIdFilter.Contains(result))
						{
							int result2;
							if (result == 0 || array.Length <= 1 || !int.TryParse(array[1], out result2))
							{
								result2 = 0;
							}
							ModfileIdPair key = new ModfileIdPair
							{
								modId = result,
								modfileId = result2
							};
							KeyValuePair<ModfileIdPair, string> item = new KeyValuePair<ModfileIdPair, string>(key, modDirectory);
							list.Add(item);
						}
					}
				}
				if (onComplete != null)
				{
					onComplete(list);
				}
			});
		}

		public static void DownloadAndUpdateMod(int modId, Action onSuccess, Action<WebRequestError> onError)
		{
			ModProfile profile = null;
			Modfile modfile = null;
			string installDir = null;
			string zipFilePath = null;
			Action<ModProfile> action = null;
			GetFileSizeAndHashCallback onGetModProfile_OnGetFileInfo = null;
			Action<ModfileIdPair, FileDownloadInfo> onDownloadSucceeded = null;
			Action<ModfileIdPair, WebRequestError> onDownloadFailed = null;
			Action<bool> onInstalled = null;
			action = delegate(ModProfile p)
			{
				profile = p;
				modfile = p.currentBuild;
				installDir = GetModInstallDirectory(p.id, modfile.id);
				DataStorage.GetDirectoryExists(installDir, delegate(string gde_path, bool gde_exists)
				{
					if (gde_exists)
					{
						if (onSuccess != null)
						{
							onSuccess();
						}
					}
					else
					{
						zipFilePath = CacheClient.GenerateModBinaryZipFilePath(profile.id, modfile.id);
						DataStorage.GetFileSizeAndHash(zipFilePath, onGetModProfile_OnGetFileInfo);
					}
				});
			};
			onGetModProfile_OnGetFileInfo = delegate(string path, bool success, long fileSize, string fileHash)
			{
				if (success && modfile.fileSize == fileSize && (modfile.fileHash == null || modfile.fileHash.md5 == fileHash))
				{
					TryInstallMod(profile.id, modfile.id, onInstalled);
				}
				else
				{
					DownloadClient.StartModBinaryDownload(modfile, CacheClient.GenerateModBinaryZipFilePath(profile.id, profile.currentBuild.id));
					DownloadClient.modfileDownloadSucceeded += onDownloadSucceeded;
					DownloadClient.modfileDownloadFailed += onDownloadFailed;
				}
			};
			onDownloadSucceeded = delegate(ModfileIdPair mip, FileDownloadInfo downloadInfo)
			{
				if (mip.modId == modId)
				{
					DownloadClient.modfileDownloadSucceeded -= onDownloadSucceeded;
					DownloadClient.modfileDownloadFailed -= onDownloadFailed;
					TryInstallMod(profile.id, modfile.id, onInstalled);
				}
			};
			onDownloadFailed = delegate(ModfileIdPair mip, WebRequestError e)
			{
				if (mip.modId == modId)
				{
					DownloadClient.modfileDownloadSucceeded -= onDownloadSucceeded;
					DownloadClient.modfileDownloadFailed -= onDownloadFailed;
					if (onError != null)
					{
						onError(e);
					}
				}
			};
			onInstalled = delegate(bool success)
			{
				if (success)
				{
					if (onSuccess != null)
					{
						onSuccess();
					}
				}
				else if (onError != null)
				{
					string errorMessage = "Successfully downloaded but failed to install mod '" + profile.name + "'. See logged message for details.";
					onError(WebRequestError.GenerateLocal(errorMessage));
				}
			};
			APIClient.GetMod(modId, action, onError);
		}

		public static IEnumerator DownloadAndUpdateMods_Coroutine(IList<int> modIds, Action onCompleted = null)
		{
			if (modIds.Count == 0)
			{
				if (onCompleted != null)
				{
					onCompleted();
				}
				yield break;
			}
			Func<WebRequestError, int> calcReattemptDelay = delegate(WebRequestError requestError)
			{
				if (requestError.limitedUntilTimeStamp > 0)
				{
					return requestError.limitedUntilTimeStamp - ServerTimeStamp.Now;
				}
				return (!requestError.isRequestUnresolvable) ? (requestError.isServerUnreachable ? 60 : 15) : 0;
			};
			int attemptCount = 0;
			int attemptLimit = 2;
			bool isRequestResolved = false;
			List<Modfile> lastestBuilds = new List<Modfile>(modIds.Count);
			RequestFilter modFilter = new RequestFilter();
			modFilter.AddFieldFilter("id", new InArrayFilter<int>
			{
				filterArray = modIds.ToArray()
			});
			while (!isRequestResolved && attemptCount < attemptLimit)
			{
				bool isDone = false;
				WebRequestError error = null;
				List<ModProfile> profiles = null;
				FetchAllResultsForQuery(delegate(APIPaginationParameters p, Action<RequestPage<ModProfile>> s, Action<WebRequestError> e)
				{
					APIClient.GetAllMods(modFilter, p, s, e);
				}, delegate(List<ModProfile> r)
				{
					profiles = r;
					isDone = true;
				}, delegate(WebRequestError e)
				{
					error = e;
					isDone = true;
				});
				while (!isDone)
				{
					yield return null;
				}
				if (error != null)
				{
					if (error.isAuthenticationInvalid)
					{
						isRequestResolved = true;
						continue;
					}
					if (error.isRequestUnresolvable)
					{
						isRequestResolved = true;
						continue;
					}
					attemptCount++;
					int reattemptDelay = calcReattemptDelay(error);
					yield return new WaitForSecondsRealtime(reattemptDelay);
					continue;
				}
				foreach (ModProfile profile in profiles)
				{
					lastestBuilds.Add(profile.currentBuild);
				}
				isRequestResolved = true;
			}
			IEnumerator assertCoroutine = AssertDownloadedAndInstalled_Coroutine(lastestBuilds);
			while (assertCoroutine.MoveNext())
			{
				yield return assertCoroutine.Current;
			}
			if (onCompleted != null)
			{
				onCompleted();
			}
		}

		public static IEnumerator AssertDownloadedAndInstalled_Coroutine(IEnumerable<Modfile> modfiles, Action onCompleted = null)
		{
			List<Modfile> unmatchedModfiles = new List<Modfile>(modfiles);
			string items = string.Empty;
			for (int i = 0; i < unmatchedModfiles.Count; i++)
			{
				if (unmatchedModfiles[i] == null || assertTargets.Contains(unmatchedModfiles[i].modId))
				{
					unmatchedModfiles.RemoveAt(i);
					i--;
				}
				else
				{
					int id = unmatchedModfiles[i].modId;
					assertTargets.Add(id);
					items = items + id + ", ";
				}
			}
			if (unmatchedModfiles.Count == 0)
			{
				if (onCompleted != null)
				{
					onCompleted();
				}
				yield break;
			}
			bool gotModVersions = false;
			List<ModfileIdPair> installedModVersions = null;
			QueryInstalledModVersions(false, delegate(List<ModfileIdPair> r)
			{
				installedModVersions = r;
				gotModVersions = true;
			});
			while (!gotModVersions)
			{
				yield return null;
			}
			for (int i2 = 0; i2 < unmatchedModfiles.Count; i2++)
			{
				Modfile m = unmatchedModfiles[i2];
				if (m == null)
				{
					unmatchedModfiles.RemoveAt(i2);
					i2--;
					continue;
				}
				bool isInstalled = false;
				foreach (ModfileIdPair idPair in installedModVersions)
				{
					if (idPair.modId == m.modId && idPair.modfileId == m.id)
					{
						isInstalled = true;
						break;
					}
				}
				if (!isInstalled)
				{
					string zipFilePath = CacheClient.GenerateModBinaryZipFilePath(m.modId, m.id);
					bool fileExists = false;
					long fileSize = -1L;
					string fileHash = null;
					bool isIOOpDone = false;
					DataStorage.GetFileSizeAndHash(zipFilePath, delegate(string p, bool success, long byteCount, string md5Hash)
					{
						fileExists = success;
						fileSize = byteCount;
						fileHash = md5Hash;
						isIOOpDone = true;
					});
					while (!isIOOpDone)
					{
						yield return null;
					}
					if (fileExists && m.fileSize == fileSize && (m.fileHash == null || m.fileHash.md5 == fileHash))
					{
						bool installDone = false;
						TryInstallMod(m.modId, m.id, delegate(bool success)
						{
							installDone = true;
							isInstalled = success;
						});
						while (!installDone)
						{
							yield return null;
						}
					}
				}
				if (isInstalled)
				{
					assertTargets.Remove(unmatchedModfiles[i2].modId);
					unmatchedModfiles.RemoveAt(i2);
					i2--;
				}
			}
			int awaitingModfileUpdates = 0;
			List<Modfile> badModfiles = new List<Modfile>();
			for (int i3 = 0; i3 < unmatchedModfiles.Count; i3++)
			{
				int modIndex = i3;
				Modfile modfile = unmatchedModfiles[i3];
				if (modfile.downloadLocator != null && modfile.downloadLocator.dateExpires > ServerTimeStamp.Now)
				{
					continue;
				}
				awaitingModfileUpdates++;
				APIClient.GetModfile(modfile.modId, modfile.id, delegate(Modfile updatedModfile)
				{
					awaitingModfileUpdates--;
					if (modfile.downloadLocator == null)
					{
						badModfiles.Add(modfile);
						Debug.LogWarning("[mod.io] Unable to get a good download locator for (modId:" + modfile.modId + "-modfileId:" + modfile.id + ").");
					}
					else
					{
						unmatchedModfiles[modIndex] = updatedModfile;
					}
				}, delegate(WebRequestError e)
				{
					awaitingModfileUpdates--;
					badModfiles.Add(modfile);
					Debug.LogWarning("[mod.io] Unable to get a good download locator for (modId:" + modfile.modId + "-modfileId:" + modfile.id + ").\n---[ Response Info ]---\n" + DebugUtilities.GetResponseInfo(e.webRequest));
				});
			}
			while (awaitingModfileUpdates > 0)
			{
				yield return null;
			}
			foreach (Modfile brokenModfile in badModfiles)
			{
				unmatchedModfiles.Remove(brokenModfile);
			}
			if (unmatchedModfiles.Count > 0)
			{
				bool startNextDownload = false;
				Modfile downloadingModfile = null;
				Action<ModfileIdPair, FileDownloadInfo> onDownloadSucceeded = delegate(ModfileIdPair modfileIdPair, FileDownloadInfo info)
				{
					if (modfileIdPair.modfileId == downloadingModfile.id)
					{
						TryInstallMod(downloadingModfile.modId, downloadingModfile.id, delegate(bool success)
						{
							if (!success)
							{
								Debug.LogWarning("[mod.io] Successfully downloaded but failed to install mod (id:" + downloadingModfile.modId + "-modfile:" + downloadingModfile.id + "). See logged message for details.");
							}
							startNextDownload = true;
						});
					}
				};
				Action<ModfileIdPair, WebRequestError> onDownloadFailed = delegate(ModfileIdPair modfileIdPair, WebRequestError e)
				{
					if (modfileIdPair.modfileId == downloadingModfile.id)
					{
						Debug.LogWarning("[mod.io] Failed to download mod (id:" + downloadingModfile.modId + "-modfile:" + downloadingModfile.id + "). See logged message for details.");
						startNextDownload = true;
					}
				};
				DownloadClient.modfileDownloadSucceeded += onDownloadSucceeded;
				DownloadClient.modfileDownloadFailed += onDownloadFailed;
				foreach (Modfile modfile2 in unmatchedModfiles)
				{
					if (DownloadClient.GetActiveModBinaryDownload(modfile2.modId, modfile2.id) == null)
					{
						downloadingModfile = modfile2;
						startNextDownload = false;
						string zipPath = CacheClient.GenerateModBinaryZipFilePath(downloadingModfile.modId, downloadingModfile.id);
						DownloadClient.StartModBinaryDownload(modfile2, zipPath);
						while (!startNextDownload)
						{
							yield return null;
						}
					}
				}
				DownloadClient.modfileDownloadSucceeded -= onDownloadSucceeded;
				DownloadClient.modfileDownloadFailed -= onDownloadFailed;
			}
			if (onCompleted != null)
			{
				onCompleted();
			}
			for (int i4 = 0; i4 < unmatchedModfiles.Count; i4++)
			{
				assertTargets.Remove(unmatchedModfiles[i4].modId);
			}
		}

		public static void GetGameProfile(Action<GameProfile> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadGameProfile(delegate(GameProfile cachedProfile)
			{
				if (cachedProfile != null)
				{
					if (onSuccess != null)
					{
						onSuccess(cachedProfile);
					}
				}
				else
				{
					Action<GameProfile> successCallback = delegate(GameProfile profile)
					{
						CacheClient.SaveGameProfile(profile, null);
						if (onSuccess != null)
						{
							onSuccess(profile);
						}
					};
					APIClient.GetGame(successCallback, onError);
				}
			});
		}

		public static void GetModProfile(int modId, Action<ModProfile> onSuccess, Action<WebRequestError> onError)
		{
			if (onSuccess == null && onError == null)
			{
				return;
			}
			APIClient.GetMod(modId, delegate(ModProfile p)
			{
				if (LocalUser.SubscribedModIds.Contains(p.id))
				{
					CacheClient.SaveModProfile(p, null);
				}
				if (onSuccess != null)
				{
					onSuccess(p);
				}
			}, delegate(WebRequestError e)
			{
				CacheClient.LoadModProfile(modId, delegate(ModProfile cachedProfile)
				{
					if (cachedProfile != null)
					{
						if (onSuccess != null)
						{
							onSuccess(cachedProfile);
						}
					}
					else if (onError != null)
					{
						onError(e);
					}
				});
			});
		}

		public static void GetModProfiles(IList<int> orderedIdList, Action<ModProfile[]> onSuccess, Action<WebRequestError> onError)
		{
			if (orderedIdList == null)
			{
				if (onSuccess != null)
				{
					onSuccess(null);
				}
				return;
			}
			ModProfile[] modProfiles = new ModProfile[orderedIdList.Count];
			List<int> missingModIds = new List<int>(orderedIdList.Count);
			for (int i = 0; i < orderedIdList.Count; i++)
			{
				int num = orderedIdList[i];
				ModProfile profile;
				if (RequestCache.TryGetMod(PluginSettings.GAME_ID, num, out profile))
				{
					modProfiles[i] = profile;
				}
				else
				{
					missingModIds.Add(num);
				}
			}
			if (missingModIds.Count == 0)
			{
				if (onSuccess != null)
				{
					onSuccess(modProfiles);
				}
				return;
			}
			Action<WebRequestError> checkForMissingModsInCache = delegate(WebRequestError error)
			{
				if (missingModIds.Count == 0 && onSuccess != null)
				{
					onSuccess(modProfiles);
				}
				CacheClient.RequestFilteredModProfiles(missingModIds, delegate(IList<ModProfile> cachedProfiles)
				{
					foreach (ModProfile cachedProfile in cachedProfiles)
					{
						int num2 = orderedIdList.IndexOf(cachedProfile.id);
						if (num2 >= 0)
						{
							modProfiles[num2] = cachedProfile;
						}
						missingModIds.Remove(cachedProfile.id);
					}
					if (missingModIds.Count > 0 && error != null)
					{
						if (onError != null)
						{
							onError(error);
						}
					}
					else if (onSuccess != null)
					{
						onSuccess(modProfiles);
					}
				});
			};
			Action<List<ModProfile>> onSuccess2 = delegate(List<ModProfile> fetchedProfiles)
			{
				foreach (ModProfile fetchedProfile in fetchedProfiles)
				{
					int num2 = orderedIdList.IndexOf(fetchedProfile.id);
					if (num2 >= 0)
					{
						modProfiles[num2] = fetchedProfile;
					}
					missingModIds.Remove(fetchedProfile.id);
				}
				checkForMissingModsInCache(null);
			};
			RequestFilter modFilter = new RequestFilter();
			modFilter.sortFieldName = "id";
			modFilter.AddFieldFilter("id", new InArrayFilter<int>
			{
				filterArray = missingModIds.ToArray()
			});
			FetchAllResultsForQuery(delegate(APIPaginationParameters p, Action<RequestPage<ModProfile>> s, Action<WebRequestError> e)
			{
				APIClient.GetAllMods(modFilter, p, s, e);
			}, onSuccess2, checkForMissingModsInCache);
		}

		public static void GetRangeOfModProfiles(RequestFilter filter, int resultOffset, int profileCount, Action<RequestPage<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			if (onSuccess == null && onError == null)
			{
				return;
			}
			if (profileCount > 100)
			{
				Debug.LogWarning("[mod.io] FetchModProfilePage has been called with a profileCount larger than the APIPaginationParameters.LIMIT_MAX.\nAs such, results may not be as expected.");
				profileCount = 100;
			}
			if (resultOffset < 0)
			{
				resultOffset = 0;
			}
			if (profileCount < 0)
			{
				profileCount = 0;
			}
			List<ModProfile> results = new List<ModProfile>(profileCount);
			APIPaginationParameters pagination = new APIPaginationParameters();
			int num = resultOffset / minimumFetchSize;
			pagination.offset = num * minimumFetchSize;
			pagination.limit = minimumFetchSize;
			APIClient.GetAllMods(filter, pagination, delegate(RequestPage<ModProfile> r01)
			{
				int pageOffset = resultOffset % minimumFetchSize;
				for (int i = pageOffset; i < r01.items.Length && i < pageOffset + profileCount; i++)
				{
					results.Add(r01.items[i]);
				}
				if (pageOffset + profileCount > r01.size && r01.items.Length == r01.size)
				{
					pagination.offset += pagination.limit;
					APIClient.GetAllMods(filter, pagination, delegate(RequestPage<ModProfile> requestPage)
					{
						for (int j = 0; j < requestPage.items.Length && j < pageOffset + profileCount - requestPage.size; j++)
						{
							results.Add(requestPage.items[j]);
							OnModsReceived(resultOffset, profileCount, requestPage.resultTotal, results, onSuccess);
						}
					}, onError);
				}
				else
				{
					OnModsReceived(resultOffset, profileCount, r01.resultTotal, results, onSuccess);
				}
			}, onError);
		}

		private static void OnModsReceived(int resultOffset, int pageSize, int resultTotal, List<ModProfile> results, Action<RequestPage<ModProfile>> onSuccess)
		{
			if (onSuccess != null)
			{
				RequestPage<ModProfile> requestPage = new RequestPage<ModProfile>();
				requestPage.size = pageSize;
				requestPage.resultOffset = resultOffset;
				requestPage.resultTotal = resultTotal;
				requestPage.items = results.ToArray();
				RequestPage<ModProfile> obj = requestPage;
				onSuccess(obj);
			}
		}

		public static void GetModLogo(ModProfile profile, LogoSize size, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			GetModLogo(profile.id, profile.logoLocator, size, onSuccess, onError);
		}

		public static void GetModLogo(int modId, LogoImageLocator logoLocator, LogoSize size, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadModLogo(modId, logoLocator.fileName, size, delegate(Texture2D logoTexture)
			{
				if (logoTexture != null)
				{
					if (onSuccess != null)
					{
						onSuccess(logoTexture);
					}
				}
				else
				{
					ImageRequest imageRequest = DownloadClient.DownloadImage(logoLocator.GetSizeURL(size));
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						CacheClient.SaveModLogo(modId, logoLocator.GetFileName(), size, d.imageTexture, null);
					};
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						onSuccess(d.imageTexture);
					};
					imageRequest.failed += delegate(ImageRequest d)
					{
						onError(d.error);
					};
				}
			});
		}

		public static void GetModGalleryImage(ModProfile profile, string imageFileName, ModGalleryImageSize size, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			GetModGalleryImage(profile.id, profile.media.GetGalleryImageWithFileName(imageFileName), size, onSuccess, onError);
		}

		public static void GetModGalleryImage(int modId, GalleryImageLocator imageLocator, ModGalleryImageSize size, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadModGalleryImage(modId, imageLocator.fileName, size, delegate(Texture2D cachedImageTexture)
			{
				if (cachedImageTexture != null)
				{
					if (onSuccess != null)
					{
						onSuccess(cachedImageTexture);
					}
				}
				else
				{
					ImageRequest imageRequest = DownloadClient.DownloadModGalleryImage(imageLocator, size);
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						CacheClient.SaveModGalleryImage(modId, imageLocator.fileName, size, d.imageTexture, null);
					};
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						onSuccess(d.imageTexture);
					};
					imageRequest.failed += delegate(ImageRequest d)
					{
						onError(d.error);
					};
				}
			});
		}

		public static void GetModYouTubeThumbnail(int modId, string youTubeVideoId, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadModYouTubeThumbnail(modId, youTubeVideoId, delegate(Texture2D cachedYouTubeThumbnail)
			{
				if (cachedYouTubeThumbnail != null)
				{
					if (onSuccess != null)
					{
						onSuccess(cachedYouTubeThumbnail);
					}
				}
				else
				{
					ImageRequest imageRequest = DownloadClient.DownloadYouTubeThumbnail(youTubeVideoId);
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						CacheClient.SaveModYouTubeThumbnail(modId, youTubeVideoId, d.imageTexture, null);
					};
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						onSuccess(d.imageTexture);
					};
					imageRequest.failed += delegate(ImageRequest d)
					{
						onError(d.error);
					};
				}
			});
		}

		public static void GetModfile(int modId, int modfileId, Action<Modfile> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadModfile(modId, modfileId, delegate(Modfile cachedModfile)
			{
				if (cachedModfile != null)
				{
					if (onSuccess != null)
					{
						onSuccess(cachedModfile);
					}
				}
				else
				{
					Action<Modfile> successCallback = delegate(Modfile modfile)
					{
						CacheClient.SaveModfile(modfile, null);
						if (onSuccess != null)
						{
							onSuccess(modfile);
						}
					};
					APIClient.GetModfile(modId, modfileId, successCallback, onError);
				}
			});
		}

		public static void GetModStatistics(int modId, Action<ModStatistics> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadModStatistics(modId, delegate(ModStatistics cachedStats)
			{
				if (cachedStats != null && cachedStats.dateExpires > ServerTimeStamp.Now)
				{
					if (onSuccess != null)
					{
						onSuccess(cachedStats);
					}
				}
				else
				{
					Action<ModStatistics> successCallback = delegate(ModStatistics stats)
					{
						CacheClient.SaveModStatistics(stats, null);
						if (onSuccess != null)
						{
							onSuccess(stats);
						}
					};
					APIClient.GetModStats(modId, successCallback, onError);
				}
			});
		}

		public static void GetAuthenticatedUserProfile(Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			if (LocalUser.Profile == null && LocalUser.AuthenticationState == AuthenticationState.ValidToken)
			{
				UserAccountManagement.UpdateUserProfile(onSuccess, onError);
			}
			else if (onSuccess != null)
			{
				onSuccess(LocalUser.Profile);
			}
		}

		public static void FetchAuthenticatedUserMods(Action<List<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			RequestFilter userModsFilter = new RequestFilter();
			userModsFilter.AddFieldFilter("game_id", new EqualToFilter<int>(0)
			{
				filterValue = PluginSettings.GAME_ID
			});
			userModsFilter.AddFieldFilter("submitted_by", new EqualToFilter<int>(0)
			{
				filterValue = LocalUser.UserId
			});
			Action<List<ModProfile>> onSuccess2 = delegate(List<ModProfile> modProfiles)
			{
				List<int> list = new List<int>(modProfiles.Count);
				foreach (ModProfile modProfile in modProfiles)
				{
					list.Add(modProfile.id);
				}
				if (onSuccess != null)
				{
					onSuccess(modProfiles);
				}
			};
			FetchAllResultsForQuery(delegate(APIPaginationParameters p, Action<RequestPage<ModProfile>> s, Action<WebRequestError> e)
			{
				APIClient.GetAllMods(userModsFilter, p, s, e);
			}, onSuccess2, onError);
		}

		public static void GetUserAvatar(UserProfile profile, UserAvatarSize size, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			GetUserAvatar(profile.id, profile.avatarLocator, size, onSuccess, onError);
		}

		public static void GetUserAvatar(int userId, AvatarImageLocator avatarLocator, UserAvatarSize size, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			CacheClient.LoadUserAvatar(userId, size, delegate(Texture2D cachedAvatarTexture)
			{
				if (cachedAvatarTexture != null)
				{
					if (onSuccess != null)
					{
						onSuccess(cachedAvatarTexture);
					}
				}
				else
				{
					ImageRequest imageRequest = DownloadClient.DownloadImage(avatarLocator.GetSizeURL(size));
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						CacheClient.SaveUserAvatar(userId, size, d.imageTexture, null);
					};
					imageRequest.succeeded += delegate(ImageRequest d)
					{
						onSuccess(d.imageTexture);
					};
					if (onError != null)
					{
						imageRequest.failed += delegate(ImageRequest d)
						{
							onError(d.error);
						};
					}
				}
			});
		}

		public static void FetchAllModEvents(int fromTimeStamp, int untilTimeStamp, Action<List<ModEvent>> onSuccess, Action<WebRequestError> onError)
		{
			FetchModEvents(null, fromTimeStamp, untilTimeStamp, onSuccess, onError);
		}

		public static void FetchModEvents(IEnumerable<int> modIdFilter, int fromTimeStamp, int untilTimeStamp, Action<List<ModEvent>> onSuccess, Action<WebRequestError> onError)
		{
			int[] array = null;
			if (modIdFilter != null)
			{
				array = modIdFilter.ToArray();
			}
			if (array != null && array.Length == 0)
			{
				if (onSuccess != null)
				{
					onSuccess(new List<ModEvent>());
				}
				return;
			}
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "id";
			requestFilter.isSortAscending = false;
			requestFilter.AddFieldFilter("date_added", new MinimumFilter<int>(0)
			{
				minimum = fromTimeStamp,
				isInclusive = false
			});
			requestFilter.AddFieldFilter("date_added", new MaximumFilter<int>(0)
			{
				maximum = untilTimeStamp,
				isInclusive = true
			});
			if (array != null)
			{
				requestFilter.AddFieldFilter("mod_id", new InArrayFilter<int>
				{
					filterArray = array
				});
			}
			APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters();
			aPIPaginationParameters.limit = 100;
			aPIPaginationParameters.offset = 0;
			APIPaginationParameters pagination = aPIPaginationParameters;
			APIClient.GetAllModEvents(requestFilter, pagination, delegate(RequestPage<ModEvent> r)
			{
				_OnModEventSuccess(r, onSuccess);
			}, onError);
		}

		public static void FetchModEventsAfterId(int eventId, IEnumerable<int> modIdFilter, Action<List<ModEvent>> onSuccess, Action<WebRequestError> onError)
		{
			int[] array = null;
			if (modIdFilter != null)
			{
				array = modIdFilter.ToArray();
			}
			if (array != null && array.Length == 0)
			{
				if (onSuccess != null)
				{
					onSuccess(new List<ModEvent>());
				}
				return;
			}
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "id";
			requestFilter.isSortAscending = false;
			requestFilter.AddFieldFilter("id", new MinimumFilter<int>(0)
			{
				minimum = eventId,
				isInclusive = false
			});
			if (array != null)
			{
				requestFilter.AddFieldFilter("mod_id", new InArrayFilter<int>
				{
					filterArray = array
				});
			}
			APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters();
			aPIPaginationParameters.limit = 100;
			aPIPaginationParameters.offset = 0;
			APIPaginationParameters pagination = aPIPaginationParameters;
			APIClient.GetAllModEvents(requestFilter, pagination, delegate(RequestPage<ModEvent> r)
			{
				_OnModEventSuccess(r, onSuccess);
			}, onError);
		}

		private static void _OnModEventSuccess(RequestPage<ModEvent> r, Action<List<ModEvent>> onSuccess)
		{
			if (onSuccess != null)
			{
				List<ModEvent> obj = new List<ModEvent>();
				if (r != null && r.items != null && r.items.Length > 0)
				{
					obj = new List<ModEvent>(r.items);
				}
				onSuccess(obj);
			}
		}

		public static void FetchAllUserEvents(int fromTimeStamp, int untilTimeStamp, Action<List<UserEvent>> onSuccess, Action<WebRequestError> onError)
		{
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "id";
			requestFilter.isSortAscending = false;
			requestFilter.AddFieldFilter("date_added", new MinimumFilter<int>(0)
			{
				minimum = fromTimeStamp,
				isInclusive = false
			});
			requestFilter.AddFieldFilter("date_added", new MaximumFilter<int>(0)
			{
				maximum = untilTimeStamp,
				isInclusive = true
			});
			requestFilter.AddFieldFilter("game_id", new EqualToFilter<int>(0)
			{
				filterValue = PluginSettings.GAME_ID
			});
			APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters();
			aPIPaginationParameters.limit = 100;
			aPIPaginationParameters.offset = 0;
			APIPaginationParameters pagination = aPIPaginationParameters;
			APIClient.GetUserEvents(requestFilter, pagination, delegate(RequestPage<UserEvent> r)
			{
				_OnUserEventsFetched(r, onSuccess);
			}, onError);
		}

		public static void FetchUserEventsAfterId(int eventId, Action<List<UserEvent>> onSuccess, Action<WebRequestError> onError)
		{
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "id";
			requestFilter.isSortAscending = false;
			requestFilter.AddFieldFilter("id", new MinimumFilter<int>(0)
			{
				minimum = eventId,
				isInclusive = false
			});
			requestFilter.AddFieldFilter("game_id", new EqualToFilter<int>(0)
			{
				filterValue = PluginSettings.GAME_ID
			});
			APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters();
			aPIPaginationParameters.limit = 100;
			aPIPaginationParameters.offset = 0;
			APIPaginationParameters pagination = aPIPaginationParameters;
			APIClient.GetUserEvents(requestFilter, pagination, delegate(RequestPage<UserEvent> r)
			{
				_OnUserEventsFetched(r, onSuccess);
			}, onError);
		}

		private static void _OnUserEventsFetched(RequestPage<UserEvent> r, Action<List<UserEvent>> onSuccess)
		{
			if (onSuccess != null)
			{
				List<UserEvent> obj = new List<UserEvent>();
				if (r != null && r.items != null && r.items.Length > 0)
				{
					obj = new List<UserEvent>(r.items);
				}
				onSuccess(obj);
			}
		}

		public static void SubmitNewMod(EditableModProfile newModProfile, Action<ModProfile> onSuccess, Action<WebRequestError> onError)
		{
			ModManager_SubmitModOperation modManager_SubmitModOperation = new ModManager_SubmitModOperation();
			modManager_SubmitModOperation.onSuccess = onSuccess;
			modManager_SubmitModOperation.onError = onError;
			modManager_SubmitModOperation.SubmitNewMod(newModProfile);
		}

		public static void SubmitModChanges(int modId, EditableModProfile modEdits, Action<ModProfile> onSuccess, Action<WebRequestError> onError)
		{
			ModManager_SubmitModOperation modManager_SubmitModOperation = new ModManager_SubmitModOperation();
			modManager_SubmitModOperation.onSuccess = onSuccess;
			modManager_SubmitModOperation.onError = onError;
			modManager_SubmitModOperation.SubmitModChanges(modId, modEdits);
		}

		public static void UploadModBinaryDirectory(int modId, EditableModfile modfileValues, string binaryDirectory, bool setActiveBuild, Action<Modfile> onSuccess, Action<WebRequestError> onError)
		{
			DataStorage.GetDirectoryExists(binaryDirectory, delegate(string dir_path, bool dir_exists)
			{
				if (dir_exists)
				{
					if (IOUtilities.PathEndsWithDirectorySeparator(binaryDirectory))
					{
						binaryDirectory = binaryDirectory.Remove(binaryDirectory.Length - 1);
					}
					DataStorage.GetFiles(binaryDirectory, null, true, delegate(string path, bool success, IList<string> fileList)
					{
						UploadModBinaryFileList(modId, modfileValues, binaryDirectory, fileList, setActiveBuild, onSuccess, onError);
					});
				}
				else if (onError != null)
				{
					onError(WebRequestError.GenerateLocal("Mod Binary directory [" + binaryDirectory + "] doesn't exist"));
				}
			});
		}

		public static void UploadModBinaryFileList(int modId, EditableModfile modfileValues, string rootDirectory, IList<string> fileList, bool setActiveBuild, Action<Modfile> onSuccess, Action<WebRequestError> onError)
		{
			if (string.IsNullOrEmpty(rootDirectory))
			{
				if (onError != null)
				{
					WebRequestError obj = WebRequestError.GenerateLocal("Unable to upload mod binary file list as the root directory was NULL or empty.");
					onError(obj);
				}
				return;
			}
			if (fileList == null || fileList.Count == 0)
			{
				if (onError != null)
				{
					WebRequestError obj2 = WebRequestError.GenerateLocal("Unable to upload mod binary file list as the file list was NULL or empty.");
					onError(obj2);
				}
				return;
			}
			if (modfileValues == null)
			{
				if (onError != null)
				{
					WebRequestError obj3 = WebRequestError.GenerateLocal("Unable to upload mod binary file list as the modfile data was NULL.");
					onError(obj3);
				}
				return;
			}
			int length = rootDirectory.Length;
			if (!IOUtilities.PathEndsWithDirectorySeparator(rootDirectory))
			{
				length++;
			}
			string text = IOUtilities.CombinePath(Application.temporaryCachePath, "modio");
			string archiveFilePath = IOUtilities.CombinePath(text, DateTime.Now.ToFileTime() + "_" + modId.ToString() + ".zip");
			DataStorage.CreateDirectory(text, delegate(string path, bool success)
			{
				if (success)
				{
					success = CompressionModule.CompressFileCollection(rootDirectory, fileList, archiveFilePath);
					if (success)
					{
						UploadModBinary_Zipped(modId, modfileValues, archiveFilePath, setActiveBuild, onSuccess, onError);
					}
				}
				if (!success && onError != null)
				{
					WebRequestError obj4 = WebRequestError.GenerateLocal("Unable to zip mod binary prior to uploading");
					onError(obj4);
				}
			});
		}

		public static void UploadModBinary_Unzipped(int modId, EditableModfile modfileValues, string unzippedBinaryLocation, bool setActiveBuild, Action<Modfile> onSuccess, Action<WebRequestError> onError)
		{
			string binaryZipLocation = IOUtilities.CombinePath(Application.temporaryCachePath, "modio", Path.GetFileNameWithoutExtension(unzippedBinaryLocation) + "_" + DateTime.Now.ToFileTime() + ".zip");
			bool zipSucceeded = false;
			DataStorage.CreateDirectory(Path.GetDirectoryName(binaryZipLocation), delegate
			{
				zipSucceeded = CompressionModule.CompressFile(unzippedBinaryLocation, binaryZipLocation);
				if (zipSucceeded)
				{
					UploadModBinary_Zipped(modId, modfileValues, binaryZipLocation, setActiveBuild, onSuccess, onError);
				}
				else if (onError != null)
				{
					WebRequestError obj = WebRequestError.GenerateLocal("Unable to zip mod binary prior to uploading");
					onError(obj);
				}
			});
		}

		public static void UploadModBinary_Zipped(int modId, EditableModfile modfileValues, string binaryZipLocation, bool setActiveBuild, Action<Modfile> onSuccess, Action<WebRequestError> onError)
		{
			DataStorage.ReadFile(binaryZipLocation, delegate(string rf_path, bool rf_success, byte[] rf_data)
			{
				string fileName = Path.GetFileName(binaryZipLocation);
				AddModfileParameters parameters = new AddModfileParameters();
				parameters.zippedBinaryData = BinaryUpload.Create(fileName, rf_data);
				if (modfileValues.version.isDirty)
				{
					parameters.version = modfileValues.version.value;
				}
				if (modfileValues.changelog.isDirty)
				{
					parameters.changelog = modfileValues.changelog.value;
				}
				if (modfileValues.metadataBlob.isDirty)
				{
					parameters.metadataBlob = modfileValues.metadataBlob.value;
				}
				parameters.isActiveBuild = setActiveBuild;
				DataStorage.GetFileSizeAndHash(binaryZipLocation, delegate(string fi_path, bool fi_success, long fi_fileSize, string fi_hash)
				{
					parameters.fileHash = fi_hash;
					APIClient.AddModfile(modId, parameters, onSuccess, onError);
				});
			});
		}

		private static void FetchAllResultsForQuery<T>(GetAllObjectsQuery<T> query, Action<List<T>> onSuccess, Action<WebRequestError> onError)
		{
			APIPaginationParameters pagination = new APIPaginationParameters
			{
				limit = 100,
				offset = 0
			};
			List<T> results = new List<T>();
			int requestCount = 0;
			query(pagination, delegate(RequestPage<T> r)
			{
				FetchQueryResultsRecursively(query, r, pagination, results, requestCount, onSuccess, onError);
			}, onError);
		}

		private static void FetchQueryResultsRecursively<T>(GetAllObjectsQuery<T> query, RequestPage<T> queryResult, APIPaginationParameters pagination, List<T> culmativeResults, int requestCount, Action<List<T>> onSuccess, Action<WebRequestError> onError)
		{
			culmativeResults.AddRange(queryResult.items);
			requestCount++;
			if (queryResult.items.Length < queryResult.size || requestCount > 10)
			{
				onSuccess(culmativeResults);
				return;
			}
			pagination.offset += pagination.limit;
			query(pagination, delegate(RequestPage<T> r)
			{
				FetchQueryResultsRecursively(query, r, pagination, culmativeResults, requestCount, onSuccess, onError);
			}, onError);
		}

		[Obsolete("Use ModManager.FetchAuthenticatedUserMods() instead.")]
		public static void GetAuthenticatedUserMods(Action<List<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			FetchAuthenticatedUserMods(onSuccess, onError);
		}

		[Obsolete("Use ModManager.DownloadAndUpdateSubscribedMods_Coroutine() instead.")]
		public static IEnumerator UpdateAllInstalledMods_Coroutine()
		{
			List<ModfileIdPair> installedModVersions = GetInstalledModVersions(false);
			List<int> list = new List<int>(installedModVersions.Count);
			foreach (ModfileIdPair item in installedModVersions)
			{
				list.Add(item.modId);
			}
			return DownloadAndUpdateMods_Coroutine(list);
		}

		[Obsolete("No longer supported by the mod.io API.")]
		public static void GetUserProfile(int userId, Action<UserProfile> onSuccess, Action<WebRequestError> onError)
		{
			if (UserAuthenticationData.instance.userId == userId)
			{
				UserProfile userProfile = CacheClient.LoadUserProfile(userId);
				if (userProfile != null)
				{
					if (onSuccess != null)
					{
						onSuccess(userProfile);
					}
					return;
				}
				Action<UserProfile> successCallback = delegate(UserProfile profile)
				{
					CacheClient.SaveUserProfile(profile);
					if (onSuccess != null)
					{
						onSuccess(profile);
					}
				};
				APIClient.GetAuthenticatedUser(successCallback, onError);
			}
			else if (onError != null)
			{
				onError(WebRequestError.GenerateLocal("Non-authenticated user profiles can no-longer be fetched."));
			}
		}

		[Obsolete("Refer to LocalUser.EnabledModIds instead.")]
		public static List<int> GetEnabledModIds()
		{
			return LocalUser.EnabledModIds;
		}

		[Obsolete("Refer to LocalUser.EnabledModIds instead.")]
		public static void SetEnabledModIds(IEnumerable<int> modIds)
		{
			if (modIds == null)
			{
				modIds = new int[0];
			}
			LocalUser.EnabledModIds = new List<int>(modIds);
			LocalUser.Save();
		}

		[Obsolete("Refer to LocalUser.SubscribedModIds instead.")]
		public static List<int> GetSubscribedModIds()
		{
			return LocalUser.SubscribedModIds;
		}

		[Obsolete("Refer to LocalUser.SubscribedModIds instead.")]
		public static void SetSubscribedModIds(IEnumerable<int> modIds)
		{
			if (modIds == null)
			{
				modIds = new int[0];
			}
			LocalUser.SubscribedModIds = new List<int>(modIds);
			LocalUser.Save();
		}

		[Obsolete("Use UninstallMod() instead.")]
		public static bool TryUninstallAllModVersions(int modId)
		{
			bool succeeded = false;
			UninstallMod(modId, delegate(bool s)
			{
				succeeded = s;
			});
			return succeeded;
		}

		[Obsolete("Use TryInstallMod(int, int, Action<bool>) instead.")]
		public static bool TryInstallMod(int modId, int modfileId, bool removeArchiveOnSuccess)
		{
			bool result = false;
			TryInstallMod(modId, modfileId, delegate(bool b)
			{
				result = b;
			});
			return result;
		}

		[Obsolete("Use TryUninstallModVersion(int, int, Action<bool>) instead.")]
		public static bool TryUninstallModVersion(int modId, int modfileId)
		{
			bool result = false;
			TryUninstallModVersion(modId, modfileId, delegate(bool b)
			{
				result = b;
			});
			return result;
		}

		[Obsolete("Use QueryInstalledModDirectories(bool, Action<List<string>>) instead.")]
		public static List<string> GetInstalledModDirectories(bool excludeDisabledMods)
		{
			List<string> result = null;
			QueryInstalledModDirectories(excludeDisabledMods, delegate(List<string> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use QueryInstalledModVersions(bool, Action<List<ModfileIdPair>>) instead.")]
		public static List<ModfileIdPair> GetInstalledModVersions(bool excludeDisabledMods)
		{
			List<ModfileIdPair> result = null;
			QueryInstalledModVersions(excludeDisabledMods, delegate(List<ModfileIdPair> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use QueryInstalledMods(bool, Action<IList<ModfileIdPair, string>>) instead.")]
		public static IEnumerable<KeyValuePair<ModfileIdPair, string>> IterateInstalledMods(IList<int> modIdFilter)
		{
			IEnumerable<KeyValuePair<ModfileIdPair, string>> result = null;
			QueryInstalledMods(modIdFilter, delegate(IList<KeyValuePair<ModfileIdPair, string>> r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use GetModProfiles(IList<int>, Action<ModProfile[]>, Action<WebRequestError>) instead.")]
		public static void GetModProfiles(IList<int> orderedIdList, Action<List<ModProfile>> onSuccess, Action<WebRequestError> onError)
		{
			GetModProfiles(orderedIdList, delegate(ModProfile[] result)
			{
				if (onSuccess != null)
				{
					List<ModProfile> obj = null;
					if (result != null)
					{
						obj = new List<ModProfile>(result);
					}
					onSuccess(obj);
				}
			}, onError);
		}
	}
}
