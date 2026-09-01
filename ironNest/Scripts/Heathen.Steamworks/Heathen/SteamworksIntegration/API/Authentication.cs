using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Authentication
	{
		public static List<AuthenticationTicket> ActiveTickets;

		public static List<AuthenticationSession> ActiveSessions;

		private static Callback<GetAuthSessionTicketResponse_t> _mGetAuthSessionTicketResponse;

		private static Callback<GetAuthSessionTicketResponse_t> _mGetAuthSessionTicketResponseServer;

		private static Callback<GetTicketForWebApiResponse_t> _mGetTicketForWebApiResponse;

		private static Callback<ValidateAuthTicketResponse_t> _mValidateAuthSessionTicketResponse;

		private static Callback<ValidateAuthTicketResponse_t> _mValidateAuthSessionTicketResponseServer;

		internal static CallResult<EncryptedAppTicketResponse_t> MEncryptedAppTicketResponse;

		private static CallResult<StoreAuthURLResponse_t> _mStoreAuthURLResponse;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
		}

		public static bool IsAuthTicketValid(AuthenticationTicket ticket)
		{
			return false;
		}

		public static string EncodedAuthTicket(AuthenticationTicket ticket)
		{
			return null;
		}

		public static void GetAuthSessionTicket(CSteamID authenticatingIdentity, Action<AuthenticationTicket, bool> callback)
		{
		}

		public static void GetAuthSessionTicket(SteamNetworkingIdentity forIdentity, Action<AuthenticationTicket, bool> callback)
		{
		}

		public static void GetEncryptedAuthSessionTicket(byte[] dataToInclude, Action<AuthenticationTicket, bool> callback)
		{
		}

		public static void GetWebAuthSessionTicket(string webIdentity, Action<AuthenticationTicket, bool> callback)
		{
		}

		public static void GetStoreAuthURL(string redirectUrl, Action<string, bool> callback)
		{
		}

		public static void CancelAuthTicket(AuthenticationTicket ticket)
		{
		}

		public static EBeginAuthSessionResult BeginAuthSession(byte[] authTicket, UserData user, Action<AuthenticationSession> callback)
		{
			return default(EBeginAuthSessionResult);
		}

		public static void EndAuthSession(UserData user)
		{
		}

		public static EUserHasLicenseForAppResult UserHasLicenseForApp(UserData user, AppData appId)
		{
			return default(EUserHasLicenseForAppResult);
		}

		private static void HandleGetAuthSessionTicketResponse(GetAuthSessionTicketResponse_t pCallback)
		{
		}

		private static void HandleGetTicketForWebApiResponse(GetTicketForWebApiResponse_t pCallback)
		{
		}

		private static void HandleValidateAuthTicketResponse(ValidateAuthTicketResponse_t param)
		{
		}

		public static void EndAllSessions()
		{
		}

		public static void CancelAllTickets()
		{
		}
	}
}
