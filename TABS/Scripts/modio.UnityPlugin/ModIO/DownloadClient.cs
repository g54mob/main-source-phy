using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO
{
	public static class DownloadClient
	{
		private class DownloadProgressMarkerCollection
		{
			public int lastIndex;

			public int recordedCount;

			public float[] timeStamps;

			public long[] byteCounts;

			public DownloadProgressMarkerCollection(int markerCount)
			{
				lastIndex = -1;
				recordedCount = 0;
				timeStamps = new float[markerCount];
				byteCounts = new long[markerCount];
			}

			public DownloadProgressMarkerCollection()
				: this(0)
			{
			}
		}

		private class DownloadMonitorBehaviour : MonoBehaviour
		{
			public Coroutine coroutine;
		}

		private static DownloadMonitorBehaviour monitorBehaviour = null;

		public const int DOWNLOAD_SPEED_MARKER_COUNT = 10;

		public const float DOWNLOAD_SPEED_UPDATE_INTERVAL = 0.5f;

		public static Dictionary<ModfileIdPair, FileDownloadInfo> modfileDownloadMap = new Dictionary<ModfileIdPair, FileDownloadInfo>();

		private static Dictionary<ModfileIdPair, DownloadProgressMarkerCollection> modfileProgressMarkers = new Dictionary<ModfileIdPair, DownloadProgressMarkerCollection>();

		[Obsolete("Use PluginSettings.REQUEST_LOGGING instead.")]
		public static bool logAllRequests => PluginSettings.REQUEST_LOGGING.logAllResponses;

		public static event Action<ModfileIdPair, FileDownloadInfo> modfileDownloadStarted;

		public static event Action<ModfileIdPair, FileDownloadInfo> modfileDownloadSucceeded;

		public static event Action<ModfileIdPair, WebRequestError> modfileDownloadFailed;

		public static ImageRequest DownloadModLogo(ModProfile profile, LogoSize size)
		{
			return DownloadImage(profile.logoLocator.GetSizeURL(size));
		}

		public static ImageRequest DownloadModGalleryImage(ModProfile profile, string imageFileName, ModGalleryImageSize size)
		{
			ImageRequest result = null;
			if (profile.media == null)
			{
				Debug.LogWarning("[mod.io] The given mod profile has no media information");
			}
			else
			{
				GalleryImageLocator galleryImageWithFileName = profile.media.GetGalleryImageWithFileName(imageFileName);
				if (galleryImageWithFileName == null)
				{
					Debug.LogWarning("[mod.io] Unable to find mod gallery image with the file name '" + imageFileName + "' for the mod profile '" + profile.name + "'[" + profile.id + "]");
				}
				else
				{
					result = DownloadModGalleryImage(galleryImageWithFileName, size);
				}
			}
			return result;
		}

		public static ImageRequest DownloadModGalleryImage(GalleryImageLocator imageLocator, ModGalleryImageSize size)
		{
			return DownloadImage(imageLocator.GetSizeURL(size));
		}

		public static ImageRequest DownloadUserAvatar(UserProfile profile, UserAvatarSize size)
		{
			ImageRequest result = null;
			if (profile.avatarLocator == null || string.IsNullOrEmpty(profile.avatarLocator.GetSizeURL(size)))
			{
				Debug.LogWarning("[mod.io] User Profile has no associated avatar information");
			}
			else
			{
				result = DownloadImage(profile.avatarLocator.GetSizeURL(size));
			}
			return result;
		}

		public static ImageRequest DownloadYouTubeThumbnail(string youTubeId)
		{
			return DownloadImage(Utility.GenerateYouTubeThumbnailURL(youTubeId));
		}

		public static ImageRequest DownloadImage(string imageURL)
		{
			ImageRequest request = new ImageRequest();
			request.isDone = false;
			UnityWebRequest unityWebRequest = UnityWebRequest.Get(imageURL);
			unityWebRequest.downloadHandler = new DownloadHandlerTexture(readable: true);
			UnityWebRequestAsyncOperation operation = unityWebRequest.SendWebRequest();
			operation.completed += delegate
			{
				OnImageDownloadCompleted(operation, request);
			};
			return request;
		}

		private static void OnImageDownloadCompleted(UnityWebRequestAsyncOperation operation, ImageRequest request)
		{
			UnityWebRequest webRequest = operation.webRequest;
			request.isDone = true;
			if (webRequest.isNetworkError || webRequest.isHttpError)
			{
				request.error = WebRequestError.GenerateFromWebRequest(webRequest);
				request.NotifyFailed();
			}
			else
			{
				request.imageTexture = (webRequest.downloadHandler as DownloadHandlerTexture).texture;
				request.NotifySucceeded();
			}
		}

		public static FileDownloadInfo GetActiveModBinaryDownload(int modId, int modfileId)
		{
			ModfileIdPair key = new ModfileIdPair
			{
				modId = modId,
				modfileId = modfileId
			};
			if (modfileDownloadMap.TryGetValue(key, out var value))
			{
				return value;
			}
			return null;
		}

		public static FileDownloadInfo StartModBinaryDownload(int modId, int modfileId, string targetFilePath)
		{
			ModfileIdPair idPair = new ModfileIdPair
			{
				modId = modId,
				modfileId = modfileId
			};
			if (modfileDownloadMap.Keys.Contains(idPair))
			{
				Debug.LogWarning("[mod.io] Mod Binary with matching ids already downloading. TargetFilePath was not updated.");
			}
			else
			{
				modfileDownloadMap[idPair] = new FileDownloadInfo
				{
					target = targetFilePath,
					fileSize = -1L,
					request = null,
					isDone = false
				};
				modfileProgressMarkers[idPair] = new DownloadProgressMarkerCollection(10);
				APIClient.GetModfile(modId, modfileId, delegate(Modfile mf)
				{
					if (GetActiveModBinaryDownload(modId, modfileId) != null)
					{
						modfileDownloadMap[idPair].fileSize = mf.fileSize;
						DownloadModBinary_Internal(idPair, mf.downloadLocator.binaryURL);
					}
				}, delegate(WebRequestError e)
				{
					if (DownloadClient.modfileDownloadFailed != null)
					{
						DownloadClient.modfileDownloadFailed(idPair, e);
					}
				});
			}
			return modfileDownloadMap[idPair];
		}

		public static FileDownloadInfo StartModBinaryDownload(Modfile modfile, string targetFilePath)
		{
			ModfileIdPair modfileIdPair = new ModfileIdPair
			{
				modId = modfile.modId,
				modfileId = modfile.id
			};
			if (modfileDownloadMap.Keys.Contains(modfileIdPair))
			{
				Debug.LogWarning("[mod.io] Mod Binary for modfile is already downloading. TargetFilePath was not updated.");
			}
			else
			{
				modfileDownloadMap[modfileIdPair] = new FileDownloadInfo
				{
					target = targetFilePath,
					fileSize = modfile.fileSize,
					request = null,
					isDone = false
				};
				modfileProgressMarkers[modfileIdPair] = new DownloadProgressMarkerCollection(10);
				DownloadModBinary_Internal(modfileIdPair, modfile.downloadLocator.binaryURL);
			}
			return modfileDownloadMap[modfileIdPair];
		}

		private static void DownloadModBinary_Internal(ModfileIdPair idPair, string downloadURL)
		{
			FileDownloadInfo downloadInfo = modfileDownloadMap[idPair];
			downloadInfo.request = UnityWebRequest.Get(downloadURL);
			string target = downloadInfo.target;
			string dir = Path.GetDirectoryName(target);
			DataStorage.GetDirectoryExists(dir, delegate(string path, bool tempDirExists)
			{
				if (!tempDirExists)
				{
					DataStorage.CreateDirectory(dir, delegate
					{
						Proceed();
					});
				}
				else
				{
					Proceed();
				}
			});
			void Proceed()
			{
				downloadInfo.request.downloadHandler = new DownloadHandlerBuffer();
				downloadInfo.request.SendWebRequest().completed += delegate
				{
					OnModBinaryRequestCompleted(idPair);
				};
				StartMonitoringSpeed();
				if (DownloadClient.modfileDownloadStarted != null)
				{
					DownloadClient.modfileDownloadStarted(idPair, downloadInfo);
				}
			}
		}

		public static void CancelModBinaryDownload(int modId, int modfileId)
		{
			CancelModfileDownload_Internal(new ModfileIdPair
			{
				modId = modId,
				modfileId = modfileId
			});
		}

		public static void CancelAnyModBinaryDownloads(int modId)
		{
			List<ModfileIdPair> list = new List<ModfileIdPair>();
			foreach (KeyValuePair<ModfileIdPair, FileDownloadInfo> item in modfileDownloadMap)
			{
				if (item.Key.modId == modId)
				{
					list.Add(item.Key);
				}
			}
			foreach (ModfileIdPair item2 in list)
			{
				CancelModfileDownload_Internal(item2);
			}
		}

		private static void CancelModfileDownload_Internal(ModfileIdPair idPair)
		{
			FileDownloadInfo value = null;
			if (modfileDownloadMap.TryGetValue(idPair, out value))
			{
				if (value.request != null)
				{
					value.request.Abort();
					return;
				}
				value.wasAborted = true;
				value.isDone = true;
				modfileDownloadMap.Remove(idPair);
			}
		}

		private static void OnModBinaryRequestCompleted(ModfileIdPair idPair)
		{
			FileDownloadInfo downloadInfo = modfileDownloadMap[idPair];
			UnityWebRequest request = downloadInfo.request;
			downloadInfo.bytesPerSecond = 0L;
			if (request.isNetworkError || request.isHttpError)
			{
				if (request.error.ToUpper() == "USER ABORTED" || request.error.ToUpper() == "REQUEST ABORTED")
				{
					downloadInfo.wasAborted = true;
					FinalizeDownload(idPair, downloadInfo);
				}
				else
				{
					downloadInfo.error = WebRequestError.GenerateFromWebRequest(request);
					FinalizeDownload(idPair, downloadInfo);
				}
				return;
			}
			string tempFilePath = downloadInfo.target;
			string dir = Path.GetDirectoryName(tempFilePath);
			DataStorage.GetDirectoryExists(dir, delegate(string dirPath, bool tempDirExists)
			{
				if (!tempDirExists)
				{
					DataStorage.CreateDirectory(dir, delegate
					{
						Proceed();
					});
				}
				else
				{
					Proceed();
				}
			});
			void Proceed()
			{
				DataStorage.WriteFile(tempFilePath, request.downloadHandler.data, delegate(string path, bool writeTempFileSuccess)
				{
					if (!writeTempFileSuccess)
					{
						string errorMessage = "Download succeeded but failed to write temporary file\nTemporary file name: " + tempFilePath;
						downloadInfo.error = WebRequestError.GenerateLocal(errorMessage);
					}
					FinalizeDownload(idPair, downloadInfo);
				});
			}
		}

		private static void FinalizeDownload(ModfileIdPair idPair, FileDownloadInfo downloadInfo)
		{
			downloadInfo.bytesPerSecond = 0L;
			downloadInfo.isDone = true;
			if (downloadInfo.error == null && DownloadClient.modfileDownloadSucceeded != null)
			{
				DownloadClient.modfileDownloadSucceeded(idPair, downloadInfo);
			}
			else if (downloadInfo.error != null && DownloadClient.modfileDownloadFailed != null)
			{
				DownloadClient.modfileDownloadFailed(idPair, downloadInfo.error);
			}
			modfileDownloadMap.Remove(idPair);
			modfileProgressMarkers.Remove(idPair);
		}

		private static void StartMonitoringSpeed()
		{
			if (monitorBehaviour == null)
			{
				GameObject gameObject = new GameObject("[mod.io] Download Speed Monitor");
				monitorBehaviour = gameObject.AddComponent<DownloadMonitorBehaviour>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			if (monitorBehaviour.coroutine == null)
			{
				monitorBehaviour.coroutine = monitorBehaviour.StartCoroutine(SpeedMonitorCoroutine());
			}
		}

		private static IEnumerator SpeedMonitorCoroutine()
		{
			while (modfileDownloadMap.Count > 0)
			{
				int count = modfileDownloadMap.Count;
				int num = 0;
				FileDownloadInfo[] array = new FileDownloadInfo[count];
				DownloadProgressMarkerCollection[] array2 = new DownloadProgressMarkerCollection[count];
				foreach (KeyValuePair<ModfileIdPair, FileDownloadInfo> item in modfileDownloadMap)
				{
					DownloadProgressMarkerCollection value = null;
					if (!item.Value.isDone && modfileProgressMarkers.TryGetValue(item.Key, out value))
					{
						array[num] = item.Value;
						array2[num] = value;
						num++;
					}
				}
				for (int i = 0; i < num; i++)
				{
					FileDownloadInfo fileDownloadInfo = array[i];
					DownloadProgressMarkerCollection markers = array2[i];
					long bytesReceived = (long)((fileDownloadInfo.request == null) ? 0 : fileDownloadInfo.request.downloadedBytes);
					AddDownloadProgressMarker(markers, bytesReceived);
					fileDownloadInfo.bytesPerSecond = CalculateAverageDownloadSpeed(markers);
				}
				yield return new WaitForSecondsRealtime(0.5f);
			}
			monitorBehaviour.coroutine = null;
		}

		private static long CalculateAverageDownloadSpeed(DownloadProgressMarkerCollection markers)
		{
			if (markers.lastIndex < 0 || markers.recordedCount <= 1)
			{
				return 0L;
			}
			int num = 0;
			if (markers.recordedCount > markers.timeStamps.Length)
			{
				num = (markers.lastIndex + 1) % markers.timeStamps.Length;
			}
			float num2 = markers.timeStamps[num];
			long num3 = markers.byteCounts[num];
			float num4 = markers.timeStamps[markers.lastIndex];
			return (long)((float)(markers.byteCounts[markers.lastIndex] - num3) / (num4 - num2));
		}

		private static void AddDownloadProgressMarker(DownloadProgressMarkerCollection markers, long bytesReceived)
		{
			float unscaledTime = Time.unscaledTime;
			if (markers.lastIndex < 0 || !(unscaledTime - markers.timeStamps[markers.lastIndex] <= 0f))
			{
				if (markers.recordedCount <= 1 && bytesReceived == 0L)
				{
					markers.lastIndex = 0;
					markers.recordedCount = 1;
					markers.timeStamps[0] = unscaledTime;
					markers.byteCounts[0] = 0L;
				}
				else
				{
					markers.lastIndex++;
					markers.lastIndex %= markers.timeStamps.Length;
					markers.recordedCount++;
					markers.timeStamps[markers.lastIndex] = unscaledTime;
					markers.byteCounts[markers.lastIndex] = bytesReceived;
				}
			}
		}
	}
}
