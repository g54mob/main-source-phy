using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Backtrace.Unity.Common;
using Backtrace.Unity.Interfaces;
using Backtrace.Unity.Model;
using Backtrace.Unity.Model.Database;
using Backtrace.Unity.Runtime.Native;
using Backtrace.Unity.Services;
using Backtrace.Unity.Types;
using UnityEngine;

namespace Backtrace.Unity
{
	public class BacktraceClient : MonoBehaviour, IBacktraceClient
	{
		public BacktraceConfiguration Configuration;

		public const string VERSION = "3.3.0";

		private readonly Dictionary<string, string> _clientAttributes = new Dictionary<string, string>();

		private static BacktraceClient _instance;

		public IBacktraceDatabase Database;

		private IBacktraceApi _backtraceApi;

		private ReportLimitWatcher _reportLimitWatcher;

		internal Action<BacktraceReport> _onClientReportLimitReached;

		public Func<BacktraceData, BacktraceData> BeforeSend;

		public Func<ReportFilterType, Exception, string, bool> SkipReport;

		public Action<Exception> OnUnhandledApplicationException;

		private INativeClient _nativeClient;

		private BacktraceLogManager _backtraceLogManager;

		public bool Enabled { get; private set; }

		public string this[string index]
		{
			get
			{
				return _clientAttributes[index];
			}
			set
			{
				_clientAttributes[index] = value;
				if (_nativeClient != null)
				{
					_nativeClient.SetAttribute(index, value);
				}
			}
		}

		public static BacktraceClient Instance => _instance;

		public Action<Exception> OnServerError
		{
			get
			{
				if (BacktraceApi != null)
				{
					return BacktraceApi.OnServerError;
				}
				return null;
			}
			set
			{
				if (ValidClientConfiguration())
				{
					BacktraceApi.OnServerError = value;
				}
			}
		}

		public Func<string, BacktraceData, BacktraceResult> RequestHandler
		{
			get
			{
				if (BacktraceApi != null)
				{
					return BacktraceApi.RequestHandler;
				}
				return null;
			}
			set
			{
				if (ValidClientConfiguration())
				{
					BacktraceApi.RequestHandler = value;
				}
			}
		}

		public Action<BacktraceResult> OnServerResponse
		{
			get
			{
				if (BacktraceApi != null)
				{
					return BacktraceApi.OnServerResponse;
				}
				return null;
			}
			set
			{
				if (ValidClientConfiguration())
				{
					BacktraceApi.OnServerResponse = value;
				}
			}
		}

		public Action<BacktraceReport> OnClientReportLimitReached
		{
			get
			{
				return _onClientReportLimitReached;
			}
			set
			{
				if (ValidClientConfiguration())
				{
					_onClientReportLimitReached = value;
				}
			}
		}

		public bool EnablePerformanceStatistics => Configuration.PerformanceStatistics;

		public int GameObjectDepth
		{
			get
			{
				if (Configuration.GameObjectDepth != 0)
				{
					return Configuration.GameObjectDepth;
				}
				return 16;
			}
		}

		internal IBacktraceApi BacktraceApi
		{
			get
			{
				return _backtraceApi;
			}
			set
			{
				_backtraceApi = value;
				if (Database != null)
				{
					Database.SetApi(_backtraceApi);
				}
			}
		}

		internal ReportLimitWatcher ReportLimitWatcher
		{
			get
			{
				return _reportLimitWatcher;
			}
			set
			{
				_reportLimitWatcher = value;
				if (Database != null)
				{
					Database.SetReportWatcher(_reportLimitWatcher);
				}
			}
		}

		public void SetAttributes(Dictionary<string, string> attributes)
		{
			if (attributes == null)
			{
				return;
			}
			foreach (KeyValuePair<string, string> attribute in attributes)
			{
				this[attribute.Key] = attribute.Value;
			}
		}

		public int GetAttributesCount()
		{
			return _clientAttributes.Count;
		}

		public static BacktraceClient Initialize(BacktraceConfiguration configuration, Dictionary<string, string> attributes = null, string gameObjectName = "BacktraceClient")
		{
			if (string.IsNullOrEmpty(gameObjectName))
			{
				throw new ArgumentException("Missing game object name");
			}
			if (configuration == null || string.IsNullOrEmpty(configuration.ServerUrl))
			{
				throw new ArgumentException("Missing valid configuration");
			}
			if (Instance != null)
			{
				return Instance;
			}
			GameObject gameObject = new GameObject(gameObjectName, typeof(BacktraceClient), typeof(BacktraceDatabase));
			BacktraceClient component = gameObject.GetComponent<BacktraceClient>();
			component.Configuration = configuration;
			if (configuration.Enabled)
			{
				gameObject.GetComponent<BacktraceDatabase>().Configuration = configuration;
			}
			gameObject.SetActive(value: true);
			component.Refresh();
			component.SetAttributes(attributes);
			return component;
		}

