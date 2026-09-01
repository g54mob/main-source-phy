using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class AuthenticationTicket
	{
		public bool IsClientTicket { get; private set; }

		public HAuthTicket Handle { get; private set; }

		public byte[] Data { get; private set; }

		public bool Verified { get; private set; }

		public uint CreatedOn { get; private set; }

		public EResult Result { get; private set; }

		public Action<AuthenticationTicket, bool> Callback { get; private set; }

		public TimeSpan Age => default(TimeSpan);

		public AuthenticationTicket(SteamNetworkingIdentity forIdentity, Action<AuthenticationTicket, bool> callback, bool isClient = true)
		{
		}

		public AuthenticationTicket(byte[] dataToInclude, Action<AuthenticationTicket, bool> callback)
		{
		}

		public AuthenticationTicket(string webIdentity, Action<AuthenticationTicket, bool> callback)
		{
		}

		public void Authenticate(GetAuthSessionTicketResponse_t response)
		{
		}

		public void Authenticate(GetTicketForWebApiResponse_t response)
		{
		}

		public void Cancel()
		{
		}
	}
}
