using BitCode;

namespace TFBGames
{
	public interface IPlatformManager : IService
	{
		bool Initialized { get; }

		IPlatformServices Services { get; }
	}
}
