using Steamworks;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
	private void Start()
	{
		DebugStatus();
	}

	private void DebugStatus()
	{
		Debug.Log("IsCloudEnabledForAccount: " + SteamRemoteStorage.IsCloudEnabledForAccount());
		Debug.Log("IsCloudEnabledForApp: " + SteamRemoteStorage.IsCloudEnabledForApp());
		Debug.Log("Current number of files in use: " + SteamRemoteStorage.GetFileCount());
		ulong pnTotalBytes;
		ulong puAvailableBytes;
		SteamRemoteStorage.GetQuota(out pnTotalBytes, out puAvailableBytes);
		Debug.Log("Available space: " + puAvailableBytes + " / " + pnTotalBytes);
		for (int i = 0; i < 54; i++)
		{
			int pnFileSizeInBytes;
			Debug.Log("File name " + i + ": " + SteamRemoteStorage.GetFileNameAndSize(i, out pnFileSizeInBytes));
		}
	}
}
