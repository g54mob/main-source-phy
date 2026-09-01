using System;
using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.Dlc
{
	public interface IDlcManager : IPlatformService
	{
		event Action<IDlc> InstalledDlc;

		void Initialize();

		void GetDlcForUserAsync(ILocalAccount userAccount, Action<IDlc[], Exception> doneCallback);

		Task<IDlc[]> GetDlcForUserAsync(ILocalAccount userAccount);
	}
}