		public static BacktraceClient Initialize(string url, string databasePath, Dictionary<string, string> attributes = null, string gameObjectName = "BacktraceClient")
		{
			BacktraceConfiguration backtraceConfiguration = ScriptableObject.CreateInstance<BacktraceConfiguration>();
			backtraceConfiguration.ServerUrl = url;
			backtraceConfiguration.Enabled = true;
			backtraceConfiguration.DatabasePath = databasePath;
			backtraceConfiguration.CreateDatabase = true;
			return Initialize(backtraceConfiguration, attributes, gameObjectName);
		}

		public static BacktraceClient Initialize(string url, Dictionary<string, string> attributes = null, string gameObjectName = "BacktraceClient")
		{
			BacktraceConfiguration backtraceConfiguration = ScriptableObject.CreateInstance<BacktraceConfiguration>();
			backtraceConfiguration.ServerUrl = url;
			backtraceConfiguration.Enabled = false;
			return Initialize(backtraceConfiguration, attributes, gameObjectName);
		}

		public void OnDisable()
		{
			Enabled = false;
		}

		public void Refresh()
		{
			if (Configuration == null || !Configuration.IsValid() || Instance != null)
			{
				return;
			}
			Enabled = true;
			CaptureUnityMessages();
			_reportLimitWatcher = new ReportLimitWatcher(Convert.ToUInt32(Configuration.ReportPerMin));
			BacktraceApi = new BacktraceApi(new BacktraceCredentials(Configuration.GetValidServerUrl()), Configuration.IgnoreSslValidation);
			BacktraceApi.EnablePerformanceStatistics = Configuration.PerformanceStatistics;
			if (!Configuration.DestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
				_instance = this;
			}
			if (Configuration.Enabled)
			{
				Database = GetComponent<BacktraceDatabase>();
				if (Database != null)
				{
					Database.Reload();
					Database.SetApi(BacktraceApi);
					Database.SetReportWatcher(_reportLimitWatcher);
				}
			}
			_nativeClient = NativeClientFactory.GetNativeClient(Configuration, base.name);
			if (_nativeClient != null)
			{
				foreach (KeyValuePair<string, string> clientAttribute in _clientAttributes)
				{
					_nativeClient.SetAttribute(clientAttribute.Key, clientAttribute.Value);
				}
			}
			if (Configuration.SendUnhandledGameCrashesOnGameStartup && base.isActiveAndEnabled)
			{
				NativeCrashUploader nativeCrashUploader = new NativeCrashUploader();
				nativeCrashUploader.SetBacktraceApi(BacktraceApi);
				StartCoroutine(nativeCrashUploader.SendUnhandledGameCrashesOnGameStartup());
			}
		}

		private void Awake()
		{
			Refresh();
		}

		private void Update()
		{
			_nativeClient?.UpdateClientTime(Time.time);
		}

		private void OnDestroy()
		{
			Enabled = false;
			Application.logMessageReceived -= HandleUnityMessage;
		}

		public void SetClientReportLimit(uint reportPerMin)
		{
			if (!Enabled)
			{
				UnityEngine.Debug.LogWarning("Please enable BacktraceClient first.");
			}
			else
			{
				_reportLimitWatcher.SetClientReportLimit(reportPerMin);
			}
		}

		public void Send(string message, List<string> attachmentPaths = null, Dictionary<string, string> attributes = null)
		{
			if (ShouldSendReport(message, attachmentPaths, attributes))
			{
				BacktraceReport report = new BacktraceReport(message, attributes, attachmentPaths);
				_backtraceLogManager.Enqueue(report);
				SendReport(report);
			}
		}

		public void Send(Exception exception, List<string> attachmentPaths = null, Dictionary<string, string> attributes = null)
		{
			if (ShouldSendReport(exception, attachmentPaths, attributes))
			{
				BacktraceReport report = new BacktraceReport(exception, attributes, attachmentPaths);
				_backtraceLogManager.Enqueue(report);
				SendReport(report);
			}
		}

