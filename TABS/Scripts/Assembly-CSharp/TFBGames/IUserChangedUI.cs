using BitCode.Users;

namespace TFBGames
{
	public interface IUserChangedUI : IService
	{
		void Show(ILocalAccount newAccount);
	}
}
