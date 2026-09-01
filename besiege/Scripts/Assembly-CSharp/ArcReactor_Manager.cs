using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcReactor_Manager : MonoBehaviour
{
	[Serializable]
	public class FPSInfo
	{
		public float minFps;

		public int priority;
	}

	public FPSInfo[] fpsPriorities;

	public float updateInterval = 1f;

	public int defaultPriority;

	protected List<ArcReactor_Arc> arcSystems = new List<ArcReactor_Arc>();

	protected List<ArcReactor_Arc> arcSystemsForDeletion = new List<ArcReactor_Arc>();

	protected float accum;

	protected int frames;

	protected float timeleft;

	protected int priority;

	protected FPSInfo[] fpsScales;

	protected float fps;

	public static ArcReactor_Manager Instance { get; private set; }

	public void AddArcSystem(ArcReactor_Arc arcSystem)
	{
		arcSystems.Add(arcSystem);
		arcSystem.SetPerformancePriority(priority);
	}

	public void DeleteArcSystem(ArcReactor_Arc arcSystem)
	{
		arcSystemsForDeletion.Add(arcSystem);
	}

	protected void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			return;
		}
		Debug.LogError("More than one instance of ArcReactor_Manager is active. Disabling additional instance");
		base.enabled = false;
	}

	protected int GetPriority(float fps)
	{
		for (int i = 0; i < fpsScales.Length; i++)
		{
			if (fps >= fpsScales[i].minFps)
			{
				return fpsScales[i].priority;
			}
		}
		return defaultPriority;
	}

	protected void Start()
	{
		priority = defaultPriority;
		fpsScales = fpsPriorities.OrderBy((FPSInfo fI) => 0f - fI.minFps).ToArray();
	}

	protected void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if (!((double)timeleft <= 0.0))
		{
			return;
		}
		foreach (ArcReactor_Arc item in arcSystemsForDeletion)
		{
			arcSystems.Remove(item);
		}
		arcSystemsForDeletion.Clear();
		fps = accum / (float)frames;
		timeleft += updateInterval;
		accum = 0f;
		frames = 0;
		priority = GetPriority(fps);
		foreach (ArcReactor_Arc arcSystem in arcSystems)
		{
			if (arcSystem == null)
			{
				DeleteArcSystem(arcSystem);
			}
			else
			{
				arcSystem.SetPerformancePriority(priority);
			}
		}
	}
}
