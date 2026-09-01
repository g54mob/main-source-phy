using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public static class PlayFabDeviceUtil
	{
		private static bool _needsAttribution;

		private static bool _gatherDeviceInfo;

		private static bool _gatherScreenTime;

		private static void DoAttributeInstall(PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			if (_needsAttribution && !settings.DisableAdvertising)
			{
				AttributeInstallRequest attributeInstallRequest = new AttributeInstallRequest();
				switch (settings.AdvertisingIdType)
				{
				case "Adid":
					attributeInstallRequest.Adid = settings.AdvertisingIdValue;
					break;
				case "Idfa":
					attributeInstallRequest.Idfa = settings.AdvertisingIdValue;
					break;
				}
				PlayFabClientInstanceAPI playFabClientInstanceAPI = instanceApi as PlayFabClientInstanceAPI;
				if (playFabClientInstanceAPI != null)
				{
					playFabClientInstanceAPI.AttributeInstall(attributeInstallRequest, OnAttributeInstall, null, settings);
				}
				else
				{
					PlayFabClientAPI.AttributeInstall(attributeInstallRequest, OnAttributeInstall, null, settings);
				}
			}
		}

		private static void OnAttributeInstall(AttributeInstallResult result)
		{
			((PlayFabApiSettings)result.CustomData).AdvertisingIdType += "_Successful";
		}

		private static void SendDeviceInfoToPlayFab(PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			if (!settings.DisableDeviceInfo && _gatherDeviceInfo)
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
				DeviceInfoRequest deviceInfoRequest = new DeviceInfoRequest();
				deviceInfoRequest.Info = plugin.DeserializeObject<Dictionary<string, object>>(plugin.SerializeObject(new PlayFabDataGatherer()));
				DeviceInfoRequest request = deviceInfoRequest;
				PlayFabClientInstanceAPI playFabClientInstanceAPI = instanceApi as PlayFabClientInstanceAPI;
				if (playFabClientInstanceAPI != null)
				{
					playFabClientInstanceAPI.ReportDeviceInfo(request, null, OnGatherFail, settings);
				}
				else
				{
					PlayFabClientAPI.ReportDeviceInfo(request, null, OnGatherFail, settings);
				}
			}
		}

		private static void OnGatherFail(PlayFabError error)
		{
			Debug.Log("OnGatherFail: " + error.GenerateErrorReport());
		}

		public static void OnPlayFabLogin(PlayFabResultCommon result, PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			LoginResult loginResult = result as LoginResult;
			RegisterPlayFabUserResult registerPlayFabUserResult = result as RegisterPlayFabUserResult;
			if (loginResult == null && registerPlayFabUserResult == null)
			{
				return;
			}
			UserSettings settingsForUser = null;
			string playFabId = null;
			string entityId = null;
			string entityType = null;
			if (loginResult != null)
			{
				settingsForUser = loginResult.SettingsForUser;
				playFabId = loginResult.PlayFabId;
				if (loginResult.EntityToken != null)
				{
					entityId = loginResult.EntityToken.Entity.Id;
					entityType = loginResult.EntityToken.Entity.Type;
				}
			}
			else if (registerPlayFabUserResult != null)
			{
				settingsForUser = registerPlayFabUserResult.SettingsForUser;
				playFabId = registerPlayFabUserResult.PlayFabId;
				if (registerPlayFabUserResult.EntityToken != null)
				{
					entityId = registerPlayFabUserResult.EntityToken.Entity.Id;
					entityType = registerPlayFabUserResult.EntityToken.Entity.Type;
				}
			}
			_OnPlayFabLogin(settingsForUser, playFabId, entityId, entityType, settings, instanceApi);
		}

		private static void _OnPlayFabLogin(UserSettings settingsForUser, string playFabId, string entityId, string entityType, PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
			_needsAttribution = (_gatherDeviceInfo = (_gatherScreenTime = false));
			if (settingsForUser != null)
			{
				_needsAttribution = settingsForUser.NeedsAttribution;
				_gatherDeviceInfo = settingsForUser.GatherDeviceInfo;
				_gatherScreenTime = settingsForUser.GatherFocusInfo;
			}
			if (settings.AdvertisingIdType != null && settings.AdvertisingIdValue != null)
			{
				DoAttributeInstall(settings, instanceApi);
			}
			else
			{
				GetAdvertIdFromUnity(settings, instanceApi);
			}
			SendDeviceInfoToPlayFab(settings, instanceApi);
			if (!string.IsNullOrEmpty(entityId) && !string.IsNullOrEmpty(entityType) && _gatherScreenTime)
			{
				PlayFabHttp.InitializeScreenTimeTracker(entityId, entityType, playFabId);
			}
			else
			{
				settings.DisableFocusTimeCollection = true;
			}
		}

		private static void GetAdvertIdFromUnity(PlayFabApiSettings settings, IPlayFabInstanceApi instanceApi)
		{
		}
	}
}
