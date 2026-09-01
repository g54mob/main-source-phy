using System;
using System.Threading.Tasks;
using BitCode;
using BitCode.Platform.Steamworks;
using Steamworks;
using UnityEngine;

namespace TFBGames
{
	public class SteamManager : IPlatformManager, IService
	{
		private const uint SteamId = 508440u;

		public bool Initialized { get; protected set; }

		public IPlatformServices Services { get; private set; }

		public async Task<SteamManager> BuildPlatform()
		{
			try
			{
				Services = await new SteamPlatformServicesBuilder(ServiceLocator.GetService<SharedServiceUpdater>(), new AppId_t(508440u)).WithSocial().WithInvites().WithDlc()
					.WithAchievements()
					.Build();
				Initialized = true;
			}
			catch (SteamNotInitializedException ex)
			{
				Debug.LogError(ex.Message);
				Initialized = false;
			}
			return this;
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
			if (Services is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}
}
