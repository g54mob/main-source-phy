using System;
using System.Threading.Tasks;

namespace BitCode.Users
{
	public interface IFriendManager : IPlatformService
	{
		void InitializeForUser(ILocalAccount user);

		void ReleaseForUser(ILocalAccount user);

		bool IsInitializedForUser(ILocalAccount user);

		void GetFriendListAsync(ILocalAccount user, Action<IRemoteAccount[], Exception> callback);

		Task<IRemoteAccount[]> GetFriendListAsync(ILocalAccount user);
	}
}
