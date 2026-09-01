using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class AuthenticationSession
	{
		public bool IsClientSession { get; private set; }

		public UserData User { get; private set; }

		public UserData GameOwner { get; private set; }

		public byte[] Data { get; private set; }

		public EAuthSessionResponse Response { get; private set; }

		public bool IsBorrowed => false;

		public Action<AuthenticationSession> OnStartCallback { get; private set; }

		public AuthenticationSession(CSteamID userId, Action<AuthenticationSession> callback, bool isClient = true)
		{
		}

		internal void Authenticate(ValidateAuthTicketResponse_t response)
		{
		}

		public void End()
		{
		}
	}
}
