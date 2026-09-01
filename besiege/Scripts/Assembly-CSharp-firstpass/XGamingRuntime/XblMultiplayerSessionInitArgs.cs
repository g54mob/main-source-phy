using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionInitArgs
	{
		public uint MaxMembersInSession { get; private set; }

		public XblMultiplayerSessionVisibility Visibility { get; private set; }

		public ulong[] InitiatorXuids { get; private set; }

		public string CustomJson { get; private set; }

		public XblMultiplayerSessionInitArgs(uint maxMembers, XblMultiplayerSessionVisibility visibility, string customJson, params ulong[] initiatorXuids)
		{
			MaxMembersInSession = maxMembers;
			Visibility = visibility;
			InitiatorXuids = new ulong[initiatorXuids.Length];
			Array.Copy(initiatorXuids, InitiatorXuids, initiatorXuids.Length);
			CustomJson = customJson;
		}

		internal XblMultiplayerSessionInitArgs(XGamingRuntime.Interop.XblMultiplayerSessionInitArgs interopStruct)
		{
			MaxMembersInSession = interopStruct.MaxMembersInSession;
			Visibility = interopStruct.Visibility;
			InitiatorXuids = interopStruct.GetInitiatorXuids((ulong x) => x);
			CustomJson = interopStruct.CustomJson.GetString();
		}
	}
}
