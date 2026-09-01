using System;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblLeaderboardRow
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		internal readonly byte[] gamertag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 97)]
		internal readonly byte[] modernGamertag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
		internal readonly byte[] modernGamertagSuffix;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
		internal readonly byte[] uniqueModernGamertag;

		internal readonly ulong xboxUserId;

		internal readonly double percentile;

		internal readonly uint rank;

		internal readonly uint globalRank;

		private readonly IntPtr columnValues;

		private readonly SizeT columnValuesCount;

		internal XblLeaderboardRow(XGamingRuntime.XblLeaderboardRow row, DisposableCollection disposableCollection)
		{
			gamertag = Converters.StringToNullTerminatedUTF8ByteArray(row.Gamertag, 48);
			modernGamertag = Converters.StringToNullTerminatedUTF8ByteArray(row.ModernGamertag, 97);
			modernGamertagSuffix = Converters.StringToNullTerminatedUTF8ByteArray(row.ModernGamertagSuffix, 15);
			uniqueModernGamertag = Converters.StringToNullTerminatedUTF8ByteArray(row.UniqueModernGamertag, 101);
			xboxUserId = row.XboxUserId;
			percentile = row.Percentile;
			rank = row.Rank;
			globalRank = row.GlobalRank;
			columnValues = Converters.ClassArrayToPtr(row.ColumnValues, (Func<string, DisposableCollection, UTF8StringPtr>)((string s, DisposableCollection dc) => new UTF8StringPtr(s, dc)), disposableCollection, out columnValuesCount);
		}

		public string[] GetColumnValues()
		{
			return Converters.PtrToClassArray(columnValues, columnValuesCount, (UTF8StringPtr s) => s.GetString());
		}
	}
}
