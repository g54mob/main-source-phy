using System;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerSessionProperties
	{
		private unsafe readonly UTF8StringPtr* Keywords;

		internal readonly SizeT KeywordCount;

		internal readonly XblMultiplayerSessionRestriction JoinRestriction;

		internal readonly XblMultiplayerSessionRestriction ReadRestriction;

		private unsafe readonly uint* TurnCollection;

		internal readonly SizeT TurnCollectionCount;

		internal readonly UTF8StringPtr MatchmakingTargetSessionConstantsJson;

		internal readonly UTF8StringPtr SessionCustomPropertiesJson;

		internal readonly UTF8StringPtr MatchmakingServerConnectionString;

		private unsafe readonly UTF8StringPtr* ServerConnectionStringCandidates;

		internal readonly SizeT ServerConnectionStringCandidatesCount;

		private unsafe readonly uint* SessionOwnerMemberIds;

		internal readonly SizeT SessionOwnerMemberIdsCount;

		internal readonly XblDeviceToken HostDeviceToken;

		internal readonly NativeBool Closed;

		internal readonly NativeBool Locked;

		internal readonly NativeBool AllocateCloudCompute;

		internal readonly NativeBool MatchmakingResubmit;

		internal unsafe XblMultiplayerSessionProperties(XGamingRuntime.XblMultiplayerSessionProperties publicObject, DisposableCollection disposableCollection)
		{
			Keywords = (UTF8StringPtr*)(void*)Converters.ClassArrayToPtr(publicObject.Keywords, (Func<string, DisposableCollection, UTF8StringPtr>)((string x, DisposableCollection _) => new UTF8StringPtr(x, disposableCollection)), disposableCollection, out KeywordCount);
			JoinRestriction = publicObject.JoinRestriction;
			ReadRestriction = publicObject.ReadRestriction;
			TurnCollection = (uint*)(void*)Converters.ClassArrayToPtr(publicObject.TurnCollection, (Func<uint, DisposableCollection, uint>)((uint x, DisposableCollection _) => x), disposableCollection, out TurnCollectionCount);
			MatchmakingTargetSessionConstantsJson = new UTF8StringPtr(publicObject.MatchmakingTargetSessionConstantsJson, disposableCollection);
			SessionCustomPropertiesJson = new UTF8StringPtr(publicObject.SessionCustomPropertiesJson, disposableCollection);
			MatchmakingServerConnectionString = new UTF8StringPtr(publicObject.MatchmakingServerConnectionString, disposableCollection);
			ServerConnectionStringCandidates = (UTF8StringPtr*)(void*)Converters.ClassArrayToPtr(publicObject.ServerConnectionStringCandidates, (Func<string, DisposableCollection, UTF8StringPtr>)((string x, DisposableCollection _) => new UTF8StringPtr(x, disposableCollection)), disposableCollection, out ServerConnectionStringCandidatesCount);
			SessionOwnerMemberIds = (uint*)(void*)Converters.ClassArrayToPtr(publicObject.SessionOwnerMemberIds, (Func<uint, DisposableCollection, uint>)((uint x, DisposableCollection _) => x), disposableCollection, out SessionOwnerMemberIdsCount);
			HostDeviceToken = new XblDeviceToken(publicObject.HostDeviceToken);
			Closed = new NativeBool(publicObject.Closed);
			Locked = new NativeBool(publicObject.Locked);
			AllocateCloudCompute = new NativeBool(publicObject.AllocateCloudCompute);
			MatchmakingResubmit = new NativeBool(publicObject.MatchmakingResubmit);
		}

		internal unsafe T[] GetKeywords<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)Keywords, KeywordCount, ctor);
		}

		internal unsafe T[] GetTurnCollection<T>(Func<uint, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)TurnCollection, TurnCollectionCount, ctor);
		}

		internal unsafe T[] GetServerConnectionStringCandidates<T>(Func<UTF8StringPtr, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)ServerConnectionStringCandidates, ServerConnectionStringCandidatesCount, ctor);
		}

		internal unsafe T[] GetSessionOwnerMemberIds<T>(Func<uint, T> ctor)
		{
			return Converters.PtrToClassArray((IntPtr)SessionOwnerMemberIds, SessionOwnerMemberIdsCount, ctor);
		}
	}
}
