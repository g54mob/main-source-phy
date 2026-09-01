using System;
using System.Collections.Generic;
using Backtrace.Unity.Common;
using Backtrace.Unity.Extensions;

namespace Backtrace.Unity.Model
{
	public class BacktraceReport
	{
		public readonly Guid Uuid = Guid.NewGuid();

		public readonly long Timestamp = default(DateTime).Timestamp();

		public readonly bool ExceptionTypeReport;

		public string Classifier = string.Empty;

		public BacktraceSourceCode SourceCode;

		public string Fingerprint { get; set; }

		public string Factor { get; set; }

		public Dictionary<string, string> Attributes { get; private set; }

		public string Message { get; private set; }

		public Exception Exception { get; private set; }

		public List<string> AttachmentPaths { get; set; }

		public List<BacktraceStackFrame> DiagnosticStack { get; set; }

		public BacktraceReport(string message, Dictionary<string, string> attributes = null, List<string> attachmentPaths = null)
			: this((Exception)null, attributes, attachmentPaths)
		{
			Message = message;
			SetStacktraceInformation();
		}

		public BacktraceReport(Exception exception, Dictionary<string, string> attributes = null, List<string> attachmentPaths = null)
		{
			Attributes = attributes ?? new Dictionary<string, string>();
			AttachmentPaths = attachmentPaths ?? new List<string>();
			Exception = exception;
			ExceptionTypeReport = exception != null;
			if (ExceptionTypeReport)
			{
				Message = exception.Message;
				SetClassifier();
				SetStacktraceInformation();
			}
		}

		internal void AssignSourceCodeToReport(string text)
		{
			if (DiagnosticStack != null && DiagnosticStack.Count != 0)
			{
				SourceCode = new BacktraceSourceCode
				{
					Text = text
				};
				DiagnosticStack[0].SourceCode = BacktraceSourceCode.SOURCE_CODE_PROPERTY;
			}
		}

		private void SetClassifier()
		{
			if (!ExceptionTypeReport)
			{
				Classifier = string.Empty;
			}
			Classifier = ((Exception is BacktraceUnhandledException) ? (Exception as BacktraceUnhandledException).Classifier : Exception.GetType().Name);
		}

		internal void SetReportFingerPrintForEmptyStackTrace()
		{
			if ((Exception != null && string.IsNullOrEmpty(Exception.StackTrace)) || DiagnosticStack == null || DiagnosticStack.Count == 0)
			{
				Attributes["_mod_fingerprint"] = Message.OnlyLetters().GetSha();
			}
		}

		internal BacktraceData ToBacktraceData(Dictionary<string, string> clientAttributes, int gameObjectDepth)
		{
			return new BacktraceData(this, clientAttributes, gameObjectDepth);
		}

		internal void SetStacktraceInformation()
		{
			BacktraceStackTrace backtraceStackTrace = new BacktraceStackTrace(Exception);
			DiagnosticStack = backtraceStackTrace.StackFrames;
		}

		internal BacktraceReport CreateInnerReport()
		{
			if (!ExceptionTypeReport || Exception.InnerException == null)
			{
				return null;
			}
			BacktraceReport obj = (BacktraceReport)MemberwiseClone();
			obj.Exception = Exception.InnerException;
			obj.SetStacktraceInformation();
			obj.Classifier = obj.Exception.GetType().Name;
			return obj;
		}
	}
}