		public void Send(BacktraceReport report, Action<BacktraceResult> sendCallback = null)
		{
			if (ShouldSendReport(report))
			{
				_backtraceLogManager.Enqueue(report);
				SendReport(report, sendCallback);
			}
		}

		private void SendReport(BacktraceReport report, Action<BacktraceResult> sendCallback = null)
		{
			if (BacktraceApi == null)
			{
				UnityEngine.Debug.LogWarning("Backtrace API doesn't exist. Please validate client token or server url!");
			}
			else
			{
				StartCoroutine(CollectDataAndSend(report, sendCallback));
			}
		}

		private IEnumerator CollectDataAndSend(BacktraceReport report, Action<BacktraceResult> sendCallback)
		{
			Dictionary<string, string> queryAttributes = new Dictionary<string, string>();
			Stopwatch stopWatch = (EnablePerformanceStatistics ? Stopwatch.StartNew() : new Stopwatch());
			BacktraceData data = SetupBacktraceData(report);
			if (EnablePerformanceStatistics)
			{
				stopWatch.Stop();
				queryAttributes["performance.report"] = stopWatch.GetMicroseconds();
			}
			if (BeforeSend != null)
			{
				data = BeforeSend(data);
				if (data == null)
				{
					yield break;
				}
			}
			BacktraceDatabaseRecord record = null;
			if (Database != null && Database.Enabled())
			{
				yield return new WaitForEndOfFrame();
				if (EnablePerformanceStatistics)
				{
					stopWatch.Restart();
				}
				record = Database.Add(data);
				if (record == null)
				{
					yield break;
				}
				data = record.BacktraceData;
				if (EnablePerformanceStatistics)
				{
					stopWatch.Stop();
					queryAttributes["performance.database"] = stopWatch.GetMicroseconds();
				}
				if (record.Duplicated)
				{
					record.Unlock();
					yield break;
				}
			}
			if (EnablePerformanceStatistics)
			{
				stopWatch.Restart();
			}
			string json = ((record != null) ? record.BacktraceDataJson() : data.ToJson());
			if (EnablePerformanceStatistics)
			{
				stopWatch.Stop();
				queryAttributes["performance.json"] = stopWatch.GetMicroseconds();
			}
			yield return new WaitForEndOfFrame();
			if (string.IsNullOrEmpty(json))
			{
				yield break;
			}
			if (RequestHandler != null)
			{
				yield return RequestHandler(BacktraceApi.ServerUrl, data);
				yield break;
			}
			if (data.Deduplication != 0)
			{
				queryAttributes["_mod_duplicate"] = data.Deduplication.ToString();
			}
			StartCoroutine(BacktraceApi.Send(json, data.Attachments, queryAttributes, delegate(BacktraceResult result)
			{
				if (record != null)
				{
					record.Unlock();
					if (Database != null && result.Status != BacktraceResultStatus.ServerError && result.Status != BacktraceResultStatus.NetworkError)
					{
						Database.Delete(record);
					}
				}
				HandleInnerException(report);
				if (sendCallback != null)
				{
					sendCallback(result);
				}
			}));
		}

		private BacktraceData SetupBacktraceData(BacktraceReport report)
		{
			if (Configuration.UseNormalizedExceptionMessage)
			{
				report.SetReportFingerPrintForEmptyStackTrace();
			}
			string text = (_backtraceLogManager.Disabled ? new BacktraceUnityMessage(report).ToString() : _backtraceLogManager.ToSourceCode());
			report.AssignSourceCodeToReport(text);
			BacktraceData backtraceData = report.ToBacktraceData(null, GameObjectDepth);
			if (_nativeClient != null)
			{
				_nativeClient.GetAttributes(backtraceData.Attributes.Attributes);
			}
			foreach (KeyValuePair<string, string> clientAttribute in _clientAttributes)
			{
				backtraceData.Attributes.Attributes[clientAttribute.Key] = clientAttribute.Value;
			}
			return backtraceData;
		}

		private void CaptureUnityMessages()
		{
			_backtraceLogManager = new BacktraceLogManager(Configuration.NumberOfLogs);
			if (Configuration.HandleUnhandledExceptions || Configuration.NumberOfLogs != 0)
			{
				Application.logMessageReceived += HandleUnityMessage;
			}
		}

