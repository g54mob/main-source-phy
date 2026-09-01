using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
	public Text text;

	private Stopwatch stopWatch = new Stopwatch();

	private DateTime lastTime = DateTime.Now;

	public int numSamplesToAverage = 50;

	private List<float> fpsHistory = new List<float>();

	private List<float> fpxHistory = new List<float>();

	private void Start()
	{
		stopWatch.Start();
	}

	private void Update()
	{
		if ((bool)text)
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - lastTime;
			lastTime = now;
			while (fpsHistory.Count >= 50)
			{
				fpsHistory.RemoveAt(0);
			}
			fpsHistory.Add(1f / ((float)timeSpan.TotalSeconds + 1E-06f));
			float num = fpsHistory.Average();
			text.text = $"PFS: {num:0.00} \r\n";
			stopWatch.Stop();
			long frequency = Stopwatch.Frequency;
			double num2 = (double)stopWatch.ElapsedTicks / (double)frequency;
			stopWatch.Reset();
			stopWatch.Start();
			while (fpxHistory.Count >= 50)
			{
				fpxHistory.RemoveAt(0);
			}
			fpxHistory.Add(1f / ((float)num2 + 1E-06f));
			float num3 = fpxHistory.Average();
			text.text += $"PFX: {num3:0.00}";
		}
	}
}
