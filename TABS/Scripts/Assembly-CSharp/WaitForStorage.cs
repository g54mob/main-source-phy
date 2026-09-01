using System;
using BitCode.Users;
using TFBGames;
using UnityEngine;

public class WaitForStorage : ServicePrefab
{
	private const float MinFps = 5f;

	private const float MaxElapsedTime = 0.2f;

	[SerializeField]
	[Tooltip("Max time to wait for storage (seconds).")]
	protected float timeout = 10f;

	private IPlatformUtils platformUtils;

	private IPlayerPrefsPlatform playerPrefs;

	private AccountManager accountManager;

	private FileIOWrapper fileIO;

	private ISaveLoaderService saveLoader;

	private float timeoutElapsedTime;

	private bool isReady;

	private bool accountManagerWasReady;

	private bool playerPrefsWasReady;

	private bool fileIOWasReady;

	private bool saveLoaderWasReady;

	private event Action readyCallbacks;

	public void FireWhenReady(Action callback)
	{
		if (callback != null)
		{
			isReady = true;
			if (isReady)
			{
				callback();
			}
			else
			{
				readyCallbacks += callback;
			}
		}
	}

	public override void OnAwake()
	{
		base.OnAwake();
		platformUtils = ServiceLocator.GetService<IPlatformUtils>();
		playerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
		fileIO = ServiceLocator.GetService<FileIOWrapper>();
		saveLoader = ServiceLocator.GetService<ISaveLoaderService>();
		accountManager = ServiceLocator.GetService<AccountManager>();
		accountManager.PreActiveAccountChange += OnPreActiveAccountChange;
	}

	public override void UnRegister()
	{
		base.UnRegister();
		if (accountManager != null)
		{
			accountManager.PreActiveAccountChange -= OnPreActiveAccountChange;
		}
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		if (platformUtils == null || (!platformUtils.IsUIOpenOrLostFocus && (!(accountManager != null) || accountManager.SelectedActiveAccount)))
		{
			if (!isReady)
			{
				CheckIfStorageIsReady();
			}
			else
			{
				CheckIfStorageNoLongerReady();
			}
		}
	}

	private void OnPreActiveAccountChange(ILocalAccount account)
	{
		isReady = false;
		timeoutElapsedTime = 0f;
	}

	private void CheckIfStorageIsReady()
	{
		timeoutElapsedTime += Mathf.Min(Time.unscaledDeltaTime, 0.2f);
		bool flag = UseTimeout() && timeoutElapsedTime > timeout;
		if (flag || DoReadyTest())
		{
			isReady = true;
			timeoutElapsedTime = 0f;
			accountManagerWasReady = accountManager != null;
			playerPrefsWasReady = playerPrefs.IsReady;
			fileIOWasReady = fileIO.IsReady;
			saveLoaderWasReady = saveLoader.IsReady;
			if (flag && !playerPrefs.IsReady)
			{
				playerPrefs.OnInitializationTimedOut();
			}
			this.readyCallbacks?.Invoke();
			this.readyCallbacks = null;
		}
	}

	private void CheckIfStorageNoLongerReady()
	{
		if (DoNoLongerReadyTest())
		{
			isReady = false;
			timeoutElapsedTime = 0f;
		}
	}

	private bool DoReadyTest()
	{
		if (accountManager != null && playerPrefs.IsReady && fileIO.IsReady)
		{
			return saveLoader.IsReady;
		}
		return false;
	}

	private bool DoNoLongerReadyTest()
	{
		if ((!accountManagerWasReady || !(accountManager == null)) && (!playerPrefsWasReady || playerPrefs.IsReady) && (!fileIOWasReady || fileIO.IsReady))
		{
			if (saveLoaderWasReady)
			{
				return !saveLoader.IsReady;
			}
			return false;
		}
		return true;
	}

	private static bool UseTimeout()
	{
		return true;
	}
}
