using System;
using System.Collections.Generic;
using Backtrace.Unity.Model;

namespace Backtrace.Unity.Interfaces
{
	public interface IBacktraceClient
	{
		void Send(BacktraceReport report, Action<BacktraceResult> sendCallback);

		void Send(string message, List<string> attachmentPaths = null, Dictionary<string, string> attributes = null);

		void Send(Exception exception, List<string> attachmentPaths = null, Dictionary<string, string> attributes = null);

		void SetClientReportLimit(uint reportPerMin);

		void Refresh();
	}
}
