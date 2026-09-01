using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ModIO
{
	public static class DataUpdater
	{
		[Serializable]
		private struct GenericJSONObject
		{
			[JsonExtensionData]
			public IDictionary<string, JToken> data;
		}

		public static void UpdateFromVersion(ModIOVersion lastRunVersion)
		{
			if (lastRunVersion < new ModIOVersion(2, 1))
			{
				Update_2_0_to_2_1_UserData();
			}
		}

		private static void Update_2_0_to_2_1_UserData()
		{
			Debug.Log("[mod.io] Attempting 2.0->2.1 UserData update.");
			byte[] fileData = null;
			UserDataStorage.ReadFile(LocalUser.FILENAME, delegate(string path, bool success, byte[] data)
			{
				fileData = data;
			});
			if (fileData != null && fileData.Length > 0)
			{
				Debug.Log("[mod.io] Aborting UserData update. FileExists: '" + LocalUser.FILENAME + "' [" + ValueFormatting.ByteCount(fileData.Length, null) + "]");
			}
			LocalUser instance = default(LocalUser);
			string text = null;
			text = ModManager.PERSISTENTDATA_FILEPATH;
			GenericJSONObject jsonObject;
			if (IOUtilities.TryReadJsonObjectFile<GenericJSONObject>(text, out jsonObject))
			{
				int[] fieldData = null;
				if (TryGetArrayField<int[]>(jsonObject, "subscribedModIds", out fieldData))
				{
					instance.subscribedModIds = new List<int>(fieldData);
				}
				if (TryGetArrayField<int[]>(jsonObject, "enabledModIds", out fieldData))
				{
					instance.enabledModIds = new List<int>(fieldData);
				}
			}
			text = IOUtilities.CombinePath(CacheClient.cacheDirectory, "browser_manifest.data");
			if (IOUtilities.TryReadJsonObjectFile<GenericJSONObject>(text, out jsonObject))
			{
				List<int> fieldData2 = null;
				if (TryGetArrayField<List<int>>(jsonObject, "queuedSubscribes", out fieldData2))
				{
					instance.queuedSubscribes = new List<int>(fieldData2);
				}
				if (TryGetArrayField<List<int>>(jsonObject, "queuedUnsubscribes", out fieldData2))
				{
					instance.queuedUnsubscribes = new List<int>(fieldData2);
				}
			}
			text = UserAuthenticationData.FILE_LOCATION;
			if (IOUtilities.TryReadJsonObjectFile<GenericJSONObject>(text, out jsonObject))
			{
				int num = -1;
				if (jsonObject.data.ContainsKey("userId"))
				{
					num = (int)jsonObject.data["userId"];
				}
				instance.profile = null;
				if (num != -1)
				{
					instance.profile = CacheClient.LoadUserProfile(num);
				}
				if (jsonObject.data.ContainsKey("token"))
				{
					instance.oAuthToken = (string)jsonObject.data["token"];
				}
				if (jsonObject.data.ContainsKey("wasTokenRejected"))
				{
					instance.wasTokenRejected = (bool)jsonObject.data["wasTokenRejected"];
				}
				IOUtilities.DeleteFile(text);
			}
			LocalUser.instance = instance;
			LocalUser.isLoaded = true;
			LocalUser.Save();
			Debug.Log("[mod.io] UserData updated completed.");
		}

		private static bool TryGetArrayField<T>(GenericJSONObject jsonObject, string fieldName, out T fieldData)
		{
			fieldData = default(T);
			JArray jArray;
			if (jsonObject.data.ContainsKey(fieldName) && (jArray = jsonObject.data[fieldName] as JArray) != null)
			{
				fieldData = jArray.ToObject<T>();
				return true;
			}
			return false;
		}
	}
}
