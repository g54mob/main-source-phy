using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionMember
	{
		[StructLayout(LayoutKind.Sequential, Size = 48)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CGamertag_003E__FixedBuffer16
		{
			public byte FixedElementField;
		}

		internal readonly uint MemberId;

		internal readonly UTF8StringPtr TeamId;

		internal readonly UTF8StringPtr InitialTeam;

		internal readonly XblTournamentArbitrationStatus ArbitrationStatus;

		internal readonly ulong Xuid;

		internal readonly UTF8StringPtr CustomConstantsJson;

		internal readonly UTF8StringPtr SecureDeviceBaseAddress64;

		private unsafe readonly XblMultiplayerSessionMemberRole* Roles;

		internal readonly SizeT RolesCount;

		internal readonly UTF8StringPtr CustomPropertiesJson;

		private _003CGamertag_003E__FixedBuffer16 Gamertag;

		internal readonly XblMultiplayerSessionMemberStatus Status;

		internal readonly NativeBool IsTurnAvailable;

		internal readonly NativeBool IsCurrentUser;

		internal readonly NativeBool InitializeRequested;

		internal readonly UTF8StringPtr MatchmakingResultServerMeasurementsJson;

		internal readonly UTF8StringPtr ServerMeasurementsJson;

		private unsafe readonly uint* MembersInGroupIds;

		internal readonly SizeT MembersInGroupCount;

		internal readonly UTF8StringPtr QosMeasurementsJson;

		internal readonly XblDeviceToken DeviceToken;

		internal readonly XblNetworkAddressTranslationSetting Nat;

		internal readonly uint ActiveTitleId;

		internal readonly uint InitializationEpisode;

		internal readonly TimeT JoinTime;

		internal readonly XblMultiplayerMeasurementFailure InitializationFailureCause;

		private unsafe readonly UTF8StringPtr* Groups;

		internal readonly SizeT GroupsCount;

		private unsafe readonly UTF8StringPtr* Encounters;

		internal readonly SizeT EncountersCount;

		internal readonly XblMultiplayerSessionReference TournamentTeamSessionReference;

		internal readonly IntPtr Internal;

		internal unsafe XblMultiplayerSessionMember(XGamingRuntime.XblMultiplayerSessionMember publicObject, DisposableCollection disposableCollection)
		{
			MemberId = publicObject.MemberId;
			TeamId = new UTF8StringPtr(publicObject.TeamId, disposableCollection);
			InitialTeam = new UTF8StringPtr(publicObject.InitialTeam, disposableCollection);
			ArbitrationStatus = publicObject.ArbitrationStatus;
			Xuid = publicObject.Xuid;
			CustomConstantsJson = new UTF8StringPtr(publicObject.CustomConstantsJson, disposableCollection);
			SecureDeviceBaseAddress64 = new UTF8StringPtr(publicObject.SecureDeviceBaseAddress64, disposableCollection);
			Roles = (XblMultiplayerSessionMemberRole*)(void*)Converters.ClassArrayToPtr(publicObject.Roles, (Func<XGamingRuntime.XblMultiplayerSessionMemberRole, DisposableCollection, XblMultiplayerSessionMemberRole>)((XGamingRuntime.XblMultiplayerSessionMemberRole x, DisposableCollection _) => new XblMultiplayerSessionMemberRole(x, disposableCollection)), disposableCollection, out RolesCount);
			CustomPropertiesJson = new UTF8StringPtr(publicObject.CustomPropertiesJson, disposableCollection);
			fixed (byte* gamertag = &Gamertag.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Gamertag, gamertag, 48);
			}
			Status = publicObject.Status;
			IsTurnAvailable = new NativeBool(publicObject.IsTurnAvailable);
			IsCurrentUser = new NativeBool(publicObject.IsCurrentUser);
			InitializeRequested = new NativeBool(publicObject.InitializeRequested);
			MatchmakingResultServerMeasurementsJson = new UTF8StringPtr(publicObject.MatchmakingResultServerMeasurementsJson, disposableCollection);
			ServerMeasurementsJson = new UTF8StringPtr(publicObject.ServerMeasurementsJson, disposableCollection);
			MembersInGroupIds = (uint*)(void*)Converters.ClassArrayToPtr(publicObject.MembersInGroupIds, (Func<uint, DisposableCollection, uint>)((uint x, DisposableCollection _) => x), disposableCollection, out MembersInGroupCount);
			MembersInGroupCount = new SizeT(publicObject.MembersInGroupIds.Length);
			QosMeasurementsJson = new UTF8StringPtr(publicObject.QosMeasurementsJson, disposableCollection);
			DeviceToken = new XblDeviceToken(publicObject.DeviceToken);
			Nat = publicObject.Nat;
			ActiveTitleId = publicObject.ActiveTitleId;
			InitializationEpisode = publicObject.InitializationEpisode;
			JoinTime = new TimeT(publicObject.JoinTime);
			InitializationFailureCause = publicObject.InitializationFailureCause;
			Groups = (UTF8StringPtr*)(void*)Converters.ClassArrayToPtr(publicObject.Groups, (Func<string, DisposableCollection, UTF8StringPtr>)((string x, DisposableCollection _) => new UTF8StringPtr(x, disposableCollection)), disposableCollection, out GroupsCount);
			Encounters = (UTF8StringPtr*)(void*)Converters.ClassArrayToPtr(publicObject.Encounters, (Func<string, DisposableCollection, UTF8StringPtr>)((string x, DisposableCollection _) => new UTF8StringPtr(x, disposableCollection)), disposableCollection, out EncountersCount);
			TournamentTeamSessionReference = new XblMultiplayerSessionReference(publicObject.TournamentTeamSessionReference);
			Internal = publicObject.Internal;
		}

		internal unsafe T[] GetRoles<T>(Func<XblMultiplayerSessionMemberRole, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)Roles, RolesCount, ctor);
		}

		internal unsafe string GetGamertag()
		{
			fixed (byte* gamertag = &Gamertag.FixedElementField)
			{
				return Converters.BytePointerToString(gamertag, 48);
			}
		}

		internal unsafe T[] GetMembersInGroupIds<T>(Func<uint, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)MembersInGroupIds, MembersInGroupCount, ctor);
		}

		internal unsafe T[] GetGroups<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)Groups, GroupsCount, ctor);
		}

		internal unsafe T[] GetEncounters<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)Encounters, EncountersCount, ctor);
		}
	}
}
