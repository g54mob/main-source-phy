using System;
using System.IO;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
	public int screenshotScaler = 2;

	private string screenDir;

	private void Awake()
	{
		screenDir = Path.Combine(StaticSettings.DataPath, "Screenshots");
	}

	private void Start()
	{
		if (!Directory.Exists(screenDir))
		{
			try
			{
				Directory.CreateDirectory(screenDir);
			}
			catch
			{
				Debug.LogError("Missing Read/Write permission");
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown("f12"))
		{
			TakeScreen();
		}
	}

	private void TakeScreen()
	{
		DateTime now = DateTime.Now;
		string text = "Besiege_" + now.ToString("yyyy_MM_dd") + "_" + now.ToString("hh_mm_ss");
		string filename = Path.Combine(screenDir, text + ".png");
		Application.CaptureScreenshot(filename, screenshotScaler);
	}
}
