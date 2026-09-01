using System;
using System.Text;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class RemoteStorage
	{
		public static class Client
		{
			private static CallResult<RemoteStorageFileReadAsyncComplete_t> _remoteStorageFileReadAsyncCompleteT;

			private static CallResult<RemoteStorageFileShareResult_t> _remoteStorageFileShareResultT;

			private static CallResult<RemoteStorageFileWriteAsyncComplete_t> _remoteStorageFileWriteAsyncCompleteT;

			private static CallResult<RemoteStorageDownloadUGCResult_t> _remoteStorageDownloadUgcResultT;

			public static bool IsEnabledForAccount => false;

			public static bool IsEnabledForApp
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			public static bool IsEnabled => false;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static bool FileDelete(string file)
			{
				return false;
			}

			public static bool FileExists(string file)
			{
				return false;
			}

			public static bool FileForget(string file)
			{
				return false;
			}

			public static byte[] FileRead(string file)
			{
				return null;
			}

			public static string FileReadString(string fileName, Encoding encoding)
			{
				return null;
			}

			public static T FileReadJson<T>(string fileName, Encoding encoding)
			{
				return default(T);
			}

			public static void FileReadAsync(string file, Action<byte[], bool> callback)
			{
			}

			public static void FileShare(string file, Action<RemoteStorageFileShareResult_t, bool> callback)
			{
			}

			public static bool FileWrite(string file, byte[] data)
			{
				return false;
			}

			public static bool FileWrite(string file, string body, Encoding encoding)
			{
				return false;
			}

			public static bool FileWrite(string file, string body)
			{
				return false;
			}

			public static bool FileWrite(string fileName, object jsonObject, Encoding encoding)
			{
				return false;
			}

			public static bool FileWrite(string fileName, object jsonObject)
			{
				return false;
			}

			public static void FileWriteAsync(string file, byte[] data, Action<RemoteStorageFileWriteAsyncComplete_t, bool> callback)
			{
			}

			public static void FileWriteAsync(string file, string body, Encoding encoding, Action<RemoteStorageFileWriteAsyncComplete_t, bool> callback)
			{
			}

			public static void FileWriteAsync(string fileName, object jsonObject, Encoding encoding, Action<RemoteStorageFileWriteAsyncComplete_t, bool> callback)
			{
			}

			public static bool FileWriteStreamCancel(UGCFileWriteStreamHandle_t handle)
			{
				return false;
			}

			public static bool FileWriteStreamClose(UGCFileWriteStreamHandle_t handle)
			{
				return false;
			}

			public static UGCFileWriteStreamHandle_t FileWriteStreamOpen(string file)
			{
				return default(UGCFileWriteStreamHandle_t);
			}

			public static bool FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t handle, byte[] data)
			{
				return false;
			}

			public static int GetCachedUgcCount()
			{
				return 0;
			}

			public static UGCHandle_t GetCachedUgcHandle(int index)
			{
				return default(UGCHandle_t);
			}

			public static UGCHandle_t[] GetCashedUgcHandles()
			{
				return null;
			}

			public static int GetFileCount()
			{
				return 0;
			}

			public static RemoteStorageFile[] GetFiles()
			{
				return null;
			}

			public static RemoteStorageFile[] GetFiles(string extension)
			{
				return null;
			}

			public static DateTime GetFileTimestamp(string name)
			{
				return default(DateTime);
			}

			public static int GetLocalFileChangeCount()
			{
				return 0;
			}

			public static string GetLocalFileChange(int index, out ERemoteStorageLocalFileChange changeType, out ERemoteStorageFilePathType pathType)
			{
				changeType = default(ERemoteStorageLocalFileChange);
				pathType = default(ERemoteStorageFilePathType);
				return null;
			}

			public static bool GetQuota(out ulong totalBytes, out ulong remainingBytes)
			{
				totalBytes = default(ulong);
				remainingBytes = default(ulong);
				return false;
			}

			public static ERemoteStoragePlatform GetSyncPlatforms(string file)
			{
				return default(ERemoteStoragePlatform);
			}

			public static bool GetUgcDetails(UGCHandle_t handle, out AppId_t appId, out string name, out int size, out CSteamID owner)
			{
				appId = default(AppId_t);
				name = null;
				size = default(int);
				owner = default(CSteamID);
				return false;
			}

			public static bool GetUgcDownloadProgress(UGCHandle_t handle, out int downloaded, out int expected)
			{
				downloaded = default(int);
				expected = default(int);
				return false;
			}

			public static bool SetSyncPlatforms(string file, ERemoteStoragePlatform platform)
			{
				return false;
			}

			public static void UgcDownload(UGCHandle_t handle, uint priority, Action<RemoteStorageDownloadUGCResult_t, bool> callback)
			{
			}

			public static void UgcDownloadToLocation(UGCHandle_t handle, string location, uint priority, Action<RemoteStorageDownloadUGCResult_t, bool> callback)
			{
			}

			public static byte[] UgcRead(UGCHandle_t handle)
			{
				return null;
			}
		}
	}
}
