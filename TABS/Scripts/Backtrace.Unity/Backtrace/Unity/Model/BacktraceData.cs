using System;
using System.Collections.Generic;
using System.Linq;
using Backtrace.Unity.Json;
using Backtrace.Unity.Model.JsonData;

namespace Backtrace.Unity.Model
{
	public class BacktraceData
	{
		private string _uuidString;

		public const string Lang = "csharp";

		public readonly string LangVersion = "Mono";

		public const string Agent = "backtrace-unity";

		public const string AgentVersion = "3.3.0";

		public Dictionary<string, ThreadInformation> ThreadInformations;

		public string MainThread;

		public string[] Classifier;

		public BacktraceSourceCode SourceCode;

		public List<string> Attachments;

		public BacktraceAttributes Attributes;

		public Annotations Annotation;

		public ThreadData ThreadData;

		public int Deduplication;

		public Guid Uuid { get; private set; }

		internal string UuidString
		{
			get
			{
				if (string.IsNullOrEmpty(_uuidString))
				{
					_uuidString = Uuid.ToString();
				}
				return _uuidString;
			}
		}

		public long Timestamp { get; private set; }

		public BacktraceReport Report { get; set; }

		public BacktraceData(BacktraceReport report, Dictionary<string, string> clientAttributes = null, int gameObjectDepth = -1)
		{
			if (report != null)
			{
				Report = report;
				Uuid = Report.Uuid;
				Timestamp = Report.Timestamp;
				Classifier = ((!Report.ExceptionTypeReport) ? null : new string[1] { Report.Classifier });
				SetAttributes(clientAttributes, gameObjectDepth);
				SetThreadInformations();
				Attachments = Report.AttachmentPaths.Distinct().ToList();
			}
		}

		public string ToJson()
		{
			BacktraceJObject backtraceJObject = new BacktraceJObject(new Dictionary<string, string>
			{
				["uuid"] = UuidString,
				["lang"] = "csharp",
				["langVersion"] = LangVersion,
				["agent"] = "backtrace-unity",
				["agentVersion"] = "3.3.0",
				["mainThread"] = MainThread
			});
			backtraceJObject.Add("timestamp", Timestamp);
			backtraceJObject.Add("classifiers", Classifier);
			backtraceJObject.Add("attributes", Attributes.ToJson());
			backtraceJObject.Add("annotations", Annotation.ToJson());
			backtraceJObject.Add("threads", ThreadData.ToJson());
			if (SourceCode != null)
			{
				backtraceJObject.Add("sourceCode", SourceCode.ToJson());
			}
			return backtraceJObject.ToJson();
		}

		private void SetThreadInformations()
		{
			bool faultingThread = !(Report.Exception is BacktraceUnhandledException) || !string.IsNullOrEmpty(Report.Exception.StackTrace);
			ThreadData = new ThreadData(Report.DiagnosticStack, faultingThread);
			ThreadInformations = ThreadData.ThreadInformations;
			MainThread = ThreadData.MainThread;
			SourceCode = Report.SourceCode;
		}

		private void SetAttributes(Dictionary<string, string> clientAttributes, int gameObjectDepth)
		{
			Attributes = new BacktraceAttributes(Report, clientAttributes);
			Annotation = new Annotations(Report.ExceptionTypeReport ? Report.Exception : null, gameObjectDepth);
		}
	}
}
