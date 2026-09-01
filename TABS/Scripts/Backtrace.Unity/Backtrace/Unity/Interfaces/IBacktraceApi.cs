using System;
using System.Collections;
using System.Collections.Generic;
using Backtrace.Unity.Model;

namespace Backtrace.Unity.Interfaces
{
	public interface IBacktraceApi
	{
		string ServerUrl { get; }

		Action<Exception> OnServerError { get; set; }

		Action<BacktraceResult> OnServerResponse { get; set; }

		Func<string, BacktraceData, BacktraceResult> RequestHandler { get; set; }

		bool EnablePerformanceStatistics { get; set; }

		IEnumerator Send(BacktraceData data, Action<BacktraceResult> callback = null);

		IEnumerator Send(string json, List<string> attachments, int deduplication, Action<BacktraceResult> callback);

		IEnumerator Send(string json, List<string> attachments, Dictionary<string, string> queryAttributes, Action<BacktraceResult> callback);

		IEnumerator SendMinidump(string minidumpPath, IEnumerable<string> attachments, Action<BacktraceResult> callback = null);
	}
}
