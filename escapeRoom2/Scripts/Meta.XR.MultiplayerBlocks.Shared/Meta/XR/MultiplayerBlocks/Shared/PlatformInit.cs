using System;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

namespace Meta.XR.MultiplayerBlocks.Shared
{
	public static class PlatformInit
	{
		private static PlatformInfo _info;

		public static BBPlatformInitStatus status { get; private set; }

		public static void GetEntitlementInformation(Action<PlatformInfo> callback)
		{
			if (status == BBPlatformInitStatus.Succeeded)
			{
				callback(_info);
				return;
			}
			try
			{
				status = BBPlatformInitStatus.Initializing;
				Core.AsyncInitialize().OnComplete(InitializeComplete);
			}
			catch (Exception ex)
			{
				status = BBPlatformInitStatus.Failed;
				Debug.LogError(ex.Message + "\n" + ex.StackTrace);
				callback(_info);
			}
			void CheckEntitlement(Message msg)
			{
				if (!msg.IsError)
				{
					Users.GetAccessToken().OnComplete(GetAccessTokenComplete);
				}
				else
				{
					status = BBPlatformInitStatus.Failed;
					Error error = msg.GetError();
					Debug.LogError($"Failed entitlement check: {error.Code} - {error.Message}");
					_info.IsEntitled = false;
					callback(_info);
				}
			}
			void GetAccessTokenComplete(Message<string> msg)
			{
				if (string.IsNullOrEmpty(msg.Data))
				{
					string text = "Token is null or empty.";
					if (msg.IsError)
					{
						Error error = msg.GetError();
						text = $"{error.Code} - {error.Message}";
					}
					status = BBPlatformInitStatus.Failed;
					Debug.LogError("Failed to retrieve access token: " + text);
					_info.IsEntitled = false;
					callback(_info);
				}
				else
				{
					string accessToken = msg.Data;
					Users.GetLoggedInUser().OnComplete(delegate(Message<User> message)
					{
						if (!message.IsError)
						{
							_info.IsEntitled = true;
							_info.Token = accessToken;
							_info.OculusUser = message.Data;
							callback(_info);
							status = BBPlatformInitStatus.Succeeded;
						}
						else
						{
							Error error2 = message.GetError();
							Debug.LogWarning("GetLoggedInUser: failed with message, " + error2.Message);
							_info.IsEntitled = false;
							status = BBPlatformInitStatus.Failed;
							callback(_info);
						}
					});
				}
			}
			void InitializeComplete(Message<PlatformInitialize> msg)
			{
				PlatformInitialize data = msg.Data;
				if (data == null || data.Result != PlatformInitializeResult.Success)
				{
					status = BBPlatformInitStatus.Failed;
					Debug.LogError("Failed to initialize OvrPlatform - " + msg.GetError().Message);
					_info.IsEntitled = false;
					callback(_info);
				}
				else
				{
					Entitlements.IsUserEntitledToApplication().OnComplete(CheckEntitlement);
				}
			}
		}
	}
}
