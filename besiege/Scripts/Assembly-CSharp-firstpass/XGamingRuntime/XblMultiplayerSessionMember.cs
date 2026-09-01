using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionMember
	{
		public uint MemberId { get; private set; }

		public string TeamId { get; private set; }

		public string InitialTeam { get; private set; }

		public XblTournamentArbitrationStatus ArbitrationStatus { get; private set; }

		public ulong Xuid { get; private set; }

		public string CustomConstantsJson { get; private set; }

		public string SecureDeviceBaseAddress64 { get; private set; }

		public XblMultiplayerSessionMemberRole[] Roles { get; private set; }

		public string CustomPropertiesJson { get; private set; }

		public string Gamertag { get; private set; }

		public XblMultiplayerSessionMemberStatus Status { get; private set; }

		public bool IsTurnAvailable { get; private set; }

		public bool IsCurrentUser { get; private set; }

		public bool InitializeRequested { get; private set; }

		public string MatchmakingResultServerMeasurementsJson { get; private set; }

		public string ServerMeasurementsJson { get; private set; }

		public uint[] MembersInGroupIds { get; private set; }

		public string QosMeasurementsJson { get; private set; }

		public XblDeviceToken DeviceToken { get; private set; }

		public XblNetworkAddressTranslationSetting Nat { get; private set; }

		public uint ActiveTitleId { get; private set; }

		public uint InitializationEpisode { get; private set; }

		public DateTime JoinTime { get; private set; }

		public XblMultiplayerMeasurementFailure InitializationFailureCause { get; private set; }

		public string[] Groups { get; private set; }

		public string[] Encounters { get; private set; }

		public XblMultiplayerSessionReference TournamentTeamSessionReference { get; private set; }

		public IntPtr Internal { get; private set; }

		internal XblMultiplayerSessionMember(XGamingRuntime.Interop.XblMultiplayerSessionMember interopStruct)
		{
			MemberId = interopStruct.MemberId;
			TeamId = interopStruct.TeamId.GetString();
			InitialTeam = interopStruct.InitialTeam.GetString();
			ArbitrationStatus = interopStruct.ArbitrationStatus;
			Xuid = interopStruct.Xuid;
			CustomConstantsJson = interopStruct.CustomConstantsJson.GetString();
			SecureDeviceBaseAddress64 = interopStruct.SecureDeviceBaseAddress64.GetString();
			Roles = interopStruct.GetRoles((XGamingRuntime.Interop.XblMultiplayerSessionMemberRole x) => new XblMultiplayerSessionMemberRole(x));
			CustomPropertiesJson = interopStruct.CustomPropertiesJson.GetString();
			Gamertag = interopStruct.GetGamertag();
			Status = interopStruct.Status;
			IsTurnAvailable = interopStruct.IsTurnAvailable.Value;
			IsCurrentUser = interopStruct.IsCurrentUser.Value;
			InitializeRequested = interopStruct.InitializeRequested.Value;
			MatchmakingResultServerMeasurementsJson = interopStruct.MatchmakingResultServerMeasurementsJson.GetString();
			ServerMeasurementsJson = interopStruct.ServerMeasurementsJson.GetString();
			MembersInGroupIds = interopStruct.GetMembersInGroupIds((uint x) => x);
			QosMeasurementsJson = interopStruct.QosMeasurementsJson.GetString();
			DeviceToken = new XblDeviceToken(interopStruct.DeviceToken);
			Nat = interopStruct.Nat;
			ActiveTitleId = interopStruct.ActiveTitleId;
			InitializationEpisode = interopStruct.InitializationEpisode;
			JoinTime = interopStruct.JoinTime.DateTime;
			InitializationFailureCause = interopStruct.InitializationFailureCause;
			Groups = interopStruct.GetGroups((UTF8StringPtr x) => x.GetString());
			Encounters = interopStruct.GetEncounters((UTF8StringPtr x) => x.GetString());
			TournamentTeamSessionReference = new XblMultiplayerSessionReference(interopStruct.TournamentTeamSessionReference);
			Internal = interopStruct.Internal;
		}
	}
}
