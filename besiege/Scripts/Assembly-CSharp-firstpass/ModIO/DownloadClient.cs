using System;
using System.Collections;
using System.Collections.Generic;
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

		public const int DOWNLOAD_SPEED_MARKER_COUNT = 10;

		public const float DOWNLOAD_SPEED_UPDATE_INTERVAL = 0.5f;

		private static DownloadMonitorBehaviour monitorBehaviour = null;

		public static Dictionary<ModfileIdPair, FileDownloadInfo> modfileDownloadMap = new Dictionary<ModfileIdPair, FileDownloadInfo>();

		private static Dictionary<ModfileIdPair, DownloadProgressMarkerCollection> modfileProgressMarkers = new Dictionary<ModfileIdPair, DownloadProgressMarkerCollection>();

		[Obsolete("Use PluginSettings.REQUEST_LOGGING instead.")]
		public static bool logAllRequests
		{
			get
			{
				return PluginSettings.REQUEST_LOGGING.logAllResponses;
			}
		}

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
			ImageRequest imageRequest = null;
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
			ImageRequest imageRequest = null;
			string imageURL = Utility.GenerateYouTubeThumbnailURL(youTubeId);
			return DownloadImage(imageURL);
		}

		public static ImageRequest DownloadImage(string imageURL)
		{
			ImageRequest request = new ImageRequest();
			request.isDone = false;
			UnityWebRequest unityWebRequest = UnityWebRequest.Get(imageURL);
			unityWebRequest.downloadHandler = new DownloadHandlerTexture(true);
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
			if (webRequest.isNetworkError() || webRequest.isHttpError())
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
			FileDownloadInfo value;
			if (modfileDownloadMap.TryGetValue(key, out value))
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
					FileDownloadInfo activeModBinaryDownload = GetActiveModBinaryDownload(modId, modfileId);
					if (activeModBinaryDownload != null)
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
			string tempFilePath = downloadInfo.target + ".download";
			DataStorage.WriteFile(tempFilePath, new byte[0], delegate(string p, bool success)
			{
				if (success)
				{
					downloadInfo.request.downloadHandler = new FileDownloadHandler(tempFilePath);
					UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = downloadInfo.request.SendWebRequest();
					unityWebRequestAsyncOperation.completed += delegate
					{
						OnModBinaryRequestCompleted(idPair);
					};
					StartMonitoringSpeed();
					if (DownloadClient.modfileDownloadStarted != null)
					{
						DownloadClient.modfileDownloadStarted(idPair, downloadInfo);
					}
				}
				else if (DownloadClient.modfileDownloadFailed != null)
				{
					string errorMessage = "Failed to create download file on disk.\nSource: " + downloadURL + "\nDestination: " + tempFilePath + "\n\n";
					DownloadClient.modfileDownloadFailed(idPair, WebRequestError.GenerateLocal(errorMessage));
				}
			});
		}

		public static void CancelModBinaryDownload(int modId, int modfileId)
		{
			ModfileIdPair idPair = new ModfileIdPair
			{
				modId = modId,
				modfileId = modfileId
			};
			CancelModfileDownload_Internal(idPair);
		}

		public static void CancelAnyModBinaryDownloads(int modId, Action onDownloadsCanceled)
		{
			List<ModfileIdPair> list = new List<ModfileIdPair>();
			foreach (KeyValuePair<ModfileIdPair, FileDownloadInfo> item in modfileDownloadMap)
			{
				if (item.Key.modId == modId)
				{
					list.Add(item.Key);
				}
			}
			List<UnityWebRequest> list2 = new List<UnityWebRequest>();
			foreach (ModfileIdPair item2 in list)
			{
				UnityWebRequest unityWebRequest = CancelModfileDownload_Internal(item2);
				if (unityWebRequest != null)
				{
					list2.Add(unityWebRequest);
				}
			}
			WebRequestDispatcher.Dispatch(WaitForPendingDownloads(list2, onDownloadsCanceled));
		}

		private static IEnumerator WaitForPendingDownloads(List<UnityWebRequest> pendingRequests, Action onDownloadsCanceled)
		{
			foreach (UnityWebRequest request in pendingRequests)
			{
				while (!request.isDone)
				{
					yield return null;
				}
				yield return null;
				yield return null;
				((FileDownloadHandler)request.downloadHandler).Dispose();
			}
			if (onDownloadsCanceled != null)
			{
				onDownloadsCanceled();
			}
		}

		private static UnityWebRequest CancelModfileDownload_Internal(ModfileIdPair idPair)
		{
			FileDownloadInfo value = null;
			if (modfileDownloadMap.TryGetValue(idPair, out value))
			{
				if (value.request != null)
				{
					value.request.Abort();
					return value.request;
				}
				value.wasAborted = true;
				value.isDone = true;
				modfileDownloadMap.Remove(idPair);
			}
			return null;
		}

		private static void OnModBinaryRequestCompleted(ModfileIdPair idPair)
		{
			FileDownloadInfo downloadInfo = modfileDownloadMap[idPair];
			UnityWebRequest request = downloadInfo.request;
			downloadInfo.bytesPerSecond = 0L;
			if (request.isNetworkError() || request.isHttpError())
			{
				if (request.error.ToUpper() == "USER ABORTED" || request.error.ToUpper() == "REQUEST ABORTED")
				{
					downloadInfo.wasAborted = true;
					downloadInfo.error = WebRequestError.GenerateLocal("User aborted download");
					FinalizeDownload(idPair, downloadInfo);
				}
				else
				{
					downloadInfo.error = WebRequestError.GenerateFromWebRequest(request);
					FinalizeDownload(idPair, downloadInfo);
				}
				return;
			}
			DataStorage.MoveFile(downloadInfo.target + ".download", downloadInfo.target, delegate(string src, string dst, bool success)
			{
				if (!success)
				{
					string errorMessage = "Download succeeded but failed to rename from temporary file name.\nTemporary file name: " + downloadInfo.target + ".download";
					downloadInfo.error = WebRequestError.GenerateLocal(errorMessage);
				}
				FinalizeDownload(idPair, downloadInfo);
			});
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
				int downloadCount = modfileDownloadMap.Count;
				int monitoredDownloadCount = 0;
				FileDownloadInfo[] infos = new FileDownloadInfo[downloadCount];
				DownloadProgressMarkerCollection[] markerCollections = new DownloadProgressMarkerCollection[downloadCount];
				foreach (KeyValuePair<ModfileIdPair, FileDownloadInfo> kvp in modfileDownloadMap)
				{
					DownloadProgressMarkerCollection markers = null;
					if (!kvp.Value.isDone && modfileProgressMarkers.TryGetValue(kvp.Key, out markers))
					{
						infos[monitoredDownloadCount] = kvp.Value;
						markerCollections[monitoredDownloadCount] = markers;
						monitoredDownloadCount++;
					}
				}
				for (int i = 0; i < monitoredDownloadCount; i++)
				{
					FileDownloadInfo downloadInfo = infos[i];
					DownloadProgressMarkerCollection markers2 = markerCollections[i];
					long bytesReceived = (long)((downloadInfo.request != null) ? downloadInfo.request.downloadedBytes : 0);
					AddDownloadProgressMarker(markers2, bytesReceived);
					downloadInfo.bytesPerSecond = CalculateAverageDownloadSpeed(markers2);
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
			long num5 = markers.byteCounts[markers.lastIndex];
			return (long)((float)(num5 - num3) / (num4 - num2));
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
