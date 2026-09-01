using System;

namespace TFBGames
{
	public class AccountPermissionsNoUser : IAccountPermissions, IService
	{
		public bool IsSignedIn => true;

		public void CanUploadUgcAsync(bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			doneCallback?.Invoke(obj: true);
		}

		public void CanViewAndDownloadUgcAsync(bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			doneCallback?.Invoke(obj: true);
		}

		public void CanPlayInAMultiplayerSessionAsync(bool showPopup, string popupMessage, Action<bool> doneCallback)
		{
			doneCallback?.Invoke(obj: true);
		}

		public void CanPlayCrossNetworkSessionAsync(Action<bool> doneCallback)
		{
			doneCallback?.Invoke(obj: true);
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}
	}
}
