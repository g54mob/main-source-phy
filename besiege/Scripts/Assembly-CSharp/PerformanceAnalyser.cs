using System;
using System.Diagnostics;
using Localisation;
using UnityEngine;

public class PerformanceAnalyser : SingleInstance<PerformanceAnalyser>
{
	private double prevTotalProcessorTime;

	private DateTime prevCheckingTime = DateTime.MinValue;

	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float nextMessageTimer;

	[SerializeField]
	private GameObject restartGameNoticeObject;

	private float lastRealtimeSeconds;

	private float myDeltaTime;

	private float globalTime;

	public override string Name
	{
		get
		{
			return "PerformanceAnalyser";
		}
	}

	public float CPULoad { get; internal set; }

	public float FPS { get; internal set; }

	public float UncappedFPS { get; internal set; }

	private void OnEnable()
	{
		if (StatMaster.PopupExceptions)
		{
			Application.logMessageReceived += HandleLog;
		}
	}

	private void OnDisable()
	{
		if (StatMaster.PopupExceptions)
		{
			Application.logMessageReceived -= HandleLog;
		}
	}

	internal void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (UnityEngine.Debug.isDebugBuild && type == LogType.Exception && Time.realtimeSinceStartup > nextMessageTimer)
		{
			nextMessageTimer = Time.realtimeSinceStartup + 1f;
			if (logString.Contains("NullReferenceException"))
			{
				string[] array = stackTrace.Split('\n');
				string arg = array[0];
				logString = string.Format("Null Ref Exception caught in:\n{0}", arg);
			}
			string text = string.Format("{0}\n{1}", logString, LocalisationManager.GetTranslation(1922));
			GenericUIPopup genericUIPopup = SingleInstanceFindOnly<GenericUIPopup>.Instance;
			genericUIPopup.Show(text, 5f);
			if ((bool)genericUIPopup)
			{
				genericUIPopup.Show(text, 5f);
			}
			WinScreen.noErrorsDetected = false;
		}
	}

	private void Update()
	{
		accum += Time.unscaledDeltaTime;
		frames++;
		if (accum >= updateInterval)
		{
			UncappedFPS = 1f / (accum / (float)frames);
			FPS = ((!(UncappedFPS < (float)StatMaster.MaxFPS)) ? ((float)StatMaster.MaxFPS) : UncappedFPS);
			UpdateCPULoad();
			accum = 0f;
			frames = 0;
		}
		SetGlobalTime();
	}

	private void SetGlobalTime()
	{
		myDeltaTime = Time.realtimeSinceStartup - lastRealtimeSeconds;
		lastRealtimeSeconds = Time.realtimeSinceStartup;
		globalTime += myDeltaTime;
		Shader.SetGlobalFloat("_RealTime", globalTime);
	}

	private void UpdateCPULoad()
	{
		Process currentProcess = Process.GetCurrentProcess();
		if (prevTotalProcessorTime == 0.0)
		{
			prevCheckingTime = currentProcess.StartTime;
		}
		double totalMilliseconds = currentProcess.TotalProcessorTime.TotalMilliseconds;
		DateTime now = DateTime.Now;
		CPULoad = (float)((totalMilliseconds - prevTotalProcessorTime) * 100.0 / (now.Subtract(prevCheckingTime).TotalMilliseconds * (double)Environment.ProcessorCount));
		prevCheckingTime = now;
		prevTotalProcessorTime = totalMilliseconds;
	}
}
