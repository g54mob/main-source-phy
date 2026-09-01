using System;
using BitCode;
using BitCode.Dlc;

namespace TFBGames
{
	public interface IDlcManagerService : IDlcManager, IPlatformService, IService
	{
		bool NeedsUserForDlc { get; }

		string AprilFoolsBugsDlcId { get; }

		event Action<string> PreGotAccessToDlc;

		event Action<string> GotAccessToDlc;

		event Action PreLostAccessToAllDlc;

		event Action LostAccessToAllDlc;

		void HasAccessToDlc(string dlcId, Action<bool, Exception> doneCallback);
	}
}
