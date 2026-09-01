using System;
using BitCode.Users;
using UnityEngine;

namespace TFBGames
{
	public abstract class UserSettingsSaveManager<TData> : IService
	{
		protected int loadFailCount;

		private AccountManager accountManager;

		private FileIOWrapper fileIO;

		private bool isLoading;

		private int loadDelayCount;

		private bool isSaving;

		private bool hasPendingSave;

		private float? saveDelayTime;

		protected ulong? userId;

		public bool IsReady { get; private set; }

		protected abstract string FileName { get; }

		protected abstract int MaxTryLoad { get; }

		protected abstract int DelayLoadFrames { get; }

		protected abstract TData Data { get; set; }

		protected bool IsLoading => isLoading;

		protected bool IsSaving => isSaving;

		public event Action<bool> Saved;

		public virtual void OnRegister()
		{
		}

		public virtual void OnAwake()
		{
		}

		public virtual void OnFixedUpdate()
		{
		}

		public virtual void OnLateUpdate()
		{
		}

		public virtual void OnStart()
		{
			fileIO = ServiceLocator.GetService<FileIOWrapper>();
			accountManager = ServiceLocator.GetService<AccountManager>();
			accountManager.PreActiveAccountChange += OnPreActiveAccountChange;
			accountManager.ActiveAccountChanged += OnActiveAccountChanged;
			accountManager.FireWhenAccountIsSelected(OnAccountIsSelected);
		}

		public virtual void UnRegister()
		{
			if (accountManager != null)
			{
				accountManager.PreActiveAccountChange -= OnPreActiveAccountChange;
				accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
			accountManager = null;
		}

		public virtual void OnUpdate()
		{
			if (!(accountManager == null) && !(fileIO == null) && fileIO.IsReady && userId.HasValue && !isLoading && !isSaving)
			{
				if (!IsReady && loadDelayCount++ > DelayLoadFrames)
				{
					Load();
				}
				else if (hasPendingSave)
				{
					SaveInternal();
				}
				else if (saveDelayTime.HasValue && saveDelayTime.Value < Time.realtimeSinceStartup)
				{
					Save();
				}
			}
		}

		public virtual void Save()
		{
			if (IsReady && !(accountManager == null) && !(fileIO == null) && fileIO.IsReady && userId.HasValue)
			{
				if (isLoading || isSaving)
				{
					hasPendingSave = true;
				}
				else
				{
					SaveInternal();
				}
			}
		}

		protected abstract void SetUserIdFromAccount(ILocalAccount account);

		protected abstract void ClearCurrentUserData();

		protected abstract bool OnLoadedData(object loadedData, Exception exception);

		protected virtual void OnAccountIsSelected(ILocalAccount account)
		{
			SetUserIdFromAccount(account);
			ResetLoadCounters();
			if (account == null)
			{
				IsReady = true;
			}
		}

		protected virtual void OnPreActiveAccountChange(ILocalAccount account)
		{
			ClearCurrentUserData();
			CancelPendingSaves();
			ResetLoadCounters();
			IsReady = false;
		}

		protected virtual void OnActiveAccountChanged(ILocalAccount account)
		{
			SetUserIdFromAccount(account);
			CancelPendingSaves();
			ResetLoadCounters();
			if (account == null)
			{
				IsReady = true;
			}
		}

		protected virtual void Load()
		{
			CancelPendingSaves();
			isLoading = true;
			fileIO.DeserializeWithBinaryFormatter<TData>(FileName, FileHandlingFileType.PlayerPrefsOrTABSSave, delegate(object loadedData, Exception exception)
			{
				bool flag = OnLoadedData(loadedData, exception);
				if (!flag)
				{
					Debug.LogErrorFormat("Load failed.\n{0}", exception);
				}
				OnLoadingDone(flag);
			});
		}

		protected virtual void OnLoadingDone(bool success)
		{
			CancelPendingSaves();
			isLoading = false;
			if (success || ++loadFailCount >= MaxTryLoad)
			{
				IsReady = true;
			}
		}

		protected virtual void SaveInternal()
		{
			CancelPendingSaves();
			if (Data == null)
			{
				return;
			}
			isSaving = true;
			fileIO.SerializeWithBinaryFormatter(FileName, Data, FileHandlingFileType.PlayerPrefsOrTABSSave, delegate(Exception exception)
			{
				OnSavingDone(exception == null);
				if (exception != null)
				{
					Debug.LogErrorFormat($"Save failed.\n{FileName}\n{exception}");
				}
			});
		}

		protected virtual void SaveAfterDelay(float delay)
		{
			saveDelayTime = Time.realtimeSinceStartup + delay;
		}

		protected virtual void OnSavingDone(bool success)
		{
			isSaving = false;
			this.Saved?.Invoke(success);
		}

		protected virtual void CancelPendingSaves()
		{
			hasPendingSave = false;
			saveDelayTime = null;
		}

		protected virtual void ResetLoadCounters()
		{
			loadDelayCount = 0;
			loadFailCount = 0;
		}
	}
}
