using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PineTesting
{
	public static bool isRunning;

	private static string fpsScene;

	private static float minFrameTime;

	private static float maxFrameTime;

	private static double avgFrameTime;

	private static int measureFrameTimeFrameCounter;

	private static float lastTime;

	public static void init()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i].Contains("-runTests"))
			{
				isRunning = true;
			}
		}
		if (isRunning)
		{
			string path = Application.persistentDataPath + "/PineTesting.log";
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}

	public static void report()
	{
		string name = SceneManager.GetActiveScene().name;
		File.AppendAllTextAsync(Application.persistentDataPath + "/PineTesting.log", $"{name} frameTimings min = {minFrameTime * 1000f:F2}ms, max = {maxFrameTime * 1000f:F2}ms, avg = {avgFrameTime * 1000.0:F2}ms\n");
	}

	public static void mark(string marker)
	{
		if (isRunning)
		{
			string name = SceneManager.GetActiveScene().name;
			File.AppendAllTextAsync(Application.persistentDataPath + "/PineTesting.log", $"{name}/{marker} timestamp = {Time.time:F2}s duration = {Time.time - lastTime:F2}s\n");
			lastTime = Time.time;
		}
	}

	public static void recordFPS()
	{
		if (isRunning)
		{
			string name = SceneManager.GetActiveScene().name;
			if (fpsScene != name)
			{
				fpsScene = name;
				measureFrameTimeFrameCounter = 0;
				minFrameTime = float.MaxValue;
				maxFrameTime = float.MinValue;
				avgFrameTime = 0.0;
			}
			if (measureFrameTimeFrameCounter > 600)
			{
				minFrameTime = Mathf.Min(minFrameTime, Time.deltaTime);
				maxFrameTime = Mathf.Max(maxFrameTime, Time.deltaTime);
				avgFrameTime += (double)Time.deltaTime / 600.0;
			}
			if (measureFrameTimeFrameCounter == 1200)
			{
				report();
			}
			measureFrameTimeFrameCounter++;
		}
	}
}
