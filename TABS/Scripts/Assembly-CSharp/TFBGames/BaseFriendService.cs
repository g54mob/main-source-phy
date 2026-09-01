using System;
using System.Threading.Tasks;
using BitCode;
using BitCode.Users;

namespace TFBGames
{
	public abstract class BaseFriendService : IFriendService, IFriendManager, IPlatformService, IService
	{
		private AccountManager accountManager;

		private ILocalAccount initedLocalAccount;

		public IFriendManager FriendManager { get; private set; }

		public event Action<IPlatformService, Exception> InternalErrorOccurred;

		public virtual void OnRegister()
		{
			FriendManager = ServiceLocator.GetService<IPlatformManager>().Services.FriendManager;
			accountManager = ServiceLocator.GetService<AccountManager>();
			accountManager.ActiveAccountChanged += OnActiveAccountChanged;
			accountManager.FireWhenAccountIsSelected(OnActiveAccountChanged);
		}

		private void OnActiveAccountChanged(ILocalAccount user)
		{
			if (initedLocalAccount != null && initedLocalAccount != user)
			{
				ReleaseForUser(initedLocalAccount);
			}
			if (user != null)
			{
				initedLocalAccount = user;
				InitializeForUser(user);
			}
		}

		public virtual void InitializeForUser(ILocalAccount user)
		{
			FriendManager.InitializeForUser(user);
		}

		public virtual void ReleaseForUser(ILocalAccount user)
		{
			FriendManager.ReleaseForUser(user);
			initedLocalAccount = null;
		}

		public virtual void GetFriendListAsync(ILocalAccount user, Action<IRemoteAccount[], Exception> callback)
		{
			FriendManager.GetFriendListAsync(user, callback);
		}

		public bool IsInitializedForUser(ILocalAccount user)
		{
			return FriendManager.IsInitializedForUser(user);
		}

		public virtual Task<IRemoteAccount[]> GetFriendListAsync(ILocalAccount user)
		{
			return FriendManager.GetFriendListAsync(user);
		}

		public virtual void UnRegister()
		{
			if (accountManager != null)
			{
				accountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		public virtual void OnAwake()
		{
		}

		public virtual void OnStart()
		{
		}

		public virtual void OnUpdate()
		{
		}

		public virtual void OnFixedUpdate()
		{
		}

		public virtual void OnLateUpdate()
		{
		}

		private void LogErrorMessage(string message)
		{
		}
	}
}
