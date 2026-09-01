using System;
using System.Threading.Tasks;
using BitCode;
using BitCode.Users;

namespace TFBGames
{
	public class DefaultFriendService : IFriendService, IFriendManager, IPlatformService, IService
	{
		public event Action<IPlatformService, Exception> InternalErrorOccurred;

		public void InitializeForUser(ILocalAccount user)
		{
			throw new NotImplementedException();
		}

		public void ReleaseForUser(ILocalAccount user)
		{
			throw new NotImplementedException();
		}

		public void GetFriendListAsync(ILocalAccount user, Action<IRemoteAccount[], Exception> callback)
		{
			throw new NotImplementedException();
		}

		public bool IsInitializedForUser(ILocalAccount user)
		{
			throw new NotImplementedException();
		}

		public Task<IRemoteAccount[]> GetFriendListAsync(ILocalAccount user)
		{
			throw new NotImplementedException();
		}

		public virtual void OnAwake()
		{
		}

		public virtual void OnRegister()
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

		public virtual void UnRegister()
		{
		}
	}
}
