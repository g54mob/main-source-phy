using BitCode;
using BitCode.Networking;

namespace TFBGames
{
	public interface IGameInvitationService : IGameInvitationManager, IPlatformService, IService
	{
		void SetAppReadyToReceiveInvites();
	}
}
