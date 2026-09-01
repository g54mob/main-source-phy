using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitCode;
using BitCode.Dlc;
using BitCode.Extensions;
using BitCode.Platform.Steamworks.Dlc;
using BitCode.Users;
using Steamworks;
using UnityEngine;

namespace TFBGames
{
	public class SteamDlcManagerService : IDlcManagerService, IDlcManager, IPlatformService, IService
	{
		private SteamDlcManager dlcManager;

		private readonly List<string> validatedDlcIds = new List<string>();

		public bool NeedsUserForDlc => true;

		public string AprilFoolsBugsDlcId => "1270340";

		public event Action<string> PreGotAccessToDlc;

		public event Action<string> GotAccessToDlc;

		public event Action PreLostAccessToAllDlc;

		public event Action LostAccessToAllDlc;

		public event Action<IDlc> InstalledDlc;

		public event Action<IPlatformService, Exception> InternalErrorOccurred;

		public void OnAwake()
		{
			dlcManager = ServiceLocator.GetService<IPlatformManager>().Services.DlcManager as SteamDlcManager;
			dlcManager.InstalledDlc += OnInstalledDlc;
			Initialize();
		}

		public void Initialize()
		{
			dlcManager?.Initialize();
		}

		public void GetDlcForUserAsync(ILocalAccount userAccount, Action<IDlc[], Exception> doneCallback)
		{
			dlcManager.GetDlcForUserAsync(userAccount, doneCallback);
		}

		public Task<IDlc[]> GetDlcForUserAsync(ILocalAccount userAccount)
		{
			return dlcManager.GetDlcForUserAsync(userAccount);
		}

		public void OnStart()
		{
		}

		public void OnRegister()
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
			dlcManager.InstalledDlc -= OnInstalledDlc;
		}

		public void HasAccessToDlc(string dlcId, Action<bool, Exception> doneCallback)
		{
			if (validatedDlcIds.Contains(dlcId))
			{
				doneCallback?.Invoke(arg1: true, null);
				return;
			}
			try
			{
				bool flag = dlcManager.CheckDlcInstalled(new AppId_t(Convert.ToUInt32(dlcId)));
				if (flag)
				{
					AddDlcIdToCache(dlcId);
				}
				doneCallback?.Invoke(flag, null);
			}
			catch (Exception ex)
			{
				Debug.LogErrorFormat("Failed to get the DLC.\n{0}", ex);
				doneCallback?.Invoke(arg1: false, ex);
			}
		}

		private void OnInstalledDlc(IDlc dlc)
		{
			if (dlc != null)
			{
				HasAccessToDlc(dlc.Id, null);
			}
			this.InstalledDlc?.SafelyInvoke(dlc);
		}

		private void ClearCache()
		{
			validatedDlcIds.Clear();
			this.PreLostAccessToAllDlc?.Invoke();
			this.LostAccessToAllDlc?.Invoke();
		}

		private void AddDlcIdToCache(string productId)
		{
			if (!validatedDlcIds.Contains(productId))
			{
				validatedDlcIds.Add(productId);
				this.PreGotAccessToDlc?.Invoke(productId);
				this.GotAccessToDlc?.Invoke(productId);
			}
		}
	}
}