		internal void HandleUnityMessage(string message, string stackTrace, LogType type)
		{
			if (!Enabled)
			{
				return;
			}
			BacktraceUnityMessage backtraceUnityMessage = new BacktraceUnityMessage(message, stackTrace, type);
			_backtraceLogManager.Enqueue(backtraceUnityMessage);
			if (!Configuration.HandleUnhandledExceptions || !backtraceUnityMessage.IsUnhandledException())
			{
				return;
			}
			BacktraceUnhandledException ex = null;
			bool invokeSkipApi = true;
			if (type == LogType.Error && SamplingShouldSkip())
			{
				if (SkipReport == null && !Configuration.ReportFilterType.HasFlag(ReportFilterType.UnhandledException))
				{
					return;
				}
				ex = new BacktraceUnhandledException(backtraceUnityMessage.Message, backtraceUnityMessage.StackTrace);
				if (ShouldSkipReport(ReportFilterType.UnhandledException, ex, string.Empty))
				{
					return;
				}
				invokeSkipApi = false;
			}
			if (ex == null)
			{
				ex = new BacktraceUnhandledException(backtraceUnityMessage.Message, backtraceUnityMessage.StackTrace);
			}
			SendUnhandledException(ex, invokeSkipApi);
		}

		private bool SamplingShouldSkip()
		{
			if (!Configuration || Configuration.Sampling == 1.0)
			{
				return false;
			}
			return (double)UnityEngine.Random.Range(0f, 1f) > Configuration.Sampling;
		}

		private void SendUnhandledException(BacktraceUnhandledException exception, bool invokeSkipApi = true)
		{
			if (OnUnhandledApplicationException != null)
			{
				OnUnhandledApplicationException(exception);
			}
			if (ShouldSendReport(exception, null, null, invokeSkipApi))
			{
				SendReport(new BacktraceReport(exception));
			}
		}

		private bool ShouldSendReport(Exception exception, List<string> attachmentPaths, Dictionary<string, string> attributes, bool invokeSkipApi = true)
		{
			if (!Enabled)
			{
				return false;
			}
			ReportFilterType type = ReportFilterType.Exception;
			if (exception is BacktraceUnhandledException)
			{
				type = (((exception as BacktraceUnhandledException).Classifier == "ANRException") ? ReportFilterType.Hang : ReportFilterType.UnhandledException);
			}
			if (invokeSkipApi && ShouldSkipReport(type, exception, string.Empty))
			{
				return false;
			}
			if (_reportLimitWatcher.WatchReport(default(DateTime).Timestamp()))
			{
				return true;
			}
			if (OnClientReportLimitReached != null)
			{
				BacktraceReport obj = new BacktraceReport(exception, attributes, attachmentPaths);
				_onClientReportLimitReached(obj);
			}
			return false;
		}

		private bool ShouldSendReport(string message, List<string> attachmentPaths, Dictionary<string, string> attributes)
		{
			if (ShouldSkipReport(ReportFilterType.Message, null, message))
			{
				return false;
			}
			if (_reportLimitWatcher.WatchReport(default(DateTime).Timestamp()))
			{
				return true;
			}
			if (OnClientReportLimitReached != null)
			{
				BacktraceReport obj = new BacktraceReport(message, attributes, attachmentPaths);
				_onClientReportLimitReached(obj);
			}
			return false;
		}

		private bool ShouldSendReport(BacktraceReport report)
		{
			if (ShouldSkipReport((!report.ExceptionTypeReport) ? ReportFilterType.Message : ReportFilterType.Exception, report.Exception, report.Message))
			{
				return false;
			}
			if (_reportLimitWatcher.WatchReport(default(DateTime).Timestamp()))
			{
				return true;
			}
			if (OnClientReportLimitReached != null)
			{
				_onClientReportLimitReached(report);
			}
			return false;
		}

		private void HandleInnerException(BacktraceReport report)
		{
			BacktraceReport backtraceReport = report.CreateInnerReport();
			if (backtraceReport != null && ShouldSendReport(backtraceReport))
			{
				SendReport(backtraceReport);
			}
		}

		private bool ValidClientConfiguration()
		{
			int num;
			if (BacktraceApi != null)
			{
				num = ((!Enabled) ? 1 : 0);
				if (num == 0)
				{
					goto IL_0021;
				}
			}
			else
			{
				num = 1;
			}
			UnityEngine.Debug.LogWarning("Cannot set method if configuration contain invalid url to Backtrace server or client is disabled");
			goto IL_0021;
			IL_0021:
			return num == 0;
		}

		private bool ShouldSkipReport(ReportFilterType type, Exception exception, string message)
		{
			if (!Configuration.ReportFilterType.HasFlag(type))
			{
				if (SkipReport != null)
				{
					return SkipReport(type, exception, message);
				}
				return false;
			}
			return true;
		}
	}
}
