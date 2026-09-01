using System;
using System.Collections.Generic;
using Backtrace.Unity.Model;
using Backtrace.Unity.Model.Database;
using Backtrace.Unity.Services;
using Backtrace.Unity.Types;

namespace Backtrace.Unity.Interfaces
{
	public interface IBacktraceDatabase
	{
		void Flush();

		void SetApi(IBacktraceApi backtraceApi);

		void Clear();

		bool ValidConsistency();

		[Obsolete("Please use Add method with Backtrace data parameter instead")]
		BacktraceDatabaseRecord Add(BacktraceReport backtraceReport, Dictionary<string, string> attributes, MiniDumpType miniDumpType = MiniDumpType.Normal);

		IEnumerable<BacktraceDatabaseRecord> Get();

		void Delete(BacktraceDatabaseRecord record);

		BacktraceDatabaseSettings GetSettings();

		long GetDatabaseSize();

		void SetReportWatcher(ReportLimitWatcher reportLimitWatcher);

		void Reload();

		BacktraceDatabaseRecord Add(BacktraceData data, bool @lock = true);

		bool Enabled();
	}
}
