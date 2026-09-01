using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Replays
{
	public static readonly int MIN_SECONDS_PER_REPLAY = 15;

	public static readonly int DEFAULT_SECONDS_PER_REPLAY = 30;

	public static readonly int MAX_SECONDS_PER_REPLAY = 60;

	public static readonly float RECORD_TIME_AFTER_PASS_OR_FAIL = 1f;

	public static readonly float DEV_SOLUTION_ORTHOGRAPHIC_SCALE = 0.85f;

	public static readonly string REPLAYS_DIRECTORY = "Replays";

	public static string m_DefaultReplaysPath;

	private static float m_LastReplayUploadTime;

	public static void Init()
	{
		m_DefaultReplaysPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, REPLAYS_DIRECTORY));
		m_LastReplayUploadTime = float.MinValue;
	}

	public static List<string> GetListLocalizedQualityLevelNames()
	{
		return new List<string>
		{
			Localize.Get("UI_LOW"),
			Localize.Get("UI_MEDIUM"),
			Localize.Get("UI_HIGH"),
			Localize.Get("UI_ULTRA")
		};
	}

	public static string GetReplaysPath()
	{
		if (!string.IsNullOrEmpty(Profiles.m_ActiveProfile.m_ReplaysFolderOverride))
		{
			return Profiles.m_ActiveProfile.m_ReplaysFolderOverride;
		}
		return GetDefaultReplaysPath();
	}

	public static string GetDefaultReplaysPath()
	{
		return m_DefaultReplaysPath;
	}

	public static void RegisterUpload()
	{
		m_LastReplayUploadTime = Time.time;
	}

	public static bool ReplayUploadedInLastMinutes(int minutes)
	{
		int num = minutes * 60;
		return Time.time - m_LastReplayUploadTime < (float)num;
	}
}
