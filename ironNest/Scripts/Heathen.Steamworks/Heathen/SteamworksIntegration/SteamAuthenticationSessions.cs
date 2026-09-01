using System;
using System.Collections.Generic;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAuthenticationEvents))]
	public class SteamAuthenticationSessions : MonoBehaviour
	{
		[Flags]
		public enum AuthSessionResponseMask
		{
			None = 0,
			Ok = 1,
			NotConnectedToSteam = 2,
			NoLicenseOrExpired = 4,
			VacBanned = 8,
			LoggedInElseWhere = 0x10,
			VacCheckTimedOut = 0x20,
			Canceled = 0x40,
			AlreadyUsed = 0x80,
			Invalid = 0x100,
			PublisherIssuedBan = 0x200,
			IdentityFailure = 0x400
		}

		[SettingsField(0, false, null)]
		public AuthSessionResponseMask acceptedResponses;

		private SteamAuthenticationEvents _mEvents;

		public List<AuthenticationSession> Sessions => null;

		private void Awake()
		{
		}

		public void Begin(ulong user, byte[] ticket)
		{
		}

		public void End(ulong user)
		{
		}

		public void End(SteamUserData user)
		{
		}

		public void EndAll()
		{
		}
	}
}
