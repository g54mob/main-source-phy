using System;
using System.Collections.Generic;
using Oculus.Platform.Models;
using UnityEngine;

namespace Oculus.Platform
{
	public sealed class Core
	{
		private static bool IsPlatformInitialized = false;

		public static bool LogMessages = false;

		public static string PlatformUninitializedError = "This function requires an initialized Oculus Platform. Run Oculus.Platform.Core.[Initialize|AsyncInitialize] and try again.";

		public static bool IsInitialized()
		{
			return IsPlatformInitialized;
		}

		internal static void ForceInitialized()
		{
			IsPlatformInitialized = true;
		}

		private static string getAppID(string appId = null)
		{
			string appIDFromConfig = GetAppIDFromConfig();
			if (string.IsNullOrEmpty(appId))
			{
				if (string.IsNullOrEmpty(appIDFromConfig))
				{
					throw new UnityException("Update your app id by selecting 'Oculus Platform' -> 'Edit Settings'");
				}
				appId = appIDFromConfig;
			}
			else if (!string.IsNullOrEmpty(appIDFromConfig))
			{
				Debug.LogWarningFormat("The 'Oculus App Id ({0})' field in 'Oculus Platform/Edit Settings' is being overridden by the App Id ({1}) that you passed in to Platform.Core.Initialize.  You should only specify this in one place.  We recommend the menu location.", appIDFromConfig, appId);
			}
			return appId;
		}

		public static Request<PlatformInitialize> AsyncInitialize(string appId = null)
		{
			appId = getAppID(appId);
			Request<PlatformInitialize> request;
			if (UnityEngine.Application.isEditor && PlatformSettings.UseStandalonePlatform)
			{
				request = new StandalonePlatform().InitializeInEditor();
			}
			else if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor || UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer)
			{
				request = new WindowsPlatform().AsyncInitialize(appId);
			}
			else
			{
				if (UnityEngine.Application.platform != RuntimePlatform.Android)
				{
					throw new NotImplementedException("Oculus platform is not implemented on this platform yet.");
				}
				request = new AndroidPlatform().AsyncInitialize(appId);
			}
			IsPlatformInitialized = request != null;
			if (!IsPlatformInitialized)
			{
				throw new UnityException("Oculus Platform failed to initialize.");
			}
			if (LogMessages)
			{
				Debug.LogWarning("Oculus.Platform.Core.LogMessages is set to true. This will cause extra heap allocations, and should not be used outside of testing and debugging.");
			}
			new GameObject("Oculus.Platform.CallbackRunner").AddComponent<CallbackRunner>();
			return request;
		}

		public static Request<PlatformInitialize> AsyncInitialize(string accessToken, Dictionary<InitConfigOptions, bool> initConfigOptions, string appId = null)
		{
			appId = getAppID(appId);
			if (UnityEngine.Application.isEditor || UnityEngine.Application.platform == RuntimePlatform.WindowsEditor || UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer)
			{
				Request<PlatformInitialize> request = new StandalonePlatform().AsyncInitializeWithAccessTokenAndOptions(appId, accessToken, initConfigOptions);
				IsPlatformInitialized = request != null;
				if (!IsPlatformInitialized)
				{
					throw new UnityException("Oculus Standalone Platform failed to initialize. Check if the access token or app id is correct.");
				}
				if (LogMessages)
				{
					Debug.LogWarning("Oculus.Platform.Core.LogMessages is set to true. This will cause extra heap allocations, and should not be used outside of testing and debugging.");
				}
				new GameObject("Oculus.Platform.CallbackRunner").AddComponent<CallbackRunner>();
				return request;
			}
			throw new NotImplementedException("Initializing with access token is not implemented on this platform yet.");
		}

		public static void Initialize(string appId = null)
		{
			appId = getAppID(appId);
			if (UnityEngine.Application.isEditor && PlatformSettings.UseStandalonePlatform)
			{
				IsPlatformInitialized = new StandalonePlatform().InitializeInEditor() != null;
			}
			else if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor || UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer)
			{
				IsPlatformInitialized = new WindowsPlatform().Initialize(appId);
			}
			else
			{
				if (UnityEngine.Application.platform != RuntimePlatform.Android)
				{
					throw new NotImplementedException("Oculus platform is not implemented on this platform yet.");
				}
				IsPlatformInitialized = new AndroidPlatform().Initialize(appId);
			}
			if (!IsPlatformInitialized)
			{
				throw new UnityException("Oculus Platform failed to initialize.");
			}
			if (LogMessages)
			{
				Debug.LogWarning("Oculus.Platform.Core.LogMessages is set to true. This will cause extra heap allocations, and should not be used outside of testing and debugging.");
			}
			new GameObject("Oculus.Platform.CallbackRunner").AddComponent<CallbackRunner>();
		}

		private static string GetAppIDFromConfig()
		{
			if (UnityEngine.Application.platform == RuntimePlatform.Android)
			{
				return PlatformSettings.MobileAppID;
			}
			if (PlatformSettings.UseMobileAppIDInEditor)
			{
				return PlatformSettings.MobileAppID;
			}
			return PlatformSettings.AppID;
		}
	}
}
