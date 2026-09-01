using System;

namespace TFBGames
{
	public interface IAccountPermissions : IService
	{
		bool IsSignedIn { get; }

		void CanUploadUgcAsync(bool showPopup, string popupMessage, Action<bool> doneCallback);

		void CanViewAndDownloadUgcAsync(bool showPopup, string popupMessage, Action<bool> doneCallback);

		void CanPlayInAMultiplayerSessionAsync(bool showPopup, string popupMessage, Action<bool> doneCallback);

		void CanPlayCrossNetworkSessionAsync(Action<bool> doneCallback);
	}
}
