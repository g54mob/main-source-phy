using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblLeaderboardQuery
	{
		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003Cscid_003E__FixedBuffer9
		{
			public byte FixedElementField;
		}

		internal readonly ulong xboxUserId;

		private _003Cscid_003E__FixedBuffer9 scid;

		internal readonly UTF8StringPtr leaderboardName;

		internal readonly UTF8StringPtr statName;

		internal readonly XblSocialGroupType socialGroup;

		private readonly IntPtr additionalColumnleaderboardNames;

		private readonly SizeT additionalColumnleaderboardNamesCount;

		internal readonly XblLeaderboardSortOrder order;

		internal readonly uint maxItems;

		internal readonly ulong skipToXboxUserId;

		internal readonly uint skipResultToRank;

		internal readonly UTF8StringPtr continuationToken;

		internal readonly XblLeaderboardQueryType queryType;

		internal unsafe XblLeaderboardQuery(XGamingRuntime.XblLeaderboardQuery query, DisposableCollection disposableCollection)
		{
			xboxUserId = query.XboxUserId;
			fixed (byte* bytePointer = &scid.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(query.ServiceConfigurationId, bytePointer, 40);
			}
			leaderboardName = new UTF8StringPtr(query.LeaderboardName, disposableCollection);
			statName = new UTF8StringPtr(query.StatName, disposableCollection);
			socialGroup = query.SocialGroup;
			additionalColumnleaderboardNames = Converters.StringArrayToUTF8StringArray(query.AdditionalColumnleaderboardNames, disposableCollection, out additionalColumnleaderboardNamesCount);
			order = query.Order;
			maxItems = query.MaxItems;
			skipToXboxUserId = query.SkipToXboxUserId;
			skipResultToRank = query.SkipResultToRank;
			continuationToken = new UTF8StringPtr(query.ContinuationToken, disposableCollection);
			queryType = query.QueryType;
		}

		internal string[] GetAdditionalColumnleaderboardNames()
		{
			return Converters.PtrToClassArray(additionalColumnleaderboardNames, additionalColumnleaderboardNamesCount, (UTF8StringPtr s) => s.GetString());
		}

		internal unsafe string GetScid()
		{
			fixed (byte* bytePointer = &scid.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 40);
			}
		}

		internal static bool ValidateFields(string scid)
		{
			return scid != null && Converters.StringToNullTerminatedUTF8ByteArray(scid).Length <= 40;
		}
	}
}
