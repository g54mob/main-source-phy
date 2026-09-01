using System;

namespace XGamingRuntime.Interop
{
	internal struct XblLeaderboardResult
	{
		internal readonly uint totalRowCount;

		private readonly IntPtr columns;

		private readonly SizeT columnsCount;

		private readonly IntPtr rows;

		private readonly SizeT rowsCount;

		internal readonly NativeBool hasNext;

		internal readonly XblLeaderboardQuery nextQuery;

		internal XblLeaderboardResult(XGamingRuntime.XblLeaderboardResult result, DisposableCollection disposableCollection)
		{
			totalRowCount = result.TotalRowCount;
			columns = Converters.ClassArrayToPtr(result.Columns, (Func<XGamingRuntime.XblLeaderboardColumn, DisposableCollection, XblLeaderboardColumn>)((XGamingRuntime.XblLeaderboardColumn c, DisposableCollection dc) => new XblLeaderboardColumn(c, dc)), disposableCollection, out columnsCount);
			rows = Converters.ClassArrayToPtr(result.Rows, (Func<XGamingRuntime.XblLeaderboardRow, DisposableCollection, XblLeaderboardRow>)((XGamingRuntime.XblLeaderboardRow r, DisposableCollection dc) => new XblLeaderboardRow(r, dc)), disposableCollection, out rowsCount);
			hasNext = new NativeBool(result.HasNext);
			nextQuery = new XblLeaderboardQuery(result.NextQuery, disposableCollection);
		}

		internal T[] GetColumns<T>(Func<XblLeaderboardColumn, T> ctor)
		{
			return Converters.PtrToClassArray(columns, columnsCount, ctor);
		}

		internal T[] GetRows<T>(Func<XblLeaderboardRow, T> ctor)
		{
			return Converters.PtrToClassArray(rows, rowsCount, ctor);
		}
	}
}
